namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCOrgs {
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
            System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode("节点0");
            this.tvwOrg = new System.Windows.Forms.TreeView();
            base.SuspendLayout();
            this.tvwOrg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwOrg.Location = new System.Drawing.Point(0, 0);
            this.tvwOrg.Name = "tvwOrg";
            node.Name = "节点0";
            node.Text = "节点0";
            this.tvwOrg.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { node });
            this.tvwOrg.Size = new System.Drawing.Size(0x123, 0x12f);
            this.tvwOrg.TabIndex = 0;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.tvwOrg);
            base.Name = "UCOrgs";
            base.Size = new System.Drawing.Size(0x123, 0x12f);
            base.ResumeLayout(false);
        }
        #endregion  
        private System.Windows.Forms.TreeView tvwOrg;

    }
}