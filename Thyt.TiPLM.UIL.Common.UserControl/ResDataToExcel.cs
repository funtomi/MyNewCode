namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using DataFileProvider;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
    using Thyt.TiPLM.UIL.Interop.Office;

    public class ResDataToExcel
    {
        private ArrayList attrList = new ArrayList();
        private ArrayList attrOuter = new ArrayList();
        private ArrayList attrShow = new ArrayList();
        private ArrayList attrSort = new ArrayList();
        private bool b_refType;
        private string clsName = "";
        private DEResFolder curFolder = new DEResFolder();
        private emResourceType emResType;
        private int EndNum;
        private IExcelPipe excelPipe;
        private Hashtable HT_AttrIsView = new Hashtable();
        private bool IsMultiPageOut = true;
        private ListView lvw = new ListView();
        private int showNum = 50;
        private int StartNum;
        private string strSort = "";
        private DataSet theDataSet = new DataSet();
        private string tmpPath = "";
        private int TotalNum;
        private int TotalPage;
        private Guid userOid = Guid.Empty;

        public ResDataToExcel(int i_pageRecordNum)
        {
            this.showNum = i_pageRecordNum;
            this.userOid = ClientData.LogonUser.Oid;
            try
            {
                string str = (string) Registry.ClassesRoot.OpenSubKey(@"Excel.Application\CurVer").GetValue("");
                string str2 = str.Substring(str.IndexOf(".", (int) (str.IndexOf(".") + 1)) + 1);
                System.Type type = Assembly.LoadFrom(ConstConfig.CurrentPath + @"\Office" + str2 + @"\Thyt.TiPLM.UIL.Interop.Office" + str2 + ".dll").GetType("Thyt.TiPLM.UIL.Interop.Office" + str2 + ".ExcelPipe");
                this.excelPipe = (IExcelPipe) Activator.CreateInstance(type);
                if (this.excelPipe == null)
                {
                    throw new Exception("无法创建Excel实例");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("获取Excel的版本号或者载入动态链接库出错。", exception);
            }
            try
            {
                this.excelPipe.RunExcel(false);
                this.excelPipe.AddWorkBook();
            }
            catch (Exception exception2)
            {
                this.excelPipe.QuitAll(null);
                throw exception2;
            }
        }

        private void ConvertToListView()
        {
            DataView defaultView = null;
            DataTable table = new DataTable();
            if (this.IsMultiPageOut)
            {
                this.lvw.Items.Clear();
            }
            defaultView = this.theDataSet.Tables[0].DefaultView;
            if (defaultView.Table.Rows.Count != 0)
            {
                int num = 0;
                foreach (DataRowView view2 in defaultView)
                {
                    int num2 = 0;
                    string columnName = "";
                    table.NewRow();
                    ListViewItem item = new ListViewItem();
                    for (int i = 0; i < defaultView.Table.Columns.Count; i++)
                    {
                        foreach (DEMetaAttribute attribute in this.attrList)
                        {
                            if (this.ISDefAttrViewable(attribute) && (defaultView.Table.Columns[i].ColumnName == ("PLM_" + attribute.Name)))
                            {
                                columnName = "PLM_" + attribute.Name;
                                if (this.b_refType)
                                {
                                    if (((attribute.Name == "R_CREATOR") || (attribute.Name == "LATESTUPDATOR")) && (attribute.DataType == 8))
                                    {
                                        string principalName = PrincipalRepository.GetPrincipalName(new Guid((byte[]) view2[columnName]));
                                        if (num2 == 0)
                                        {
                                            item.Text = principalName;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(principalName);
                                        }
                                    }
                                    else if (attribute.Name == "M_CLASS")
                                    {
                                        string classname = view2[columnName].ToString();
                                        if (num2 == 0)
                                        {
                                            item.Text = ModelContext.MetaModel.GetClassLabel(classname);
                                        }
                                        else
                                        {
                                            item.SubItems.Add(ModelContext.MetaModel.GetClassLabel(classname));
                                        }
                                    }
                                    else if (attribute.Name == "R_REVSTATE")
                                    {
                                        string revStateLabel = DEItemRevision2.GetRevStateLabel(Convert.ToChar(view2[columnName].ToString().Trim()));
                                        if (num2 == 0)
                                        {
                                            item.Text = revStateLabel;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(revStateLabel);
                                        }
                                    }
                                    else if (attribute.Name == "M_STATE")
                                    {
                                        string str5 = view2[columnName].ToString().Trim();
                                        string text = "无状态";
                                        switch (str5)
                                        {
                                            case "I":
                                                text = "检入";
                                                break;

                                            case "O":
                                                text = "检出";
                                                break;

                                            case "A":
                                                text = "废弃";
                                                break;

                                            case "R":
                                                text = "定版";
                                                break;

                                            case "N":
                                                text = "无状态";
                                                break;
                                        }
                                        if (num2 == 0)
                                        {
                                            item.Text = text;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(text);
                                        }
                                    }
                                    else if (attribute.DataType2 == PLMDataType.Guid)
                                    {
                                        string str7 = "";
                                        if (((view2[columnName] != null) && !view2.Row.IsNull(columnName)) && (((attribute.SpecialType2 == PLMSpecialType.OrganizationType) || (attribute.SpecialType2 == PLMSpecialType.RoleType)) || (attribute.SpecialType2 == PLMSpecialType.UserType)))
                                        {
                                            str7 = PrincipalRepository.GetPrincipalName(new Guid((byte[]) view2[columnName]));
                                        }
                                        if (num2 == 0)
                                        {
                                            item.Text = str7;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(str7);
                                        }
                                    }
                                    else
                                    {
                                        string str8 = Convert.ToString(view2[columnName]);
                                        if (attribute.DataType2 == PLMDataType.DateTime)
                                        {
                                            if ((view2[columnName] != null) && !view2.Row.IsNull(columnName))
                                            {
                                                DEMetaAttribute exAttributeByOid = ModelContext.GetExAttributeByOid(this.clsName, attribute.Oid);
                                                string format = "yyyy年MM月dd日 HH:mm:ss";
                                                if (((exAttributeByOid != null) && (exAttributeByOid.GetEditorSetup().format != null)) && (exAttributeByOid.GetEditorSetup().format.Length > 0))
                                                {
                                                    format = exAttributeByOid.GetEditorSetup().format.Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                                                }
                                                str8 = Convert.ToDateTime(view2[columnName]).ToString(format);
                                            }
                                            else
                                            {
                                                str8 = "";
                                            }
                                        }
                                        if (num2 == 0)
                                        {
                                            item.Text = str8;
                                        }
                                        else
                                        {
                                            item.SubItems.Add(str8);
                                        }
                                    }
                                }
                                else if (num2 == 0)
                                {
                                    item.Text = view2[columnName].ToString().Trim();
                                }
                                else
                                {
                                    item.SubItems.Add(view2[columnName].ToString().Trim());
                                }
                                item.Tag = view2;
                                num2++;
                                break;
                            }
                        }
                    }
                    this.lvw.Items.Add(item);
                    num++;
                }
            }
        }

        private void DoOutFile(string strFileName)
        {
            ArrayList files = new ArrayList();
            for (int i = 0; i < this.TotalNum; i++)
            {
                int num2 = 0;
                int num3 = 0;
                int num4 = 0;
                if ((i >= (this.StartNum - 1)) && (i <= (this.EndNum - 1)))
                {
                    num4++;
                    if (i == 0)
                    {
                        num2 = i * this.showNum;
                    }
                    else
                    {
                        num2 = (i * this.showNum) + 1;
                    }
                    num3 = (i + 1) * this.showNum;
                    if (ResFunc.IsOnlineOutRes(this.curFolder.ClassOid))
                    {
                        ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, num2, num3, this.strSort);
                        ResFunc.ConvertOuterDSHead(this.theDataSet, this.attrList, this.attrOuter);
                    }
                    else if (ResFunc.IsRefRes(this.curFolder.ClassOid))
                    {
                        PLSPL plspl = new PLSPL();
                        DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                        DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                        this.clsName = class3.Name;
                        ArrayList attrList = new ArrayList();
                        foreach (DEMetaAttribute attribute in this.attrList)
                        {
                            if (this.ISDefAttrViewable(attribute))
                            {
                                attrList.Add(attribute);
                            }
                        }
                        this.theDataSet = plspl.GetSPLNumDataSet(class3.Name, attrList, ClientData.LogonUser.Oid, num2, num3, this.curFolder.FilterString, this.curFolder.FilterValue, this.strSort);
                        this.b_refType = true;
                    }
                    else if (ResFunc.IsTabRes(this.curFolder.ClassOid))
                    {
                        new ArrayList();
                        ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, num2, num3, this.strSort);
                    }
                    if (this.theDataSet != null)
                    {
                        if (num4 == 1)
                        {
                            this.InitLvShowHeader();
                        }
                        this.ConvertToListView();
                        if (this.IsMultiPageOut)
                        {
                            string str = Path.Combine(this.tmpPath, "page" + ((i + 1)).ToString());
                            this.ToXlsFile(this.lvw, str, "page" + ((i + 1)).ToString());
                            files.Add(str);
                        }
                    }
                }
            }
            if (!this.IsMultiPageOut)
            {
                string str2 = Path.Combine(this.tmpPath, "page1");
                this.ToXlsFile(this.lvw, str2, "page1");
                files.Add(str2);
            }
            if (files.Count > 0)
            {
                try
                {
                    this.excelPipe.UnionBooks(files);
                    this.excelPipe.RemoveWorkSheet(1);
                }
                catch (Exception exception)
                {
                    MessageBoxPLM.Show(exception.Message);
                }
                finally
                {
                    try
                    {
                        this.excelPipe.QuitAll(strFileName);
                    }
                    catch (Exception exception2)
                    {
                        MessageBoxPLM.Show(exception2.Message);
                    }
                }
            }
        }

        public DataSet GetDataFromExcel(string strFileName, out ArrayList al_error)
        {
            DataSet ds = null;
            ExcelProvider.GetExcelData(strFileName, out ds, out al_error);
            return ds;
        }

        private int GetResDataCount()
        {
            int num = 0;
            if (ResFunc.IsOnlineOutRes(this.curFolder.ClassOid))
            {
                return ResFunc.GetDataCount(this.curFolder, this.attrList, this.attrOuter, emResourceType.OutSystem);
            }
            if (ResFunc.IsRefRes(this.curFolder.ClassOid))
            {
                PLSPL plspl = new PLSPL();
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                ArrayList attrList = new ArrayList();
                foreach (DEMetaAttribute attribute in this.attrList)
                {
                    if (this.ISDefAttrViewable(attribute))
                    {
                        attrList.Add(attribute);
                    }
                }
                return plspl.GetSPLCount(class3.Name, attrList, ClientData.LogonUser.Oid, this.curFolder.FilterString, this.curFolder.FilterValue);
            }
            if (ResFunc.IsTabRes(this.curFolder.ClassOid))
            {
                ArrayList list2 = new ArrayList();
                num = ResFunc.GetDataCount(this.curFolder, list2, this.attrOuter, emResourceType.Customize);
            }
            return num;
        }

        private void InitLvShowHeader()
        {
            ArrayList list = new ArrayList();
            this.lvw.Columns.Clear();
            int item = 0;
            for (int i = 0; i < this.theDataSet.Tables[0].Columns.Count; i++)
            {
                foreach (DEMetaAttribute attribute in this.attrList)
                {
                    if (this.ISDefAttrViewable(attribute) && (this.theDataSet.Tables[0].Columns[i].ColumnName == ("PLM_" + attribute.Name)))
                    {
                        System.Type type = typeof(string);
                        if (((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6)))
                        {
                            if (!list.Contains(item))
                            {
                                list.Add(item);
                            }
                            type = typeof(decimal);
                        }
                        ColumnHeader header = new ColumnHeader {
                            Text = attribute.Label,
                            Width = 10,
                            Tag = type
                        };
                        this.lvw.Columns.Add(header);
                        item++;
                    }
                }
            }
        }

        private void InitResInfo()
        {
            ArrayList showAttrList = ResFunc.GetShowAttrList(this.curFolder, emTreeType.NodeTree);
            this.attrList = ResFunc.CloneMetaAttrLst(showAttrList);
            this.attrSort = ResFunc.GetSortAttrList(this.curFolder);
            this.attrOuter = ResFunc.GetOuterAttr(this.curFolder);
            this.clsName = this.curFolder.ClassName;
            this.InitResStatus();
            this.InitSortList(true);
            this.InitShowAttrLst();
            this.SetAttrDataType();
        }

        private void InitResStatus()
        {
            if (ResFunc.IsOnlineOutRes(this.curFolder.ClassOid))
            {
                this.emResType = emResourceType.OutSystem;
            }
            else if (ResFunc.IsRefRes(this.curFolder.ClassOid))
            {
                this.emResType = emResourceType.Standard;
                this.b_refType = true;
            }
            else if (ResFunc.IsTabRes(this.curFolder.ClassOid))
            {
                this.emResType = emResourceType.Customize;
            }
            else
            {
                this.emResType = emResourceType.PLM;
            }
        }

        private void InitShowAttrLst()
        {
            this.attrShow = new PLReference().GetDefAttrLst(this.curFolder.Oid, emTreeType.NodeTree);
        }

        private void InitSortList(bool b_first)
        {
            this.strSort = "";
            string userOption = (string) ClientData.GetUserOption(this.userOid + this.curFolder.Oid.ToString());
            if ((b_first && (userOption != null)) && (userOption.Trim().Length > 0))
            {
                if (userOption != "nothing")
                {
                    this.strSort = userOption;
                }
            }
            else
            {
                int num = 0;
                if (this.attrSort.Count > 0)
                {
                    for (int i = 0; i < this.attrSort.Count; i++)
                    {
                        DEDefSort sort = (DEDefSort) this.attrSort[i];
                        switch (this.emResType)
                        {
                            case emResourceType.PLM:
                                foreach (DEMetaAttribute attribute4 in this.attrList)
                                {
                                    if (attribute4.Oid == sort.ATTROID)
                                    {
                                        string str8 = sort.ISASC ? " asc" : " desc";
                                        string str9 = "PLM_CUS_" + this.curFolder.ClassName;
                                        if (num == 0)
                                        {
                                            this.strSort = " order by " + str9 + ".PLM_" + attribute4.Name + str8;
                                        }
                                        else
                                        {
                                            string strSort = this.strSort;
                                            this.strSort = strSort + " , " + str9 + ".PLM_" + attribute4.Name + str8;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.Customize:
                                foreach (DEMetaAttribute attribute in this.attrList)
                                {
                                    if (attribute.Oid == sort.ATTROID)
                                    {
                                        string str2 = sort.ISASC ? " asc" : " desc";
                                        string str3 = "PLM_CUS_" + this.curFolder.ClassName;
                                        if (num == 0)
                                        {
                                            this.strSort = " order by " + str3 + ".PLM_" + attribute.Name + str2;
                                        }
                                        else
                                        {
                                            string str10 = this.strSort;
                                            this.strSort = str10 + " , " + str3 + ".PLM_" + attribute.Name + str2;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.OutSystem:
                                foreach (DEOuterAttribute attribute2 in this.attrOuter)
                                {
                                    if (attribute2.FieldOid == sort.ATTROID)
                                    {
                                        string str4 = sort.ISASC ? " asc" : " desc";
                                        if (num == 0)
                                        {
                                            this.strSort = " order by " + attribute2.OuterAttrName + str4;
                                        }
                                        else
                                        {
                                            this.strSort = this.strSort + " , " + attribute2.OuterAttrName + str4;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.Standard:
                            {
                                new PLSPL();
                                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                                DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                                if (sort.ATTROID == Guid.Empty)
                                {
                                    ClientData.SetUserOption(this.userOid + this.curFolder.Oid.ToString(), "nothing");
                                    ClientData.SetUserOption(this.userOid + this.curFolder.Oid.ToString() + "_def", "nothing");
                                    return;
                                }
                                foreach (DEMetaAttribute attribute3 in this.attrList)
                                {
                                    if (attribute3.Oid == sort.ATTROID)
                                    {
                                        string str5 = sort.ISASC ? " asc" : " desc";
                                        string str6 = "PLM_CUS_".Replace("_CUS_", "_CUSV_") + class3.Name;
                                        string str7 = "PLM_PSM_ITEMMASTER_REVISION";
                                        if (num == 0)
                                        {
                                            if (attribute3.Name.StartsWith("M_") || attribute3.Name.StartsWith("R_"))
                                            {
                                                this.strSort = " order by " + str7 + ".PLM_" + attribute3.Name + str5;
                                            }
                                            else
                                            {
                                                this.strSort = " order by " + str6 + ".PLM_" + attribute3.Name + str5;
                                            }
                                        }
                                        else if (attribute3.Name.StartsWith("M_") || attribute3.Name.StartsWith("R_"))
                                        {
                                            string str11 = this.strSort;
                                            this.strSort = str11 + " , " + str7 + ".PLM_" + attribute3.Name + str5;
                                        }
                                        else
                                        {
                                            string str12 = this.strSort;
                                            this.strSort = str12 + " , " + str6 + ".PLM_" + attribute3.Name + str5;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

        private bool ISDefAttrViewable(DEMetaAttribute demattr)
        {
            bool flag = false;
            if (this.attrShow.Count > 0)
            {
                if (this.HT_AttrIsView.ContainsKey(this.curFolder.Oid + "|" + demattr.Oid))
                {
                    return Convert.ToBoolean(this.HT_AttrIsView[this.curFolder.Oid + "|" + demattr.Oid]);
                }
                foreach (DEDefAttr attr in this.attrShow)
                {
                    if (demattr.Oid == attr.ATTROID)
                    {
                        this.HT_AttrIsView.Add(this.curFolder.Oid + "|" + demattr.Oid, attr.ISVISUAL);
                        return attr.ISVISUAL;
                    }
                }
                return flag;
            }
            return demattr.IsViewable;
        }

        private void outPageExcel(object objs)
        {
            ArrayList list = (ArrayList) objs;
            IProgressCallback callback = list[0] as IProgressCallback;
            string strFileName = list[1] as string;
            try
            {
                callback.Begin(0, 100);
                callback.SetText("正在生成{'资源'}的导出文件……");
                this.DoOutFile(strFileName);
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
            finally
            {
                callback.End();
            }
        }

        public void Output(DEResFolder curResFolder)
        {
            if (curResFolder != null)
            {
                this.curFolder = curResFolder;
                this.InitResInfo();
                this.TotalNum = this.GetResDataCount();
                if (this.TotalNum == 0)
                {
                    MessageBoxPLM.Show("资源的记录数为零，不能导出", "资源数据导出", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    this.TotalPage = ((this.TotalNum % this.showNum) == 0) ? (this.TotalNum / this.showNum) : ((this.TotalNum / this.showNum) + 1);
                    this.attrShow = new PLReference().GetDefAttrLst(this.curFolder.Oid, emTreeType.NodeTree);
                    foreach (DEDefAttr attr in this.attrShow)
                    {
                        if (!attr.ISVISUAL)
                        {
                            for (int i = this.attrList.Count - 1; i >= 0; i--)
                            {
                                DEMetaAttribute attribute = this.attrList[i] as DEMetaAttribute;
                                if (attribute.Oid == attr.ATTROID)
                                {
                                    this.attrList.RemoveAt(i);
                                }
                            }
                        }
                    }
                    FrmOutputOption option = new FrmOutputOption(this.TotalPage, this.attrList);
                    if (option.ShowDialog() == DialogResult.OK)
                    {
                        SaveFileDialog dialog;
                        this.StartNum = option.StartPage;
                        this.EndNum = option.EndPage;
                        this.IsMultiPageOut = option.IsMultiPage;
                        if (this.saveFile(out dialog))
                        {
                            IProgressCallback progressWindow = ClientData.GetProgressWindow();
                            ArrayList state = new ArrayList {
                                progressWindow,
                                dialog.FileName
                            };
                            ThreadPool.QueueUserWorkItem(new WaitCallback(this.outPageExcel), state);
                            progressWindow.ShowWindow();
                        }
                    }
                }
            }
        }

        private bool saveFile(out SaveFileDialog sf)
        {
            this.tmpPath = ConstConfig.CurrentPath + @"\ResOutput";
            if (Directory.Exists(this.tmpPath))
            {
                Directory.Delete(this.tmpPath, true);
                Thread.Sleep(100);
            }
            while (Directory.Exists(this.tmpPath))
            {
            }
            Directory.CreateDirectory(this.tmpPath);
            sf = new SaveFileDialog();
            sf.Filter = "Excel文件|*.xls";
            sf.DefaultExt = "xls";
            bool canWrite = true;
            do
            {
                if (sf.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }
                if (!File.Exists(sf.FileName))
                {
                    return true;
                }
                try
                {
                    FileStream stream = new FileStream(sf.FileName, FileMode.Open, FileAccess.Write);
                    canWrite = stream.CanWrite;
                    stream.Close();
                }
                catch (Exception)
                {
                    canWrite = false;
                }
                if (!canWrite)
                {
                    MessageBoxPLM.Show("无法保存文件" + sf.FileName + ", 文件存在且为“只读”。\n\n请用其他文件名保存，或者选择将文件存至其他地方。", "资源输出");
                }
            }
            while (!canWrite);
            return true;
        }

        private void SetAttrDataType()
        {
            foreach (DEMetaAttribute attribute in this.attrList)
            {
                foreach (DEOuterAttribute attribute2 in this.attrOuter)
                {
                    if (attribute.Oid == attribute2.FieldOid)
                    {
                        attribute.DataType = attribute2.DataType;
                        break;
                    }
                }
            }
        }

        private void ToXlsFile(ListView lv_items, string strFileName, string strPageName)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable tab = new DataTable(strPageName);
            for (int i = 1; i <= lv_items.Columns.Count; i++)
            {
                System.Type tag = lv_items.Columns[i - 1].Tag as System.Type;
                tab.Columns.Add(lv_items.Columns[i - 1].Text, tag);
            }
            foreach (ListViewItem item in lv_items.Items)
            {
                DataRow row = tab.NewRow();
                for (int j = 1; j <= lv_items.Columns.Count; j++)
                {
                    System.Type type2 = lv_items.Columns[j - 1].Tag as System.Type;
                    if (j == 1)
                    {
                        if (string.IsNullOrEmpty(item.Text))
                        {
                            row[lv_items.Columns[j - 1].Text] = DBNull.Value;
                        }
                        else if (type2 == typeof(decimal))
                        {
                            row[lv_items.Columns[j - 1].Text] = Convert.ToDecimal(item.Text);
                        }
                        else
                        {
                            row[lv_items.Columns[j - 1].Text] = item.Text;
                        }
                    }
                    else if (string.IsNullOrEmpty(item.SubItems[j - 1].Text))
                    {
                        row[lv_items.Columns[j - 1].Text] = DBNull.Value;
                    }
                    else if (type2 == typeof(decimal))
                    {
                        row[lv_items.Columns[j - 1].Text] = Convert.ToDecimal(item.SubItems[j - 1].Text);
                    }
                    else
                    {
                        row[lv_items.Columns[j - 1].Text] = item.SubItems[j - 1].Text;
                    }
                }
                tab.Rows.Add(row);
            }
            ExcelProvider.SetExcelData(strFileName, tab);
            Cursor.Current = Cursors.Default;
        }
    }
}

