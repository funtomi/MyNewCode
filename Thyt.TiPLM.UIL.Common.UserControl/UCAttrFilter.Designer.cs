namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCAttrFilter {
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
        ///          

        private void InitializeComponent() {
            this.cb_name = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label5 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cb_operator = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label6 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel_value = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.chkCaseSensitive = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.chkParameter = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.ckbLoginoid = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.cb_name.Properties.BeginInit();
            this.cb_operator.Properties.BeginInit();
            this.panel_value.BeginInit();
            this.chkCaseSensitive.Properties.BeginInit();
            this.chkParameter.Properties.BeginInit();
            this.ckbLoginoid.Properties.BeginInit();
            base.SuspendLayout();
            this.cb_name.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.cb_name.Location = new System.Drawing.Point(0x30, 0);
            this.cb_name.Name = "cb_name";
            this.cb_name.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cb_name.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_name.Size = new System.Drawing.Size(280, 0x16);
            this.cb_name.TabIndex = 11;
            this.cb_name.SelectedIndexChanged += new System.EventHandler(this.cb_name_SelectedIndexChanged);
            this.label4.Location = new System.Drawing.Point(0, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "属性名:";
            this.label5.Location = new System.Drawing.Point(0, 0x1a);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 14);
            this.label5.TabIndex = 8;
            this.label5.Text = "运算符:";
            this.cb_operator.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.cb_operator.Location = new System.Drawing.Point(0x30, 0x18);
            this.cb_operator.Name = "cb_operator";
            this.cb_operator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cb_operator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cb_operator.Size = new System.Drawing.Size(280, 0x16);
            this.cb_operator.TabIndex = 12;
            this.cb_operator.SelectedIndexChanged += new System.EventHandler(this.cb_operator_SelectedIndexChanged);
            this.label6.Location = new System.Drawing.Point(0, 0x34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "属性值:";
            this.panel_value.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.panel_value.Location = new System.Drawing.Point(0x30, 0x30);
            this.panel_value.Name = "panel_value";
            this.panel_value.Size = new System.Drawing.Size(280, 0x18);
            this.panel_value.TabIndex = 13;
            this.chkCaseSensitive.EditValue = true;
            this.chkCaseSensitive.Location = new System.Drawing.Point(0x30, 0x4e);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Properties.Caption = "比较时不区分大小写";
            this.chkCaseSensitive.Size = new System.Drawing.Size(0x90, 0x13);
            this.chkCaseSensitive.TabIndex = 14;
            this.chkCaseSensitive.CheckedChanged += new System.EventHandler(this.chkCaseSensitive_CheckedChanged);
            this.chkParameter.Location = new System.Drawing.Point(0xda, 0x4e);
            this.chkParameter.Name = "chkParameter";
            this.chkParameter.Properties.Caption = "作为参数";
            this.chkParameter.Size = new System.Drawing.Size(0x58, 0x13);
            this.chkParameter.TabIndex = 15;
            this.chkParameter.Visible = false;
            this.ckbLoginoid.Location = new System.Drawing.Point(0x30, 0x4e);
            this.ckbLoginoid.Name = "ckbLoginoid";
            this.ckbLoginoid.Properties.Caption = "登录用户";
            this.ckbLoginoid.Size = new System.Drawing.Size(0x4a, 0x13);
            this.ckbLoginoid.TabIndex = 0x12;
            this.ckbLoginoid.Visible = false;
            base.Controls.Add(this.cb_operator);
            base.Controls.Add(this.ckbLoginoid);
            base.Controls.Add(this.chkParameter);
            base.Controls.Add(this.chkCaseSensitive);
            base.Controls.Add(this.panel_value);
            base.Controls.Add(this.cb_name);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label6);
            base.Name = "UCAttrFilter";
            base.Size = new System.Drawing.Size(330, 100);
            this.cb_name.Properties.EndInit();
            this.cb_operator.Properties.EndInit();
            this.panel_value.EndInit();
            this.chkCaseSensitive.Properties.EndInit();
            this.chkParameter.Properties.EndInit();
            this.ckbLoginoid.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion 
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label5;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label6;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel_value;
        private UCSelectClassPLM tvClass;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cb_name;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cb_operator;
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkCaseSensitive;
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkParameter;
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM ckbLoginoid;

    }
}