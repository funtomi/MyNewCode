    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon
{
    public partial class FrmFilterData : FormPLM {
        private DEMetaAttribute _curAttr;

        private string clsName;
        private Guid clsOid;

        private ArrayList filterAttrs;
        private bool isOnLineOutRes;

        private SortableListView lvwCondition;
        private StringBuilder myConditon;
        private string strFilterString;
        private string strFilterValue;

        public FrmFilterData(string clsName) {
            this.myConditon = new StringBuilder();
            this.strFilterString = "";
            this.strFilterValue = "";
            this.filterAttrs = new ArrayList();
            this.clsOid = Guid.Empty;
            this.InitializeComponent();
            this.clsName = clsName;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.clsName);
            this.clsOid = class2.Oid;
            if (class2.IsOuterResClass) {
                this.isOnLineOutRes = true;
            }
        }

        public FrmFilterData(string clsName, Guid clsOid, bool isOnLineOutRes) {
            this.myConditon = new StringBuilder();
            this.strFilterString = "";
            this.strFilterValue = "";
            this.filterAttrs = new ArrayList();
            this.clsOid = Guid.Empty;
            this.InitializeComponent();
            this.clsOid = clsOid;
            this.clsName = clsName;
            this.isOnLineOutRes = isOnLineOutRes;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            string str = "";
            string str2 = "";
            StringBuilder builder = new StringBuilder();
            string[] strArray = new string[6];
            string str3 = "";
            bool flag = false;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.clsOid);
            builder.Append("PLM_");
            if ((this.cobParamName.Text == null) || (this.cobParamName.Text == "")) {
                MessageBoxPLM.Show("请选择参数名！", "过滤数据", MessageBoxButtons.OK);
            } else {
                foreach (DEMetaAttribute attribute in this.filterAttrs) {
                    if (this.cobParamName.Text == attribute.Label) {
                        if (attribute.DataType2 == PLMDataType.DateTime) {
                            try {
                                Convert.ToDateTime(this.txtParamValue.Text.Trim());
                                flag = true;
                            } catch {
                                MessageBoxPLM.Show("日期型格式不正确");
                                return;
                            }
                        }
                        if ((((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6))) && !this.IsValidKey(this.txtParamValue.Text.Trim())) {
                            MessageBoxPLM.Show("数值型格式不正确");
                            return;
                        }
                        if (class2.IsRefResClass) {
                            DEMetaClass refCls = this.GetRefCls(this.clsOid);
                            if (attribute.Name.StartsWith("M_") || attribute.Name.StartsWith("R_")) {
                                builder.Append(attribute.Name);
                                if (attribute.DataType2 == PLMDataType.String) {
                                    strArray[0] = "UPPER(PLM_PSM_ITEMMASTER_REVISION.PLM_" + attribute.Name + ")";
                                } else {
                                    strArray[0] = "PLM_PSM_ITEMMASTER_REVISION.PLM_" + attribute.Name;
                                }
                            } else {
                                builder.Append(attribute.Name);
                                string str4 = "PLM_CUS_".Replace("_CUS_", "_CUSV_") + refCls.Name;
                                if (attribute.DataType2 == PLMDataType.String) {
                                    strArray[0] = "UPPER(" + str4 + ".PLM_" + attribute.Name + ")";
                                } else {
                                    strArray[0] = str4 + ".PLM_" + attribute.Name;
                                }
                            }
                            str = "Ver" + attribute.Name;
                            str2 = Convert.ToString(attribute.DataType);
                        } else {
                            builder.Append(attribute.Name);
                            if (attribute.DataType2 == PLMDataType.String) {
                                strArray[0] = "UPPER(PLM_" + attribute.Name + ")";
                            } else {
                                strArray[0] = "PLM_" + attribute.Name;
                            }
                            str = "Ver" + attribute.Name;
                            str2 = Convert.ToString(attribute.DataType);
                        }
                        if ((attribute.DataType2 == PLMDataType.Decimal) && (this.txtParamValue.Text.Trim().IndexOf(".") == -1)) {
                            this.txtParamValue.Text = this.txtParamValue.Text.Trim() + ".0";
                        }
                        break;
                    }
                }
                if ((this.cobOper.Text != null) && (this.cobOper.Text != "")) {
                    switch (this.cobOper.Text) {
                        case "是":
                            builder.Append("=");
                            strArray[1] = " = ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                builder.Append(" '");
                                builder.Append("' ");
                            }
                            str3 = this.txtParamValue.Text.ToUpper();
                            break;

                        case "不是":
                            builder.Append("<>");
                            strArray[1] = " <> ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                builder.Append(" '");
                                builder.Append("' ");
                            }
                            str3 = this.txtParamValue.Text.ToUpper();
                            break;

                        case "前几字符是":
                            builder.Append(" LIKE ");
                            strArray[1] = " LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                                str3 = this.txtParamValue.Text.Trim().ToUpper() + "%";
                                break;
                            }
                            builder.Append(" '");
                            if (this.isOnLineOutRes) {
                                builder.Append("%' ");
                            } else {
                                builder.Append("*' ");
                            }
                            str3 = "%";
                            break;

                        case "前几字符不包含":
                            builder.Append(" NOT LIKE ");
                            strArray[1] = " NOT LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                                str3 = this.txtParamValue.Text.Trim().ToUpper() + "%";
                                break;
                            }
                            builder.Append(" '");
                            if (this.isOnLineOutRes) {
                                builder.Append("%' ");
                            } else {
                                builder.Append("*' ");
                            }
                            str3 = "%";
                            break;

                        case "后几字符是":
                            builder.Append(" LIKE ");
                            strArray[1] = " LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append("' ");
                                str3 = "%";
                            }
                            str3 = "%" + this.txtParamValue.Text.Trim().ToUpper();
                            break;

                        case "后几字符不包含":
                            builder.Append(" NOT LIKE ");
                            strArray[1] = " NOT LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append("' ");
                                str3 = "%";
                            }
                            str3 = "%" + this.txtParamValue.Text.Trim().ToUpper();
                            break;

                        case "包含":
                            builder.Append(" LIKE ");
                            strArray[1] = " LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                            } else {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                            }
                            str3 = "%" + this.txtParamValue.Text.Trim().ToUpper() + "%";
                            break;

                        case "不包含":
                            builder.Append(" NOT LIKE ");
                            strArray[1] = " NOT LIKE ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text != "")) {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                builder.Append(this.txtParamValue.Text.ToUpper());
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                            } else {
                                if (this.isOnLineOutRes) {
                                    builder.Append("'%");
                                } else {
                                    builder.Append("'*");
                                }
                                if (this.isOnLineOutRes) {
                                    builder.Append("%' ");
                                } else {
                                    builder.Append("*' ");
                                }
                            }
                            str3 = "%" + this.txtParamValue.Text.Trim().ToUpper() + "%";
                            break;

                        case "为空":
                            builder.Append(" IS NULL ");
                            strArray[1] = " IS NULL ";
                            str3 = " ";
                            break;

                        case "不为空":
                            builder.Append(" IS NOT NULL ");
                            strArray[1] = " IS NOT NULL ";
                            str3 = " ";
                            break;

                        case ">":
                            builder.Append(" > ");
                            strArray[1] = " > ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                if (flag) {
                                    str3 = "'" + this.txtParamValue.Text.Trim() + "'";
                                    builder.Append(str3);
                                } else {
                                    str3 = this.txtParamValue.Text.Trim().ToUpper();
                                    builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                }
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;

                        case "<":
                            builder.Append(" < ");
                            strArray[1] = " < ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                if (flag) {
                                    str3 = "'" + this.txtParamValue.Text.Trim() + "'";
                                    builder.Append(str3);
                                } else {
                                    str3 = this.txtParamValue.Text.Trim().ToUpper();
                                    builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                }
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;

                        case "=":
                            builder.Append(" = ");
                            strArray[1] = " = ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                if (flag) {
                                    str3 = "'" + this.txtParamValue.Text.Trim() + "'";
                                    builder.Append(str3);
                                } else {
                                    str3 = this.txtParamValue.Text.Trim().ToUpper();
                                    builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                }
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;

                        case "<=":
                            builder.Append(" <= ");
                            strArray[1] = " <= ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                if (flag) {
                                    str3 = "'" + this.txtParamValue.Text.Trim() + "'";
                                    builder.Append(str3);
                                } else {
                                    str3 = this.txtParamValue.Text.Trim().ToUpper();
                                    builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                }
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;

                        case ">=":
                            builder.Append(" >= ");
                            strArray[1] = " >= ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                if (flag) {
                                    str3 = "'" + this.txtParamValue.Text.Trim() + "'";
                                    builder.Append(str3);
                                } else {
                                    str3 = this.txtParamValue.Text.Trim().ToUpper();
                                    builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                }
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;

                        case "<>":
                            builder.Append(" <> ");
                            strArray[1] = " <> ";
                            if ((this.txtParamValue.Text != null) && (this.txtParamValue.Text.Trim() != "")) {
                                builder.Append(this.txtParamValue.Text.Trim().ToUpper());
                                str3 = this.txtParamValue.Text.Trim().ToUpper();
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtParamValue.Focus();
                            return;
                    }
                } else {
                    MessageBoxPLM.Show("请选择运算符！", "过滤数据", MessageBoxButtons.OK);
                    return;
                }
                if (this.txtParamValue.Text != null) {
                    strArray[2] = str3.ToUpper();
                } else {
                    strArray[2] = "";
                }
                strArray[3] = str;
                strArray[4] = str2;
                strArray[5] = this.cobOper.Text;
                int num = this.lvwCondition.Items.Count + 1;
                this.lvwCondition.Items.Add(num.ToString());
                this.lvwCondition.Items[num - 1].SubItems.Add(builder.ToString());
                this.lvwCondition.Items[num - 1].Tag = strArray;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            base.Close();
        }

        private void btnClear_Click(object sender, EventArgs e) {
            this.lvwCondition.Items.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.lvwCondition.SelectedItems.Count > 0) {
                foreach (ListViewItem item in this.lvwCondition.SelectedItems) {
                    this.lvwCondition.Items.Remove(item);
                }
                foreach (ListViewItem item2 in this.lvwCondition.Items) {
                    item2.SubItems[0].Text = (item2.Index + 1).ToString();
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e) {
            if ((this.lvwCondition.SelectedItems.Count > 0) && (this.filterAttrs != null)) {
                string[] tag = (string[])this.lvwCondition.SelectedItems[0].Tag;
                FrmModifyFilter filter = new FrmModifyFilter(this.filterAttrs, tag, this.clsOid);
                if (filter.ShowDialog() == DialogResult.OK) {
                    if (filter.newConditon != "") {
                        this.lvwCondition.SelectedItems[0].SubItems[1].Text = filter.newConditon;
                    }
                    if (filter.newCond != null) {
                        this.lvwCondition.SelectedItems[0].Tag = filter.newCond;
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            string str = " ";
            string str2 = this.txtCondition.Text.ToUpper();
            char[] separator = str.ToCharArray();
            string[] strArray = null;
            string[] strArray2 = null;
            if (this.lvwCondition.Items.Count <= 0) {
                base.DialogResult = DialogResult.Cancel;
            } else {
                int num;
                if (str2 != "") {
                    strArray = str2.Split(separator);
                    strArray2 = str2.Split(separator);
                    for (num = 1; num <= this.lvwCondition.Items.Count; num++) {
                        for (int i = 0; i < strArray.Length; i++) {
                            if (num.ToString() == strArray[i]) {
                                strArray[i] = this.lvwCondition.Items[num - 1].SubItems[1].Text;
                                string[] tag = (string[])this.lvwCondition.Items[num - 1].Tag;
                                if (tag[1].ToString().IndexOf("IS") != -1) {
                                    strArray2[i] = tag[0].ToString() + tag[1].ToString();
                                } else {
                                    strArray2[i] = tag[0].ToString() + tag[1].ToString() + ":" + tag[3].ToString();
                                    if (this.strFilterValue.Trim().Length > 0) {
                                        this.strFilterValue = this.strFilterValue + ",";
                                    }
                                    this.strFilterValue = this.strFilterValue + tag[2].ToString() + "|" + tag[4].ToString();
                                }
                            }
                        }
                    }
                    this.strFilterString = this.strFilterString + " ( ";
                    for (num = 0; num < strArray.Length; num++) {
                        if (num > 0) {
                            builder.Append(" ");
                        }
                        builder.Append(strArray[num]);
                        if (num > 0) {
                            this.strFilterString = this.strFilterString + " ";
                        }
                        this.strFilterString = this.strFilterString + strArray2[num].ToString();
                    }
                    this.strFilterString = this.strFilterString + " ) ";
                } else {
                    num = 0;
                    foreach (ListViewItem item in this.lvwCondition.Items) {
                        if (num > 0) {
                            builder.Append("AND ");
                        }
                        builder.Append(item.SubItems[1].Text);
                        string[] strArray4 = (string[])item.Tag;
                        if (num > 0) {
                            this.strFilterString = this.strFilterString + " AND ";
                        }
                        if (strArray4[1].ToString().IndexOf("IS") != -1) {
                            this.strFilterString = this.strFilterString + strArray4[0].ToString() + strArray4[1].ToString();
                        } else {
                            string strFilterString = this.strFilterString;
                            this.strFilterString = strFilterString + strArray4[0].ToString() + strArray4[1].ToString() + ":" + strArray4[3].ToString();
                            if (this.strFilterValue.Trim().Length > 0) {
                                this.strFilterValue = this.strFilterValue + ",";
                            }
                            this.strFilterValue = this.strFilterValue + strArray4[2].ToString() + "|" + strArray4[4].ToString();
                        }
                        num++;
                    }
                }
                if (this.isOnLineOutRes) {
                    if (str2 != "") {
                        strArray = str2.Split(separator);
                        for (num = 1; num <= this.lvwCondition.Items.Count; num++) {
                            for (int j = 0; j < strArray.Length; j++) {
                                if (num.ToString() == strArray[j]) {
                                    strArray[j] = this.ReplaceAttrName(this.lvwCondition.Items[num - 1].SubItems[1].Text);
                                }
                            }
                        }
                        for (num = 0; num < strArray.Length; num++) {
                            if (num > 0) {
                                builder2.Append(" ");
                            }
                            builder2.Append(strArray[num]);
                        }
                    } else {
                        num = 0;
                        foreach (ListViewItem item2 in this.lvwCondition.Items) {
                            if (num > 0) {
                                builder2.Append("AND ");
                            }
                            string str4 = this.ReplaceAttrName(item2.SubItems[1].Text);
                            builder2.Append(str4);
                            num++;
                        }
                    }
                }
                if (builder2.Length > 0) {
                    this.myConditon = builder;
                    this.myConditon.Append(" [");
                    this.myConditon.Append(builder2);
                    this.myConditon.Append("]");
                } else {
                    this.myConditon = builder;
                }
                if (!this.CheckOptionByDA()) {
                    MessageBoxPLM.Show("筛选条件输入框有误！");
                    this.strFilterString = "";
                    this.strFilterValue = "";
                } else {
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                }
            }
        }

        private bool CheckOption() {
            bool flag = true;
            if (this.txtCondition.Text.Trim().Length != 0) {
                string str = "";
                string str2 = "";
                str = this.txtCondition.Text.Trim();
                str.ToLower();
                if (!str.StartsWith("1")) {
                    return false;
                }
                str2 = str.Replace(" ", "0").Replace("and", "0").Replace("or", "0").Replace("(", "0").Replace(")", "0");
                try {
                    Convert.ToInt64(str2);
                } catch {
                    flag = false;
                }
            }
            return flag;
        }

        private bool CheckOptionByDA() {
            bool flag = true;
            DEResFolder defolder = new DEResFolder {
                Oid = Guid.Empty,
                ClassOid = this.clsOid,
                ClassName = this.clsName,
                Filter = this.FilterConditon.ToString(),
                FilterString = this.FilterString,
                FilterValue = this.FilterValue
            };
            ArrayList list = new ArrayList();
            ArrayList outerAttr = new ArrayList();
            list = ResFunc.CloneMetaAttrLst(ResFunc.GetShowAttrList(defolder, emTreeType.NodeTree));
            outerAttr = ResFunc.GetOuterAttr(defolder);
            try {
                if (ResFunc.IsOnlineOutRes(defolder.ClassOid)) {
                    ResFunc.GetDataCount(defolder, list, outerAttr, emResourceType.OutSystem);
                    return flag;
                }
                if (ResFunc.IsRefRes(defolder.ClassOid)) {
                    PLSPL plspl = new PLSPL();
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(defolder.ClassOid);
                    DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                    plspl.GetSPLCount(class3.Name, list, ClientData.LogonUser.Oid, defolder.FilterString, defolder.FilterValue);
                    return flag;
                }
                if (ResFunc.IsTabRes(defolder.ClassOid)) {
                    ArrayList list4 = new ArrayList();
                    ResFunc.GetDataCount(defolder, list4, outerAttr, emResourceType.Customize);
                }
            } catch {
                return false;
            }
            return flag;
        }

        private void cobOper_SelectedIndexChanged(object sender, EventArgs e) {
            if (this._curAttr != null) {
                if (this.cobOper.Text.IndexOf("空") != -1) {
                    if (this._curAttr.DataType2 == PLMDataType.Decimal) {
                        this.txtParamValue.Text = "1.00";
                    }
                    if (((this._curAttr.DataType2 == PLMDataType.Int16) || (this._curAttr.DataType2 == PLMDataType.Int32)) || (this._curAttr.DataType2 == PLMDataType.Int64)) {
                        this.txtParamValue.Text = "1";
                    }
                    if (this._curAttr.DataType2 == PLMDataType.DateTime) {
                        this.txtParamValue.Text = DateTime.Now.ToString();
                    }
                    this.txtParamValue.Enabled = false;
                } else {
                    this.txtParamValue.Enabled = true;
                }
            }
        }

        private void cobParamName_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.cobParamName.SelectedIndex > -1) {
                this.cobOper.Properties.Items.Clear();
                DEMetaAttribute attribute = (DEMetaAttribute)this.filterAttrs[this.cobParamName.SelectedIndex];
                this._curAttr = attribute;
                if (((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6))) {
                    this.cobOper.Properties.Items.AddRange(new object[] { "=", ">", "<", ">=", "<=", "<>", "为空", "不为空" });
                    this.cobOper.Text = "=";
                } else if (((attribute.DataType == 4) || (attribute.DataType == 3)) || (attribute.DataType == 5)) {
                    this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "不包含", "前几字符不包含", "后几字符不包含", "是", "不是", "为空", "不为空" });
                    this.cobOper.Text = "包含";
                } else if (attribute.DataType == 7) {
                    this.cobOper.Properties.Items.AddRange(new object[] { "=", ">", "<", ">=", "<=", "为空", "不为空" });
                    this.cobOper.Text = "=";
                } else {
                    this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "不包含", "前几字符不包含", "后几字符不包含", "是", "不是", "为空", "不为空", "=", ">", "<", ">=", "<=", "<>" });
                    this.cobOper.Text = "包含";
                }
            }
        }

        private void FrmFilterData_Load(object sender, EventArgs e) {
            ArrayList attrList = new ArrayList();
            ArrayList attrOuter = new ArrayList();
            ArrayList list3 = new ArrayList();
            try {
                DEMetaAttribute attribute;
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.clsOid);
                attrList = ResFunc.CloneMetaAttrLst(ModelContext.MetaModel.GetAttributes(this.clsName));
                if (this.isOnLineOutRes) {
                    DEResFolder defolder = new DEResFolder {
                        ClassOid = this.clsOid
                    };
                    attrOuter = ResFunc.GetOuterAttr(defolder);
                    this.SetAttrDataType(attrList, attrOuter);
                }
                if (class2.IsRefResClass) {
                    DEMetaClass refCls = this.GetRefCls(this.clsOid);
                    ArrayList attributes = ModelContext.MetaModel.GetAttributes(refCls.Name);
                    ArrayList fixedAttrList = ResFunc.GetFixedAttrList();
                    attributes.AddRange(fixedAttrList);
                    attrList = ResFunc.CloneMetaAttrLst(attributes);
                }
                for (int i = 0; i < attrList.Count; i++) {
                    attribute = (DEMetaAttribute)attrList[i];
                    if (attribute.IsViewable) {
                        list3.Add(attribute.Label);
                        this.filterAttrs.Add(attribute);
                    }
                }
                if (this.isOnLineOutRes) {
                    PLOuterResource resource = new PLOuterResource();
                    ArrayList list7 = new ArrayList();
                    attrOuter = resource.GetOuterResAttrs(this.clsOid);
                    ArrayList list8 = new ArrayList();
                    for (int j = 0; j < this.filterAttrs.Count; j++) {
                        bool flag = false;
                        attribute = (DEMetaAttribute)this.filterAttrs[j];
                        foreach (DEOuterAttribute attribute2 in attrOuter) {
                            if (attribute2.FieldOid == attribute.Oid) {
                                list7.Add(attribute2);
                                flag = true;
                                break;
                            }
                        }
                        if (!flag) {
                            list8.Add(attribute);
                        }
                    }
                    foreach (DEMetaAttribute attribute3 in list8) {
                        this.filterAttrs.Remove(attribute3);
                        list3.Remove(attribute3.Label);
                    }
                    if (list7.Count > 0) {
                        this.cobParamName.Tag = list7;
                    }
                }
                if (list3.Count > 0) {
                    this.cobParamName.Properties.Items.AddRange(list3.ToArray());
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private DEMetaClass GetRefCls(Guid clsid) {
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(clsid);
            ArrayList list = new ArrayList();
            list = UIDataModel.GetChildren("RESOURCE", 0, true);
            if (list != null) {
                IEnumerator enumerator = list.GetEnumerator();
                while (enumerator.MoveNext()) {
                    DEMetaClass current = (DEMetaClass)enumerator.Current;
                    if ((current.IsRefResClass && !current.IsTableResClass) && current.Visible) {
                        DEMetaClass class4 = ModelContext.MetaModel.GetClass(current.RefClass);
                        if ((class4 != null) && (class2.Label == current.Label)) {
                            return class4;
                        }
                    }
                }
            }
            return null;
        }


        private bool IsValidKey(string strKey) {
            int num = 0;
            int num2 = 0;
            string str = "0123456789.-";
            if ((strKey.Substring(0, 1) == "0") && (strKey.Substring(1, 1) != ".")) {
                return false;
            }
            for (int i = 0; i < strKey.Length; i++) {
                if (strKey.Substring(i, 1) == ".") {
                    num++;
                }
                if (strKey.Substring(i, 1) == "-") {
                    num2++;
                }
                if (((str.IndexOf(strKey.Substring(i, 1)) < 0) || (num > 1)) || (num2 > 1)) {
                    return false;
                }
            }
            return true;
        }

        private string ReplaceAttrName(string condStr) {
            if ((condStr != null) && (condStr.Trim() != "")) {
                for (int i = 0; i < this.filterAttrs.Count; i++) {
                    string str = "PLM_";
                    DEMetaAttribute attribute = (DEMetaAttribute)this.filterAttrs[i];
                    str = str + attribute.Name;
                    if (condStr.IndexOf(str) == 0) {
                        if (typeof(ArrayList).IsInstanceOfType(this.cobParamName.Tag)) {
                            ArrayList tag = (ArrayList)this.cobParamName.Tag;
                            foreach (DEOuterAttribute attribute2 in tag) {
                                if (attribute2.FieldOid == attribute.Oid) {
                                    condStr = condStr.Replace(str, attribute2.OuterAttrName);
                                    return condStr;
                                }
                            }
                        }
                        return condStr;
                    }
                }
            }
            return condStr;
        }

        private void SetAttrDataType(ArrayList attrList, ArrayList attrOuter) {
            foreach (DEMetaAttribute attribute in attrList) {
                foreach (DEOuterAttribute attribute2 in attrOuter) {
                    if (attribute.Oid == attribute2.FieldOid) {
                        attribute.DataType = attribute2.DataType;
                        break;
                    }
                }
            }
        }

        public StringBuilder FilterConditon {
            get {
                return this.myConditon;
            }
        }
        public string FilterString {
            get {
                return this.strFilterString;
            }
        }
        public string FilterValue {
            get {
                return this.strFilterValue;
            }
        }
    }
}

