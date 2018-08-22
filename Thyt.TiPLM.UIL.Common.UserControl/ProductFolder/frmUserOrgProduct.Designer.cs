namespace Thyt.TiPLM.UIL.Common.UserControl.ProductFolder {
    partial class frmUserOrgProduct {
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
            this.tabPage2 = new Thyt.TiPLM.UIL.Controls.TabPagePLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOk = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer = new Thyt.TiPLM.UIL.Controls.SplitContainerPLM();
            this.tabControl1 = new Thyt.TiPLM.UIL.Controls.TabControlPLM();
            this.tabPage1 = new Thyt.TiPLM.UIL.Controls.TabPagePLM();
            this.lvwTeam = new SortableListView();
            this.splitContainer.BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            base.SuspendLayout();
            this.tabPage2.Location = new System.Drawing.Point(4, 0x16);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(0x126, 0x139);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "组织/用户";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0x221, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x40, 0x17);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(0x1c1, 350);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(0x41, 0x17);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.columnHeader2.Text = "姓名";
            this.columnHeader2.Width = 100;
            this.columnHeader1.Text = "登录标识";
            this.columnHeader1.Width = 100;
            this.splitContainer.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
            this.splitContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.splitContainer.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(2, 1);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer.Size = new System.Drawing.Size(0x29f, 0x155);
            this.splitContainer.SplitterDistance = 0x130;
            this.splitContainer.TabIndex = 5;
            this.tabControl1.TabPages.Add(this.tabPage1);
            this.tabControl1.TabPages.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(0x12e, 0x153);
            this.tabControl1.TabIndex = 8;
            this.tabPage1.Controls.Add(this.lvwTeam);
            this.tabPage1.Location = new System.Drawing.Point(4, 0x16);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(0x126, 0x139);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "产品团队";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.lvwTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvwTeam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.lvwTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwTeam.FullRowSelect = true;
            this.lvwTeam.HideSelection = false;
            this.lvwTeam.Location = new System.Drawing.Point(3, 3);
            this.lvwTeam.MultiSelect = false;
            this.lvwTeam.Name = "lvwTeam";
            this.lvwTeam.Size = new System.Drawing.Size(0x120, 0x133);
            this.lvwTeam.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwTeam.TabIndex = 8;
            this.lvwTeam.UseCompatibleStateImageBehavior = false;
            this.lvwTeam.View = System.Windows.Forms.View.Details;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.ClientSize = new System.Drawing.Size(0x2a1, 0x17f);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.splitContainer);
            base.Name = "frmUserOrgProduct";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择用户或组织";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOk;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public SortableListView lvwTeam;
        private Thyt.TiPLM.UIL.Controls.SplitContainerPLM splitContainer;
        private Thyt.TiPLM.UIL.Controls.TabControlPLM tabControl1;
        private Thyt.TiPLM.UIL.Controls.TabPagePLM tabPage1;
        private Thyt.TiPLM.UIL.Controls.TabPagePLM tabPage2;

    }
}