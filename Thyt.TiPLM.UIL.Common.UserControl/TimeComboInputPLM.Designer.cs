namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class TimeComboInputPLM {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (this.dlhandler != null) {
                this.ucUser.TimeSelected -= this.dlhandler;
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
            base.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.popupContainer = new DevExpress.XtraEditors.PopupContainerControl();
            base.Properties.PopupControl = this.popupContainer;
            this.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new System.Drawing.Size(100, 0x15);
        }
        #endregion 
        private DevExpress.XtraEditors.PopupContainerControl popupContainer;
   
    }
}