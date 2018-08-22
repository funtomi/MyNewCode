namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCHasFile {
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
            this.chkBox_hasFile = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.numUpDown = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.numUpDown.Properties.BeginInit();
            base.SuspendLayout();
            this.chkBox_hasFile.Location = new System.Drawing.Point(0x10, 8);
            this.chkBox_hasFile.Name = "chkBox_hasFile";
            this.chkBox_hasFile.Size = new System.Drawing.Size(0xd8, 0x18);
            this.chkBox_hasFile.TabIndex = 0;
            this.chkBox_hasFile.Text = "必须包含文件";
            this.numUpDown.Location = new System.Drawing.Point(0x10, 0x40);
            int[] bits = new int[4];
            bits[0] = 0x3e8;
            this.numUpDown.Maximum = new decimal(bits);
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numUpDown.Size = new System.Drawing.Size(0x58, 0x15);
            this.numUpDown.TabIndex = 1;
            this.label1.Location = new System.Drawing.Point(0x10, 0x30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x60, 0x10);
            this.label1.TabIndex = 2;
            this.label1.Text = "文件个数最少:";
            base.Controls.Add(this.label1);
            base.Controls.Add(this.numUpDown);
            base.Controls.Add(this.chkBox_hasFile);
            base.Name = "UCHasFile";
            base.Size = new System.Drawing.Size(0xf8, 0x70);
            this.numUpDown.Properties.EndInit();
            base.ResumeLayout(false);
        }
        #endregion   
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chkBox_hasFile;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numUpDown;
    }
}