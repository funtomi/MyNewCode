namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgProcessProperty {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgProcessProperty));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCell = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbProcess = new System.Windows.Forms.ComboBox();
            this.cmbSign = new System.Windows.Forms.ComboBox();
            this.cmbSizeModel = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.lblRemark = new System.Windows.Forms.Label();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.txtCellDate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            base.SuspendLayout();
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.txtCell.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCell, "txtCell");
            this.txtCell.Name = "txtCell";
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.label4.AutoEllipsis = true;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            resources.ApplyResources(this.cmbProcess, "cmbProcess");
            this.cmbProcess.Items.AddRange(new object[] { resources.GetString("cmbProcess.Items"), resources.GetString("cmbProcess.Items1"), resources.GetString("cmbProcess.Items2"), resources.GetString("cmbProcess.Items3"), resources.GetString("cmbProcess.Items4"), resources.GetString("cmbProcess.Items5") });
            this.cmbProcess.Name = "cmbProcess";
            this.cmbSign.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbSign, "cmbSign");
            this.cmbSign.Items.AddRange(new object[] { resources.GetString("cmbSign.Items"), resources.GetString("cmbSign.Items1") });
            this.cmbSign.Name = "cmbSign";
            this.cmbSign.SelectedIndexChanged += new System.EventHandler(this.cmbSign_SelectedIndexChanged);
            this.cmbSizeModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbSizeModel, "cmbSizeModel");
            this.cmbSizeModel.Name = "cmbSizeModel";
            this.cmbSizeModel.SelectionChangeCommitted += new System.EventHandler(this.cmbSizeModel_SelectionChangeCommitted);
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancle, "btnCancle");
            this.btnCancle.Name = "btnCancle";
            resources.ApplyResources(this.lblRemark, "lblRemark");
            this.lblRemark.Name = "lblRemark";
            resources.ApplyResources(this.chkTime, "chkTime");
            this.chkTime.Name = "chkTime";
            this.txtCellDate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.txtCellDate, "txtCellDate");
            this.txtCellDate.Name = "txtCellDate";
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.label5.AutoEllipsis = true;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancle;
            base.Controls.Add(this.chkTime);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.txtCellDate);
            base.Controls.Add(this.txtCell);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.lblRemark);
            base.Controls.Add(this.btnCancle);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.cmbSizeModel);
            base.Controls.Add(this.cmbSign);
            base.Controls.Add(this.cmbProcess);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgProcessProperty";
            base.ShowInTaskbar = false;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.ComboBox cmbProcess;
        private System.Windows.Forms.ComboBox cmbSign;
        private System.Windows.Forms.ComboBox cmbSizeModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRemark;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtCell;
        private System.Windows.Forms.TextBox txtCellDate;
    }
}