using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.PLL.Environment;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Resource.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgPicture : Form {
        private PictureInfo info = new PictureInfo();
        public InterPicType PicType;
        public string Script;

        public DlgPicture(string xml, bool readOnly) {
            this.InitializeComponent();
            this.cmbPicType.Items.Add(PPCConvert.PicType2String(InterPicType.SourceFile));
            this.cmbPicType.Items.Add(PPCConvert.PicType2String(InterPicType.ResourcePic));
            this.chkGrayscale.Checked = PLSystemParam.GrayResPicWhenPrint;
            if (PPCardCompiler.ExplainInterPicXml(xml, this.info)) {
                this.cmbPicType.Enabled = false;
                this.cmbPicType.Text = PPCConvert.PicType2String(this.info.Type);
                this.cmbPicType.Enabled = true;
                if (this.info.FileIndex >= 0) {
                    this.numFileIndex.Value = Convert.ToDecimal(this.info.FileIndex);
                } else {
                    this.numFileIndex.Enabled = false;
                }
                this.txtCellStart.Text = this.info.CellStart;
                this.txtCellEnd.Text = this.info.CellEnd;
                this.chkGrayscale.Checked = this.info.Grayscale;
            } else {
                this.cmbPicType.SelectedIndex = 0;
            }
            if (readOnly) {
                this.panel1.Enabled = false;
                this.btnOk.Visible = false;
                this.btnCancel.Text = "确定";
            }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            this.info.Type = PPCConvert.ToPicType(this.cmbPicType.Text);
            if (this.info.Type == InterPicType.SourceFile) {
                this.info.FileIndex = Convert.ToInt32(this.numFileIndex.Value);
            } else {
                this.info.FileIndex = -1;
            }
            this.info.CellStart = this.txtCellStart.Text.Trim();
            this.info.CellEnd = this.txtCellEnd.Text.Trim();
            this.info.Grayscale = this.chkGrayscale.Checked;
            this.Script = PPCardCompiler.CreateInterPicXml(this.info);
            this.PicType = this.info.Type;
            base.DialogResult = DialogResult.OK;
        }

        private void cmbPicType_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.cmbPicType.Enabled) {
                if (this.cmbPicType.Text == PPCConvert.PicType2String(InterPicType.ResourcePic)) {
                    this.numFileIndex.Enabled = false;
                    FrmRESPicture picture = new FrmRESPicture {
                        WindowState = FormWindowState.Normal
                    };
                    if (picture.ShowDialog() == DialogResult.OK) {
                        this.info.FileOid = picture.SelectedPicture.Oid;
                    } else {
                        this.cmbPicType.Text = PPCConvert.PicType2String(InterPicType.SourceFile);
                    }
                } else {
                    this.numFileIndex.Enabled = true;
                }
            }
        }

        private void DlgPicture_Load(object sender, EventArgs e) {
        }

        public string CellStart {
            set {
                this.txtCellEnd.Text = value;
                this.txtCellStart.Text = value;
            }
        }
    }
}

