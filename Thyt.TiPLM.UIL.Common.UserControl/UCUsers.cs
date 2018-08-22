using DevExpress.XtraEditors.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCUsers : UserControlPLM {

        private int lastFoundItemIndex = -1;

        private TreeViewEventHandler selectOrg;
        public bool showSysAdmin = true;
        private UserViewMode viewMode;
        public event SelectPrinHandler PrinSelected;
        public event AddProjectWorkTime UsersLoadCompleted;

        public UCUsers() {
            this.InitializeComponent();
            this.lvwUser.SmallImageList = this.tvwOrg.ImageList = this.toolStrip1.ImageList = ClientData.MyImageList.imageList;
            this.btnOrg.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
            this.btnUser.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_USER");
            this.selectOrg = new TreeViewEventHandler(this.tvwOrg_AfterSelect);
            this.DisplayUsers();
        }

        private void btnOrg_Click(object sender, EventArgs e) {
            if (this.viewMode == UserViewMode.ByList) {
                this.tvwOrg.SelectedNode = null;
                this.viewMode = UserViewMode.ByOrgTree;
                this.btnOrg.Checked = true;
                this.btnUser.Checked = false;
                this.DisplayUsers();
            }
        }

        private void btnUser_Click(object sender, EventArgs e) {
            if (this.viewMode == UserViewMode.ByOrgTree) {
                this.viewMode = UserViewMode.ByList;
                this.btnUser.Checked = true;
                this.btnOrg.Checked = false;
                this.DisplayUsers();
                if (this.UsersLoadCompleted != null) {
                    this.UsersLoadCompleted();
                }
            }
        }

        private void DisplayUsers() {
            this.lvwUser.Items.Clear();
            List<string> list = new List<string>();
            switch (this.viewMode) {
                case UserViewMode.ByOrgTree:
                    this.tvwOrg.Visible = true;
                    OrgModelUL.FillOrgTree(this.tvwOrg.Nodes[0], ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG"));
                    this.tvwOrg.Nodes[0].Expand();
                    this.tvwOrg.AfterSelect += this.selectOrg;
                    break;

                case UserViewMode.ByList: {
                        this.tvwOrg.Visible = false;
                        this.tvwOrg.AfterSelect -= this.selectOrg;
                        ArrayList allUsers = null;
                        try {
                            PLUser user = new PLUser();
                            allUsers = user.GetAllUsers();
                            if (this.showSysAdmin) {
                                string sysAdmin = user.GetSysAdmin();
                                DEUser byLogId = user.GetByLogId(sysAdmin);
                                allUsers.Add(byLogId);
                            }
                        } catch (Exception exception) {
                            PrintException.Print(exception);
                            return;
                        }
                        if (allUsers != null) {
                            foreach (DEUser user3 in allUsers) {
                                ListViewItem item = this.lvwUser.Items.Add(user3.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                                item.SubItems.Add(user3.Name);
                                item.Tag = user3;
                                if (!list.Contains(user3.Name)) {
                                    list.Add(user3.Name);
                                }
                                if (!list.Contains(user3.LogId)) {
                                    list.Add(user3.LogId);
                                }
                            }
                        }
                        OrgModelUL.ChangeFrozenIco(this.lvwUser);
                        break;
                    }
            }
            this.txtUser.AutoCompleteCustomSource.Clear();
            this.txtUser.AutoCompleteCustomSource.AddRange(list.ToArray());
        }

        private void lvwUser_ItemActivate(object sender, EventArgs e) {
            if (((this.lvwUser.SelectedItems.Count > 0) && (this.lvwUser.SelectedItems[0].Tag is DEUser)) && (this.PrinSelected != null)) {
                this.PrinSelected(this.lvwUser.SelectedItems[0].Tag as DEUser);
            }
        }

        private void tvwOrg_AfterSelect(object sender, TreeViewEventArgs e) {
            if ((e.Node.Tag != null) && (e.Node.Tag is DEOrganization)) {
                this.lvwUser.Items.Clear();
                ArrayList members = null;
                try {
                    members = new PLOrganization().GetMembers(((DEOrganization)e.Node.Tag).Oid);
                } catch (Exception exception) {
                    PrintException.Print(exception);
                    return;
                }
                if (members != null) {
                    foreach (DEUser user in members) {
                        ListViewItem item = this.lvwUser.Items.Add(user.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                        item.SubItems.Add(user.Name);
                        item.Tag = user;
                    }
                }
                OrgModelUL.ChangeFrozenIco(this.lvwUser);
                if (this.UsersLoadCompleted != null) {
                    this.UsersLoadCompleted();
                }
            }
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                if (this.lastFoundItemIndex < -1) {
                    foreach (ListViewItem item in this.lvwUser.Items) {
                        DEUser tag = item.Tag as DEUser;
                        if ((tag != null) && ((tag.LogId.Trim().ToUpper() == this.txtUser.Text.Trim().ToUpper()) || (tag.Name.Trim().ToUpper() == this.txtUser.Text.Trim().ToUpper()))) {
                            this.lvwUser.EnsureVisible(item.Index);
                            item.Selected = true;
                            this.lastFoundItemIndex = item.Index;
                            break;
                        }
                    }
                } else {
                    for (int i = 0; i < this.lvwUser.Items.Count; i++) {
                        int index = ((this.lastFoundItemIndex + i) + 1) % this.lvwUser.Items.Count;
                        DEUser user2 = this.lvwUser.Items[index].Tag as DEUser;
                        if ((user2 != null) && ((user2.LogId.Trim().ToUpper() == this.txtUser.Text.Trim().ToUpper()) || (user2.Name.Trim().ToUpper() == this.txtUser.Text.Trim().ToUpper()))) {
                            this.lvwUser.EnsureVisible(index);
                            this.lvwUser.Items[index].Selected = true;
                            this.lastFoundItemIndex = index;
                            return;
                        }
                    }
                }
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e) {
        }

        private enum UserViewMode {
            ByOrgTree,
            ByList
        }
    }
}

