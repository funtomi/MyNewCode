namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC {
    partial class UCResGrid {
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
            this.TV_Class = new System.Windows.Forms.TreeView();
            this.splitter1 = new Thyt.TiPLM.UIL.Controls.SplitterPLM();
            this.panel2 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.panel3 = new Thyt.TiPLM.UIL.Controls.PanelPLM();
            this.uGrid_Res = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.uGrid_Res).BeginInit();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.TV_Class);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x58, 0x128);
            this.panel1.TabIndex = 0;
            this.TV_Class.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TV_Class.Location = new System.Drawing.Point(2, 2);
            this.TV_Class.Name = "TV_Class";
            this.TV_Class.Size = new System.Drawing.Size(0x54, 0x124);
            this.TV_Class.TabIndex = 0;
            this.TV_Class.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Class_AfterSelect);
            this.splitter1.Location = new System.Drawing.Point(0x58, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 0x128);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.uGrid_Res);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0x60, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0xe0, 0x128);
            this.panel2.TabIndex = 2;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(0xe0, 0x18);
            this.panel3.TabIndex = 2;
            this.uGrid_Res.Location = new System.Drawing.Point(0, 0);
            this.uGrid_Res.Name = "uGrid_Res";
            this.uGrid_Res.Size = new System.Drawing.Size(0xe0, 0x128);
            this.uGrid_Res.TabIndex = 0;
            this.uGrid_Res.Text = "-";
            this.uGrid_Res.Click += new System.EventHandler(this.uGrid_Res_Click);
            this.uGrid_Res.DoubleClick += new System.EventHandler(this.uGrid_Res_DoubleClick);
            base.Controls.Add(this.panel2);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.panel1);
            base.Name = "UCResGrid";
            base.Size = new System.Drawing.Size(320, 0x128);
            base.Enter += new System.EventHandler(this.UCRes_Enter);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.uGrid_Res).EndInit();
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel2;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel3;
        private Thyt.TiPLM.UIL.Controls.SplitterPLM splitter1;
        private System.Windows.Forms.TreeView TV_Class;
        private Thyt.TiPLM.UIL.Controls.TextEditPLM[] txtbox;
        private Infragistics.Win.UltraWinGrid.UltraGrid uGrid_Res;

    }
}