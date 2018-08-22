using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgPageIndex : Form {
        private bool bIsRealPage;

        public string Script;

        public DlgPageIndex(string script, bool isPageIndex, bool readOnly) {
            this.InitializeComponent();
            try {
                XmlDocument document = new XmlDocument();
                document.LoadXml(script);
                XmlElement element = null;
                element = (XmlElement)document.GetElementsByTagName("前缀")[0];
                if (element != null) {
                    this.txtPrefix.Text = element.InnerText;
                }
                element = (XmlElement)document.GetElementsByTagName("后缀")[0];
                if (element != null) {
                    this.txtPostfix.Text = element.InnerText;
                }
            } catch {
                if (isPageIndex) {
                    this.txtPrefix.Text = "第";
                } else {
                    this.txtPrefix.Text = "共";
                }
            }
            if (isPageIndex) {
                this.Text = "页码设置";
            } else {
                this.Text = "总页数设置";
            }
            if (readOnly) {
                this.txtPrefix.ReadOnly = true;
                this.txtPostfix.ReadOnly = true;
                this.btnOK.Visible = false;
                this.btnCancle.Text = "确定";
            }
            if (script.Contains("<是否实际页码>是</是否实际页码>")) {
                this.checkBox1.Checked = true;
            } else if (script.Contains("<是否实际页码>否</是否实际页码>")) {
                this.checkBox1.Checked = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            string str = this.bIsRealPage ? "<是否实际页码>是</是否实际页码>" : "<是否实际页码>否</是否实际页码>";
            if (this.Text == "页码设置") {
                this.Script = "<页码>" + str + "<前缀>" + this.txtPrefix.Text.Trim() + "</前缀><后缀>" + this.txtPostfix.Text.Trim() + "</后缀></页码>";
            } else {
                this.Script = "<总页数>" + str + "<前缀>" + this.txtPrefix.Text.Trim() + "</前缀><后缀>" + this.txtPostfix.Text.Trim() + "</后缀></总页数>";
            }
            base.DialogResult = DialogResult.OK;
        }

        private void btnShow_Click(object sender, EventArgs e) {
            this.labelShow.Text = this.txtPrefix.Text.Trim() + "1" + this.txtPostfix.Text.Trim();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (this.checkBox1.Checked) {
                this.bIsRealPage = true;
            } else {
                this.bIsRealPage = false;
            }
        }

    }
}

