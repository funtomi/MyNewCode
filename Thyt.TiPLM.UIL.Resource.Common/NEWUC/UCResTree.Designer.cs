namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC {
    partial class UCResTree {
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
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.TV_class = new System.Windows.Forms.TreeView();
            this.txtBox_filter = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.btn_filter = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.TV_class);
            this.panel1.Controls.Add(this.txtBox_filter);
            this.panel1.Controls.Add(this.btn_filter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 0x128);
            this.panel1.TabIndex = 0;
            this.TV_class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_class.ImageIndex = -1;
            this.TV_class.Location = new System.Drawing.Point(0, 0);
            this.TV_class.Name = "TV_class";
            this.TV_class.SelectedImageIndex = -1;
            this.TV_class.Size = new System.Drawing.Size(320, 0x128);
            this.TV_class.TabIndex = 4;
            this.TV_class.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_class_AfterSelect);
            this.txtBox_filter.Location = new System.Drawing.Point(40, 8);
            this.txtBox_filter.Name = "txtBox_filter";
            this.txtBox_filter.Size = new System.Drawing.Size(0x110, 0x15);
            this.txtBox_filter.TabIndex = 3;
            this.txtBox_filter.Text = "";
            this.btn_filter.Location = new System.Drawing.Point(8, 8);
            this.btn_filter.Name = "btn_filter";
            this.btn_filter.Size = new System.Drawing.Size(0x18, 0x17);
            this.btn_filter.TabIndex = 2;
            this.btn_filter.Text = "滤";
            base.Controls.Add(this.panel1);
            base.Name = "UCResTree";
            base.Size = new System.Drawing.Size(320, 0x128);
            base.Load += new System.EventHandler(this.UCResTree_Load);
            base.Enter += new System.EventHandler(this.UCResTree_Enter);
            this.panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion 
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_filter;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private System.Windows.Forms.TreeView TV_class;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtBox_filter;   
    }
}