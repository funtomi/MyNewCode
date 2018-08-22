    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using Infragistics.Win.UltraWinGrid;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC
{
    public partial class UCResGrid : UserControlPLM
    {
        private ArrayList al_guid;
        private ArrayList al_mid;
        private ArrayList Attrs;
        private bool b_filter;
        private bool b_isfilter;
        private bool b_start;
        private string className;
        private Guid classOid;
        private string comColumn;
        private DEMetaAttribute curFieldAttr;
        private DEMetaAttribute deMetaAttri;
        private int i_pos;
        private DataSet myds;
        private DataView myView;
      
        private Guid resOid;
        private static ulong Stamp;
       
        private UltraDateTimeEditor ucValue;

        public event SelectResHandler ResSelected;

        public UCResGrid()
        {
            this.b_start = true;
            this.Attrs = new ArrayList();
            this.comColumn = "选_中";
            this.resOid = Guid.Empty;
            this.al_guid = new ArrayList();
            this.al_mid = new ArrayList();
            this.InitializeComponent();
        }

        public UCResGrid(string clsName, DEMetaAttribute metaAttr, int i_pos) : this()
        {
            this.className = clsName;
            this.deMetaAttri = metaAttr;
            this.i_pos = i_pos;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
            if (class2 != null)
            {
                this.classOid = class2.Oid;
                this.InitObject();
                this.InitializeData();
            }
        }

        private void ConfigureResCombo()
        {
            this.uGrid_Res.DataSource = this.myView;
            this.uGrid_Res.DisplayLayout.BorderStyle = UIElementBorderStyle.Solid;
        }
 

        private ArrayList GetAttributes(string classname) {
            return ModelContext.MetaModel.GetAttributes(classname);
        }
        private ArrayList GetChildMID(Guid g_mid, string strSBLTAB, string strRelName)
        {
            ArrayList list = new ArrayList();
            ArrayList masterOids = new ArrayList(1);
            ArrayList revNums = new ArrayList(1);
            masterOids.Add(g_mid);
            revNums.Add(0);
            Guid curView = ClientData.UserGlobalOption.CurView;
            foreach (DEBusinessItem item in PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem))
            {
                if (item.Iteration.LinkRelationSet.GetRelationBizItemList(strRelName) == null)
                {
                    DERelationList list5 = PLItem.Agent.GetLinkRelations(item.IterOid, strSBLTAB, strRelName, ClientData.LogonUser.Oid);
                    item.Iteration.LinkRelationSet.RelationBizItemLists[strRelName] = list5;
                    if (list5.Count > 0)
                    {
                        for (int i = 0; i < list5.Count; i++)
                        {
                            DERelation2 relation = new DERelation2();
                            relation = (DERelation2) list5[i];
                            list.Add(relation.RightObj);
                        }
                    }
                }
            }
            return list;
        }

        private ArrayList GetItemLST(string strSBLTAB)
        {
            ArrayList list = new ArrayList();
            ArrayList itemMasters = PLItem.Agent.GetItemMasters(strSBLTAB, ClientData.LogonUser.Oid);
            ArrayList masterOids = new ArrayList(itemMasters.Count);
            ArrayList revNums = new ArrayList(itemMasters.Count);
            foreach (DEItemMaster2 master in itemMasters)
            {
                masterOids.Add(master.Oid);
                revNums.Add(0);
            }
            Guid curView = ClientData.UserGlobalOption.CurView;
            return PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
        }

        private ArrayList GetSplitString(string str_pos, string str_fh)
        {
            char[] separator = str_fh.ToCharArray();
            string[] strArray = null;
            ArrayList list = new ArrayList();
            if (str_pos != null)
            {
                strArray = str_pos.Split(separator);
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                list.Add(strArray[i].ToString());
            }
            return list;
        }


        private void InitializeData()
        {
            if ((this.className != null) && (this.className != ""))
            {
                string str4 = "PLM_CUS_" + this.className;
                this.Attrs = this.GetAttributes(this.className);
                try
                {
                    this.myds = new DataSet();
                    ArrayList items = new ArrayList();
                    items = this.GetItemLST(this.className);
                    DataTable dataSource = DataSourceMachine.GetDataSource(this.className, items);
                    this.myds.Tables.Add(dataSource);
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                if ((this.myds != null) && (this.deMetaAttri != null))
                {
                    if (((this.deMetaAttri != null) && (this.deMetaAttri.LinkType == 1)) && (this.deMetaAttri.Combination != ""))
                    {
                        DataColumn column = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[str4].Columns.Add(column);
                        foreach (DataRow row in this.myds.Tables[str4].Rows)
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
                            row[this.comColumn] = combination;
                        }
                    }
                    else if ((this.deMetaAttri.LinkType == 0) && (this.deMetaAttri.DataType == 8))
                    {
                        DataColumn column2 = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[str4].Columns.Add(column2);
                        foreach (DataRow row2 in this.myds.Tables[str4].Rows)
                        {
                            ArrayList splitString = this.GetSplitString(this.deMetaAttri.Combination, "}");
                            string str5 = "";
                            for (int i = 0; i < (splitString.Count - 1); i++)
                            {
                                str5 = str5 + "{" + row2["PLM_ID"].ToString() + "}";
                            }
                            row2[this.comColumn] = str5;
                        }
                    }
                    DataSet ds = this.myds.Copy();
                    this.SetDisplayName(ds);
                    this.myView = ds.Tables[str4].DefaultView;
                    this.ConfigureResCombo();
                    this.SetDisplayGrid();
                }
            }
        }

        private void InitLeftTree(DEMetaClass cls)
        {
            TreeNode node = new TreeNode(cls.Label) {
                Tag = cls
            };
            this.TV_Class.Nodes.Add(node);
            UIDataModel.FillClassesTree(this.TV_Class, cls.Name, 0, 0);
        }

        private void InitObject()
        {
            if (this.IsHasChildCls(this.classOid))
            {
                this.panel1.Visible = true;
                DEMetaClass cls = ModelContext.MetaModel.GetClass(this.className);
                if (cls != null)
                {
                    this.InitLeftTree(cls);
                }
            }
            else
            {
                this.panel1.Visible = false;
            }
        }

        private bool IsHasChildCls(Guid g_clsid)
        {
            bool flag = false;
            ArrayList children = ModelContext.MetaModel.GetChildren(g_clsid);
            if ((children.Count > 0) && (children != null))
            {
                flag = true;
            }
            return flag;
        }

        public bool IsInDataSet(string str)
        {
            string tableName = "PLM_CUS_" + this.className;
            bool flag = false;
            if (this.myds == null)
            {
                this.InitializeData();
            }
            DataSet myds = this.myds;
            if ((myds == null) || (myds.Tables.Count <= 0))
            {
                return false;
            }
            if (ResFunc.IsOnlineOutRes(this.className))
            {
                tableName = myds.Tables[0].TableName;
            }
            if (myds.Tables[tableName].Rows.Count <= 0)
            {
                return false;
            }
            foreach (DataColumn column in myds.Tables[tableName].Columns)
            {
                if (column.ColumnName == this.comColumn)
                {
                    foreach (DataRow row in myds.Tables[tableName].Rows)
                    {
                        if (str == row[this.comColumn].ToString())
                        {
                            return true;
                        }
                    }
                    return flag;
                }
            }
            return flag;
        }

        private void LoadChildData(Guid g_mid, string str_leftClsName, string str_reltab)
        {
            ArrayList list = new ArrayList();
            list = this.GetChildMID(g_mid, str_leftClsName, str_reltab);
            ArrayList masterOids = new ArrayList(list.Count);
            ArrayList revNums = new ArrayList(list.Count);
            foreach (Guid guid in list)
            {
                masterOids.Add(guid);
                revNums.Add(0);
            }
            Guid curView = ClientData.UserGlobalOption.CurView;
            ArrayList list4 = PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
            this.LoadData(list4);
        }

        private void LoadData(ArrayList al_item)
        {
            DataTable dataSource = new DataTable();
            if ((this.className != null) && (this.className != ""))
            {
                string name = "PLM_CUS_" + this.className;
                this.Attrs = this.GetAttributes(this.className);
                new ArrayList();
                new ArrayList();
                if (al_item.Count > 0)
                {
                    dataSource = DataSourceMachine.GetDataSource(this.className, al_item);
                }
                else
                {
                    this.uGrid_Res.DataSource = null;
                    return;
                }
                this.myds.Clear();
                this.myds.Tables.Remove(name);
                this.myds.Tables.Add(dataSource);
                if ((this.myds != null) && (this.deMetaAttri != null))
                {
                    if (((this.deMetaAttri != null) && (this.deMetaAttri.LinkType == 1)) && (this.deMetaAttri.Combination != ""))
                    {
                        DataColumn column = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[name].Columns.Add(column);
                        foreach (DataRow row in this.myds.Tables[name].Rows)
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
                            row[this.comColumn] = combination;
                        }
                    }
                    else if ((this.deMetaAttri.LinkType == 0) && (this.deMetaAttri.DataType == 8))
                    {
                        DataColumn column2 = new DataColumn(this.comColumn) {
                            DataType = typeof(string)
                        };
                        this.myds.Tables[name].Columns.Add(column2);
                        foreach (DataRow row2 in this.myds.Tables[name].Rows)
                        {
                            ArrayList splitString = this.GetSplitString(this.deMetaAttri.Combination, "}");
                            string str5 = "";
                            for (int i = 0; i < (splitString.Count - 1); i++)
                            {
                                str5 = str5 + "{" + row2["PLM_ID"].ToString() + "}";
                            }
                            row2[this.comColumn] = str5;
                        }
                    }
                    DataSet ds = this.myds.Copy();
                    this.SetDisplayName(ds);
                    this.myView = ds.Tables[name].DefaultView;
                    this.ConfigureResCombo();
                    this.SetDisplayGrid();
                }
            }
        }

        private void LoadParentData(Guid g_childid, string str_reltab)
        {
            ArrayList list = PLItem.Agent.GetLinkedRelationItems(g_childid, 0, 0, str_reltab, ClientData.LogonUser.Oid, ClientData.UserGlobalOption);
            this.LoadData(list);
        }

        public void ReLoad(Guid g_mid, string str_leftClsName, string str_reltab, bool b_isReflex)
        {
            if (b_isReflex)
            {
                this.LoadParentData(g_mid, str_reltab);
            }
            else
            {
                this.LoadChildData(g_mid, str_leftClsName, str_reltab);
            }
        }

        private void SetDisplayGrid()
        {
            int num = 0;
            int index = 0;
            int count = 0;
            string label = "";
            count = this.Attrs.Count;
            this.panel3.Controls.Clear();
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                if (attribute.DataType == 8)
                {
                    label = attribute.Label;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                    count--;
                }
            }
            foreach (string str2 in this.al_guid)
            {
                this.uGrid_Res.DisplayLayout.Bands[0].Columns[str2].Hidden = true;
            }
            this.uGrid_Res.DisplayLayout.Bands[0].Columns[this.comColumn].Hidden = true;
            this.txtbox = new TextEditPLM[count];
            for (int i = 0; i < this.uGrid_Res.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (!this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].Hidden)
                {
                    this.txtbox[index] = new TextEditPLM();
                    if (index == 0)
                    {
                        this.txtbox[index].Location = new Point(0, 0);
                        this.txtbox[index].Width = this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].Width + 0x12;
                    }
                    else
                    {
                        this.txtbox[index].Location = new Point(num + 0x12, 0);
                        this.txtbox[index].Width = this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].Width;
                    }
                    num += this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].Width;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].Width--;
                    this.txtbox[index].Name = this.uGrid_Res.DisplayLayout.Bands[0].Columns[i].ToString();
                    this.txtbox[index].Text = "";
                    this.panel3.Controls.Add(this.txtbox[index]);
                    this.txtbox[index].TextChanged += new EventHandler(this.txtbox_TextChanged);
                    index++;
                    this.panel3.Width = num + 0x12;
                    this.uGrid_Res.Width = this.panel3.Width;
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
                }
                string str2 = "PLM_";
                string str3 = "";
                if (((ds != null) && (ds.Tables.Count > 0)) && (ds.Tables[tableName] != null))
                {
                    this.SetParam();
                    foreach (DataColumn column in ds.Tables[tableName].Columns)
                    {
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

        private void TV_Class_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.TV_Class.SelectedNode != null)
            {
                DEMetaClass tag = (DEMetaClass) this.TV_Class.SelectedNode.Tag;
                this.classOid = tag.Oid;
                this.className = tag.Name;
                this.Attrs.Clear();
                this.al_guid.Clear();
                this.al_mid.Clear();
                this.InitializeData();
            }
        }

        private void txtbox_TextChanged(object sender, EventArgs e)
        {
            if (this.txtbox.GetLength(0) != 0)
            {
                string str = "";
                int num = 0;
                for (int i = 0; i < this.txtbox.GetLength(0); i++)
                {
                    if (this.txtbox[i].Text.Trim().Length > 0)
                    {
                        if (num == 0)
                        {
                            string str2 = str;
                            str = str2 + this.txtbox[i].Name + " like '*" + this.txtbox[i].Text + "*'";
                        }
                        else
                        {
                            string str3 = str;
                            str = str3 + " AND " + this.txtbox[i].Name + " like '*" + this.txtbox[i].Text + "*'";
                        }
                        num++;
                    }
                }
                try
                {
                    if (str.Trim().Length > 0)
                    {
                        this.myView.RowFilter = str;
                    }
                    else
                    {
                        this.myView.RowFilter = this.txtbox[0].Name + " like '**'";
                    }
                }
                catch
                {
                }
            }
        }

        private void UCRes_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }

        private void uGrid_Res_Click(object sender, EventArgs e)
        {
            if (!this.b_start && (this.ResSelected != null))
            {
                string str = this.uGrid_Res.ActiveRow.Cells[this.comColumn].Value.ToString() + "{" + this.uGrid_Res.ActiveRow.Cells[constResource.Label_OID].Value.ToString() + "}";
                this.ResSelected(str, 1, this.i_pos);
            }
        }

        private void uGrid_Res_DoubleClick(object sender, EventArgs e)
        {
            base.Hide();
        }
    }
}

