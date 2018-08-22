namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmViewAllMarkups {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmViewAllMarkups));
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.grbProcess = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.lvwWorkItem = new SortableListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.lvwProcess = new SortableListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.lblSourceFile = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.cmbFileList = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.grbProcess.BeginInit();
            this.grbProcess.SuspendLayout();
            this.panel1.BeginInit();
            this.panel1.SuspendLayout();
            this.cmbFileList.Properties.BeginInit();
            base.SuspendLayout();
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.grbProcess.Controls.Add(this.splitter1);
            this.grbProcess.Controls.Add(this.lvwWorkItem);
            this.grbProcess.Controls.Add(this.lvwProcess);
            resources.ApplyResources(this.grbProcess, "grbProcess");
            this.grbProcess.Name = "grbProcess";
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            this.lvwWorkItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader3, this.columnHeader5, this.columnHeader4, this.columnHeader6 });
            resources.ApplyResources(this.lvwWorkItem, "lvwWorkItem");
            this.lvwWorkItem.FullRowSelect = true;
            this.lvwWorkItem.HideSelection = false;
            this.lvwWorkItem.MultiSelect = false;
            this.lvwWorkItem.Name = "lvwWorkItem";
            this.lvwWorkItem.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwWorkItem.UseCompatibleStateImageBehavior = false;
            this.lvwWorkItem.View = System.Windows.Forms.View.Details;
            this.lvwWorkItem.DoubleClick += new System.EventHandler(this.lvwWorkItem_DoubleClick);
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            resources.ApplyResources(this.columnHeader5, "columnHeader5");
            resources.ApplyResources(this.columnHeader4, "columnHeader4");
            resources.ApplyResources(this.columnHeader6, "columnHeader6");
            this.lvwProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            resources.ApplyResources(this.lvwProcess, "lvwProcess");
            this.lvwProcess.FullRowSelect = true;
            this.lvwProcess.HideSelection = false;
            this.lvwProcess.MultiSelect = false;
            this.lvwProcess.Name = "lvwProcess";
            this.lvwProcess.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwProcess.UseCompatibleStateImageBehavior = false;
            this.lvwProcess.View = System.Windows.Forms.View.Details;
            this.lvwProcess.SelectedIndexChanged += new System.EventHandler(this.lvwProcess_SelectedIndexChanged);
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.lblSourceFile.Appearance.ForeColor = (System.Drawing.Color)resources.GetObject("lblSourceFile.Appearance.ForeColor");
            this.lblSourceFile.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.lblSourceFile.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            resources.ApplyResources(this.lblSourceFile, "lblSourceFile");
            this.lblSourceFile.Name = "lblSourceFile";
            resources.ApplyResources(this.cmbFileList, "cmbFileList");
            this.cmbFileList.Name = "cmbFileList";
            this.cmbFileList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cmbFileList.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFileList.SelectedIndexChanged += new System.EventHandler(this.cmbFileList_SelectedIndexChanged);
            base.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            base.CancelButton = this.btnCancel;
            base.Controls.Add(this.cmbFileList);
            base.Controls.Add(this.lblSourceFile);
            base.Controls.Add(this.grbProcess);
            base.Controls.Add(this.panel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmViewAllMarkups";
            base.ShowInTaskbar = false;
            this.grbProcess.EndInit();
            this.grbProcess.ResumeLayout(false);
            this.panel1.EndInit();
            this.panel1.ResumeLayout(false);
            this.cmbFileList.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion  
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cmbFileList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM grbProcess;
        private Thyt.TiPLM.UIL.Controls.LabelPLM lblSourceFile;
        private SortableListView lvwProcess;
        private SortableListView lvwWorkItem;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;

    }
}