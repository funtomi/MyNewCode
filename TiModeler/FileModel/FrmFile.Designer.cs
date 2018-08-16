namespace Thyt.TiPLM.CLT.TiModeler.FileModel {
    partial class FrmFile {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFile));
            this.lvwFile = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.cmuFile = new System.Windows.Forms.ContextMenu();
            this.SuspendLayout();
            // 
            // lvwFile
            // 
            resources.ApplyResources(this.lvwFile, "lvwFile");
            this.lvwFile.FullRowSelect = true;
            this.lvwFile.HideSelection = false;
            this.lvwFile.MultiSelect = false;
            this.lvwFile.Name = "lvwFile";
            this.lvwFile.OwnerDraw = true;
            this.lvwFile.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwFile.UseCompatibleStateImageBehavior = false;
            this.lvwFile.View = System.Windows.Forms.View.Details;
            this.lvwFile.DoubleClick += new System.EventHandler(this.cmiEdit_Click);
            this.lvwFile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwFile_MouseUp);
            // 
            // FrmFile
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lvwFile);
            this.Name = "FrmFile";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmFile_Closing);
            this.Load += new System.EventHandler(this.FrmFile_Load);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ContextMenu cmuFile;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwFile;
    }
}