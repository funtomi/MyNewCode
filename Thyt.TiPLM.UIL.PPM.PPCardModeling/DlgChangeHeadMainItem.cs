using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.UIL.DeskLib.WinControls;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgChangeHeadMainItem : Form {

        private CLCardTemplate m_tp;

        public DlgChangeHeadMainItem() {
            this.InitializeComponent();
        }

        public DlgChangeHeadMainItem(CLCardTemplate tp) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.InitializeUI();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            string name = "";
            string headOrMainFromRevDesc = "";
            string str3 = "";
            if (this.tvHead.TextValue.Trim() != "") {
                if (ModelContext.MetaModel.GetClassByLabel(this.tvHead.TextValue.Trim()) == null) {
                    MessageBox.Show("您填写的头对象中文类名不正确！");
                    return;
                }
                name = ModelContext.MetaModel.GetClassByLabel(this.tvHead.TextValue.Trim()).Name;
            }
            headOrMainFromRevDesc = PLCardTemplate.GetHeadOrMainFromRevDesc(this.m_tp.Item.Revision, 1);
            str3 = name + ":" + headOrMainFromRevDesc;
            this.m_tp.Item.Revision.ReleaseDesc = str3;
            base.DialogResult = DialogResult.OK;
        }

        public static void CreateTypeTree(TreeView tv) {
            ArrayList namefilter = new ArrayList { 
                "FORM",
                "PPCLASSIFY",
                "OBJECTSTATE",
                "RESOURCE",
                "PPCLASSIFY",
                "COLLECTION",
                "PRODUCT",
                "PPCARD",
                "PPCALCULATE",
                "PPSIGNTEMPLATE"
            };
            UIDataModel.FillCustomizedClassTree(tv, 0, 0, namefilter);
            foreach (TreeNode node in tv.Nodes) {
                if (((DEMetaClass)node.Tag).Name == "PPOBJECT") {
                    node.ExpandAll();
                    break;
                }
            }
        }

        private void InitializeUI() {
            string headOrMainFromRevDesc = PLCardTemplate.GetHeadOrMainFromRevDesc(this.m_tp.Item.Revision, 0);
            string classname = PLCardTemplate.GetHeadOrMainFromRevDesc(this.m_tp.Item.Revision, 1);
            if ((headOrMainFromRevDesc != "") && (ModelContext.MetaModel.GetClassLabel(headOrMainFromRevDesc) != null)) {
                headOrMainFromRevDesc = ModelContext.MetaModel.GetClassLabel(headOrMainFromRevDesc);
            }
            if ((classname != "") && (ModelContext.MetaModel.GetClassLabel(classname) != null)) {
                classname = ModelContext.MetaModel.GetClassLabel(classname);
            }
            this.tvHead.TextValue = headOrMainFromRevDesc;
            this.tvMain.TextValue = classname;
            CreateTypeTree(this.tvHead.treeView);
        }
    }
}

