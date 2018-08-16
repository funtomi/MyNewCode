namespace Thyt.TiPLM.CLT.TiModeler.FileModel {
    partial class FrmBrowser {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBrowser));
            this.lvwBrowser = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.cmuBrowser = new System.Windows.Forms.ContextMenu();
            this.ilsB = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lvwBrowser
            // 
            resources.ApplyResources(this.lvwBrowser, "lvwBrowser");
            this.lvwBrowser.FullRowSelect = true;
            this.lvwBrowser.HideSelection = false;
            this.lvwBrowser.MultiSelect = false;
            this.lvwBrowser.Name = "lvwBrowser";
            this.lvwBrowser.OwnerDraw = true;
            this.lvwBrowser.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwBrowser.UseCompatibleStateImageBehavior = false;
            this.lvwBrowser.View = System.Windows.Forms.View.Details;
            this.lvwBrowser.DoubleClick += new System.EventHandler(this.cmiEdit_Click);
            this.lvwBrowser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwBrowser_MouseUp);
            // 
            // ilsB
            // 
            this.ilsB.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilsB.ImageStream")));
            this.ilsB.TransparentColor = System.Drawing.Color.Transparent;
            this.ilsB.Images.SetKeyName(0, "");
            this.ilsB.Images.SetKeyName(1, "");
            // 
            // FrmBrowser
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lvwBrowser);
            this.Name = "FrmBrowser";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmBrower_Closing);
            this.Load += new System.EventHandler(this.FrmBrowser_Load);
            this.ResumeLayout(false);

        }
         
        #endregion
        private System.Windows.Forms.ContextMenu cmuBrowser;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwBrowser;
         
    }
}