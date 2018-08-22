namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCParamMain {
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
            this.panel_down = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btn_cancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel_Param = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.panel_down.SuspendLayout();
            base.SuspendLayout();
            this.panel_down.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_down.Controls.Add(this.btn_cancel);
            this.panel_down.Controls.Add(this.btn_ok);
            this.panel_down.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_down.Location = new System.Drawing.Point(0, 240);
            this.panel_down.Name = "panel_down";
            this.panel_down.Size = new System.Drawing.Size(0x130, 40);
            this.panel_down.TabIndex = 0;
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btn_cancel.Location = new System.Drawing.Point(0xde, 8);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(0x48, 0x18);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            this.btn_ok.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btn_ok.Location = new System.Drawing.Point(0x86, 8);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x48, 0x18);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel_Param.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Param.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Param.Location = new System.Drawing.Point(0, 0);
            this.panel_Param.Name = "panel_Param";
            this.panel_Param.Size = new System.Drawing.Size(0x130, 240);
            this.panel_Param.TabIndex = 1;
            base.Controls.Add(this.panel_Param);
            base.Controls.Add(this.panel_down);
            base.Name = "UCParamMain";
            base.Size = new System.Drawing.Size(0x130, 280);
            base.Enter += new System.EventHandler(this.UCParamMain_Enter);
            this.panel_down.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion 
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_cancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel_down;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel_Param;
    }
}