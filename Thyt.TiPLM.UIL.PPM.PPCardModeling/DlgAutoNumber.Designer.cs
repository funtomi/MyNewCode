namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgAutoNumber {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgAutoNumber));
            this.rbtAllPages = new System.Windows.Forms.RadioButton();
            this.rbtCurrentPage = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.numStart = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPostfix = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labNextPage = new System.Windows.Forms.Label();
            this.cmbNextPageCol = new System.Windows.Forms.ComboBox();
            this.labMainPage = new System.Windows.Forms.Label();
            this.cmbMainPageCol = new System.Windows.Forms.ComboBox();
            this.labCurPage = new System.Windows.Forms.Label();
            this.cmbCurPageCol = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numStart.BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            resources.ApplyResources(this.rbtAllPages, "rbtAllPages");
            this.rbtAllPages.Checked = true;
            this.rbtAllPages.Name = "rbtAllPages";
            this.rbtAllPages.TabStop = true;
            this.rbtAllPages.CheckedChanged += new System.EventHandler(this.rbtAllPages_CheckedChanged);
            resources.ApplyResources(this.rbtCurrentPage, "rbtCurrentPage");
            this.rbtCurrentPage.Name = "rbtCurrentPage";
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            resources.ApplyResources(this.numStart, "numStart");
            int[] bits = new int[4];
            bits[0] = 0x270f;
            this.numStart.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 0x270f;
            numArray2[3] = -2147483648;
            this.numStart.Minimum = new decimal(numArray2);
            this.numStart.Name = "numStart";
            int[] numArray3 = new int[4];
            numArray3[0] = 10;
            this.numStart.Value = new decimal(numArray3);
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            resources.ApplyResources(this.cmbInterval, "cmbInterval");
            this.cmbInterval.Items.AddRange(new object[] { resources.GetString("cmbInterval.Items"), resources.GetString("cmbInterval.Items1"), resources.GetString("cmbInterval.Items2"), resources.GetString("cmbInterval.Items3"), resources.GetString("cmbInterval.Items4"), resources.GetString("cmbInterval.Items5") });
            this.cmbInterval.Name = "cmbInterval";
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.groupBox1.Controls.Add(this.txtPostfix);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPrefix);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numStart);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbInterval);
            this.groupBox1.Controls.Add(this.label2);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            resources.ApplyResources(this.txtPostfix, "txtPostfix");
            this.txtPostfix.Name = "txtPostfix";
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            resources.ApplyResources(this.txtPrefix, "txtPrefix");
            this.txtPrefix.Name = "txtPrefix";
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.groupBox2.Controls.Add(this.labNextPage);
            this.groupBox2.Controls.Add(this.cmbNextPageCol);
            this.groupBox2.Controls.Add(this.labMainPage);
            this.groupBox2.Controls.Add(this.cmbMainPageCol);
            this.groupBox2.Controls.Add(this.labCurPage);
            this.groupBox2.Controls.Add(this.cmbCurPageCol);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.labNextPage.AutoEllipsis = true;
            resources.ApplyResources(this.labNextPage, "labNextPage");
            this.labNextPage.Name = "labNextPage";
            resources.ApplyResources(this.cmbNextPageCol, "cmbNextPageCol");
            this.cmbNextPageCol.Items.AddRange(new object[] { 
                resources.GetString("cmbNextPageCol.Items"), resources.GetString("cmbNextPageCol.Items1"), resources.GetString("cmbNextPageCol.Items2"), resources.GetString("cmbNextPageCol.Items3"), resources.GetString("cmbNextPageCol.Items4"), resources.GetString("cmbNextPageCol.Items5"), resources.GetString("cmbNextPageCol.Items6"), resources.GetString("cmbNextPageCol.Items7"), resources.GetString("cmbNextPageCol.Items8"), resources.GetString("cmbNextPageCol.Items9"), resources.GetString("cmbNextPageCol.Items10"), resources.GetString("cmbNextPageCol.Items11"), resources.GetString("cmbNextPageCol.Items12"), resources.GetString("cmbNextPageCol.Items13"), resources.GetString("cmbNextPageCol.Items14"), resources.GetString("cmbNextPageCol.Items15"),
                resources.GetString("cmbNextPageCol.Items16"), resources.GetString("cmbNextPageCol.Items17"), resources.GetString("cmbNextPageCol.Items18"), resources.GetString("cmbNextPageCol.Items19"), resources.GetString("cmbNextPageCol.Items20"), resources.GetString("cmbNextPageCol.Items21"), resources.GetString("cmbNextPageCol.Items22"), resources.GetString("cmbNextPageCol.Items23"), resources.GetString("cmbNextPageCol.Items24"), resources.GetString("cmbNextPageCol.Items25")
            });
            this.cmbNextPageCol.Name = "cmbNextPageCol";
            this.labMainPage.AutoEllipsis = true;
            resources.ApplyResources(this.labMainPage, "labMainPage");
            this.labMainPage.Name = "labMainPage";
            resources.ApplyResources(this.cmbMainPageCol, "cmbMainPageCol");
            this.cmbMainPageCol.Items.AddRange(new object[] { 
                resources.GetString("cmbMainPageCol.Items"), resources.GetString("cmbMainPageCol.Items1"), resources.GetString("cmbMainPageCol.Items2"), resources.GetString("cmbMainPageCol.Items3"), resources.GetString("cmbMainPageCol.Items4"), resources.GetString("cmbMainPageCol.Items5"), resources.GetString("cmbMainPageCol.Items6"), resources.GetString("cmbMainPageCol.Items7"), resources.GetString("cmbMainPageCol.Items8"), resources.GetString("cmbMainPageCol.Items9"), resources.GetString("cmbMainPageCol.Items10"), resources.GetString("cmbMainPageCol.Items11"), resources.GetString("cmbMainPageCol.Items12"), resources.GetString("cmbMainPageCol.Items13"), resources.GetString("cmbMainPageCol.Items14"), resources.GetString("cmbMainPageCol.Items15"),
                resources.GetString("cmbMainPageCol.Items16"), resources.GetString("cmbMainPageCol.Items17"), resources.GetString("cmbMainPageCol.Items18"), resources.GetString("cmbMainPageCol.Items19"), resources.GetString("cmbMainPageCol.Items20"), resources.GetString("cmbMainPageCol.Items21"), resources.GetString("cmbMainPageCol.Items22"), resources.GetString("cmbMainPageCol.Items23"), resources.GetString("cmbMainPageCol.Items24"), resources.GetString("cmbMainPageCol.Items25")
            });
            this.cmbMainPageCol.Name = "cmbMainPageCol";
            this.cmbMainPageCol.TextChanged += new System.EventHandler(this.cmbMainPageCol_TextChanged);
            resources.ApplyResources(this.labCurPage, "labCurPage");
            this.labCurPage.Name = "labCurPage";
            resources.ApplyResources(this.cmbCurPageCol, "cmbCurPageCol");
            this.cmbCurPageCol.Items.AddRange(new object[] { 
                resources.GetString("cmbCurPageCol.Items"), resources.GetString("cmbCurPageCol.Items1"), resources.GetString("cmbCurPageCol.Items2"), resources.GetString("cmbCurPageCol.Items3"), resources.GetString("cmbCurPageCol.Items4"), resources.GetString("cmbCurPageCol.Items5"), resources.GetString("cmbCurPageCol.Items6"), resources.GetString("cmbCurPageCol.Items7"), resources.GetString("cmbCurPageCol.Items8"), resources.GetString("cmbCurPageCol.Items9"), resources.GetString("cmbCurPageCol.Items10"), resources.GetString("cmbCurPageCol.Items11"), resources.GetString("cmbCurPageCol.Items12"), resources.GetString("cmbCurPageCol.Items13"), resources.GetString("cmbCurPageCol.Items14"), resources.GetString("cmbCurPageCol.Items15"),
                resources.GetString("cmbCurPageCol.Items16"), resources.GetString("cmbCurPageCol.Items17"), resources.GetString("cmbCurPageCol.Items18"), resources.GetString("cmbCurPageCol.Items19"), resources.GetString("cmbCurPageCol.Items20"), resources.GetString("cmbCurPageCol.Items21"), resources.GetString("cmbCurPageCol.Items22"), resources.GetString("cmbCurPageCol.Items23"), resources.GetString("cmbCurPageCol.Items24"), resources.GetString("cmbCurPageCol.Items25")
            });
            this.cmbCurPageCol.Name = "cmbCurPageCol";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.rbtCurrentPage);
            base.Controls.Add(this.rbtAllPages);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgAutoNumber";
            base.ShowInTaskbar = false;
            base.Load += new System.EventHandler(this.DlgAutoNumber_Load);
            this.numStart.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbCurPageCol;
        private System.Windows.Forms.ComboBox cmbInterval;
        private System.Windows.Forms.ComboBox cmbMainPageCol;
        private System.Windows.Forms.ComboBox cmbNextPageCol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labCurPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labMainPage;
        private System.Windows.Forms.Label labNextPage;
        private System.Windows.Forms.NumericUpDown numStart;
        private System.Windows.Forms.RadioButton rbtAllPages;
        private System.Windows.Forms.RadioButton rbtCurrentPage;
        private System.Windows.Forms.TextBox txtPostfix;
        private System.Windows.Forms.TextBox txtPrefix;
    }
}