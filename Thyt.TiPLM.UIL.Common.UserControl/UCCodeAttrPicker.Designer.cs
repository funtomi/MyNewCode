namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCCodeAttrPicker {
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
            this.panel1 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.pnlValue = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.comBoxAttrName = new Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.comBoxAttrName.Properties.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x7c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x110, 0x20);
            this.panel1.TabIndex = 0;
            this.btn_ok.Location = new System.Drawing.Point(0xb5, 3);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnlValue);
            this.panel2.Controls.Add(this.comBoxAttrName);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x110, 0x7c);
            this.panel2.TabIndex = 1;
            this.pnlValue.Location = new System.Drawing.Point(0x68, 0x42);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(140, 0x19);
            this.pnlValue.TabIndex = 3;
            this.comBoxAttrName.FormattingEnabled = true;
            this.comBoxAttrName.Location = new System.Drawing.Point(0x68, 0x1f);
            this.comBoxAttrName.Name = "comBoxAttrName";
            this.comBoxAttrName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.comBoxAttrName.Size = new System.Drawing.Size(140, 20);
            this.comBoxAttrName.TabIndex = 2;
            this.comBoxAttrName.SelectedIndexChanged += new System.EventHandler(this.comBoxAttrName_SelectedIndexChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 0x42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "资源类属性名:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x27, 0x22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "类属性名:";
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCCodeAttrPicker";
            base.Size = new System.Drawing.Size(0x110, 0x9c);
            this.comBoxAttrName.Properties.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.ComboBoxEditPLM comBoxAttrName;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM pnlValue;
    }
}