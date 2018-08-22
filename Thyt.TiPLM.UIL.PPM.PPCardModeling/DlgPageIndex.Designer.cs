namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgPageIndex {
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
            this.btnShow = new System.Windows.Forms.Button();
            this.labelShow = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPostfix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            base.SuspendLayout();
            this.btnShow.AutoSize = true;
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnShow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnShow.Location = new System.Drawing.Point(20, 0x2f);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(0x68, 0x17);
            this.btnShow.TabIndex = 0x1b;
            this.btnShow.Text = "查看效果";
            this.btnShow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            this.labelShow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelShow.Location = new System.Drawing.Point(0x8a, 0x2e);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(110, 0x17);
            this.labelShow.TabIndex = 0x1a;
            this.labelShow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(0x88, 0x10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 0x10);
            this.label2.TabIndex = 0x19;
            this.label2.Text = "后缀:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPostfix.Location = new System.Drawing.Point(0xc0, 0x10);
            this.txtPostfix.Name = "txtPostfix";
            this.txtPostfix.Size = new System.Drawing.Size(0x38, 0x15);
            this.txtPostfix.TabIndex = 0x18;
            this.txtPostfix.Text = "页";
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(0x10, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x30, 0x10);
            this.label1.TabIndex = 0x17;
            this.label1.Text = "前缀:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.txtPrefix.Location = new System.Drawing.Point(0x40, 0x10);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(0x38, 0x15);
            this.txtPrefix.TabIndex = 0x16;
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancle.Location = new System.Drawing.Point(140, 100);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(0x4b, 0x18);
            this.btnCancle.TabIndex = 0x1d;
            this.btnCancle.Text = "取消";
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(0x31, 100);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x4b, 0x18);
            this.btnOK.TabIndex = 0x1c;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 0x4e);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(0x60, 0x10);
            this.checkBox1.TabIndex = 30;
            this.checkBox1.Text = "是否实际页码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.btnCancle;
            base.ClientSize = new System.Drawing.Size(0x110, 0x88);
            base.Controls.Add(this.checkBox1);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnShow);
            base.Controls.Add(this.labelShow);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtPostfix);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtPrefix);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgPageIndex";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.TextBox txtPostfix;
        private System.Windows.Forms.TextBox txtPrefix;
    }
}