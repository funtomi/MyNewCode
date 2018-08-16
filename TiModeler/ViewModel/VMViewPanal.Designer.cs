namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    partial class VMViewPanal {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary> 

        private void InitializeComponent() {
            this.cmuCommon = new System.Windows.Forms.ContextMenu();
            this.AutoScroll = true;
            base.AutoScrollMargin = new System.Drawing.Size(0x400, 0x300);
            base.AutoScrollMinSize = new System.Drawing.Size(5, 5);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            base.Click += new System.EventHandler(this.VMView_Click);
            base.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VMView_MouseUp);
            base.DoubleClick += new System.EventHandler(this.VMView_DoubleClick);
            base.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VMView_MouseMove);
            base.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VMView_MouseDown);
        }
        #endregion
        private System.Windows.Forms.ContextMenu cmuCommon;

    }
}