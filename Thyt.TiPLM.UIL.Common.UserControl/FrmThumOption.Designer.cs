namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class FrmThumOption {
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
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.radThumbnail2Visualization = new System.Windows.Forms.RadioButton();
            this.radThumbnail = new System.Windows.Forms.RadioButton();
            this.radVisualization = new System.Windows.Forms.RadioButton();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.btnCancel = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.btnOk = new Thyt.TiPLM.UIL.Controls.ButtonPLM();
            this.panel1.SuspendLayout();
            this.groupBox1.BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x1ab, 0x89);
            this.panel1.TabIndex = 0;
            this.groupBox1.Controls.Add(this.radThumbnail2Visualization);
            this.groupBox1.Controls.Add(this.radThumbnail);
            this.groupBox1.Controls.Add(this.radVisualization);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.ShowCaption = false;
            this.groupBox1.Size = new System.Drawing.Size(0x1ab, 0x89);
            this.groupBox1.TabIndex = 3;
            this.radThumbnail2Visualization.AutoSize = true;
            this.radThumbnail2Visualization.Location = new System.Drawing.Point(0x26, 0x36);
            this.radThumbnail2Visualization.Name = "radThumbnail2Visualization";
            this.radThumbnail2Visualization.Size = new System.Drawing.Size(0xe5, 0x12);
            this.radThumbnail2Visualization.TabIndex = 5;
            this.radThumbnail2Visualization.Text = "没有缩略图文件时加载可视化文件预览";
            this.radThumbnail2Visualization.UseVisualStyleBackColor = true;
            this.radThumbnail.AutoSize = true;
            this.radThumbnail.Checked = true;
            this.radThumbnail.Location = new System.Drawing.Point(0x26, 20);
            this.radThumbnail.Name = "radThumbnail";
            this.radThumbnail.Size = new System.Drawing.Size(0x85, 0x12);
            this.radThumbnail.TabIndex = 4;
            this.radThumbnail.TabStop = true;
            this.radThumbnail.Text = "使用缩略图文件预览";
            this.radThumbnail.UseVisualStyleBackColor = true;
            this.radVisualization.AutoSize = true;
            this.radVisualization.Location = new System.Drawing.Point(0x26, 0x59);
            this.radVisualization.Name = "radVisualization";
            this.radVisualization.Size = new System.Drawing.Size(0x85, 0x12);
            this.radVisualization.TabIndex = 3;
            this.radVisualization.Text = "使用可视化文件预览";
            this.radVisualization.UseVisualStyleBackColor = true;
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 0x89);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0x1ab, 0x2c);
            this.panel2.TabIndex = 1;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.Location = new System.Drawing.Point(340, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnOk.Location = new System.Drawing.Point(0x103, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            base.AutoScaleMode =  System.Windows.Forms.AutoScaleMode.Inherit;
            base.ClientSize = new System.Drawing.Size(0x1ab, 0xb5);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.panel2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FrmThumOption";
            base.ShowInTaskbar = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预览选项";
            this.panel1.ResumeLayout(false);
            this.groupBox1.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnCancel;
        private Thyt.TiPLM.UIL.Controls.ButtonPLM btnOk;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private System.Windows.Forms.RadioButton radThumbnail;
        private System.Windows.Forms.RadioButton radThumbnail2Visualization;
        private System.Windows.Forms.RadioButton radVisualization;
    }
}