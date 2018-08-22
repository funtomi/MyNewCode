namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmNewID {
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
            this.txtId = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnecms = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.lblId = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.pnlSecurity = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cobSecurityLevel = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.cobGroup = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.txtRevLabel = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.btnnext = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cobOrgs = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.pnlButtons = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.labCodeInfo = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.labEffway = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cobFolderEffWay = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.pnlOrganizations = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.txtId.Properties.BeginInit();
            this.pnlSecurity.BeginInit();
            this.pnlSecurity.SuspendLayout();
            this.cobSecurityLevel.Properties.BeginInit();
            this.cobGroup.Properties.BeginInit();
            this.txtRevLabel.Properties.BeginInit();
            this.cobOrgs.Properties.BeginInit();
            this.pnlButtons.BeginInit();
            this.pnlButtons.SuspendLayout();
            this.cobFolderEffWay.Properties.BeginInit();
            this.pnlOrganizations.BeginInit();
            this.pnlOrganizations.SuspendLayout();
            base.SuspendLayout();
            this.txtId.Location = new System.Drawing.Point(0x58, 0x10);
            this.txtId.Name = "txtId";
            this.txtId.Properties.MaxLength = 0x40;
            this.txtId.Size = new System.Drawing.Size(0xb0, 0x16);
            this.txtId.TabIndex = 0;
            this.btnOK.Location = new System.Drawing.Point(0xb0, 8);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x38, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x10b, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x38, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnecms.Location = new System.Drawing.Point(0x10c, 0x10);
            this.btnecms.Name = "btnecms";
            this.btnecms.Size = new System.Drawing.Size(0x18, 20);
            this.btnecms.TabIndex = 4;
            this.btnecms.Text = "…";
            this.btnecms.Click += new System.EventHandler(this.btnecms_Click);
            this.lblId.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblId.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.lblId.Location = new System.Drawing.Point(0x10, 0x10);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0x2c, 14);
            this.lblId.TabIndex = 5;
            this.lblId.Text = "代    号:";
            this.pnlSecurity.Controls.Add(this.label1);
            this.pnlSecurity.Controls.Add(this.cobSecurityLevel);
            this.pnlSecurity.Controls.Add(this.cobGroup);
            this.pnlSecurity.Controls.Add(this.label2);
            this.pnlSecurity.Controls.Add(this.label4);
            this.pnlSecurity.Controls.Add(this.txtRevLabel);
            this.pnlSecurity.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSecurity.Location = new System.Drawing.Point(0, -49);
            this.pnlSecurity.Name = "pnlSecurity";
            this.pnlSecurity.Size = new System.Drawing.Size(0x146, 0x60);
            this.pnlSecurity.TabIndex = 8;
            this.pnlSecurity.Visible = false;
            this.label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label1.Location = new System.Drawing.Point(0x10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x34, 14);
            this.label1.TabIndex = 12;
            this.label1.Text = "安全级别:";
            this.cobSecurityLevel.Location = new System.Drawing.Point(0x58, 40);
            this.cobSecurityLevel.Name = "cobSecurityLevel";
            this.cobSecurityLevel.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cobSecurityLevel.Size = new System.Drawing.Size(0x58, 0x16);
            this.cobSecurityLevel.TabIndex = 10;
            this.cobGroup.Location = new System.Drawing.Point(0x58, 8);
            this.cobGroup.Name = "cobGroup";
            this.cobGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cobGroup.Size = new System.Drawing.Size(0xb0, 0x16);
            this.cobGroup.TabIndex = 11;
            this.label2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label2.Location = new System.Drawing.Point(0x10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x30, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "项 目 组:";
            this.label4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label4.Location = new System.Drawing.Point(0x10, 0x48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0x34, 14);
            this.label4.TabIndex = 5;
            this.label4.Text = "版本名称:";
            this.txtRevLabel.EditValue = "1";
            this.txtRevLabel.Location = new System.Drawing.Point(0x58, 0x48);
            this.txtRevLabel.Name = "txtRevLabel";
            this.txtRevLabel.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRevLabel.Properties.MaxLength = 50;
            this.txtRevLabel.Size = new System.Drawing.Size(0x80, 0x16);
            this.txtRevLabel.TabIndex = 0;
            this.btnnext.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnnext.Location = new System.Drawing.Point(0x126, 0x10);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(0x1d, 20);
            this.btnnext.TabIndex = 13;
            this.btnnext.TabStop = false;
            this.btnnext.Text = "…";
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            this.label3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label3.Location = new System.Drawing.Point(0x10, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x34, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "所属部门:";
            this.cobOrgs.Location = new System.Drawing.Point(0x58, 8);
            this.cobOrgs.Name = "cobOrgs";
            this.cobOrgs.Size = new System.Drawing.Size(0xb0, 0x16);
            this.cobOrgs.TabIndex = 13;
            this.pnlButtons.Controls.Add(this.labCodeInfo);
            this.pnlButtons.Controls.Add(this.labEffway);
            this.pnlButtons.Controls.Add(this.btnOK);
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.cobFolderEffWay);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0x2f);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(0x146, 40);
            this.pnlButtons.TabIndex = 9;
            this.labCodeInfo.Location = new System.Drawing.Point(0x10, 11);
            this.labCodeInfo.Name = "labCodeInfo";
            this.labCodeInfo.Size = new System.Drawing.Size(120, 14);
            this.labCodeInfo.TabIndex = 8;
            this.labCodeInfo.Text = "请先点击生成编码按钮";
            this.labCodeInfo.Visible = false;
            this.labEffway.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labEffway.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labEffway.Location = new System.Drawing.Point(0x10, 8);
            this.labEffway.Name = "labEffway";
            this.labEffway.Size = new System.Drawing.Size(0x2c, 14);
            this.labEffway.TabIndex = 6;
            this.labEffway.Text = "有 效 性";
            this.labEffway.Visible = false;
            this.cobFolderEffWay.Location = new System.Drawing.Point(0x58, 8);
            this.cobFolderEffWay.Name = "cobFolderEffWay";
            this.cobFolderEffWay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cobFolderEffWay.Properties.Items.AddRange(new object[] { "最新版本", "精确迭代" });
            this.cobFolderEffWay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cobFolderEffWay.Size = new System.Drawing.Size(0x48, 0x16);
            this.cobFolderEffWay.TabIndex = 4;
            this.cobFolderEffWay.Visible = false;
            this.pnlOrganizations.Controls.Add(this.label3);
            this.pnlOrganizations.Controls.Add(this.cobOrgs);
            this.pnlOrganizations.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOrganizations.Location = new System.Drawing.Point(0, -84);
            this.pnlOrganizations.Name = "pnlOrganizations";
            this.pnlOrganizations.Size = new System.Drawing.Size(0x146, 0x23);
            this.pnlOrganizations.TabIndex = 15;
            this.pnlOrganizations.Visible = false;
            base.AcceptButton = this.btnOK;
//            base.Appearance.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            //base.Appearance.Options.UseFont = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x146, 0x57);
            base.Controls.Add(this.txtId);
            base.Controls.Add(this.lblId);
            base.Controls.Add(this.btnecms);
            base.Controls.Add(this.btnnext);
            base.Controls.Add(this.pnlOrganizations);
            base.Controls.Add(this.pnlSecurity);
            base.Controls.Add(this.pnlButtons);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmNewID";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "命名";
            base.Load += new System.EventHandler(this.FrmNewID_Load);
            this.txtId.Properties.EndInit();
            this.pnlSecurity.EndInit();
            this.pnlSecurity.ResumeLayout(false);
            this.pnlSecurity.PerformLayout();
            this.cobSecurityLevel.Properties.EndInit();
            this.cobGroup.Properties.EndInit();
            this.txtRevLabel.Properties.EndInit();
            this.cobOrgs.Properties.EndInit();
            this.pnlButtons.EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.cobFolderEffWay.Properties.EndInit();
            this.pnlOrganizations.EndInit();
            this.pnlOrganizations.ResumeLayout(false);
            this.pnlOrganizations.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnecms;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnnext;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobFolderEffWay;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobGroup;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobOrgs;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobSecurityLevel;
        private Thyt.TiPLM.UIL.Controls.LabelPLM labCodeInfo;
        private Thyt.TiPLM.UIL.Controls.LabelPLM labEffway;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.LabelPLM lblId;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlButtons;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlOrganizations;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlSecurity;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtId;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtRevLabel;
    }
}