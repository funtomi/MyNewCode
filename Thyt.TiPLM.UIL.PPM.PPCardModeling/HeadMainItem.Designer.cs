namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    partial class HeadMainItem {
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
            this.lab_Head = new System.Windows.Forms.Label();
            this.lab_Main = new System.Windows.Forms.Label();
            this.tvHead = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            this.tvMain = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            base.SuspendLayout();
            this.lab_Head.Location = new System.Drawing.Point(8, 0x10);
            this.lab_Head.Name = "lab_Head";
            this.lab_Head.Size = new System.Drawing.Size(0x38, 0x17);
            this.lab_Head.TabIndex = 0;
            this.lab_Head.Text = "头对象：";
            this.lab_Head.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lab_Main.Location = new System.Drawing.Point(0xe0, 0x10);
            this.lab_Main.Name = "lab_Main";
            this.lab_Main.Size = new System.Drawing.Size(0x38, 0x17);
            this.lab_Main.TabIndex = 1;
            this.lab_Main.Text = "主对象：";
            this.lab_Main.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tvHead.AutoScroll = true;
            this.tvHead.DropDown = true;
            this.tvHead.Imagelist = null;
            this.tvHead.IsAollowUseParentNode = false;
            this.tvHead.Location = new System.Drawing.Point(0x38, 0x10);
            this.tvHead.Name = "tvHead";
            this.tvHead.SelectedNode = null;
            this.tvHead.Size = new System.Drawing.Size(150, 0x15);
            this.tvHead.TabIndex = 4;
            this.tvHead.TextReadOnly = true;
            this.tvHead.TextValue = "";
            this.tvMain.DropDown = true;
            this.tvMain.Imagelist = null;
            this.tvMain.IsAollowUseParentNode = false;
            this.tvMain.Location = new System.Drawing.Point(280, 14);
            this.tvMain.Name = "tvMain";
            this.tvMain.SelectedNode = null;
            this.tvMain.Size = new System.Drawing.Size(150, 0x15);
            this.tvMain.TabIndex = 5;
            this.tvMain.TextReadOnly = true;
            this.tvMain.TextValue = "";
            base.Controls.Add(this.tvMain);
            base.Controls.Add(this.tvHead);
            base.Controls.Add(this.lab_Main);
            base.Controls.Add(this.lab_Head);
            base.Name = "HeadMainItem";
            base.Size = new System.Drawing.Size(440, 0x98);
            base.ResumeLayout(false);
        }

        #endregion   
        private System.Windows.Forms.Label lab_Head;
        private System.Windows.Forms.Label lab_Main;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView tvHead;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView tvMain;
    }
}