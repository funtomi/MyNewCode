namespace Thyt.TiPLM.UIL.Resource.Common.UserControl {
    partial class UCselRes {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        protected override void Dispose(bool disposing) {
            if (this.handler != null) {
                this.ucUser.ResSelected -= this.handler;
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
            Infragistics.Win.UltraWinEditors.DropDownEditorButton button = new Infragistics.Win.UltraWinEditors.DropDownEditorButton("SelectRes") {
                Key = "SelectRes",
                RightAlignDropDown = Infragistics.Win.DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "(无)";
            this.b_ReadOnly = true;
            base.Size = new System.Drawing.Size(100, 0x15);
        }
        #endregion    
    }
}