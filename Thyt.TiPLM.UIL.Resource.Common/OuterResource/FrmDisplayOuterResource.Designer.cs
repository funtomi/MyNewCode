namespace Thyt.TiPLM.UIL.Resource.Common.OuterResource {
    partial class FrmDisplayOuterResource {
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
            this.gbData = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.dgdData = new System.Windows.Forms.DataGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gbData)).BeginInit();
            this.gbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdData)).BeginInit();
            this.SuspendLayout();
            // 
            // gbData
            // 
            this.gbData.Controls.Add(this.dgdData);
            this.gbData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbData.Location = new System.Drawing.Point(0, 0);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(464, 357);
            this.gbData.TabIndex = 2;
            this.gbData.Text = "资源数据";
            // 
            // dgdData
            // 
            this.dgdData.CaptionVisible = false;
            this.dgdData.DataMember = "";
            this.dgdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgdData.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgdData.Location = new System.Drawing.Point(2, 26);
            this.dgdData.Name = "dgdData";
            this.dgdData.ReadOnly = true;
            this.dgdData.Size = new System.Drawing.Size(460, 329);
            this.dgdData.TabIndex = 0;
            // 
            // FrmDisplayOuterResource
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(464, 357);
            this.Controls.Add(this.gbData);
            this.Name = "FrmDisplayOuterResource";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "外部资源数据";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDisplayOuterResource_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbData)).EndInit();
            this.gbData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgdData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion    
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM gbData;
        private System.Windows.Forms.DataGrid dgdData;


    }
}