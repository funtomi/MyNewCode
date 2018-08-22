namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgBarcode {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgBarcode));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCellEnd = new System.Windows.Forms.TextBox();
            this.txtCellStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAttrList = new System.Windows.Forms.ComboBox();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.panel1.Controls.Add(this.cmbTemplate);
            this.panel1.Controls.Add(this.txtCellEnd);
            this.panel1.Controls.Add(this.txtCellStart);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbAttrList);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            resources.ApplyResources(this.txtCellEnd, "txtCellEnd");
            this.txtCellEnd.Name = "txtCellEnd";
            resources.ApplyResources(this.txtCellStart, "txtCellStart");
            this.txtCellStart.Name = "txtCellStart";
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.cmbAttrList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbAttrList, "cmbAttrList");
            this.cmbAttrList.Name = "cmbAttrList";
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbTemplate, "cmbTemplate");
            this.cmbTemplate.Name = "cmbTemplate";
            base.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.btnOk);
            base.Controls.Add(this.btnCancel);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgBarcode";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbAttrList;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCellEnd;
        private System.Windows.Forms.TextBox txtCellStart;
    }
}