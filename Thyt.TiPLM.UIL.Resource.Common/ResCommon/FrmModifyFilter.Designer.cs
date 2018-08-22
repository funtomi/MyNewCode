namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon {
    partial class FrmModifyFilter {
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
            this.cobOper = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.txtBoxValue = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.cobName = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.cobOper.Properties.BeginInit();
            this.cobName.Properties.BeginInit();
            base.SuspendLayout();
            this.cobOper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobOper.Properties.Items.AddRange(new object[] { "前几字符是", "后几字符是", "包含", "是", "不是", ">", "<", "=", ">=", "<=", "<>" });
            this.cobOper.Location = new System.Drawing.Point(0x40, 40);
            this.cobOper.Name = "cobOper";
            this.cobOper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cobOper.Size = new System.Drawing.Size(0xe0, 20);
            this.cobOper.TabIndex = 3;
            this.cobOper.SelectedIndexChanged += new System.EventHandler(this.cobOper_SelectedIndexChanged);
            this.txtBoxValue.Location = new System.Drawing.Point(0x40, 0x48);
            this.txtBoxValue.Name = "txtBoxValue";
            this.txtBoxValue.Size = new System.Drawing.Size(0xe0, 0x15);
            this.txtBoxValue.TabIndex = 5;
            this.txtBoxValue.Text = "";
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x30, 0x17);
            this.label2.TabIndex = 6;
            this.label2.Text = "参数名:";
            this.label1.Location = new System.Drawing.Point(8, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x30, 0x17);
            this.label1.TabIndex = 8;
            this.label1.Text = "运算符:";
            this.label3.Location = new System.Drawing.Point(8, 0x48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x30, 0x17);
            this.label3.TabIndex = 9;
            this.label3.Text = "参数值:";
            this.btnOK.Location = new System.Drawing.Point(0x88, 0x68);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x40, 0x18);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0xe0, 0x68);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x40, 0x18);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取 消";
            this.cobName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobName.Location = new System.Drawing.Point(0x40, 8);
            this.cobName.Name = "cobName";
            this.cobName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cobName.Size = new System.Drawing.Size(0xe0, 20);
            this.cobName.TabIndex = 12;
            this.cobName.SelectedIndexChanged += new System.EventHandler(this.cobName_SelectedIndexChanged);
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x130, 0x85);
            base.Controls.Add(this.cobName);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.txtBoxValue);
            base.Controls.Add(this.cobOper);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmModifyFilter";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改过滤条件";
            base.Load += new System.EventHandler(this.FrmModifyFilter_Load);
            this.cobOper.Properties.EndInit();
            this.cobName.Properties.EndInit();
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobName;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cobOper;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtBoxValue;

    }
}