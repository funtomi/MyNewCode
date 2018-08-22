namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCUsers {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCUsers));
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.lvwUser = new SortableListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tvwOrg = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOrg = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUser = new System.Windows.Forms.ToolStripButton();
            this.txtUser = new System.Windows.Forms.ToolStripTextBox();
            this.panel1.BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.lvwUser);
            this.panel1.Controls.Add(this.tvwOrg);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            this.lvwUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            resources.ApplyResources(this.lvwUser, "lvwUser");
            this.lvwUser.FullRowSelect = true;
            this.lvwUser.HideSelection = false;
            this.lvwUser.MultiSelect = false;
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.OwnerDraw = true;
            this.lvwUser.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwUser.UseCompatibleStateImageBehavior = false;
            this.lvwUser.View = System.Windows.Forms.View.Details;
            this.lvwUser.ItemActivate += new System.EventHandler(this.lvwUser_ItemActivate);
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            this.tvwOrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tvwOrg, "tvwOrg");
            this.tvwOrg.ItemHeight = 14;
            this.tvwOrg.Name = "tvwOrg";
            this.tvwOrg.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { (System.Windows.Forms.TreeNode)resources.GetObject("tvwOrg.Nodes") });
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.btnOrg, this.toolStripSeparator1, this.btnUser, this.txtUser });
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.btnOrg.Checked = true;
            this.btnOrg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnOrg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnOrg, "btnOrg");
            this.btnOrg.Name = "btnOrg";
            this.btnOrg.Click += new System.EventHandler(this.btnOrg_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.btnUser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.btnUser, "btnUser");
            this.btnUser.Name = "btnUser";
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            this.txtUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUser.AutoToolTip = true;
            this.txtUser.Name = "txtUser";
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUser_KeyDown);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.toolStrip1);
            base.Name = "UCUsers";
            resources.ApplyResources(this, "$this");
            this.panel1.EndInit();
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion 
        private System.Windows.Forms.ToolStripButton btnOrg;
        private System.Windows.Forms.ToolStripButton btnUser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public SortableListView lvwUser;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.TreeView tvwOrg;
        private System.Windows.Forms.ToolStripTextBox txtUser;
    }
}