    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon
{
    public partial class FrmModifyFilter : FormPLM {
        private ArrayList attrList;

        private Guid clsOid;

        private string condition;

        private string[] myCond = new string[6];

        public FrmModifyFilter(ArrayList attrList, string[] myCond, Guid clsOid) {
            this.InitializeComponent();
            this.attrList = attrList;
            this.myCond = myCond;
            this.clsOid = clsOid;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            string str = "";
            string str2 = "";
            string str3 = "";
            StringBuilder builder = new StringBuilder();
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.clsOid);
            builder.Append("PLM_");
            if ((this.cobName.Text == null) || (this.cobName.Text == "")) {
                MessageBoxPLM.Show("请选择参数名！", "过滤数据", MessageBoxButtons.OK);
            } else if ((this.cobOper.Text == null) || (this.cobOper.Text == "")) {
                MessageBoxPLM.Show("请选择运算符！", "过滤数据", MessageBoxButtons.OK);
            } else {
                foreach (DEMetaAttribute attribute in this.attrList) {
                    if (this.cobName.Text == attribute.Label) {
                        if (attribute.DataType2 == PLMDataType.DateTime) {
                            try {
                                Convert.ToDateTime(this.txtBoxValue.Text.Trim());
                            } catch {
                                MessageBoxPLM.Show("日期型格式不正确");
                                return;
                            }
                        }
                        if (class2.IsRefResClass) {
                            DEMetaClass refCls = this.GetRefCls(this.clsOid);
                            if (attribute.Name.StartsWith("M_") || attribute.Name.StartsWith("R_")) {
                                builder.Append(attribute.Name);
                                if (attribute.DataType2 == PLMDataType.String) {
                                    this.myCond[0] = "UPPER(PLM_PSM_ITEMMASTER_REVISION.PLM_" + attribute.Name + ")";
                                } else {
                                    this.myCond[0] = "PLM_PSM_ITEMMASTER_REVISION.PLM_" + attribute.Name;
                                }
                            } else {
                                builder.Append(attribute.Name);
                                string str4 = "PLM_CUS_".Replace("_CUS_", "_CUSV_") + refCls.Name;
                                if (attribute.DataType2 == PLMDataType.String) {
                                    this.myCond[0] = "UPPER(" + str4 + ".PLM_" + attribute.Name + ")";
                                } else {
                                    this.myCond[0] = str4 + ".PLM_" + attribute.Name;
                                }
                            }
                            str = "Ver" + attribute.Name;
                            str2 = Convert.ToString(attribute.DataType);
                        } else {
                            builder.Append(attribute.Name);
                            if (attribute.DataType2 == PLMDataType.String) {
                                this.myCond[0] = "UPPER(PLM_" + attribute.Name + ")";
                            } else {
                                this.myCond[0] = "PLM_" + attribute.Name;
                            }
                            str = "Ver" + attribute.Name;
                            str2 = Convert.ToString(attribute.DataType);
                        }
                        if ((attribute.DataType2 == PLMDataType.Decimal) && (this.txtBoxValue.Text.Trim().IndexOf(".") < 0)) {
                            this.txtBoxValue.Text = this.txtBoxValue.Text.Trim() + ".0";
                        }
                        break;
                    }
                }
                if ((this.cobOper.Text != null) && (this.cobOper.Text != "")) {
                    switch (this.cobOper.Text) {
                        case "是":
                            builder.Append("=");
                            this.myCond[1] = " = ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                builder.Append(" '");
                                builder.Append("' ");
                            }
                            str3 = this.txtBoxValue.Text.ToUpper();
                            break;

                        case "不是":
                            builder.Append("<>");
                            this.myCond[1] = " <> ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("' ");
                            } else {
                                builder.Append(" '");
                                builder.Append("' ");
                            }
                            str3 = this.txtBoxValue.Text.ToUpper();
                            break;

                        case "前几字符是":
                            builder.Append(" LIKE ");
                            this.myCond[1] = " LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("*' ");
                                str3 = this.txtBoxValue.Text.Trim().ToUpper() + "%";
                                break;
                            }
                            builder.Append(" '");
                            builder.Append("*' ");
                            str3 = "%";
                            break;

                        case "前几字符不包含":
                            builder.Append(" NOT LIKE ");
                            this.myCond[1] = " NOT LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append(" '");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("*' ");
                                str3 = this.txtBoxValue.Text.Trim().ToUpper() + "%";
                                break;
                            }
                            builder.Append(" '");
                            builder.Append("*' ");
                            str3 = "%";
                            break;

                        case "后几字符是":
                            builder.Append(" LIKE ");
                            this.myCond[1] = " LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append("'*");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("' ");
                                str3 = "%" + this.txtBoxValue.Text.Trim().ToUpper();
                                break;
                            }
                            builder.Append("'*");
                            builder.Append("' ");
                            str3 = "%";
                            break;

                        case "后几字符不包含":
                            builder.Append(" NOT LIKE ");
                            this.myCond[1] = " NOT LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append("'*");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("' ");
                                str3 = "%" + this.txtBoxValue.Text.Trim().ToUpper();
                                break;
                            }
                            builder.Append("'*");
                            builder.Append("' ");
                            str3 = "%";
                            break;

                        case "包含":
                            builder.Append(" LIKE ");
                            this.myCond[1] = " LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append("'*");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("*' ");
                            } else {
                                builder.Append("'*");
                                builder.Append("' ");
                            }
                            str3 = "%" + this.txtBoxValue.Text.Trim().ToUpper() + "%";
                            break;

                        case "不包含":
                            builder.Append(" NOT LIKE ");
                            this.myCond[1] = " NOT LIKE ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text != "")) {
                                builder.Append("'*");
                                builder.Append(this.txtBoxValue.Text.ToUpper());
                                builder.Append("*' ");
                            } else {
                                builder.Append("'*");
                                builder.Append("' ");
                            }
                            str3 = "%" + this.txtBoxValue.Text.Trim().ToUpper() + "%";
                            break;

                        case "为空":
                            builder.Append(" IS NULL ");
                            this.myCond[1] = " IS NULL ";
                            str3 = " ";
                            break;

                        case "不为空":
                            builder.Append(" IS NOT NULL ");
                            this.myCond[1] = " IS NOT NULL ";
                            str3 = " ";
                            break;

                        case ">":
                            builder.Append(" > ");
                            this.myCond[1] = " > ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                str3 = this.txtBoxValue.Text.ToUpper();
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;

                        case "<":
                            builder.Append(" < ");
                            this.myCond[1] = " < ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                str3 = this.txtBoxValue.Text.ToUpper();
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;

                        case "=":
                            builder.Append(" = ");
                            this.myCond[1] = " = ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                str3 = this.txtBoxValue.Text.ToUpper();
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;

                        case "<=":
                            builder.Append(" <= ");
                            this.myCond[1] = " <= ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                str3 = this.txtBoxValue.Text.ToUpper();
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;

                        case ">=":
                            builder.Append(" >= ");
                            this.myCond[1] = " >= ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                str3 = this.txtBoxValue.Text.ToUpper();
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;

                        case "<>":
                            builder.Append(" <> ");
                            this.myCond[1] = " <> ";
                            if ((this.txtBoxValue.Text != null) && (this.txtBoxValue.Text.Trim() != "")) {
                                str3 = this.txtBoxValue.Text.ToUpper();
                                builder.Append(this.txtBoxValue.Text.Trim().ToUpper());
                                break;
                            }
                            MessageBoxPLM.Show("请输入参数值！", "过滤数据", MessageBoxButtons.OK);
                            this.txtBoxValue.Focus();
                            return;
                    }
                } else {
                    MessageBoxPLM.Show("请选择运算符！", "过滤数据", MessageBoxButtons.OK);
                    return;
                }
                if (this.txtBoxValue.Text != null) {
                    this.myCond[2] = str3.ToUpper();
                } else {
                    this.myCond[2] = "";
                }
                this.myCond[3] = str;
                this.myCond[4] = str2;
                this.myCond[5] = this.cobOper.Text;
                this.condition = builder.ToString();
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void cobName_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.cobName.SelectedIndex > -1) {
                DEMetaAttribute myAttr = (DEMetaAttribute)this.attrList[this.cobName.SelectedIndex];
                this.LoadOperData(myAttr);
            }
        }

        private void cobOper_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.cobOper.Text.IndexOf("空") != -1) {
                this.txtBoxValue.Enabled = false;
            } else {
                this.txtBoxValue.Enabled = true;
            }
        }

        private void FrmModifyFilter_Load(object sender, EventArgs e) {
            foreach (DEMetaAttribute attribute in this.attrList) {
                this.cobName.Properties.Items.Add(attribute.Label);
                if (this.myCond[0] != null) {
                    string str = "PLM_" + attribute.Name;
                    if (this.myCond[0].ToString().IndexOf(str) != -1) {
                        this.LoadOperData(attribute);
                        this.cobName.Text = attribute.Label;
                    }
                }
            }
            if (this.myCond[1] != null) {
                this.cobOper.Text = this.myCond[5].Trim();
            }
            if (this.myCond[2] != null) {
                string str2 = this.myCond[2];
                str2 = str2.Replace("%", "");
                this.txtBoxValue.Text = str2;
            }
        }

        private string GetAttributeLabel(string className, string attrName) {
            DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(className, attrName);
            return attribute.Label;
        }

        private string GetOperString(string str_oper) {
            return ConstProduct.GetExpression(ConstProduct.GetOperatorType(str_oper));
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


        private void LoadOperData(DEMetaAttribute myAttr) {
            this.cobOper.Properties.Items.Clear();
            if (((myAttr.DataType == 0) || (myAttr.DataType == 1)) || ((myAttr.DataType == 2) || (myAttr.DataType == 6))) {
                this.cobOper.Properties.Items.AddRange(new object[] { "=", ">", "<", ">=", "<=", "<>", "为空", "不为空" });
                this.cobOper.Text = "=";
            } else if (((myAttr.DataType == 4) || (myAttr.DataType == 7)) || ((myAttr.DataType == 3) || (myAttr.DataType == 5))) {
                this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "不包含", "前几字符不包含", "后几字符不包含", "是", "不是", "为空", "不为空" });
                this.cobOper.Text = "包含";
            } else {
                this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "不包含", "前几字符不包含", "后几字符不包含", "是", "不是", "为空", "不为空", "=", ">", "<", ">=", "<=", "<>" });
                this.cobOper.Text = "包含";
            }
        }

        public string[] newCond {
            get {
                return this.myCond;
            }
        }
        public string newConditon {
            get {
                return this.condition;
            }
        }
    }
}

