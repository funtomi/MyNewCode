using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgDataHiddenCol : Form {

        public string dataHiddenCol;
        private bool hasNextPage;
        private bool isReadOnly;

        private ArrayList mainColList;
        private ArrayList nextColList;

        public DlgDataHiddenCol() {
            this.InitializeComponent();
        }

        public DlgDataHiddenCol(string dataHiddenCol, bool hasNext, bool isReadOnly, ArrayList mainColList, ArrayList nextColList) {
            this.InitializeComponent();
            this.dataHiddenCol = dataHiddenCol;
            this.hasNextPage = hasNext;
            this.isReadOnly = isReadOnly;
            this.mainColList = mainColList;
            this.nextColList = nextColList;
            this.InitialMainAndNextCol();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (this.isReadOnly) {
                base.DialogResult = DialogResult.OK;
            } else {
                string mainHiddenCol = this.cmbMain.Text.Trim().Replace(" ", "");
                string nextHiddenCol = mainHiddenCol;
                if (this.hasNextPage) {
                    nextHiddenCol = this.cmbNext.Text.Trim().Replace(" ", "");
                }
                if (this.ValidateHiddenCol(mainHiddenCol, nextHiddenCol, this.hasNextPage)) {
                    this.dataHiddenCol = mainHiddenCol + "‖" + nextHiddenCol;
                    if (this.dataHiddenCol == "‖") {
                        this.dataHiddenCol = string.Empty;
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
        }

        private void DlgDataHiddenCol_Load(object sender, EventArgs e) {
            if (this.isReadOnly) {
                this.groupBox1.Enabled = false;
                this.btnOK.Visible = false;
                this.btnCancel.Text = "确定";
            } else if (!this.hasNextPage) {
                this.label2.Enabled = false;
                this.cmbNext.Enabled = false;
            }
        }

        private void InitialMainAndNextCol() {
            if (this.mainColList != null) {
                for (int i = 0; i < this.mainColList.Count; i++) {
                    this.cmbMain.Items.Add(this.mainColList[i]);
                }
            }
            if (this.nextColList != null) {
                for (int j = 0; j < this.nextColList.Count; j++) {
                    this.cmbNext.Items.Add(this.nextColList[j]);
                }
            }
            if ((this.dataHiddenCol != null) && (this.dataHiddenCol != string.Empty)) {
                string[] strArray = this.dataHiddenCol.Split(new char[] { '‖' });
                if ((strArray != null) && (strArray.Length > 0)) {
                    this.cmbMain.Text = strArray[0].ToString();
                    if (this.hasNextPage) {
                        this.cmbNext.Text = strArray[1].ToString();
                    }
                }
            }
        }

        private bool ValidateHiddenCol(string hiddenCol, ArrayList midColList, string pageLabel) {
            string[] strArray = hiddenCol.Split(new char[] { ',', '，' });
            ArrayList list = new ArrayList();
            foreach (string str in strArray) {
                list.Add(str.ToUpper());
            }
            list.Sort();
            for (int i = 0; i < list.Count; i++) {
                if ((list[i].ToString() != string.Empty) && !midColList.Contains(list[i].ToString())) {
                    MessageBox.Show("您设置的" + pageLabel + "表中列" + list[i].ToString() + "，不是有效的列名称，请检查更正。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (((i > 0) && (list[i].ToString() == list[i - 1].ToString())) && (list[i].ToString() != "")) {
                    MessageBox.Show(pageLabel + "表中列" + list[i].ToString() + "重复设置，请检查更正。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private bool ValidateHiddenCol(string mainHiddenCol, string nextHiddenCol, bool hasNextPage) {
            if (!this.ValidateHiddenCol(mainHiddenCol, this.mainColList, "首页")) {
                return false;
            }
            if (hasNextPage && !this.ValidateHiddenCol(nextHiddenCol, this.nextColList, "续页")) {
                return false;
            }
            return true;
        }
    }
}

