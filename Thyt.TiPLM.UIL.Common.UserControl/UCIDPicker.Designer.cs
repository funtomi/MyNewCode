namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCIDPicker {
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
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btn_clear = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.grpEdit = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.txtEditID = new Thyt.TiPLM.UIL.Controls.MemoEditPLM();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpEdit.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xb0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x110, 0x20);
            this.panel1.TabIndex = 0;
            this.btn_clear.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btn_clear.Location = new System.Drawing.Point(0x8a, 4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_clear.TabIndex = 1;
            this.btn_clear.Text = "清空";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btn_ok.Location = new System.Drawing.Point(0xd0, 4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.grpEdit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x110, 0xb0);
            this.panel2.TabIndex = 1;
            this.grpEdit.Controls.Add(this.txtEditID);
            this.grpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEdit.Location = new System.Drawing.Point(0, 0);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(270, 0xae);
            this.grpEdit.TabIndex = 0;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "编辑区";
            this.txtEditID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditID.Location = new System.Drawing.Point(3, 0x11);
            this.txtEditID.Multiline = true;
            this.txtEditID.Name = "txtEditID";
            this.txtEditID.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEditID.Size = new System.Drawing.Size(0x108, 0x9a);
            this.txtEditID.TabIndex = 0;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCIDPicker";
            base.Size = new System.Drawing.Size(0x110, 0xd0);
            base.Enter += new System.EventHandler(this.UCIDPicker_Enter);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            base.ResumeLayout(false);
        }
         
        #endregion   
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_clear;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM grpEdit;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.MemoEditPLM txtEditID;
    }
}