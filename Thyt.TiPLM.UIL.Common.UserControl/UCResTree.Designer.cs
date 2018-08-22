namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCResTree {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (CAPPResHandler != null) {
                this.SelectCAPPResChanged -= CAPPResHandler;
            }
            this.lvw.DoubleClick -= this.lvwDblClickHandler;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager manager = new System.ComponentModel.ComponentResourceManager(typeof(UCResTree));
            this.pnlAll = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gbFilter = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.gbData = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.tb = new System.Windows.Forms.ToolStrip();
            this.tbnSort = new System.Windows.Forms.ToolStripButton();
            this.tbn_split = new System.Windows.Forms.ToolStripSeparator();
            this.tbnClear = new System.Windows.Forms.ToolStripButton();
            this.tbnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbn_split1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbnShowMPage = new System.Windows.Forms.ToolStripButton();
            this.tbnPre = new System.Windows.Forms.ToolStripButton();
            this.tbnNext = new System.Windows.Forms.ToolStripButton();
            this.cB_pageSize = new System.Windows.Forms.ToolStripComboBox();
            this.tbn_split2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_upStep = new System.Windows.Forms.ToolStripButton();
            this.btn_downStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_viewFile = new System.Windows.Forms.ToolStripButton();
            this.btn_Float = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tBnOutput = new System.Windows.Forms.ToolStripButton();
            this.pnlPicker = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlLvw = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.lvw = new ThumbnailListView();
            this.pnlAll.SuspendLayout();
            this.splitContainer.BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.gbData.SuspendLayout();
            this.tb.SuspendLayout();
            this.pnlLvw.SuspendLayout();
            base.SuspendLayout();
            this.pnlAll.AutoScroll = true;
            this.pnlAll.Controls.Add(this.splitContainer);
            this.pnlAll.Controls.Add(this.tb);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAll.Location = new System.Drawing.Point(0, 0x19);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(0x188, 0x20a);
            this.pnlAll.TabIndex = 2;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0x19);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.Controls.Add(this.gbFilter);
            this.splitContainer.Panel1MinSize = 0;
            this.splitContainer.Panel2.Controls.Add(this.gbData);
            this.splitContainer.Panel2MinSize = 50;
            this.splitContainer.Size = new System.Drawing.Size(0x188, 0x1f1);
            this.splitContainer.SplitterDistance = 100;
            this.splitContainer.TabIndex = 5;
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Location = new System.Drawing.Point(0, 0);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(0x188, 100);
            this.gbFilter.TabIndex = 2;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "过滤条件:";
            this.gbData.Controls.Add(this.pnlLvw);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Location = new System.Drawing.Point(0, 0);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(0x188, 0x189);
            this.gbData.TabIndex = 2;
            this.gbData.TabStop = false;
            this.gbData.Text = "资源数据:";
            this.tb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { 
                this.tbnSort, this.tbn_split, this.tbnClear, this.tbnRefresh, this.tbn_split1, this.tbnShowMPage, this.tbnPre, this.tbnNext, this.cB_pageSize, this.tbn_split2, this.btn_upStep, this.btn_downStep, this.toolStripSeparator1, this.btn_viewFile, this.btn_Float, this.toolStripSeparator2,
                this.tBnOutput
            });
            this.tb.Location = new System.Drawing.Point(0, 0);
            this.tb.Name = "tb";
            this.tb.Size = new System.Drawing.Size(0x188, 0x19);
            this.tb.TabIndex = 0;
            this.tbnSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnSort.Image = (System.Drawing.Image)manager.GetObject("tbnSort.Image");
            this.tbnSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnSort.Name = "tbnSort";
            this.tbnSort.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnSort.ToolTipText = "排序";
            this.tbnSort.Click += new System.EventHandler(this.tbnSort_Click);
            this.tbn_split.Name = "tbn_split";
            this.tbn_split.Size = new System.Drawing.Size(6, 0x19);
            this.tbnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnClear.Image = (System.Drawing.Image)manager.GetObject("tbnClear.Image");
            this.tbnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnClear.Name = "tbnClear";
            this.tbnClear.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnClear.ToolTipText = "清空过滤条件";
            this.tbnClear.Click += new System.EventHandler(this.tbnClear_Click);
            this.tbnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnRefresh.Image = (System.Drawing.Image)manager.GetObject("tbnRefresh.Image");
            this.tbnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnRefresh.Name = "tbnRefresh";
            this.tbnRefresh.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnRefresh.ToolTipText = "刷新数据";
            this.tbnRefresh.Click += new System.EventHandler(this.tbnRefresh_Click);
            this.tbn_split1.Name = "tbn_split1";
            this.tbn_split1.Size = new System.Drawing.Size(6, 0x19);
            this.tbnShowMPage.Checked = true;
            this.tbnShowMPage.CheckOnClick = true;
            this.tbnShowMPage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbnShowMPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnShowMPage.Image = (System.Drawing.Image)manager.GetObject("tbnShowMPage.Image");
            this.tbnShowMPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnShowMPage.Name = "tbnShowMPage";
            this.tbnShowMPage.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnShowMPage.ToolTipText = "隐藏分页显示";
            this.tbnShowMPage.Click += new System.EventHandler(this.tbnShowMPage_Click);
            this.tbnPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnPre.Image = (System.Drawing.Image)manager.GetObject("tbnPre.Image");
            this.tbnPre.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnPre.Name = "tbnPre";
            this.tbnPre.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnPre.ToolTipText = "上一页";
            this.tbnPre.Click += new System.EventHandler(this.tbnPre_Click);
            this.tbnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbnNext.Image = (System.Drawing.Image)manager.GetObject("tbnNext.Image");
            this.tbnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnNext.Name = "tbnNext";
            this.tbnNext.Size = new System.Drawing.Size(0x17, 0x16);
            this.tbnNext.Text = "toolStripButton1";
            this.tbnNext.ToolTipText = "下一页";
            this.tbnNext.Click += new System.EventHandler(this.tbnNext_Click);
            this.cB_pageSize.Items.AddRange(new object[] { "全部", "50", "100", "200" });
            this.cB_pageSize.Name = "cB_pageSize";
            this.cB_pageSize.Size = new System.Drawing.Size(0x4b, 0x19);
            this.cB_pageSize.Text = "全部";
            this.cB_pageSize.SelectedIndexChanged += new System.EventHandler(this.cB_pageSize_SelectedIndexChanged);
            this.tbn_split2.Name = "tbn_split2";
            this.tbn_split2.Size = new System.Drawing.Size(6, 0x19);
            this.btn_upStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_upStep.Enabled = false;
            this.btn_upStep.Image = (System.Drawing.Image)manager.GetObject("btn_upStep.Image");
            this.btn_upStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_upStep.Name = "btn_upStep";
            this.btn_upStep.Size = new System.Drawing.Size(0x17, 0x16);
            this.btn_upStep.ToolTipText = "回退";
            this.btn_upStep.Click += new System.EventHandler(this.btn_upStep_Click);
            this.btn_downStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_downStep.Enabled = false;
            this.btn_downStep.Image = (System.Drawing.Image)manager.GetObject("btn_downStep.Image");
            this.btn_downStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_downStep.Name = "btn_downStep";
            this.btn_downStep.Size = new System.Drawing.Size(0x17, 0x16);
            this.btn_downStep.ToolTipText = "前进";
            this.btn_downStep.Click += new System.EventHandler(this.btn_downStep_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 0x19);
            this.btn_viewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_viewFile.Enabled = false;
            this.btn_viewFile.Image = (System.Drawing.Image)manager.GetObject("btn_viewFile.Image");
            this.btn_viewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_viewFile.Name = "btn_viewFile";
            this.btn_viewFile.Size = new System.Drawing.Size(0x17, 0x16);
            this.btn_viewFile.Text = "显示源文件";
            this.btn_viewFile.Click += new System.EventHandler(this.btn_viewFile_Click);
            this.btn_Float.Checked = true;
            this.btn_Float.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btn_Float.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Float.Enabled = false;
            this.btn_Float.Image = (System.Drawing.Image)manager.GetObject("btn_Float.Image");
            this.btn_Float.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Float.Name = "btn_Float";
            this.btn_Float.Size = new System.Drawing.Size(0x17, 0x16);
            this.btn_Float.Text = "源文件浮动窗口显示(浮动/分屏)";
            this.btn_Float.Click += new System.EventHandler(this.btn_Float_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 0x19);
            this.tBnOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tBnOutput.Image = Thyt.TiPLM.UIL.Common.UserControl.Properties.Resources.ICO_SCH_EXPORTEXCEL;
            this.tBnOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tBnOutput.Name = "tBnOutput";
            this.tBnOutput.Size = new System.Drawing.Size(0x17, 0x16);
            this.tBnOutput.Text = "导出";
            this.tBnOutput.Click += new System.EventHandler(this.tBnOutput_Click);
            this.pnlPicker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPicker.Location = new System.Drawing.Point(0, 0);
            this.pnlPicker.Name = "pnlPicker";
            this.pnlPicker.Size = new System.Drawing.Size(0x188, 0x19);
            this.pnlPicker.TabIndex = 4;
            this.pnlLvw.Controls.Add(this.lvw);
            this.pnlLvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLvw.Location = new System.Drawing.Point(3, 0x11);
            this.pnlLvw.Name = "pnlLvw";
            this.pnlLvw.Size = new System.Drawing.Size(0x182, 0x175);
            this.pnlLvw.TabIndex = 0;
            this.lvw.AllowDrop = true;
            this.lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvw.FullRowSelect = true;
            this.lvw.GridLines = true;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(0, 0);
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(0x182, 0x175);
            this.lvw.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvw.TabIndex = 5;
            this.lvw.ThumPSOption = null;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            this.lvw.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvw_ItemDrag);
            this.lvw.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvw_DragEnter);
            this.lvw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvw_MouseDown);
            this.lvw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvw_MouseUp);
            base.Controls.Add(this.pnlAll);
            base.Controls.Add(this.pnlPicker);
            base.Name = "UCResTree";
            base.Size = new System.Drawing.Size(0x188, 0x223);
            base.Load += new System.EventHandler(this.UCResTree_Load);
            this.pnlAll.ResumeLayout(false);
            this.pnlAll.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.EndInit();
            this.splitContainer.ResumeLayout(false);
            this.gbData.ResumeLayout(false);
            this.tb.ResumeLayout(false);
            this.tb.PerformLayout();
            this.pnlLvw.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion      
        private System.Windows.Forms.ToolStripButton btn_downStep;
        private System.Windows.Forms.ToolStripButton btn_Float;
        private System.Windows.Forms.ToolStripButton btn_upStep;
        private System.Windows.Forms.ToolStripButton btn_viewFile;
        private System.Windows.Forms.ToolStripComboBox cB_pageSize;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor dropDownPicker;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbData;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbFilter;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlAll;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlLvw;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlPicker;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}