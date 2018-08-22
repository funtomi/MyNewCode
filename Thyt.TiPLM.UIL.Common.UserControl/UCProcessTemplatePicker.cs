    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCProcessTemplatePicker : UserControlPLM {
        private const string BUSINESS_PROCESS = "BusinessPro";


        public event SelectProcessTemplateHandler processTemplateSelected;

        public UCProcessTemplatePicker() {
            this.InitializeComponent();
            this.tvwProcess.ImageList = ClientData.MyImageList.imageList;
            this.lvProcess.SmallImageList = ClientData.MyImageList.imageList;
            this.InitTreeView();
        }


        private void InitTreeView() {
            DELBPMEntityList list2;
            this.tvwProcess.Nodes.Clear();
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEFROOT");
            TreeNode node = new TreeNode("流程模板", iconIndex, iconIndex) {
                Tag = "BusinessPro"
            };
            this.tvwProcess.Nodes.Add(node);
            node.Expand();
            DELBPMEntityList allProcessClasses = new BPMProcessor().GetAllProcessClasses();
            new BPMAdmin().GetProcessDefPropertyList(ClientData.LogonUser.Oid, out list2);
            Hashtable hashtable = new Hashtable();
            foreach (DELProcessDefProperty property in list2) {
                hashtable.Add(property.ID, property);
            }
            Hashtable hashtable2 = new Hashtable();
            foreach (DELProcessClass class2 in allProcessClasses) {
                TreeNode node2 = new TreeNode(class2.Name) {
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                    Tag = class2
                };
                node.Nodes.Add(node2);
                foreach (Guid guid in class2.ProcessIDList) {
                    DELProcessDefProperty property2 = hashtable[guid] as DELProcessDefProperty;
                    if (property2 != null) {
                        TreeNode node3 = new TreeNode(property2.Name) {
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            Tag = property2
                        };
                        hashtable2.Add(property2.ID, property2);
                        node2.Nodes.Add(node3);
                    }
                }
            }
            for (int i = 0; i < list2.Count; i++) {
                DELProcessDefProperty property3 = (DELProcessDefProperty)list2[i];
                if (((property3.Reserve2 != "FORPPM") && (property3.IsVisible == 1)) && (!property3.State.Equals("Deleted") && !hashtable2.ContainsKey(property3.ID))) {
                    TreeNode node4 = new TreeNode(property3.Name) {
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                        SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                        Tag = property3
                    };
                    hashtable2.Add(property3.ID, property3);
                    node.Nodes.Add(node4);
                }
            }
        }

        private void lvProcess_DoubleClick(object sender, EventArgs e) {
            if ((this.lvProcess.SelectedItems.Count != 0) && ((this.lvProcess.SelectedItems[0].Tag is DELProcessDefProperty) && (this.processTemplateSelected != null))) {
                this.processTemplateSelected((DELProcessDefProperty)this.lvProcess.SelectedItems[0].Tag);
            }
        }

        private void tvwProcess_AfterSelect(object sender, TreeViewEventArgs e) {
            this.lvProcess.Items.Clear();
            foreach (TreeNode node in e.Node.Nodes) {
                if (node.Tag is DELProcessClass) {
                    DELProcessClass tag = (DELProcessClass)node.Tag;
                    this.lvProcess.Items.Add(tag.Name, node.ImageIndex).Tag = tag;
                } else {
                    DELProcessDefProperty property = (DELProcessDefProperty)node.Tag;
                    ListViewItem item2 = this.lvProcess.Items.Add(property.Name, node.SelectedImageIndex);
                    item2.SubItems.Add(property.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    item2.Tag = property;
                }
            }
        }

        private void tvwProcess_DoubleClick(object sender, EventArgs e) {
            if ((this.tvwProcess.SelectedNode != null) && ((this.tvwProcess.SelectedNode.Tag is DELProcessDefProperty) && (this.processTemplateSelected != null))) {
                this.processTemplateSelected((DELProcessDefProperty)this.tvwProcess.SelectedNode.Tag);
            }
        }
    }
}

