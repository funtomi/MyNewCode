namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgNewGuid {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgNewGuid));
            this.label1 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCover = new System.Windows.Forms.CheckBox();
            this.chkNextPage = new System.Windows.Forms.CheckBox();
            this.chkMainPage = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCode = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cobSecurityLevel = new System.Windows.Forms.ComboBox();
            this.cobGroup = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRevLabel = new System.Windows.Forms.TextBox();
            this.labMainItem = new System.Windows.Forms.Label();
            this.labHeadItem = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeadItem = new System.Windows.Forms.Panel();
            this.pnlMainItem = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoEllipsis = true;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.txtId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtId, "txtId");
            this.txtId.Name = "txtId";
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.groupBox1.Controls.Add(this.chkCover);
            this.groupBox1.Controls.Add(this.chkNextPage);
            this.groupBox1.Controls.Add(this.chkMainPage);
            this.groupBox1.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            resources.ApplyResources(this.chkCover, "chkCover");
            this.chkCover.Name = "chkCover";
            resources.ApplyResources(this.chkNextPage, "chkNextPage");
            this.chkNextPage.Name = "chkNextPage";
            resources.ApplyResources(this.chkMainPage, "chkMainPage");
            this.chkMainPage.Checked = true;
            this.chkMainPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMainPage.Name = "chkMainPage";
            this.groupBox2.Controls.Add(this.btnCode);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cobSecurityLevel);
            this.groupBox2.Controls.Add(this.cobGroup);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtRevLabel);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            resources.ApplyResources(this.btnCode, "btnCode");
            this.btnCode.Name = "btnCode";
            this.btnCode.Click += new System.EventHandler(this.btnCode_Click);
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.cobSecurityLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cobSecurityLevel, "cobSecurityLevel");
            this.cobSecurityLevel.Items.AddRange(new object[] { resources.GetString("cobSecurityLevel.Items"), resources.GetString("cobSecurityLevel.Items1"), resources.GetString("cobSecurityLevel.Items2"), resources.GetString("cobSecurityLevel.Items3"), resources.GetString("cobSecurityLevel.Items4"), resources.GetString("cobSecurityLevel.Items5"), resources.GetString("cobSecurityLevel.Items6"), resources.GetString("cobSecurityLevel.Items7"), resources.GetString("cobSecurityLevel.Items8"), resources.GetString("cobSecurityLevel.Items9") });
            this.cobSecurityLevel.Name = "cobSecurityLevel";
            this.cobGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cobGroup, "cobGroup");
            this.cobGroup.Name = "cobGroup";
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.label6.AutoEllipsis = true;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            resources.ApplyResources(this.txtRevLabel, "txtRevLabel");
            this.txtRevLabel.Name = "txtRevLabel";
            resources.ApplyResources(this.labMainItem, "labMainItem");
            this.labMainItem.Name = "labMainItem";
            resources.ApplyResources(this.labHeadItem, "labHeadItem");
            this.labHeadItem.Name = "labHeadItem";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            resources.ApplyResources(this.pnlHeadItem, "pnlHeadItem");
            this.pnlHeadItem.Name = "pnlHeadItem";
            resources.ApplyResources(this.pnlMainItem, "pnlMainItem");
            this.pnlMainItem.Name = "pnlMainItem";
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.pnlMainItem);
            base.Controls.Add(this.pnlHeadItem);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.labHeadItem);
            base.Controls.Add(this.labMainItem);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgNewGuid";
            base.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCode;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkCover;
        private System.Windows.Forms.CheckBox chkMainPage;
        private System.Windows.Forms.CheckBox chkNextPage;
        private System.Windows.Forms.ComboBox cobGroup;
        private System.Windows.Forms.ComboBox cobSecurityLevel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labHeadItem;
        private System.Windows.Forms.Label labMainItem;
        private System.Windows.Forms.Panel pnlHeadItem;
        private System.Windows.Forms.Panel pnlMainItem;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtRevLabel;
    }
}