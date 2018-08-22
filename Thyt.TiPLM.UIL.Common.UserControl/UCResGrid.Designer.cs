namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCResGrid {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCResGrid));
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.TV_Class = new System.Windows.Forms.TreeView();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.lvw = new SortableListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnHide = new System.Windows.Forms.ToolStripButton();
            this.btnSort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowMPage = new System.Windows.Forms.ToolStripButton();
            this.btnPrevPage = new System.Windows.Forms.ToolStripButton();
            this.btnNextPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cobPageSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.txtKeyword = new System.Windows.Forms.ToolStripTextBox();
            this.stBar_PageInfo = new System.Windows.Forms.StatusBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.TV_Class);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 0x1ed);
            this.panel1.TabIndex = 0;
            this.TV_Class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Class.Location = new System.Drawing.Point(0, 0);
            this.TV_Class.Name = "TV_Class";
            this.TV_Class.Size = new System.Drawing.Size(0xf8, 0x1eb);
            this.TV_Class.TabIndex = 0;
            this.TV_Class.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Class_AfterSelect);
            this.splitter1.Location = new System.Drawing.Point(250, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 0x1ed);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lvw);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.stBar_PageInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0xfc, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x21a, 0x1ed);
            this.panel2.TabIndex = 2;
            this.lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvw.FullRowSelect = true;
            this.lvw.GridLines = true;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(0, 0x19);
            this.lvw.Margin = new System.Windows.Forms.Padding(1);
            this.lvw.MultiSelect = false;
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(0x218, 0x1bc);
            this.lvw.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvw.TabIndex = 6;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            this.lvw.ItemActivate += new System.EventHandler(this.lvw_ItemActivate);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.btnHide, this.btnSort, this.toolStripSeparator1, this.btnShowMPage, this.btnPrevPage, this.btnNextPage, this.toolStripSeparator2, this.cobPageSize, this.toolStripSeparator3, this.txtKeyword });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(0x218, 0x19);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            this.btnHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHide.Image = (System.Drawing.Image)resources.GetObject("btnHide.Image");
            this.btnHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnHide.Text = "显示/隐藏导航树";
            this.btnHide.ToolTipText = "显示/隐藏导航树";
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            this.btnSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSort.Image = (System.Drawing.Image)resources.GetObject("btnSort.Image");
            this.btnSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnSort.Text = "排序";
            this.btnSort.ToolTipText = "设置排序列";
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 0x19);
            this.btnShowMPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowMPage.Image = (System.Drawing.Image)resources.GetObject("btnShowMPage.Image");
            this.btnShowMPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowMPage.Name = "btnShowMPage";
            this.btnShowMPage.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnShowMPage.Text = "隐藏分页显示";
            this.btnShowMPage.Click += new System.EventHandler(this.btnShowMPage_Click);
            this.btnPrevPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrevPage.Image = (System.Drawing.Image)resources.GetObject("btnPrevPage.Image");
            this.btnPrevPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnPrevPage.Text = "上一页";
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            this.btnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextPage.Image = (System.Drawing.Image)resources.GetObject("btnNextPage.Image");
            this.btnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(0x17, 0x16);
            this.btnNextPage.Text = "下一页";
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 0x19);
            this.cobPageSize.Items.AddRange(new object[] { "全部", "50", "100", "200" });
            this.cobPageSize.Name = "cobPageSize";
            this.cobPageSize.Size = new System.Drawing.Size(80, 0x19);
            this.cobPageSize.Text = "全部";
            this.cobPageSize.SelectedIndexChanged += new System.EventHandler(this.cobPageSize_SelectedIndexChanged);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 0x19);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(80, 0x19);
            this.txtKeyword.ToolTipText = "输入要搜索的关键字，按回车进行查询";
            this.txtKeyword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtKeyword_KeyPress);
            this.stBar_PageInfo.Location = new System.Drawing.Point(0, 0x1d5);
            this.stBar_PageInfo.Name = "stBar_PageInfo";
            this.stBar_PageInfo.Size = new System.Drawing.Size(0x218, 0x16);
            this.stBar_PageInfo.TabIndex = 5;
            this.stBar_PageInfo.Text = "1/5页，共500条记录";
            this.stBar_PageInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.stBar_PageInfo_MouseMove);
            this.stBar_PageInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stBar_PageInfo_MouseDown);
            this.stBar_PageInfo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stBar_PageInfo_MouseUp);
            this.AllowDrop = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.panel1);
            base.Name = "UCResGrid";
            base.Size = new System.Drawing.Size(790, 0x1ed);
            base.Enter += new System.EventHandler(this.UCRes_Enter);
            base.SizeChanged += new System.EventHandler(this.UCResGrid_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion   
        private System.Windows.Forms.ToolStripButton btnHide;
        private System.Windows.Forms.ToolStripButton btnNextPage;
        private System.Windows.Forms.ToolStripButton btnPrevPage;
        private System.Windows.Forms.ToolStripButton btnShowMPage;
        private System.Windows.Forms.ToolStripButton btnSort;
        private System.Windows.Forms.ToolStripComboBox cobPageSize;
        private SortableListView lvw;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.StatusBar stBar_PageInfo;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.TreeView TV_Class;
        private System.Windows.Forms.ToolStripTextBox txtKeyword;
    }
}