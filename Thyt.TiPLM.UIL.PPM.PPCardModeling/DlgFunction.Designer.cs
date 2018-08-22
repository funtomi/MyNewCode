namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgFunction {
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
            this.cmbNextPage = new System.Windows.Forms.ComboBox();
            this.cmbMainPage = new System.Windows.Forms.ComboBox();
            this.labNextPage = new System.Windows.Forms.Label();
            this.labMainPage = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOnlyLastPage = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.cmbNextPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextPage.Location = new System.Drawing.Point(0x80, 0x38);
            this.cmbNextPage.Name = "cmbNextPage";
            this.cmbNextPage.Size = new System.Drawing.Size(0x79, 20);
            this.cmbNextPage.TabIndex = 6;
            this.cmbMainPage.AllowDrop = true;
            this.cmbMainPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMainPage.Location = new System.Drawing.Point(0x80, 0x18);
            this.cmbMainPage.Name = "cmbMainPage";
            this.cmbMainPage.Size = new System.Drawing.Size(0x79, 20);
            this.cmbMainPage.TabIndex = 5;
            this.labNextPage.AutoSize = true;
            this.labNextPage.Location = new System.Drawing.Point(20, 0x38);
            this.labNextPage.Name = "labNextPage";
            this.labNextPage.Size = new System.Drawing.Size(0x4d, 12);
            this.labNextPage.TabIndex = 4;
            this.labNextPage.Text = "续页表中列：";
            this.labNextPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labMainPage.AutoSize = true;
            this.labMainPage.Location = new System.Drawing.Point(20, 0x18);
            this.labMainPage.Name = "labMainPage";
            this.labMainPage.Size = new System.Drawing.Size(0x4d, 12);
            this.labMainPage.TabIndex = 3;
            this.labMainPage.Text = "首页表中列：";
            this.labMainPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.Location = new System.Drawing.Point(0x30, 0x88);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x40, 0x17);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xa8, 0x88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x40, 0x17);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取 消";
            this.groupBox1.Controls.Add(this.chkOnlyLastPage);
            this.groupBox1.Controls.Add(this.labMainPage);
            this.groupBox1.Controls.Add(this.cmbMainPage);
            this.groupBox1.Controls.Add(this.labNextPage);
            this.groupBox1.Controls.Add(this.cmbNextPage);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x110, 120);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择列";
            this.chkOnlyLastPage.AutoSize = true;
            this.chkOnlyLastPage.Checked = true;
            this.chkOnlyLastPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOnlyLastPage.Location = new System.Drawing.Point(20, 0x58);
            this.chkOnlyLastPage.Name = "chkOnlyLastPage";
            this.chkOnlyLastPage.Size = new System.Drawing.Size(0x90, 0x10);
            this.chkOnlyLastPage.TabIndex = 7;
            this.chkOnlyLastPage.Text = "只在卡片最后一页显示";
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x124, 0xa8);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgFunction";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "列总计";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkOnlyLastPage;
        private System.Windows.Forms.ComboBox cmbMainPage;
        private System.Windows.Forms.ComboBox cmbNextPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labMainPage;
        private System.Windows.Forms.Label labNextPage;
    }
}