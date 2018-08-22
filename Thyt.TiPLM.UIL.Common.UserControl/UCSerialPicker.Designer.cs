namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCSerialPicker {
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
            this.numSplit = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.numStart = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.numCodePosition = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.numSplit.Properties.BeginInit();
            this.numStart.Properties.BeginInit();
            this.numCodePosition.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0x7c);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x110, 0x20);
            this.panel1.TabIndex = 0;
            this.btn_ok.Location = new System.Drawing.Point(0xd0, 4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.numSplit);
            this.panel2.Controls.Add(this.numStart);
            this.panel2.Controls.Add(this.numCodePosition);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x110, 0x7c);
            this.panel2.TabIndex = 1;
            this.numSplit.Location = new System.Drawing.Point(0x60, 0x52);
            int[] bits = new int[4];
            bits[0] = 0x3e8;
            this.numSplit.Maximum = new decimal(bits);
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numSplit.Minimum = new decimal(numArray2);
            this.numSplit.Name = "numSplit";
            this.numSplit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numSplit.Size = new System.Drawing.Size(120, 0x15);
            this.numSplit.TabIndex = 5;
            int[] numArray3 = new int[4];
            numArray3[0] = 1;
            this.numSplit.Value = new decimal(numArray3);
            this.numStart.Location = new System.Drawing.Point(0x60, 0x33);
            int[] numArray4 = new int[4];
            numArray4[0] = 0x2710;
            this.numStart.Maximum = new decimal(numArray4);
            this.numStart.Name = "numStart";
            this.numStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numStart.Size = new System.Drawing.Size(120, 0x15);
            this.numStart.TabIndex = 4;
            int[] numArray5 = new int[4];
            numArray5[0] = 1;
            this.numStart.Value = new decimal(numArray5);
            this.numCodePosition.Location = new System.Drawing.Point(0x60, 20);
            int[] numArray6 = new int[4];
            numArray6[0] = 20;
            this.numCodePosition.Maximum = new decimal(numArray6);
            int[] numArray7 = new int[4];
            numArray7[0] = 1;
            this.numCodePosition.Minimum = new decimal(numArray7);
            this.numCodePosition.Name = "numCodePosition";
            this.numCodePosition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.numCodePosition.Size = new System.Drawing.Size(120, 0x15);
            this.numCodePosition.TabIndex = 3;
            int[] numArray8 = new int[4];
            numArray8[0] = 4;
            this.numCodePosition.Value = new decimal(numArray8);
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0x29, 0x54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0x2f, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "间隔数:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0x29, 0x35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x2f, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "开始值:";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0x29, 0x16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "位数:";
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCSerialPicker";
            base.Size = new System.Drawing.Size(0x110, 0x9c);
            base.Enter += new System.EventHandler(this.UCTimePicker_Enter);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.numSplit.Properties.EndInit();
            this.numStart.Properties.EndInit();
            this.numCodePosition.Properties.EndInit();
            base.ResumeLayout(false);
        }

        #endregion  
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numCodePosition;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numSplit;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM numStart;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
    }
}