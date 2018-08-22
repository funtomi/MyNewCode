namespace Thyt.TiPLM.UIL.Resource.Common {
    partial class FrmGetCode {
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmGetCode));
            this.ecms = new AxtiEcmsControll.AxecmsControll();
            this.ecms.BeginInit();
            base.SuspendLayout();
            this.ecms.Enabled = true;
            this.ecms.Location = new System.Drawing.Point(0x20, 0x18);
            this.ecms.Name = "ecms";
            this.ecms.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("ecms.OcxState");
            this.ecms.Size = new System.Drawing.Size(320, 240);
            this.ecms.TabIndex = 0;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(360, 0x10d);
            base.Controls.Add(this.ecms);
            base.Name = "FrmGetCode";
            this.Text = "FrmGetCode";
            base.Load += new System.EventHandler(this.FrmGetCode_Load);
            this.ecms.EndInit();
            base.ResumeLayout(false);
        }
        #endregion    
        private AxtiEcmsControll.AxecmsControll ecms;

    }
}