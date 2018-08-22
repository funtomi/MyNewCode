namespace Thyt.TiPLM.UIL.Resource.Common.OuterResource {
    partial class FrmSelAddin {
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
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.lV_Addin = new Thyt.TiPLM.UIL.Controls.ListViewPLM();
            base.SuspendLayout();
            this.btn_ok.Location = new System.Drawing.Point(0xe0, 0xe8);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x38, 0x18);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确 定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.lV_Addin.FullRowSelect = true;
            this.lV_Addin.Location = new System.Drawing.Point(8, 8);
            this.lV_Addin.MultiSelect = false;
            this.lV_Addin.Name = "lV_Addin";
            this.lV_Addin.Size = new System.Drawing.Size(0x110, 0xd0);
            this.lV_Addin.TabIndex = 1;
            this.lV_Addin.View = System.Windows.Forms.View.Details;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.ClientSize = new System.Drawing.Size(0x124, 0x10a);
            base.Controls.Add(this.lV_Addin);
            base.Controls.Add(this.btn_ok);
            base.Name = "FrmSelAddin";
            this.Text = "FrmSelAddin";
            base.Load += new System.EventHandler(this.FrmSelAddin_Load);
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.ListViewPLM lV_Addin;
    }
}