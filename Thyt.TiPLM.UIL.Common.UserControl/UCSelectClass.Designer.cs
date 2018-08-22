namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCSelectClass {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            this.ucClassTree.ClassSelected -= this.handler;
            if (this.handler2 != null) {
                this.ucClassTree.DoubClicked -= this.handler2;
                this.handler2 = null;
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
            Infragistics.Win.UltraWinEditors.DropDownEditorButton button = new Infragistics.Win.UltraWinEditors.DropDownEditorButton("SelectClass") {
                Key = "SelectClass",
                RightAlignDropDown = Infragistics.Win.DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "(无)";
            base.ReadOnly = true;
            base.Size = new System.Drawing.Size(100, 0x15);
            base.SizeChanged += new System.EventHandler(this.UCSelectClass_SizeChanged);
        }
        #endregion   
  
    }
}