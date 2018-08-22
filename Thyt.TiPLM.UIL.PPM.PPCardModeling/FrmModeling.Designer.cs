namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class FrmModeling {
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FrmModeling));
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x268, 0x18e);
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.Name = "FrmModeling";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "卡片模板";
            base.Closing += new System.ComponentModel.CancelEventHandler(this.FrmPPCModeling_Closing);
            base.Closed += new System.EventHandler(this.FrmModeling_Closed);
            base.Enter += new System.EventHandler(this.FrmPPCModeling_Enter);
        }
        #endregion 

    }
}