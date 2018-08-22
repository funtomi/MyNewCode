namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCSearchInProject {
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
            this.chkInPrj = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lv_projects = new Thyt.TiPLM.UIL.Controls.ListViewPLM();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.chkInPrj);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 0x23);
            this.panel1.TabIndex = 0;
            this.chkInPrj.AutoSize = true;
            this.chkInPrj.Location = new System.Drawing.Point(0x10, 11);
            this.chkInPrj.Name = "chkInPrj";
            this.chkInPrj.Size = new System.Drawing.Size(0x6c, 0x10);
            this.chkInPrj.TabIndex = 0;
            this.chkInPrj.Text = "必须关联该项目";
            this.chkInPrj.UseVisualStyleBackColor = true;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 0x163);
            this.panel2.TabIndex = 1;
            this.groupBox1.Controls.Add(this.lv_projects);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 0x163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "项目列表";
            this.lv_projects.CheckBoxes = true;
            this.lv_projects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_projects.GridLines = true;
            this.lv_projects.Location = new System.Drawing.Point(3, 0x11);
            this.lv_projects.Name = "lv_projects";
            this.lv_projects.Size = new System.Drawing.Size(0x180, 0x14f);
            this.lv_projects.TabIndex = 0;
            this.lv_projects.UseCompatibleStateImageBehavior = false;
            this.lv_projects.View = System.Windows.Forms.View.Details;
            this.lv_projects.ItemActivate += new System.EventHandler(this.lv_projects_ItemActivate);
            this.lv_projects.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lv_projects_ItemChecked);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCSearchInProject";
            base.Size = new System.Drawing.Size(390, 390);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion   
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkInPrj;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.ListViewPLM lv_projects;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;  
    }
}