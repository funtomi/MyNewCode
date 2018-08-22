namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgFormSignature {
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            base.SuspendLayout();
            this.textBox1.Location = new System.Drawing.Point(0x68, 0x10);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(0x88, 0x15);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "表单签字";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.Location = new System.Drawing.Point(0x68, 0x30);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(0x88, 0x15);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.label1.Location = new System.Drawing.Point(0x18, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x48, 0x17);
            this.label1.TabIndex = 4;
            this.label1.Text = "条目：";
            this.label2.Location = new System.Drawing.Point(0x18, 0x30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x48, 0x17);
            this.label2.TabIndex = 5;
            this.label2.Text = "条目操作：";
            this.button1.Location = new System.Drawing.Point(0x30, 0xb0);
            this.button1.Name = "button1";
            this.button1.TabIndex = 2;
            this.button1.Text = "确认";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(160, 0xb0);
            this.button2.Name = "button2";
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(8, 80);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(0x100, 0x58);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "1.定制签电子签名时，条目操作填写签字别名；\r\n2.定制签字时间时，条目操作填写“XX签字时间”，其中XX为签字别名；\r\n3.定制签字意见（批注）时，条目操作填写签字别名;\r\n4.此外，还可以使用菜单定制“当时时间”和“当时用户”。";
            base.AcceptButton = this.button1;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.button2;
            base.ClientSize = new System.Drawing.Size(0x112, 0xd0);
            base.Controls.Add(this.textBox3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.textBox1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgFormSignature";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "表单签字";
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}