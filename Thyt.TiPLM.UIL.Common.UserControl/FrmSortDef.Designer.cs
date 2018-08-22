namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmSortDef {
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
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.btnDown = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnUp = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnAddDesc = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.LV_Sort = new Thyt.TiPLM.UIL.Controls.ListViewPLM();
            this.cH_attr = new System.Windows.Forms.ColumnHeader();
            this.cH_type = new System.Windows.Forms.ColumnHeader();
            this.btnRemove = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnAddAsc = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.LB_Old = new Thyt.TiPLM.UIL.Controls.ListBoxPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.groupBox1.BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.LB_Old).BeginInit();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.btnDown);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnAddDesc);
            this.groupBox1.Controls.Add(this.LV_Sort);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnAddAsc);
            this.groupBox1.Controls.Add(this.LB_Old);
            this.groupBox1.Location = new System.Drawing.Point(8, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 0xe8);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.Text = "选择排序的列";
            this.btnDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDown.Location = new System.Drawing.Point(0x9c, 0xb8);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(40, 0x16);
            this.btnDown.TabIndex = 7;
            this.btnDown.Text = "下移";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            this.btnUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUp.Location = new System.Drawing.Point(0x9c, 0x9a);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(40, 0x19);
            this.btnUp.TabIndex = 6;
            this.btnUp.Text = "上移";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            this.btnAddDesc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddDesc.Location = new System.Drawing.Point(0x9c, 0x6b);
            this.btnAddDesc.Name = "btnAddDesc";
            this.btnAddDesc.Size = new System.Drawing.Size(40, 0x19);
            this.btnAddDesc.TabIndex = 5;
            this.btnAddDesc.Text = "降 >";
            this.btnAddDesc.Click += new System.EventHandler(this.btnAddDesc_Click);
            this.LV_Sort.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.LV_Sort.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.cH_attr, this.cH_type });
            this.LV_Sort.FullRowSelect = true;
            this.LV_Sort.GridLines = true;
            this.LV_Sort.Location = new System.Drawing.Point(0xd8, 30);
            this.LV_Sort.MultiSelect = false;
            this.LV_Sort.Name = "LV_Sort";
            this.LV_Sort.Size = new System.Drawing.Size(0x80, 0xb8);
            this.LV_Sort.TabIndex = 4;
            this.LV_Sort.UseCompatibleStateImageBehavior = false;
            this.LV_Sort.View = System.Windows.Forms.View.Details;
            this.LV_Sort.ItemActivate += new System.EventHandler(this.LV_Sort_ItemActivate);
            this.cH_attr.Text = "属性";
            this.cH_attr.Width = 0x55;
            this.cH_type.Text = "升降";
            this.cH_type.Width = 0x26;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(0x9c, 0x22);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(40, 0x1a);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "<";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnAddAsc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddAsc.Location = new System.Drawing.Point(0x9c, 0x4d);
            this.btnAddAsc.Name = "btnAddAsc";
            this.btnAddAsc.Size = new System.Drawing.Size(40, 0x1a);
            this.btnAddAsc.TabIndex = 2;
            this.btnAddAsc.Text = "升 >";
            this.btnAddAsc.Click += new System.EventHandler(this.btnAddAsc_Click);
            this.LB_Old.HorizontalScrollbar = true;
            this.LB_Old.ItemHeight = 0x10;
            this.LB_Old.Location = new System.Drawing.Point(12, 30);
            this.LB_Old.Name = "LB_Old";
            this.LB_Old.Size = new System.Drawing.Size(0x80, 0xb8);
            this.LB_Old.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.LB_Old.TabIndex = 0;
            this.LB_Old.DoubleClick += new System.EventHandler(this.LB_Old_DoubleClick);
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(0xe0, 0xf4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 0x1a);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(0x138, 0xf4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x38, 0x1a);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            base.AcceptButton = this.btnOK;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x17a, 0x114);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmSortDef";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "排序设置";
            base.Load += new System.EventHandler(this.FrmSortDef_Load);
            this.groupBox1.EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.LB_Old).EndInit();
            base.ResumeLayout(false);
        }
        #endregion   
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnAddAsc;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnAddDesc;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnDown;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnRemove;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnUp;
        private System.Windows.Forms.ColumnHeader cH_attr;
        private System.Windows.Forms.ColumnHeader cH_type;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.ListBoxPLM LB_Old;
        private Thyt.TiPLM.UIL.Controls.ListViewPLM LV_Sort;
    }
}