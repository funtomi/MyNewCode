using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.Common.Interface.Product;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl.ProductFolder {
    public partial class FrmProuctRole : FormPLM {
        private ArrayList allRoleGroupList;
        private ArrayList allRoleList;
        private DEFolder2 folder;
        private Hashtable hsOldTable;
        private Hashtable hsTable;
        private PLRole plRole;
        private DEFolder2 product;
        private Hashtable roles;
        private ArrayList selusers;
        private bool showallrole;

        public FrmProuctRole() {
            this.plRole = new PLRole();
            this.hsTable = new Hashtable();
            this.hsOldTable = new Hashtable();
            this.selusers = new ArrayList();
            this.InitializeComponent();
        }

        public FrmProuctRole(DEFolder2 Folder, DEFolder2 Product) {
            this.plRole = new PLRole();
            this.hsTable = new Hashtable();
            this.hsOldTable = new Hashtable();
            this.selusers = new ArrayList();
            this.InitializeComponent();
            this.folder = Folder;
            this.product = Product;
            if (this.folder != null) {
                try {
                    this.tvwRole.ImageList = ClientData.MyImageList.imageList;
                    this.lvwTeam.SmallImageList = ClientData.MyImageList.imageList;
                    ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                    this.allRoleList = this.plRole.GetAllRoles();
                    this.allRoleGroupList = this.plRole.GetAllRoleGroups();
                    this.hsTable = (PLProductFolder.RemotingAgent as IProductFolder).GetProductRolesAndMembers(this.folder.Oid);
                    this.roles = (PLProductFolder.RemotingAgent as IProductFolder).GetAllProductRolesInclueParents(this.folder.Oid, Guid.Empty);
                    foreach (Guid guid in this.hsTable.Keys) {
                        ArrayList list = new ArrayList();
                        foreach (Guid guid2 in this.hsTable[guid] as ArrayList) {
                            list.Add(guid2);
                        }
                        this.hsOldTable.Add(guid, list);
                    }
                    this.RefreshRoleNode();
                } catch {
                    base.Close();
                    throw;
                }
            }
        }

        private void AddNode(int imageIndex, int indexadd, DERole deRole) {
            TreeNode node = new TreeNode(deRole.Name, imageIndex, imageIndex) {
                Tag = deRole
            };
            this.tvwRole.Nodes.Add(node);
            if (this.hsTable.ContainsKey(deRole.Oid)) {
                node.ImageIndex = indexadd;
                node.SelectedImageIndex = indexadd;
            } else {
                ArrayList list = new ArrayList();
                foreach (Guid guid in this.roles.Keys) {
                    if (guid == deRole.Oid) {
                        list = this.roles[guid] as ArrayList;
                        if ((list != null) && (list.Count > 0)) {
                            node.ImageIndex = indexadd;
                            node.SelectedImageIndex = indexadd;
                        }
                        break;
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if ((this.tvwRole.SelectedNode == null) || !(this.tvwRole.SelectedNode.Tag is DERole)) {
                MessageBoxPLM.Show("请先选择角色。");
            } else {
                DERole tag = this.tvwRole.SelectedNode.Tag as DERole;
                frmUserOrgProduct product = new frmUserOrgProduct(this.folder, this.product);
                product.ShowDialog();
                if ((product.Tag != null) && (product.Tag is ArrayList)) {
                    ArrayList list = product.Tag as ArrayList;
                    foreach (Guid guid in list) {
                        DEUser userByOid = PLUser.Agent.GetUserByOid(guid);
                        ArrayList list2 = this.hsTable[tag.Oid] as ArrayList;
                        ArrayList list3 = new ArrayList();
                        if ((list2 == null) || !list2.Contains(guid)) {
                            bool flag = true;
                            foreach (Guid guid2 in this.roles.Keys) {
                                if (guid2 == tag.Oid) {
                                    list3 = this.roles[guid2] as ArrayList;
                                    if ((list3 != null) && list3.Contains(guid)) {
                                        foreach (ListViewItem item in this.lvwTeam.Items) {
                                            if (item.Text == userByOid.LogId) {
                                                flag = false;
                                                item.SubItems[2].Text = "";
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (flag) {
                                ListViewItem item2 = this.lvwTeam.Items.Add(userByOid.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                                item2.SubItems.Add(userByOid.Name);
                                item2.Tag = userByOid.Oid;
                            }
                            if (list2 == null) {
                                list2 = new ArrayList {
                                    guid
                                };
                                this.hsTable.Add(tag.Oid, list2);
                            } else {
                                list2.Add(guid);
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            base.Close();
        }

        private void btnDel_Click(object sender, EventArgs e) {
            if ((this.tvwRole.SelectedNode == null) || !(this.tvwRole.SelectedNode.Tag is DERole)) {
                MessageBoxPLM.Show("请先选择角色。");
            } else {
                DERole tag = this.tvwRole.SelectedNode.Tag as DERole;
                foreach (ListViewItem item in this.lvwTeam.SelectedItems) {
                    if (item.ImageIndex == ClientData.MyImageList.GetIconIndex("ICO_MSG_OFFLINE")) {
                        break;
                    }
                    Guid guid = (Guid)item.Tag;
                    ArrayList list = this.hsTable[tag.Oid] as ArrayList;
                    if ((list != null) && list.Contains(guid)) {
                        item.Remove();
                        list.Remove(guid);
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            Hashtable deletedRolesMembers = new Hashtable();
            Hashtable addedRolesMembers = new Hashtable();
            foreach (Guid guid in this.hsTable.Keys) {
                if (!this.hsOldTable.Contains(guid)) {
                    this.hsOldTable.Add(guid, null);
                }
                foreach (Guid guid2 in this.hsOldTable.Keys) {
                    if (guid == guid2) {
                        ArrayList list = this.hsTable[guid] as ArrayList;
                        ArrayList list2 = this.hsOldTable[guid] as ArrayList;
                        if ((list == null) && (list2 != null)) {
                            deletedRolesMembers.Add(guid, list2);
                        } else if ((list != null) && (list2 == null)) {
                            addedRolesMembers.Add(guid, list);
                        } else if ((list != null) && (list2 != null)) {
                            foreach (Guid guid3 in list) {
                                if (!list2.Contains(guid3)) {
                                    if (addedRolesMembers.ContainsKey(guid)) {
                                        (addedRolesMembers[guid] as ArrayList).Add(guid3);
                                    } else {
                                        ArrayList list3 = new ArrayList {
                                            guid3
                                        };
                                        addedRolesMembers.Add(guid, list3);
                                    }
                                }
                            }
                            foreach (Guid guid4 in list2) {
                                if (!list.Contains(guid4)) {
                                    if (deletedRolesMembers.ContainsKey(guid)) {
                                        (deletedRolesMembers[guid] as ArrayList).Add(guid4);
                                    } else {
                                        ArrayList list4 = new ArrayList {
                                            guid4
                                        };
                                        deletedRolesMembers.Add(guid, list4);
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            (PLProductFolder.RemotingAgent as IProductFolder).SaveProudctRoleMembers(this.folder.Oid, deletedRolesMembers, addedRolesMembers, ClientData.LogonUser.Name);
            if (addedRolesMembers.Count > 0) {
                DEFolder2 folderParent = this.GetFolderParent(this.folder);
                ArrayList productTeamMembers = (PLProductFolder.RemotingAgent as IProductFolder).GetProductTeamMembers(folderParent.Oid);
                List<Guid> list6 = new List<Guid>();
                foreach (DEUser user in productTeamMembers) {
                    list6.Add(user.Oid);
                }
                ArrayList addedUserOids = new ArrayList();
                ArrayList deleteUserOids = new ArrayList();
                foreach (Guid guid5 in addedRolesMembers.Keys) {
                    ArrayList list9 = addedRolesMembers[guid5] as ArrayList;
                    if (list9 != null) {
                        foreach (Guid guid6 in list9) {
                            if (!list6.Contains(guid6)) {
                                addedUserOids.Add(guid6);
                            }
                        }
                    }
                }
                if (addedUserOids.Count > 0) {
                    (PLProductFolder.RemotingAgent as IProductFolder).SaveProductTeamMembers(folderParent.Oid, deleteUserOids, addedUserOids);
                }
            }
            base.DialogResult = DialogResult.OK;
        }

        private void ckbSysRole_CheckedChanged(object sender, EventArgs e) {
            if ((this.tvwRole.SelectedNode == null) || !(this.tvwRole.SelectedNode.Tag is DERole)) {
                MessageBoxPLM.Show("请先选择角色。");
            } else {
                DERole tag = this.tvwRole.SelectedNode.Tag as DERole;
                if (tag != null) {
                    for (int i = this.lvwTeam.Items.Count; i > 0; i--) {
                        ListViewItem item = this.lvwTeam.Items[i - 1];
                        if (item.ImageIndex == ClientData.MyImageList.GetIconIndex("ICO_MSG_OFFLINE")) {
                            Guid guid = (Guid)item.Tag;
                            ArrayList list = this.hsTable[tag.Oid] as ArrayList;
                            for (int j = list.Count - 1; j >= 0; j--) {
                                if (((Guid)list[j]) == guid) {
                                    list.Remove(list[j]);
                                    break;
                                }
                            }
                            item.Remove();
                        }
                    }
                    if (!this.ckbSysRole.Checked) {
                        ArrayList list6 = this.hsTable[tag.Oid] as ArrayList;
                        if (list6 != null) {
                            for (int k = list6.Count - 1; k >= 0; k--) {
                                list6.Remove(Guid.Empty);
                            }
                        }
                    } else {
                        ArrayList usersByRole = this.plRole.GetUsersByRole(tag.Oid);
                        ArrayList list3 = this.hsTable[tag.Oid] as ArrayList;
                        ArrayList list4 = new ArrayList();
                        foreach (Guid guid2 in this.roles.Keys) {
                            if (guid2 == tag.Oid) {
                                list4 = this.roles[guid2] as ArrayList;
                                break;
                            }
                        }
                        ArrayList list5 = new ArrayList();
                        foreach (DEUser user in usersByRole) {
                            if (list3 == null) {
                                list3 = new ArrayList {
                                    Guid.Empty,
                                    user.Oid
                                };
                                this.hsTable.Add(tag.Oid, list3);
                            } else {
                                if (!list3.Contains(Guid.Empty)) {
                                    list3.Add(Guid.Empty);
                                }
                                if (!list4.Contains(user.Oid)) {
                                    if (list5.Contains(user.Oid) || list3.Contains(user.Oid)) {
                                        goto Label_0322;
                                    }
                                    list5.Add(user.Oid);
                                    ListViewItem item2 = this.lvwTeam.Items.Add(user.LogId, ClientData.MyImageList.GetIconIndex("ICO_MSG_OFFLINE"));
                                    item2.SubItems.Add(user.Name);
                                    item2.Tag = user.Oid;
                                }
                                if (!list3.Contains(user.Oid)) {
                                    list3.Add(user.Oid);
                                }
                            Label_0322: ;
                            }
                        }
                    }
                }
            }
        }

        private DEFolder2 GetFolderParent(DEFolder2 Folder) {
            if (Folder.FolderType == 'F') {
                return Folder;
            }
            if (Folder.FolderType == 'G') {
                Guid parent = Folder.Parent;
                DEFolder2 folderByOid = PLFolder.RemotingAgent.GetFolderByOid(parent);
                return this.GetFolderParent(folderByOid);
            }
            return null;
        }

        private void RefreshRoleNode() {
            this.tvwRole.Nodes.Clear();
            this.lvwTeam.Items.Clear();
            ArrayList list = new ArrayList();
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            int indexadd = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE_ADD");
            if ((this.allRoleGroupList != null) && (this.allRoleGroupList.Count > 0)) {
                foreach (DERoleGroup group in this.allRoleGroupList) {
                    TreeNode node = new TreeNode(group.Name) {
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                        SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN"),
                        Tag = group
                    };
                    ArrayList allRolesInOneGroup = this.plRole.GetAllRolesInOneGroup(group.Oid);
                    if ((allRolesInOneGroup != null) && (allRolesInOneGroup.Count > 0)) {
                        foreach (DERole role in allRolesInOneGroup) {
                            Guid oid = role.Oid;
                            foreach (DERole role2 in this.allRoleList) {
                                if (role2.Oid == oid) {
                                    TreeNode node2 = new TreeNode(role2.Name, iconIndex, iconIndex) {
                                        Tag = role2
                                    };
                                    if (role2.RoleType == "S") {
                                        node.Nodes.Add(node2);
                                        if (this.hsTable.ContainsKey(role2.Oid)) {
                                            node2.ImageIndex = indexadd;
                                            node2.SelectedImageIndex = indexadd;
                                        } else {
                                            ArrayList list3 = new ArrayList();
                                            foreach (Guid guid2 in this.roles.Keys) {
                                                if (guid2 == oid) {
                                                    list3 = this.roles[guid2] as ArrayList;
                                                    if ((list3 != null) && (list3.Count > 0)) {
                                                        node2.ImageIndex = indexadd;
                                                        node2.SelectedImageIndex = indexadd;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            list.Add(oid);
                        }
                    }
                    this.tvwRole.Nodes.Add(node);
                }
                for (int i = this.tvwRole.Nodes.Count - 1; i >= 0; i--) {
                    TreeNode node3 = this.tvwRole.Nodes[i];
                    if ((node3.Tag is DERoleGroup) && (node3.Nodes.Count <= 0)) {
                        this.tvwRole.Nodes.RemoveAt(i);
                    }
                }
            }
            if ((this.allRoleList != null) && (this.allRoleList.Count > 0)) {
                foreach (DERole role3 in this.allRoleList) {
                    if (((list == null) || !list.Contains(role3.Oid)) && (role3.RoleType == "S")) {
                        this.AddNode(iconIndex, indexadd, role3);
                    }
                }
            }
        }

        private void tvwRole_AfterSelect(object sender, TreeViewEventArgs e) {
            if (this.tvwRole.SelectedNode != null) {
                DERole tag = this.tvwRole.SelectedNode.Tag as DERole;
                if (tag != null) {
                    ArrayList list = this.hsTable[tag.Oid] as ArrayList;
                    ArrayList list2 = new ArrayList();
                    foreach (Guid guid in this.roles.Keys) {
                        if (guid == tag.Oid) {
                            list2 = this.roles[guid] as ArrayList;
                            break;
                        }
                    }
                    this.lvwTeam.Items.Clear();
                    this.selusers.Clear();
                    if ((list != null) && list.Contains(Guid.Empty)) {
                        this.ckbSysRole.Checked = true;
                        this.ckbSysRole_CheckedChanged(this.ckbSysRole, null);
                    } else {
                        this.ckbSysRole.Checked = false;
                    }
                    ArrayList list3 = new ArrayList();
                    foreach (Guid guid2 in list2) {
                        if ((guid2 != Guid.Empty) && !list3.Contains(guid2)) {
                            list3.Add(guid2);
                            DEUser userByOid = PLUser.Agent.GetUserByOid(guid2);
                            ListViewItem item = this.lvwTeam.Items.Add(userByOid.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                            string str = "父级";
                            if ((list != null) && list.Contains(guid2)) {
                                str = "本身";
                            }
                            item.SubItems.AddRange(new string[] { userByOid.Name, str });
                            item.Tag = userByOid.Oid;
                            this.selusers.Add(userByOid.Oid);
                        }
                    }
                }
            }
        }
    }
}

