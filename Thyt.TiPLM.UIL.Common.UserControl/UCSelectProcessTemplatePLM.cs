using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.BPM;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCSelectProcessTemplatePLM : PopupContainerEditPLM {
        private UCProcessTemplatePicker ucProcess;

        private event SelectProcessTemplateHandler dlHandler;

        public event SelectProcessTemplateHandler DropListSelected;

        public UCSelectProcessTemplatePLM() {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        private void InitializeConfig() {
            this.ucProcess = new UCProcessTemplatePicker();
            this.popupContainer.Controls.Add(this.ucProcess);
            base.Properties.PopupControl.Size = new Size(base.Width, this.ucProcess.Height);
            this.ucProcess.Dock = DockStyle.Fill;
            this.dlHandler = new SelectProcessTemplateHandler(this.ucProcess_ProcessSelected);
            this.ucProcess.processTemplateSelected += this.dlHandler;
            base.KeyDown += new KeyEventHandler(this.this_KeyDown);
            this.AllowDrop = true;
        }

        private void PopupContainerEdit_QueryPopUp(object sender, CancelEventArgs e) {
            base.Properties.PopupControl.Width = base.Width;
        }

        private void this_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                this.Text = "";
                base.Tag = null;
                if (this.DropListSelected != null) {
                    this.DropListSelected(null);
                }
            }
        }

        private void ucProcess_ProcessSelected(DELProcessDefProperty proDef) {
            if (proDef != null) {
                this.Text = proDef.Name;
                base.Tag = proDef;
                if (this.DropListSelected != null) {
                    this.DropListSelected(proDef);
                    this.ClosePopup();
                }
            }
        }

        public string NullText {
            get {
                return base.Properties.NullText;
            }
            set {
                base.Properties.NullText = value;
            }
        }

        public bool ReadOnly {
            get {
                if (base.Properties.TextEditStyle == TextEditStyles.Standard) {
                    return false;
                }
                return true;
            }
            set {
                if (value) {
                    base.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                } else {
                    base.Properties.TextEditStyle = TextEditStyles.Standard;
                }
            }
        }
    }
}

