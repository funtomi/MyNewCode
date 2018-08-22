using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgHistoryProperty : Form {
        private CLHistory history;

        public DlgHistoryProperty(int order) {
            this.InitializeComponent();
            this.history = new CLHistory();
            this.history.Order = order;
            this.labOrder.Text = order.ToString();
            this.chkTime.Checked = false;
            this.cmbSizeModel.Items.Add("自动");
            this.cmbSizeModel.Items.Add("拉伸");
            this.cmbSizeModel.Items.Add("简单居中");
            this.cmbSizeModel.Items.Add("居中");
            this.cmbSizeModel.Text = this.history.SgnSizeMode;
            this.cmbSign.SelectedIndex = 1;
        }

        public DlgHistoryProperty(CLHistory history) {
            this.InitializeComponent();
            this.history = history;
            if (this.history != null) {
                this.labOrder.Text = history.Order.ToString();
                string str = string.Empty;
                if (history.CellsRemark.Count > 0) {
                    foreach (string str2 in history.CellsRemark) {
                        str = str + str2 + ";";
                    }
                    str = str.Substring(0, str.Length - 1);
                }
                this.txtCellsRemark.Text = str;
                this.txtCellSign.Text = history.CellSign.ToUpper();
                this.txtCellDate.Text = history.CellDate.ToUpper();
                this.textBox1.Text = history.CellDateFormat;
                this.chkTime.Checked = history.DateIncludeTime;
                this.cmbSizeModel.Items.Add("自动");
                this.cmbSizeModel.Items.Add("拉伸");
                this.cmbSizeModel.Items.Add("简单居中");
                this.cmbSizeModel.Items.Add("居中");
                this.cmbSizeModel.Text = this.history.SgnSizeMode;
                this.cmbSign.Text = history.EleSignature ? "是" : "否";
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (!CLPPFile.CheckAddress(this.txtCellSign.Text)) {
                MessageBox.Show("签名单元格填写错误，请检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtCellSign.Focus();
                this.txtCellSign.SelectAll();
            } else if (!CLPPFile.CheckAddress(this.txtCellDate.Text)) {
                MessageBox.Show("日期单元格填写错误，请检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtCellDate.Focus();
                this.txtCellDate.SelectAll();
            } else if (((this.txtCellsRemark.Text.IndexOf(";") > 1) || (this.txtCellsRemark.Text.IndexOf(",") > 1)) || ((this.txtCellsRemark.Text.IndexOf("，") > 1) || (this.txtCellsRemark.Text.IndexOf("；") > 1))) {
                foreach (string str in this.txtCellsRemark.Text.Split(new char[] { ';', ',', '，', '；' })) {
                    if (!CLPPFile.CheckAddress(str)) {
                        MessageBox.Show("修改说明单元格填写错误，请检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtCellsRemark.Focus();
                        this.txtCellsRemark.SelectAll();
                        return;
                    }
                }
                base.DialogResult = DialogResult.OK;
            } else if (!CLPPFile.CheckAddress(this.txtCellsRemark.Text)) {
                MessageBox.Show("修改说明单元格填写错误，请检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtCellsRemark.Focus();
                this.txtCellsRemark.SelectAll();
            } else {
                base.DialogResult = DialogResult.OK;
            }
        }

        private void cmbSign_SelectedIndexChanged(object sender, EventArgs e) {
            this.cmbSizeModel.Enabled = this.cmbSign.Text == "是";
        }

        private void cmbSizeModel_SelectionChangeCommitted(object sender, EventArgs e) {
            string text = this.cmbSizeModel.Text;
            if (text != null) {
                if (text != "自动") {
                    if (text != "拉伸") {
                        if (text == "简单居中") {
                            this.lblRemark.Text = "图片居中。只有图片小于指定区域时，才不会变形，图片太大时，只显示一部分。";
                            return;
                        }
                        if (text == "居中") {
                            this.lblRemark.Text = "图片居中。不受图片大小影响，不会变形，但是刷新效果不是很好。";
                        }
                        return;
                    }
                } else {
                    this.lblRemark.Text = "图片自动适应指定区域，不会变形，图片左上角与区域左上角对齐。";
                    return;
                }
                this.lblRemark.Text = "拉伸或者缩小以适应指定区域，可能变形。";
            }
        }

        public CLHistory History {
            get {
                this.history.CellsRemark = new ArrayList(this.txtCellsRemark.Text.Trim().Split(new char[] { ';', ',', '，', '；' }));
                this.history.CellSign = this.txtCellSign.Text;
                this.history.CellDate = this.txtCellDate.Text;
                this.history.CellDateFormat = this.textBox1.Text;
                this.history.DateIncludeTime = this.chkTime.Checked;
                this.history.EleSignature = this.cmbSign.Text == "是";
                if (this.history.EleSignature) {
                    this.history.SgnSizeMode = this.cmbSizeModel.Text;
                }
                return this.history;
            }
        }
    }
}

