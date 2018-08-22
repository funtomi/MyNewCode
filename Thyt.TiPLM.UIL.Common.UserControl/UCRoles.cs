using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCRoles : UserControlPLM {
        public DERoleGroup currentRoleGroup;
        public DEAdminRole deAdminRole;

        public event SelectPrinHandler PrinSelected;

        public UCRoles() {
            this.InitializeComponent();
            this.lvwRole.SmallImageList = ClientData.MyImageList.imageList;
        }

        private void FillRolesOfOneGroup(DERoleGroup deRoleGroup) {
            this.lvwRole.Items.Clear();
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE");
            ListViewItem item = new ListViewItem("返回上一层", iconIndex) {
                Tag = "返回上一层"
            };
            this.lvwRole.Items.Add(item);
            int imageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            ArrayList allRolesInOneGroup = new PLRole().GetAllRolesInOneGroup(deRoleGroup.Oid);
            ArrayList members = null;
            if ((this.deAdminRole != null) && (this.deAdminRole.ParentAdminRole != Guid.Empty)) {
                try {
                    members = new PLAdminRole().GetMembers(this.deAdminRole.Oid, "Role");
                } catch (PLMException exception) {
                    MessageBoxPLM.Show(exception.Message, "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
            if ((allRolesInOneGroup != null) && (allRolesInOneGroup.Count > 0)) {
                foreach (DERole role3 in allRolesInOneGroup) {
                    if (members != null) {
                        bool flag = false;
                        foreach (DERole role4 in members) {
                            if (role4.Oid == role3.Oid) {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag) {
                            continue;
                        }
                    }
                    try {
                        ListViewItem item2 = new ListViewItem(role3.Name, imageIndex);
                        switch (role3.Status) {
                            case 'D':
                                item2.SubItems.Add("删除");
                                break;

                            case 'F':
                                item2.SubItems.Add("冻结");
                                break;

                            case 'S':
                                item2.SubItems.Add("系统");
                                break;

                            case 'A':
                                item2.SubItems.Add("可用");
                                break;
                        }
                        string text = "通用角色";
                        switch (role3.RoleType) {
                            case "P":
                                text = "项目角色";
                                break;

                            case "C":
                                text = "通用角色";
                                break;

                            case "S":
                                text = "产品共享区角色";
                                break;

                            default:
                                text = "通用角色";
                                break;
                        }
                        item2.SubItems.Add(text);
                        if (role3.Status.Equals('S')) {
                            item2.SubItems.Add("");
                            item2.SubItems.Add("");
                        } else {
                            item2.SubItems.Add(role3.Creator);
                            item2.SubItems.Add(role3.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        item2.SubItems.Add(role3.Description);
                        item2.Tag = role3;
                        this.lvwRole.Items.Add(item2);
                    } catch {
                    }
                }
            }
            this.currentRoleGroup = deRoleGroup;
        }

        public void LoadRoles() {
            this.lvwRole.Columns.Add("名称", 100, HorizontalAlignment.Left);
            this.lvwRole.Columns.Add("状态", 50, HorizontalAlignment.Left);
            this.lvwRole.Columns.Add("创建人", 80, HorizontalAlignment.Left);
            this.lvwRole.Columns.Add("创建时间", 150, HorizontalAlignment.Left);
            this.lvwRole.Columns.Add("描述", 200, HorizontalAlignment.Left);
            PLAdminRole role = new PLAdminRole();
            object adminRoleByUserId = role.GetAdminRoleByUserId(ClientData.LogonUser.Oid);
            if (adminRoleByUserId != null) {
                this.deAdminRole = role.GetAdminRole((adminRoleByUserId as DEAdminRoleGrantUser).AdminRole);
            }
        }

        private void lvwRole_ItemActivate(object sender, EventArgs e) {
            if (this.lvwRole.SelectedItems.Count > 0) {
                if (this.lvwRole.SelectedItems[0].Tag is DERole) {
                    if (this.PrinSelected != null) {
                        this.PrinSelected(this.lvwRole.SelectedItems[0].Tag as DERole);
                    }
                } else if (this.lvwRole.SelectedItems[0].Tag is DERoleGroup) {
                    this.FillRolesOfOneGroup((DERoleGroup)this.lvwRole.SelectedItems[0].Tag);
                } else if (((this.currentRoleGroup != null) && (this.lvwRole.SelectedItems[0].Tag is string)) && this.lvwRole.SelectedItems[0].Tag.ToString().Equals("返回上一层")) {
                    int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                    this.lvwRole.Items.Clear();
                    if (this.deAdminRole == null) {
                        AdminRoleUL.FillAllRoles(this.lvwRole, iconIndex, false, true);
                    } else if (this.deAdminRole.ParentAdminRole == Guid.Empty) {
                        AdminRoleUL.FillAllRoles(this.lvwRole, iconIndex, false, true);
                    } else {
                        AdminRoleUL.FillAdminRoleRoles(this.lvwRole, iconIndex, this.deAdminRole.Oid, true);
                    }
                    this.currentRoleGroup = null;
                }
            }
        }

        public void RoleRefresh(object sender, EventArgs e) {
            this.lvwRole.Items.Clear();
            if (this.deAdminRole == null) {
                PLAdminRole role = new PLAdminRole();
                object adminRoleByUserId = role.GetAdminRoleByUserId(ClientData.LogonUser.Oid);
                if (adminRoleByUserId != null) {
                    this.deAdminRole = role.GetAdminRole((adminRoleByUserId as DEAdminRoleGrantUser).AdminRole);
                }
            }
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            if (this.deAdminRole == null) {
                AdminRoleUL.FillAllRoles(this.lvwRole, iconIndex, false, true);
            } else if (this.deAdminRole.ParentAdminRole == Guid.Empty) {
                AdminRoleUL.FillAllRoles(this.lvwRole, iconIndex, false, true);
            } else {
                AdminRoleUL.FillAdminRoleRoles(this.lvwRole, iconIndex, this.deAdminRole.Oid, true);
            }
        }
    }
}

