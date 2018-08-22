namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class FrmBrowse {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
 
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 
        ///       

        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrowse));
            this.pnlMain = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.lvwTemplates = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.colId = new System.Windows.Forms.ColumnHeader();
            this.colRevision = new System.Windows.Forms.ColumnHeader();
            this.colIteration = new System.Windows.Forms.ColumnHeader();
            this.colReleaseDesc = new System.Windows.Forms.ColumnHeader();
            this.colState = new System.Windows.Forms.ColumnHeader();
            this.colHolder = new System.Windows.Forms.ColumnHeader();
            this.colRevCreator = new System.Windows.Forms.ColumnHeader();
            this.colRevCreatTime = new System.Windows.Forms.ColumnHeader();
            this.colReleaser = new System.Windows.Forms.ColumnHeader();
            this.colReleaseTime = new System.Windows.Forms.ColumnHeader();
            this.pnlBottom = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.grpHistory = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.numRevisions = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.btnRevisions = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.pnlMain.BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlBottom.BeginInit();
            this.pnlBottom.SuspendLayout();
            this.grpHistory.BeginInit();
            this.grpHistory.SuspendLayout();
            this.numRevisions.Properties.BeginInit();
            base.SuspendLayout();
            this.pnlMain.Controls.Add(this.lvwTemplates);
            this.pnlMain.Controls.Add(this.pnlBottom);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            this.lvwTemplates.AllowColumnReorder = true;
            this.lvwTemplates.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colId, this.colRevision, this.colIteration, this.colReleaseDesc, this.colState, this.colHolder, this.colRevCreator, this.colRevCreatTime, this.colReleaser, this.colReleaseTime });
            resources.ApplyResources(this.lvwTemplates, "lvwTemplates");
            this.lvwTemplates.FullRowSelect = true;
            this.lvwTemplates.HideSelection = false;
            this.lvwTemplates.MultiSelect = false;
            this.lvwTemplates.Name = "lvwTemplates";
            this.lvwTemplates.OwnerDraw = true;
            this.lvwTemplates.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwTemplates.UseCompatibleStateImageBehavior = false;
            this.lvwTemplates.View = System.Windows.Forms.View.Details;
            this.lvwTemplates.ItemActivate += new System.EventHandler(this.lvwTemplates_ItemActivate);
            this.lvwTemplates.SelectedIndexChanged += new System.EventHandler(this.lvwTemplates_SelectedIndexChanged);
            this.lvwTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwTemplates_MouseUp);
            resources.ApplyResources(this.colId, "colId");
            resources.ApplyResources(this.colRevision, "colRevision");
            resources.ApplyResources(this.colIteration, "colIteration");
            resources.ApplyResources(this.colReleaseDesc, "colReleaseDesc");
            resources.ApplyResources(this.colState, "colState");
            resources.ApplyResources(this.colHolder, "colHolder");
            resources.ApplyResources(this.colRevCreator, "colRevCreator");
            resources.ApplyResources(this.colRevCreatTime, "colRevCreatTime");
            resources.ApplyResources(this.colReleaser, "colReleaser");
            resources.ApplyResources(this.colReleaseTime, "colReleaseTime");
            this.pnlBottom.Controls.Add(this.grpHistory);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            resources.ApplyResources(this.grpHistory, "grpHistory");
            this.grpHistory.Controls.Add(this.numRevisions);
            this.grpHistory.Controls.Add(this.btnRevisions);
            this.grpHistory.Name = "grpHistory";
            resources.ApplyResources(this.numRevisions, "numRevisions");
            int[] bits = new int[4];
            bits[0] = 1;
            this.numRevisions.Minimum = new decimal(bits);
            this.numRevisions.Name = "numRevisions";
            this.numRevisions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numRevisions.ReadOnly = true;
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numRevisions.Value = new decimal(numArray2);
            resources.ApplyResources(this.btnRevisions, "btnRevisions");
            this.btnRevisions.Name = "btnRevisions";
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            resources.ApplyResources(this, "$this");
            base.Controls.Add(this.pnlMain);
            base.Name = "FrmBrowse";
            base.Load += new System.EventHandler(this.FrmBrowse_Load);
            this.pnlMain.EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlBottom.EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.grpHistory.EndInit();
            this.grpHistory.ResumeLayout(false);
            this.numRevisions.Properties.EndInit();
            base.ResumeLayout(false);
        }
        #endregion
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnRevisions;
        private System.Windows.Forms.ColumnHeader colHolder;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ColumnHeader colIteration;
        private System.Windows.Forms.ColumnHeader colReleaseDesc;
        private System.Windows.Forms.ColumnHeader colReleaser;
        private System.Windows.Forms.ColumnHeader colReleaseTime;
        private System.Windows.Forms.ColumnHeader colRevCreator;
        private System.Windows.Forms.ColumnHeader colRevCreatTime;
        private System.Windows.Forms.ColumnHeader colRevision;
        private System.Windows.Forms.ColumnHeader colState;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM grpHistory;
        private Thyt.TiPLM.UIL.Common.SortableListView lvwTemplates;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numRevisions;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlBottom;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlMain;

    }
}