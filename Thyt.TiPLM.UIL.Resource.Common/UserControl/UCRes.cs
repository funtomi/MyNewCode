    using DevExpress.XtraEditors.Controls;
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
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.UserControl
{
    public partial class UCRes : UserControlPLM
    {
        private ArrayList al_guid;
        private ArrayList Attrs;
        private bool b_filter;
        private bool b_start;
        
        private string className;
        private Guid classOid;
        
        private string comColumn;
        private DEMetaAttribute curFieldAttr;
        private DEMetaAttribute deMetaAttri;
        private ImageList imageList1;
        
        private UltraGridRow m_dropdownRow;
        private DataSet myds;
        private DataView myView;
        
        private Guid resOid;
        private ArrayList resOidLst;
        private static ulong Stamp;
        

        public event SelectResHandler ResSelected;

        public UCRes()
        {
            this.b_start = true;
            this.Attrs = new ArrayList();
            this.comColumn = "选_中";
            this.resOid = Guid.Empty;
            this.al_guid = new ArrayList();
            this.InitializeComponent();
            this.panel1.Height = base.Height - this.toolBar1.Height;
            this.panel2.Visible = false;
            this.panel2.Height = 0;
        }

        public UCRes(Guid classOid, DEMetaAttribute metaAttr) : this()
        {
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
            if (class2 != null)
            {
                this.className = class2.Name;
                this.InitializeData();
            }
        }

        public UCRes(string clsName, DEMetaAttribute metaAttr) : this()
        {
            this.className = clsName;
            this.deMetaAttri = metaAttr;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
            if (class2 != null)
            {
                this.classOid = class2.Oid;
                this.InitializeData();
            }
        }

        private void AddUC(DEMetaAttribute attr)
        {
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string text = "*";
            text = this.uTE_opervalue.Text;
            if (this.curFieldAttr.DataType2.ToString() == "DateTime")
            {
                try
                {
                    Convert.ToDateTime(text);
                }
                catch
                {
                    MessageBoxPLM.Show("日期类型值格式不正确！");
                    return;
                }
            }
            if ((((this.curFieldAttr.DataType != 0) && (this.curFieldAttr.DataType != 1)) && ((this.curFieldAttr.DataType != 2) && (this.curFieldAttr.DataType != 6))) || this.IsValidKey(text))
            {
                if (this.uTE_opervalue.Text.Trim().Length == 0)
                {
                    text = "*";
                }
                if (this.lstBox_Text.Items.Count == 0)
                {
                    this.lstBox_Text.Items.Add(this.cmBox_fname.Text + " " + this.cmBox_oper.Text + " " + text);
                    this.lstBox_value.Items.Add(this.GetOptionSet(this.cmBox_oper.Text, this.cmBox_fname.Text, text));
                }
                else
                {
                    this.lstBox_Text.Items.Add(this.cmbBox_relation.Text);
                    this.lstBox_value.Items.Add(this.GetRelation(this.cmbBox_relation.Text));
                    this.lstBox_Text.Items.Add(this.cmBox_fname.Text + " " + this.cmBox_oper.Text + " " + text);
                    this.lstBox_value.Items.Add(this.GetOptionSet(this.cmBox_oper.Text, this.cmBox_fname.Text, text));
                }
            }
            else
            {
                MessageBoxPLM.Show("数值型格式不正确");
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.lstBox_Text.Items.Clear();
            this.lstBox_value.Items.Clear();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int selectedIndex = 0;
            if (this.lstBox_Text.SelectedIndex >= 0)
            {
                selectedIndex = this.lstBox_Text.SelectedIndex;
                if (this.lstBox_Text.SelectedIndex == 0)
                {
                    if (this.lstBox_Text.Items.Count > 1)
                    {
                        this.lstBox_Text.Items.RemoveAt(0);
                        this.lstBox_Text.Items.RemoveAt(0);
                        this.lstBox_value.Items.RemoveAt(0);
                        this.lstBox_value.Items.RemoveAt(0);
                    }
                    else
                    {
                        this.lstBox_Text.Items.RemoveAt(0);
                        this.lstBox_value.Items.RemoveAt(0);
                    }
                }
                if (((this.lstBox_Text.SelectedIndex > 1) && (this.lstBox_Text.SelectedIndex <= this.lstBox_Text.Items.Count)) && (this.lstBox_Text.Items.Count > 1))
                {
                    this.lstBox_Text.Items.RemoveAt(selectedIndex - 1);
                    this.lstBox_Text.Items.RemoveAt(selectedIndex - 1);
                    this.lstBox_value.Items.RemoveAt(selectedIndex - 1);
                    this.lstBox_value.Items.RemoveAt(selectedIndex - 1);
                }
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.lstBox_value.Items.Count != 0)
            {
                string str = "";
                foreach (string str2 in this.lstBox_value.Items)
                {
                    str = str + " " + str2;
                }
                this.myView.RowFilter = str;
            }
        }

        private void cmBox_fname_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = 0;
            selectedIndex = this.cmBox_fname.SelectedIndex;
            int num2 = 0;
            if (this.Attrs != null)
            {
                foreach (DEMetaAttribute attribute in this.Attrs)
                {
                    if ((attribute.DataType != 8) && (attribute.Label != this.comColumn))
                    {
                        if (num2 == selectedIndex)
                        {
                            this.SetOperValue(attribute);
                            this.curFieldAttr = attribute;
                            this.cmBox_fname.Text = attribute.Label;
                            break;
                        }
                        num2++;
                    }
                }
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

        private string GetOptionSet(string str_in, string str_fname, string str_fvalue)
        {
            string str2 = "";
            str2 = str_fvalue;
            switch (str_in)
            {
                case "等于":
                    if ((this.curFieldAttr.DataType2 != PLMDataType.String) && (this.curFieldAttr.DataType2 != PLMDataType.Char))
                    {
                        return (str_fname + " = " + str2);
                    }
                    return (str_fname + " = '" + str2 + "'");

                case "是":
                    if ((this.curFieldAttr.DataType2 != PLMDataType.String) && (this.curFieldAttr.DataType2 != PLMDataType.Char))
                    {
                        return (str_fname + " = " + str2);
                    }
                    return (str_fname + " = '" + str2 + "'");

                case "不是":
                    return (str_fname + " <> '" + str2 + "'");

                case "不等于":
                    return (str_fname + " <> " + str2);

                case "前几字符是":
                    return (str_fname + " like '" + str2 + "*'");

                case "包含":
                    return (str_fname + " like '*" + str2 + "*'");

                case "后几字符是":
                    return (str_fname + " like '*" + str2 + "'");

                case "大于":
                    return (str_fname + " > " + str2);

                case "小于":
                    return (str_fname + " < " + str2);

                case "大于等于":
                    return (str_fname + " >= " + str2);

                case "小于等于":
                    return (str_fname + " <= " + str2);
            }
            if ((this.curFieldAttr.DataType2 == PLMDataType.String) || (this.curFieldAttr.DataType2 == PLMDataType.Char))
            {
                return (str_fname + " = '" + str2 + "'");
            }
            return (str_fname + " = " + str2);
        }

        private string GetRelation(string str_in)
        {
            string str = "";
            if (str_in.Equals("并且"))
            {
                str = "AND";
            }
            if (str_in.Equals("或者"))
            {
                str = "OR";
            }
            return str;
        }

        private void InitFilterData()
        {
            int num = 0;
            if (this.Attrs != null)
            {
                foreach (DEMetaAttribute attribute in this.Attrs)
                {
                    if ((attribute.DataType != 8) && (attribute.Label != this.comColumn))
                    {
                        this.cmBox_fname.Properties.Items.Add(attribute.Label);
                        if (num == 0)
                        {
                            this.SetOperValue(attribute);
                            this.curFieldAttr = attribute;
                            this.cmBox_fname.Text = attribute.Label;
                        }
                        num++;
                    }
                }
            }
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
                    this.SetDisplayGrid();
                    this.InitFilterData();
                }
            }
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

        private bool IsValidKey(string strKey)
        {
            int num = 0;
            int num2 = 0;
            string str = "0123456789.-";
            if ((strKey.Substring(0, 1) == "0") && (strKey.Substring(1, 1) != "."))
            {
                return false;
            }
            for (int i = 0; i < strKey.Length; i++)
            {
                if (strKey.Substring(i, 1) == ".")
                {
                    num++;
                }
                if (strKey.Substring(i, 1) == "-")
                {
                    num2++;
                }
                if (((str.IndexOf(strKey.Substring(i, 1)) < 0) || (num > 1)) || (num2 > 1))
                {
                    return false;
                }
            }
            return true;
        }

        private void SetDisplayGrid()
        {
            int num = 0;
            int index = 0;
            int count = 0;
            string label = "";
            count = this.Attrs.Count;
            foreach (DEMetaAttribute attribute in this.Attrs)
            {
                if (attribute.DataType == 8)
                {
                    label = attribute.Label;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                    count--;
                }
                if (attribute.Name == "CREATETIME")
                {
                    label = attribute.Label;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                    count--;
                }
                if (attribute.Name == "CHECKINTIME")
                {
                    label = attribute.Label;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                    count--;
                }
                if (attribute.Name == "CHECKINDESC")
                {
                    label = attribute.Label;
                    this.uGrid_Res.DisplayLayout.Bands[0].Columns[label].Hidden = true;
                    count--;
                }
                if (attribute.Name == "ITERATION")
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
                    this.panel4.Controls.Add(this.txtbox[index]);
                    this.txtbox[index].TextChanged += new EventHandler(this.txtbox_TextChanged);
                    index++;
                    this.panel4.Width = num + 0x16;
                    this.uGrid_Res.Width = this.panel4.Width;
                    this.uGrid_Res.Height = this.panel1.Height;
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

        private void SetOperValue(DEMetaAttribute myAttr)
        {
            this.cmBox_oper.Properties.Items.Clear();
            if (((myAttr.DataType == 0) || (myAttr.DataType == 1)) || ((myAttr.DataType == 2) || (myAttr.DataType == 6)))
            {
                this.cmBox_oper.Properties.Items.AddRange(new object[] { "等于", "大于", "小于", "大于等于", "小于等于", "不等于" });
                this.cmBox_oper.Text = "等于";
            }
            else if (((myAttr.DataType == 4) || (myAttr.DataType == 3)) || (myAttr.DataType == 5))
            {
                this.cmBox_oper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "是", "不是" });
                this.cmBox_oper.Text = "是";
            }
            else if (myAttr.DataType == 7)
            {
                this.cmBox_oper.Properties.Items.AddRange(new object[] { "等于", "大于", "小于", "大于等于", "小于等于" });
                this.cmBox_oper.Text = "等于";
            }
            else
            {
                this.cmBox_oper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "是", "不是", "等于", "大于", "小于", "大于等于", "小于等于", "不等于" });
                this.cmBox_oper.Text = "是";
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

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == this.btnFilter)
            {
                if (this.b_filter)
                {
                    this.b_filter = false;
                    this.panel1.Height = base.Height - this.toolBar1.Height;
                    this.panel2.Visible = false;
                }
                else
                {
                    this.panel1.Height = (base.Height - this.toolBar1.Height) / 2;
                    this.b_filter = true;
                    this.panel2.Visible = true;
                }
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
                if (str.Trim().Length > 0)
                {
                    this.myView.RowFilter = str;
                }
                else
                {
                    this.myView.RowFilter = this.txtbox[0].Name + " like '**'";
                }
            }
        }

        private void UCRes_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }

        private void uGrid_Res_AfterRowActivate(object sender, EventArgs e)
        {
            if (!this.b_start && (this.ResSelected != null))
            {
                this.ResSelected(this.uGrid_Res.ActiveRow.Cells[this.comColumn].Value.ToString());
            }
        }

        private void uGrid_Res_DoubleClick(object sender, EventArgs e)
        {
            base.Hide();
        }
    }
}

