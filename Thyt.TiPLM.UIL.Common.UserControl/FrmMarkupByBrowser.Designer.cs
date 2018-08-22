namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmMarkupByBrowser {
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
            this.btnOK = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel3 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.pnlBrowser = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.panel4 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.txtMark = new Thyt.TiPLM.UIL.Controls.MemoEditPLM();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBrowser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 52);
            this.panel1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOK.Location = new System.Drawing.Point(197, 11);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCancel.Location = new System.Drawing.Point(315, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlBrowser);
            this.panel3.Controls.Add(this.splitter1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(432, 297);
            this.panel3.TabIndex = 2;
            // 
            // pnlBrowser
            // 
            this.pnlBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrowser.Location = new System.Drawing.Point(0, 0);
            this.pnlBrowser.Name = "pnlBrowser";
            this.pnlBrowser.Size = new System.Drawing.Size(432, 154);
            this.pnlBrowser.TabIndex = 2;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitter1.Location = new System.Drawing.Point(0, 154);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(432, 5);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtMark);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 159);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(432, 138);
            this.panel4.TabIndex = 0;
            // 
            // txtMark
            // 
            this.txtMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMark.Location = new System.Drawing.Point(0, 0);
            this.txtMark.Name = "txtMark";
            this.txtMark.Properties.MaxLength = 2000;
            this.txtMark.Size = new System.Drawing.Size(432, 138);
            this.txtMark.TabIndex = 0;
            this.txtMark.UseOptimizedRendering = true;
            this.txtMark.TextChanged += new System.EventHandler(this.txtMark_TextChanged);
            // 
            // FrmMarkupByBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(432, 349);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "FrmMarkupByBrowser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批注";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmMarkupByBrowser_Closing);
            this.Load += new System.EventHandler(this.FrmMarkupByBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel3)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBrowser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel4)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMark.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel3;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel4;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlBrowser;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private Thyt.TiPLM.UIL.Controls.MemoEditPLM txtMark;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOK;
    }
}