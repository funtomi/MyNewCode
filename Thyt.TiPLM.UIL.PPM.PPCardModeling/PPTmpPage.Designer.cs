namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class PPTmpPage {
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
            if (this.axSpreadsheet1 != null) {
                this.axSpreadsheet1.Dispose();
                this.axSpreadsheet1 = null;
            }
            System.GC.SuppressFinalize(this);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 
        ///   
        private void InitializeComponent() {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PPTmpPage));
            this.axSpreadsheet1 = new AxOWC.AxSpreadsheet();
            this.axSpreadsheet1.BeginInit();
            base.SuspendLayout();
            this.axSpreadsheet1.AccessibleDescription = resources.GetString("axSpreadsheet1.AccessibleDescription");
            this.axSpreadsheet1.AccessibleName = resources.GetString("axSpreadsheet1.AccessibleName");
            this.axSpreadsheet1.Anchor = (System.Windows.Forms.AnchorStyles)resources.GetObject("axSpreadsheet1.Anchor");
            this.axSpreadsheet1.BackgroundImage = (System.Drawing.Image)resources.GetObject("axSpreadsheet1.BackgroundImage");
            this.axSpreadsheet1.Dock = (System.Windows.Forms.DockStyle)resources.GetObject("axSpreadsheet1.Dock");
            this.axSpreadsheet1.Enabled = (bool)resources.GetObject("axSpreadsheet1.Enabled");
            this.axSpreadsheet1.Font = (System.Drawing.Font)resources.GetObject("axSpreadsheet1.Font");
            this.axSpreadsheet1.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("axSpreadsheet1.ImeMode");
            this.axSpreadsheet1.Location = (System.Drawing.Point)resources.GetObject("axSpreadsheet1.Location");
            this.axSpreadsheet1.Name = "axSpreadsheet1";
            this.axSpreadsheet1.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("axSpreadsheet1.OcxState");
            this.axSpreadsheet1.RightToLeft = (bool)resources.GetObject("axSpreadsheet1.RightToLeft");
            this.axSpreadsheet1.Size = (System.Drawing.Size)resources.GetObject("axSpreadsheet1.Size");
            this.axSpreadsheet1.TabIndex = (int)resources.GetObject("axSpreadsheet1.TabIndex");
            this.axSpreadsheet1.Text = resources.GetString("axSpreadsheet1.Text");
            this.axSpreadsheet1.Visible = (bool)resources.GetObject("axSpreadsheet1.Visible");
            this.axSpreadsheet1.DblClick += new AxOWC.IWebCalcEventSink_DblClickEventHandler(this.axSpreadsheet1_DblClick);
            this.axSpreadsheet1.StartEdit += new AxOWC.IWebCalcEventSink_StartEditEventHandler(this.axSpreadsheet1_StartEdit);
            base.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
            base.AccessibleName = resources.GetString("$this.AccessibleName");
            this.AutoScroll = (bool)resources.GetObject("$this.AutoScroll");
            base.AutoScrollMargin = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMargin");
            base.AutoScrollMinSize = (System.Drawing.Size)resources.GetObject("$this.AutoScrollMinSize");
            this.BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            base.Controls.Add(this.axSpreadsheet1);
            base.Enabled = (bool)resources.GetObject("$this.Enabled");
            this.Font = (System.Drawing.Font)resources.GetObject("$this.Font");
            base.ImeMode = (System.Windows.Forms.ImeMode)resources.GetObject("$this.ImeMode");
            base.Location = (System.Drawing.Point)resources.GetObject("$this.Location");
            base.Name = "PPTmpPage";
            this.RightToLeft = (System.Windows.Forms.RightToLeft)resources.GetObject("$this.RightToLeft");
            base.Size = (System.Drawing.Size)resources.GetObject("$this.Size");
            this.axSpreadsheet1.EndInit();
            base.ResumeLayout(false);
        }

        #endregion    
        private AxOWC.AxSpreadsheet axSpreadsheet1;

    }
}