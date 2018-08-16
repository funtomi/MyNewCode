
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler {
    public partial class FrmNavigator : Form
    {

        public FrmNavigator(FrmMain frmMain)
        {
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_TIMODELER_NAV_PIC");
            this.frmMain = frmMain;
            this.lvwNavigater.SmallImageList = ClientData.MyImageList.imageList;
        }

        private void FrmNavigator_Closing(object sender, CancelEventArgs e)
        {
            base.Visible = false;
            e.Cancel = true;
        }

        private void lvwNavigater_ItemActivate(object sender, EventArgs e)
        {
            if (this.lvwNavigater.SelectedItems.Count != 0)
            {
                this.frmMain.isDoubleClick = true;
                this.frmMain.tvwNavigator.SelectedNode = (TreeNode) this.lvwNavigater.SelectedItems[0].Tag;
                this.frmMain.isDoubleClick = false;
            }
        }

        private void lvwNavigater_MouseUp(object sender, MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Right) && (e.Clicks == 1)) && (this.lvwNavigater.SelectedItems.Count == 1))
            {
                string menuName = null;
                TreeNode tag = (TreeNode) this.lvwNavigater.SelectedItems[0].Tag;
                if (tag.Tag is string)
                {
                    menuName = tag.Tag.ToString();
                }
                else if (tag.Tag is DEMetaClass)
                {
                    TreeNode parent = tag.Parent;
                    while (parent.Parent.Tag.ToString() != "EnterpriseModel")
                    {
                        parent = parent.Parent;
                    }
                    if (parent.Tag.Equals("PPROOT"))
                    {
                        menuName = "PPCARD";
                        if (((DEMetaClass) tag.Tag).Name == "PPCARD")
                        {
                            menuName = "PPROOT";
                        }
                    }
                    else if (parent.Tag.Equals("RPTROOT"))
                    {
                        menuName = "RPTROOT";
                    }
                    else
                    {
                        menuName = tag.Tag.GetType().ToString();
                    }
                }
                else
                {
                    if (tag.Tag is DELProcessDefProperty)
                    {
                        return;
                    }
                    menuName = tag.Tag.GetType().ToString();
                }
                ContextMenu menu = this.frmMain.BuildMenu(menuName, tag);
                if (menu.MenuItems.Count > 0)
                {
                    menu.Show(this.lvwNavigater, new Point(e.X, e.Y));
                }
            }
        }

        private void lvwNavigater_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void UpdateListView(TreeNode node)
        {
            this.lvwNavigater.Items.Clear();
            ListViewItem item = null;
            foreach (TreeNode node2 in node.Nodes)
            {
                item = this.lvwNavigater.Items.Add(node2.Text, node2.ImageIndex);
                if (node2.Tag is DELProcessDefProperty)
                {
                    item.SubItems.Add(((DELProcessDefProperty) node2.Tag).Description);
                }
                else if (node2.Tag is DERole)
                {
                    item.SubItems.Add(((DERole) node2.Tag).Description);
                }
                else
                {
                    item.SubItems.Add("");
                }
                item.Tag = node2;
            }
            foreach (ListViewItem item2 in this.lvwNavigater.Items)
            {
                switch (((TreeNode) item2.Tag).Tag.ToString())
                {
                    case "DataModel":
                        item2.SubItems[1].Text = "数据模型管理";
                        break;

                    case "OrgModel":
                        item2.SubItems[1].Text = "包括对企业组织、人员、角色及其权限的管理";
                        break;

                    case "BusinessPro":
                        item2.SubItems[1].Text = "业务过程管理";
                        break;

                    case "ViewManage":
                        item2.SubItems[1].Text = "视图模型管理";
                        break;

                    case "ExternalResource":
                        item2.SubItems[1].Text = "包括对工具软件、浏览器、编辑器和文件类型的管理";
                        break;

                    case "Folder":
                        item2.SubItems[1].Text = "对公共文件夹进行管理";
                        break;

                    case "PPROOT":
                        item2.SubItems[1].Text = "工艺卡片模板管理";
                        break;

                    case "EXTENDED_MODEL":
                        item2.SubItems[1].Text = "创建扩展企业模型，并且将其绑定到插件上";
                        break;
                }
            }
        }
    }
}

