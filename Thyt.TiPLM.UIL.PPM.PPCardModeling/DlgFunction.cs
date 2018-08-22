using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgFunction : Form {

        private string colInMain;
        private string colInNext;
        private string functionType;
        private bool isMainPage;

        private CLCardTemplate m_tp;
        public string script;

        public DlgFunction(CLCardTemplate tp, bool isMain, CLFunction func) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.isMainPage = isMain;
            this.functionType = this.Text = func.FuncType;
            this.InitialColumn(null, null, func);
            this.SetControlState(true);
        }

        public DlgFunction(CLCardTemplate tp, ArrayList midColLstOfMain, ArrayList midColLstOfNext, bool isMain, CLFunction func, bool readOnly) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.isMainPage = isMain;
            this.functionType = this.Text = func.FuncType;
            this.InitialColumn(midColLstOfMain, midColLstOfNext, func);
            this.SetControlState(readOnly);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            try {
                this.SetScript();
                base.DialogResult = DialogResult.OK;
            } catch (Exception exception) {
                MessageBox.Show("设置统计列出错。具体原因为：\n" + exception.Message, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool CheckEnough() {
            if (((this.colInMain == null) || (this.colInMain == "")) && (this.isMainPage || (!this.isMainPage && (this.functionType == "全部页列总计")))) {
                MessageBox.Show("请选择首页表中列。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (this.isMainPage || ((this.colInNext != null) && (this.colInNext != ""))) {
                return true;
            }
            MessageBox.Show("请选择续页表中列。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        private void CreateScript() {
            if (this.CheckEnough()) {
                if ((this.colInNext == null) || (this.colInNext == "")) {
                    this.colInNext = this.colInMain;
                }
                CLFunction func = new CLFunction(this.functionType, this.colInMain, this.colInNext, this.chkOnlyLastPage.Checked);
                this.script = PPCardCompiler.CreateXML(func);
            }
        }

        private void InitialColumn(ArrayList midColLstOfMain, ArrayList midColLstOfNext, CLFunction func) {
            if (this.isMainPage || (this.functionType != "单页列小计")) {
                this.cmbMainPage.DataSource = midColLstOfMain;
                if (!string.IsNullOrEmpty(func.ColInMainPage)) {
                    if (this.cmbMainPage.Items.Count == 0) {
                        this.cmbMainPage.Items.Add(func.ColInMainPage);
                    }
                    this.cmbMainPage.Text = func.ColInMainPage;
                }
            }
            if (this.m_tp.HasNextPage && (!this.isMainPage || (this.functionType != "单页列小计"))) {
                this.cmbNextPage.DataSource = midColLstOfNext;
                if (!string.IsNullOrEmpty(func.ColInNextPage)) {
                    if (this.cmbNextPage.Items.Count == 0) {
                        this.cmbNextPage.Items.Add(func.ColInNextPage);
                    }
                    this.cmbNextPage.Text = func.ColInNextPage;
                }
            }
            this.chkOnlyLastPage.Checked = func.DisplayInLastPage;
        }

        private void SetControlState(bool readOnly) {
            if (readOnly) {
                this.groupBox1.Enabled = false;
                this.btnOK.Visible = false;
            } else {
                if (!this.m_tp.HasNextPage) {
                    this.cmbNextPage.Enabled = false;
                }
                if (this.functionType == "单页列小计") {
                    if (this.isMainPage) {
                        this.cmbNextPage.Enabled = false;
                    } else {
                        this.cmbMainPage.Enabled = false;
                    }
                    this.chkOnlyLastPage.Enabled = false;
                }
            }
        }

        private void SetScript() {
            if (this.cmbMainPage.SelectedItem != null) {
                this.colInMain = this.cmbMainPage.SelectedItem.ToString();
            }
            if (this.cmbNextPage.SelectedItem != null) {
                this.colInNext = this.cmbNextPage.SelectedItem.ToString();
            }
            this.CreateScript();
        }
    }
}

