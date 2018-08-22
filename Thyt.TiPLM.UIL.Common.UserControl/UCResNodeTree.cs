using System;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.DEL.Resource;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.Resource;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCResNodeTree : UserControlPLM {

        public event SelectResNodeHandler ResNodeSelected;

        public UCResNodeTree() {
            this.InitializeComponent();
            this.InitializeResTree(ResClsType.All);
        }

        public UCResNodeTree(ResClsType rtype) {
            this.InitializeComponent();
            this.InitializeResTree(rtype);
        }

        private void InitializeResTree(ResClsType rtype) {
            this.tvwResNode.ImageList = ClientData.MyImageList.imageList;
            this.tvwResNode.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NODE");
            this.tvwResNode.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NODE");
            TreeNode node = new TreeNode("工程资源", ClientData.MyImageList.GetIconIndex("ICO_RES_FDL"), ClientData.MyImageList.GetIconIndex("ICO_RES_FDL_OPEN"));
            this.tvwResNode.Nodes.Add(node);
            PLResFolder folder = new PLResFolder();
            try {
                DataSet allFolders = folder.GetAllFolders(ClientData.LogonUser.Oid);
                if (allFolders.Tables.Count > 0) {
                    if (rtype != ResClsType.All) {
                        for (int i = allFolders.Tables[0].Rows.Count - 1; i >= 0; i--) {
                            DEMetaClass metaClass;
                            DataRow row = allFolders.Tables[0].Rows[i];
                            if (row["PLM_NODE_TYPE"].ToString() == "O") {
                                metaClass = ModelContext.GetMetaClass(row["PLM_CLASS_NAME"].ToString());
                                switch (rtype) {
                                    case ResClsType.OutRes:
                                        if (!metaClass.IsOuterResClass) {
                                            allFolders.Tables[0].Rows.RemoveAt(i);
                                        }
                                        break;

                                    case ResClsType.RefRes:
                                        if (!metaClass.IsRefResClass) {
                                            allFolders.Tables[0].Rows.RemoveAt(i);
                                        }
                                        break;

                                    case ResClsType.TabRes:
                                        goto Label_0185;
                                }
                            }
                            continue;
                        Label_0185:
                            if (metaClass.IsRefResClass || metaClass.IsOuterResClass) {
                                allFolders.Tables[0].Rows.RemoveAt(i);
                            }
                        }
                    }
                    ResFunc.FillMyTreeByUser(node, allFolders, ClientData.MyImageList);
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void tvwResNode_AfterSelect(object sender, TreeViewEventArgs e) {
            if (this.ResNodeSelected != null) {
                this.ResNodeSelected(e.Node.Tag as DEResFolder);
            }
        }

        public TreeView ResNodeTree {
            get {
                return this.tvwResNode;
            }
        }
        public TreeNode SelectedNode {
            get {
                return this.tvwResNode.SelectedNode;
            }
        }
    }
}

