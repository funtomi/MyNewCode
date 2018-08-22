using System.Drawing;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    partial class UCSearchInFolder {
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
            this.chContainSubFolder = new Thyt.TiPLM.UIL.Controls.CheckEditPLM();
            this.groupBox1 = new Thyt.TiPLM.UIL.Controls.GroupBoxPLM();
            this.dropDownFolderTree = new Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView();
            this.label1 = new Thyt.TiPLM.UIL.Controls.LabelPLM();
            this.panel1.BeginInit();
            this.panel1.SuspendLayout();
            this.chContainSubFolder.Properties.BeginInit();
            this.groupBox1.BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.panel1.Controls.Add(this.chContainSubFolder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0x1d0, 0x29);
            this.panel1.TabIndex = 0;
            this.chContainSubFolder.Location = new System.Drawing.Point(0x18, 13);
            this.chContainSubFolder.Name = "chContainSubFolder";
            this.chContainSubFolder.Properties.Caption = "包含子文件夹";
            this.chContainSubFolder.Size = new System.Drawing.Size(0x70, 0x13);
            this.chContainSubFolder.TabIndex = 0;
            this.groupBox1.Controls.Add(this.dropDownFolderTree);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(0x1d0, 60);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.Text = "需要查询的业务对象";
            this.dropDownFolderTree.DropDown = true;
            this.dropDownFolderTree.Imagelist = null;
            this.dropDownFolderTree.IsAollowUseParentNode = true;
            this.dropDownFolderTree.Location = new Point(0x6c, 0x1c);
            this.dropDownFolderTree.Name = "dropDownFolderTree";
            this.dropDownFolderTree.SelectedNode = null;
            this.dropDownFolderTree.Size = new System.Drawing.Size(330, 0x16);
            this.dropDownFolderTree.TabIndex = 9;
            this.dropDownFolderTree.TextReadOnly = true;
            this.dropDownFolderTree.TextValue = "";
            this.label1.Location = new System.Drawing.Point(0x1c, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0x40, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "定位文件夹:";
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.panel1);
            base.Name = "UCSearchInFolder";
            base.Size = new System.Drawing.Size(0x1d0, 0x65);
            this.panel1.EndInit();
            this.panel1.ResumeLayout(false);
            this.chContainSubFolder.Properties.EndInit();
            this.groupBox1.EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }
        #endregion    
        private Thyt.TiPLM.UIL.Controls.CheckEditPLM chContainSubFolder;
        private Thyt.TiPLM.CLT.UIL.DeskLib.WinControls.DropDownTreeView dropDownFolderTree;
        private Thyt.TiPLM.UIL.Controls.GroupBoxPLM groupBox1;
        private Thyt.TiPLM.UIL.Controls.LabelPLM label1;
        private Thyt.TiPLM.UIL.Controls.PanelPLM panel1;
  
    }
}