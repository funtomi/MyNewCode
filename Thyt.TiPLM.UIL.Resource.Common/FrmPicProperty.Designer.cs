namespace Thyt.TiPLM.UIL.Resource.Common {
    partial class FrmPicProperty {
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
            this.gb = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.txtDescrip = new Thyt.TiPLM.UIL.Controls.MemoEditPLM();
            this.txtAlias = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.label5 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.gb.SuspendLayout();
            base.SuspendLayout();
            this.gb.Controls.Add(this.txtDescrip);
            this.gb.Controls.Add(this.txtAlias);
            this.gb.Controls.Add(this.label5);
            this.gb.Controls.Add(this.label1);
            this.gb.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb.Location = new System.Drawing.Point(0, 0);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(290, 0xd8);
            this.gb.TabIndex = 0;
            this.gb.TabStop = false;
            this.txtDescrip.Location = new System.Drawing.Point(0x10, 0x58);
            this.txtDescrip.Multiline = true;
            this.txtDescrip.Name = "txtDescrip";
            this.txtDescrip.Size = new System.Drawing.Size(0x100, 0x70);
            this.txtDescrip.TabIndex = 9;
            this.txtDescrip.Text = "";
            this.txtAlias.Location = new System.Drawing.Point(0x10, 0x20);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(0x100, 0x15);
            this.txtAlias.TabIndex = 5;
            this.txtAlias.Text = "";
            this.label5.Location = new System.Drawing.Point(0x10, 0x40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0x30, 0x10);
            this.label5.TabIndex = 4;
            this.label5.Text = "描述：";
            this.label1.Location = new System.Drawing.Point(0x10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x30, 0x10);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(0x80, 0xde);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xd0, 0xde);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取 消";
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(290, 0xf7);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.gb);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmPicProperty";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图片属性";
            base.Load += new System.EventHandler(this.FrmPicProperty_Load);
            this.gb.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gb;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label5;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtAlias;
        private Thyt.TiPLM.UIL.Controls.MemoEditPLM txtDescrip;
    }
}