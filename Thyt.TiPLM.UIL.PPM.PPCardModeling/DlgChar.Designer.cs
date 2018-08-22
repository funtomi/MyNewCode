namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgChar {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgChar));
            this.axSpreadsheet1 = new AxOWC.AxSpreadsheet();
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // axSpreadsheet1
            // 
            this.axSpreadsheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axSpreadsheet1.Enabled = true;
            this.axSpreadsheet1.Location = new System.Drawing.Point(0, 0);
            this.axSpreadsheet1.Name = "axSpreadsheet1";
            this.axSpreadsheet1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpreadsheet1.OcxState")));
            this.axSpreadsheet1.Size = new System.Drawing.Size(360, 278);
            this.axSpreadsheet1.TabIndex = 0;
            this.axSpreadsheet1.DblClick += new AxOWC.IWebCalcEventSink_DblClickEventHandler(this.axSpreadsheet1_DblClick);
            // 
            // DlgChar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(360, 278);
            this.Controls.Add(this.axSpreadsheet1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DlgChar";
            this.Text = "特殊字符";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DlgChar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private AxOWC.AxSpreadsheet axSpreadsheet1;

    }
}