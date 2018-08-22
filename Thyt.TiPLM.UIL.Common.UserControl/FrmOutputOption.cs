    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class FrmOutputOption : Form {

        private int TotalPageNum;

        public FrmOutputOption(int i_totalpagenum) {
            this.InitializeComponent();
            this.TotalPageNum = i_totalpagenum;
            this.numStart.Minimum = 1M;
            this.numStart.Maximum = i_totalpagenum;
            this.numEnd.Minimum = 1M;
            this.numEnd.Maximum = i_totalpagenum;
            this.numEnd.Value = i_totalpagenum;
        }

        public FrmOutputOption(int i_totalpagenum, ArrayList attrList)
            : this(i_totalpagenum) {
            foreach (DEMetaAttribute attribute in attrList) {
                this.lbxHeader.Items.Add(attribute, true);
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (this.numStart.Value > this.numEnd.Value) {
                MessageBoxPLM.Show("输出的开始页数大于结束页数。", "资源导出选项", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            } else {
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < this.lbxHeader.Items.Count; i++) {
                this.lbxHeader.SetItemChecked(i, true);
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e) {
            for (int i = 0; i < this.lbxHeader.Items.Count; i++) {
                this.lbxHeader.SetItemChecked(i, false);
            }
        }


        public int EndPage {
            get {
                return
                    Convert.ToInt32(this.numEnd.Value);
            }
        }
        public bool IsMultiPage {
            get {
                return
                    this.rdBtnMultiPage.Checked;
            }
        }

        public int StartPage {
            get {
                return
                    Convert.ToInt32(this.numStart.Value);
            }
        }
    }
}

