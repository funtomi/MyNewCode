using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgProcessProperty : Form {
        private CLState state;

        public DlgProcessProperty(ArrayList lstProcess) {
            this.InitializeComponent();
            this.state = new CLState();
            this.cmbSizeModel.Items.Add("自动");
            this.cmbSizeModel.Items.Add("拉伸");
            this.cmbSizeModel.Items.Add("简单居中");
            this.cmbSizeModel.Items.Add("居中");
            this.cmbSizeModel.Text = this.state.SgnSizeMode;
            this.cmbSign.SelectedIndex = 0;
        }

        public DlgProcessProperty(CLState state) {
            this.InitializeComponent();
            if (state == null) {
                throw new Exception("该状态属性不完整。");
            }
            this.state = state;
            this.cmbProcess.Text = state.OprKey;
            this.cmbSizeModel.Items.Add("自动");
            this.cmbSizeModel.Items.Add("拉伸");
            this.cmbSizeModel.Items.Add("简单居中");
            this.cmbSizeModel.Items.Add("居中");
            this.cmbSizeModel.Text = state.SgnSizeMode;
            this.cmbSign.Text = state.EleSignature ? "是" : "否";
            this.txtCell.Text = state.CellSign.ToUpper();
            this.txtCellDate.Text = state.CellDate.ToUpper();
            this.chkTime.Checked = state.DateIncludeTime;
            this.textBox1.Text = state.CellDateFormat;
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (CLPPFile.CheckAddress(this.txtCell.Text)) {
                base.DialogResult = DialogResult.OK;
            } else {
                MessageBox.Show("签名单元格填写错误，请检查。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtCell.Focus();
                this.txtCell.SelectAll();
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

        public CLState State {
            get {
                this.state.OprKey = this.cmbProcess.Text;
                if ((this.state.OprKey == null) || (this.state.OprKey == "")) {
                    this.state.OprKey = this.cmbProcess.SelectedValue as string;
                }
                this.state.CellSign = this.txtCell.Text;
                this.state.EleSignature = this.cmbSign.Text == "是";
                if (this.state.EleSignature) {
                    this.state.SgnSizeMode = this.cmbSizeModel.Text;
                }
                this.state.CellDate = this.txtCellDate.Text.ToUpper().Trim();
                this.state.CellDateFormat = this.textBox1.Text;
                this.state.DateIncludeTime = this.chkTime.Checked;
                return this.state;
            }
        }
    }
}

