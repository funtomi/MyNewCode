    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
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
    public partial class frmUserOrgProduct : FormPLM {
        private Dictionary<string, object> attrs;

        private UCOrgs ctlorg;
        private UCUsers ctluser;
        private DataTable dt;
        private bool IsTeam;
        private bool SelUser;
        private bool ShowCal;

        public frmUserOrgProduct() {
            this.SelUser = true;
            this.dt = new DataTable();
            this.InitializeComponent();
        }

        public frmUserOrgProduct(DEFolder2 folder, DEFolder2 product) {
            this.SelUser = true;
            this.dt = new DataTable();
            this.InitializeComponent();
            if (folder.Oid != Guid.Empty) {
                this.ctluser = new UCUsers();
                this.ctluser.showSysAdmin = false;
                this.ctluser.Dock = DockStyle.Fill;
                this.ctluser.lvwUser.MultiSelect = true;
                this.tabPage2.Controls.Add(this.ctluser);
                this.lvwTeam.MultiSelect = true;
                ArrayList productTeamMembers = (PLProductFolder.RemotingAgent as IProductFolder).GetProductTeamMembers(product.Oid);
                if (productTeamMembers.Count > 0) {
                    this.tabControl1.SelectedTab = this.tabPage1;
                    foreach (DEUser user in productTeamMembers) {
                        ListViewItem item = this.lvwTeam.Items.Add(user.LogId, ClientData.MyImageList.GetIconIndex("ICO_RSP_USER"));
                        item.SubItems.Add(user.Name);
                        item.Tag = user;
                    }
                } else {
                    this.tabControl1.SelectedTab = this.tabPage2;
                }
                this.Text = "选择用户";
                this.splitContainer.Panel2MinSize = 0;
                this.splitContainer.Panel2Collapsed = true;
                base.Width = 400;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            base.Close();
        }

        private void btnOk_Click(object sender, EventArgs e) {
            ArrayList list = new ArrayList();
            if (this.SelUser && (this.lvwTeam.SelectedItems.Count > 0)) {
                foreach (ListViewItem item in this.lvwTeam.SelectedItems) {
                    DEUser tag = item.Tag as DEUser;
                    list.Add(tag.Oid);
                }
            }
            foreach (ListViewItem item2 in this.ctluser.lvwUser.SelectedItems) {
                DEUser user2 = item2.Tag as DEUser;
                if (!list.Contains(user2.Oid)) {
                    list.Add(user2.Oid);
                }
            }
            base.Tag = list;
            base.DialogResult = DialogResult.OK;
        }


    }
}

