namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCTreeViewPicker {
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
            this.tvInput = new System.Windows.Forms.TreeView();
            base.SuspendLayout();
            this.tvInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvInput.Location = new System.Drawing.Point(0, 0);
            this.tvInput.Name = "tvInput";
            this.tvInput.Size = new System.Drawing.Size(0x110, 0x9c);
            this.tvInput.TabIndex = 0;
            this.tvInput.DoubleClick += new System.EventHandler(this.tvInput_DoubleClick);
            this.tvInput.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvInput_AfterSelect);
            base.Controls.Add(this.tvInput);
            base.Name = "UCTreeViewPicker";
            base.Size = new System.Drawing.Size(0x110, 0x9c);
            base.Enter += new System.EventHandler(this.UCTimePicker_Enter);
            base.ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TreeView tvInput;

    }
}