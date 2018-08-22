using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.Common.Interface.Product;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Common.UserControl;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl.ProductFolder {
    public partial class FrmProductTeam : FormPLM {

        private UCUsers ctluser;
        private DEFolder2 folder;
        public SortableListView lvwTeam;
        private ArrayList OldTeams;

        private ArrayList Teams;

        public FrmProductTeam() {
            this.Teams = new ArrayList();
            this.OldTeams = new ArrayList();
            this.InitializeComponent();
        }

        public FrmProductTeam(DEFolder2 Folder) {
            this.Teams = new ArrayList();
            this.OldTeams = new ArrayList();
            this.InitializeComponent();
            this.folder = Folder;
            if (this.folder != null) {
                this.ctluser = new UCUsers();
                this.ctluser.showSysAdmin = false;
                this.ctluser.Dock = DockStyle.Fill;
                this.ctluser.PrinSelected += new SelectPrinHandler(this.ctluser_PrinSelected);
                this.ctluser.lvwUser.MultiSelect = true;
                this.splitContainer.Panel1.Controls.Add(this.ctluser);
                foreach (DEUser user in (PLProductFolder.RemotingAgent as IProductFolder).GetProductTeamMembers(this.folder.Oid)) {
                    ListViewItem item = this.lvwTeam.Items.Add(user.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                    item.SubItems.Add(user.Name);
                    item.Tag = user.Oid;
                    this.Teams.Add(user.Oid);
                    this.OldTeams.Add(user.Oid);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            if (this.ctluser.lvwUser.SelectedItems.Count > 0) {
                foreach (ListViewItem item in this.ctluser.lvwUser.SelectedItems) {
                    DEUser tag = item.Tag as DEUser;
                    if (!this.Teams.Contains(tag.Oid)) {
                        this.Teams.Add(tag.Oid);
                        ListViewItem item2 = this.lvwTeam.Items.Add(tag.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                        item2.SubItems.Add(tag.Name);
                        item2.Tag = tag.Oid;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            base.DialogResult = DialogResult.Cancel;
        }

        private void btnDel_Click(object sender, EventArgs e) {
            if (this.lvwTeam.SelectedItems.Count > 0) {
                foreach (ListViewItem item in this.lvwTeam.SelectedItems) {
                    Guid tag = (Guid)item.Tag;
                    this.Teams.Remove(tag);
                    item.Remove();
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e) {
            bool flag = false;
            ArrayList addedUserOids = new ArrayList();
            ArrayList deleteUserOids = new ArrayList();
            foreach (Guid guid in this.Teams) {
                if (!this.OldTeams.Contains(guid)) {
                    addedUserOids.Add(guid);
                    flag = true;
                }
            }
            foreach (Guid guid2 in this.OldTeams) {
                if (!this.Teams.Contains(guid2)) {
                    deleteUserOids.Add(guid2);
                    flag = true;
                }
            }
            if (flag) {
                (PLProductFolder.RemotingAgent as IProductFolder).SaveProductTeamMembers(this.folder.Oid, deleteUserOids, addedUserOids);
            }
            base.DialogResult = DialogResult.OK;
        }

        private void ctluser_PrinSelected(DEPrincipal prin) {
            if (prin is DEUser) {
                DEUser user1 = (DEUser)prin;
                base.Tag = prin;
                this.btnAdd_Click(null, null);
            }
        }

    }
}

