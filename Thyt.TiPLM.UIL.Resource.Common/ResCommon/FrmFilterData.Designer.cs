namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon {
    partial class FrmFilterData {
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
            this.lvwCondition = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.col1 = new System.Windows.Forms.ColumnHeader();
            this.col2 = new System.Windows.Forms.ColumnHeader();
            this.label5 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.txtCondition = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.label4 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.btnClear = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnModify = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnDelete = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnAdd = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.cobOper = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.txtParamValue = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.cobParamName = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.colh1 = new System.Windows.Forms.ColumnHeader();
            this.colh2 = new System.Windows.Forms.ColumnHeader();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnYes = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.cobParamName.Properties.BeginInit();
            this.cobOper.Properties.BeginInit();
            this.groupBox1.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.lvwCondition);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCondition);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnModify);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.cobOper);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtParamValue);
            this.groupBox1.Controls.Add(this.cobParamName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x1d8, 0x123);
            this.groupBox1.TabIndex = 0;
            this.lvwCondition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.col1, this.col2 });
            this.lvwCondition.FullRowSelect = true;
            this.lvwCondition.GridLines = true;
            this.lvwCondition.HideSelection = false;
            this.lvwCondition.Location = new System.Drawing.Point(8, 0x4d);
            this.lvwCondition.Name = "lvwCondition";
            this.lvwCondition.Size = new System.Drawing.Size(0x188, 0x9a);
            this.lvwCondition.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwCondition.TabIndex = 14;
            this.lvwCondition.UseCompatibleStateImageBehavior = false;
            this.lvwCondition.View = System.Windows.Forms.View.Details;
            this.col1.Text = "编号";
            this.col1.Width = 0x62;
            this.col2.Text = "筛选条件";
            this.col2.Width = 290;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(0x90, 0x10a);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0x110, 0x11);
            this.label5.TabIndex = 13;
            this.label5.Text = "例:1 and ( 2 or 3 )   注意：用空格分隔 ！";
            this.txtCondition.Location = new System.Drawing.Point(0x92, 240);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(0xfe, 0x16);
            this.txtCondition.TabIndex = 12;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(8, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0x89, 0x19);
            this.label4.TabIndex = 11;
            this.label4.Text = "筛选条件之间的关系:";
            this.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnClear.Location = new System.Drawing.Point(0x195, 0xc5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(0x38, 0x1a);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnModify.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnModify.Location = new System.Drawing.Point(0x195, 0xa3);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(0x38, 0x1a);
            this.btnModify.TabIndex = 9;
            this.btnModify.Text = "修改";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDelete.Location = new System.Drawing.Point(0x195, 0x81);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(0x38, 0x19);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "删除";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(0x195, 0x5e);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(0x38, 0x1a);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.cobOper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOper.ItemHeight = 0x10;
            this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "是", "不是", ">", "<", "=", ">=", "<=", "<>" });
            this.cobOper.Location = new System.Drawing.Point(360, 0x11);
            this.cobOper.Name = "cobOper";
            this.cobOper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cobOper.Size = new System.Drawing.Size(0x68, 0x16);
            this.cobOper.TabIndex = 5;
            this.cobOper.SelectedIndexChanged += new System.EventHandler(this.cobOper_SelectedIndexChanged);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(0x12f, 0x11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x3b, 0x19);
            this.label3.TabIndex = 4;
            this.label3.Text = "运算符:";
            this.txtParamValue.Location = new System.Drawing.Point(0x45, 0x2e);
            this.txtParamValue.Name = "txtParamValue";
            this.txtParamValue.Size = new System.Drawing.Size(0xe5, 0x16);
            this.txtParamValue.TabIndex = 3;
            this.cobParamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobParamName.ItemHeight = 0x10;
            this.cobParamName.Location = new System.Drawing.Point(0x45, 0x11);
            this.cobParamName.Name = "cobParamName";
            this.cobParamName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cobParamName.Size = new System.Drawing.Size(0xe5, 0x16);
            this.cobParamName.TabIndex = 2;
            this.cobParamName.SelectedIndexChanged += new System.EventHandler(this.cobParamName_SelectedIndexChanged);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(8, 0x2e);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x3d, 0x17);
            this.label2.TabIndex = 1;
            this.label2.Text = "参数值:";
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x3d, 0x16);
            this.label1.TabIndex = 0;
            this.label1.Text = "参数名:";
            this.colh1.Text = "编号";
            this.colh1.Width = 0x54;
            this.colh2.Text = "筛选条件";
            this.colh2.Width = 300;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(0x188, 0x127);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x19);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnYes.Location = new System.Drawing.Point(0x130, 0x127);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(0x4b, 0x19);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "确定";
            this.btnYes.Click += new System.EventHandler(this.btnOK_Click);
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x1d8, 0x147);
            base.Controls.Add(this.btnYes);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.groupBox1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmFilterData";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置过滤条件";
            base.Load += new System.EventHandler(this.FrmFilterData_Load);
            this.cobParamName.Properties.EndInit();
            this.cobOper.Properties.EndInit();
            this.groupBox1.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnAdd;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnClear;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnDelete;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnModify;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnYes;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobOper;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobParamName;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.ColumnHeader colh1;
        private System.Windows.Forms.ColumnHeader colh2;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label4;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label5;

        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtCondition;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtParamValue;
    }
}