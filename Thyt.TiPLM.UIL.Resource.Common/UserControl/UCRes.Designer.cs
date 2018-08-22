namespace Thyt.TiPLM.UIL.Resource.Common.UserControl {
    partial class UCRes {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCRes));
            this.btnFilter = new System.Windows.Forms.ToolBarButton();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.btnFresh = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.panel4 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.uGrid_Res = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.panel3 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.uTE_opervalue = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lstBox_Text = new Thyt.TiPLM.UIL.Controls.ListBoxPLM();
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_clear = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_del = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_add = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.cmbBox_relation = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.cmBox_oper = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.cmBox_fname = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.lstBox_value = new Thyt.TiPLM.UIL.Controls.ListBoxPLM();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.uGrid_Res).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.lstBox_Text).BeginInit();
            this.cmbBox_relation.Properties.BeginInit();
            this.cmBox_oper.Properties.BeginInit();
            this.cmBox_oper.Properties.BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.lstBox_value).BeginInit();
            base.SuspendLayout();
            this.btnFilter.ImageIndex = 2;
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnFilter.Text = "滤";
            this.btnFilter.ToolTipText = "过滤设置";
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.btnFilter, this.btnFresh });
            this.toolBar1.ButtonSize = new System.Drawing.Size(0x17, 0x16);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(0x164, 0x2b);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            this.btnFresh.ImageIndex = 1;
            this.btnFresh.Name = "btnFresh";
            this.btnFresh.Text = "刷新";
            this.btnFresh.ToolTipText = "刷新资源数据";
            this.btnFresh.Visible = false;
            this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.uGrid_Res);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0x2b);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x164, 0x9c);
            this.panel1.TabIndex = 3;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0x158, 0x18);
            this.panel4.TabIndex = 3;
            this.uGrid_Res.Location = new System.Drawing.Point(0, 9);
            this.uGrid_Res.Name = "uGrid_Res";
            this.uGrid_Res.Size = new System.Drawing.Size(0x158, 0x8a);
            this.uGrid_Res.TabIndex = 4;
            this.uGrid_Res.Text = "-";
            this.uGrid_Res.AfterRowActivate += new System.EventHandler(this.uGrid_Res_AfterRowActivate);
            this.uGrid_Res.DoubleClick += new System.EventHandler(this.uGrid_Res_DoubleClick);
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0xc7);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(0x164, 5);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0xcc);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x164, 0x9d);
            this.panel2.TabIndex = 5;
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x160, 0x99);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "过滤设置";
            this.panel3.Controls.Add(this.uTE_opervalue);
            this.panel3.Controls.Add(this.lstBox_Text);
            this.panel3.Controls.Add(this.btn_ok);
            this.panel3.Controls.Add(this.btn_clear);
            this.panel3.Controls.Add(this.btn_del);
            this.panel3.Controls.Add(this.btn_add);
            this.panel3.Controls.Add(this.cmbBox_relation);
            this.panel3.Controls.Add(this.cmBox_oper);
            this.panel3.Controls.Add(this.cmBox_fname);
            this.panel3.Controls.Add(this.lstBox_value);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(2, 0x16);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0x15c, 0x81);
            this.panel3.TabIndex = 0;
            this.uTE_opervalue.AutoSize = true;
            this.uTE_opervalue.Location = new System.Drawing.Point(0x9a, 8);
            this.uTE_opervalue.Name = "uTE_opervalue";
            this.uTE_opervalue.Size = new System.Drawing.Size(0x76, 0x17);
            this.uTE_opervalue.TabIndex = 12;
            this.lstBox_Text.ItemHeight = 0x10;
            this.lstBox_Text.Location = new System.Drawing.Point(0x10, 0x25);
            this.lstBox_Text.Name = "lstBox_Text";
            this.lstBox_Text.Size = new System.Drawing.Size(0x130, 0x2e);
            this.lstBox_Text.TabIndex = 11;
            this.btn_ok.Location = new System.Drawing.Point(0x110, 0x58);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x30, 0x18);
            this.btn_ok.TabIndex = 10;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.btn_clear.Location = new System.Drawing.Point(0xb8, 0x58);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(0x30, 0x18);
            this.btn_clear.TabIndex = 9;
            this.btn_clear.Text = "清空";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            this.btn_del.Location = new System.Drawing.Point(0x60, 0x59);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(0x30, 0x18);
            this.btn_del.TabIndex = 8;
            this.btn_del.Text = "删除";
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            this.btn_add.Appearance.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0x86);
            this.btn_add.Appearance.Options.UseFont = true;
            this.btn_add.Location = new System.Drawing.Point(0x10, 0x59);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(0x30, 0x18);
            this.btn_add.TabIndex = 7;
            this.btn_add.Text = "增加";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            this.cmbBox_relation.Properties.Items.AddRange(new object[] { "并且", "或者" });
            this.cmbBox_relation.Location = new System.Drawing.Point(0x113, 8);
            this.cmbBox_relation.Name = "cmbBox_relation";
            this.cmbBox_relation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cmbBox_relation.Size = new System.Drawing.Size(50, 0x16);
            this.cmbBox_relation.TabIndex = 5;
            this.cmbBox_relation.Text = "并且";
            this.cmBox_oper.Location = new System.Drawing.Point(0x68, 8);
            this.cmBox_oper.Name = "cmBox_oper";
            this.cmBox_oper.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cmBox_oper.Size = new System.Drawing.Size(0x30, 0x16);
            this.cmBox_oper.TabIndex = 4;
            this.cmBox_fname.Location = new System.Drawing.Point(0x10, 8);
            this.cmBox_fname.Name = "cmBox_fname";
            this.cmBox_fname.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.cmBox_fname.Size = new System.Drawing.Size(80, 0x16);
            this.cmBox_fname.TabIndex = 3;
            this.cmBox_fname.SelectedIndexChanged += new System.EventHandler(this.cmBox_fname_SelectedIndexChanged);
            this.lstBox_value.ItemHeight = 0x10;
            this.lstBox_value.Location = new System.Drawing.Point(0xb0, 0x20);
            this.lstBox_value.Name = "lstBox_value";
            this.lstBox_value.Size = new System.Drawing.Size(0x88, 0x2e);
            this.lstBox_value.TabIndex = 13;
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.toolBar1);
            base.Name = "UCRes";
            base.Size = new System.Drawing.Size(0x164, 0x169);
            base.Enter += new System.EventHandler(this.UCRes_Enter);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.uGrid_Res).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.lstBox_Text).EndInit();
            this.cmbBox_relation.Properties.EndInit();
            this.cmBox_oper.Properties.EndInit();
            this.cmBox_oper.Properties.EndInit();
            ((System.ComponentModel.ISupportInitialize)this.lstBox_value).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_add;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_clear;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_del;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private System.Windows.Forms.ToolBarButton btnFilter;
        private System.Windows.Forms.ToolBarButton btnFresh;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cmbBox_relation;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cmBox_fname;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM cmBox_oper;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.ListBoxPLM lstBox_Text;
        private Thyt.TiPLM.UIL.Controls.ListBoxPLM lstBox_value;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel3;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel4;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.ToolBar toolBar1;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM[] txtbox;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor ucValue;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGrid_Res;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor uTE_opervalue;
    }
}