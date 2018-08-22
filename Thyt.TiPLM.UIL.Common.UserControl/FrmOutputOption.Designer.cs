namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmOutputOption {
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
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.rdBtnMultiPage = new System.Windows.Forms.RadioButton();
            this.rdBtnOnePage = new System.Windows.Forms.RadioButton();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.numStart = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.numEnd = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.btnSelectNone = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnSelectAll = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.lbxHeader = new Thyt.TiPLM.UIL.Controls.CheckedListBoxPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel1.SuspendLayout();
            this.numStart.Properties.BeginInit();
            this.numEnd.Properties.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x1b2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x155, 0x24);
            this.panel1.TabIndex = 0;
            this.btnOK.Location = new System.Drawing.Point(0xff, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.rdBtnMultiPage.AutoSize = true;
            this.rdBtnMultiPage.Checked = true;
            this.rdBtnMultiPage.Location = new System.Drawing.Point(0x66, 90);
            this.rdBtnMultiPage.Name = "rdBtnMultiPage";
            this.rdBtnMultiPage.Size = new System.Drawing.Size(0x47, 0x10);
            this.rdBtnMultiPage.TabIndex = 1;
            this.rdBtnMultiPage.TabStop = true;
            this.rdBtnMultiPage.Text = "分页输出";
            this.rdBtnMultiPage.UseVisualStyleBackColor = true;
            this.rdBtnOnePage.AutoSize = true;
            this.rdBtnOnePage.Location = new System.Drawing.Point(190, 90);
            this.rdBtnOnePage.Name = "rdBtnOnePage";
            this.rdBtnOnePage.Size = new System.Drawing.Size(0x6b, 0x10);
            this.rdBtnOnePage.TabIndex = 2;
            this.rdBtnOnePage.Text = "整合到一页输出";
            this.rdBtnOnePage.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x13, 0x20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x35, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "开始页数";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0xc4, 0x20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "结束页数";
            this.numStart.Location = new System.Drawing.Point(0x4e, 0x1c);
            this.numStart.Name = "numStart";
            this.numStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numStart.Size = new System.Drawing.Size(0x49, 0x15);
            this.numStart.TabIndex = 5;
            this.numEnd.Location = new System.Drawing.Point(0xff, 0x1c);
            this.numEnd.Name = "numEnd";
            this.numEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numEnd.Size = new System.Drawing.Size(0x43, 0x15);
            this.numEnd.TabIndex = 6;
            this.groupBox1.Controls.Add(this.btnSelectNone);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.lbxHeader);
            this.groupBox1.Controls.Add(this.rdBtnMultiPage);
            this.groupBox1.Controls.Add(this.numEnd);
            this.groupBox1.Controls.Add(this.rdBtnOnePage);
            this.groupBox1.Controls.Add(this.numStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x155, 0x1b2);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出选项";
            this.btnSelectNone.Location = new System.Drawing.Point(0x66, 0x192);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnSelectNone.TabIndex = 8;
            this.btnSelectNone.Text = "全不选";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Visible = false;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            this.btnSelectAll.Location = new System.Drawing.Point(0x15, 0x192);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Visible = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            this.lbxHeader.Enabled = false;
            this.lbxHeader.FormattingEnabled = true;
            this.lbxHeader.Location = new System.Drawing.Point(0x15, 0x88);
            this.lbxHeader.Name = "lbxHeader";
            this.lbxHeader.Size = new System.Drawing.Size(0x12d, 260);
            this.lbxHeader.TabIndex = 7;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x13, 0x75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x47, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "指定输出列:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0x13, 0x5c);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0x3b, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "输出方式;";
            base.AutoScaleMode =  System.Windows.Forms.AutoScaleMode.Inherit;
            base.ClientSize = new System.Drawing.Size(0x155, 470);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmOutputOption";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资源导出选项";
            this.panel1.ResumeLayout(false);
            this.numStart.Properties.EndInit();
            this.numEnd.Properties.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }      
         
        #endregion  
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnSelectAll;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnSelectNone;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.CheckedListBoxPLM lbxHeader;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numEnd;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numStart;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private System.Windows.Forms.RadioButton rdBtnMultiPage;
        private System.Windows.Forms.RadioButton rdBtnOnePage;
    }
}