namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCSelectProcessTemplate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>

        protected override void Dispose(bool disposing) {
            if (this.dlHandler != null) {
                this.ucProcess.processTemplateSelected -= this.dlHandler;
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
            Infragistics.Win.UltraWinEditors.DropDownEditorButton button = new Infragistics.Win.UltraWinEditors.DropDownEditorButton("SelectProcessTemplate") {
                RightAlignDropDown = Infragistics.Win.DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "(无)";
            base.ReadOnly = true;
            base.Size = new System.Drawing.Size(100, 0x15);
        }

        #endregion  
  
    }
}