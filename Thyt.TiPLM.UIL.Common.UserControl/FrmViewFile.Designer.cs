namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmViewFile {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (this.components != null) {
                    this.components.Dispose();
                }
                Thyt.TiPLM.Common.PLMEvent.Instance.D_DelSameViewer = (Thyt.TiPLM.Common.PLMDelSameViewer)System.Delegate.Remove(Thyt.TiPLM.Common.PLMEvent.Instance.D_DelSameViewer, this.DelSameViewer);
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
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.labelTitle = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.btnTopMost = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnClose = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.pnlView = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.splitContainerSmall = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelBrowser = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lstSelfDefRemarks = new Thyt.TiPLM.UIL.Controls.ListBoxPLM();
            this.groupBox2 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.txtRemark = new Thyt.TiPLM.UIL.Controls.MemoEditPLM();
            this.btnAddSDRemark = new DevExpress.XtraEditors.SimpleButton();
            this.panelButtons = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnSave = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.pnlFileList = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btnMarkup = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.combFiles = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.labFileList = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tspStateVaild = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.splitContainerSmall.BeginInit();
            this.splitContainerSmall.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.lstSelfDefRemarks).BeginInit();
            this.groupBox2.BeginInit();
            this.groupBox2.SuspendLayout();
            this.txtRemark.Properties.BeginInit();
            this.panelButtons.BeginInit();
            this.panelButtons.SuspendLayout();
            this.pnlFileList.SuspendLayout();
            this.combFiles.Properties.BeginInit();
            this.statusStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Controls.Add(this.btnTopMost);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x3fe, 40);
            this.panel1.TabIndex = 0;
            this.labelTitle.Location = new System.Drawing.Point(8, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(0, 14);
            this.labelTitle.TabIndex = 2;
            this.btnTopMost.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnTopMost.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnTopMost.Location = new System.Drawing.Point(0x346, 8);
            this.btnTopMost.Name = "btnTopMost";
            this.btnTopMost.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnTopMost.TabIndex = 1;
            this.btnTopMost.Text = "置顶";
            this.btnTopMost.Click += new System.EventHandler(this.btnTopMost_Click);
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(0x3a6, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.pnlView.Controls.Add(this.splitContainerSmall);
            this.pnlView.Controls.Add(this.panelButtons);
            this.pnlView.Controls.Add(this.pnlFileList);
            this.pnlView.Controls.Add(this.statusStrip1);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(0x3fe, 0x176);
            this.pnlView.TabIndex = 1;
            this.splitContainerSmall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSmall.Location = new System.Drawing.Point(0, 0x21);
            this.splitContainerSmall.Name = "splitContainerSmall";
            this.splitContainerSmall.Panel1.Controls.Add(this.panelBrowser);
            this.splitContainerSmall.Panel1.Text = "Panel1";
            this.splitContainerSmall.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainerSmall.Panel2.Text = "Panel2";
            this.splitContainerSmall.Size = new System.Drawing.Size(0x3fe, 0x119);
            this.splitContainerSmall.SplitterPosition = 790;
            this.splitContainerSmall.TabIndex = 4;
            this.splitContainerSmall.Text = "splitContainerControl1";
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(0, 0);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(790, 0x119);
            this.panelBrowser.TabIndex = 1;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnAddSDRemark, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 151f));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.10738f));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.89262f));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(0xe3, 0x119);
            this.tableLayoutPanel1.TabIndex = 0;
            this.groupBox3.Controls.Add(this.lstSelfDefRemarks);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 0xae);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(0xdd, 0x68);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.Text = "自定义批注";
            this.lstSelfDefRemarks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSelfDefRemarks.HorizontalScrollbar = true;
            this.lstSelfDefRemarks.ItemHeight = 0x10;
            this.lstSelfDefRemarks.Location = new System.Drawing.Point(2, 0x16);
            this.lstSelfDefRemarks.Name = "lstSelfDefRemarks";
            this.lstSelfDefRemarks.Size = new System.Drawing.Size(0xd9, 80);
            this.lstSelfDefRemarks.TabIndex = 0;
            this.lstSelfDefRemarks.DoubleClick += new System.EventHandler(this.lstSelfDefRemarks_DoubleClick);
            this.lstSelfDefRemarks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lstSelfDefRemarks_MouseMove);
            this.groupBox2.Controls.Add(this.txtRemark);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(0xdd, 0x91);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.Text = "填写批注(1000字以内)";
            this.txtRemark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRemark.Location = new System.Drawing.Point(2, 0x16);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.MaxLength = 0x3e8;
            this.txtRemark.Size = new System.Drawing.Size(0xd9, 0x79);
            this.txtRemark.TabIndex = 1;
            this.txtRemark.UseOptimizedRendering = true;
            this.txtRemark.TextChanged += new System.EventHandler(this.txtRemark_TextChanged);
            this.btnAddSDRemark.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddSDRemark.Location = new System.Drawing.Point(0x5f, 0x9a);
            this.btnAddSDRemark.Name = "btnAddSDRemark";
            this.btnAddSDRemark.Size = new System.Drawing.Size(0x25, 14);
            this.btnAddSDRemark.TabIndex = 5;
            this.btnAddSDRemark.Text = "Λ";
            this.btnAddSDRemark.Click += new System.EventHandler(this.btnAddSDRemark_Click);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 0x13a);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(0x3fe, 0x26);
            this.panelButtons.TabIndex = 5;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Appearance.Options.UseBackColor = true;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.Location = new System.Drawing.Point(0x38c, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消批注";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Appearance.Options.UseBackColor = true;
            this.btnSave.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(0x33b, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存批注";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.pnlFileList.Controls.Add(this.btnMarkup);
            this.pnlFileList.Controls.Add(this.combFiles);
            this.pnlFileList.Controls.Add(this.labFileList);
            this.pnlFileList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFileList.Location = new System.Drawing.Point(0, 0);
            this.pnlFileList.Name = "pnlFileList";
            this.pnlFileList.Size = new System.Drawing.Size(0x3fe, 0x21);
            this.pnlFileList.TabIndex = 2;
            this.btnMarkup.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnMarkup.Location = new System.Drawing.Point(0x242, 8);
            this.btnMarkup.Name = "btnMarkup";
            this.btnMarkup.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnMarkup.TabIndex = 1;
            this.btnMarkup.Text = "批注";
            this.btnMarkup.Click += new System.EventHandler(this.btnMarkup_Click);
            this.combFiles.Location = new System.Drawing.Point(0x47, 6);
            this.combFiles.Name = "combFiles";
            this.combFiles.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.combFiles.Properties.PopupFormSize = new System.Drawing.Size(600, 0);
            this.combFiles.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.combFiles.Size = new System.Drawing.Size(0x1f1, 0x16);
            this.combFiles.TabIndex = 1;
            this.combFiles.SelectedIndexChanged += new System.EventHandler(this.combFiles_SelectedIndexChanged);
            this.labFileList.Location = new System.Drawing.Point(12, 9);
            this.labFileList.Name = "labFileList";
            this.labFileList.Size = new System.Drawing.Size(0x30, 14);
            this.labFileList.TabIndex = 0;
            this.labFileList.Text = "源文件：";
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tspStateVaild });
            this.statusStrip1.Location = new System.Drawing.Point(0, 0x160);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(0x3fe, 0x16);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            this.tspStateVaild.Name = "tspStateVaild";
            this.tspStateVaild.Size = new System.Drawing.Size(0, 0x11);
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x3fe, 0x19e);
            base.Controls.Add(this.pnlView);
            base.Controls.Add(this.panel1);
            base.Name = "FrmViewFile";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "浏览文件";
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.Closing += new System.ComponentModel.CancelEventHandler(this.FrmViewFile_Closing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlView.ResumeLayout(false);
            this.pnlView.PerformLayout();
            this.splitContainerSmall.EndInit();
            this.splitContainerSmall.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox3.EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.lstSelfDefRemarks).EndInit();
            this.groupBox2.EndInit();
            this.groupBox2.ResumeLayout(false);
            this.txtRemark.Properties.EndInit();
            this.panelButtons.EndInit();
            this.panelButtons.ResumeLayout(false);
            this.pnlFileList.ResumeLayout(false);
            this.pnlFileList.PerformLayout();
            this.combFiles.Properties.EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            base.ResumeLayout(false);
        }
         
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnClose;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnTopMost;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM combFiles;
        private Thyt.TiPLM.UIL.Controls.LabelPLM labelTitle;
        private Thyt.TiPLM.UIL.Controls.LabelPLM labFileList;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlFileList;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tspStateVaild;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panelBrowser;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnMarkup;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnSave;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox2;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox3;
        private DevExpress.XtraEditors.PanelControl panelButtons;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerSmall;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Thyt.TiPLM.UIL.Controls.ListBoxPLM lstSelfDefRemarks;
        private Thyt.TiPLM.UIL.Controls.MemoEditPLM txtRemark;


    }
}