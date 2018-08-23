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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPTmpPage));
            this.axSpreadsheet1 = new AxOWC.AxSpreadsheet();
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // axSpreadsheet1
            // 
            resources.ApplyResources(this.axSpreadsheet1, "axSpreadsheet1");
            this.axSpreadsheet1.Name = "axSpreadsheet1";
            this.axSpreadsheet1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axSpreadsheet1.OcxState")));
            this.axSpreadsheet1.StartEdit += new AxOWC.IWebCalcEventSink_StartEditEventHandler(this.axSpreadsheet1_StartEdit);
            this.axSpreadsheet1.DblClick += new AxOWC.IWebCalcEventSink_DblClickEventHandler(this.axSpreadsheet1_DblClick);
            // 
            // PPTmpPage
            // 
            this.Controls.Add(this.axSpreadsheet1);
            this.Name = "PPTmpPage";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.axSpreadsheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion    
        private AxOWC.AxSpreadsheet axSpreadsheet1;

    }
}