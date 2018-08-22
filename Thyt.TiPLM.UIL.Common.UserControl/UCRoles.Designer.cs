namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCRoles {
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
        ///        
        private void InitializeComponent() {
            this.lvwRole = new SortableListView();
            base.SuspendLayout();
            this.lvwRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRole.FullRowSelect = true;
            this.lvwRole.GridLines = true;
            this.lvwRole.HideSelection = false;
            this.lvwRole.Location = new System.Drawing.Point(0, 0);
            this.lvwRole.Name = "lvwRole";
            this.lvwRole.Size = new System.Drawing.Size(0x120, 0x11a);
            this.lvwRole.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwRole.TabIndex = 0;
            this.lvwRole.UseCompatibleStateImageBehavior = false;
            this.lvwRole.View = System.Windows.Forms.View.Details;
            this.lvwRole.ItemActivate += new System.EventHandler(this.lvwRole_ItemActivate);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.lvwRole);
            base.Name = "UCRoles";
            base.Size = new System.Drawing.Size(0x120, 0x11a);
            base.ResumeLayout(false);
        }
        #endregion     
        private SortableListView lvwRole;
  
    }
}