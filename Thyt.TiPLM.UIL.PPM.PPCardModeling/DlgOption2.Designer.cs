namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgOption2 {
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
            this.tbcData = new System.Windows.Forms.TabControl();
            this.tabBinding = new System.Windows.Forms.TabPage();
            this.grpArea = new System.Windows.Forms.GroupBox();
            this.btnSubKeyExplain = new System.Windows.Forms.Button();
            this.chkSubKey = new System.Windows.Forms.CheckBox();
            this.chkKey = new System.Windows.Forms.CheckBox();
            this.rbtTail = new System.Windows.Forms.RadioButton();
            this.rbtMid = new System.Windows.Forms.RadioButton();
            this.rbtHead = new System.Windows.Forms.RadioButton();
            this.grpBinding = new System.Windows.Forms.GroupBox();
            this.pnlString = new System.Windows.Forms.Panel();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.chkBarcode = new System.Windows.Forms.CheckBox();
            this.numReturnRows = new System.Windows.Forms.NumericUpDown();
            this.chkReturn = new System.Windows.Forms.CheckBox();
            this.numJumpPage = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.ultraEditor = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSmartConfig = new System.Windows.Forms.Button();
            this.cobAttrList = new System.Windows.Forms.ComboBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.chkGridReturn = new System.Windows.Forms.CheckBox();
            this.txtGridEnd = new System.Windows.Forms.TextBox();
            this.txtGridStart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBool = new System.Windows.Forms.Panel();
            this.cobBool = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tvwType = new System.Windows.Forms.TreeView();
            this.tabScript = new System.Windows.Forms.TabPage();
            this.rtxScript = new System.Windows.Forms.RichTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbcData.SuspendLayout();
            this.tabBinding.SuspendLayout();
            this.grpArea.SuspendLayout();
            this.grpBinding.SuspendLayout();
            this.pnlString.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJumpPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraEditor)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.pnlBool.SuspendLayout();
            this.tabScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcData
            // 
            this.tbcData.Controls.Add(this.tabBinding);
            this.tbcData.Controls.Add(this.tabScript);
            this.tbcData.Location = new System.Drawing.Point(0, 0);
            this.tbcData.Multiline = true;
            this.tbcData.Name = "tbcData";
            this.tbcData.SelectedIndex = 0;
            this.tbcData.Size = new System.Drawing.Size(200, 100);
            this.tbcData.TabIndex = 1;
            this.tbcData.Tag = "";
            this.tbcData.SelectedIndexChanged += new System.EventHandler(this.tbcData_SelectedIndexChanged);
            // 
            // tabBinding
            // 
            this.tabBinding.Controls.Add(this.grpArea);
            this.tabBinding.Controls.Add(this.grpBinding);
            this.tabBinding.Controls.Add(this.tvwType);
            this.tabBinding.Location = new System.Drawing.Point(4, 25);
            this.tabBinding.Name = "tabBinding";
            this.tabBinding.Size = new System.Drawing.Size(192, 71);
            this.tabBinding.TabIndex = 0;
            this.tabBinding.Tag = "Binding";
            // 
            // grpArea
            // 
            this.grpArea.Controls.Add(this.btnSubKeyExplain);
            this.grpArea.Controls.Add(this.chkSubKey);
            this.grpArea.Controls.Add(this.chkKey);
            this.grpArea.Controls.Add(this.rbtTail);
            this.grpArea.Controls.Add(this.rbtMid);
            this.grpArea.Controls.Add(this.rbtHead);
            this.grpArea.Location = new System.Drawing.Point(0, 0);
            this.grpArea.Name = "grpArea";
            this.grpArea.Size = new System.Drawing.Size(200, 100);
            this.grpArea.TabIndex = 0;
            this.grpArea.TabStop = false;
            // 
            // btnSubKeyExplain
            // 
            this.btnSubKeyExplain.ForeColor = System.Drawing.Color.Black;
            this.btnSubKeyExplain.Location = new System.Drawing.Point(0, 0);
            this.btnSubKeyExplain.Name = "btnSubKeyExplain";
            this.btnSubKeyExplain.Size = new System.Drawing.Size(75, 23);
            this.btnSubKeyExplain.TabIndex = 0;
            this.btnSubKeyExplain.UseVisualStyleBackColor = true;
            this.btnSubKeyExplain.Click += new System.EventHandler(this.btnSubKeyExplain_Click);
            // 
            // chkSubKey
            // 
            this.chkSubKey.ForeColor = System.Drawing.Color.Black;
            this.chkSubKey.Location = new System.Drawing.Point(0, 0);
            this.chkSubKey.Name = "chkSubKey";
            this.chkSubKey.Size = new System.Drawing.Size(104, 24);
            this.chkSubKey.TabIndex = 1;
            this.chkSubKey.CheckedChanged += new System.EventHandler(this.chkSubKey_CheckedChanged);
            // 
            // chkKey
            // 
            this.chkKey.ForeColor = System.Drawing.Color.Red;
            this.chkKey.Location = new System.Drawing.Point(0, 0);
            this.chkKey.Name = "chkKey";
            this.chkKey.Size = new System.Drawing.Size(104, 24);
            this.chkKey.TabIndex = 2;
            this.chkKey.CheckedChanged += new System.EventHandler(this.chkKey_CheckedChanged);
            // 
            // rbtTail
            // 
            this.rbtTail.Location = new System.Drawing.Point(0, 0);
            this.rbtTail.Name = "rbtTail";
            this.rbtTail.Size = new System.Drawing.Size(104, 24);
            this.rbtTail.TabIndex = 3;
            // 
            // rbtMid
            // 
            this.rbtMid.Checked = true;
            this.rbtMid.Location = new System.Drawing.Point(0, 0);
            this.rbtMid.Name = "rbtMid";
            this.rbtMid.Size = new System.Drawing.Size(104, 24);
            this.rbtMid.TabIndex = 4;
            this.rbtMid.TabStop = true;
            // 
            // rbtHead
            // 
            this.rbtHead.Location = new System.Drawing.Point(0, 0);
            this.rbtHead.Name = "rbtHead";
            this.rbtHead.Size = new System.Drawing.Size(104, 24);
            this.rbtHead.TabIndex = 5;
            // 
            // grpBinding
            // 
            this.grpBinding.Controls.Add(this.pnlString);
            this.grpBinding.Controls.Add(this.ultraEditor);
            this.grpBinding.Controls.Add(this.label12);
            this.grpBinding.Controls.Add(this.btnSmartConfig);
            this.grpBinding.Controls.Add(this.cobAttrList);
            this.grpBinding.Controls.Add(this.txtType);
            this.grpBinding.Controls.Add(this.label7);
            this.grpBinding.Controls.Add(this.label6);
            this.grpBinding.Controls.Add(this.txtField);
            this.grpBinding.Controls.Add(this.label5);
            this.grpBinding.Controls.Add(this.pnlGrid);
            this.grpBinding.Controls.Add(this.pnlBool);
            this.grpBinding.Location = new System.Drawing.Point(0, 0);
            this.grpBinding.Name = "grpBinding";
            this.grpBinding.Size = new System.Drawing.Size(200, 100);
            this.grpBinding.TabIndex = 1;
            this.grpBinding.TabStop = false;
            // 
            // pnlString
            // 
            this.pnlString.Controls.Add(this.cmbTemplate);
            this.pnlString.Controls.Add(this.chkBarcode);
            this.pnlString.Controls.Add(this.numReturnRows);
            this.pnlString.Controls.Add(this.chkReturn);
            this.pnlString.Controls.Add(this.numJumpPage);
            this.pnlString.Controls.Add(this.label11);
            this.pnlString.Location = new System.Drawing.Point(0, 0);
            this.pnlString.Name = "pnlString";
            this.pnlString.Size = new System.Drawing.Size(200, 100);
            this.pnlString.TabIndex = 0;
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplate.Location = new System.Drawing.Point(0, 0);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(121, 23);
            this.cmbTemplate.TabIndex = 0;
            // 
            // chkBarcode
            // 
            this.chkBarcode.Location = new System.Drawing.Point(0, 0);
            this.chkBarcode.Name = "chkBarcode";
            this.chkBarcode.Size = new System.Drawing.Size(104, 24);
            this.chkBarcode.TabIndex = 1;
            this.chkBarcode.CheckedChanged += new System.EventHandler(this.chkBarcode_CheckedChanged);
            // 
            // numReturnRows
            // 
            this.numReturnRows.Location = new System.Drawing.Point(0, 0);
            this.numReturnRows.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numReturnRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numReturnRows.Name = "numReturnRows";
            this.numReturnRows.Size = new System.Drawing.Size(120, 25);
            this.numReturnRows.TabIndex = 2;
            this.numReturnRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkReturn
            // 
            this.chkReturn.Location = new System.Drawing.Point(0, 0);
            this.chkReturn.Name = "chkReturn";
            this.chkReturn.Size = new System.Drawing.Size(104, 24);
            this.chkReturn.TabIndex = 3;
            this.chkReturn.CheckedChanged += new System.EventHandler(this.chkReturn_CheckedChanged);
            // 
            // numJumpPage
            // 
            this.numJumpPage.Location = new System.Drawing.Point(0, 0);
            this.numJumpPage.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numJumpPage.Name = "numJumpPage";
            this.numJumpPage.Size = new System.Drawing.Size(120, 25);
            this.numJumpPage.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 23);
            this.label11.TabIndex = 5;
            // 
            // ultraEditor
            // 
            this.ultraEditor.AlwaysInEditMode = true;
            this.ultraEditor.HideSelection = false;
            this.ultraEditor.Location = new System.Drawing.Point(0, 0);
            this.ultraEditor.MaxLength = 128;
            this.ultraEditor.Name = "ultraEditor";
            this.ultraEditor.ReadOnly = true;
            this.ultraEditor.Size = new System.Drawing.Size(100, 24);
            this.ultraEditor.TabIndex = 1;
            this.ultraEditor.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 23);
            this.label12.TabIndex = 2;
            // 
            // btnSmartConfig
            // 
            this.btnSmartConfig.Location = new System.Drawing.Point(0, 0);
            this.btnSmartConfig.Name = "btnSmartConfig";
            this.btnSmartConfig.Size = new System.Drawing.Size(75, 23);
            this.btnSmartConfig.TabIndex = 3;
            // 
            // cobAttrList
            // 
            this.cobAttrList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobAttrList.Location = new System.Drawing.Point(0, 0);
            this.cobAttrList.Name = "cobAttrList";
            this.cobAttrList.Size = new System.Drawing.Size(121, 23);
            this.cobAttrList.TabIndex = 4;
            this.cobAttrList.SelectionChangeCommitted += new System.EventHandler(this.cobAttrList_SelectionChangeCommitted);
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(0, 0);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(100, 25);
            this.txtType.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 7;
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(0, 0);
            this.txtField.Name = "txtField";
            this.txtField.ReadOnly = true;
            this.txtField.Size = new System.Drawing.Size(100, 25);
            this.txtField.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 9;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.chkGridReturn);
            this.pnlGrid.Controls.Add(this.txtGridEnd);
            this.pnlGrid.Controls.Add(this.txtGridStart);
            this.pnlGrid.Controls.Add(this.label3);
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(200, 100);
            this.pnlGrid.TabIndex = 10;
            // 
            // chkGridReturn
            // 
            this.chkGridReturn.Location = new System.Drawing.Point(0, 0);
            this.chkGridReturn.Name = "chkGridReturn";
            this.chkGridReturn.Size = new System.Drawing.Size(104, 24);
            this.chkGridReturn.TabIndex = 0;
            // 
            // txtGridEnd
            // 
            this.txtGridEnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGridEnd.Location = new System.Drawing.Point(0, 0);
            this.txtGridEnd.Name = "txtGridEnd";
            this.txtGridEnd.Size = new System.Drawing.Size(100, 25);
            this.txtGridEnd.TabIndex = 1;
            // 
            // txtGridStart
            // 
            this.txtGridStart.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtGridStart.Location = new System.Drawing.Point(0, 0);
            this.txtGridStart.Name = "txtGridStart";
            this.txtGridStart.ReadOnly = true;
            this.txtGridStart.Size = new System.Drawing.Size(100, 25);
            this.txtGridStart.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 3;
            // 
            // pnlBool
            // 
            this.pnlBool.Controls.Add(this.cobBool);
            this.pnlBool.Controls.Add(this.label2);
            this.pnlBool.Location = new System.Drawing.Point(0, 0);
            this.pnlBool.Name = "pnlBool";
            this.pnlBool.Size = new System.Drawing.Size(200, 100);
            this.pnlBool.TabIndex = 11;
            // 
            // cobBool
            // 
            this.cobBool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cobBool.Location = new System.Drawing.Point(0, 0);
            this.cobBool.Name = "cobBool";
            this.cobBool.Size = new System.Drawing.Size(121, 23);
            this.cobBool.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 1;
            // 
            // tvwType
            // 
            this.tvwType.ItemHeight = 14;
            this.tvwType.Location = new System.Drawing.Point(0, 0);
            this.tvwType.Name = "tvwType";
            this.tvwType.Size = new System.Drawing.Size(121, 97);
            this.tvwType.TabIndex = 2;
            this.tvwType.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwType_AfterSelect);
            // 
            // tabScript
            // 
            this.tabScript.Controls.Add(this.rtxScript);
            this.tabScript.Location = new System.Drawing.Point(4, 25);
            this.tabScript.Name = "tabScript";
            this.tabScript.Size = new System.Drawing.Size(192, 71);
            this.tabScript.TabIndex = 1;
            this.tabScript.Tag = "Script";
            // 
            // rtxScript
            // 
            this.rtxScript.Location = new System.Drawing.Point(0, 0);
            this.rtxScript.Name = "rtxScript";
            this.rtxScript.ReadOnly = true;
            this.rtxScript.Size = new System.Drawing.Size(100, 96);
            this.rtxScript.TabIndex = 0;
            this.rtxScript.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // DlgOption2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(282, 254);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbcData);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgOption2";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.DlgOption2_Load);
            this.tbcData.ResumeLayout(false);
            this.tabBinding.ResumeLayout(false);
            this.grpArea.ResumeLayout(false);
            this.grpBinding.ResumeLayout(false);
            this.grpBinding.PerformLayout();
            this.pnlString.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numReturnRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJumpPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraEditor)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            this.pnlBool.ResumeLayout(false);
            this.tabScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion      
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnSmartConfig;
        private System.Windows.Forms.Button btnSubKeyExplain;
        private System.Windows.Forms.CheckBox chkBarcode;
        private System.Windows.Forms.CheckBox chkGridReturn;
        private System.Windows.Forms.CheckBox chkKey;
        private System.Windows.Forms.CheckBox chkReturn;
        private System.Windows.Forms.CheckBox chkSubKey;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.ComboBox cobAttrList;
        private System.Windows.Forms.ComboBox cobBool;
        private System.Windows.Forms.GroupBox grpArea;
        private System.Windows.Forms.GroupBox grpBinding;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numJumpPage;
        private System.Windows.Forms.NumericUpDown numReturnRows;
        private System.Windows.Forms.Panel pnlBool;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlString;
        private System.Windows.Forms.RadioButton rbtHead;
        private System.Windows.Forms.RadioButton rbtMid;
        private System.Windows.Forms.RadioButton rbtTail;
        private System.Windows.Forms.TabPage tabBinding;
        private System.Windows.Forms.TabPage tabScript;
        private System.Windows.Forms.TabControl tbcData;
        private System.Windows.Forms.TreeView tvwType;
        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.TextBox txtGridEnd;
        private System.Windows.Forms.TextBox txtGridStart;
        private System.Windows.Forms.TextBox txtType;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor ultraEditor;

    }
}