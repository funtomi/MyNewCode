namespace Thyt.TiPLM.CLT.TiModeler.FileModel {
    partial class FrmTool {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTool));
            this.cmuTool = new System.Windows.Forms.ContextMenu();
            this.lvwTool = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.SuspendLayout();
            // 
            // lvwTool
            // 
            resources.ApplyResources(this.lvwTool, "lvwTool");
            this.lvwTool.FullRowSelect = true;
            this.lvwTool.HideSelection = false;
            this.lvwTool.MultiSelect = false;
            this.lvwTool.Name = "lvwTool";
            this.lvwTool.OwnerDraw = true;
            this.lvwTool.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwTool.UseCompatibleStateImageBehavior = false;
            this.lvwTool.View = System.Windows.Forms.View.Details;
            this.lvwTool.ItemActivate += new System.EventHandler(this.lvwTool_ItemActivate);
            this.lvwTool.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwTool_MouseUp);
            // 
            // FrmTool
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lvwTool);
            this.Name = "FrmTool";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmTool_Closing);
            this.Load += new System.EventHandler(this.FrmTool_Load);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ContextMenu cmuTool;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwTool;

    }
}