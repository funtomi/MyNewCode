namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    partial class FrmViewModel {
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 

        private void InitializeComponent() {
            this.lvwViewModel = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreator = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreateTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderIsActive = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescrip = new System.Windows.Forms.ColumnHeader();
            this.cmuVM = new System.Windows.Forms.ContextMenu();
            base.SuspendLayout();
            this.lvwViewModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeaderName, this.columnHeaderCreator, this.columnHeaderCreateTime, this.columnHeaderIsActive, this.columnHeaderDescrip });
            this.lvwViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwViewModel.FullRowSelect = true;
            this.lvwViewModel.Location = new System.Drawing.Point(0, 0);
            this.lvwViewModel.Name = "lvwViewModel";
            this.lvwViewModel.Size = new System.Drawing.Size(0x1f8, 0x16d);
            this.lvwViewModel.TabIndex = 0;
            this.lvwViewModel.View = System.Windows.Forms.View.Details;
            this.lvwViewModel.ItemActivate += new System.EventHandler(this.lvwViewModel_ItemActivate);
            this.lvwViewModel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwViewModel_MouseUp);
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 150;
            this.columnHeaderCreator.Text = "创建人";
            this.columnHeaderCreator.Width = 100;
            this.columnHeaderCreateTime.Text = "创建时间";
            this.columnHeaderCreateTime.Width = 150;
            this.columnHeaderIsActive.Text = "是否激活";
            this.columnHeaderDescrip.Text = "描述";
            this.columnHeaderDescrip.Width = 200;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x1f8, 0x16d);
            base.Controls.Add(this.lvwViewModel);
            base.Name = "FrmViewModel";
            this.Text = "视图模型";
            base.Closing += new System.ComponentModel.CancelEventHandler(this.FrmViewModel_Closing);
            base.Load += new System.EventHandler(this.FrmViewModel_Load);
            base.Activated += new System.EventHandler(this.FrmViewModel_Activated);
            base.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ContextMenu cmuVM;
        private System.Windows.Forms.ColumnHeader columnHeaderCreateTime;
        private System.Windows.Forms.ColumnHeader columnHeaderCreator;
        private System.Windows.Forms.ColumnHeader columnHeaderDescrip;
        private System.Windows.Forms.ColumnHeader columnHeaderIsActive;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwViewModel;
    }
}