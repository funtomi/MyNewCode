    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.BPM;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCSelectProcessTemplate : UltraTextEditor
    {
        private UCProcessTemplatePicker ucProcess;

        private event SelectProcessTemplateHandler dlHandler;

        public event SelectProcessTemplateHandler DropListSelected;

        public UCSelectProcessTemplate()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }


        private void InitializeConfig()
        {
            this.ucProcess = new UCProcessTemplatePicker();
            DropDownEditorButton button = base.ButtonsRight["SelectProcessTemplate"] as DropDownEditorButton;
            button.Control = this.ucProcess;
            this.dlHandler = new SelectProcessTemplateHandler(this.ucProcess_ProcessSelected);
            this.ucProcess.processTemplateSelected += this.dlHandler;
            base.KeyDown += new KeyEventHandler(this.this_KeyDown);
            this.AllowDrop = true;
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.Text = "";
                base.Tag = null;
                if (this.DropListSelected != null)
                {
                    this.DropListSelected(null);
                }
            }
        }

        private void ucProcess_ProcessSelected(DELProcessDefProperty proDef)
        {
            if (proDef != null)
            {
                this.Text = proDef.Name;
                base.Tag = proDef;
                if (this.DropListSelected != null)
                {
                    this.DropListSelected(proDef);
                    base.CloseEditorButtonDropDowns();
                }
            }
        }
    }
}

