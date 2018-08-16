namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.DEL.Admin.View;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.Common;

    public partial class FrmViewModel : Form {
        private ArrayList allVMList;

        public FrmViewModel() {
            this.allVMList = new ArrayList();
            this.InitializeComponent();
            this.lvwViewModel.SmallImageList = ClientData.MyImageList.imageList;
            this.InitializeImageList();
        }

        public FrmViewModel(FrmMain main) {
            this.allVMList = new ArrayList();
            this.InitializeComponent();
            this.frmMain = main;
            this.lvwViewModel.SmallImageList = ClientData.MyImageList.imageList;
            this.InitializeImageList();
        }

        public FrmViewModel(DEViewModel deVM) {
            this.allVMList = new ArrayList();
            this.InitializeComponent();
            this.lvwViewModel.SmallImageList = ClientData.MyImageList.imageList;
            this.InitializeImageList();
        }

        public void ActiveViewModel(object sender, EventArgs e) {
        }

        private void BuildMenu(string VMName) {
            this.cmuVM.MenuItems.Clear();
            MenuItemEx item = null;
            string str = VMName;
            if (str != null) {
                if (str != "Thyt.TiPLM.DEL.Admin.View.DEViewModel") {
                    if (str != "Empty") {
                        return;
                    }
                } else {
                    this.BuildVMMenu();
                    return;
                }
                item = new MenuItemEx("&New ViewModel", "新建视图(&N)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEW")
                };
                item.Click += new EventHandler(this.NewViewModel);
                this.cmuVM.MenuItems.Add(item);
                item = new MenuItemEx("-", "-", null, null);
                this.cmuVM.MenuItems.Add(item);
                item = new MenuItemEx("ViewModelList Re&Fresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                item.Click += new EventHandler(this.RefreshViewModelList);
                this.cmuVM.MenuItems.Add(item);
            }
        }

        private void BuildVMMenu() {
            this.cmuVM.MenuItems.Clear();
            MenuItemEx item = null;
            bool flag = ((DEViewModel)this.lvwViewModel.SelectedItems[0].Tag).Locker == Guid.Empty;
            bool flag2 = ((DEViewModel)this.lvwViewModel.SelectedItems[0].Tag).Locker == ClientData.LogonUser.Oid;
            bool flag3 = ((DEViewModel)this.lvwViewModel.SelectedItems[0].Tag).IsActive == 'U';
            item = new MenuItemEx("&Open ViewModel", "打开视图模型(&O)", null, null);
            item.Click += new EventHandler(this.OpenViewModel);
            this.cmuVM.MenuItems.Add(item);
            if (flag || flag2) {
                item = new MenuItemEx("&Delete ViewModel", "删除视图模型(&D)", null, null);
                item.Click += new EventHandler(this.DeleteViewModel);
                item.ImageList = ClientData.MyImageList.imageList;
                item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                this.cmuVM.MenuItems.Add(item);
            }
            item = new MenuItemEx("-", "-", null, null);
            this.cmuVM.MenuItems.Add(item);
            item = new MenuItemEx("ViewModel &Property", "视图模型属性(&P)", null, null);
            item.Click += new EventHandler(this.ViewModelProperty);
            this.cmuVM.MenuItems.Add(item);
            if (flag && flag3) {
                item = new MenuItemEx("-", "-", null, null);
                this.cmuVM.MenuItems.Add(item);
                item = new MenuItemEx("&Active ViewModel", "激活视图模型(&A)", null, null);
                item.Click += new EventHandler(this.ActiveViewModel);
                this.cmuVM.MenuItems.Add(item);
            }
            if (!flag3) {
                item = new MenuItemEx("-", "-", null, null);
                this.cmuVM.MenuItems.Add(item);
                item = new MenuItemEx("&UnActive ViewModel", "取消激活(&U)", null, null);
                item.Click += new EventHandler(this.UnactiveViewModel);
                this.cmuVM.MenuItems.Add(item);
            }
        }

        public void DeleteViewModel(object sender, EventArgs e) {
        }


        private void FrmViewModel_Activated(object sender, EventArgs e) {
            this.frmMain.tvwNavigator.SelectedNode = TagForTiModeler.TreeNode_ViewNetwork;
        }

        private void FrmViewModel_Closing(object sender, CancelEventArgs e) {
            this.frmMain.RemoveWnd(this);
        }

        private void FrmViewModel_Load(object sender, EventArgs e) {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEWMODEL");
            ViewModelUL.FillViewModels(this.lvwViewModel, false, iconIndex);
        }


        private void InitializeImageList() {
            ClientData.MyImageList.AddIcon("ICO_VIW_VIEWMODEL");
        }

        private void LocateSelectdVMInTree() {
            DEViewModel tag = (DEViewModel)this.lvwViewModel.SelectedItems[0].Tag;
            for (int i = 0; i < TagForTiModeler.TreeNode_ViewNetwork.Nodes.Count; i++) {
                if (((DEViewModel)TagForTiModeler.TreeNode_ViewNetwork.Nodes[i].Tag).Oid.Equals(tag.Oid)) {
                    this.frmMain.tvwNavigator.SelectedNode = TagForTiModeler.TreeNode_ViewNetwork.Nodes[i];
                    return;
                }
            }
        }

        private void lvwViewModel_ItemActivate(object sender, EventArgs e) {
            if (this.lvwViewModel.SelectedItems.Count == 1) {
                ListViewItem item = this.lvwViewModel.SelectedItems[0];
                if (item.Tag != null) {
                    FrmViewModelProperty property = new FrmViewModelProperty(true, item.Tag as DEViewModel);
                    if (property.ShowDialog() == DialogResult.OK) {
                        item.Tag = property.theVM;
                        item.Text = property.theVM.Name.Trim();
                        string str = "";
                        if (property.theVM.IsActive == 'A') {
                            str = "激活";
                        } else {
                            str = "未激活";
                        }
                        item.SubItems[3].Text = str;
                        item.SubItems[4].Text = property.theVM.Description;
                        for (int i = 0; i < TagForTiModeler.TreeNode_ViewNetwork.Nodes.Count; i++) {
                            if (((DEViewModel)TagForTiModeler.TreeNode_ViewNetwork.Nodes[i].Tag).Oid.Equals(property.theVM.Oid)) {
                                TagForTiModeler.TreeNode_ViewNetwork.Nodes[i].Tag = property.theVM;
                                TagForTiModeler.TreeNode_ViewNetwork.Nodes[i].Text = property.theVM.Name.Trim();
                                break;
                            }
                        }
                    }
                    property.Dispose();
                }
            }
        }

        private void lvwViewModel_MouseUp(object sender, MouseEventArgs e) {
        }

        public void NewViewModel(object sender, EventArgs e) {
            this.frmMain.NewViewModel(sender, e);
        }

        public void OpenViewModel(object sender, EventArgs e) {
            this.LocateSelectdVMInTree();
            this.frmMain.OpenViewModel(sender, e);
        }

        public void RefreshViewModelList(object sender, EventArgs e) {
            this.frmMain.RefreshAllViewModel(sender, e);
        }

        public void UnactiveViewModel(object sender, EventArgs e) {
        }

        public void ViewModelProperty(object sender, EventArgs e) {
            this.lvwViewModel_ItemActivate(sender, e);
        }
    }
}

