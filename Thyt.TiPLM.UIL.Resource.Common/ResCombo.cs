    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public class ResCombo : UltraCombo
    {
        private ArrayList al_guid;
        private ArrayList al_ShowColumn;
        private ArrayList Attrs;
        private bool b_isRefCls;
        private string className;
        private Guid classOid;
        private string comColumn;
        private DEMetaAttribute deMetaAttri;
        private UltraGridRow m_dropdownRow;
        private DataSet myds;
        private DataView myView;
        private Guid resOid;
        private ArrayList resOidLst;
        private static ulong Stamp;

        public event SelectResHandler ResTextChanged;

        public ResCombo(Guid classOid, DEMetaAttribute metaAttr)
        {
            this.Attrs = new ArrayList();
            this.comColumn = "选_中";
            this.resOid = Guid.Empty;
            this.al_guid = new ArrayList();
            this.al_ShowColumn = new ArrayList();
            this.InitializeConfig();
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
            if (class2 != null)
            {
                this.className = class2.Name;
            }
        }

        public ResCombo(string clsName, DEMetaAttribute metaAttr)
        {
            this.Attrs = new ArrayList();
            this.comColumn = "选_中";
            this.resOid = Guid.Empty;
            this.al_guid = new ArrayList();
            this.al_ShowColumn = new ArrayList();
            this.InitializeConfig();
            this.className = clsName;
            this.deMetaAttri = metaAttr;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
            if (class2 != null)
            {
                this.classOid = class2.Oid;
            }
        }

        private void ConfigureResCombo()
        {
            base.DataSource = this.myView;
            if (ResFunc.IsOnlineOutRes(this.classOid))
            {
                base.ValueMember = this.comColumn;
            }
            else
            {
                base.ValueMember = constResource.Label_OID;
            }
            base.DisplayMember = this.comColumn;
            this.SetDisplayPos();
            if (this.deMetaAttri.IsResInputEnable)
            {
                base.AutoEdit = true;
            }
            else
            {
                base.AutoEdit = false;
            }
            base.MaxDropDownItems = 6;
        }

        private ArrayList GetAttributes(string classname){
           return ModelContext.MetaModel.GetAttributes(classname);
        }
        public object GetAttrValue(string str_attrname)
        {
            this.LoadData();
            object obj2 = new object();
            if (this.m_dropdownRow == null)
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
                if ((this.m_dropdownRow.Cells[str_attrname] != null) && (this.m_dropdownRow.Cells[str_attrname].Value.ToString() != ""))
                {
                    obj2 = this.m_dropdownRow.Cells[str_attrname].Value;
                }
                return obj2;
            }
            string str = str_attrname;
            string str2 = str;
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                if (str_attrname.IndexOf("[" + attribute.Name + "]") > -1)
                {
                    string label = attribute.Label;
                    str = str.Replace("[" + attribute.Name + "]", Convert.ToString(this.m_dropdownRow.Cells[label].Value.ToString()));
                    str2 = str2.Replace("[" + attribute.Name + "]", "");
                }
            }
            if (str == str2)
            {
                str = "";
            }
            return str;
        }

        public object GetAttrValue(string itemClassName, string itemAttribute)
        {
            this.LoadData();
            DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(itemClassName, itemAttribute);
            if (((attribute != null) && (attribute.LinkedResClass != Guid.Empty)) && (ModelContext.MetaModel.GetClass(attribute.LinkedResClass).Name == this.className))
            {
                return this.GetAttrValue(attribute.Combination);
            }
            return null;
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

        public DataSet GetMyDs()
        {
            this.LoadData();
            return this.myds;
        }

        public string GetResourceID(Guid resOID, string clsName)
        {
            string str = "PLM_CUS_" + clsName;
            if (ResFunc.IsRefRes(this.classOid))
            {
                str = "PLM_CUSV_" + clsName;
            }
            string str2 = "";
            ulong stamp = 0L;
            if (resOID != Guid.Empty)
            {
                DataSet set;
                UCCusResource.GetData(out stamp, out set, clsName, this.Attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return str2;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return str2;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        Guid guid = new Guid((byte[]) row["PLM_OID"]);
                        if (resOID == guid)
                        {
                            return row["PLM_ID"].ToString();
                        }
                    }
                    return str2;
                }
                catch (Exception exception)
                {
                    throw new Exception("error!" + exception.Message);
                }
            }
            return str2;
        }

        public Guid GetResourceOID(string id, string clsName)
        {
            string str = "PLM_CUS_" + clsName;
            if (ResFunc.IsRefRes(this.classOid))
            {
                str = "PLM_CUSV_" + clsName;
            }
            Guid empty = Guid.Empty;
            ulong stamp = 0L;
            if (id != "")
            {
                DataSet set;
                UCCusResource.GetData(out stamp, out set, clsName, this.Attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return empty;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return empty;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        string str2 = row["PLM_ID"].ToString();
                        if (id == str2)
                        {
                            return new Guid((byte[]) row["PLM_OID"]);
                        }
                    }
                    return empty;
                }
                catch (Exception exception)
                {
                    throw new Exception("error!" + exception.Message);
                }
            }
            return empty;
        }

        private void InitializeConfig()
        {
            base.RowSelected += new RowSelectedEventHandler(this.ResCombo_RowSelected);
            base.BeforeDropDown += new CancelEventHandler(this.ResCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.ResCombo_TextChanged);
            base.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            base.DragEnter += new DragEventHandler(this.ResCombo_DragEnter);
            base.DragDrop += new DragEventHandler(this.ResCombo_DragDrop);
        }

        private void InitializeData()
        {
            if ((this.className != null) && (this.className != ""))
            {
                string tableName = "PLM_CUS_" + this.className;
                this.Attrs = this.GetAttributes(this.className);
                try
                {
                    if (ResFunc.IsOnlineOutRes(this.className))
                    {
                        this.myds = new PLOuterResource().GetOuterResData(this.classOid, true);
                        this.Attrs = ResFunc.GetAttrList(this.myds, this.Attrs);
                        if (this.myds != null)
                        {
                            tableName = this.myds.Tables[0].TableName;
                        }
                    }
                    else if (ResFunc.IsTabRes(this.classOid))
                    {
                        ResFunc.GetData(out Stamp, out this.myds, this.className, this.Attrs);
                    }
                    else if (ResFunc.IsRefRes(this.classOid))
                    {
                        this.b_isRefCls = true;
                        PLSPL plspl = new PLSPL();
                        DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
                        DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                        this.Attrs = this.GetAttributes(class3.Name);
                        this.className = class3.Name;
                        tableName = "PLM_CUSV_" + this.className;
                        this.myds = plspl.GetSPLDataSet(class3.Name, this.Attrs, ClientData.LogonUser.Oid, "", "");
                    }
                    else
                    {
                        this.myds = new DataSet();
                        ArrayList items = new ArrayList();
                        items = this.GetItemLST(this.className);
                        DataTable dataSource = DataSourceMachine.GetDataSource(this.className, items);
                        this.myds.Tables.Add(dataSource);
                    }
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
                        this.myds.Tables[tableName].Columns.Add(column);
                        foreach (DataRow row in this.myds.Tables[tableName].Rows)
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
                                string str5 = "PLM_M_ID";
                                combination = combination.Replace("[ID]", Convert.ToString(row[str5]));
                                str3 = str3.Replace("[ID]", "");
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
                        this.myds.Tables[tableName].Columns.Add(column2);
                        foreach (DataRow row2 in this.myds.Tables[tableName].Rows)
                        {
                            row2[this.comColumn] = row2["PLM_ID"].ToString();
                        }
                    }
                    DataSet ds = this.myds.Copy();
                    this.SetDisplayName(ds);
                    this.myView = ds.Tables[tableName].DefaultView;
                    this.ConfigureResCombo();
                }
            }
        }

        public bool IsInDataSet(string str)
        {
            this.LoadData();
            string tableName = "PLM_CUS_" + this.className;
            if (ResFunc.IsRefRes(this.classOid))
            {
                tableName = "PLM_CUSV_" + this.className;
            }
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

        public bool IsInDataSet(object ob_in, string str_comattr)
        {
            this.LoadData();
            string tableName = "PLM_CUS_" + this.className;
            if (ResFunc.IsRefRes(this.classOid))
            {
                tableName = "PLM_CUSV_" + this.className;
            }
            if (str_comattr == null)
            {
                return false;
            }
            if (str_comattr == string.Empty)
            {
                return false;
            }
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
                        if (str_comattr.Equals("对象全局唯一标识"))
                        {
                            if ((row["PLM_OID"] != null) && (row["PLM_OID"].ToString() != ""))
                            {
                                Guid guid = (Guid) row["PLM_OID"];
                                if (((Guid) ob_in) == guid)
                                {
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
                                    str2 = str2.Replace("[" + attribute.Name + "]", Convert.ToString(row[str4].ToString()));
                                    str3 = str3.Replace("[" + attribute.Name + "]", "");
                                }
                            }
                            if (str2 == str3)
                            {
                                str2 = "";
                            }
                            string str5 = str2;
                            if (ob_in.ToString() == str5)
                            {
                                return true;
                            }
                        }
                    }
                    return flag;
                }
            }
            return flag;
        }

        private void LoadData()
        {
            if (this.myds == null)
            {
                this.InitializeData();
            }
        }

        private void ResCombo_BeforeDropDown(object sender, CancelEventArgs e)
        {
            int num3;
            int num = 0;
            int num2 = 0;
            string label = "";
            this.LoadData();
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                if (attribute.DataType == 8)
                {
                    label = attribute.Label;
                    base.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                }
            }
            foreach (string str2 in this.al_guid)
            {
                base.DisplayLayout.Bands[0].Columns[str2].Hidden = true;
            }
            base.DisplayLayout.Bands[0].Columns[this.comColumn].Hidden = true;
            for (num3 = 0; num3 < base.DisplayLayout.Bands[0].Columns.Count; num3++)
            {
                if (!base.DisplayLayout.Bands[0].Columns[num3].Hidden)
                {
                    num += base.DisplayLayout.Bands[0].Columns[num3].Width;
                    num2++;
                }
            }
            if (num2 > 0)
            {
                if (num < base.Width)
                {
                    base.DropDownWidth = base.Width;
                    for (num3 = 0; num3 < base.DisplayLayout.Bands[0].Columns.Count; num3++)
                    {
                        base.DisplayLayout.Bands[0].Columns[num3].Width = (base.DropDownWidth - 2) / num2;
                    }
                }
                else if (num >= 300)
                {
                    base.DropDownWidth = 300;
                }
                else if (num < 300)
                {
                    for (num3 = 0; num3 < base.DisplayLayout.Bands[0].Columns.Count; num3++)
                    {
                        base.DisplayLayout.Bands[0].Columns[num3].Width = 0x12a / num2;
                    }
                }
            }
        }

        private void ResCombo_DragDrop(object sender, DragEventArgs e)
        {
            if (!base.ReadOnly)
            {
                this.LoadData();
                DEMetaAttribute deMetaAttri = this.deMetaAttri;
                CLCopyData data = new CLCopyData();
                data = (CLCopyData) e.Data.GetData(typeof(CLCopyData));
                if (data != null)
                {
                    string combination;
                    if (data[0] is DEBusinessItem)
                    {
                        DEBusinessItem item = (DEBusinessItem) data[0];
                        combination = deMetaAttri.Combination;
                        foreach (DEMetaAttribute attribute2 in this.Attrs)
                        {
                            if (deMetaAttri.Combination.IndexOf("[" + attribute2.Name + "]") > -1)
                            {
                                combination = combination.Replace("[" + attribute2.Name + "]", item.GetAttrValue(this.className, attribute2.Name).ToString());
                            }
                        }
                        combination = combination.Replace("[ID]", item.Id.ToString());
                        this.Text = combination.Trim();
                        this.ResourceOid = item.MasterOid;
                    }
                    else
                    {
                        DECopyData data2 = (DECopyData) data[0];
                        if (data2 != null)
                        {
                            if (data2.ClassName != this.className)
                            {
                                MessageBoxPLM.Show("资源类不匹配", "工程资源", MessageBoxButtons.OK);
                            }
                            else
                            {
                                DataRowView view = (DataRowView) data2.ItemList[0];
                                if (view != null)
                                {
                                    if ((deMetaAttri != null) && (deMetaAttri.LinkType == 1))
                                    {
                                        combination = deMetaAttri.Combination;
                                        foreach (DEMetaAttribute attribute3 in this.Attrs)
                                        {
                                            if (deMetaAttri.Combination.IndexOf("[" + attribute3.Name + "]") > -1)
                                            {
                                                string str2 = "PLM_" + attribute3.Name;
                                                combination = combination.Replace("[" + attribute3.Name + "]", Convert.ToString(view[str2]));
                                            }
                                        }
                                        if (ResFunc.IsRefRes(this.classOid))
                                        {
                                            string str3 = "PLM_M_ID";
                                            combination = combination.Replace("[ID]", Convert.ToString(view[str3]));
                                        }
                                        this.Text = combination.Trim();
                                    }
                                    else
                                    {
                                        this.Text = view["PLM_ID"].ToString();
                                    }
                                    this.ResourceOid = new Guid((byte[]) view["PLM_OID"]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ResCombo_DragEnter(object sender, DragEventArgs e)
        {
            if (!base.ReadOnly)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void ResCombo_RowSelected(object sender, RowSelectedEventArgs e)
        {
            this.m_dropdownRow = e.Row;
            if ((((this.m_dropdownRow != null) && !ResFunc.IsOnlineOutRes(this.classOid)) && (constResource.Label_OID != "")) && ((this.m_dropdownRow.Cells[constResource.Label_OID] != null) && (this.m_dropdownRow.Cells[constResource.Label_OID].Value.ToString() != "")))
            {
                if (this.m_dropdownRow.Cells[constResource.Label_OID].Value.GetType() == typeof(Guid))
                {
                    this.resOid = (Guid) this.m_dropdownRow.Cells[constResource.Label_OID].Value;
                }
                else
                {
                    this.resOid = new Guid((byte[]) this.m_dropdownRow.Cells[constResource.Label_OID].Value);
                }
            }
        }

        private void ResCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.ResTextChanged != null)
            {
                this.ResTextChanged(this.ResValue.ToString());
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

        private void SetDisplayPos()
        {
            if (this.deMetaAttri != null)
            {
                foreach (DEMetaAttribute attribute in this.Attrs)
                {
                    if (this.deMetaAttri.Combination.IndexOf("[" + attribute.Name + "]") > -1)
                    {
                        string label = "";
                        label = attribute.Label;
                        if (ResFunc.IsRefRes(this.classOid))
                        {
                            if (attribute.Name == "ID")
                            {
                                label = "代号";
                            }
                            if (attribute.Name == "LABEL")
                            {
                                label = "类名";
                            }
                        }
                        if ((base.DisplayLayout.Bands.Count != 0) && (base.DisplayLayout.Bands[0].Columns.Count != 0))
                        {
                            base.DisplayLayout.Bands[0].Columns[label].Header.VisiblePosition = 0;
                        }
                    }
                }
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

        public bool readOnly
        {
            get {
               return base.ReadOnly;
            }set
            {
                base.ReadOnly = value;
            }
        }

        public Guid ResourceOid
        {
            get { 
               return this.resOid;
            }set
            {
                this.resOid = value;
            }
        }

        public string ResValue
        {
            get {
                return this.Text.Trim();
            }
            set
            {
                this.Text = value;
            }
        }
    }
}

