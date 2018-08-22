namespace Thyt.TiPLM.UIL.Common.UserControl.ProductFolder {
    partial class FrmProuctRole {
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
            this.splitContainer1 = new Thyt.TiPLM.UIL.Controls.SplitContainerPLM();
            this.tvwRole = new System.Windows.Forms.TreeView();
            this.lvwTeam = new SortableListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btnDel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnAdd = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.ckbSysRole = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOk = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.splitContainer1.Location = new System.Drawing.Point(1, 1);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.tvwRole);
            this.splitContainer1.Panel2.Controls.Add(this.lvwTeam);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(0x248, 0x156);
            this.splitContainer1.SplitterDistance = 0xe4;
            this.splitContainer1.TabIndex = 1;
            this.tvwRole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwRole.HideSelection = false;
            this.tvwRole.Location = new System.Drawing.Point(0, 0);
            this.tvwRole.Name = "tvwRole";
            this.tvwRole.Size = new System.Drawing.Size(0xe4, 0x156);
            this.tvwRole.TabIndex = 0;
            this.tvwRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwRole_AfterSelect);
            this.lvwTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwTeam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3 });
            this.lvwTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwTeam.FullRowSelect = true;
            this.lvwTeam.HideSelection = false;
            this.lvwTeam.Location = new System.Drawing.Point(0, 0);
            this.lvwTeam.Name = "lvwTeam";
            this.lvwTeam.Size = new System.Drawing.Size(0xfc, 0x156);
            this.lvwTeam.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwTeam.TabIndex = 7;
            this.lvwTeam.UseCompatibleStateImageBehavior = false;
            this.lvwTeam.View = System.Windows.Forms.View.Details;
            this.columnHeader1.Text = "成员标识";
            this.columnHeader1.Width = 100;
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 100;
            this.columnHeader3.Text = "来源";
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.ckbSysRole);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(0xfc, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 0x156);
            this.panel1.TabIndex = 0;
            this.btnDel.Location = new System.Drawing.Point(0, 0x4f);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(0x52, 0x17);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = ">>移除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            this.btnAdd.Location = new System.Drawing.Point(0, 0x26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(0x52, 0x17);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "<<添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.ckbSysRole.AutoSize = true;
            this.ckbSysRole.Location = new System.Drawing.Point(4, 140);
            this.ckbSysRole.Name = "ckbSysRole";
            this.ckbSysRole.Size = new System.Drawing.Size(0x60, 0x10);
            this.ckbSysRole.TabIndex = 2;
            this.ckbSysRole.Text = "继承系统角色";
            this.ckbSysRole.UseVisualStyleBackColor = true;
            this.ckbSysRole.CheckedChanged += new System.EventHandler(this.ckbSysRole_CheckedChanged);
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(480, 0x165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4d, 0x17);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Location = new System.Drawing.Point(0x176, 0x165);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(0x4a, 0x17);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(12, 0x15b);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x161, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "注：来自父级的成员无法移除，需要到父级对应角色成员中操作！";
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.ClientSize = new System.Drawing.Size(0x24a, 0x188);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.splitContainer1);
            base.Name = "FrmProuctRole";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品共享区角色";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnAdd;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnDel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOk;
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM ckbSysRole;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        public SortableListView lvwTeam;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.SplitContainerPLM splitContainer1;
        private System.Windows.Forms.TreeView tvwRole;
    }
}