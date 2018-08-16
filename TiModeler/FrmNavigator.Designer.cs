namespace Thyt.TiPLM.CLT.TiModeler {
    partial class FrmNavigator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code


        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNavigator));
            this.lvwNavigater = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwNavigater
            // 
            this.lvwNavigater.AllowColumnReorder = true;
            this.lvwNavigater.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDesc});
            resources.ApplyResources(this.lvwNavigater, "lvwNavigater");
            this.lvwNavigater.FullRowSelect = true;
            this.lvwNavigater.HideSelection = false;
            this.lvwNavigater.MultiSelect = false;
            this.lvwNavigater.Name = "lvwNavigater";
            this.lvwNavigater.OwnerDraw = true;
            this.lvwNavigater.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwNavigater.UseCompatibleStateImageBehavior = false;
            this.lvwNavigater.View = System.Windows.Forms.View.Details;
            this.lvwNavigater.ItemActivate += new System.EventHandler(this.lvwNavigater_ItemActivate);
            this.lvwNavigater.SelectedIndexChanged += new System.EventHandler(this.lvwNavigater_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderDesc
            // 
            resources.ApplyResources(this.columnHeaderDesc, "columnHeaderDesc");
            // 
            // FrmNavigator
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lvwNavigater);
            this.Name = "FrmNavigator";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmNavigator_Closing);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwNavigater;

        #endregion
    }
}