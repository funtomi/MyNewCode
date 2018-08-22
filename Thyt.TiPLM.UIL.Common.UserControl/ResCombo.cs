    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class ResCombo : UltraTextEditor
    {
        private ArrayList al_guid;
        private ArrayList attrOuter;
        private ArrayList Attrs;
        private bool b_AllowLike;
        private bool b_DenyByHand;
        private bool b_isRefCls;
        private bool b_ReadOnly;
        private string className;
        private Guid classOid;
        private string comColumn;
        public DEMetaAttribute deMetaAttri;
        private ResDropListHandler dlhandler;
        private DateTime Dt_ClickEnd;
        private DateTime Dt_ClickStart;
        private emResourceType emResType;
        private Hashtable Ht_resParam;
        private int i_PageSearchNum;
        private bool IsFromEdit;
        private DataRowView LocateDRV;
        private DataRowView myDRV;
        private DataSet myds;
        private DEResFolder myFolder;
        private DataView myView;
        private string OrgText;
        private Guid resOid;
        private static ulong Stamp;
        private UCResGrid ucUser;
        private Guid userOid;

        public event ResDropListHandler DropListChanged;

        public event SelectResHandler ResTextChanged;

        public ResCombo()
        {
            this.comColumn = "选_中";
            this.i_PageSearchNum = 0x1388;
            this.IsFromEdit = true;
            this.Attrs = new ArrayList();
            this.attrOuter = new ArrayList();
            this.OrgText = "";
            this.resOid = Guid.Empty;
            this.al_guid = new ArrayList();
            this.Dt_ClickStart = DateTime.Now;
            this.Dt_ClickEnd = DateTime.Now;
            this.InitializeComponent();
            this.userOid = ClientData.LogonUser.Oid;
        }

        public ResCombo(Guid classOid, DEMetaAttribute metaAttr) : this()
        {
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(classOid);
            this.className = class2.Name;
            this.InitializeConfig();
            this.SetFolder();
            this.GetClsAttrLst();
            this.ucUser.Width = base.Width;
        }

        public ResCombo(string clsName, DEMetaAttribute metaAttr) : this()
        {
            this.className = clsName;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(clsName);
            this.classOid = class2.Oid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            this.InitializeConfig();
            this.SetFolder();
            this.GetClsAttrLst();
            this.ucUser.Width = base.Width;
        }

        public ResCombo(Guid classOid, DEMetaAttribute metaAttr, Hashtable ht_param) : this()
        {
            this.Ht_resParam = ht_param;
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(classOid);
            this.className = class2.Name;
            this.InitializeConfig();
            this.CreateFolder();
            this.GetClsAttrLst();
            this.SetFolder(ht_param);
            this.ucUser.Width = base.Width;
        }

        public ResCombo(string clsName, DEMetaAttribute metaAttr, Hashtable ht_param) : this()
        {
            this.Ht_resParam = ht_param;
            this.className = clsName;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(clsName);
            this.classOid = class2.Oid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            this.InitializeConfig();
            this.CreateFolder();
            this.GetClsAttrLst();
            this.SetFolder(ht_param);
            this.ucUser.Width = base.Width;
        }

        public ResCombo(Guid classOid, DEMetaAttribute metaAttr, Hashtable ht_param, bool AllowLike) : this()
        {
            this.b_AllowLike = AllowLike;
            this.Ht_resParam = ht_param;
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(classOid);
            this.className = class2.Name;
            this.InitializeConfig();
            this.CreateFolder();
            this.GetClsAttrLst();
            this.SetFolder(ht_param);
            this.ucUser.Width = base.Width;
        }

        public ResCombo(string clsName, DEMetaAttribute metaAttr, Hashtable ht_param, bool AllowLike) : this()
        {
            this.b_AllowLike = AllowLike;
            this.Ht_resParam = ht_param;
            this.className = clsName;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(clsName);
            this.classOid = class2.Oid;
            this.deMetaAttri = metaAttr;
            this.b_DenyByHand = metaAttr.IsResFromDataBase;
            this.InitializeConfig();
            this.CreateFolder();
            this.GetClsAttrLst();
            this.SetFolder(ht_param);
            this.ucUser.Width = base.Width;
        }

        private int ComputTotalPageNum(int i_total, int i_show){return 
            (((i_total % i_show) == 0) ? (i_total / i_show) : ((i_total / i_show) + 1));
        }
        private Hashtable ConvertToResKey(Hashtable ht_param)
        {
            Hashtable hashtable = new Hashtable();
            if (this.emResType == emResourceType.Standard)
            {
                ArrayList fixedAttrList = ResFunc.GetFixedAttrList();
                if (ht_param.Count > 0)
                {
                    IDictionaryEnumerator enumerator = ht_param.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        string name = enumerator.Key.ToString();
                        foreach (DEMetaAttribute attribute in fixedAttrList)
                        {
                            if (attribute.Name.EndsWith(name))
                            {
                                name = attribute.Name;
                                break;
                            }
                        }
                        if (hashtable.ContainsKey(name))
                        {
                            hashtable[name] = enumerator.Value;
                        }
                        else
                        {
                            hashtable.Add(name, enumerator.Value);
                        }
                    }
                }
                return hashtable;
            }
            if (this.emResType != emResourceType.OutSystem)
            {
                return ht_param;
            }
            if (ht_param.Count > 0)
            {
                IDictionaryEnumerator enumerator2 = ht_param.GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    string key = enumerator2.Key.ToString();
                    foreach (DEMetaAttribute attribute2 in this.Attrs)
                    {
                        if ((key == ("M_" + attribute2.Name)) || (key == ("R_" + attribute2.Name)))
                        {
                            key = attribute2.Name;
                            break;
                        }
                    }
                    hashtable.Add(key, enumerator2.Value);
                }
            }
            return hashtable;
        }

        private void CreateFolder()
        {
            this.myFolder = new DEResFolder();
            this.myFolder.Oid = this.classOid;
            this.myFolder.ClassOid = this.classOid;
            this.myFolder.ClassName = this.className;
            this.InitResStatus();
        }

   
        private ArrayList GetAttributes(string classname){return
            ModelContext.MetaModel.GetAttributes(classname);
        }
        public object GetAttrValue(string str_attrname)
        {
            object obj2 = new object();
            if (this.myDRV == null)
            {
                return null;
            }
            if (str_attrname == null)
            {
                return null;
            }
            if (str_attrname == "")
            {
                return null;
            }
            if (str_attrname.Equals("对象全局唯一标识"))
            {
                string str = "PLM_OID";
                if ((this.myDRV[str] != null) && (this.myDRV[str].ToString() != ""))
                {
                    obj2 = this.myDRV[str];
                }
                return obj2;
            }
            string str2 = str_attrname;
            string str3 = str2;
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                if (str_attrname.IndexOf("[" + attribute.Name + "]") > -1)
                {
                    string str4 = "PLM_" + attribute.Name;
                    str2 = str2.Replace("[" + attribute.Name + "]", Convert.ToString(this.myDRV[str4].ToString()));
                    str3 = str3.Replace("[" + attribute.Name + "]", "");
                }
            }
            if (this.b_isRefCls)
            {
                string name = "PLM_M_ID";
                if (this.myDRV.DataView.Table.Columns.Contains(name))
                {
                    str2 = str2.Replace("[ID]", Convert.ToString(this.myDRV[name]));
                    str3 = str3.Replace("[ID]", "");
                }
            }
            if (str2 == str3)
            {
                str2 = "";
            }
            return str2;
        }

        public object GetAttrValue(string itemClassName, string itemAttribute)
        {
            DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(itemClassName, itemAttribute);
            if (((attribute != null) && (attribute.LinkedResClass != Guid.Empty)) && (ModelContext.MetaModel.GetClass(attribute.LinkedResClass).Name == this.className))
            {
                return this.GetAttrValue(attribute.Combination);
            }
            return null;
        }

        private void GetClsAttrLst()
        {
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.myFolder.ClassOid);
            if (class2.IsTableResClass)
            {
                this.Attrs = this.GetAttributes(this.myFolder.ClassName);
            }
            if (class2.IsRefResClass)
            {
                DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                this.Attrs = this.GetAttributes(class3.Name);
                ArrayList c = new ArrayList();
                c = ResFunc.GetFixedAttrList();
                this.Attrs.AddRange(c);
            }
            if (class2.IsOuterResClass)
            {
                this.Attrs = this.GetAttributes(this.myFolder.ClassName);
                this.attrOuter = ResFunc.GetOuterAttr(this.myFolder);
                this.SetAttrDataType(this.Attrs, this.attrOuter);
            }
        }

        public int GetDataCount()
        {
            int num = 0;
            if (this.myFolder == null)
            {
                return 0;
            }
            if (ResFunc.IsOnlineOutRes(this.myFolder.ClassOid))
            {
                return ResFunc.GetDataCount(this.myFolder, this.Attrs, this.attrOuter, emResourceType.OutSystem);
            }
            if (ResFunc.IsRefRes(this.myFolder.ClassOid))
            {
                this.b_isRefCls = true;
                PLSPL plspl = new PLSPL();
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.myFolder.ClassOid);
                DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                this.className = class3.Name;
                return plspl.GetSPLCount(class3.Name, this.Attrs, this.userOid, this.myFolder.FilterString, this.myFolder.FilterValue);
            }
            if (ResFunc.IsTabRes(this.myFolder.ClassOid))
            {
                ArrayList list = new ArrayList();
                num = ResFunc.GetDataCount(this.myFolder, list, this.attrOuter, emResourceType.Customize);
            }
            return num;
        }

        private string GetResKeyByParameter(string str_key)
        {
            string str = "";
            string str2 = str_key;
            string oldValue = "[";
            int num = (str2.Length - str2.Replace(oldValue, "").Length) / oldValue.Length;
            if (num == 1)
            {
                str = str2.Substring(1, str2.Length - 2);
            }
            if (num <= 1)
            {
                return str;
            }
            string str4 = "";
            if (str2.StartsWith("["))
            {
                str4 = str2.Substring(1, str2.Length - 1);
            }
            if (str2.EndsWith("]"))
            {
                str4 = str4.Substring(0, str4.Length - 1);
            }
            return str4.Replace("][", "||").Replace("]", "||'").Replace("[", "'||");
        }

        public string GetResourceID(Guid resOID, string clsName)
        {
            string str = "";
            int dataCount = 0;
            int num2 = 0;
            dataCount = this.GetDataCount();
            if (dataCount != 0)
            {
                num2 = this.ComputTotalPageNum(dataCount, this.i_PageSearchNum);
                for (int i = 0; i < num2; i++)
                {
                    this.InitializeData(i * this.i_PageSearchNum, (i + 1) * this.i_PageSearchNum);
                    DataSet myds = this.myds;
                    if (resOID == Guid.Empty)
                    {
                        return str;
                    }
                    if ((myds == null) || (myds.Tables.Count <= 0))
                    {
                        return str;
                    }
                    if (myds.Tables[0].Rows.Count <= 0)
                    {
                        return str;
                    }
                    try
                    {
                        foreach (DataRow row in myds.Tables[0].Rows)
                        {
                            Guid guid = new Guid((byte[]) row["PLM_OID"]);
                            if (resOID == guid)
                            {
                                return row["PLM_ID"].ToString();
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("error!" + exception.Message);
                    }
                }
            }
            return str;
        }

        public Guid GetResourceOID(string id, string clsName)
        {
            this.className = clsName;
            Guid empty = Guid.Empty;
            if (id != "")
            {
                int dataCount = 0;
                int num2 = 0;
                dataCount = this.GetDataCount();
                if (dataCount == 0)
                {
                    return empty;
                }
                num2 = this.ComputTotalPageNum(dataCount, this.i_PageSearchNum);
                for (int i = 0; i < num2; i++)
                {
                    this.InitializeData(i * this.i_PageSearchNum, (i + 1) * this.i_PageSearchNum);
                    DataSet myds = this.myds;
                    if ((myds == null) || (myds.Tables.Count <= 0))
                    {
                        return empty;
                    }
                    if (myds.Tables[0].Rows.Count <= 0)
                    {
                        return empty;
                    }
                    try
                    {
                        foreach (DataRow row in myds.Tables[0].Rows)
                        {
                            string str = row["PLM_ID"].ToString();
                            if (id == str)
                            {
                                empty = new Guid((byte[]) row["PLM_OID"]);
                                continue;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("error!" + exception.Message);
                    }
                }
            }
            return empty;
        }

        public Hashtable GetResParameter(){return 
            this.Ht_resParam;
        }
        private void InitData(int i_start, int i_end)
        {
            if (this.myFolder != null)
            {
                if (ResFunc.IsOnlineOutRes(this.myFolder.ClassOid))
                {
                    ResFunc.GetNumDS(out this.myds, this.myFolder, this.Attrs, i_start, i_end, "");
                    ResFunc.ConvertOuterDSHead(this.myds, this.Attrs, this.attrOuter);
                }
                else if (ResFunc.IsRefRes(this.myFolder.ClassOid))
                {
                    this.b_isRefCls = true;
                    PLSPL plspl = new PLSPL();
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.myFolder.ClassOid);
                    DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                    this.className = class3.Name;
                    this.myds = plspl.GetSPLNumDataSet(class3.Name, this.Attrs, this.userOid, i_start, i_end, this.myFolder.FilterString, this.myFolder.FilterValue, "");
                }
                else if (ResFunc.IsTabRes(this.myFolder.ClassOid))
                {
                    new ArrayList();
                    ResFunc.GetNumDS(out this.myds, this.myFolder, this.Attrs, i_start, i_end, "");
                }
            }
        }

   

        private void InitializeConfig()
        {
            this.ucUser = new UCResGrid(this.className);
            if (this.deMetaAttri.IsListInDomain)
            {
                this.ucUser.CloseWhenActivated = false;
            }
            DropDownEditorButton button = base.ButtonsRight["SelectRes"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new ResDropListHandler(this.ucUser_ResSelected);
            this.ucUser.ResSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.ResCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.Resize += new EventHandler(this.ResCombo_Resize);
            base.MouseDown += new MouseEventHandler(this.ResCombo_MouseDown);
            base.TextChanged += new EventHandler(this.ResCombo_TextChanged);
            base.DragEnter += new DragEventHandler(this.ResCombo_DragEnter);
            base.DragDrop += new DragEventHandler(this.ResCombo_DragDrop);
            base.KeyDown += new KeyEventHandler(this.ResCombo_KeyDown);
        }

        private void InitializeData(int i_start, int i_end)
        {
            if ((this.className != null) && (this.className != ""))
            {
                string text1 = "PLM_CUS_" + this.className;
                try
                {
                    this.InitData(i_start, i_end);
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    PrintException.Print(exception2);
                }
                if (this.myds != null)
                {
                    if (this.deMetaAttri == null)
                    {
                        return;
                    }
                    if (((this.deMetaAttri != null) && (this.deMetaAttri.LinkType == 1)) && (this.deMetaAttri.Combination != ""))
                    {
                        DataColumn column = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[0].Columns.Add(column);
                        foreach (DataRow row in this.myds.Tables[0].Rows)
                        {
                            string combination = this.deMetaAttri.Combination;
                            string str3 = combination;
                            foreach (DEMetaAttribute attribute in this.Attrs)
                            {
                                if (this.deMetaAttri.Combination.IndexOf("[" + attribute.Name + "]") > -1)
                                {
                                    string str2 = "PLM_" + attribute.Name;
                                    combination = combination.Replace("[" + attribute.Name + "]", Convert.ToString(row[str2]));
                                    str3 = str3.Replace("[" + attribute.Name + "]", "");
                                }
                            }
                            if (this.b_isRefCls)
                            {
                                string name = "PLM_M_ID";
                                if (row.Table.Columns.Contains(name))
                                {
                                    combination = combination.Replace("[ID]", Convert.ToString(row[name]));
                                    str3 = str3.Replace("[ID]", "");
                                }
                            }
                            if (combination == str3)
                            {
                                combination = "";
                            }
                            row[this.comColumn] = combination;
                        }
                    }
                    else if ((this.deMetaAttri.LinkType == 0) && (this.deMetaAttri.DataType == 8))
                    {
                        DataColumn column2 = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[0].Columns.Add(column2);
                        foreach (DataRow row2 in this.myds.Tables[0].Rows)
                        {
                            row2[this.comColumn] = row2["PLM_ID"].ToString();
                        }
                    }
                }
                DataSet set = this.myds.Copy();
                this.myView = set.Tables[0].DefaultView;
            }
        }

        private void InitResStatus()
        {
            if (ResFunc.IsOnlineOutRes(this.myFolder.ClassOid))
            {
                this.emResType = emResourceType.OutSystem;
            }
            else if (ResFunc.IsRefRes(this.myFolder.ClassOid))
            {
                this.emResType = emResourceType.Standard;
            }
            else if (ResFunc.IsTabRes(this.myFolder.ClassOid))
            {
                this.emResType = emResourceType.Customize;
            }
            else
            {
                this.emResType = emResourceType.PLM;
            }
        }

        public bool IsEditByHand()
        {
            bool flag = false;
            if (this.Dt_ClickStart == this.Dt_ClickEnd)
            {
                flag = true;
            }
            return flag;
        }

        private bool IsExistInFamily(string str_clsname, string str_tablename)
        {
            ArrayList list = new ArrayList();
            string classname = "";
            DEMetaClass item = null;
            item = ModelContext.MetaModel.GetClass(str_clsname);
            if (item != null)
            {
                str_clsname = item.Name;
                if (ResFunc.IsRefRes(item.Oid))
                {
                    classname = ModelContext.MetaModel.GetClass(item.RefClass).Name;
                }
                ArrayList allLockedSmallChildren = UIDataModel.GetAllLockedSmallChildren(classname);
                DEMetaClass class4 = ModelContext.MetaModel.GetClass(classname);
                if (!list.Contains(class4))
                {
                    list.Add(class4);
                }
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
                list.AddRange(allLockedSmallChildren);
                if (list.Count > 0)
                {
                    foreach (DEMetaClass class5 in list)
                    {
                        if (str_tablename.EndsWith(class5.Name))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsExistSelectedRow(string str_value)
        {
            bool flag = false;
            object attrValue = this.GetAttrValue(this.deMetaAttri.Combination);
            if ((attrValue != null) && (attrValue.ToString() == str_value))
            {
                flag = true;
            }
            return flag;
        }

        public bool IsInDataSet(string str_in)
        {
            if (str_in.Trim().Length == 0)
            {
                return false;
            }
            if (this.IsExistSelectedRow(str_in))
            {
                return true;
            }
            return this.JudgeIsInDataSet(str_in);
        }

        private bool IsInDataSet_Disuse(string str)
        {
            if (str.Trim().Length == 0)
            {
                return false;
            }
            if (this.IsExistSelectedRow(str))
            {
                return true;
            }
            int dataCount = 0;
            int num2 = 0;
            dataCount = this.GetDataCount();
            if (dataCount == 0)
            {
                return false;
            }
            num2 = this.ComputTotalPageNum(dataCount, this.i_PageSearchNum);
            for (int i = 0; i < num2; i++)
            {
                this.InitializeData(i * this.i_PageSearchNum, (i + 1) * this.i_PageSearchNum);
                DataSet myds = this.myds;
                if ((myds == null) || (myds.Tables.Count <= 0))
                {
                    return false;
                }
                string tableName = "PLM_CUS_" + this.className;
                if (ResFunc.IsRefRes(this.classOid))
                {
                    tableName = "PLM_CUSV_" + this.className;
                }
                if (ResFunc.IsOnlineOutRes(this.className))
                {
                    tableName = myds.Tables[0].TableName;
                }
                if (myds.Tables[tableName].Rows.Count <= 0)
                {
                    return false;
                }
                foreach (DataColumn column in this.myView.Table.Columns)
                {
                    if (column.ColumnName == this.comColumn)
                    {
                        foreach (DataRowView view in this.myView)
                        {
                            if (str.Trim() == view[this.comColumn].ToString().Trim())
                            {
                                this.LocateDRV = view;
                                return true;
                            }
                        }
                        break;
                    }
                }
            }
            return false;
        }

        private bool IsInDataSet_Disuse(object ob_in, string str_comattr)
        {
            if (str_comattr == null)
            {
                return false;
            }
            if (str_comattr == string.Empty)
            {
                return false;
            }
            if (ob_in == null)
            {
                return false;
            }
            int dataCount = 0;
            int num2 = 0;
            dataCount = this.GetDataCount();
            if (dataCount == 0)
            {
                return false;
            }
            num2 = this.ComputTotalPageNum(dataCount, this.i_PageSearchNum);
            for (int i = 0; i < num2; i++)
            {
                this.InitializeData(i * this.i_PageSearchNum, (i + 1) * this.i_PageSearchNum);
                DataSet myds = this.myds;
                if ((myds == null) || (myds.Tables.Count <= 0))
                {
                    return false;
                }
                string tableName = "PLM_CUS_" + this.className;
                if (ResFunc.IsRefRes(this.classOid))
                {
                    tableName = "PLM_CUSV_" + this.className;
                }
                if (ResFunc.IsOnlineOutRes(this.className))
                {
                    tableName = myds.Tables[0].TableName;
                }
                if (myds.Tables[tableName].Rows.Count <= 0)
                {
                    return false;
                }
                foreach (DataColumn column in this.myView.Table.Columns)
                {
                    if (column.ColumnName == this.comColumn)
                    {
                        foreach (DataRowView view in this.myView)
                        {
                            if (str_comattr.Equals("对象全局唯一标识"))
                            {
                                if ((view["PLM_OID"] != null) && (view["PLM_OID"].ToString() != ""))
                                {
                                    Guid guid = (Guid) view["PLM_OID"];
                                    if (((Guid) ob_in) == guid)
                                    {
                                        this.LocateDRV = view;
                                        return true;
                                    }
                                }
                            }
                            else
                            {
                                string str2 = str_comattr;
                                string str3 = str2;
                                foreach (DEMetaAttribute attribute in this.Attrs)
                                {
                                    if (str_comattr.IndexOf("[" + attribute.Name + "]") > -1)
                                    {
                                        string str4 = "PLM_" + attribute.Name;
                                        str2 = str2.Replace("[" + attribute.Name + "]", Convert.ToString(view[str4].ToString()));
                                        str3 = str3.Replace("[" + attribute.Name + "]", "");
                                    }
                                }
                                if (this.b_isRefCls)
                                {
                                    string name = "PLM_M_ID";
                                    if (view.DataView.Table.Columns.Contains(name))
                                    {
                                        str2 = str2.Replace("[ID]", Convert.ToString(view[name]));
                                        str3 = str3.Replace("[ID]", "");
                                    }
                                }
                                if (str2 == str3)
                                {
                                    str2 = "";
                                }
                                string str6 = str2;
                                if (ob_in.ToString() == str6)
                                {
                                    this.LocateDRV = view;
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public bool IsInSelectedRow(string str)
        {
            bool flag = false;
            if (this.IsExistSelectedRow(str))
            {
                flag = true;
            }
            return flag;
        }

        private bool JudgeIsInDataSet(string str_value)
        {
            bool flag = false;
            if (this.myFolder == null)
            {
                return false;
            }
            if (ResFunc.IsOnlineOutRes(this.myFolder.ClassOid))
            {
                DEMetaClass theCls = ModelContext.MetaModel.GetClass(this.myFolder.ClassOid);
                flag = new PLOuterResource().IsInDataSet(theCls, this.deMetaAttri, str_value, this.myFolder.FilterString, this.myFolder.FilterValue, this.Attrs, this.attrOuter, emResourceType.OutSystem);
            }
            else if (ResFunc.IsRefRes(this.myFolder.ClassOid))
            {
                this.b_isRefCls = true;
                PLSPL plspl = new PLSPL();
                DEMetaClass class3 = ModelContext.MetaModel.GetClass(this.myFolder.ClassOid);
                DEMetaClass class4 = ModelContext.MetaModel.GetClass(class3.RefClass);
                this.className = class4.Name;
                flag = plspl.IsInDataSet(this.deMetaAttri, str_value, class4.Name, this.Attrs, this.userOid, this.myFolder.FilterString, this.myFolder.FilterValue);
            }
            else if (ResFunc.IsTabRes(this.myFolder.ClassOid))
            {
                new ArrayList();
                flag = ResFunc.IsInDataSet(this.deMetaAttri, str_value, this.myFolder, this.Attrs, this.attrOuter, emResourceType.Customize);
            }
            this.SetFolder();
            return flag;
        }

        public void Locate()
        {
            this.myDRV = this.LocateDRV;
        }

        private void ResCombo_AfterEnterEditMode(object sender, EventArgs e)
        {
            if (this.b_ReadOnly)
            {
                base.ReadOnly = true;
            }
            else
            {
                base.ReadOnly = this.DenyWriteByHand;
            }
        }

        private void ResCombo_AfterExitEditMode(object sender, EventArgs e)
        {
            if (this.b_ReadOnly)
            {
                base.ReadOnly = true;
            }
            else
            {
                base.ReadOnly = false;
            }
        }

        private void ResCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this.myFolder);
            Cursor.Current = Cursors.Default;
        }

        private void ResCombo_DragDrop(object sender, DragEventArgs e) {
            if (!this.b_ReadOnly) {
                CLCopyData data = new CLCopyData();
                data = (CLCopyData)e.Data.GetData(typeof(CLCopyData));
                if (data != null) {
                    DataRowView clientData;
                    if (data[0] is DEBusinessItem) {
                        DEBusinessItem item = (DEBusinessItem)data[0];
                        clientData = (DataRowView)item.ClientData;
                        if (clientData != null) {
                            this.myDRV = clientData;
                            this.SetEditText(this.myDRV);
                        }
                    } else {
                        DECopyData data2 = (DECopyData)data[0];
                        if (data2 != null) {
                            if (data2.ClassName != this.className) {
                                MessageBox.Show("资源类不匹配。", "工程资源", MessageBoxButtons.OK);
                            } else {
                                clientData = (DataRowView)data2.ItemList[0];
                                if (clientData != null) {
                                    this.myDRV = clientData;
                                    this.SetEditText(this.myDRV);
                                }
                            }
                        }
                    }
                }
            }
        }


        private void ResCombo_DragEnter(object sender, DragEventArgs e)
        {
            if (!this.b_ReadOnly)
            {
                e.Effect = DragDropEffects.Move;
            }
            CLCopyData data = new CLCopyData();
            data = (CLCopyData) e.Data.GetData(typeof(CLCopyData));
            if (data != null)
            {
                if (data[0] is DEBusinessItem)
                {
                    DEBusinessItem item = (DEBusinessItem) data[0];
                    DataRowView clientData = (DataRowView) item.ClientData;
                    if (clientData == null)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    if (!this.IsExistInFamily(this.className, clientData.Row.Table.TableName))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }
                else
                {
                    DECopyData data2 = (DECopyData) data[0];
                    if (data2 == null)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    if (data2.ClassName != this.className)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                    if (((DataRowView) data2.ItemList[0]) == null)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }
                e.Effect = DragDropEffects.Move;
            }
        }

        private void ResCombo_KeyDown(object sender, KeyEventArgs e)
        {
            this.Dt_ClickStart = DateTime.Now;
            this.Dt_ClickEnd = DateTime.Now;
            if ((this.DenyWriteByHand && !this.b_ReadOnly) && (e.KeyCode == Keys.Delete)) {
                this.Text = "";
            }
        }

        private void ResCombo_MouseDown(object sender, MouseEventArgs e)
        {
            if ((this.IsFromEdit && ClientData.OptResTreeActionWhenEditAttr) && (UCResTree.CAPPResHandler != null))
            {
                UCResTree.CAPPResHandler(this.classOid);
            }
        }

        private void ResCombo_Resize(object sender, EventArgs e)
        {
            if (this.ucUser.Width < base.Width)
            {
                this.ucUser.Width = base.Width;
            }
        }

        private void ResCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.ResTextChanged != null)
            {
                this.ResTextChanged(this.ResValue.ToString());
            }
        }

        private void SetAttrDataType(ArrayList attrList, ArrayList attrOuter)
        {
            foreach (DEMetaAttribute attribute in attrList)
            {
                foreach (DEOuterAttribute attribute2 in attrOuter)
                {
                    if (attribute.Oid == attribute2.FieldOid)
                    {
                        attribute.DataType = attribute2.DataType;
                        break;
                    }
                }
            }
        }

        private void SetDisplayName(DataSet ds)
        {
            try
            {
                string tableName = "";
                if (ResFunc.IsOnlineOutRes(this.classOid))
                {
                    tableName = this.myds.Tables[0].TableName;
                }
                else
                {
                    tableName = "PLM_CUS_" + this.className;
                    if (ResFunc.IsRefRes(this.classOid))
                    {
                        tableName = "PLM_CUSV_" + this.className;
                    }
                }
                string str2 = "PLM_";
                string str3 = "";
                if (((ds != null) && (ds.Tables.Count > 0)) && (ds.Tables[tableName] != null))
                {
                    this.SetParam();
                    foreach (DataColumn column in ds.Tables[tableName].Columns)
                    {
                        if (column.ColumnName == "PLM_M_ID")
                        {
                            column.ColumnName = "代号";
                        }
                        if (column.ColumnName == "PLM_LABEL")
                        {
                            column.ColumnName = "类名";
                        }
                        foreach (DEMetaAttribute attribute in this.Attrs)
                        {
                            str3 = str2 + attribute.Name;
                            if (column.ColumnName == str3)
                            {
                                column.ColumnName = attribute.Label;
                                break;
                            }
                        }
                        if (column.DataType.Equals(System.Type.GetType("System.Byte[]")))
                        {
                            this.al_guid.Add(column.ColumnName);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("error!" + exception.Message);
            }
        }

        private void SetEditText(DataRowView drv)
        {
            this.Dt_ClickEnd = DateTime.Now;
            if (this.deMetaAttri != null)
            {
                if (((this.deMetaAttri != null) && (this.deMetaAttri.LinkType == 1)) && (this.deMetaAttri.Combination != ""))
                {
                    string combination = this.deMetaAttri.Combination;
                    string str3 = combination;
                    foreach (DEMetaAttribute attribute in this.Attrs)
                    {
                        if (this.deMetaAttri.Combination.IndexOf("[" + attribute.Name + "]") > -1)
                        {
                            string str2 = "PLM_" + attribute.Name;
                            combination = combination.Replace("[" + attribute.Name + "]", Convert.ToString(drv.Row[str2]));
                            str3 = str3.Replace("[" + attribute.Name + "]", "");
                        }
                    }
                    if (this.b_isRefCls)
                    {
                        string name = "PLM_M_ID";
                        if (drv.DataView.Table.Columns.Contains(name))
                        {
                            combination = combination.Replace("[ID]", Convert.ToString(drv[name]));
                            str3 = str3.Replace("[ID]", "");
                        }
                    }
                    if (combination == str3)
                    {
                        combination = "";
                    }
                    if (this.deMetaAttri.IsListInDomain)
                    {
                        string resSeparator = this.deMetaAttri.ResSeparator;
                        if (this.OrgText.Length == 0)
                        {
                            this.Text = combination.Trim();
                        }
                        else
                        {
                            this.Text = this.OrgText + resSeparator + combination.Trim();
                        }
                    }
                    else
                    {
                        this.Text = combination.Trim();
                    }
                }
                else if ((this.deMetaAttri.LinkType == 0) && (this.deMetaAttri.DataType == 8))
                {
                    if (this.b_isRefCls)
                    {
                        string str6 = "PLM_M_ID";
                        if (drv.DataView.Table.Columns.Contains(str6))
                        {
                            this.Text = drv.Row[str6].ToString().Trim();
                        }
                    }
                    else
                    {
                        this.Text = drv.Row["PLM_ID"].ToString().Trim();
                    }
                }
            }
        }

        private void SetFilter(Hashtable ht_param, string str_oldfilter, string str_oldfilterval, out string str_filter, out string str_filterval)
        {
            str_filter = str_oldfilter;
            str_filterval = str_oldfilterval;
            if (ht_param.Count != 0)
            {
                int num = 0;
                string str = "";
                string str2 = "";
                ArrayList attrs = new ArrayList();
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
                attrs = this.Attrs;
                if (class2.IsRefResClass && (class2.RefClass != Guid.Empty))
                {
                    foreach (DEMetaAttribute attribute in ResFunc.GetFixedAttrList())
                    {
                        bool flag = false;
                        foreach (DEMetaAttribute attribute2 in attrs)
                        {
                            if (attribute2.Oid == attribute.Oid)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            attrs.Add(attribute);
                        }
                    }
                }
                if ((attrs != null) && (attrs.Count > 0))
                {
                    foreach (DEMetaAttribute attribute3 in attrs)
                    {
                        if (ht_param.ContainsKey(attribute3.Name))
                        {
                            num++;
                            string str3 = "";
                            string str4 = "";
                            this.SetFilterInfo(ht_param[attribute3.Name].ToString(), attribute3, out str3, out str4);
                            if ((str != null) && (str.Trim().Length > 0))
                            {
                                str = str + " AND " + str3;
                            }
                            else
                            {
                                str = str3;
                            }
                            if ((str2 != null) && (str2.Trim().Length > 0))
                            {
                                if ((str4 != null) && (str4.Trim().Length > 0))
                                {
                                    str2 = str2 + "," + str4;
                                }
                            }
                            else if ((str4 != null) && (str4.Trim().Length > 0))
                            {
                                str2 = str4;
                            }
                        }
                    }
                }
                if (ht_param.Count > num)
                {
                    string resKeyByParameter = "";
                    if (ht_param.ContainsKey(this.deMetaAttri.Combination))
                    {
                        resKeyByParameter = this.SetResKeyByPreFix(this.deMetaAttri.Combination);
                        resKeyByParameter = this.GetResKeyByParameter(resKeyByParameter);
                        if ((str != null) && (str.Trim().Length > 0))
                        {
                            string str6 = str;
                            str = str6 + " AND " + resKeyByParameter + " LIKE '%" + ht_param[this.deMetaAttri.Combination].ToString() + "%'";
                        }
                        else
                        {
                            str = resKeyByParameter + " LIKE '%" + ht_param[this.deMetaAttri.Combination].ToString() + "%'";
                        }
                    }
                }
                if ((str_oldfilter == null) || (str_oldfilter == ""))
                {
                    str_filter = str;
                    str_filterval = str2;
                }
                else if ((str != null) || (str.Trim().Length > 0))
                {
                    str_filter = "(" + str_filter + ") AND (" + str + ")";
                    if (str_oldfilterval == null)
                    {
                        str_filterval = str2;
                    }
                    else if (str_oldfilterval.Trim().Length == 0)
                    {
                        str_filterval = str2;
                    }
                    else
                    {
                        str_filterval = str_oldfilterval + "," + str2;
                    }
                }
            }
        }
       
        private void SetFilterInfo(string str_value, DEMetaAttribute deattr, out string str_pasfilter, out string str_pasfilterval) {
            str_pasfilter = "";
            str_pasfilterval = "";
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            string strFilterStr = "";
            string strFilterVal = "";
            TextBox txtBox = new TextBox();
            if (deattr.DataType == 7) {
                txtBox.Text = Convert.ToDateTime(str_value).ToShortDateString();
                txtBox.Tag = deattr;
            } else {
                txtBox.Text = str_value;
                txtBox.Tag = deattr;
            }
            ComboBox cob = new ComboBox();
            if (this.b_AllowLike) {
                if ((((deattr.DataType == 4) || (deattr.DataType == 11)) || ((deattr.DataType == 0) || (deattr.DataType == 1))) || ((deattr.DataType == 2) || (deattr.DataType == 6))) {
                    cob.Text = "包含";
                } else {
                    cob.Text = "等于";
                }
            } else {
                cob.Text = "等于";
            }
            ArrayList txtList = new ArrayList();
            if (str_value != "[空]") {
                ResFunc.CreateCondition(cob, txtBox, txtList, this.emResType, this.attrOuter, this.myFolder, out strFilterStr, out strFilterVal);
            } else {
                flag = true;
                switch (this.emResType) {
                    case emResourceType.Customize:
                        builder2.Append("PLM_");
                        builder2.Append(deattr.Name);
                        break;

                    case emResourceType.OutSystem:
                        foreach (DEOuterAttribute attribute in this.attrOuter) {
                            if (deattr.Oid == attribute.FieldOid) {
                                builder2.Append(attribute.OuterAttrName);
                                break;
                            }
                        }
                        break;

                    case emResourceType.Standard: {
                            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
                            DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                            string str3 = "PLM_CUSV_" + class3.Name;
                            if (deattr.Name.StartsWith("M_") || deattr.Name.StartsWith("R_")) {
                                builder2.Append("PLM_PSM_ITEMMASTER_REVISION.PLM_" + deattr.Name.ToUpper());
                            } else {
                                builder2.Append(str3);
                                builder2.Append(".");
                                builder2.Append("PLM_");
                                builder2.Append(deattr.Name);
                            }
                            break;
                        }
                }
                builder2.Append(" is null ");
            }
            builder2.Append(strFilterStr);
            builder.Append(builder2.ToString());
            if (this.b_AllowLike && ((((deattr.DataType == 4) || (deattr.DataType == 11)) || ((deattr.DataType == 0) || (deattr.DataType == 1))) || ((deattr.DataType == 2) || (deattr.DataType == 6)))) {
                strFilterVal = strFilterVal.Replace(".00%", "%").Replace(".0%", "%");
            }
            builder3.Append(strFilterVal);
            try {
                str_pasfilter = builder.ToString();
                if (!flag) {
                    str_pasfilterval = builder3.ToString();
                }
            } catch (Exception exception) {
                MessageBox.Show("过滤数据发生错误:请检查传递的参数否正确:" + exception.Message, "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void SetFolder()
        {
            this.myFolder = new DEResFolder();
            this.myFolder.Oid = this.classOid;
            this.myFolder.ClassOid = this.classOid;
            this.myFolder.ClassName = this.className;
            this.InitResStatus();
            ArrayList clsTreeCls = new ArrayList();
            clsTreeCls = new PLReference().GetClsTreeCls(this.classOid);
            if (clsTreeCls.Count > 0)
            {
                DEDefCls cls = (DEDefCls) clsTreeCls[0];
                this.myFolder.Filter = cls.FILTER;
                this.myFolder.FilterString = cls.FILTERSTRING;
                this.myFolder.FilterValue = cls.FILTERVALUE;
            }
            if (ResFunc.IsRefRes(this.myFolder.ClassOid))
            {
                this.b_isRefCls = true;
            }
        }

        private void SetFolder(Hashtable ht_param)
        {
            ArrayList clsTreeCls = new ArrayList();
            PLReference reference = new PLReference();
            Hashtable hashtable = this.ConvertToResKey(ht_param);
            clsTreeCls = reference.GetClsTreeCls(this.classOid);
            if (clsTreeCls.Count > 0)
            {
                DEDefCls cls = (DEDefCls) clsTreeCls[0];
                this.myFolder.Filter = cls.FILTER;
                string str = "";
                string str2 = "";
                this.SetFilter(hashtable, cls.FILTERSTRING, cls.FILTERVALUE, out str, out str2);
                this.myFolder.FilterString = str;
                this.myFolder.FilterValue = str2;
            }
            else
            {
                string str3 = "";
                string str4 = "";
                this.SetFilter(hashtable, "", "", out str3, out str4);
                this.myFolder.FilterString = str3;
                this.myFolder.FilterValue = str4;
            }
            if (ResFunc.IsRefRes(this.myFolder.ClassOid))
            {
                this.b_isRefCls = true;
            }
        }

        public void SetFromEdit(bool b_set)
        {
            this.IsFromEdit = b_set;
        }

        private void SetParam()
        {
            string str = "PLM_";
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                switch ((str + attribute.Name))
                {
                    case "PLM_ID":
                        constResource.Label_ID = attribute.Label;
                        break;

                    case "PLM_OID":
                        constResource.Label_OID = attribute.Label;
                        break;
                }
            }
        }

        public void SetParameter(Hashtable ht_param, bool AllowLike)
        {
            this.b_AllowLike = AllowLike;
            this.Ht_resParam = ht_param;
            this.CreateFolder();
            this.ucUser.SetClassLable("this is a test!");
            if (ht_param.Count > 0)
            {
                this.ucUser.IsShowTree = false;
            }
            else
            {
                this.ucUser.IsShowTree = true;
            }
            this.SetFolder(ht_param);
        }

        private string SetResKeyByPreFix(string str_key)
        {
            string str = str_key;
            ArrayList attrs = this.Attrs;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
            if (class2.IsRefResClass && (class2.RefClass != Guid.Empty))
            {
                ArrayList fixedAttrList = ResFunc.GetFixedAttrList();
                attrs.AddRange(fixedAttrList);
            }
            if ((attrs != null) && (attrs.Count > 0))
            {
                foreach (DEMetaAttribute attribute in attrs)
                {
                    string name = attribute.Name;
                    if (name.StartsWith("M_"))
                    {
                        name = name.Replace("M_", "");
                    }
                    if (name.StartsWith("R_"))
                    {
                        name = name.Replace("R_", "");
                    }
                    string str3 = "[" + name + "]";
                    string newValue = "";
                    if (str_key.IndexOf(str3) >= 0)
                    {
                        switch (this.emResType)
                        {
                            case emResourceType.Customize:
                                newValue = "PLM_" + attribute.Name;
                                break;

                            case emResourceType.OutSystem:
                                foreach (DEOuterAttribute attribute2 in this.attrOuter)
                                {
                                    if (attribute.Oid == attribute2.FieldOid)
                                    {
                                        newValue = attribute2.OuterAttrName;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.Standard:
                            {
                                DEMetaClass class3 = ModelContext.MetaModel.GetClass(this.className);
                                DEMetaClass class4 = ModelContext.MetaModel.GetClass(class3.RefClass);
                                string str5 = "PLM_CUSV_" + class4.Name;
                                if (attribute.Name.StartsWith("M_") || attribute.Name.StartsWith("R_"))
                                {
                                    newValue = "PLM_PSM_ITEMMASTER_REVISION.PLM_" + attribute.Name.ToUpper();
                                }
                                else
                                {
                                    newValue = str5 + ".PLM_" + attribute.Name;
                                }
                                break;
                            }
                        }
                        newValue = "[" + newValue + "]";
                        str = str.Replace(str3, newValue);
                    }
                }
            }
            return str;
        }

        private void ucUser_ResSelected(DataRowView drv)
        {
            bool flag = false;
            if ((base.Tag == null) && (drv != null))
            {
                flag = true;
            }
            this.myDRV = drv;
            if ((this.deMetaAttri != null) && this.deMetaAttri.IsListInDomain)
            {
                this.OrgText = this.Text;
            }
            this.Text = "";
            this.SetEditText(drv);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(drv);
                base.CloseEditorButtonDropDowns();
            }
        }

        public bool DenyWriteByHand
        {
            get{return
                this.b_DenyByHand;
            
            }set
            {
                this.b_DenyByHand = value;
            }
        }

        public bool readOnly
        {
            get {
                return this.b_ReadOnly;
            }
            set
            {
                this.b_ReadOnly = value;
                if (this.b_ReadOnly)
                {
                    DropDownEditorButton button = base.ButtonsRight["SelectRes"] as DropDownEditorButton;
                    button.Control = null;
                    base.ReadOnly = true;
                }
            }
        }

        public Guid ResourceOid
        {
            get{return
                this.resOid;
            }set
            {
                this.resOid = value;
            }
        }

        public string ResValue
        {
            get {return
                this.Text.TrimEnd(new char[0]);
        }set
            {
                this.Text = value;
            }
        }

        public DECopyData SelectedRow
        {
            get
            {
                if (this.myDRV == null)
                {
                    return null;
                }
                return new DECopyData { 
                    ClassName = this.className,
                    ItemList = { this.myDRV }
                };
            }
        }
    }
}

