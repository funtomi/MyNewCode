namespace Thyt.TiPLM.CLT.TiModeler.Addin {
    partial class FrmAddin {
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

        private void InitializeComponent() {
            this.lvwAddin = new Thyt.TiPLM.UIL.Common.SortableListView();
            this.cmuAddin = new System.Windows.Forms.ContextMenu();
            this.SuspendLayout();
            // 
            // lvwAddin
            // 
            this.lvwAddin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAddin.FullRowSelect = true;
            this.lvwAddin.HideSelection = false;
            this.lvwAddin.Location = new System.Drawing.Point(0, 0);
            this.lvwAddin.Name = "lvwAddin";
            this.lvwAddin.OwnerDraw = true;
            this.lvwAddin.Size = new System.Drawing.Size(608, 373);
            this.lvwAddin.SortingOrder = System.Windows.Forms.SortOrder.None;
            this.lvwAddin.TabIndex = 1;
            this.lvwAddin.UseCompatibleStateImageBehavior = false;
            this.lvwAddin.View = System.Windows.Forms.View.Details;
            this.lvwAddin.ItemActivate += new System.EventHandler(this.lvwAddin_ItemActivate);
            this.lvwAddin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lvwAddin_MouseUp);
            // 
            // FrmAddin
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 18);
            this.ClientSize = new System.Drawing.Size(608, 373);
            this.Controls.Add(this.lvwAddin);
            this.Name = "FrmAddin";
            this.Text = "插件管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmAddin_Closing);
            this.Load += new System.EventHandler(this.FrmAddin_Load);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.ContextMenu cmuAddin;
        private FrmMain frmMain;
        public Thyt.TiPLM.UIL.Common.SortableListView lvwAddin;
         
        #endregion
    }
}