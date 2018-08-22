    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCSelectClass : UltraTextEditor {
        private SelectClassHandler handler;
        private DoubClickClassHandler handler2;
        private int height;
        private UCClassTree ucClassTree;

        public event DoubClickClassHandler DoubClickClassed;

        public event SelectClassHandler SelectedClassChanged;

        public UCSelectClass() {
            this.height = 190;
            this.InitializeComponent();
        }

        public UCSelectClass(bool leafClassOnly)
            : this(leafClassOnly, SelectClassConstraint.None) {
        }

        public UCSelectClass(IContainer container)
            : this() {
            container.Add(this);
        }

        public UCSelectClass(bool leafClassOnly, SelectClassConstraint constraint)
            : this((string)null, leafClassOnly, constraint) {
        }

        public UCSelectClass(bool IsQuickSch, bool leafClassOnly, SelectClassConstraint constraint)
            : this() {
            this.ucClassTree = new UCClassTree(null, leafClassOnly, constraint);
            this.ucClassTree.ClassTree.Nodes.RemoveByKey("资源");
            this.ucClassTree.ClassTree.Nodes.RemoveByKey("业务状态");
            for (int i = 0; i < this.ucClassTree.ClassTree.Nodes.Count; i++) {
                TreeNode node = this.ucClassTree.ClassTree.Nodes[i];
                DEMetaClass tag = node.Tag as DEMetaClass;
                if (tag != null) {
                    if (tag.Name == "RESOURCE") {
                        this.ucClassTree.ClassTree.Nodes.Remove(node);
                        i--;
                    } else {
                        this.NodeClear(node);
                        if ((node.Nodes.Count == 0) && ((!tag.IsSearchable || !ModelContext.MetaModel.IsVisibleCustomizedClass(tag.Name)) || (tag.IsGrantable && (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, tag.Name, Guid.Empty, "ClaRel_SEARCH") != 1)))) {
                            this.ucClassTree.ClassTree.Nodes.Remove(node);
                            i--;
                        }
                    }
                }
            }
            DropDownEditorButton button = base.ButtonsRight["SelectClass"] as DropDownEditorButton;
            button.Control = this.ucClassTree;
            this.handler = new SelectClassHandler(this.ucClassTree_ClassSelected);
            this.ucClassTree.ClassSelected += this.handler;
            this.handler2 = new DoubClickClassHandler(this.ucClassTree_DoubleClikedcls);
            this.ucClassTree.DoubClicked += this.handler2;
        }

        public UCSelectClass(string className, bool leafClassOnly, SelectClassConstraint constraint)
            : this() {
            this.ucClassTree = new UCClassTree(((className == null) || (className == "")) ? null : className, leafClassOnly, constraint);
            DropDownEditorButton button = base.ButtonsRight["SelectClass"] as DropDownEditorButton;
            button.Control = this.ucClassTree;
            this.handler = new SelectClassHandler(this.ucClassTree_ClassSelected);
            this.ucClassTree.ClassSelected += this.handler;
            this.handler2 = new DoubClickClassHandler(this.ucClassTree_DoubleClikedcls);
            this.ucClassTree.DoubClicked += this.handler2;
        }

        private void NodeClear(TreeNode node) {
            if (node.Nodes.Count != 0) {
                for (int i = 0; i < node.Nodes.Count; i++) {
                    TreeNode node2 = node.Nodes[i];
                    DEMetaClass tag = node2.Tag as DEMetaClass;
                    if (tag != null) {
                        this.NodeClear(node2);
                        if ((node2.Nodes.Count == 0) && ((!tag.IsSearchable || !ModelContext.MetaModel.IsVisibleCustomizedClass(tag.Name)) || (tag.IsGrantable && (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, tag.Name, Guid.Empty, "ClaRel_SEARCH") != 1)))) {
                            node.Nodes.Remove(node2);
                            i--;
                        }
                    }
                }
            }
        }

        public void SetValue(Guid classOid) {
            if (classOid == Guid.Empty) {
                base.Tag = null;
                base.textValue = "";
            } else {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(classOid);
                base.Tag = class2;
                base.textValue = class2.Label;
                this.ucClassTree.SelectClass(class2.Name);
            }
            this.Refresh();
        }

        public void SetValue(string className) {
            if ((className == null) || (className.Trim() == "")) {
                base.Tag = null;
                base.textValue = "";
            } else {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(className);
                if (class2 != null) {
                    base.Tag = class2;
                    base.textValue = class2.Label;
                    this.ucClassTree.SelectClass(className);
                } else {
                    base.Tag = null;
                    base.textValue = "";
                }
            }
            this.Refresh();
        }

        public void ShowClassTree(string className) {
            this.ucClassTree.ShowClassTree(className);
        }

        private void ucClassTree_ClassSelected(DEMetaClass meta) {
            bool flag = false;
            if (meta == null) {
                if ((meta == null) && (base.Tag != null)) {
                    flag = true;
                }
                this.Text = "";
                base.Tag = null;
            } else {
                if ((base.Tag == null) || (meta.Oid != ((DEMeta)base.Tag).Oid)) {
                    flag = true;
                }
                this.Text = meta.Label;
                base.Tag = meta;
            }
            if (flag && (this.SelectedClassChanged != null)) {
                this.SelectedClassChanged(meta);
            }
        }

        private void ucClassTree_DoubleClikedcls() {
            if (this.DoubClickClassed != null) {
                this.DoubClickClassed();
            }
        }

        private void UCSelectClass_SizeChanged(object sender, EventArgs e) {
            if (base.Width > 170) {
                this.ucClassTree.Width = base.Width;
            }
        }

        public DEMetaClass SelectedClass {
            get {
                return (base.Tag as DEMetaClass);
            }
        }
    }
}

