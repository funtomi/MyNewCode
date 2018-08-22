namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgDataHiddenCol {
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbNext = new System.Windows.Forms.ComboBox();
            this.cmbMain = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlBottom.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(0x91, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x4b, 0x18);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult =  System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xe4, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x18);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 0xc9);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(0x134, 0x26);
            this.pnlBottom.TabIndex = 3;
            this.label3.Location = new System.Drawing.Point(0x18, 0x71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0xfe, 0x3f);
            this.label3.TabIndex = 4;
            this.label3.Text = "说明：表中列可选可填。如果需要设置多个数据隐藏列，请在选项框中手工键入逗号间隔的列标（如：A,B）";
            this.cmbNext.FormattingEnabled = true;
            this.cmbNext.Location = new System.Drawing.Point(0x74, 0x43);
            this.cmbNext.Name = "cmbNext";
            this.cmbNext.Size = new System.Drawing.Size(0xa2, 20);
            this.cmbNext.TabIndex = 3;
            this.cmbMain.FormattingEnabled = true;
            this.cmbMain.Location = new System.Drawing.Point(0x74, 0x20);
            this.cmbMain.Name = "cmbMain";
            this.cmbMain.Size = new System.Drawing.Size(0xa2, 20);
            this.cmbMain.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x18, 0x43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x4d, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "续页表中列：";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x18, 0x23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x4d, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "首页表中列：";
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbNext);
            this.groupBox1.Controls.Add(this.cmbMain);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock =  System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x134, 0xc9);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x134, 0xef);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.pnlBottom);
            base.FormBorderStyle =  System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgDataHiddenCol";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置数据隐藏列";
            base.Load += new System.EventHandler(this.DlgDataHiddenCol_Load);
            this.pnlBottom.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbMain;
        private System.Windows.Forms.ComboBox cmbNext;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlBottom;

    }
}