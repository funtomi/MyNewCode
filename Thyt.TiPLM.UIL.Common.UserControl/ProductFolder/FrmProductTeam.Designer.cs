namespace Thyt.TiPLM.UIL.Common.UserControl.ProductFolder {
    partial class FrmProductTeam {
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
            this.splitContainer = new Thyt.TiPLM.UIL.Controls.SplitContainerPLM();
            this.lvwTeam = new SortableListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btnDel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnAdd = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOk = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.splitContainer.BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel2.Controls.Add(this.lvwTeam);
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            this.splitContainer.Panel2.Controls.Add(this.panel2);
            this.splitContainer.Size = new System.Drawing.Size(770, 0x1b6);
            this.splitContainer.SplitterDistance = 340;
            this.splitContainer.TabIndex = 1;
            this.lvwTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwTeam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.lvwTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwTeam.FullRowSelect = true;
            this.lvwTeam.HideSelection = false;
            this.lvwTeam.Location = new System.Drawing.Point(0x4a, 0);
            this.lvwTeam.Name = "lvwTeam";
            this.lvwTeam.Size = new System.Drawing.Size(0x160, 390);
            this.lvwTeam.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwTeam.TabIndex = 6;
            this.lvwTeam.UseCompatibleStateImageBehavior = false;
            this.lvwTeam.View = System.Windows.Forms.View.Details;
            this.columnHeader1.Text = "登录标识";
            this.columnHeader1.Width = 100;
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 100;
            this.panel1.Controls.Add(this.btnDel);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x4a, 390);
            this.panel1.TabIndex = 0;
            this.btnDel.Location = new System.Drawing.Point(3, 0xdf);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(0x40, 0x17);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "《 删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            this.btnAdd.Location = new System.Drawing.Point(3, 0x7c);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(0x40, 0x17);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "增加 》";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 390);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x1aa, 0x30);
            this.panel2.TabIndex = 3;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x158, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.Location = new System.Drawing.Point(0xe1, 13);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.ClientSize = new System.Drawing.Size(770, 0x1b6);
            base.Controls.Add(this.splitContainer);
            base.Name = "FrmProductTeam";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产品团队";
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnAdd;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnDel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.SplitContainerPLM splitContainer;

    }
}