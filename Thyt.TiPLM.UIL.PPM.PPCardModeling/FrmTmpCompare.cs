using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class FrmTmpCompare : Form {

        public FrmTmpCompare(DEBusinessItem item1, DEBusinessItem item2, DEUser user) {
            this.InitializeComponent();
            this.Text = "比较浏览";
            this.InitializeControls(item1, item2, user);
        }
       
        private void FrmTmpCompare_Load(object sender, EventArgs e) {
            this.pnlLeft.Width = base.Width / 2;
            this.panel1.Height = (base.Height * 9) / 10;
            this.panel2.Height = (base.Height * 9) / 10;
        }

        private void FrmTmpCompare_SizeChanged(object sender, EventArgs e) {
            this.pnlLeft.Width = base.Width / 2;
            this.panel1.Height = (base.Height * 9) / 10;
            this.panel2.Height = (base.Height * 9) / 10;
        }

        private void InitializeControls(DEBusinessItem item1, DEBusinessItem item2, DEUser user) {
            PPTmpControl control = null;
            PPTmpControl control2 = null;
            if (item1 != null) {
                control = new PPTmpControl(item1, user, true) {
                    Dock = DockStyle.Fill
                };
                this.panel1.Controls.Clear();
                this.panel1.Controls.Add(control);
            }
            if (item2 != null) {
                control2 = new PPTmpControl(item2, user, true) {
                    Dock = DockStyle.Fill
                };
                this.panel2.Controls.Clear();
                this.panel2.Controls.Add(control2);
            }
            if ((control != null) && (control2 != null)) {
                control.CompareControl = control2;
                control2.CompareControl = control;
                control.TemplateCompare();
            }
            this.lab_Card1.Text = string.Concat(new object[] { "模板代号：", item1.Id, " 版本号：", item1.RevNum, ".", item1.IterNum });
            this.lab_Card2.Text = string.Concat(new object[] { "模板代号：", item2.Id, " 版本号：", item2.RevNum, ".", item2.IterNum });
        }
    }
}

