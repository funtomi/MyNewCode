namespace Thyt.TiPLM.CLT.TiModeler.Folder {
    partial class FrmFolder {
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmFolder));
            this.tvwFolder = new System.Windows.Forms.TreeView();
            this.cmuFolder = new System.Windows.Forms.ContextMenu();
            base.SuspendLayout();
            this.tvwFolder.AccessibleDescription = resources.GetString("tvwFolder.AccessibleDescription");
            this.tvwFolder.AccessibleName = resources.GetString("tvwFolder.AccessibleName");
            this.tvwFolder.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("tvwFolder.Anchor");
            this.tvwFolder.BackgroundImage = (System.Drawing.Image)resources.GetObject("tvwFolder.BackgroundImage");
            this.tvwFolder.Dock = (System.Windows.Forms.DockStyle)resources.GetObject("tvwFolder.Dock");
            this.tvwFolder.Enabled = (bool)resources.GetObject("tvwFolder.Enabled");
            this.tvwFolder.Font = (System.Drawing.Font)resources.GetObject("tvwFolder.Font");
            this.tvwFolder.HideSelection = false;
            this.tvwFolder.ImageIndex = (int)resources.GetObject("tvwFolder.ImageIndex");
            this.tvwFolder.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("tvwFolder.ImeMode");
            this.tvwFolder.Indent = (int)resources.GetObject("tvwFolder.Indent");
            this.tvwFolder.ItemHeight = (int)resources.GetObject("tvwFolder.ItemHeight");
            this.tvwFolder.Location = (System.Drawing.Point)resources.GetObject("tvwFolder.Location");
            this.tvwFolder.Name = "tvwFolder";
            this.tvwFolder.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("tvwFolder.RightToLeft");
            this.tvwFolder.SelectedImageIndex = (int)resources.GetObject("tvwFolder.SelectedImageIndex");
            this.tvwFolder.Size = (System.Drawing.Size)resources.GetObject("tvwFolder.Size");
            this.tvwFolder.TabIndex = (int)resources.GetObject("tvwFolder.TabIndex");
            this.tvwFolder.Text = resources.GetString("tvwFolder.Text");
            this.tvwFolder.Visible = (bool)resources.GetObject("tvwFolder.Visible");
            this.tvwFolder.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvwFolder_AfterExpand);
            this.tvwFolder.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvwFolder_AfterCollapse);
            this.tvwFolder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwFolder_MouseUp);
            this.tvwFolder.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvwFolder_AfterLabelEdit);
            this.cmuFolder.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("cmuFolder.RightToLeft");
            base.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            base.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScaleBaseSize = (System.Drawing.Size)resources.GetObject("$this.AutoScaleBaseSize");
            this.AutoScroll = (bool)resources.GetObject("$this.AutoScroll");
            base.AutoScrollMargin = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMargin");
            base.AutoScrollMinSize = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMinSize");
            this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            base.ClientSize = (System.Drawing.Size)resources.GetObject("$this.ClientSize");
            base.Controls.Add(this.tvwFolder);
            base.Enabled = (bool)resources.GetObject("$this.Enabled");
            this.Font = (System.Drawing.Font)resources.GetObject("$this.Font");
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("$this.ImeMode");
            base.Location = (System.Drawing.Point)resources.GetObject("$this.Location");
            this.MaximumSize = (System.Drawing.Size)resources.GetObject("$this.MaximumSize");
            this.MinimumSize = (System.Drawing.Size)resources.GetObject("$this.MinimumSize");
            base.Name = "FrmFolder";
            this.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("$this.RightToLeft");
            base.ShowInTaskbar = false;
            base.StartPosition = (System.Windows.Forms.FormStartPosition)resources.GetObject("$this.StartPosition");
            this.Text = resources.GetString("$this.Text");
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.Closing += new System.ComponentModel.CancelEventHandler(this.FrmFolder_Closing);
            base.Load += new System.EventHandler(this.FrmFolder_Load);
            base.Activated += new System.EventHandler(this.FrmFolder_Activated);
            base.ResumeLayout(false);
        }
         
        #endregion 
        private System.Windows.Forms.ContextMenu cmuFolder;
        private FrmMain frmMain;
        public System.Windows.Forms.TreeView tvwFolder;

    }
}