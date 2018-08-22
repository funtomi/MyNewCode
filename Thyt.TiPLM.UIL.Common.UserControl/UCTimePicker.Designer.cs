namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCTimePicker {
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
            this.btn_today = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_clear = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btn_ok = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.panel4 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.mon_ymd = new System.Windows.Forms.MonthCalendar();
            this.panel3 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.num_ss = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.label3 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.num_mm = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.label2 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.num_hh = new Thyt.TiPLM.UIL.Controls.SpinEditPLM();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel1.BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.BeginInit();
            this.panel2.SuspendLayout();
            this.panel4.BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.BeginInit();
            this.panel3.SuspendLayout();
            this.num_ss.Properties.BeginInit();
            this.num_mm.Properties.BeginInit();
            this.num_hh.Properties.BeginInit();
            base.SuspendLayout();
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_today);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_ok);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 0xd0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x110, 0x20);
            this.panel1.TabIndex = 0;
            this.btn_today.Appearance.Options.UseBackColor = true;
            this.btn_today.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_today.Location = new System.Drawing.Point(0x10, 4);
            this.btn_today.Name = "btn_today";
            this.btn_today.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_today.TabIndex = 2;
            this.btn_today.Text = "今天";
            this.btn_today.Click += new System.EventHandler(this.btn_today_Click);
            this.btn_clear.Appearance.Options.UseBackColor = true;
            this.btn_clear.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_clear.Location = new System.Drawing.Point(0x8a, 4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_clear.TabIndex = 1;
            this.btn_clear.Text = "清空";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            this.btn_ok.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btn_ok.Location = new System.Drawing.Point(0xd0, 4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(0x3f, 0x17);
            this.btn_ok.TabIndex = 0;
            this.btn_ok.Text = "确定";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x110, 0xd0);
            this.panel2.TabIndex = 1;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel4.Controls.Add(this.mon_ymd);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(2, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0x10c, 0xac);
            this.panel4.TabIndex = 1;
            this.mon_ymd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mon_ymd.Location = new System.Drawing.Point(0, 0);
            this.mon_ymd.Name = "mon_ymd";
            this.mon_ymd.ShowToday = false;
            this.mon_ymd.ShowTodayCircle = false;
            this.mon_ymd.TabIndex = 0;
            this.mon_ymd.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mon_ymd_DateChanged);
            this.mon_ymd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mon_ymd_MouseUp);
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panel3.Controls.Add(this.num_ss);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.num_mm);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.num_hh);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(2, 0xae);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0x10c, 0x20);
            this.panel3.TabIndex = 0;
            int[] bits = new int[4];
            this.num_ss.EditValue = new decimal(bits);
            this.num_ss.Location = new System.Drawing.Point(0xa8, 5);
            this.num_ss.Name = "num_ss";
            this.num_ss.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            int[] numArray2 = new int[4];
            numArray2[0] = 0x3b;
            this.num_ss.Properties.MaxValue = new decimal(numArray2);
            this.num_ss.Size = new System.Drawing.Size(0x30, 0x16);
            this.num_ss.TabIndex = 5;
            this.num_ss.ValueChanged += new System.EventHandler(this.num_ss_ValueChanged);
            this.label3.Location = new System.Drawing.Point(0xd8, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "秒";
            int[] numArray3 = new int[4];
            this.num_mm.EditValue = new decimal(numArray3);
            this.num_mm.Location = new System.Drawing.Point(0x58, 5);
            this.num_mm.Name = "num_mm";
            this.num_mm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            int[] numArray4 = new int[4];
            numArray4[0] = 0x3b;
            this.num_mm.Properties.MaxValue = new decimal(numArray4);
            this.num_mm.Size = new System.Drawing.Size(0x30, 0x16);
            this.num_mm.TabIndex = 3;
            this.num_mm.ValueChanged += new System.EventHandler(this.num_mm_ValueChanged);
            this.label2.Location = new System.Drawing.Point(0x88, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "分";
            int[] numArray5 = new int[4];
            this.num_hh.EditValue = new decimal(numArray5);
            this.num_hh.Location = new System.Drawing.Point(0x10, 5);
            this.num_hh.Name = "num_hh";
            this.num_hh.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            int[] numArray6 = new int[4];
            numArray6[0] = 0x17;
            this.num_hh.Properties.MaxValue = new decimal(numArray6);
            this.num_hh.Size = new System.Drawing.Size(40, 0x16);
            this.num_hh.TabIndex = 1;
            this.num_hh.ValueChanged += new System.EventHandler(this.num_hh_ValueChanged);
            this.label1.Location = new System.Drawing.Point(0x38, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "时";
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.panel1);
            base.Name = "UCTimePicker";
            base.Size = new System.Drawing.Size(0x110, 240);
            base.Enter += new System.EventHandler(this.UCTimePicker_Enter);
            this.panel1.EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.EndInit();
            this.panel2.ResumeLayout(false);
            this.panel4.EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.num_ss.Properties.EndInit();
            this.num_mm.Properties.EndInit();
            this.num_hh.Properties.EndInit();
            base.ResumeLayout(false);
        }

        #endregion 
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_clear;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_ok;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btn_today;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label2;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label3;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM num_hh;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM num_mm;
        private Thyt.TiPLM.UIL.Controls.SpinEditPLM num_ss;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel3;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel4;
        private System.Windows.Forms.MonthCalendar mon_ymd;

    }
}