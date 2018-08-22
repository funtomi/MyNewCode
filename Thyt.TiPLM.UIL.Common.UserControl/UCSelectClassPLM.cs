    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCSelectClassPLM : PopupContainerEditPLM
    {
        private SelectClassHandler handler;
        private DoubClickClassHandler handler2;
        private int height;
        private UCClassTree ucClassTree;

        public event DoubClickClassHandler DoubClickClassed;

        public event SelectClassHandler SelectedClassChanged;

        public UCSelectClassPLM()
        {
            this.height = 190;
            this.InitializeComponent();
        }

        public UCSelectClassPLM(bool leafClassOnly) : this(leafClassOnly, SelectClassConstraint.None)
        {
        }

        public UCSelectClassPLM(IContainer container) : this()
        {
            container.Add(this);
        }

        public UCSelectClassPLM(bool leafClassOnly, SelectClassConstraint constraint) : this((string) null, leafClassOnly, constraint)
        {
        }

        public UCSelectClassPLM(bool IsQuickSch, bool leafClassOnly, SelectClassConstraint constraint) : this()
        {
            this.ucClassTree = new UCClassTree(null, leafClassOnly, constraint);
            this.ucClassTree.ClassTree.Nodes.RemoveByKey("资源");
            this.ucClassTree.ClassTree.Nodes.RemoveByKey("业务状态");
            for (int i = 0; i < this.ucClassTree.ClassTree.Nodes.Count; i++)
            {
                TreeNode node = this.ucClassTree.ClassTree.Nodes[i];
                DEMetaClass tag = node.Tag as DEMetaClass;
                if (tag != null)
                {
                    if (tag.Name == "RESOURCE")
                    {
                        this.ucClassTree.ClassTree.Nodes.Remove(node);
                        i--;
                    }
                    else
                    {
                        this.NodeClear(node);
                        if ((node.Nodes.Count == 0) && ((!tag.IsSearchable || !ModelContext.MetaModel.IsVisibleCustomizedClass(tag.Name)) || (tag.IsGrantable && (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, tag.Name, Guid.Empty, "ClaRel_SEARCH") != 1))))
                        {
                            this.ucClassTree.ClassTree.Nodes.Remove(node);
                            i--;
                        }
                    }
                }
            }
            this.popupContainer.Controls.Add(this.ucClassTree);
            base.Properties.PopupControl.Size = new Size(base.Width, this.ucClassTree.Height);
            this.ucClassTree.Dock = DockStyle.Fill;
            this.handler = new SelectClassHandler(this.ucClassTree_ClassSelected);
            this.ucClassTree.ClassSelected += this.handler;
            this.handler2 = new DoubClickClassHandler(this.ucClassTree_DoubleClikedcls);
            this.ucClassTree.DoubClicked += this.handler2;
        }

        public UCSelectClassPLM(string className, bool leafClassOnly, SelectClassConstraint constraint) : this()
        {
            this.ucClassTree = new UCClassTree(((className == null) || (className == "")) ? null : className, leafClassOnly, constraint);
            this.popupContainer.Controls.Add(this.ucClassTree);
            base.Properties.PopupControl.Size = new Size(base.Width, this.ucClassTree.Height);
            this.ucClassTree.Dock = DockStyle.Fill;
            this.handler = new SelectClassHandler(this.ucClassTree_ClassSelected);
            this.ucClassTree.ClassSelected += this.handler;
            this.handler2 = new DoubClickClassHandler(this.ucClassTree_DoubleClikedcls);
            this.ucClassTree.DoubClicked += this.handler2;
        }

        private void NodeClear(TreeNode node)
        {
            if (node.Nodes.Count != 0)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    TreeNode node2 = node.Nodes[i];
                    DEMetaClass tag = node2.Tag as DEMetaClass;
                    if (tag != null)
                    {
                        this.NodeClear(node2);
                        if ((node2.Nodes.Count == 0) && ((!tag.IsSearchable || !ModelContext.MetaModel.IsVisibleCustomizedClass(tag.Name)) || (tag.IsGrantable && (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, tag.Name, Guid.Empty, "ClaRel_SEARCH") != 1))))
                        {
                            node.Nodes.Remove(node2);
                            i--;
                        }
                    }
                }
            }
        }

        private void PopupContainerEdit_QueryPopUp(object sender, CancelEventArgs e)
        {
            base.Properties.PopupControl.Width = base.Width;
        }

        public void SetValue(Guid classOid)
        {
            if (classOid == Guid.Empty)
            {
                base.Tag = null;
                this.textValue = "";
            }
            else
            {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(classOid);
                base.Tag = class2;
                this.textValue = class2.Label;
                this.ucClassTree.SelectClass(class2.Name);
            }
            this.Refresh();
        }

        public void SetValue(string className)
        {
            if ((className == null) || (className.Trim() == ""))
            {
                base.Tag = null;
                this.textValue = "";
            }
            else
            {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(className);
                if (class2 != null)
                {
                    base.Tag = class2;
                    this.textValue = class2.Label;
                    this.ucClassTree.SelectClass(className);
                }
                else
                {
                    base.Tag = null;
                    this.textValue = "";
                }
            }
            this.Refresh();
        }

        public void ShowClassTree(string className)
        {
            this.ucClassTree.ShowClassTree(className);
        }

        private void ucClassTree_ClassSelected(DEMetaClass meta)
        {
            bool flag = false;
            if (meta == null)
            {
                if ((meta == null) && (base.Tag != null))
                {
                    flag = true;
                }
                this.Text = "";
                base.Tag = null;
            }
            else
            {
                if ((base.Tag == null) || (meta.Oid != ((DEMeta) base.Tag).Oid))
                {
                    flag = true;
                }
                this.Text = meta.Label;
                base.Tag = meta;
            }
            if (flag && (this.SelectedClassChanged != null))
            {
                this.SelectedClassChanged(meta);
            }
        }

        private void ucClassTree_DoubleClikedcls()
        {
            if (this.DoubClickClassed != null)
            {
                this.DoubClickClassed();
            }
        }

        private void UCSelectClass_SizeChanged(object sender, EventArgs e)
        {
            if (base.Width > 170)
            {
                this.ucClassTree.Width = base.Width;
            }
        }

        public string NullText
        {
            get{
               return base.Properties.NullText;
            }set
            {
                base.Properties.NullText = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                if (base.Properties.TextEditStyle == TextEditStyles.Standard)
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    base.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                }
                else
                {
                    base.Properties.TextEditStyle = TextEditStyles.Standard;
                }
            }
        }

        public DEMetaClass SelectedClass{
          get{ return (base.Tag as DEMetaClass);
          }}
        public string textValue
        {
            get {
                return this.EditValue.ToString();
            }
            set
            {
                this.EditValue = value;
            }
        }
    }
}

