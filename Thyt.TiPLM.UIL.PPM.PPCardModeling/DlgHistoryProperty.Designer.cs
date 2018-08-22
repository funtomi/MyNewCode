namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgHistoryProperty {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgHistoryProperty));
            this.btnCancle = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbSizeModel = new System.Windows.Forms.ComboBox();
            this.cmbSign = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCellsRemark = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCellSign = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCellDate = new System.Windows.Forms.TextBox();
            this.labOrder = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.lblRemark = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            base.SuspendLayout();
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancle, "btnCancle");
            this.btnCancle.Name = "btnCancle";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.cmbSizeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbSizeModel, "cmbSizeModel");
            this.cmbSizeModel.Name = "cmbSizeModel";
            this.cmbSizeModel.SelectionChangeCommitted += new System.EventHandler(this.cmbSizeModel_SelectionChangeCommitted);
            this.cmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbSign, "cmbSign");
            this.cmbSign.Items.AddRange(new object[] { resources.GetString("cmbSign.Items"), resources.GetString("cmbSign.Items1") });
            this.cmbSign.Name = "cmbSign";
            this.cmbSign.SelectedIndexChanged += new System.EventHandler(this.cmbSign_SelectedIndexChanged);
            this.label4.AutoEllipsis = true;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label2.AutoEllipsis = true;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.txtCellsRemark.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCellsRemark, "txtCellsRemark");
            this.txtCellsRemark.Name = "txtCellsRemark";
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.txtCellSign.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCellSign, "txtCellSign");
            this.txtCellSign.Name = "txtCellSign";
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.txtCellDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCellDate, "txtCellDate");
            this.txtCellDate.Name = "txtCellDate";
            resources.ApplyResources(this.labOrder, "labOrder");
            this.labOrder.Name = "labOrder";
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            resources.ApplyResources(this.chkTime, "chkTime");
            this.chkTime.Name = "chkTime";
            resources.ApplyResources(this.lblRemark, "lblRemark");
            this.lblRemark.Name = "lblRemark";
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancle;
            base.Controls.Add(this.lblRemark);
            base.Controls.Add(this.chkTime);
            base.Controls.Add(this.label7);
            base.Controls.Add(this.labOrder);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.txtCellDate);
            base.Controls.Add(this.txtCellSign);
            base.Controls.Add(this.txtCellsRemark);
            base.Controls.Add(this.label8);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cmbSizeModel);
            base.Controls.Add(this.cmbSign);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgHistoryProperty";
            base.ShowInTaskbar = false;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labOrder;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtCellDate;
        private System.Windows.Forms.TextBox txtCellSign;
        private System.Windows.Forms.TextBox txtCellsRemark;
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.ComboBox cmbSign;
        private System.Windows.Forms.ComboBox cmbSizeModel;
    }
}