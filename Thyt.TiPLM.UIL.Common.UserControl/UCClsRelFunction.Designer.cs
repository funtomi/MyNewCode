namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCClsRelFunction {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            if (disposing && (this.components != null)) {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>  

        private void InitializeComponent() {
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cb_funcname = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label6 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel_value = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.cb_funcname.Properties.BeginInit();
            this.panel_value.BeginInit();
            base.SuspendLayout();
            this.label4.Location = new System.Drawing.Point(0, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "函数名:";
            this.cb_funcname.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.cb_funcname.Location = new System.Drawing.Point(0x30, 8);
            this.cb_funcname.Name = "cb_funcname";
            this.cb_funcname.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cb_funcname.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_funcname.Size = new System.Drawing.Size(280, 0x16);
            this.cb_funcname.TabIndex = 12;
            this.cb_funcname.SelectedIndexChanged += new System.EventHandler(this.cb_funcname_SelectedIndexChanged);
            this.label6.Location = new System.Drawing.Point(0, 0x34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "函数值:";
            this.panel_value.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.panel_value.Location = new System.Drawing.Point(0x30, 0x30);
            this.panel_value.Name = "panel_value";
            this.panel_value.Size = new System.Drawing.Size(280, 0x18);
            this.panel_value.TabIndex = 13;
            base.Controls.Add(this.panel_value);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.cb_funcname);
            base.Controls.Add(this.label6);
            base.Name = "UCClsRelFunction";
            base.Size = new System.Drawing.Size(330, 100);
            this.cb_funcname.Properties.EndInit();
            this.panel_value.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion 
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cb_funcname;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label6;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel_value;
    }
}