using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgFormSignature : Form {

        public string Script;

        public DlgFormSignature(string xml, bool readOnly) {
            this.InitializeComponent();
            try {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                this.Text = this.textBox1.Text = document.DocumentElement.Name.Trim();
                XmlElement element = (XmlElement)document.GetElementsByTagName(document.DocumentElement.Name)[0];
                if (element != null) {
                    this.textBox2.Text = element.InnerText.Trim();
                }
            } catch {
            }
            if (this.Text == "其他") {
                readOnly = true;
            }
            if (readOnly) {
                this.textBox1.ReadOnly = true;
                this.textBox2.ReadOnly = true;
                this.button1.Enabled = false;
                this.button1.Visible = false;
                this.button2.Text = "确定";
            }
            this.textBox2.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (this.textBox1.Text.Trim() == "") {
                MessageBox.Show("表单签字项不能为空。");
                this.textBox1.SelectAll();
            } else if (this.textBox2.Text.Trim() == "") {
                MessageBox.Show("签字项属性不能为空。");
                this.textBox2.SelectAll();
            } else {
                this.Script = "<" + this.textBox1.Text.Trim() + ">" + this.textBox2.Text.Trim() + "</" + this.textBox1.Text.Trim() + ">";
                base.DialogResult = DialogResult.OK;
            }
        }

    }
}

