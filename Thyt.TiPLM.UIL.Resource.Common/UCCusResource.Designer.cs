namespace Thyt.TiPLM.UIL.Resource.Common {
    partial class UCCusResource {
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
            this.pnlFilter = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.gbFilter = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.spl = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.pnlData = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.gbData = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lvw = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.tb = new System.Windows.Forms.ToolBar();
            this.tbnPre = new System.Windows.Forms.ToolBarButton();
            this.tbnNext = new System.Windows.Forms.ToolBarButton();
            this.tbnClear = new System.Windows.Forms.ToolBarButton();
            this.tbnRefresh = new System.Windows.Forms.ToolBarButton();
            this.pnlFilter.SuspendLayout();
            this.gbFilter.BeginInit();
            this.pnlData.SuspendLayout();
            this.gbData.BeginInit();
            this.gbData.SuspendLayout();
            base.SuspendLayout();
            this.pnlFilter.Controls.Add(this.gbFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(0x178, 80);
            this.pnlFilter.TabIndex = 0;
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFilter.Location = new System.Drawing.Point(2, 2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(0x174, 0x4c);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.Text = "过滤条件：";
            this.spl.Dock = System.Windows.Forms.DockStyle.Top;
            this.spl.Location = new System.Drawing.Point(0, 80);
            this.spl.Name = "spl";
            this.spl.Size = new System.Drawing.Size(0x178, 5);
            this.spl.TabIndex = 1;
            this.spl.TabStop = false;
            this.pnlData.Controls.Add(this.gbData);
            this.pnlData.Controls.Add(this.tb);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(0, 0x55);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(0x178, 0x133);
            this.pnlData.TabIndex = 2;
            this.gbData.Controls.Add(this.lvw);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Location = new System.Drawing.Point(2, 30);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(0x174, 0x113);
            this.gbData.TabIndex = 1;
            this.gbData.Text = "资源数据：";
            this.lvw.AllowDrop = true;
            this.lvw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvw.FullRowSelect = true;
            this.lvw.GridLines = true;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(2, 0x16);
            this.lvw.MultiSelect = false;
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(0x170, 0xfb);
            this.lvw.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvw.TabIndex = 0;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            this.lvw.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvw_ItemDrag);
            this.tb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.tbnPre, this.tbnNext, this.tbnClear, this.tbnRefresh });
            this.tb.DropDownArrows = true;
            this.tb.Location = new System.Drawing.Point(2, 2);
            this.tb.Name = "tb";
            this.tb.ShowToolTips = true;
            this.tb.Size = new System.Drawing.Size(0x174, 0x1c);
            this.tb.TabIndex = 0;
            this.tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tb_ButtonClick);
            this.tbnPre.ImageIndex = 0;
            this.tbnPre.Name = "tbnPre";
            this.tbnPre.ToolTipText = "上一页";
            this.tbnNext.ImageIndex = 1;
            this.tbnNext.Name = "tbnNext";
            this.tbnNext.ToolTipText = "下一页";
            this.tbnClear.ImageIndex = 2;
            this.tbnClear.Name = "tbnClear";
            this.tbnClear.ToolTipText = "清空过滤条件";
            this.tbnRefresh.Name = "tbnRefresh";
            this.tbnRefresh.ToolTipText = "刷新数据";
            base.Controls.Add(this.pnlData);
            base.Controls.Add(this.spl);
            base.Controls.Add(this.pnlFilter);
            base.Name = "UCCusResource";
            base.Size = new System.Drawing.Size(0x178, 0x188);
            base.Load += new System.EventHandler(this.UCCusResource_Load);
            this.pnlFilter.ResumeLayout(false);
            this.gbFilter.EndInit();
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.gbData.EndInit();
            this.gbData.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbData;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbFilter;
        private Thyt.TiPLM.UIL.Common.SortableListView lvw;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlData;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlFilter;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM spl;
        private System.Windows.Forms.ToolBar tb;
        private System.Windows.Forms.ToolBarButton tbnClear;
        private System.Windows.Forms.ToolBarButton tbnNext;
        private System.Windows.Forms.ToolBarButton tbnPre;
        private System.Windows.Forms.ToolBarButton tbnRefresh;
    }
}