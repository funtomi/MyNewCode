using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.UIL.DeskLib.WinControls;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class HeadMainItem : UserControl {

        public HeadMainItem() {
            this.InitializeComponent();
            this.InitializeTreeView();
        }

        public static void CreateTypeTree(TreeView tv, int type) {
            ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
            if (type == 1) {
                ArrayList namefilter = new ArrayList { 
                    "OBJECTSTATE",
                    "PRODUCT",
                    "DOC",
                    "FORM",
                    "RESOURCE",
                    "PPCLASSIFY",
                    "COLLECTION"
                };
                UIDataModel.FillCustomizedClassTree(tv, 0, 0, namefilter);
                foreach (TreeNode node in tv.Nodes) {
                    if (((DEMetaClass)node.Tag).Name == "PPOBJECT") {
                        node.ExpandAll();
                        break;
                    }
                }
            } else if (type == 2) {
                ArrayList list2 = new ArrayList { 
                    "FORM",
                    "PPCLASSIFY",
                    "OBJECTSTATE",
                    "RESOURCE",
                    "PPCLASSIFY",
                    "COLLECTION",
                    "PPCARD",
                    "PPCALCULATE"
                };
                UIDataModel.FillCustomizedClassTree(tv, 0, 0, list2);
                foreach (TreeNode node2 in tv.Nodes) {
                    if (((DEMetaClass)node2.Tag).Name == "PPOBJECT") {
                        node2.ExpandAll();
                        break;
                    }
                }
            } else if (type == 3) {
                ArrayList list3 = new ArrayList { 
                    "OBJECTSTATE",
                    "RESOURCE",
                    "PPCLASSIFY",
                    "COLLECTION",
                    "PPCALCULATE"
                };
                UIDataModel.FillCustomizedClassTree(tv, 0, 0, list3);
                foreach (TreeNode node3 in tv.Nodes) {
                    if (((DEMetaClass)node3.Tag).Name == "PPOBJECT") {
                        node3.ExpandAll();
                        break;
                    }
                }
            }
        }

        private void InitializeTreeView() {
        }
    }
}

