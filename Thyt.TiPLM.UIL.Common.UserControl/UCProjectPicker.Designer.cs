namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCProjectPicker {
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
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.grpEdit = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lv_projects = new SortableListView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpEdit.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xc9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x10b, 0x20);
            this.panel1.TabIndex = 0;
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btn_ok.Location = new System.Drawing.Point(0xcb, 4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.grpEdit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x10b, 0xc9);
            this.panel2.TabIndex = 1;
            this.grpEdit.Controls.Add(this.lv_projects);
            this.grpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEdit.Location = new System.Drawing.Point(0, 0);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(0x109, 0xc7);
            this.grpEdit.TabIndex = 0;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "项目选择区";
            this.lv_projects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_projects.FullRowSelect = true;
            this.lv_projects.GridLines = true;
            this.lv_projects.HideSelection = false;
            this.lv_projects.Location = new System.Drawing.Point(3, 0x11);
            this.lv_projects.MultiSelect = false;
            this.lv_projects.Name = "lv_projects";
            this.lv_projects.Size = new System.Drawing.Size(0x103, 0xb3);
            this.lv_projects.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lv_projects.TabIndex = 0;
            this.lv_projects.UseCompatibleStateImageBehavior = false;
            this.lv_projects.View = System.Windows.Forms.View.Details;
            this.lv_projects.DoubleClick += new System.EventHandler(this.lv_projects_DoubleClick);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCProjectPicker";
            base.Size = new System.Drawing.Size(0x10b, 0xe9);
            base.Enter += new System.EventHandler(this.UCIDPicker_Enter);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpEdit.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM grpEdit;
        private SortableListView lv_projects;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
    }
}