namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCProcessTemplatePicker {
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
            this.tvwProcess = new System.Windows.Forms.TreeView();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.lvProcess = new SortableListView();
            this.templateName = new System.Windows.Forms.ColumnHeader();
            this.templateTime = new System.Windows.Forms.ColumnHeader();
            base.SuspendLayout();
            this.tvwProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvwProcess.Location = new System.Drawing.Point(0, 0);
            this.tvwProcess.Name = "tvwProcess";
            node.Name = "节点0";
            node.Text = "节点0";
            this.tvwProcess.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { node });
            this.tvwProcess.Size = new System.Drawing.Size(0x88, 0xdb);
            this.tvwProcess.TabIndex = 1;
            this.tvwProcess.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwProcess_AfterSelect);
            this.tvwProcess.DoubleClick += new System.EventHandler(this.tvwProcess_DoubleClick);
            this.splitter1.Location = new System.Drawing.Point(0x88, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 0xdb);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.lvProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.templateName, this.templateTime });
            this.lvProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProcess.FullRowSelect = true;
            this.lvProcess.GridLines = true;
            this.lvProcess.HideSelection = false;
            this.lvProcess.Location = new System.Drawing.Point(0x88, 0);
            this.lvProcess.MultiSelect = false;
            this.lvProcess.Name = "lvProcess";
            this.lvProcess.Size = new System.Drawing.Size(220, 0xdb);
            this.lvProcess.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvProcess.TabIndex = 2;
            this.lvProcess.UseCompatibleStateImageBehavior = false;
            this.lvProcess.View = System.Windows.Forms.View.Details;
            this.lvProcess.DoubleClick += new System.EventHandler(this.lvProcess_DoubleClick);
            this.templateName.Text = "模板名称";
            this.templateName.Width = 0x80;
            this.templateTime.Text = "修改时间";
            this.templateTime.Width = 0x56;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.lvProcess);
            base.Controls.Add(this.tvwProcess);
            base.Name = "UCProcessTemplatePicker";
            base.Size = new System.Drawing.Size(0x164, 0xdb);
            base.ResumeLayout(false);
        }
        #endregion     
        private SortableListView lvProcess;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.ColumnHeader templateName;
        private System.Windows.Forms.ColumnHeader templateTime;
        private System.Windows.Forms.TreeView tvwProcess;
    }
}