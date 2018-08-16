namespace Thyt.TiPLM.CLT.TiModeler.BPModel {
    partial class WFTEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFTEditor));
            this.menuItem7 = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.panelMam = new System.Windows.Forms.Panel();
            this.hsbParent = new System.Windows.Forms.HScrollBar();
            this.vsbParent = new System.Windows.Forms.VScrollBar();
            this.cmuProcessProperty = new System.Windows.Forms.ContextMenu();
            this.cmiSaveToDB = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiImageOutput = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiSaveToLocal = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.menuItem1 = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiNodePaste = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.menuItem3 = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiProProperty = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiVerify = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.menuItemSeperate = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiClose = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.tbrForWin = new System.Windows.Forms.ToolBar();
            this.toolBarBunCursor = new System.Windows.Forms.ToolBarButton();
            this.toolBarBunTaskNode = new System.Windows.Forms.ToolBarButton();
            this.toolBarBunRouteNode = new System.Windows.Forms.ToolBarButton();
            this.toolBarSeprate = new System.Windows.Forms.ToolBarButton();
            this.toolBarBunUndo = new System.Windows.Forms.ToolBarButton();
            this.toolBarBunRedo = new System.Windows.Forms.ToolBarButton();
            this.cmuNodeProperty = new System.Windows.Forms.ContextMenu();
            this.cmiNodeProperty = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiNodeCopy = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmuLineProperty = new System.Windows.Forms.ContextMenu();
            this.cmiLineTrue = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.cmiLineFalse = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelMam.SuspendLayout();
            base.SuspendLayout();
            this.menuItem7.ClickHandler = null;
            this.menuItem7.EventHandlerName = null;
            this.menuItem7.Icon = null;
            this.menuItem7.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.menuItem7.ImageIndex = -1;
            this.menuItem7.ImageList = null;
            this.menuItem7.Index = -1;
            this.menuItem7.OwnerDraw = true;
            this.menuItem7.ShortcutText = "";
            resources.ApplyResources(this.menuItem7, "menuItem7");
            this.panelMam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMam.Controls.Add(this.hsbParent);
            this.panelMam.Controls.Add(this.vsbParent);
            resources.ApplyResources(this.panelMam, "panelMam");
            this.panelMam.Name = "panelMam";
            this.panelMam.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMam_Paint);
            resources.ApplyResources(this.hsbParent, "hsbParent");
            this.hsbParent.Name = "hsbParent";
            this.hsbParent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbParent_Scroll);
            resources.ApplyResources(this.vsbParent, "vsbParent");
            this.vsbParent.Name = "vsbParent";
            this.vsbParent.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbParent_Scroll);
            this.cmuProcessProperty.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.cmiSaveToDB, this.cmiImageOutput, this.cmiSaveToLocal, this.menuItem1, this.cmiNodePaste, this.menuItem3, this.cmiProProperty, this.cmiVerify, this.menuItemSeperate, this.cmiClose });
            this.cmiSaveToDB.ClickHandler = null;
            this.cmiSaveToDB.EventHandlerName = null;
            this.cmiSaveToDB.Icon = null;
            this.cmiSaveToDB.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiSaveToDB.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiSaveToDB.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_SAVE");
            this.cmiSaveToDB.Index = 0;
            this.cmiSaveToDB.OwnerDraw = true;
            this.cmiSaveToDB.ShortcutText = "";
            resources.ApplyResources(this.cmiSaveToDB, "cmiSaveToDB");
            this.cmiSaveToDB.Click += new System.EventHandler(this.cmiSaveToDB_Click);
            this.cmiImageOutput.ClickHandler = null;
            this.cmiImageOutput.EventHandlerName = null;
            this.cmiImageOutput.Icon = null;
            this.cmiImageOutput.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiImageOutput.ImageIndex = -1;
            this.cmiImageOutput.ImageList = null;
            this.cmiImageOutput.Index = 1;
            this.cmiImageOutput.OwnerDraw = true;
            this.cmiImageOutput.ShortcutText = "";
            resources.ApplyResources(this.cmiImageOutput, "cmiImageOutput");
            this.cmiImageOutput.Click += new System.EventHandler(this.cmiImageOutput_Click);
            this.cmiSaveToLocal.ClickHandler = null;
            this.cmiSaveToLocal.EventHandlerName = null;
            this.cmiSaveToLocal.Icon = null;
            this.cmiSaveToLocal.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiSaveToLocal.ImageIndex = -1;
            this.cmiSaveToLocal.ImageList = null;
            this.cmiSaveToLocal.Index = 2;
            this.cmiSaveToLocal.OwnerDraw = true;
            this.cmiSaveToLocal.ShortcutText = "";
            resources.ApplyResources(this.cmiSaveToLocal, "cmiSaveToLocal");
            this.cmiSaveToLocal.Click += new System.EventHandler(this.cmiSaveToLocal_Click);
            this.menuItem1.ClickHandler = null;
            this.menuItem1.EventHandlerName = null;
            this.menuItem1.Icon = null;
            this.menuItem1.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.menuItem1.ImageIndex = -1;
            this.menuItem1.ImageList = null;
            this.menuItem1.Index = 3;
            this.menuItem1.OwnerDraw = true;
            this.menuItem1.ShortcutText = "";
            resources.ApplyResources(this.menuItem1, "menuItem1");
            this.cmiNodePaste.ClickHandler = null;
            this.cmiNodePaste.EventHandlerName = null;
            this.cmiNodePaste.Icon = null;
            this.cmiNodePaste.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiNodePaste.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiNodePaste.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_PASTE");
            this.cmiNodePaste.Index = 4;
            this.cmiNodePaste.OwnerDraw = true;
            this.cmiNodePaste.ShortcutText = "";
            resources.ApplyResources(this.cmiNodePaste, "cmiNodePaste");
            this.cmiNodePaste.Click += new System.EventHandler(this.cmiNodePaste_Click);
            this.menuItem3.ClickHandler = null;
            this.menuItem3.EventHandlerName = null;
            this.menuItem3.Icon = null;
            this.menuItem3.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.menuItem3.ImageIndex = -1;
            this.menuItem3.ImageList = null;
            this.menuItem3.Index = 5;
            this.menuItem3.OwnerDraw = true;
            this.menuItem3.ShortcutText = "";
            resources.ApplyResources(this.menuItem3, "menuItem3");
            this.cmiProProperty.ClickHandler = null;
            this.cmiProProperty.EventHandlerName = null;
            this.cmiProProperty.Icon = null;
            this.cmiProProperty.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiProProperty.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiProProperty.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_PROPERT");
            this.cmiProProperty.Index = 6;
            this.cmiProProperty.OwnerDraw = true;
            this.cmiProProperty.ShortcutText = "";
            resources.ApplyResources(this.cmiProProperty, "cmiProProperty");
            this.cmiProProperty.Click += new System.EventHandler(this.cmiProProperty_Click);
            this.cmiVerify.ClickHandler = null;
            this.cmiVerify.EventHandlerName = null;
            this.cmiVerify.Icon = null;
            this.cmiVerify.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiVerify.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiVerify.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_CHECK");
            this.cmiVerify.Index = 7;
            this.cmiVerify.OwnerDraw = true;
            this.cmiVerify.ShortcutText = "";
            resources.ApplyResources(this.cmiVerify, "cmiVerify");
            this.cmiVerify.Click += new System.EventHandler(this.cmiVerify_Click);
            this.menuItemSeperate.ClickHandler = null;
            this.menuItemSeperate.EventHandlerName = null;
            this.menuItemSeperate.Icon = null;
            this.menuItemSeperate.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.menuItemSeperate.ImageIndex = -1;
            this.menuItemSeperate.ImageList = null;
            this.menuItemSeperate.Index = 8;
            this.menuItemSeperate.OwnerDraw = true;
            this.menuItemSeperate.ShortcutText = "";
            resources.ApplyResources(this.menuItemSeperate, "menuItemSeperate");
            this.cmiClose.ClickHandler = null;
            this.cmiClose.EventHandlerName = null;
            this.cmiClose.Icon = null;
            this.cmiClose.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiClose.ImageIndex = -1;
            this.cmiClose.ImageList = null;
            this.cmiClose.Index = 9;
            this.cmiClose.OwnerDraw = true;
            this.cmiClose.ShortcutText = "";
            resources.ApplyResources(this.cmiClose, "cmiClose");
            this.cmiClose.Click += new System.EventHandler(this.cmiClose_Click);
            resources.ApplyResources(this.tbrForWin, "tbrForWin");
            this.tbrForWin.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.toolBarBunCursor, this.toolBarBunTaskNode, this.toolBarBunRouteNode, this.toolBarSeprate, this.toolBarBunUndo, this.toolBarBunRedo });
            this.tbrForWin.Name = "tbrForWin";
            this.tbrForWin.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tbrForWin_ButtonClick);
            resources.ApplyResources(this.toolBarBunCursor, "toolBarBunCursor");
            resources.ApplyResources(this.toolBarBunTaskNode, "toolBarBunTaskNode");
            resources.ApplyResources(this.toolBarBunRouteNode, "toolBarBunRouteNode");
            resources.ApplyResources(this.toolBarSeprate, "toolBarSeprate");
            resources.ApplyResources(this.toolBarBunUndo, "toolBarBunUndo");
            resources.ApplyResources(this.toolBarBunRedo, "toolBarBunRedo");
            this.cmuNodeProperty.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.cmiNodeProperty, this.cmiNodeCopy });
            this.cmiNodeProperty.ClickHandler = null;
            this.cmiNodeProperty.EventHandlerName = null;
            this.cmiNodeProperty.Icon = null;
            this.cmiNodeProperty.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiNodeProperty.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiNodeProperty.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_PROPERT");
            this.cmiNodeProperty.Index = 0;
            this.cmiNodeProperty.OwnerDraw = true;
            this.cmiNodeProperty.ShortcutText = "";
            resources.ApplyResources(this.cmiNodeProperty, "cmiNodeProperty");
            this.cmiNodeProperty.Click += new System.EventHandler(this.cmiNodeProperty_Click);
            this.cmiNodeCopy.ClickHandler = null;
            this.cmiNodeCopy.EventHandlerName = null;
            this.cmiNodeCopy.Icon = null;
            this.cmiNodeCopy.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiNodeCopy.ImageList = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.imageList;
            this.cmiNodeCopy.ImageIndex = Thyt.TiPLM.UIL.Common.ClientData.MyImageList.GetIconIndex("ICO_BPM_COPY");
            this.cmiNodeCopy.Index = 1;
            this.cmiNodeCopy.OwnerDraw = true;
            this.cmiNodeCopy.ShortcutText = "";
            resources.ApplyResources(this.cmiNodeCopy, "cmiNodeCopy");
            this.cmiNodeCopy.Click += new System.EventHandler(this.cmiNodeCopy_Click);
            this.cmuLineProperty.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.cmiLineTrue, this.cmiLineFalse });
            this.cmiLineTrue.ClickHandler = null;
            this.cmiLineTrue.EventHandlerName = null;
            this.cmiLineTrue.Icon = null;
            this.cmiLineTrue.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiLineTrue.ImageIndex = -1;
            this.cmiLineTrue.ImageList = null;
            this.cmiLineTrue.Index = 0;
            this.cmiLineTrue.OwnerDraw = true;
            this.cmiLineTrue.ShortcutText = "";
            resources.ApplyResources(this.cmiLineTrue, "cmiLineTrue");
            this.cmiLineTrue.Click += new System.EventHandler(this.cmiLineTrue_Click);
            this.cmiLineFalse.ClickHandler = null;
            this.cmiLineFalse.EventHandlerName = null;
            this.cmiLineFalse.Icon = null;
            this.cmiLineFalse.IconTransparentColor = System.Drawing.Color.FromArgb(0, 0x80, 0x80);
            this.cmiLineFalse.ImageIndex = -1;
            this.cmiLineFalse.ImageList = null;
            this.cmiLineFalse.Index = 1;
            this.cmiLineFalse.OwnerDraw = true;
            this.cmiLineFalse.ShortcutText = "";
            resources.ApplyResources(this.cmiLineFalse, "cmiLineFalse");
            this.cmiLineFalse.Click += new System.EventHandler(this.cmiLineFalse_Click);
            this.saveFileDialog1.DefaultExt = "png";
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            base.Controls.Add(this.panelMam);
            base.Controls.Add(this.tbrForWin);
            base.KeyPreview = true;
            base.Name = "WFTEditor";
            base.ShowInTaskbar = false;
            base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            base.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WFTEditor_MouseUp);
            base.Closing += new System.ComponentModel.CancelEventHandler(this.WFTEditor_Closing);
            base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WFTEditor_KeyPress);
            base.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WFTEditor_KeyDown);
            base.Load += new System.EventHandler(this.Form1_Load);
            this.panelMam.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        #endregion

        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiClose;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiImageOutput;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiLineFalse;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiLineTrue;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiNodeCopy;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiNodePaste;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiNodeProperty;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiProProperty;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiSaveToDB;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiSaveToLocal;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx cmiVerify;
        public System.Windows.Forms.ContextMenu cmuLineProperty;
        public System.Windows.Forms.ContextMenu cmuNodeProperty;
        public System.Windows.Forms.ContextMenu cmuProcessProperty;
        private System.Windows.Forms.HScrollBar hsbParent;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx menuItem1;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx menuItem3;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx menuItem7;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx menuItemSeperate;
        public System.Windows.Forms.Panel panelMam;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolBar tbrForWin;
        public FrmMain tiMain;
        private System.Windows.Forms.ToolBarButton toolBarBunCursor;
        private System.Windows.Forms.ToolBarButton toolBarBunRedo;
        private System.Windows.Forms.ToolBarButton toolBarBunRouteNode;
        private System.Windows.Forms.ToolBarButton toolBarBunTaskNode;
        private System.Windows.Forms.ToolBarButton toolBarBunUndo;
        private System.Windows.Forms.ToolBarButton toolBarSeprate;
        public ViewPanel viewPanel;
        private System.Windows.Forms.VScrollBar vsbParent;
    }
}