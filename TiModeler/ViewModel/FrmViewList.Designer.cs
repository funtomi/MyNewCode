namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    partial class FrmViewList {
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
            this.lvwView = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreator = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCreateTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAllowEfficient = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAllowVariable = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDescrip = new System.Windows.Forms.ColumnHeader();
            this.cmuView = new System.Windows.Forms.ContextMenu();
            base.SuspendLayout();
            this.lvwView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeaderName, this.columnHeaderCreator, this.columnHeaderCreateTime, this.columnHeaderAllowEfficient, this.columnHeaderAllowVariable, this.columnHeaderDescrip });
            this.lvwView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwView.FullRowSelect = true;
            this.lvwView.Location = new System.Drawing.Point(0, 0);
            this.lvwView.Name = "lvwView";
            this.lvwView.Size = new System.Drawing.Size(0x1f0, 0x15d);
            this.lvwView.TabIndex = 1;
            this.lvwView.View = System.Windows.Forms.View.Details;
            this.lvwView.DoubleClick += new System.EventHandler(this.lvwView_DoubleClick);
            this.lvwView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwView_MouseUp);
            this.columnHeaderName.Text = "名称";
            this.columnHeaderName.Width = 100;
            this.columnHeaderCreator.Text = "创建人";
            this.columnHeaderCreator.Width = 80;
            this.columnHeaderCreateTime.Text = "创建时间";
            this.columnHeaderCreateTime.Width = 150;
            this.columnHeaderAllowEfficient.Text = "允许设置有效性";
            this.columnHeaderAllowEfficient.Width = 100;
            this.columnHeaderAllowVariable.Text = "允许进行配置";
            this.columnHeaderAllowVariable.Width = 100;
            this.columnHeaderDescrip.Text = "描述";
            this.columnHeaderDescrip.Width = 200;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x1f0, 0x15d);
            base.Controls.Add(this.lvwView);
            base.Name = "FrmViewList";
            this.Text = "视图列表";
            base.Closing += new System.ComponentModel.CancelEventHandler(this.FrmViewList_Closing);
            base.Load += new System.EventHandler(this.FrmViewList_Load);
            base.Activated += new System.EventHandler(this.FrmViewList_Activated);
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.ContextMenu cmuView;
        private System.Windows.Forms.ColumnHeader columnHeaderAllowEfficient;
        private System.Windows.Forms.ColumnHeader columnHeaderAllowVariable;
        private System.Windows.Forms.ColumnHeader columnHeaderCreateTime;
        private System.Windows.Forms.ColumnHeader columnHeaderCreator;
        private System.Windows.Forms.ColumnHeader columnHeaderDescrip;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private Thyt.TiPLM.CLT.TiModeler.FrmMain frmMain;
        private Thyt.TiPLM.UIL.Common.SortableListView lvwView;
    }
}