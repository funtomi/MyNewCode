namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCHasRelObject {
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
            this.chkBox_HasRelObj = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.num_relobject = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lv_relation = new Thyt.TiPLM.UIL.Controls.ListViewPLM();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.num_relobject.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.chkBox_HasRelObj.Location = new System.Drawing.Point(8, 11);
            this.chkBox_HasRelObj.Name = "chkBox_HasRelObj";
            this.chkBox_HasRelObj.Size = new System.Drawing.Size(0x80, 0x15);
            this.chkBox_HasRelObj.TabIndex = 0;
            this.chkBox_HasRelObj.Text = "必须有关联对象";
            this.label1.Location = new System.Drawing.Point(160, 0x10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x20, 0x10);
            this.label1.TabIndex = 1;
            this.label1.Text = "数量";
            this.num_relobject.Location = new System.Drawing.Point(200, 8);
            int[] bits = new int[4];
            bits[0] = 0x2710;
            this.num_relobject.Maximum = new decimal(bits);
            this.num_relobject.Name = "num_relobject";
            this.num_relobject.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.num_relobject.ReadOnly = true;
            this.num_relobject.Size = new System.Drawing.Size(0x68, 0x15);
            this.num_relobject.TabIndex = 2;
            this.panel1.Controls.Add(this.chkBox_HasRelObj);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.num_relobject);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xe0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 40);
            this.panel1.TabIndex = 4;
            this.groupBox1.Controls.Add(this.lv_relation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 0xe0);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关系列表";
            this.lv_relation.CheckBoxes = true;
            this.lv_relation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.lv_relation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_relation.FullRowSelect = true;
            this.lv_relation.Location = new System.Drawing.Point(3, 0x11);
            this.lv_relation.MultiSelect = false;
            this.lv_relation.Name = "lv_relation";
            this.lv_relation.Size = new System.Drawing.Size(0x13a, 0xcc);
            this.lv_relation.TabIndex = 0;
            this.lv_relation.UseCompatibleStateImageBehavior = false;
            this.lv_relation.View = System.Windows.Forms.View.Details;
            this.lv_relation.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lv_relation_ItemCheck);
            this.columnHeader1.Text = "关系名";
            this.columnHeader1.Width = 0xb1;
            this.columnHeader2.Text = "右类名";
            this.columnHeader2.Width = 0x7e;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Name = "UCHasRelObject";
            base.Size = new System.Drawing.Size(320, 0x108);
            this.num_relobject.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion   
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkBox_HasRelObj;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.ListViewPLM lv_relation;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM num_relobject;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
    }
}