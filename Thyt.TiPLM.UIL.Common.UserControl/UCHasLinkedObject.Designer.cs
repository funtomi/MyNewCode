namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCHasLinkedObject {
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
            this.chkBox_HasRelObj = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lv_relation = new Thyt.TiPLM.UIL.Controls.ListViewPLM();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.chkBox_HasRelObj);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x13d);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x189, 0x24);
            this.panel1.TabIndex = 0;
            this.chkBox_HasRelObj.Location = new System.Drawing.Point(0x10, 9);
            this.chkBox_HasRelObj.Name = "chkBox_HasRelObj";
            this.chkBox_HasRelObj.Size = new System.Drawing.Size(0x80, 0x15);
            this.chkBox_HasRelObj.TabIndex = 1;
            this.chkBox_HasRelObj.Text = "必须有被关联对象";
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x189, 0x13d);
            this.panel2.TabIndex = 1;
            this.groupBox1.Controls.Add(this.lv_relation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x189, 0x13d);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关系列表";
            this.lv_relation.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lv_relation.CheckBoxes = true;
            this.lv_relation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.lv_relation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_relation.FullRowSelect = true;
            this.lv_relation.GridLines = true;
            this.lv_relation.Location = new System.Drawing.Point(3, 0x11);
            this.lv_relation.MultiSelect = false;
            this.lv_relation.Name = "lv_relation";
            this.lv_relation.Size = new System.Drawing.Size(0x183, 0x129);
            this.lv_relation.TabIndex = 0;
            this.lv_relation.UseCompatibleStateImageBehavior = false;
            this.lv_relation.View = System.Windows.Forms.View.Details;
            this.lv_relation.ItemActivate += new System.EventHandler(this.lv_relation_ItemActivate);
            this.lv_relation.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lv_relation_ItemChecked);
            this.columnHeader1.Text = "关系名";
            this.columnHeader1.Width = 0xb1;
            this.columnHeader2.Text = "右类名";
            this.columnHeader2.Width = 0x7e;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCHasLinkedObject";
            base.Size = new System.Drawing.Size(0x189, 0x161);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
         
        #endregion   
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkBox_HasRelObj;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.ListViewPLM lv_relation;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
    }
}