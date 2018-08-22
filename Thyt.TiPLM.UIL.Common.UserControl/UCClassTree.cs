    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    internal partial class UCClassTree : UserControlPLM
    {
        private bool b_start;
        private SelectClassConstraint constraint;
        private bool leafClassOnly;

        public event SelectClassHandler ClassSelected;

        public event DoubClickClassHandler DoubClicked;

        public UCClassTree(bool leafClassOnly, SelectClassConstraint constraint)
        {
            this.leafClassOnly = true;
            this.b_start = true;
            this.InitializeComponent();
            this.tvwClass.DoubleClick += new EventHandler(this.tvwClass_DoubleClick);
            this.leafClassOnly = leafClassOnly;
            this.constraint = constraint;
            this.tvwClass.ImageList = ClientData.MyImageList.imageList;
        }

        public UCClassTree(string className, bool leafClassOnly, SelectClassConstraint constraint)
        {
            this.leafClassOnly = true;
            this.b_start = true;
            this.InitializeComponent();
            this.tvwClass.DoubleClick += new EventHandler(this.tvwClass_DoubleClick);
            this.leafClassOnly = leafClassOnly;
            this.tvwClass.ImageList = ClientData.MyImageList.imageList;
            this.constraint = constraint;
            this.ShowClassTree(className);
        }

        private TreeNode CreateClassNode(Guid pOid, string parentText, string parentClass, SelectClassConstraint constraint, DEMetaClass meta)
        {
            TreeNode node = null;
            bool flag = false;
            bool flag2 = true;
            if (constraint == SelectClassConstraint.InstancableClass)
            {
                Hashtable creatableClasses = ClientData.GetCreatableClasses();
                if (creatableClasses == null)
                {
                    return null;
                }
                flag2 = Convert.ToBoolean(creatableClasses[parentClass]);
            }
            if (pOid != Guid.Empty)
            {
                node = new TreeNode(parentClass) {
                    Text = parentText
                };
                node.SelectedImageIndex = node.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                node.Tag = meta;
            }
            else
            {
                node = new TreeNode("");
            }
            foreach (DEMetaClass class2 in ModelContext.MetaModel.GetClasses())
            {
                if (((constraint == SelectClassConstraint.CanSchableClass) || (class2.SystemClass != 'Y')) && ModelContext.MetaModel.IsVisibleCustomizedClass(class2.Name))
                {
                    if ((constraint == SelectClassConstraint.CanSchableClass) || (constraint == SelectClassConstraint.CustomizedClasses))
                    {
                        if (!class2.IsSearchable)
                        {
                            continue;
                        }
                        try
                        {
                            if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, class2.Name, Guid.Empty, "ClaRel_BROWSE") == 0)
                            {
                                continue;
                            }
                        }
                        catch (Exception exception)
                        {
                            PLMEventLog.WriteExceptionLog(exception);
                            continue;
                        }
                    }
                    if (((pOid == Guid.Empty) && (class2.Parent == Guid.Empty)) || ((class2.Parent == pOid) && (class2.Parent != Guid.Empty)))
                    {
                        flag = true;
                        string label = class2.Label;
                        string name = class2.Name;
                        TreeNode node2 = this.CreateClassNode(class2.Oid, label, name, constraint, class2);
                        if (node2 != null)
                        {
                            node.Nodes.Add(node2);
                        }
                    }
                }
            }
            if (node == null)
            {
                return null;
            }
            if ((node.Nodes.Count <= 0) && (((node.Nodes.Count != 0) || flag) || !flag2))
            {
                return null;
            }
            return node;
        }

        private bool NestSelectClass(TreeNode parent, string className)
        {
            foreach (TreeNode node in parent.Nodes)
            {
                DEMetaClass tag = node.Tag as DEMetaClass;
                if ((tag != null) && (tag.Name == className))
                {
                    if (this.tvwClass.SelectedNode != node)
                    {
                        this.ClassSelected(node.Tag as DEMetaClass);
                    }
                    this.tvwClass.SelectedNode = node;
                    return true;
                }
                if (this.NestSelectClass(node, className))
                {
                    node.Expand();
                    return true;
                }
            }
            return false;
        }

        public void SelectClass(string className)
        {
            foreach (TreeNode node in this.tvwClass.Nodes)
            {
                DEMetaClass tag = node.Tag as DEMetaClass;
                if ((tag != null) && (tag.Name == className))
                {
                    if (this.tvwClass.SelectedNode != node)
                    {
                        this.ClassSelected(node.Tag as DEMetaClass);
                    }
                    this.tvwClass.SelectedNode = node;
                    break;
                }
                if (this.NestSelectClass(node, className))
                {
                    node.Expand();
                    break;
                }
            }
        }

        public void ShowClassTree(string className)
        {
            switch (this.constraint)
            {
                case SelectClassConstraint.InstancableClass:
                    UIDataModel.FillClassTree(this.tvwClass, className, new AuthorizeFilter(), -1, -1);
                    break;

                case SelectClassConstraint.CanSchableClass:
                    UIDataModel.FillClassTree(this.tvwClass, className, new SearchableFilter(), -1, -1);
                    break;

                case SelectClassConstraint.CustomizedClasses:
                    UIDataModel.FillClassTree(this.tvwClass, className, new CustomizedClassFilter(), -1, -1);
                    break;

                case SelectClassConstraint.BusinessItemClass:
                    UIDataModel.FillClassTree(this.tvwClass, className, new VersionManageFilter(), -1, -1);
                    break;

                default:
                    UIDataModel.FillClassTree(this.tvwClass, className, null, -1, -1);
                    break;
            }
            if (this.tvwClass.Nodes.Count == 1)
            {
                this.tvwClass.ExpandAll();
            }
        }

        private void tvwClass_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!this.b_start && (this.ClassSelected != null))
            {
                if ((e.Node.Nodes.Count == 0) || !this.leafClassOnly)
                {
                    this.ClassSelected(e.Node.Tag as DEMetaClass);
                }
                else
                {
                    this.ClassSelected(null);
                }
            }
        }

        private void tvwClass_DoubleClick(object sender, EventArgs e)
        {
            if (this.DoubClicked != null)
            {
                this.DoubClicked();
            }
        }

        private void UCClassTree_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }

        public TreeView ClassTree
        {
            get {
                return
                this.tvwClass;
            }
            set
            {
                this.tvwClass = value;
            }
        }
    }
}

