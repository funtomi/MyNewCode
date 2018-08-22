using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Utility;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgBarcode : Form {
        
        private PictureInfo info = new PictureInfo();
        
        public InterPicType PicType;
        public string Script;
        

        public DlgBarcode(string xml, string className, bool readOnly) {
            this.InitializeComponent();
            ArrayList attris = new ArrayList(ModelContext.MetaModel.GetAllAttributes(className));
            this.FilterAttributes(attris);
            if ((attris != null) && (attris.Count > 0)) {
                this.cmbAttrList.DataSource = attris;
                this.cmbAttrList.ValueMember = "Name";
                this.cmbAttrList.DisplayMember = "Label";
            }
            List<string> configNames = new PLBarCode().GetConfigNames();
            for (int i = 0; i < configNames.Count; i++) {
                this.cmbTemplate.Items.Add(configNames[i]);
            }
            if (PPCardCompiler.ExplainInterPicXml(xml, this.info)) {
                this.cmbAttrList.SelectedValue = this.info.AttrName;
                if (this.cmbTemplate.Items.Contains(this.info.TemplateName)) {
                    this.cmbTemplate.Text = this.info.TemplateName;
                }
                this.txtCellStart.Text = this.info.CellStart;
                this.txtCellEnd.Text = this.info.CellEnd;
            } else {
                this.cmbAttrList.SelectedIndex = 0;
                if (this.cmbTemplate.Items.Count > 0) {
                    this.cmbTemplate.SelectedIndex = 0;
                } else {
                    MessageBox.Show("还未定义条码模板，请先定义条码模板。", "工艺", MessageBoxButtons.OK);
                }
            }
            if (readOnly) {
                this.panel1.Enabled = false;
                this.btnOk.Visible = false;
                this.btnCancel.Text = "确定";
            }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            if (this.cmbTemplate.Text == "") {
                MessageBox.Show("条码模板为空，设置无效。", "工艺", MessageBoxButtons.OK);
            } else if (this.txtCellStart.Text.Trim() == "") {
                MessageBox.Show("起始单元格为空，设置无效。", "工艺", MessageBoxButtons.OK);
            } else if (this.txtCellEnd.Text.Trim() == "") {
                MessageBox.Show("结束单元格为空，设置无效。", "工艺", MessageBoxButtons.OK);
            } else {
                this.info.Type = InterPicType.BarCode;
                this.info.AttrName = this.cmbAttrList.SelectedValue.ToString();
                this.info.TemplateName = this.cmbTemplate.Text;
                this.info.CellStart = this.txtCellStart.Text.Trim();
                this.info.CellEnd = this.txtCellEnd.Text.Trim();
                this.Script = PPCardCompiler.CreateInterPicXml(this.info);
                this.PicType = this.info.Type;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void FilterAttributes(ArrayList attris) {
            for (int i = 0; i < attris.Count; i++) {
                GenericAttribute attribute = (GenericAttribute)attris[i];
                if ((attribute.Category == "I") || (attribute.Category == "S")) {
                    DEMetaAttribute attach = attribute.Attach as DEMetaAttribute;
                    if (!attach.IsViewable) {
                        attris.Remove(attribute);
                        i--;
                    }
                }
            }
        }

        public string CellStart {
            set {
                this.txtCellEnd.Text = value;
                this.txtCellStart.Text = value;
            }
        }
    }
}

