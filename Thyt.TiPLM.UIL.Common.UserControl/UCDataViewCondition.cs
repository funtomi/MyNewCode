    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Project2;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.PLL.Project2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCDataViewCondition : UserControlPLM, IDataViewUC
    {
        private ArrayList al_RemovedPosition = new ArrayList();
        private bool b_canviewattrfunction = true;
        private bool b_canviewclsfunction = true;
        private bool b_check;
        private bool b_IsRevFind;
        private bool b_lock;
        private bool b_lockRelNode;
        private bool b_needParamSet = true;
        private bool b_ReadOnly;
        private bool b_SelClsConditonNode = true;
        private bool b_start;
        
        private MenuItemEx cmiAdd;
        private MenuItemEx cmiAddAndbranch;
        private MenuItemEx cmiAddAttrFunc;
        private MenuItemEx cmiAddClsFunc;
        private MenuItemEx cmiAddORBranch;
        private MenuItemEx cmiDelete;
        private MenuItemEx[] cmiSepa;
        private MenuItemEx cmiSetAndRoot;
        private MenuItemEx cmiSetOrRoot;
        private MenuItemEx cmiSetParam;
        private MenuItemEx cmiUnSetParam;
        private ContextMenu ConTreeMenu = new ContextMenu();
        private DEConditionTreeEx curFilter;
        private object curFolder;
        private DEDataView DataView;
        private DERelationLink de_Link;
        

        public UCDataViewCondition(bool isRevFind)
        {
            this.b_IsRevFind = isRevFind;
            this.curFilter = new DEConditionTreeEx();
            this.InitializeComponent();
            this.AddIcon();
            this.InitializeDisplay();
        }

        private void AddIcon()
        {
            string[] resNames = new string[] { "ICO_RES_FILTER", "ICO_RES_REFRESH", "ICO_RES_CUSTOMIZE_RESOURCE", "ICO_RES_UNIT", "ICO_RES_RES_FENLEI", "ICO_RES_STANDARDPARTLIB", "ICO_RES_PICTURE", "ICO_RES_FDL_OPEN", "ICO_RES_FDL", "ICO_RES_NODE" };
            ClientData.MyImageList.AddIcons(resNames);
            this.tvwConditionTreeFilter.ImageList = ClientData.MyImageList.imageList;
            this.tvwConditionTreeFilter.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL");
            this.tvwConditionTreeFilter.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL_OPEN");
        }

        private void btnSetConditionFilter_Click(object sender, EventArgs e)
        {
            if (!this.b_ReadOnly)
            {
                if (this.pnlFilter.Controls.Count == 0)
                {
                    MessageBoxPLM.Show("请从关系链定义中选择一个节点。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (this.tabCon_Filter.SelectedTab == this.tabPage_AttrOption)
                    {
                        this.ModifyAttrItem();
                    }
                    if (this.tabCon_Filter.SelectedTab == this.tabPage_ClsRule)
                    {
                        this.ModifyClsFuncItem();
                    }
                    if (this.tabCon_Filter.SelectedTab == this.tabPage_AttrRule)
                    {
                        this.ModifyAttrFuncItem();
                    }
                }
            }
        }

        private void btnSetParaFilter_Click(object sender, EventArgs e)
        {
            if (this.tvwConditionTreeFilter.SelectedNode == null)
            {
                MessageBoxPLM.Show("请选择条件树节点。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (!(this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                if ((this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionItem) && (this.tvwConditionTreeFilter.SelectedNode.Tag as DEConditionItem).BelongFunction)
                {
                    MessageBoxPLM.Show("函数节点不能设为参数。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    this.b_check = true;
                    if (this.tvwConditionTreeFilter.SelectedNode.Checked)
                    {
                        this.tvwConditionTreeFilter.SelectedNode.Checked = false;
                    }
                    else
                    {
                        this.tvwConditionTreeFilter.SelectedNode.Checked = true;
                    }
                }
            }
        }

        private void ConditionLeafSelected(object obj, string str_dvoid)
        {
            if (this.DataView != null)
            {
                try
                {
                    if (Delegates.Instance.D_AfterAttributeSelected != null)
                    {
                        Delegates.Instance.D_AfterAttributeSelected(obj, str_dvoid);
                    }
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
            }
        }

       

        public void FillDataView()
        {
            if (this.DataView != null)
            {
                this.DataView.Filter = this.GetFilterValue();
            }
        }

        private ArrayList GetAloneSet(ArrayList al_in)
        {
            ArrayList list = new ArrayList();
            foreach (Guid guid in al_in)
            {
                bool flag = false;
                foreach (Guid guid2 in list)
                {
                    if (guid2 == guid)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    list.Add(guid);
                }
            }
            return list;
        }

        private string GetAttributeLabel(string className, string attrName, ItemMatchType itemType)
        {
            if ((itemType == ItemMatchType.Master) || (itemType == ItemMatchType.Revision))
            {
                return this.GetFixAttribute(className, attrName);
            }
            DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(className, attrName);
            DEMetaAttribute relationAttribute = ModelContext.MetaModel.GetRelationAttribute(className, attrName);
            if (attribute != null)
            {
                return attribute.Label;
            }
            return relationAttribute.Label;
        }

        private string GetAttributeValue(string clsname, string attrname, object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (obj is string)
            {
                try
                {
                    Guid oid = new Guid(obj.ToString());
                    DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(clsname, attrname);
                    if (attribute != null)
                    {
                        if ((attribute.SpecialType > 0) && (attribute.SpecialType2 != PLMSpecialType.ProcessTemplate))
                        {
                            string specTypeNameByOid = "";
                            if (attribute.DataType2 == PLMDataType.Guid)
                            {
                                specTypeNameByOid = ModelContext.GetSpecTypeNameByOid(oid, attribute.SpecialType);
                            }
                            return specTypeNameByOid;
                        }
                        if (attribute.SpecialType2 == PLMSpecialType.ProcessTemplate)
                        {
                            if ((UCAttrFilter.htProcessList != null) && UCAttrFilter.htProcessList.ContainsKey(oid))
                            {
                                return (UCAttrFilter.htProcessList[oid] as DELProcessDefProperty).Name;
                            }
                            return oid.ToString();
                        }
                        if (attribute.Name == "GROUP")
                        {
                            DEProject projectByMasterOid = PLProject.Agent.GetProjectByMasterOid(ClientData.LogonUser.Oid, oid);
                            if (projectByMasterOid == null)
                            {
                                PLProject project2 = new PLProject();
                                foreach (DEProject project3 in project2.GetAllProjects(ClientData.LogonUser.Oid))
                                {
                                    if (project3.Moid == oid)
                                    {
                                        projectByMasterOid = project3;
                                        break;
                                    }
                                }
                            }
                            return (projectByMasterOid.Name + "(" + projectByMasterOid.ID + ")");
                        }
                    }
                    else if (attrname == "GROUP")
                    {
                        DEProject project4 = PLProject.Agent.GetProjectByMasterOid(ClientData.LogonUser.Oid, oid);
                        if (project4 == null)
                        {
                            PLProject project5 = new PLProject();
                            foreach (DEProject project6 in project5.GetAllProjects(ClientData.LogonUser.Oid))
                            {
                                if (project6.Moid == oid)
                                {
                                    project4 = project6;
                                    break;
                                }
                            }
                        }
                        return (project4.Name + "(" + project4.ID + ")");
                    }
                    DEUser userByOid = new PLUser().GetUserByOid(oid);
                    return (userByOid.Name + "(" + userByOid.LogId + ")");
                }
                catch
                {
                    return obj.ToString();
                }
            }
            if (obj is Guid)
            {
                try
                {
                    Guid guid2 = (Guid) obj;
                    DEMetaAttribute attribute2 = ModelContext.MetaModel.GetAttribute(clsname, attrname);
                    if (attribute2 != null)
                    {
                        if (attribute2.SpecialType > 0)
                        {
                            string str5 = "";
                            if (attribute2.DataType2 == PLMDataType.Guid)
                            {
                                str5 = ModelContext.GetSpecTypeNameByOid(guid2, attribute2.SpecialType);
                            }
                            return str5;
                        }
                    }
                    else if (attrname == "GROUP")
                    {
                        DEProject project7 = PLProject.Agent.GetProjectByMasterOid(ClientData.LogonUser.Oid, guid2);
                        return (project7.Name + "(" + project7.ID + ")");
                    }
                    DEUser user4 = new PLUser().GetUserByOid(guid2);
                    return (user4.Name + "(" + user4.LogId + ")");
                }
                catch
                {
                    return obj.ToString();
                }
            }
            return obj.ToString();
        }

        private string GetClassCNName(string className)
        {
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(className);
            DEMetaRelation relation = ModelContext.MetaModel.GetRelation(className);
            if (class2 != null)
            {
                return class2.Label;
            }
            return relation.Label;
        }

        private ArrayList GetConditionTreeAllPosSet()
        {
            ArrayList list = new ArrayList();
            TreeNode node = this.tvwConditionTreeFilter.Nodes[0];
            if (this.tvwConditionTreeFilter.GetNodeCount(true) > 1)
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.SetConditionTreeAllPosSet(node2, list);
                }
            }
            return list;
        }

        private DEConditionTreeEx GetFilterValue()
        {
            TreeNode node = this.tvwConditionTreeFilter.Nodes[0];
            DEConditionBranch tag = (DEConditionBranch) node.Tag;
            tag.Nodes.Clear();
            if (this.tvwConditionTreeFilter.GetNodeCount(true) <= 1)
            {
                return null;
            }
            foreach (TreeNode node2 in node.Nodes)
            {
                this.SetFilterValue(node2, tag);
            }
            DEConditionTreeEx ex = new DEConditionTreeEx {
                ChildrenRelation = tag.ChildrenRelation,
                Oid = tag.Oid
            };
            foreach (object obj2 in tag.Nodes)
            {
                ex.Nodes.Add(obj2);
            }
            return ex;
        }

        private string GetFixAttribute(string className, string attrName)
        {
            string label = " ";
            foreach (GenericAttribute attribute in ModelContext.MetaModel.GetAllSearchableAttributes(className))
            {
                if (attribute.Name == attrName)
                {
                    label = attribute.Label;
                }
            }
            return label;
        }

        private Guid[] GetRelationLst(){
           return this.DataView.GetPositions();
        }
        private TreeNode GetRightNode(TreeNode root, Point p)
        {
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                if (root.Nodes[i].Bounds.Contains(p))
                {
                    this.tvwConditionTreeFilter.SelectedNode = root.Nodes[i];
                    return root.Nodes[i];
                }
                TreeNode rightNode = this.GetRightNode(root.Nodes[i], p);
                if (rightNode != null)
                {
                    return rightNode;
                }
            }
            return null;
        }

        private ArrayList GetSavePosSet(ArrayList al_old, ArrayList al_new)
        {
            ArrayList list = new ArrayList();
            foreach (Guid guid in al_old)
            {
                bool flag = false;
                foreach (Guid guid2 in al_new)
                {
                    if (guid2 == guid)
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    list.Add(guid);
                }
            }
            return list;
        }


        private void InitializeDisplay()
        {
            this.cmiAdd = new MenuItemEx("增加当前属性条件", new EventHandler(this.OnAddConItem), null);
            this.cmiAddClsFunc = new MenuItemEx("增加当前类函数", new EventHandler(this.OnAddClsFuncItem), null);
            this.cmiAddAttrFunc = new MenuItemEx("增加当前属性函数", new EventHandler(this.OnAddAttrFuncItem), null);
            this.cmiDelete = new MenuItemEx("删除", new EventHandler(this.OnDelete), null);
            this.cmiAddAndbranch = new MenuItemEx("增加并且枝", new EventHandler(this.OnAddAndBranch), null);
            this.cmiAddORBranch = new MenuItemEx("增加或者枝", new EventHandler(this.OnAddORBranch), null);
            this.cmiSetAndRoot = new MenuItemEx("设置并且枝", new EventHandler(this.OnSetAndRoot), null);
            this.cmiSetOrRoot = new MenuItemEx("设置或者枝", new EventHandler(this.OnSetOrRoot), null);
            this.cmiSetParam = new MenuItemEx("设为参数", new EventHandler(this.OnSetParam), null);
            this.cmiUnSetParam = new MenuItemEx("取消参数设置", new EventHandler(this.OnSetParam), null);
            this.cmiSepa = new MenuItemEx[4];
            this.cmiSepa[0] = new MenuItemEx("-", "-", null, null);
            this.cmiSepa[1] = new MenuItemEx("-", "-", null, null);
            this.cmiSepa[2] = new MenuItemEx("-", "-", null, null);
            this.b_lockRelNode = false;
            this.b_SelClsConditonNode = true;
            this.rbtClass.Enabled = false;
            this.rbtRelation.Enabled = false;
            Delegates.Instance.D_AfterNodeSelected = (PLMSimpleDelegate2) Delegate.Combine(Delegates.Instance.D_AfterNodeSelected, new PLMSimpleDelegate2(this.NodeSelected));
            Delegates.Instance.D_AfterNodeDeleted = (PLMSimpleDelegate2) Delegate.Combine(Delegates.Instance.D_AfterNodeDeleted, new PLMSimpleDelegate2(this.NodeDeleted));
            Delegates.Instance.D_AfterRightClassChanged = (PLMSimpleDelegate2) Delegate.Combine(Delegates.Instance.D_AfterRightClassChanged, new PLMSimpleDelegate2(this.NodeRightClassChanged));
        }

        public void InitUI(DEDataView dataView, bool readOnly)
        {
            if ((dataView != null) && !this.b_start)
            {
                this.DataView = dataView;
                this.b_ReadOnly = readOnly;
                this.b_start = true;
                if (this.DataView.Filter != null)
                {
                    if (this.tvwConditionTreeFilter.GetNodeCount(true) > 0)
                    {
                        this.DataView.Filter = this.GetFilterValue();
                    }
                    if ((this.DataView.Filter != null) && (this.DataView.Filter.Nodes.Count > 0))
                    {
                        Guid[] relationLst = this.GetRelationLst();
                        if (relationLst.Length > 0)
                        {
                            this.LoadConditionTreeUC(relationLst);
                            return;
                        }
                    }
                }
                this.tvwConditionTreeFilter.Nodes.Clear();
                DEConditionBranch branch = new DEConditionBranch {
                    ChildrenRelation = ConditionsRelation.And
                };
                TreeNode node = new TreeNode("条件根(并且)") {
                    Tag = branch,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_RES_FENLEI")
                };
                this.tvwConditionTreeFilter.Nodes.Add(node);
            }
        }

        private bool IsSameLeaf(TreeNode pnode, DEConditionItem deitem)
        {
            if (pnode.GetNodeCount(false) > 0)
            {
                foreach (TreeNode node in pnode.Nodes)
                {
                    if (node.Tag is DEConditionItem)
                    {
                        int num = 0;
                        if ((node.Tag as DEConditionItem).ClassPosition == deitem.ClassPosition)
                        {
                            num++;
                        }
                        if ((node.Tag as DEConditionItem).AttrName == deitem.AttrName)
                        {
                            num++;
                        }
                        if ((node.Tag as DEConditionItem).Operator == deitem.Operator)
                        {
                            num++;
                        }
                        if (((node.Tag as DEConditionItem).AttrValue != null) && (deitem.AttrValue != null))
                        {
                            if ((node.Tag as DEConditionItem).AttrValue.ToString().Trim() == deitem.AttrValue.ToString().Trim())
                            {
                                num++;
                            }
                        }
                        else if (((node.Tag as DEConditionItem).AttrValue == null) && (deitem.AttrValue == null))
                        {
                            num++;
                        }
                        if ((node.Tag as DEConditionItem).Option == deitem.Option)
                        {
                            num++;
                        }
                        if (num == 5)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void LoadConditionTreeUC(Guid[] g_poslst)
        {
            this.tvwConditionTreeFilter.Nodes.Clear();
            this.DataView.Filter = this.DataView.Filter.Shrink(g_poslst);
            this.curFilter = this.DataView.Filter;
            DEConditionBranch branch = new DEConditionBranch {
                ChildrenRelation = this.curFilter.ChildrenRelation
            };
            string text = "条件根(并且)";
            if (branch.ChildrenRelation == ConditionsRelation.Or)
            {
                text = "条件根(或者)";
            }
            TreeNode node = new TreeNode(text) {
                Tag = branch,
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_RES_FENLEI")
            };
            this.tvwConditionTreeFilter.Nodes.Add(node);
            this.SetFilterTVUC(node, this.curFilter);
            this.tvwConditionTreeFilter.ExpandAll();
        }

        private void LoadFilterUC(string str_clsname, Guid g_pos, bool b_iscls)
        {
            if (this.pnlFilter.Controls.Count > 0)
            {
                if (this.pnlFilter.Controls[0] is UCAttrFilter)
                {
                    (this.pnlFilter.Controls[0] as UCAttrFilter).LoadOCXByClsName(str_clsname, b_iscls);
                    (this.pnlFilter.Controls[0] as UCAttrFilter).Position = g_pos;
                }
                else
                {
                    this.pnlFilter.Controls.Clear();
                    UCAttrFilter filter = new UCAttrFilter {
                        Dock = DockStyle.Fill,
                        Position = g_pos
                    };
                    this.pnlFilter.Controls.Add(filter);
                    filter.LoadOCXByClsName(str_clsname, b_iscls);
                }
            }
            else
            {
                UCAttrFilter filter2 = new UCAttrFilter {
                    Dock = DockStyle.Fill,
                    Position = g_pos
                };
                this.pnlFilter.Controls.Add(filter2);
                filter2.LoadOCXByClsName(str_clsname, b_iscls);
            }
            if (this.pnlClsFunc.Controls.Count > 0)
            {
                if (this.pnlClsFunc.Controls[0] is UCClsRelFunction)
                {
                    (this.pnlClsFunc.Controls[0] as UCClsRelFunction).LoadFunction(str_clsname, b_iscls);
                    (this.pnlClsFunc.Controls[0] as UCClsRelFunction).Position = g_pos;
                }
                else
                {
                    this.pnlClsFunc.Controls.Clear();
                    UCClsRelFunction function = new UCClsRelFunction {
                        Dock = DockStyle.Fill,
                        Position = g_pos
                    };
                    this.pnlClsFunc.Controls.Add(function);
                    function.LoadFunction(str_clsname, b_iscls);
                }
            }
            else
            {
                UCClsRelFunction function2 = new UCClsRelFunction {
                    Dock = DockStyle.Fill,
                    Position = g_pos
                };
                this.pnlClsFunc.Controls.Add(function2);
                function2.LoadFunction(str_clsname, b_iscls);
            }
            if (this.pnlAttrFunc.Controls.Count > 0)
            {
                if (this.pnlAttrFunc.Controls[0] is UCAttrFunction)
                {
                    (this.pnlAttrFunc.Controls[0] as UCAttrFunction).LoadOCXByClsName(str_clsname, b_iscls);
                    (this.pnlAttrFunc.Controls[0] as UCAttrFunction).Position = g_pos;
                }
                else
                {
                    this.pnlAttrFunc.Controls.Clear();
                    UCAttrFunction function3 = new UCAttrFunction {
                        Dock = DockStyle.Fill,
                        Position = g_pos
                    };
                    this.pnlAttrFunc.Controls.Add(function3);
                    function3.LoadOCXByClsName(str_clsname, b_iscls);
                }
            }
            else
            {
                UCAttrFunction function4 = new UCAttrFunction {
                    Dock = DockStyle.Fill,
                    Position = g_pos
                };
                this.pnlAttrFunc.Controls.Add(function4);
                function4.LoadOCXByClsName(str_clsname, b_iscls);
            }
        }

        private void LoadFilterUCByItem()
        {
            this.LoadFilterUC((this.curFolder as DEConditionItem).ClassName, (this.curFolder as DEConditionItem).ClassPosition, (this.curFolder as DEConditionItem).AttrBelongClass);
            if (!(this.curFolder as DEConditionItem).BelongFunction)
            {
                if (this.pnlFilter.Controls.Count == 0)
                {
                    UCAttrFilter filter = new UCAttrFilter {
                        Dock = DockStyle.Fill,
                        Position = (this.curFolder as DEConditionItem).ClassPosition
                    };
                    this.pnlFilter.Controls.Add(filter);
                    filter.LoadOCXByItem(this.curFolder as DEConditionItem);
                }
                else
                {
                    ((UCAttrFilter) this.pnlFilter.Controls[0]).LoadOCXByItem(this.curFolder as DEConditionItem);
                }
                this.tabCon_Filter.SelectedIndex = 0;
            }
            else if ((this.curFolder as DEConditionItem).FunctionType == 3)
            {
                if (this.pnlAttrFunc.Controls.Count == 0)
                {
                    UCAttrFunction function = new UCAttrFunction {
                        Dock = DockStyle.Fill,
                        Position = (this.curFolder as DEConditionItem).ClassPosition
                    };
                    this.pnlAttrFunc.Controls.Add(function);
                    function.LoadFunctionByItem(this.curFolder as DEConditionItem);
                }
                else
                {
                    ((UCAttrFunction) this.pnlAttrFunc.Controls[0]).LoadFunctionByItem(this.curFolder as DEConditionItem);
                }
                if (!this.CanViewClassFuction)
                {
                    this.tabCon_Filter.SelectedIndex = 1;
                }
                else
                {
                    this.tabCon_Filter.SelectedIndex = 2;
                }
            }
            else
            {
                if (this.pnlClsFunc.Controls.Count == 0)
                {
                    UCClsRelFunction function3 = new UCClsRelFunction {
                        Dock = DockStyle.Fill,
                        Position = (this.curFolder as DEConditionItem).ClassPosition
                    };
                    this.pnlClsFunc.Controls.Add(function3);
                    function3.LoadFunctionByItem(this.curFolder as DEConditionItem);
                }
                else
                {
                    ((UCClsRelFunction) this.pnlClsFunc.Controls[0]).LoadFunctionByItem(this.curFolder as DEConditionItem);
                }
                this.tabCon_Filter.SelectedIndex = 1;
            }
        }

        private void LocationClsOrRel(string className)
        {
            this.b_SelClsConditonNode = true;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(className);
            DEMetaRelation relation = ModelContext.MetaModel.GetRelation(className);
            if (class2 == null)
            {
                if (relation != null)
                {
                    this.b_SelClsConditonNode = false;
                }
            }
            else
            {
                this.b_SelClsConditonNode = true;
            }
        }

        public void LockClassByUser(string str_clsname)
        {
            DERelationLinkHeader header = new DERelationLinkHeader {
                ClassName = str_clsname,
                ClassPosition = this.DataView.ClassPosition
            };
            this.NodeSelected(header, this.DataView.Oid.ToString());
        }

        private void LocRelTreeNode(DEConditionItem theItem)
        {
            this.b_lockRelNode = true;
            if (this.DataView != null)
            {
                this.LocationClsOrRel(theItem.ClassName);
                this.ConditionLeafSelected(theItem.ClassPosition, this.DataView.Oid.ToString());
            }
        }

        private void ModifyAttrFuncItem()
        {
            UCAttrFunction function = this.pnlAttrFunc.Controls[0] as UCAttrFunction;
            if (function.Operator == OperatorType.Unknown)
            {
                MessageBoxPLM.Show("请选择某种属性函数名。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (function.AttrValue == null)
            {
                MessageBoxPLM.Show("请输入或指定属性值。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.curFolder is DEConditionItem)
            {
                if (this.b_lock && (this.tvwConditionTreeFilter.SelectedNode != null))
                {
                    this.curFolder = this.tvwConditionTreeFilter.SelectedNode.Tag;
                    DEConditionItem deitem = new DEConditionItem(function.ConditionAttrName, function.Operator, function.AttrValue) {
                        Option = function.Option,
                        ClassPosition = function.Position
                    };
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode.Parent, deitem))
                    {
                        (this.curFolder as DEConditionItem).ConditionAttrName = function.ConditionAttrName;
                        (this.curFolder as DEConditionItem).Operator = function.Operator;
                        (this.curFolder as DEConditionItem).AttrValue = function.AttrValue;
                        (this.curFolder as DEConditionItem).ClassPosition = function.Position;
                        (this.curFolder as DEConditionItem).Option = function.Option;
                        string str2 = this.GetClassCNName((this.curFolder as DEConditionItem).ClassName) + "." + function.AttrText;
                        string str3 = this.GetAttributeValue((this.curFolder as DEConditionItem).ClassName, (this.curFolder as DEConditionItem).AttrName, (this.curFolder as DEConditionItem).AttrValue);
                        PLMOperator @operator = new PLMOperator((this.curFolder as DEConditionItem).Operator);
                        str2 = str2 + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            str2 = str2 + str3;
                        }
                        this.tvwConditionTreeFilter.SelectedNode.Text = str2;
                    }
                }
            }
            else
            {
                MessageBoxPLM.Show("条件对象类型不匹配。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ModifyAttrItem()
        {
            UCAttrFilter filter = this.pnlFilter.Controls[0] as UCAttrFilter;
            if (filter.Operator == OperatorType.Unknown)
            {
                MessageBoxPLM.Show("请选择某种属性比较符。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (((filter.AttrValue == null) && (filter.Operator != OperatorType.IsNull)) && (filter.Operator != OperatorType.IsNotNull))
            {
                MessageBoxPLM.Show("请输入或指定属性值。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.curFolder is DEConditionItem)
            {
                if (this.b_lock && (this.tvwConditionTreeFilter.SelectedNode != null))
                {
                    this.curFolder = this.tvwConditionTreeFilter.SelectedNode.Tag;
                    DEConditionItem deitem = new DEConditionItem(filter.ConditionAttrName, filter.Operator, filter.AttrValue) {
                        Option = filter.Option,
                        ClassPosition = filter.Position
                    };
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode.Parent, deitem))
                    {
                        (this.curFolder as DEConditionItem).ConditionAttrName = filter.ConditionAttrName;
                        (this.curFolder as DEConditionItem).Operator = filter.Operator;
                        (this.curFolder as DEConditionItem).AttrValue = filter.AttrValue;
                        (this.curFolder as DEConditionItem).ClassPosition = filter.Position;
                        (this.curFolder as DEConditionItem).Option = filter.Option;
                        string str2 = this.GetClassCNName((this.curFolder as DEConditionItem).ClassName) + "." + filter.AttrText + " ";
                        string str3 = this.GetAttributeValue((this.curFolder as DEConditionItem).ClassName, (this.curFolder as DEConditionItem).AttrName, (this.curFolder as DEConditionItem).AttrValue);
                        PLMOperator @operator = new PLMOperator((this.curFolder as DEConditionItem).Operator);
                        str2 = str2 + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            str2 = str2 + str3;
                        }
                        this.tvwConditionTreeFilter.SelectedNode.Text = str2;
                    }
                }
            }
            else
            {
                MessageBoxPLM.Show("条件对象类型不匹配。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ModifyClsFuncItem()
        {
            UCClsRelFunction function = this.pnlClsFunc.Controls[0] as UCClsRelFunction;
            if (function.FunctionValue == null)
            {
                MessageBoxPLM.Show("请输入或指定属性值。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.curFolder is DEConditionItem)
            {
                if (this.b_lock && (this.tvwConditionTreeFilter.SelectedNode != null))
                {
                    this.curFolder = this.tvwConditionTreeFilter.SelectedNode.Tag;
                    DEConditionItem conditionItem = function.ConditionItem;
                    conditionItem.ClassPosition = function.Position;
                    conditionItem.Operator = function.Operator;
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode.Parent, conditionItem))
                    {
                        (this.curFolder as DEConditionItem).ConditionAttrName = conditionItem.ConditionAttrName;
                        (this.curFolder as DEConditionItem).Operator = conditionItem.Operator;
                        (this.curFolder as DEConditionItem).AttrValue = conditionItem.AttrValue;
                        (this.curFolder as DEConditionItem).ClassPosition = conditionItem.ClassPosition;
                        (this.curFolder as DEConditionItem).Option = conditionItem.Option;
                        conditionItem.ConditionAttrName.ToString();
                        string str2 = this.GetClassCNName(conditionItem.ClassName) + " ";
                        string str3 = function.FunctionValue.ToString();
                        PLMOperator @operator = new PLMOperator(conditionItem.Operator);
                        str2 = str2 + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            str2 = str2 + str3;
                        }
                        this.tvwConditionTreeFilter.SelectedNode.Text = str2;
                    }
                }
            }
            else
            {
                MessageBoxPLM.Show("条件对象类型不匹配。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ModifyConTreeCls(Guid g_pos, string str_clsname)
        {
            TreeNode node = this.tvwConditionTreeFilter.Nodes[0];
            if (this.tvwConditionTreeFilter.GetNodeCount(true) > 1)
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.SetConTreeCls(node2, g_pos, str_clsname);
                }
            }
        }

        private void NodeDeleted(object obj, string str_dvoid)
        {
            if (((obj != null) && (this.DataView != null)) && (this.DataView.Oid.ToString() == str_dvoid))
            {
                this.de_Link = null;
                this.rbtClass.Enabled = false;
                this.rbtRelation.Enabled = false;
                this.pnlFilter.Controls.Clear();
                ArrayList savePosSet = new ArrayList();
                if (obj is DERelationLink)
                {
                    ArrayList list2 = new ArrayList {
                        (obj as DERelationLink).RelationPosition,
                        (obj as DERelationLink).RightClassPosition
                    };
                    ArrayList conditionTreeAllPosSet = new ArrayList();
                    conditionTreeAllPosSet = this.GetConditionTreeAllPosSet();
                    conditionTreeAllPosSet = this.GetAloneSet(conditionTreeAllPosSet);
                    savePosSet = this.GetSavePosSet(conditionTreeAllPosSet, list2);
                }
                if (this.DataView.Filter != null)
                {
                    if (this.tvwConditionTreeFilter.GetNodeCount(true) > 0)
                    {
                        this.DataView.Filter = this.GetFilterValue();
                    }
                    if ((this.DataView.Filter != null) && (this.DataView.Filter.Nodes.Count > 0))
                    {
                        Guid[] guidArray = new Guid[savePosSet.Count];
                        int index = 0;
                        foreach (Guid guid in savePosSet)
                        {
                            guidArray[index] = guid;
                            index++;
                        }
                        if (guidArray.Length > 0)
                        {
                            this.LoadConditionTreeUC(guidArray);
                            return;
                        }
                    }
                }
                this.tvwConditionTreeFilter.Nodes.Clear();
                DEConditionBranch branch = new DEConditionBranch {
                    ChildrenRelation = ConditionsRelation.And
                };
                TreeNode node = new TreeNode("条件根(并且)") {
                    Tag = branch,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_RES_FENLEI")
                };
                this.tvwConditionTreeFilter.Nodes.Add(node);
            }
        }

        private void NodeRightClassChanged(object obj, string str_dvoid)
        {
            if ((((obj != null) && (this.DataView != null)) && (this.DataView.Oid.ToString() == str_dvoid)) && (obj is DERelationLink))
            {
                this.ModifyConTreeCls((obj as DERelationLink).RightClassPosition, (obj as DERelationLink).RightClassName);
            }
        }

        private void NodeSelected(object obj, string str_dvoid)
        {
            int num = 0;
            if (((obj != null) && (this.DataView != null)) && (this.DataView.Oid.ToString() == str_dvoid))
            {
                if (obj is DERelationLinkHeader)
                {
                    this.rbtRelation.Enabled = false;
                    this.rbtClass.Enabled = true;
                    this.de_Link = new DERelationLink(obj as DERelationLinkHeader);
                    this.de_Link.RightClassName = (obj as DERelationLinkHeader).ClassName;
                    this.de_Link.RightClassPosition = (obj as DERelationLinkHeader).ClassPosition;
                    this.rbtClass.Text = "类: {this.GetClassCNName(this.de_Link.RightClassName)}";
                    this.rbtRelation.Text = "关联";
                    num++;
                }
                if (obj is DERelationLink)
                {
                    this.de_Link = obj as DERelationLink;
                    this.rbtRelation.Enabled = true;
                    this.rbtClass.Enabled = true;
                    this.rbtClass.Text = "类: {this.GetClassCNName(this.de_Link.RightClassName)}";
                    this.rbtRelation.Text = "关联: {this.GetClassCNName(this.de_Link.RelationName)}";
                    num++;
                }
                if (num == 1)
                {
                    this.btnSetConditionFilter.Enabled = false;
                    if (this.b_lockRelNode)
                    {
                        this.b_lockRelNode = false;
                        this.btnSetConditionFilter.Enabled = true;
                        if (this.b_SelClsConditonNode)
                        {
                            this.rbtClass.Checked = true;
                            this.rbtRelation.Checked = false;
                        }
                        else
                        {
                            this.rbtClass.Checked = false;
                            this.rbtRelation.Checked = true;
                        }
                    }
                    else
                    {
                        this.LoadFilterUC(this.de_Link.RightClassName, this.de_Link.RightClassPosition, true);
                        this.rbtClass.Checked = true;
                        this.rbtRelation.Checked = false;
                    }
                }
            }
        }

        private void OnAddAndBranch(object sender, EventArgs e)
        {
            if ((this.tvwConditionTreeFilter.SelectedNode != null) && (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                DEConditionBranch branch = new DEConditionBranch {
                    ChildrenRelation = ConditionsRelation.And
                };
                TreeNode node = new TreeNode("关系(并且)") {
                    Tag = branch,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_STANDARDPARTLIB")
                };
                this.tvwConditionTreeFilter.SelectedNode.Nodes.Add(node);
            }
        }

        private void OnAddAttrFuncItem(object sender, EventArgs e)
        {
            if (this.pnlAttrFunc.Controls.Count == 0)
            {
                MessageBoxPLM.Show("请在关系链定义中选择一个节点。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UCAttrFunction function = this.pnlAttrFunc.Controls[0] as UCAttrFunction;
                if (function.Operator == OperatorType.Unknown)
                {
                    MessageBoxPLM.Show("请选择某种属性比较符。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if ((function.AttrName == null) || (function.AttrName == ""))
                {
                    MessageBoxPLM.Show("请选择属性函数的属性名称", "设置函数");
                }
                else if ((function.AttrValue == null) || (function.AttrValue == ""))
                {
                    MessageBoxPLM.Show("请选择属性函数值", "设置函数");
                }
                else if (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch)
                {
                    DEConditionItem conditionItem = function.ConditionItem;
                    conditionItem.Oid = Guid.NewGuid();
                    conditionItem.ClassPosition = function.Position;
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode, conditionItem))
                    {
                        string text = this.GetClassCNName(conditionItem.ClassName) + "." + function.AttrText + " ";
                        string str3 = this.GetAttributeValue(conditionItem.ClassName, conditionItem.AttrName, conditionItem.AttrValue);
                        PLMOperator @operator = new PLMOperator(conditionItem.Operator);
                        text = text + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            text = text + str3;
                        }
                        TreeNode node = new TreeNode(text) {
                            Tag = conditionItem,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT")
                        };
                        this.tvwConditionTreeFilter.SelectedNode.Nodes.Add(node);
                    }
                }
            }
        }

        private void OnAddClsFuncItem(object sender, EventArgs e)
        {
            if (this.pnlClsFunc.Controls.Count == 0)
            {
                MessageBoxPLM.Show("类函数未定义", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UCClsRelFunction function = (UCClsRelFunction) this.pnlClsFunc.Controls[0];
                if (function.Operator == OperatorType.Unknown)
                {
                    MessageBoxPLM.Show("类函数未定义", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if ((function.FunctionValue == null) || (function.FunctionValue == ""))
                {
                    MessageBoxPLM.Show("请选择类函数值", "设置函数");
                }
                else if (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch)
                {
                    DEConditionItem conditionItem = function.ConditionItem;
                    conditionItem.Oid = Guid.NewGuid();
                    conditionItem.ClassPosition = function.Position;
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode, conditionItem))
                    {
                        conditionItem.ConditionAttrName.ToString();
                        string text = this.GetClassCNName(conditionItem.ClassName) + " ";
                        string str3 = function.FunctionValue.ToString();
                        PLMOperator @operator = new PLMOperator(conditionItem.Operator);
                        text = text + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            text = text + str3;
                        }
                        TreeNode node = new TreeNode(text) {
                            Tag = conditionItem,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT")
                        };
                        this.tvwConditionTreeFilter.SelectedNode.Nodes.Add(node);
                    }
                }
            }
        }

        private void OnAddConItem(object sender, EventArgs e)
        {
            if (this.pnlFilter.Controls.Count == 0)
            {
                MessageBoxPLM.Show("请在关系链定义中选择一个节点。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                UCAttrFilter filter = this.pnlFilter.Controls[0] as UCAttrFilter;
                if (filter.Operator == OperatorType.Unknown)
                {
                    MessageBoxPLM.Show("请选择某种属性比较符。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!UCAttrFilter.CheckConditionValueValid(filter.Operator, filter.AttrValue))
                {
                    MessageBoxPLM.Show("请选择属性值", "设置条件");
                }
                else if (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch)
                {
                    DEConditionItem conditionItem = filter.ConditionItem;
                    conditionItem.Oid = Guid.NewGuid();
                    conditionItem.ClassPosition = filter.Position;
                    if (!this.IsSameLeaf(this.tvwConditionTreeFilter.SelectedNode, conditionItem))
                    {
                        string text = this.GetClassCNName(conditionItem.ClassName) + "." + filter.AttrText + " ";
                        string str3 = this.GetAttributeValue(conditionItem.ClassName, conditionItem.AttrName, conditionItem.AttrValue);
                        PLMOperator @operator = new PLMOperator(conditionItem.Operator);
                        text = text + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            text = text + str3;
                        }
                        TreeNode node = new TreeNode(text) {
                            Tag = conditionItem,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT")
                        };
                        this.tvwConditionTreeFilter.SelectedNode.Nodes.Add(node);
                    }
                }
            }
        }

        private void OnAddORBranch(object sender, EventArgs e)
        {
            if ((this.tvwConditionTreeFilter.SelectedNode != null) && (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                DEConditionBranch branch = new DEConditionBranch {
                    ChildrenRelation = ConditionsRelation.Or
                };
                TreeNode node = new TreeNode("关系(或者)") {
                    Tag = branch,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_CUSTOMIZE_RESOURCE")
                };
                this.tvwConditionTreeFilter.SelectedNode.Nodes.Add(node);
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (this.tvwConditionTreeFilter.SelectedNode != null)
            {
                if (this.tvwConditionTreeFilter.SelectedNode.Parent == null)
                {
                    if (MessageBoxPLM.Show("您确认要删除选中的条件吗？", "数据视图管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        this.tvwConditionTreeFilter.SelectedNode.Nodes.Clear();
                    }
                }
                else if (MessageBoxPLM.Show("您确认要删除选中的条件吗？", "数据视图管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    this.tvwConditionTreeFilter.SelectedNode.Remove();
                }
            }
        }

        private void OnSetAndRoot(object sender, EventArgs e)
        {
            if ((this.tvwConditionTreeFilter.SelectedNode != null) && (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                DEConditionBranch tag = this.tvwConditionTreeFilter.SelectedNode.Tag as DEConditionBranch;
                tag.ChildrenRelation = ConditionsRelation.And;
                if (this.tvwConditionTreeFilter.SelectedNode.Parent == null)
                {
                    this.tvwConditionTreeFilter.SelectedNode.Text = "条件根(并且)";
                }
                else
                {
                    this.tvwConditionTreeFilter.SelectedNode.Text = "关系(并且)";
                }
            }
        }

        private void OnSetOrRoot(object sender, EventArgs e)
        {
            if ((this.tvwConditionTreeFilter.SelectedNode != null) && (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                DEConditionBranch tag = this.tvwConditionTreeFilter.SelectedNode.Tag as DEConditionBranch;
                tag.ChildrenRelation = ConditionsRelation.Or;
                if (this.tvwConditionTreeFilter.SelectedNode.Parent == null)
                {
                    this.tvwConditionTreeFilter.SelectedNode.Text = "条件根(或者)";
                }
                else
                {
                    this.tvwConditionTreeFilter.SelectedNode.Text = "关系(或者)";
                }
            }
        }

        private void OnSetParam(object sender, EventArgs e)
        {
            if ((this.tvwConditionTreeFilter.SelectedNode != null) && !(this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch))
            {
                if (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionItem)
                {
                    if ((this.tvwConditionTreeFilter.SelectedNode.Tag as DEConditionItem).BelongFunction)
                    {
                        MessageBoxPLM.Show("函数节点不能设为参数。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (PLDataView.Agent.IsExistInCompositeDV(this.DataView.Oid) && this.DataView.IsObjectList)
                    {
                        MessageBoxPLM.Show("数据视图是组合视图的节点,不能设为参数。", "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                this.b_check = true;
                if (this.tvwConditionTreeFilter.SelectedNode.Checked)
                {
                    this.tvwConditionTreeFilter.SelectedNode.Checked = false;
                }
                else
                {
                    this.tvwConditionTreeFilter.SelectedNode.Checked = true;
                }
            }
        }

        private void rbtClass_Click(object sender, EventArgs e)
        {
            if (this.de_Link != null)
            {
                this.btnSetConditionFilter.Enabled = false;
                this.b_lock = false;
                this.LoadFilterUC(this.de_Link.RightClassName, this.de_Link.RightClassPosition, true);
            }
        }

        private void rbtRelation_Click(object sender, EventArgs e)
        {
            if ((this.de_Link != null) && this.rbtRelation.Enabled)
            {
                this.btnSetConditionFilter.Enabled = false;
                this.b_lock = false;
                this.LoadFilterUC(this.de_Link.RelationName, this.de_Link.RelationPosition, false);
            }
        }

        private void SetConditionTreeAllPosSet(TreeNode pnode, ArrayList al_get)
        {
            if (pnode.GetNodeCount(true) == 0)
            {
                if (pnode.Tag is DEConditionItem)
                {
                    DEConditionItem tag = (DEConditionItem) pnode.Tag;
                    al_get.Add(tag.ClassPosition);
                }
            }
            else if (pnode.Tag is DEConditionBranch)
            {
                DEConditionBranch branch = (DEConditionBranch) pnode.Tag;
                branch.Nodes.Clear();
                foreach (TreeNode node in pnode.Nodes)
                {
                    this.SetConditionTreeAllPosSet(node, al_get);
                }
            }
        }

        private void SetConTreeCls(TreeNode pnode, Guid g_pos, string str_clsname)
        {
            if (pnode.GetNodeCount(true) == 0)
            {
                if (pnode.Tag is DEConditionItem)
                {
                    DEConditionItem tag = (DEConditionItem) pnode.Tag;
                    if (tag.ClassPosition == g_pos)
                    {
                        char attrType = Convert.ToChar("I");
                        if (tag.AttrType.ToString() == "M")
                        {
                            attrType = Convert.ToChar("M");
                        }
                        if (tag.AttrType.ToString() == "R")
                        {
                            attrType = Convert.ToChar("R");
                        }
                        tag.ConditionAttrName = new ConditionAttributeName(str_clsname, attrType, tag.AttrName);
                    }
                }
            }
            else if (pnode.Tag is DEConditionBranch)
            {
                DEConditionBranch branch = (DEConditionBranch) pnode.Tag;
                branch.Nodes.Clear();
                foreach (TreeNode node in pnode.Nodes)
                {
                    this.SetConTreeCls(node, g_pos, str_clsname);
                }
            }
        }

        private void SetFilterTVUC(TreeNode node, DEConditionBranch de_bran)
        {
            if (de_bran.Nodes.Count != 0)
            {
                foreach (object obj2 in de_bran.Nodes)
                {
                    string str = "并且";
                    if (obj2 is DEConditionBranch)
                    {
                        if ((obj2 as DEConditionBranch).ChildrenRelation == ConditionsRelation.And)
                        {
                            str = "并且";
                        }
                        if ((obj2 as DEConditionBranch).ChildrenRelation == ConditionsRelation.Or)
                        {
                            str = "或者";
                        }
                        TreeNode node2 = new TreeNode("关系(" + str + ")") {
                            Tag = obj2,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL")
                        };
                        node.Nodes.Add(node2);
                        this.SetFilterTVUC(node2, obj2 as DEConditionBranch);
                    }
                    if (obj2 is DEConditionItem)
                    {
                        string str2 = this.GetAttributeLabel((obj2 as DEConditionItem).ClassName, (obj2 as DEConditionItem).AttrName, (obj2 as DEConditionItem).AttrType);
                        string classCNName = this.GetClassCNName((obj2 as DEConditionItem).ClassName);
                        string text = "";
                        switch (str2)
                        {
                            case null:
                            case "":
                                text = classCNName + " ";
                                break;

                            default:
                                text = classCNName + "." + str2 + " ";
                                break;
                        }
                        string str5 = this.GetAttributeValue((obj2 as DEConditionItem).ClassName, (obj2 as DEConditionItem).AttrName, (obj2 as DEConditionItem).AttrValue);
                        PLMOperator @operator = new PLMOperator((obj2 as DEConditionItem).Operator);
                        text = text + @operator.OperName + " ";
                        if ((@operator.OperType != OperatorType.IsNull) && (@operator.OperType != OperatorType.IsNotNull))
                        {
                            text = text + str5;
                        }
                        TreeNode node3 = new TreeNode(text) {
                            Checked = (obj2 as DEConditionItem).IsParam,
                            Tag = obj2,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_UNIT")
                        };
                        node.Nodes.Add(node3);
                    }
                }
            }
        }

        private void SetFilterValue(TreeNode pnode, DEConditionBranch debranch)
        {
            if (pnode.GetNodeCount(true) == 0)
            {
                if (pnode.Tag is DEConditionItem)
                {
                    DEConditionItem tag = (DEConditionItem) pnode.Tag;
                    tag.IsParam = pnode.Checked;
                    debranch.Nodes.Add(tag);
                }
            }
            else if (pnode.Tag is DEConditionBranch)
            {
                DEConditionBranch branch = (DEConditionBranch) pnode.Tag;
                branch.Nodes.Clear();
                debranch.Nodes.Add(branch);
                foreach (TreeNode node in pnode.Nodes)
                {
                    this.SetFilterValue(node, branch);
                }
            }
        }

        private void tvwConditionTreeFilter_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvwConditionTreeFilter.SelectedNode == null)
            {
                this.b_lock = false;
            }
            else if (this.tvwConditionTreeFilter.SelectedNode.Tag is DEConditionBranch)
            {
                this.b_lock = false;
                this.curFolder = (DEConditionBranch) this.tvwConditionTreeFilter.SelectedNode.Tag;
                this.btnSetConditionFilter.Enabled = false;
            }
            else
            {
                this.btnSetConditionFilter.Enabled = true;
                this.b_lock = true;
                this.curFolder = (DEConditionItem) this.tvwConditionTreeFilter.SelectedNode.Tag;
                this.LocRelTreeNode(this.curFolder as DEConditionItem);
                this.LoadFilterUCByItem();
            }
        }

        private void tvwConditionTreeFilter_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (!this.b_check)
            {
                e.Cancel = true;
            }
            else
            {
                this.b_check = false;
            }
        }

        private void tvwConditionTreeFilter_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Point pt = new Point(e.X, e.Y);
                if (this.tvwConditionTreeFilter.Nodes.Count > 0)
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        TreeNode rightNode = new TreeNode();
                        if (this.tvwConditionTreeFilter.Nodes[0].Bounds.Contains(pt))
                        {
                            rightNode = this.tvwConditionTreeFilter.Nodes[0];
                            this.tvwConditionTreeFilter.SelectedNode = rightNode;
                        }
                        else
                        {
                            rightNode = this.GetRightNode(this.tvwConditionTreeFilter.Nodes[0], pt);
                        }
                        if (rightNode != null)
                        {
                            this.ConTreeMenu.MenuItems.Clear();
                            if (this.b_ReadOnly)
                            {
                                return;
                            }
                            if (typeof(DEConditionBranch).IsInstanceOfType(rightNode.Tag))
                            {
                                this.b_lock = false;
                                this.curFolder = (DEConditionBranch) rightNode.Tag;
                                this.ConTreeMenu.MenuItems.AddRange(new MenuItemEx[] { this.cmiAdd, this.cmiAddClsFunc, this.cmiAddAttrFunc, this.cmiSepa[0], this.cmiSetAndRoot, this.cmiSetOrRoot, this.cmiSepa[1], this.cmiAddAndbranch, this.cmiAddORBranch, this.cmiSepa[2], this.cmiDelete });
                            }
                            else
                            {
                                this.curFolder = (DEConditionItem) rightNode.Tag;
                                this.b_lock = true;
                                if (rightNode.Checked)
                                {
                                    this.ConTreeMenu.MenuItems.AddRange(new MenuItemEx[] { this.cmiUnSetParam, this.cmiDelete });
                                }
                                else
                                {
                                    this.ConTreeMenu.MenuItems.AddRange(new MenuItemEx[] { this.cmiSetParam, this.cmiDelete });
                                }
                            }
                            pt.X += 5;
                            pt.Y += 40;
                            this.ConTreeMenu.Show(this, pt);
                        }
                    }
                    if (e.Button == MouseButtons.Left)
                    {
                        TreeNode node2 = new TreeNode();
                        if (this.tvwConditionTreeFilter.Nodes[0].Bounds.Contains(pt))
                        {
                            node2 = this.tvwConditionTreeFilter.Nodes[0];
                        }
                        else
                        {
                            node2 = this.GetRightNode(this.tvwConditionTreeFilter.Nodes[0], pt);
                        }
                        if (node2 != null)
                        {
                            pt.X += 5;
                            if (typeof(DEConditionBranch).IsInstanceOfType(node2.Tag))
                            {
                                this.b_lock = false;
                                this.curFolder = (DEConditionBranch) node2.Tag;
                                this.btnSetConditionFilter.Enabled = false;
                            }
                            else
                            {
                                this.btnSetConditionFilter.Enabled = true;
                                this.curFolder = (DEConditionItem) node2.Tag;
                                this.LocRelTreeNode(this.curFolder as DEConditionItem);
                                this.b_lock = true;
                                this.LoadFilterUCByItem();
                            }
                        }
                    }
                }
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
            }
            catch (Exception exception2)
            {
                MessageBoxPLM.Show("显示上下文菜单出错。\r\n" + exception2.Message, "数据视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public bool ValidateUI(){ 
           return true;
    }
        public bool CanViewAttributeFuction
        {
            get{ 
               return this.b_canviewattrfunction;
            }set
            {
                this.b_canviewattrfunction = value;
                if (!this.b_canviewattrfunction)
                {
                    if (this.tabCon_Filter.Contains(this.tabPage_AttrRule))
                    {
                        this.tabCon_Filter.TabPages.Remove(this.tabPage_AttrRule);
                    }
                    this.cmiAddAttrFunc.Visible = false;
                }
            }
        }

        public bool CanViewClassFuction
        {
            get{
               return this.b_canviewclsfunction;
            }set
            {
                this.b_canviewclsfunction = value;
                if (!this.b_canviewclsfunction)
                {
                    if (this.tabCon_Filter.Contains(this.tabPage_ClsRule))
                    {
                        this.tabCon_Filter.TabPages.Remove(this.tabPage_ClsRule);
                    }
                    this.cmiAddClsFunc.Visible = false;
                }
            }
        }

        public bool NeedParamSet
        {
            get{
               return this.b_needParamSet;
            }set
            {
                this.b_needParamSet = value;
                if (!this.b_needParamSet)
                {
                    this.cmiSetParam.Visible = false;
                    this.cmiUnSetParam.Visible = false;
                }
            }
        }
    }
}

