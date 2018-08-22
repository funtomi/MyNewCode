using System;
using System.ComponentModel;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Product2;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgAutoNumber : Form {
        private bool allowUpdate;
        private string AutoNumberXml;

        private bool hasNextPage;
        private bool isTiModeler;

        private CLCardTemplate m_tp;

        private bool readOnly;


        public DlgAutoNumber() {
            this.InitializeComponent();
        }

        public DlgAutoNumber(CLCardTemplate tp, bool isFromTiModeler) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.isTiModeler = isFromTiModeler;
            this.AutoNumberXml = this.m_tp.AutoNumber;
            this.hasNextPage = this.m_tp.HasNextPage;
            this.readOnly = this.m_tp.ReadOnly;
            this.allowUpdate = false;
            if (isFromTiModeler && ((this.m_tp.Item.State == ItemState.CheckIn) || (this.m_tp.Item.State == ItemState.Release))) {
                this.allowUpdate = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            try {
                if (this.allowUpdate) {
                    if ((this.btnOK.Text == "更新") || (this.btnOK.Text == "设置")) {
                        this.rbtAllPages.Enabled = true;
                        this.groupBox1.Enabled = true;
                        this.groupBox2.Enabled = true;
                        if (!this.hasNextPage) {
                            this.cmbNextPageCol.Enabled = false;
                        }
                        this.btnOK.Text = "确定";
                    } else if (this.btnOK.Text == "确定") {
                        string attrValue = PPCardCompiler.CreateXML(this.NumberFormat);
                        this.m_tp.Item.Iteration.SetAttrValue("AUTONUMBER", attrValue);
                        PLItem.UpdateItemIterationDirectly(this.m_tp.Item, this.m_tp.UserOid, false);
                        base.DialogResult = DialogResult.OK;
                    }
                } else {
                    AutoNumber numberFormat = this.NumberFormat;
                    base.DialogResult = DialogResult.OK;
                }
            } catch {
                MessageBox.Show("自动编号设置出错。", ConstCommon.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmbMainPageCol_TextChanged(object sender, EventArgs e) {
            this.cmbNextPageCol.Text = this.cmbMainPageCol.Text;
        }

        private void DlgAutoNumber_Load(object sender, EventArgs e) {
            if (this.isTiModeler) {
                this.rbtCurrentPage.Enabled = false;
            }
            if (!this.hasNextPage) {
                this.cmbNextPageCol.Enabled = false;
            }
            if (this.readOnly) {
                this.rbtAllPages.Enabled = false;
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                if (this.allowUpdate) {
                    if (this.m_tp.AutoNumber == null) {
                        this.btnOK.Text = "设置";
                    } else {
                        this.btnOK.Text = "更新";
                    }
                } else {
                    this.btnOK.Visible = false;
                    this.btnCancel.Text = "确定";
                }
            }
            if ((this.AutoNumberXml != null) && (this.AutoNumberXml.ToString() != "")) {
                AutoNumber number = PPCardCompiler.ExplainAutoNumberXML(this.AutoNumberXml);
                this.txtPrefix.Text = number.Prefix;
                this.txtPostfix.Text = number.Postfix;
                this.numStart.Value = number.Start;
                this.cmbInterval.Text = number.Interval.ToString();
                this.cmbMainPageCol.Text = PPCConvert.Int2ABC(number.ColAtMainPage);
                this.cmbNextPageCol.Text = PPCConvert.Int2ABC(number.ColAtNextPage);
            }
        }

        private void rbtAllPages_CheckedChanged(object sender, EventArgs e) {
            bool flag = this.rbtAllPages.Checked;
            this.labMainPage.Visible = flag;
            this.cmbMainPageCol.Visible = flag;
            this.labNextPage.Visible = flag;
            this.cmbNextPageCol.Visible = flag;
            this.labCurPage.Visible = !flag;
            this.cmbCurPageCol.Visible = !flag;
        }

        public AutoNumber NumberFormat {
            get {
                AutoNumber number;
                number.Prefix = this.txtPrefix.Text;
                number.Postfix = this.txtPostfix.Text;
                number.IsAllPages = this.rbtAllPages.Checked;
                number.Start = Convert.ToInt32(this.numStart.Value);
                number.Interval = Convert.ToInt32(this.cmbInterval.Text);
                number.ColAtCurPage = PPCConvert.ABC2Int(this.cmbCurPageCol.Text);
                number.ColAtMainPage = PPCConvert.ABC2Int(this.cmbMainPageCol.Text);
                number.ColAtNextPage = PPCConvert.ABC2Int(this.cmbNextPageCol.Text);
                return number;
            }
        }
    }
}

