using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCOrgs : UserControlPLM {
        private EventHandler selectOrg;

        public event SelectPrinHandler PrinSelected;

        public UCOrgs() {
            this.InitializeComponent();
            this.selectOrg = new EventHandler(this.UCOrgs_DoubleClick);
        }

        public void LoadOrgs() {
            this.tvwOrg.Visible = true;
            OrgModelUL.FillOrgTree(this.tvwOrg.Nodes[0], ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG"));
            this.tvwOrg.Nodes[0].Expand();
            this.tvwOrg.DoubleClick += this.selectOrg;
        }

        private void UCOrgs_DoubleClick(object sender, EventArgs e) {
            if ((this.tvwOrg.SelectedNode != null) && (((this.tvwOrg.SelectedNode.Tag != null) && (this.tvwOrg.SelectedNode.Tag is DEOrganization)) && (this.PrinSelected != null))) {
                this.PrinSelected(this.tvwOrg.SelectedNode.Tag as DEOrganization);
            }
        }
    }
}

