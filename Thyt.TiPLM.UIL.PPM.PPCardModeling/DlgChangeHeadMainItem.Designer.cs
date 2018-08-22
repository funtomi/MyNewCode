namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class DlgChangeHeadMainItem {
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tvHead = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            this.tvMain = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            base.SuspendLayout();
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(120, 0x98);
            this.btnOK.Name = "btnOK";
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(320, 0x98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取 消";
            this.tvHead.DropDown = true;
            this.tvHead.Imagelist = null;
            this.tvHead.IsAollowUseParentNode = true;
            this.tvHead.Location = new System.Drawing.Point(0x58, 8);
            this.tvHead.Name = "tvHead";
            this.tvHead.SelectedNode = null;
            this.tvHead.Size = new System.Drawing.Size(150, 0x15);
            this.tvHead.TabIndex = 2;
            this.tvHead.TextReadOnly = false;
            this.tvHead.TextValue = "";
            this.tvMain.DropDown = true;
            this.tvMain.Enabled = false;
            this.tvMain.Imagelist = null;
            this.tvMain.IsAollowUseParentNode = true;
            this.tvMain.Location = new System.Drawing.Point(0x158, 8);
            this.tvMain.Name = "tvMain";
            this.tvMain.SelectedNode = null;
            this.tvMain.Size = new System.Drawing.Size(150, 0x15);
            this.tvMain.TabIndex = 3;
            this.tvMain.TextReadOnly = false;
            this.tvMain.TextValue = "";
            this.label1.Location = new System.Drawing.Point(0x10, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x38, 0x17);
            this.label1.TabIndex = 4;
            this.label1.Text = "头对象：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(0x110, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0x38, 0x17);
            this.label2.TabIndex = 5;
            this.label2.Text = "主对象：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            base.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x200, 0xb6);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.tvMain);
            base.Controls.Add(this.tvHead);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DlgChangeHeadMainItem";
            this.Text = "修改头对象/主对象";
            base.ResumeLayout(false);
        }
        #endregion 
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView tvHead;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView tvMain;
    }
}