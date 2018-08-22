namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCAttrTest {
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
            this.chkBox_Test = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            base.SuspendLayout();
            this.chkBox_Test.Location = new System.Drawing.Point(0x10, 8);
            this.chkBox_Test.Name = "chkBox_Test";
            this.chkBox_Test.TabIndex = 0;
            this.chkBox_Test.Text = "必须检查";
            base.Controls.Add(this.chkBox_Test);
            base.Name = "UCAttrTest";
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkBox_Test;

    }
}