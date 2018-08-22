namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class ThumToolsBaseCtrl {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            if (this.ucThumbnail != null) {
                this.ucThumbnail.Dispose();
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThumToolsBaseCtrl));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.tspBtnSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspBtnPreview = new System.Windows.Forms.ToolStripButton();
            this.tspBtnDisplay = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.panMain = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.panLeft = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.panRight = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.toolBar.SuspendLayout();
            this.panMain.SuspendLayout();
            base.SuspendLayout();
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tspBtnSetting, this.toolStripSeparator1, this.tspBtnPreview, this.tspBtnDisplay });
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(200, 0x19);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            this.tspBtnSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspBtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnSetting.Image = (System.Drawing.Image)resources.GetObject("tspBtnSetting.Image");
            this.tspBtnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnSetting.Name = "tspBtnSetting";
            this.tspBtnSetting.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnSetting.Text = "设置";
            this.tspBtnSetting.Visible = false;
            this.tspBtnSetting.Click += new System.EventHandler(this.tspBtnSetting_Click);
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 0x19);
            this.toolStripSeparator1.Visible = false;
            this.tspBtnPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspBtnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnPreview.Image = (System.Drawing.Image)resources.GetObject("tspBtnPreview.Image");
            this.tspBtnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnPreview.Name = "tspBtnPreview";
            this.tspBtnPreview.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnPreview.Text = "预览";
            this.tspBtnPreview.Click += new System.EventHandler(this.tspBtnPreview_Click);
            this.tspBtnDisplay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tspBtnDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnDisplay.Image = (System.Drawing.Image)resources.GetObject("tspBtnDisplay.Image");
            this.tspBtnDisplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnDisplay.Name = "tspBtnDisplay";
            this.tspBtnDisplay.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnDisplay.Text = "显示方式";
            this.tspBtnDisplay.Click += new System.EventHandler(this.tspBtnDisplay_Click);
            this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "detail.jpg");
            this.imageList1.Images.SetKeyName(1, "icon.jpg");
            this.panMain.Controls.Add(this.panLeft);
            this.panMain.Controls.Add(this.splitter1);
            this.panMain.Controls.Add(this.panRight);
            this.panMain.Controls.Add(this.toolBar);
            this.panMain.Location = new System.Drawing.Point(3, 3);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(200, 100);
            this.panMain.TabIndex = 1;
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLeft.Location = new System.Drawing.Point(0, 0x19);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(0x77, 0x4b);
            this.panLeft.TabIndex = 0;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(0x77, 0x19);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 0x4b);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.panRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panRight.Location = new System.Drawing.Point(0x7a, 0x19);
            this.panRight.Name = "panRight";
            this.panRight.Size = new System.Drawing.Size(0x4e, 0x4b);
            this.panRight.TabIndex = 2;
            this.panRight.Resize += new System.EventHandler(this.panRight_Resize);
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.panMain);
            base.Name = "ThumToolsBaseCtrl";
            base.Size = new System.Drawing.Size(0xfe, 120);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion    
        private System.Windows.Forms.ImageList imageList1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panLeft;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panMain;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panRight;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.ToolStrip toolBar;
        private ToolStripCtrl toolsItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tspBtnDisplay;
        private System.Windows.Forms.ToolStripButton tspBtnPreview;
        private System.Windows.Forms.ToolStripButton tspBtnSetting;

    }
}