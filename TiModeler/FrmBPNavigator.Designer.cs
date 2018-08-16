namespace Thyt.TiPLM.CLT.TiModeler {
    partial class FrmBPNavigator {
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
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBPNavigator));
            this.lvwNavigater = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreatorName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreaterTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderUpateTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLifecycle = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDesc = new System.Windows.Forms.ColumnHeader();
            this.txtSearch = new DevExpress.XtraEditors.TextEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnPre = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearch.Properties.BeginInit();
            this.panelControl1.BeginInit();
            this.panelControl1.SuspendLayout();
            base.SuspendLayout();
            this.lvwNavigater.AllowColumnReorder = true;
            this.lvwNavigater.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeaderName, this.columnHeaderCreatorName, this.columnHeaderCreaterTime, this.columnHeaderUpateTime, this.columnHeaderLifecycle, this.columnHeaderDesc });
            resources.ApplyResources(this.lvwNavigater, "lvwNavigater");
            this.lvwNavigater.FullRowSelect = true;
            this.lvwNavigater.HideSelection = false;
            this.lvwNavigater.Name = "lvwNavigater";
            this.lvwNavigater.OwnerDraw = true;
            this.lvwNavigater.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwNavigater.UseCompatibleStateImageBehavior = false;
            this.lvwNavigater.View = System.Windows.Forms.View.Details;
            this.lvwNavigater.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvwNavigater_AfterLabelEdit);
            this.lvwNavigater.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvwNavigator_ItemDrag);
            this.lvwNavigater.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwNavigater_ItemSelectionChanged);
            this.lvwNavigater.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvwNavigator_DragDrop);
            this.lvwNavigater.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwNavigator_DragEnter);
            this.lvwNavigater.DragOver += new System.Windows.Forms.DragEventHandler(this.lvwNavigator_DragOver);
            this.lvwNavigater.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvwNavigater_KeyUp);
            this.lvwNavigater.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwNavigater_MouseDoubleClick);
            this.lvwNavigater.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwNavigater_MouseUp);
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            resources.ApplyResources(this.columnHeaderCreatorName, "columnHeaderCreatorName");
            resources.ApplyResources(this.columnHeaderCreaterTime, "columnHeaderCreaterTime");
            resources.ApplyResources(this.columnHeaderUpateTime, "columnHeaderUpateTime");
            resources.ApplyResources(this.columnHeaderLifecycle, "columnHeaderLifecycle");
            resources.ApplyResources(this.columnHeaderDesc, "columnHeaderDesc");
            resources.ApplyResources(this.txtSearch, "txtSearch");
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.panelControl1.Controls.Add(this.btnNext);
            this.panelControl1.Controls.Add(this.btnPre);
            this.panelControl1.Controls.Add(this.txtSearch);
            this.panelControl1.Controls.Add(this.btnSearch);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            resources.ApplyResources(this.btnNext, "btnNext");
            this.btnNext.Name = "btnNext";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            resources.ApplyResources(this.btnPre, "btnPre");
            this.btnPre.Name = "btnPre";
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            resources.ApplyResources(this, "$this");
            base.Controls.Add(this.lvwNavigater);
            base.Controls.Add(this.panelControl1);
            base.Name = "FrmBPNavigator";
            base.ShowInTaskbar = false;
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.txtSearch.Properties.EndInit();
            this.panelControl1.EndInit();
            this.panelControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnPre;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.ColumnHeader columnHeaderCreaterTime;
        private System.Windows.Forms.ColumnHeader columnHeaderCreatorName;
        private System.Windows.Forms.ColumnHeader columnHeaderDesc;
        private System.Windows.Forms.ColumnHeader columnHeaderLifecycle;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderUpateTime;
        #endregion
    }
}