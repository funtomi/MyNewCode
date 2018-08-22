namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class FrmProperty {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProperty));
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbcProperty = new System.Windows.Forms.TabControl();
            this.tabProperties = new System.Windows.Forms.TabPage();
            this.tabMother = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkCover = new System.Windows.Forms.CheckBox();
            this.chkNextPage = new System.Windows.Forms.CheckBox();
            this.chkMainPage = new System.Windows.Forms.CheckBox();
            this.tbcProperty.SuspendLayout();
            this.tabMother.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancle, "btnCancle");
            this.btnCancle.Name = "btnCancle";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.tbcProperty.Controls.Add(this.tabProperties);
            this.tbcProperty.Controls.Add(this.tabMother);
            resources.ApplyResources(this.tbcProperty, "tbcProperty");
            this.tbcProperty.Name = "tbcProperty";
            this.tbcProperty.SelectedIndex = 0;
            this.tbcProperty.SelectedIndexChanged += new System.EventHandler(this.tbcProperty_SelectedIndexChanged);
            resources.ApplyResources(this.tabProperties, "tabProperties");
            this.tabProperties.Name = "tabProperties";
            this.tabMother.Controls.Add(this.groupBox2);
            this.tabMother.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.tabMother, "tabMother");
            this.tabMother.Name = "tabMother";
            this.groupBox2.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.groupBox1.Controls.Add(this.chkCover);
            this.groupBox1.Controls.Add(this.chkNextPage);
            this.groupBox1.Controls.Add(this.chkMainPage);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            resources.ApplyResources(this.chkCover, "chkCover");
            this.chkCover.Name = "chkCover";
            resources.ApplyResources(this.chkNextPage, "chkNextPage");
            this.chkNextPage.Checked = true;
            this.chkNextPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNextPage.Name = "chkNextPage";
            resources.ApplyResources(this.chkMainPage, "chkMainPage");
            this.chkMainPage.Checked = true;
            this.chkMainPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMainPage.Name = "chkMainPage";
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancle;
            base.Controls.Add(this.tbcProperty);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.btnCancle);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmProperty";
            base.ShowInTaskbar = false;
            this.tbcProperty.ResumeLayout(false);
            this.tabMother.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion 
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkCover;
        private System.Windows.Forms.CheckBox chkMainPage;
        private System.Windows.Forms.CheckBox chkNextPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabMother;
        private System.Windows.Forms.TabPage tabProperties;
        private System.Windows.Forms.TabControl tbcProperty;
    }
}