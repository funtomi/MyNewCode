namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCThumbnail {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCThumbnail));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tspBtnFirst = new System.Windows.Forms.ToolStripButton();
            this.tspbtnPreview = new System.Windows.Forms.ToolStripButton();
            this.tspBtnNext = new System.Windows.Forms.ToolStripButton();
            this.tspBtnLast = new System.Windows.Forms.ToolStripButton();
            this.tspLabelPage = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenObject = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tspBtnShowType = new System.Windows.Forms.ToolStripButton();
            this.pnlViewer = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.toolStrip1.SuspendLayout();
            base.SuspendLayout();
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.tspBtnFirst, this.tspbtnPreview, this.tspBtnNext, this.tspBtnLast, this.tspLabelPage, this.toolStripSeparator2, this.btnOpenObject, this.toolStripSeparator1, this.tspBtnShowType });
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(0x17e, 0x19);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.tspBtnFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnFirst.Image = (System.Drawing.Image)resources.GetObject("tspBtnFirst.Image");
            this.tspBtnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnFirst.Name = "tspBtnFirst";
            this.tspBtnFirst.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnFirst.Text = "第一张";
            this.tspBtnFirst.Click += new System.EventHandler(this.tspBtnFirst_Click);
            this.tspbtnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspbtnPreview.Image = (System.Drawing.Image)resources.GetObject("tspbtnPreview.Image");
            this.tspbtnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspbtnPreview.Name = "tspbtnPreview";
            this.tspbtnPreview.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspbtnPreview.Text = "上一张";
            this.tspbtnPreview.Click += new System.EventHandler(this.tspbtnPreview_Click);
            this.tspBtnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnNext.Image = (System.Drawing.Image)resources.GetObject("tspBtnNext.Image");
            this.tspBtnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnNext.Name = "tspBtnNext";
            this.tspBtnNext.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnNext.Text = "下一张";
            this.tspBtnNext.Click += new System.EventHandler(this.tspBtnNext_Click);
            this.tspBtnLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tspBtnLast.Image = (System.Drawing.Image)resources.GetObject("tspBtnLast.Image");
            this.tspBtnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnLast.Name = "tspBtnLast";
            this.tspBtnLast.Size = new System.Drawing.Size(0x17, 0x16);
            this.tspBtnLast.Text = "最后一张";
            this.tspBtnLast.Click += new System.EventHandler(this.tspBtnLast_Click);
            this.tspLabelPage.Name = "tspLabelPage";
            this.tspLabelPage.Size = new System.Drawing.Size(0, 0x16);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 0x19);
            this.btnOpenObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpenObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenObject.Name = "btnOpenObject";
            this.btnOpenObject.Size = new System.Drawing.Size(60, 0x16);
            this.btnOpenObject.Text = "打开对象";
            this.btnOpenObject.Click += new System.EventHandler(this.btnOpenObject_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 0x19);
            this.tspBtnShowType.Checked = true;
            this.tspBtnShowType.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tspBtnShowType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tspBtnShowType.Image = (System.Drawing.Image)resources.GetObject("tspBtnShowType.Image");
            this.tspBtnShowType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspBtnShowType.Name = "tspBtnShowType";
            this.tspBtnShowType.Size = new System.Drawing.Size(0x30, 0x16);
            this.tspBtnShowType.Text = "缩略图";
            this.tspBtnShowType.Click += new System.EventHandler(this.tspBtnShowType_Click);
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(0, 0x19);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(0x17e, 0x125);
            this.pnlViewer.TabIndex = 1;
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            base.Controls.Add(this.pnlViewer);
            base.Controls.Add(this.toolStrip1);
            base.Name = "UCThumbnail";
            base.Size = new System.Drawing.Size(0x17e, 0x13e);
            base.Resize += new System.EventHandler(this.UCThumbnail_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion  
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlViewer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tspBtnFirst;
        private System.Windows.Forms.ToolStripButton tspBtnLast;
        private System.Windows.Forms.ToolStripButton tspBtnNext;
        private System.Windows.Forms.ToolStripButton tspbtnPreview;
        private System.Windows.Forms.ToolStripButton tspBtnShowType;
        private System.Windows.Forms.ToolStripLabel tspLabelPage;
    }
}