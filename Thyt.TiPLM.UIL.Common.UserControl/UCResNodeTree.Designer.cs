namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCResNodeTree {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCClassTree));
            this.tvwResNode = new System.Windows.Forms.TreeView();
            base.SuspendLayout();
            resources.ApplyResources(this.tvwResNode, "tvwResNode");
            this.tvwResNode.HideSelection = false;
            this.tvwResNode.ItemHeight = 14;
            this.tvwResNode.Name = "tvwResNode";
            this.tvwResNode.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwResNode_AfterSelect);
            base.Controls.Add(this.tvwResNode);
            base.Name = "UCClassTree";
            resources.ApplyResources(this, "$this");
            base.ResumeLayout(false);
        }
        #endregion   
        private System.Windows.Forms.TreeView tvwResNode;
    
    }
}