namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCClassTree {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            this.tvwClass.DoubleClick -= new System.EventHandler(this.tvwClass_DoubleClick);
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UCClassTree));
            this.tvwClass = new System.Windows.Forms.TreeView();
            base.SuspendLayout();
            this.tvwClass.AccessibleDescription = resources.GetString("tvwClass.AccessibleDescription");
            this.tvwClass.AccessibleName = resources.GetString("tvwClass.AccessibleName");
            this.tvwClass.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("tvwClass.Anchor");
            this.tvwClass.BackgroundImage = (System.Drawing.Image)resources.GetObject("tvwClass.BackgroundImage");
            this.tvwClass.Dock = (System.Windows.Forms.DockStyle)resources.GetObject("tvwClass.Dock");
            this.tvwClass.Enabled = (bool)resources.GetObject("tvwClass.Enabled");
            this.tvwClass.Font = (System.Drawing.Font)resources.GetObject("tvwClass.Font");
            this.tvwClass.HideSelection = false;
            this.tvwClass.ImageIndex = (int)resources.GetObject("tvwClass.ImageIndex");
            this.tvwClass.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("tvwClass.ImeMode");
            this.tvwClass.Indent = (int)resources.GetObject("tvwClass.Indent");
            this.tvwClass.ItemHeight = (int)resources.GetObject("tvwClass.ItemHeight");
            this.tvwClass.Location = (System.Drawing.Point)resources.GetObject("tvwClass.Location");
            this.tvwClass.Name = "tvwClass";
            this.tvwClass.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("tvwClass.RightToLeft");
            this.tvwClass.SelectedImageIndex = (int)resources.GetObject("tvwClass.SelectedImageIndex");
            this.tvwClass.Size = (System.Drawing.Size)resources.GetObject("tvwClass.Size");
            this.tvwClass.TabIndex = (int)resources.GetObject("tvwClass.TabIndex");
            this.tvwClass.Text = resources.GetString("tvwClass.Text");
            this.tvwClass.Visible = (bool)resources.GetObject("tvwClass.Visible");
            this.tvwClass.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwClass_AfterSelect);
            base.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            base.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScroll = (bool)resources.GetObject("$this.AutoScroll");
            base.AutoScrollMargin = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMargin");
            base.AutoScrollMinSize = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMinSize");
            this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            base.Controls.Add(this.tvwClass);
            base.Enabled = (bool)resources.GetObject("$this.Enabled");
            this.Font = (System.Drawing.Font)resources.GetObject("$this.Font");
            base.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("$this.ImeMode");
            base.Location = (System.Drawing.Point)resources.GetObject("$this.Location");
            base.Name = "UCClassTree";
            this.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("$this.RightToLeft");
            base.Size = (System.Drawing.Size)resources.GetObject("$this.Size");
            base.Enter += new System.EventHandler(this.UCClassTree_Enter);
            base.ResumeLayout(false);
        }
         
        #endregion    
        private System.Windows.Forms.TreeView tvwClass;

    }
}