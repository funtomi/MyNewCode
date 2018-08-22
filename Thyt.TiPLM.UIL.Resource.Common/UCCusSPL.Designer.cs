namespace Thyt.TiPLM.UIL.Resource.Common {
    partial class UCCusSPL {
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
            this.lblSPL = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.dropDownTree = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            this.pnlAll = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.pnlData = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.gbData = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.lvw = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.tb = new System.Windows.Forms.ToolBar();
            this.tbnPre = new System.Windows.Forms.ToolBarButton();
            this.tbnNext = new System.Windows.Forms.ToolBarButton();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.gbFilter = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.pnlFilter = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.txtValue = new Thyt.TiPLM.UIL.Controls.TextEditPLM();
            this.cob = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.lblID = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.pnlAll.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.gbData.BeginInit();
            this.gbData.SuspendLayout();
            this.gbFilter.BeginInit();
            this.gbFilter.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.txtValue.Properties.BeginInit();
            this.cob.Properties.BeginInit();
            base.SuspendLayout();
            this.lblSPL.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSPL.Location = new System.Drawing.Point(0, 0);
            this.lblSPL.Name = "lblSPL";
            this.lblSPL.Size = new System.Drawing.Size(40, 14);
            this.lblSPL.TabIndex = 0;
            this.lblSPL.Text = "选择类:";
            this.dropDownTree.Dock = System.Windows.Forms.DockStyle.Top;
            this.dropDownTree.DropDown = true;
            this.dropDownTree.Imagelist = null;
            this.dropDownTree.IsAollowUseParentNode = false;
            this.dropDownTree.Location = new System.Drawing.Point(0, 14);
            this.dropDownTree.Name = "dropDownTree";
            this.dropDownTree.SelectedNode = null;
            this.dropDownTree.Size = new System.Drawing.Size(0x188, 0x15);
            this.dropDownTree.TabIndex = 1;
            this.dropDownTree.TextReadOnly = true;
            this.dropDownTree.TextValue = "";
            this.pnlAll.Controls.Add(this.pnlData);
            this.pnlAll.Controls.Add(this.splitter1);
            this.pnlAll.Controls.Add(this.gbFilter);
            this.pnlAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAll.Location = new System.Drawing.Point(0, 0x23);
            this.pnlAll.Name = "pnlAll";
            this.pnlAll.Size = new System.Drawing.Size(0x188, 0x185);
            this.pnlAll.TabIndex = 2;
            this.pnlData.Controls.Add(this.gbData);
            this.pnlData.Controls.Add(this.tb);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(2, 0x40);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(0x184, 0x143);
            this.pnlData.TabIndex = 2;
            this.gbData.Controls.Add(this.lvw);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Location = new System.Drawing.Point(2, 30);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(0x180, 0x123);
            this.gbData.TabIndex = 1;
            this.gbData.Text = "资源数据:";
            this.lvw.AllowDrop = true;
            this.lvw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvw.FullRowSelect = true;
            this.lvw.GridLines = true;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(2, 0x16);
            this.lvw.MultiSelect = false;
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(380, 0x10b);
            this.lvw.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvw.TabIndex = 0;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            this.lvw.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvw_ItemDrag);
            this.tb.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.tbnPre, this.tbnNext });
            this.tb.DropDownArrows = true;
            this.tb.Location = new System.Drawing.Point(2, 2);
            this.tb.Name = "tb";
            this.tb.ShowToolTips = true;
            this.tb.Size = new System.Drawing.Size(0x180, 0x1c);
            this.tb.TabIndex = 0;
            this.tb.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tb_ButtonClick);
            this.tbnPre.Name = "tbnPre";
            this.tbnPre.ToolTipText = "上一页";
            this.tbnNext.Name = "tbnNext";
            this.tbnNext.ToolTipText = "下一页";
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(2, 0x3a);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(0x184, 6);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.gbFilter.Controls.Add(this.pnlFilter);
            this.gbFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFilter.Location = new System.Drawing.Point(2, 2);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(0x184, 0x38);
            this.gbFilter.TabIndex = 0;
            this.gbFilter.Text = "过滤条件:";
            this.pnlFilter.Controls.Add(this.txtValue);
            this.pnlFilter.Controls.Add(this.cob);
            this.pnlFilter.Controls.Add(this.lblID);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilter.Location = new System.Drawing.Point(2, 0x16);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(0x180, 0x20);
            this.pnlFilter.TabIndex = 0;
            this.txtValue.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            this.txtValue.Location = new System.Drawing.Point(0xa5, 8);
            this.txtValue.Name = "txtValue";
            this.txtValue.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.txtValue.Properties.Appearance.Options.UseBackColor = true;
            this.txtValue.Size = new System.Drawing.Size(0xd8, 0x16);
            this.txtValue.TabIndex = 2;
            this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
            this.cob.Location = new System.Drawing.Point(70, 8);
            this.cob.Name = "cob";
            this.cob.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.cob.Properties.Appearance.Options.UseBackColor = true;
            this.cob.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cob.Properties.Items.AddRange(new object[] { "等于", "不等于", "前几字符是", "后几字符是", "包含" });
            this.cob.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cob.Size = new System.Drawing.Size(90, 0x16);
            this.cob.TabIndex = 1;
            this.cob.SelectedIndexChanged += new System.EventHandler(this.comb_SelectedIndexChanged);
            this.lblID.Location = new System.Drawing.Point(5, 8);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0x34, 14);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "资源代号:";
            base.Controls.Add(this.pnlAll);
            base.Controls.Add(this.dropDownTree);
            base.Controls.Add(this.lblSPL);
            base.Name = "UCCusSPL";
            base.Size = new System.Drawing.Size(0x188, 0x1a8);
            base.Load += new System.EventHandler(this.UCCusSPL_Load);
            this.pnlAll.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.gbData.EndInit();
            this.gbData.ResumeLayout(false);
            this.gbFilter.EndInit();
            this.gbFilter.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.txtValue.Properties.EndInit();
            this.cob.Properties.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cob;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbData;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbFilter;
        private Thyt.TiPLM.UIL.Controls.LabelPLM lblID;
        private Thyt.TiPLM.UIL.Controls.LabelPLM lblSPL;
        private Thyt.TiPLM.UIL.Common.SortableListView lvw;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlAll;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlData;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlFilter;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.ToolBar tb;
        private System.Windows.Forms.ToolBarButton tbnNext;
        private System.Windows.Forms.ToolBarButton tbnPre;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM txtValue;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView dropDownTree;


    }
}