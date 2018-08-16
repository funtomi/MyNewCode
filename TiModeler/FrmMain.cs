
    using ClientUIFramework;
    using Crownwood.DotNetMagic.Docking;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using Thyt.TiPLM.CLT.Admin.BPM;
    using Thyt.TiPLM.CLT.Admin.BPM.ExportAndImport;
    using Thyt.TiPLM.CLT.Admin.BPM.Modeler;
    using Thyt.TiPLM.CLT.TiModeler.Addin;
    using Thyt.TiPLM.CLT.TiModeler.BPModel;
    using Thyt.TiPLM.CLT.TiModeler.FileModel;
    using Thyt.TiPLM.CLT.TiModeler.Folder;
    using Thyt.TiPLM.CLT.TiModeler.ViewModel;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Collections;
    using Thyt.TiPLM.CLT.UIL.DeskLib.CommandBars;
    using Thyt.TiPLM.CLT.UIL.DeskLib.General;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Win32;
    using Thyt.TiPLM.CLT.UIL.DeskLib.WinControls;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Admin.View;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Utility;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Admin.View;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.PLL.Utility;
    using Thyt.TiPLM.UIL.Addin;
    using Thyt.TiPLM.UIL.Admin.BizOperation;
    using Thyt.TiPLM.UIL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.AdminRole;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.Common;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.MetaPerm;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.ObjectPerm;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.Org;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.Role;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.Search;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.User;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.ChartReport;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Environment;
    using Thyt.TiPLM.UIL.Operation;
    using Thyt.TiPLM.UIL.PPM.Formula2;
    using Thyt.TiPLM.UIL.PPM.PPCardModeling;
    using Thyt.TiPLM.UIL.PPM.PPSignManager;
    using Thyt.TiPLM.UIL.Product.Common.ClassFormula;
    using Thyt.TiPLM.UIL.Product.DataCheck;
    using Thyt.TiPLM.UIL.Product.DataView;
    using Thyt.TiPLM.UIL.Report.CustomTemplate;
    using Thyt.TiPLM.UIL.TiMessage;
    using Thyt.TiPLM.UIL.Utility;
    using Thyt.TiPLM.UIL.Utility.ExtendedModel;
namespace Thyt.TiPLM.CLT.TiModeler
{
    public partial class FrmMain : Form
    {
        public bool adminRoleFirstSelect = true;
        private D_AfterMetaClassCreated AfterMetaClassCreated;
        private D_AfterMetaClassDeleted AfterMetaClassDeleted;
        private D_AfterMetaClassModified AfterMetaClassModified;
        private D_AfterMetaContextCreated AfterMetaContextCreated;
        private D_AfterMetaContextDeleted AfterMetaContextDeleted;
        private D_AfterMetaContextModified AfterMetaContextModified;
        private D_AfterMetaRelationCreated AfterMetaRelationCreated;
        private D_AfterMetaRelationDeleted AfterMetaRelationDeleted;
        private D_AfterMetaRelationModified AfterMetaRelationModified;
        private bool allowAddinManagement;
        private bool allowBrowseProcessManagement;
        private bool allowConfigBPM;
        private bool allowConfigBrowser;
        private bool allowConfigDataModel;
        private bool allowConfigFileType;
        private bool allowConfigFormTemplate;
        private bool allowConfigOrgModel;
        private bool allowConfigOutputTemplate;
        private bool allowConfigPPCardTemplate;
        private bool allowConfigTool;
        private bool allowCreateProcessManagement;
        private bool allowDataCheckManagement;
        private bool allowDataIntegrateRule;
        private bool allowDelProcessManagement;
        private bool allowEcmsDataManagement;
        private bool allowEventManagement;
        private bool allowFolderManagement;
        private bool allowFormulaManagement;
        private bool allowLifeCycleManage;
        private bool allowModifyProcessManagement;
        private bool allowOperationDefinition;
        private bool allowViewManagement;
        private Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx ClassPasteMi = new Thyt.TiPLM.CLT.UIL.DeskLib.Menus.MenuItemEx("&OnPasteProTem", "粘贴模版(&V)", null, null);
        
        private string curContextMenu;
        private ToolBarEx debugToolBar;
        public DEAdminRole deCurrentAdminRole = new DEAdminRole();
        public bool dictSelected;
        public bool displayCreateOrg;
        public bool displayCreateRole;
        public bool displayCreateUser;
        public bool displayDeleteOrg;
        public bool displayDeleteRole;
        public bool displayModifyOrg;
        public bool displayModifyRole;
        private FrmCustomExceptionList exceptionList;
        public FrmBPNavigator frmBPNavigator;
        public FrmNavigator frmNavigator;
        public PLMHashtable HashMDiWindows;
        public ImageList ilsIcon;
        private ComboBox imageComboBox;
        private bool IsAdminRole;
        public bool isCuttingNodeDeleted;
        public bool isDoubleClick;
        public bool isNavigationShow;
        public bool IsNewProClass = true;
        private bool loaded;
        private string menuFileName;
        
        private RefreshNavigatorHandler NavigatorUpdateHandler;
        private ToolBarItem nextItem;
       
        public bool orgFirstSelect = true;
        private ToolBarEx outlookToolBar;
        private object parent;
        private ToolBarItem prevItem;
        private static ResourceManager rmMenus;
        private static ResourceManager rmTiModeler;
        public static ResourceManager rmTiModelerDtMd;
        private static ResourceManager rmToolBarItems;
        private MenuItemEx RootPasteMi = new MenuItemEx("&OnPasteProTem", "粘贴模版(&V)", null, null);
       
        private ToolBarEx standardToolBar;
        public ArrayList TheCopiedObjectList = new ArrayList();
        
        public Hashtable TheProClsNodeList = new Hashtable();
        private Hashtable TheProDefNodeList = new Hashtable();

        public FrmMain()
        {
            SplashHelper.Instance.ShowSplashMessage("正在初始化主窗体框架和菜单……");
            if (ConstCommon.FUNCTION_EDMS)
            {
                this.menuFileName = Application.StartupPath + @"\timodeler.edm.mnu";
            }
            else
            {
                this.menuFileName = Application.StartupPath + @"\timodeler.plm.mnu";
            }
            if (ConstCommon.FUNCTION_IORS)
            {
                this.menuFileName = Application.StartupPath + @"\timodeler.ecms.mnu";
            }
            else
            {
                this.menuFileName = Application.StartupPath + @"\timodeler.plm.mnu";
            }
            this.HashMDiWindows = new PLMHashtable();
            rmMenus = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.MenusImages", Assembly.GetExecutingAssembly());
            rmToolBarItems = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.ToolBarImages", Assembly.GetExecutingAssembly());
            rmTiModeler = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.TiModelerStrings", Assembly.GetExecutingAssembly());
            rmTiModelerDtMd = new ResourceManager("Thyt.TiPLM.CLT.TiModeler.DataModel.TiModelerDtMdStrings", Assembly.GetExecutingAssembly());
            Bitmap bitmap = (Bitmap) rmToolBarItems.GetObject("Icons32x32");
            this.bigIconsImageList = new ImageList();
            this.bigIconsImageList.ImageSize = new Size(0x20, 0x20);
            this.bigIconsImageList.TransparentColor = Color.Fuchsia;
            this.bigIconsImageList.Images.AddStrip(bitmap);
            TiModelerUIContainer.mainMenu = new MainMenu(this.CreateMainMenu());
            base.Menu = TiModelerUIContainer.mainMenu;
            TiModelerUIContainer.dockingManager = new DockingManager(this, ClientData.OptApplicationStyle);
            this.InitializeComponent();
            this.Text = PLSystemParam.ParameterAliasOfTiModeler;
            this.InitializeImageList();
            SplashHelper.Instance.ShowSplashMessage("正在初始化主窗体状态栏……");
            this.CreateStatusBar();
            this.CreateDockingControl();
            UIMessage.Instance.ServerConnectionErrorForControl += new ErrorEventHandler(this.Instance_ServerConnectionErrorForControl);
            this.isNavigationShow = true;
            SplashHelper.Instance.ShowSplashMessage("正在初始化主窗体工具栏……");
            this.InitializeToolBars();
            if (IsCommonCtrl6())
            {
                this.BackColor = SystemColors.Control;
            }
            else
            {
                this.BackColor = ColorUtil.VSNetControlColor;
            }
            base.SystemColorsChanged += new EventHandler(MenuItemEx.UpdateMenuColors);
            base.SystemColorsChanged += new EventHandler(ReBar.UpdateBandsColors);
            base.SystemColorsChanged += new EventHandler(Thyt.TiPLM.CLT.TiModeler.FrmMain.UpdateBackground);
            try
            {
                PLAdminRole role = new PLAdminRole();
                Guid empty = Guid.Empty;
                DEAdminRoleGrantUser adminRoleByUserId = role.GetAdminRoleByUserId(ClientData.LogonUser.Oid);
                if (adminRoleByUserId == null)
                {
                    this.IsAdminRole = false;
                }
                else
                {
                    this.IsAdminRole = true;
                }
                empty = adminRoleByUserId.AdminRole;
                this.deCurrentAdminRole = role.GetAdminRole(empty);
            }
            catch
            {
            }
            ClientData.Instance.D_RefreshAllViewSchemas = (PLM_RefreshAllViewSchemas) Delegate.Combine(ClientData.Instance.D_RefreshAllViewSchemas, new PLM_RefreshAllViewSchemas(this.RefreshAllViewSchemas));
            ClientData.Instance.D_AfterViewSchemaCreated = (PLM_AfterViewSchemaCreated) Delegate.Combine(ClientData.Instance.D_AfterViewSchemaCreated, new PLM_AfterViewSchemaCreated(this.AfterViewSchemaCreated));
            ClientData.Instance.D_AfterViewSchemaModified = (PLM_AfterViewSchemaModified) Delegate.Combine(ClientData.Instance.D_AfterViewSchemaModified, new PLM_AfterViewSchemaModified(this.AfterViewSchemaModified));
            ClientData.Instance.D_AfterViewSchemaDeleted = (PLM_AfterViewSchemaDeleted) Delegate.Combine(ClientData.Instance.D_AfterViewSchemaDeleted, new PLM_AfterViewSchemaDeleted(this.AfterViewSchemaDeleted));
            ClientData.Instance.D_RefreshAllContexts = (PLM_RefreshAllContexts) Delegate.Combine(ClientData.Instance.D_RefreshAllContexts, new PLM_RefreshAllContexts(this.RefreshAllContexts));
            ClientData.Instance.D_AfterContextCreated = (PLM_AfterContextCreated) Delegate.Combine(ClientData.Instance.D_AfterContextCreated, new PLM_AfterContextCreated(this.AfterContextCreated));
            ClientData.Instance.D_AfterContextModified = (PLM_AfterContextModified) Delegate.Combine(ClientData.Instance.D_AfterContextModified, new PLM_AfterContextModified(this.AfterContextModified));
            ClientData.Instance.D_AfterContextDeleted = (PLM_AfterContextDeleted) Delegate.Combine(ClientData.Instance.D_AfterContextDeleted, new PLM_AfterContextDeleted(this.AfterContextDeleted));
            this.nextItem.Enabled = this.prevItem.Enabled = false;
            this.AfterMetaClassCreated = new D_AfterMetaClassCreated(this.OnMetaClassCreated);
            DataModelDelegate.Instance.AfterMetaClassCreated = (D_AfterMetaClassCreated) Delegate.Combine(DataModelDelegate.Instance.AfterMetaClassCreated, this.AfterMetaClassCreated);
            this.AfterMetaClassModified = new D_AfterMetaClassModified(this.OnMetaClassModified);
            DataModelDelegate.Instance.AfterMetaClassModified = (D_AfterMetaClassModified) Delegate.Combine(DataModelDelegate.Instance.AfterMetaClassModified, this.AfterMetaClassModified);
            this.AfterMetaClassDeleted = new D_AfterMetaClassDeleted(this.OnMetaClassDeleted);
            DataModelDelegate.Instance.AfterMetaClassDeleted = (D_AfterMetaClassDeleted) Delegate.Combine(DataModelDelegate.Instance.AfterMetaClassDeleted, this.AfterMetaClassDeleted);
            this.AfterMetaRelationCreated = new D_AfterMetaRelationCreated(this.OnMetaRelationCreated);
            DataModelDelegate.Instance.AfterMetaRelationCreated = (D_AfterMetaRelationCreated) Delegate.Combine(DataModelDelegate.Instance.AfterMetaRelationCreated, this.AfterMetaRelationCreated);
            this.AfterMetaRelationModified = new D_AfterMetaRelationModified(this.OnMetaRelationModified);
            DataModelDelegate.Instance.AfterMetaRelationModified = (D_AfterMetaRelationModified) Delegate.Combine(DataModelDelegate.Instance.AfterMetaRelationModified, this.AfterMetaRelationModified);
            this.AfterMetaRelationDeleted = new D_AfterMetaRelationDeleted(this.OnMetaRelationDeleted);
            DataModelDelegate.Instance.AfterMetaRelationDeleted = (D_AfterMetaRelationDeleted) Delegate.Combine(DataModelDelegate.Instance.AfterMetaRelationDeleted, this.AfterMetaRelationDeleted);
            this.AfterMetaContextCreated = new D_AfterMetaContextCreated(this.OnMetaContextCreated);
            DataModelDelegate.Instance.AfterMetaContextCreated = (D_AfterMetaContextCreated) Delegate.Combine(DataModelDelegate.Instance.AfterMetaContextCreated, this.AfterMetaContextCreated);
            this.AfterMetaContextModified = new D_AfterMetaContextModified(this.OnMetaContextModified);
            DataModelDelegate.Instance.AfterMetaContextModified = (D_AfterMetaContextModified) Delegate.Combine(DataModelDelegate.Instance.AfterMetaContextModified, this.AfterMetaContextModified);
            this.AfterMetaContextDeleted = new D_AfterMetaContextDeleted(this.OnMetaContextDeleted);
            DataModelDelegate.Instance.AfterMetaContextDeleted = (D_AfterMetaContextDeleted) Delegate.Combine(DataModelDelegate.Instance.AfterMetaContextDeleted, this.AfterMetaContextDeleted);
        }

        private void ActivateModel(object sender, EventArgs e)
        {
            DEExtendedModel tag = this.tvwNavigator.SelectedNode.Tag as DEExtendedModel;
            IExtendedModelAddin addinEntryObject = AddinFramework.Instance.GetAddinEntryObject(tag.AddinOid) as IExtendedModelAddin;
            if (addinEntryObject != null)
            {
                addinEntryObject.OnModelActivate(tag);
            }
            else
            {
                MessageBox.Show("无法激活模型", "扩展企业模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ActiveViewModel(object sender, EventArgs e)
        {
            try
            {
                DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                if (PLViewModel.GetAllViewsByVMOid(tag.Oid).Count == 0)
                {
                    MessageBox.Show("无法激活视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”，该视图模型未包含任何视图！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (MessageBox.Show("激活该视图模型后，\n该视图模型将在英泰全生命周期系统使用。\n是否确定要激活该视图？", "激活视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
                {
                    new PLViewModel().ActiveViewModelOrNot(tag.Oid, true);
                    tag.IsActive = 'A';
                    this.tvwNavigator.SelectedNode.Tag = tag;
                    IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Key is DEViewModel)
                        {
                            DEViewModel key = (DEViewModel) enumerator.Key;
                            DEViewModel model4 = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                            if (key.Oid.Equals(model4.Oid))
                            {
                                VMFrame frame = (VMFrame) enumerator.Value;
                                frame.Show();
                                frame.Activate();
                                frame.Tag = tag;
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("激活视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void AddinRefresh(object sender, EventArgs e)
        {
            this.GetAddinWnd().lvwAddin.Items.Clear();
            this.GetAddinWnd().DisplayList(true);
        }

        private void AddOneProcess(TreeNode newNode)
        {
            if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator.AddOneProcess(newNode);
                this.frmBPNavigator.SetSelectItem(newNode, false);
            }
        }

        public void AddTreeNode(WFTEditor TargetWindow, Shape TaskNode)
        {
            TreeNode keyByValue = (TreeNode) this.HashMDiWindows.GetKeyByValue(TargetWindow);
            if (keyByValue != null)
            {
                TreeNode node = new TreeNode(TaskNode.realNode.Name);
                if (TaskNode.realNode.NodeType == StepType.ROUTER)
                {
                    node.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_ROUTER");
                    node.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_ROUTER");
                }
                else
                {
                    node.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_TASK");
                    node.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_TASK");
                }
                node.Tag = TaskNode;
                keyByValue.Nodes.Add(node);
            }
        }

        private void AdminRoleDel(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认要删除管理角色" + this.tvwNavigator.SelectedNode.Text + "吗？", "管理角色删除", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                PLAdminRole role = new PLAdminRole();
                DEAdminRole tag = (DEAdminRole) this.tvwNavigator.SelectedNode.Tag;
                try
                {
                    role.Delete(ClientData.LogonUser.LogId, tag.Oid);
                }
                catch (ResponsibilityException exception)
                {
                    MessageBox.Show(exception.Message, "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch
                {
                    MessageBox.Show("无法删除指定的管理角色！", "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (this.tvwNavigator.SelectedNode.Parent.Tag.Equals("OrgModel"))
                {
                    this.tvwNavigator.SelectedNode.Text = "管理角色";
                    this.tvwNavigator.SelectedNode.Tag = "AdminRole";
                }
                else
                {
                    TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                    this.tvwNavigator.SelectedNode = this.tvwNavigator.SelectedNode.Parent;
                    this.tvwNavigator.Nodes.Remove(selectedNode);
                }
            }
        }

        private void AdminRoleProp(object sender, EventArgs e)
        {
            bool forSearch = true;
            if (((this.tvwNavigator.SelectedNode != null) && (this.tvwNavigator.SelectedNode != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0])) && (this.tvwNavigator.SelectedNode.Parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0]))
            {
                forSearch = false;
            }
            if (this.tvwNavigator.SelectedNode != null)
            {
                Guid empty = Guid.Empty;
                bool isForRoot = false;
                if ((this.tvwNavigator.SelectedNode.Parent.Tag is string) && (this.tvwNavigator.SelectedNode.Parent.Tag.ToString() == "OrgModel"))
                {
                    isForRoot = true;
                }
                FrmAdminRoleProperty property = new FrmAdminRoleProperty(forSearch, isForRoot);
                if ((this.tvwNavigator.SelectedNode.Tag != null) && (this.tvwNavigator.SelectedNode.Tag is DEAdminRole))
                {
                    property.deAdminRole = ((DEAdminRole) this.tvwNavigator.SelectedNode.Tag).Clone();
                    empty = ((DEAdminRole) this.tvwNavigator.SelectedNode.Tag).Oid;
                }
                property.ShowDialog();
                if (property.isComPropApply)
                {
                    this.tvwNavigator.SelectedNode.Tag = property.deAdminRole;
                    this.tvwNavigator.SelectedNode.Text = property.deAdminRole.Name;
                    this.RefreshAdminRoleTree();
                    TreeNode node = this.JudgeSelectesAdminRoleNode(empty);
                    if (node != null)
                    {
                        this.tvwNavigator.SelectedNode = node;
                    }
                    this.GetAdminRoleWnd().UserRefresh(sender, e);
                }
            }
        }

        private void AdminRoleRefresh(object sender, EventArgs e)
        {
            Guid oid = ((DEAdminRole) this.tvwNavigator.SelectedNode.Tag).Oid;
            try
            {
                this.RefreshAdminRoleTree();
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("管理角色模型不存在！"))
                {
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Text = "管理角色";
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Tag = "AdminRole";
                }
                MessageBox.Show(exception.Message, "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            this.GetAdminRoleWnd().lvwAdminRoleUser.Items.Clear();
            this.GetAdminRoleWnd().lvwSubadminRole.Items.Clear();
            this.GetAdminRoleWnd().ShowAdminRole(this.tvwNavigator.SelectedNode.Tag);
        }

        private void AdminRoleRename(object sender, EventArgs e)
        {
            this.tvwNavigator.LabelEdit = true;
            if (((DEAdminRole) this.tvwNavigator.SelectedNode.Tag).ParentAdminRole.Equals(Guid.Empty))
            {
                this.tvwNavigator.SelectedNode.Text = ((DEAdminRole) this.tvwNavigator.SelectedNode.Tag).Name;
            }
            this.tvwNavigator.SelectedNode.BeginEdit();
        }

        private void AfterContextCreated(DEMetaContext context)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context;
            if ((node != null) && (((string) node.Tag) == "Context"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEMetaContext) node2.Tag).Oid == context.Oid)
                    {
                        return;
                    }
                }
                TreeNode node3 = node.Nodes.Add(context.Label);
                node3.ImageIndex = node3.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE");
                node3.Tag = context;
            }
        }

        private void AfterContextDeleted(Guid contextOid)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context;
            if ((node != null) && (((string) node.Tag) == "Context"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEMetaContext) node2.Tag).Oid == contextOid)
                    {
                        node.Nodes.Remove(node2);
                        break;
                    }
                }
            }
        }

        private void AfterContextModified(DEMetaContext context)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context;
            if ((node != null) && (((string) node.Tag) == "Context"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEMetaContext) node2.Tag).Oid == context.Oid)
                    {
                        node2.Text = context.Label;
                        node2.Tag = context;
                        break;
                    }
                }
            }
        }

        public void AfterCreateRoleInGroup(DERole deRole)
        {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            ListViewItem item = new ListViewItem(deRole.Name, iconIndex) {
                Tag = deRole
            };
            string str = "可用";
            string str2 = "通用角色";
            switch (deRole.RoleType)
            {
                case "P":
                    str2 = "项目角色";
                    break;

                case "C":
                    str2 = "通用角色";
                    break;

                case "S":
                    str2 = "协同工作区角色";
                    break;

                default:
                    str2 = "通用角色";
                    break;
            }
            item.SubItems.AddRange(new string[] { str, str2, deRole.Creator, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), deRole.Description });
            this.GetRoleWnd().lvwRole.Items.Add(item);
        }

        public void AfterDeleteRoleGroup(DERoleGroup deRoleGroup)
        {
            TreeNode selectedNode = this.tvwNavigator.SelectedNode;
            selectedNode.Parent.Nodes.Remove(selectedNode);
        }

        public void AfterRefreshRoleGroup()
        {
            TreeNode selectedNode = this.tvwNavigator.SelectedNode;
            this.GetRoleWnd().RoleGroupRefresh((DERoleGroup) selectedNode.Tag);
        }

        public void AfterRenameRoleInGroup()
        {
            this.tvwNavigator.LabelEdit = true;
            this.tvwNavigator.SelectedNode.BeginEdit();
        }

        private void AfterViewSchemaCreated(DEItemsViewSchema schema)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewSchema;
            if ((node != null) && (((string) node.Tag) == "ViewSchema"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEItemsViewSchema) node2.Tag).Oid == schema.Oid)
                    {
                        return;
                    }
                }
                TreeNode node3 = node.Nodes.Add(schema.Name);
                node3.ImageIndex = node3.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE");
                node3.Tag = schema;
            }
        }

        private void AfterViewSchemaDeleted(Guid schemaOid)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewSchema;
            if ((node != null) && (((string) node.Tag) == "ViewSchema"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEItemsViewSchema) node2.Tag).Oid == schemaOid)
                    {
                        node.Nodes.Remove(node2);
                        break;
                    }
                }
            }
        }

        private void AfterViewSchemaModified(DEItemsViewSchema schema)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewSchema;
            if ((node != null) && (((string) node.Tag) == "ViewSchema"))
            {
                foreach (TreeNode node2 in node.Nodes)
                {
                    if (((DEItemsViewSchema) node2.Tag).Oid == schema.Oid)
                    {
                        node2.Text = schema.Name;
                        node2.Tag = schema;
                        break;
                    }
                }
            }
        }

        public void BrowserRefresh(object sender, EventArgs e)
        {
            this.RefreshBrowser();
        }

        public ContextMenu BuildMenu(string menuName, TreeNode node)
        {
            MenuItemEx ex2;
            this.cmuCommon.MenuItems.Clear();
            this.curContextMenu = "";
            MenuItemEx item = null;
            if (this.deCurrentAdminRole.ParentAdminRole != Guid.Empty)
            {
                ArrayList members = new PLAdminRole().GetMembers(this.deCurrentAdminRole.Oid, "Oper");
                for (int i = 0; i < members.Count; i++)
                {
                    DEAdminOperation operation = (DEAdminOperation) members[i];
                    switch (operation.Code)
                    {
                        case "CreateOrg":
                            this.displayCreateOrg = true;
                            break;

                        case "ModifyOrg":
                            this.displayModifyOrg = true;
                            break;

                        case "DeleteOrg":
                            this.displayDeleteOrg = true;
                            break;

                        case "CreateUser":
                            this.displayCreateUser = true;
                            break;

                        case "CreateRole":
                            this.displayCreateRole = true;
                            break;

                        case "DeleteRole":
                            this.displayDeleteRole = true;
                            break;

                        case "ModifyRole":
                            this.displayModifyRole = true;
                            break;
                    }
                }
            }
            else
            {
                this.displayCreateOrg = true;
                this.displayDeleteOrg = true;
                this.displayModifyOrg = true;
                this.displayCreateUser = true;
                this.displayCreateRole = true;
                this.displayDeleteRole = true;
                this.displayModifyRole = true;
            }
            switch (menuName)
            {
                case "DataModel":
                    if (this.allowConfigDataModel)
                    {
                        item = new MenuItemEx("&Retrive", "刷新(&R)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.OnRetriveFromDB);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Define Template", "定义网格模板", null, null);
                        item.Click += new EventHandler(this.OnDefineDGTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Define Template", "定义默认类和关联显示", null, null);
                        item.Click += new EventHandler(this.OnDefineDisplaySchema);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Check DataModel", "检查一致性(&C)", null, null);
                        item.Click += new EventHandler(this.OnCheckDataModel);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Update Views", "重建对象视图(&V)", null, null);
                        item.Click += new EventHandler(this.OnUpdateAllViews);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "DataModel";
                    }
                    goto Label_380B;

                case "Class":
                    if (this.allowConfigDataModel)
                    {
                    }
                    goto Label_380B;

                case "Relation":
                    if (this.allowConfigDataModel)
                    {
                        item = new MenuItemEx("&New Relation", "新建关联(&N)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_NEW_RELATION")
                        };
                        item.Click += new EventHandler(this.OnNewRelation);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "Relation";
                    }
                    goto Label_380B;

                case "Context":
                    if (this.allowConfigDataModel)
                    {
                        item = new MenuItemEx("New Context", "新建上下文(&N)", null, null);
                        item.Click += new EventHandler(this.OnNewContext);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Refresh Contexts", "刷新(&R)", null, null);
                        item.Click += new EventHandler(this.OnRefreshContexts);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    goto Label_380B;

                case "ViewSchema":
                    if (this.allowConfigDataModel)
                    {
                        item = new MenuItemEx("New Schema", "新建显示方案(&N)", null, null);
                        item.Click += new EventHandler(this.OnNewViewSchema);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Refresh Schemas", "刷新(&R)", null, null);
                        item.Click += new EventHandler(this.OnRefreshSchemas);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Admin.DataModel.DEMetaClass":
                {
                    object tag = this.tvwNavigator.SelectedNode.Tag;
                    if ((tag != null) && typeof(DEMetaClass).IsInstanceOfType(tag))
                    {
                        if ((!((DEMetaClass) tag).IsRefResClass && !((DEMetaClass) tag).IsOuterResClass) && ((DEMetaClass) tag).IsInheritable)
                        {
                            item = new MenuItemEx("&New Sub Class", "新建子类(&N)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_NEW_CLASS")
                            };
                            item.Click += new EventHandler(this.OnNewSubClass);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        if (ModelContext.MetaModel.IsForm(((DEMetaClass) tag).Name))
                        {
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("Page Setting", "设置分页显示", null, null);
                            item.Click += new EventHandler(this.OnFormPageSetting);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        if ((!((DEMetaClass) tag).Parent.Equals(Guid.Empty) && (((DEMetaClass) tag).SystemClass == 'N')) && (((DEMetaClass) tag).Status == 'E'))
                        {
                            if (this.cmuCommon.MenuItems.Count > 0)
                            {
                                item = new MenuItemEx("-", "-", null, null);
                                this.cmuCommon.MenuItems.Add(item);
                            }
                            item = new MenuItemEx("&Delete", "删除(&D)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                            };
                            item.Click += new EventHandler(this.OnClassDelete);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                    }
                    goto Label_380B;
                }
                case "Thyt.TiPLM.DEL.Admin.DataModel.DEMetaRelation":
                {
                    object o = this.tvwNavigator.SelectedNode.Tag;
                    if (((o != null) && typeof(DEMetaRelation).IsInstanceOfType(o)) && ((((DEMetaRelation) o).SystemRelation == 'N') && (((DEMetaRelation) o).Status == 'E')))
                    {
                        if (this.cmuCommon.MenuItems.Count > 0)
                        {
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        item = new MenuItemEx("&Delete", "删除(&D)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        item.Click += new EventHandler(this.OnRelationDelete);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    goto Label_380B;
                }
                case "Thyt.TiPLM.DEL.Admin.DataModel.DEMetaContext":
                    item = new MenuItemEx("Open Context", "打开(&O)", null, null);
                    item.Click += new EventHandler(this.OnOpenContext);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("Delete Context", "删除(&D)", null, null);
                    item.Click += new EventHandler(this.OnDeleteContext);
                    this.cmuCommon.MenuItems.Add(item);
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Product.DEItemsViewSchema":
                    item = new MenuItemEx("Open Schema", "打开(&O)", null, null);
                    item.Click += new EventHandler(this.OnOpenViewSchema);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("Delete Schema", "删除(&D)", null, null);
                    item.Click += new EventHandler(this.OnDeleteViewSchema);
                    this.cmuCommon.MenuItems.Add(item);
                    goto Label_380B;

                case "Organization":
                {
                    if (!this.allowConfigOrgModel)
                    {
                        goto Label_380B;
                    }
                    TreeNode node2 = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization;
                    DEOrganization organization = new DEOrganization();
                    PLOrganization organization2 = new PLOrganization();
                    if ((organization2.GetRootOrg() != null) || !(this.deCurrentAdminRole.ParentAdminRole == Guid.Empty))
                    {
                        if (node2.Text != "组织")
                        {
                            if (this.displayCreateOrg)
                            {
                                item = new MenuItemEx("&New Organization", "新建子组织(&N)", null, null) {
                                    ImageList = ClientData.MyImageList.imageList,
                                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG_ADD")
                                };
                                item.Click += new EventHandler(this.NewOrg);
                                this.cmuCommon.MenuItems.Add(item);
                                item = new MenuItemEx("-", "-", null, null);
                                this.cmuCommon.MenuItems.Add(item);
                            }
                            item = new MenuItemEx("Organization &Property", "属性(&P)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                            };
                            item.Click += new EventHandler(this.OrgProp);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                            if (this.displayDeleteOrg)
                            {
                                item = new MenuItemEx("Organization &Delete", "删除(&D)", null, null) {
                                    ImageList = ClientData.MyImageList.imageList,
                                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                                };
                                item.Click += new EventHandler(this.OrgDel);
                                this.cmuCommon.MenuItems.Add(item);
                            }
                            if (this.displayModifyOrg)
                            {
                                item = new MenuItemEx("Organization &Rename", "重命名(&R)", null, null);
                                item.Click += new EventHandler(this.OrgRename);
                                this.cmuCommon.MenuItems.Add(item);
                                item = new MenuItemEx("-", "-", null, null);
                                this.cmuCommon.MenuItems.Add(item);
                            }
                            item = new MenuItemEx("Organization Re&Fresh", "刷新(&F)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                            };
                            item.Click += new EventHandler(this.OrgRefresh);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        break;
                    }
                    item = new MenuItemEx("&New Organization", "新建根组织(&N)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG_ADD")
                    };
                    item.Click += new EventHandler(this.NewOrg);
                    this.cmuCommon.MenuItems.Add(item);
                    break;
                }
                case "Thyt.TiPLM.DEL.Admin.NewResponsibility.DEOrganization":
                    if (this.displayCreateOrg)
                    {
                        item = new MenuItemEx("&New Organization", "新建子组织(&N)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG_ADD")
                        };
                        item.Click += new EventHandler(this.NewOrg);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    item = new MenuItemEx("Organization &Property", "属性(&P)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                    };
                    item.Click += new EventHandler(this.OrgProp);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    if (this.displayDeleteOrg)
                    {
                        item = new MenuItemEx("Organization &Delete", "删除(&D)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        item.Click += new EventHandler(this.OrgDel);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (this.displayModifyOrg)
                    {
                        item = new MenuItemEx("Organization &Rename", "重命名(&R)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_RENAME")
                        };
                        item.Click += new EventHandler(this.OrgRename);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (ClientData.LogonUser.LogId == "sysadmin")
                    {
                        item = new MenuItemEx("BatchProcess", "批量操作", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_SINGLESELECT")
                        };
                        ex2 = new MenuItemEx("(&)Download Templet", "下载组织模板(&D)", null, null);
                        ex2.Click += new EventHandler(this.DownloadORGTemp);
                        item.MenuItems.Add(0, ex2);
                        ex2 = new MenuItemEx("(&)Import ORG", "组织导入(&I)", null, null);
                        ex2.Click += new EventHandler(this.ImportSubOrg);
                        item.MenuItems.Add(1, ex2);
                        ex2 = new MenuItemEx("(&)Emport ORG", "组织导出(&E)", null, null);
                        ex2.Click += new EventHandler(this.ExportSubOrg);
                        item.MenuItems.Add(2, ex2);
                        ex2 = new MenuItemEx("(&)Import ORGUser", "人员导入(&U)", null, null);
                        ex2.Click += new EventHandler(this.ImportORGUser);
                        item.MenuItems.Add(3, ex2);
                        ex2 = new MenuItemEx("(&)Emport ORGUser", "人员导出(&S)", null, null);
                        ex2.Click += new EventHandler(this.ExportORGUser);
                        item.MenuItems.Add(4, ex2);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    this.ClearSplitMenuItem(this.cmuCommon);
                    this.curContextMenu = "Organization";
                    goto Label_380B;

                case "User":
                    if (this.allowConfigOrgModel)
                    {
                        if (this.displayCreateUser)
                        {
                            item = new MenuItemEx("&New User", "新建用户(&N)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_USER_ADD")
                            };
                            item.Click += new EventHandler(this.NewUser);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        item = new MenuItemEx("User Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.UserRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("PassWordRule", "口令合法性规则", null, null);
                        item.Click += new EventHandler(this.PassWordRule);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("ElecSign", "电子签名", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_SINGLESELECT"),
                            MdiList = true
                        };
                        ex2 = new MenuItemEx("(&)Import ElecSign", "签名导入(&I)", null, null);
                        ex2.Click += new EventHandler(this.ImportElecSign);
                        item.MenuItems.Add(0, ex2);
                        ex2 = new MenuItemEx("(&)Emport ElecSign", "签名导出(&E)", null, null);
                        ex2.Click += new EventHandler(this.ExportElecSign);
                        item.MenuItems.Add(1, ex2);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("BatchProcess", "批量操作", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_SINGLESELECT"),
                            MdiList = true
                        };
                        ex2 = new MenuItemEx("(&)Download Templet", "下载人员模板(&D)", null, null);
                        ex2.Click += new EventHandler(this.DownloadUserTemp);
                        item.MenuItems.Add(0, ex2);
                        ex2 = new MenuItemEx("(&)Import User", "人员导入(&I)", null, null);
                        ex2.Click += new EventHandler(this.ImportUser);
                        item.MenuItems.Add(1, ex2);
                        ex2 = new MenuItemEx("(&)Emport User", "人员导出(&E)", null, null);
                        ex2.Click += new EventHandler(this.ExportUser);
                        item.MenuItems.Add(2, ex2);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "User";
                        this.ClearSplitMenuItem(this.cmuCommon);
                    }
                    goto Label_380B;

                case "Role":
                    if (this.allowConfigOrgModel)
                    {
                        if (this.displayCreateRole)
                        {
                            item = new MenuItemEx("&New Role", "新建角色(&N)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE_ADD")
                            };
                            item.Click += new EventHandler(this.NewRole);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("&NewRoleGroup", "新建分组(&G)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE")
                            };
                            item.Click += new EventHandler(this.OnNewRoleGroup);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        item = new MenuItemEx("&Paste Role", "粘贴(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PSM_PASTE")
                        };
                        item.Click += new EventHandler(this.PasteRoles);
                        FrmRoleList roleWnd = this.GetRoleWnd();
                        if ((roleWnd.cuttedDataList != null) && (roleWnd.cuttedDataList.Count > 0))
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Role Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.RoleRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "Role";
                        item = new MenuItemEx("Role Operations", "批量管理(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_SINGLESELECT")
                        };
                        item.Click += new EventHandler(this.RoleRefresh);
                        ex2 = new MenuItemEx("(&)Download RoleTemplet", "下载角色模板(&D)", null, null);
                        ex2.Click += new EventHandler(this.DownloadRoleTemp);
                        item.MenuItems.Add(0, ex2);
                        ex2 = new MenuItemEx("(&)Import Role", "角色导入(&I)", null, null);
                        ex2.Click += new EventHandler(this.ImportRole);
                        item.MenuItems.Add(1, ex2);
                        ex2 = new MenuItemEx("(&)Emport Role", "角色导出(&E)", null, null);
                        ex2.Click += new EventHandler(this.ExportRole);
                        item.MenuItems.Add(2, ex2);
                        ex2 = new MenuItemEx("(&)Emport RoleUser", "人员导出(&S)", null, null);
                        ex2.Click += new EventHandler(this.ExportRoleUser);
                        item.MenuItems.Add(3, ex2);
                        this.cmuCommon.MenuItems.Add(item);
                        this.ClearSplitMenuItem(this.cmuCommon);
                    }
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Admin.NewResponsibility.DERoleGroup":
                    if (this.allowConfigOrgModel)
                    {
                        RoleGroupMenuProcess process = new RoleGroupMenuProcess((DERoleGroup) this.tvwNavigator.SelectedNode.Tag) {
                            associateWithTreeView = true,
                            tvwNavigator = this.tvwNavigator,
                            frmRoleList = this.GetRoleWnd(),
                            displayCreateRole = this.displayCreateRole,
                            displayDeleteOrg = this.displayDeleteOrg,
                            displayModifyRole = this.displayModifyRole
                        };
                        if (this.displayCreateRole)
                        {
                            this.cmuCommon.MenuItems.Add(process.CreateMenuItem_NewRole());
                            process.AfterCreateRoleInGroup = (PLM_RoleGroup_AfterCreateRoleInGroup) Delegate.Combine(process.AfterCreateRoleInGroup, new PLM_RoleGroup_AfterCreateRoleInGroup(this.AfterCreateRoleInGroup));
                            this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Spliter());
                        }
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_ReName());
                        process.AfterRename = (PLM_RoleGroup_AfterRename) Delegate.Combine(process.AfterRename, new PLM_RoleGroup_AfterRename(this.AfterRenameRoleInGroup));
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Spliter());
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Delete());
                        process.AfterDeleteRoleGroup = (PLM_RoleGroup_AfterDeleteRoleGroup) Delegate.Combine(process.AfterDeleteRoleGroup, new PLM_RoleGroup_AfterDeleteRoleGroup(this.AfterDeleteRoleGroup));
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Spliter());
                        item = new MenuItemEx("&Paste Role", "粘贴(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PSM_PASTE")
                        };
                        item.Click += new EventHandler(this.PasteRoles);
                        if ((process.frmRoleList.cuttedDataList != null) && (process.frmRoleList.cuttedDataList.Count > 0))
                        {
                            item.Enabled = true;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                        this.cmuCommon.MenuItems.Add(item);
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Spliter());
                        this.cmuCommon.MenuItems.Add(process.CreateMenuItem_Refresh());
                        process.AfterRefreshRoleGroup = (PLM_RoleGroup_AfterRefreshRoleGroup) Delegate.Combine(process.AfterRefreshRoleGroup, new PLM_RoleGroup_AfterRefreshRoleGroup(this.AfterRefreshRoleGroup));
                        item = new MenuItemEx("Role Operations", "批量管理(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_SINGLESELECT")
                        };
                        item.Click += new EventHandler(this.RoleRefresh);
                        ex2 = new MenuItemEx("(&)Download RoleTemplet", "下载角色模板(&D)", null, null);
                        ex2.Click += new EventHandler(this.DownloadRoleTemp);
                        item.MenuItems.Add(0, ex2);
                        ex2 = new MenuItemEx("(&)Import Role", "角色导入(&I)", null, null);
                        ex2.Click += new EventHandler(this.ImportRoleInGroup);
                        item.MenuItems.Add(1, ex2);
                        ex2 = new MenuItemEx("(&)Emport Role", "角色导出(&E)", null, null);
                        ex2.Click += new EventHandler(this.ExportRoleInGroup);
                        item.MenuItems.Add(2, ex2);
                        ex2 = new MenuItemEx("(&)Emport RoleUser", "人员导出(&S)", null, null);
                        ex2.Click += new EventHandler(this.ExportRoleUserInGroup);
                        item.MenuItems.Add(3, ex2);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    goto Label_380B;

                case "AdminRole":
                    if (this.allowConfigOrgModel)
                    {
                        this.ClearSplitMenuItem(this.cmuCommon);
                    }
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Admin.NewResponsibility.DEAdminRole":
                    if (this.allowConfigOrgModel)
                    {
                        if ((this.tvwNavigator.SelectedNode != null) && (this.tvwNavigator.SelectedNode == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0]))
                        {
                            item = new MenuItemEx("&New AdminRole", "新建子管理角色(&N)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ADMINROLE_ADD")
                            };
                            item.Click += new EventHandler(this.NewAdminRole);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        if ((this.tvwNavigator.SelectedNode != null) && (this.tvwNavigator.SelectedNode != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0]))
                        {
                            if (this.tvwNavigator.SelectedNode.Parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0])
                            {
                                item = new MenuItemEx("AdminRole &Delete", "删除(&D)", null, null) {
                                    ImageList = ClientData.MyImageList.imageList,
                                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                                };
                                item.Click += new EventHandler(this.AdminRoleDel);
                                this.cmuCommon.MenuItems.Add(item);
                                item = new MenuItemEx("AdminRole &Rename", "重命名(&R)", null, null) {
                                    ImageList = ClientData.MyImageList.imageList,
                                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_RENAME")
                                };
                                item.Click += new EventHandler(this.AdminRoleRename);
                                this.cmuCommon.MenuItems.Add(item);
                                item = new MenuItemEx("-", "-", null, null);
                                this.cmuCommon.MenuItems.Add(item);
                            }
                            item = new MenuItemEx("AdminRole &Property", "属性(&P)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                            };
                            item.Click += new EventHandler(this.AdminRoleProp);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        if ((this.tvwNavigator.SelectedNode != null) && (this.tvwNavigator.SelectedNode.Text == "管理角色 - 企业安全管理员"))
                        {
                            item = new MenuItemEx("AdminRole &Property", "属性(&P)", null, null) {
                                ImageList = ClientData.MyImageList.imageList,
                                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                            };
                            item.Click += new EventHandler(this.AdminRoleProp);
                            this.cmuCommon.MenuItems.Add(item);
                            item = new MenuItemEx("-", "-", null, null);
                            this.cmuCommon.MenuItems.Add(item);
                        }
                        item = new MenuItemEx("AdminRole Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.AdminRoleRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "AdminRole";
                        this.ClearSplitMenuItem(this.cmuCommon);
                    }
                    goto Label_380B;

                case "Tool":
                    if (this.allowConfigTool)
                    {
                        item = new MenuItemEx("&New Tool", "新增工具软件(&N)", null, null);
                        item.Click += new EventHandler(this.NewTool);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Tool Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.ToolRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "Browser";
                    }
                    goto Label_380B;

                case "Browser":
                    if (this.allowConfigBrowser)
                    {
                        item = new MenuItemEx("New &Browser", "新增浏览器(&B)", null, null);
                        item.Click += new EventHandler(this.NewBrowser);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("New &Editor", "新增编辑器(&E)", null, null);
                        item.Click += new EventHandler(this.NewEditor);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Role Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.BrowserRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "Browser";
                    }
                    goto Label_380B;

                case "File":
                    if (this.allowConfigFileType)
                    {
                        item = new MenuItemEx("&New FileType", "新建文件类型(&N)", null, null);
                        item.Click += new EventHandler(this.NewFileType);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Role Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.FileTypeRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "File";
                    }
                    goto Label_380B;

                case "BusinessPro":
                    if (this.allowCreateProcessManagement)
                    {
                        item = new MenuItemEx("NewPro&Class", "新建分类(&C)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        item.Click += new EventHandler(this.OnNewProClass);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("New&ProTem", "新建模板(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        item.Click += new EventHandler(this.OnNewProTem);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&ImportTemplate", "导入模板(&I)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        item.Click += new EventHandler(this.OnImportTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                        this.RootPasteMi.ImageList = ClientData.MyImageList.imageList;
                        this.RootPasteMi.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_PASTE");
                        if (this.TheCuttingNode != null)
                        {
                            if (this.isCuttingNodeDeleted)
                            {
                                this.RootPasteMi.Enabled = false;
                            }
                            else if (this.TheCuttingNode.Parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                            {
                                this.RootPasteMi.Enabled = false;
                            }
                            else
                            {
                                this.RootPasteMi.Enabled = true;
                            }
                        }
                        else
                        {
                            this.RootPasteMi.Enabled = false;
                        }
                        this.RootPasteMi.Click += new EventHandler(this.OnPasteProTem);
                        this.cmuCommon.MenuItems.Add(this.RootPasteMi);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    item = new MenuItemEx("&Refresh", "刷新(&R)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                    };
                    item.Click += new EventHandler(this.OnRefresh);
                    this.cmuCommon.MenuItems.Add(item);
                    this.curContextMenu = "BusinessPro";
                    goto Label_380B;

                case "Addin":
                    if (this.allowAddinManagement)
                    {
                        item = new MenuItemEx("&New Tool", "新增插件(&N)", null, null);
                        item.Click += new EventHandler(this.NewAddin);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("Tool Re&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.AddinRefresh);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "Addin";
                    }
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Admin.BPM.DELProcessDefProperty":
                {
                    PLGrantPerm perm = new PLGrantPerm();
                    WFTEditor editor = this.HashMDiWindows[node] as WFTEditor;
                    bool flag = (node != null) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, ((DELProcessDefProperty) node.Tag).ID, "BPM_PROCESS_DEFINITION", "ClaRel_BROWSE", 0, Guid.Empty) == 1);
                    if (((editor != null) && editor.isNew) || flag)
                    {
                        item = new MenuItemEx("&OperTem", "打开模板(&O)", null, null);
                        item.Click += new EventHandler(this.OnOpenProTem);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (((editor != null) && editor.isNew) || ((node != null) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, ((DELProcessDefProperty) node.Tag).ID, "BPM_PROCESS_DEFINITION", "ClaRel_MODIFY", 0, Guid.Empty) == 1)))
                    {
                        item = new MenuItemEx("&SaveToDB", "保存到数据库(&S)", null, null);
                        item.Click += new EventHandler(this.OnSaveToDB);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (this.allowCreateProcessManagement)
                    {
                        item = new MenuItemEx("&SaveTemAs", "类似创建模板(&A)", null, null);
                        item.Click += new EventHandler(this.OnSaveProTemAs);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Export", "导出模板(&E)", null, null);
                        item.Click += new EventHandler(this.OnExportTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (((editor != null) && editor.isNew) || ((node != null) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, ((DELProcessDefProperty) node.Tag).ID, "BPM_PROCESS_DEFINITION", "ClaRel_DELETE", 0, Guid.Empty) == 1)))
                    {
                        item = new MenuItemEx("&CutTemplate", "剪切模版(&X)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_CUT")
                        };
                        item.Click += new EventHandler(this.OnCutProTem);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&DelTemplate", "删除模板(&D)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        item.Click += new EventHandler(this.OnDelProTem);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (this.IsAdminRole)
                    {
                        item = new MenuItemEx("&Authorize", "设置权限(&R)", null, null);
                        item.Click += new EventHandler(this.OnAuthorizeTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (flag || ((editor != null) && editor.isNew))
                    {
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&ProProperty", "过程属性(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                        };
                        item.Click += new EventHandler(this.OnProProperty);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Close", "关闭", null, null);
                        item.Click += new EventHandler(this.OnCloseProTem);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    this.curContextMenu = "ProcessList";
                    goto Label_380B;
                }
                case "Thyt.TiPLM.DEL.Admin.BPM.DELProcessClass":
                    if (this.allowModifyProcessManagement)
                    {
                        item = new MenuItemEx("&RenameProClass", "重命名(&R)", null, null);
                        item.Click += new EventHandler(this.OnRenameProClass);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("New&ProTem", "新建模板(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        item.Click += new EventHandler(this.OnNewProTem);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&ImportTemplate", "导入模板(&I)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        item.Click += new EventHandler(this.OnImportTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                        this.ClassPasteMi.ImageList = ClientData.MyImageList.imageList;
                        this.ClassPasteMi.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_PASTE");
                        if (this.TheCuttingNode != null)
                        {
                            if (this.isCuttingNodeDeleted)
                            {
                                this.ClassPasteMi.Enabled = false;
                            }
                            else if (this.TheCuttingNode.Parent == node)
                            {
                                this.ClassPasteMi.Enabled = false;
                            }
                            else
                            {
                                this.ClassPasteMi.Enabled = true;
                            }
                        }
                        else
                        {
                            this.ClassPasteMi.Enabled = false;
                        }
                        this.ClassPasteMi.Click += new EventHandler(this.OnPasteProTem);
                        this.cmuCommon.MenuItems.Add(this.ClassPasteMi);
                    }
                    if (this.allowDelProcessManagement)
                    {
                        item = new MenuItemEx("&DelProClass", "删除(&D)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                        };
                        item.Click += new EventHandler(this.OnDelProClass);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    this.curContextMenu = "ProcessList";
                    goto Label_380B;

                case "Thyt.TiPLM.CLT.Admin.BPM.Modeler.StartNode":
                case "Thyt.TiPLM.CLT.Admin.BPM.Modeler.EndNode":
                case "Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode":
                case "Thyt.TiPLM.CLT.Admin.BPM.Modeler.RouteNode":
                    item = new MenuItemEx("&NodeProperty", "属性(&P)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                    };
                    item.Click += new EventHandler(this.OnNodeProperty);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("&DelNode", "删除 (&D)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                    };
                    item.Click += new EventHandler(this.OnDelNode);
                    this.cmuCommon.MenuItems.Add(item);
                    this.curContextMenu = "ProcessNode";
                    goto Label_380B;

                case "ViewManage":
                    if (this.allowViewManagement)
                    {
                        item = new MenuItemEx("&Show All Views", "显示所有视图节点(&S)", null, null);
                        item.Click += new EventHandler(this.ShowAllViews);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("New &ViewModel", "新建视图模型(&N)", null, null);
                        item.Click += new EventHandler(this.NewViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Refresh All ViewModel", "刷新(&R)", null, null);
                        item.Click += new EventHandler(this.RefreshAllViewModel);
                        item.ImageList = ClientData.MyImageList.imageList;
                        item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "ViewManage";
                    }
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Admin.View.DEViewModel":
                {
                    bool flag2 = (this.tvwNavigator.SelectedNode.Tag is DEViewModel) && (((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Locker == Guid.Empty);
                    bool flag3 = (this.tvwNavigator.SelectedNode.Tag is DEViewModel) && (((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Locker == ClientData.LogonUser.Oid);
                    bool flag4 = (this.tvwNavigator.SelectedNode.Tag is DEViewModel) && (((DEViewModel) this.tvwNavigator.SelectedNode.Tag).IsActive == 'U');
                    item = new MenuItemEx("&Open ViewModel", "打开视图模型(&O)", null, null);
                    item.Click += new EventHandler(this.OpenViewModel);
                    this.cmuCommon.MenuItems.Add(item);
                    if (flag2 || flag3)
                    {
                        item = new MenuItemEx("&Delete ViewModel", "删除视图模型(&D)", null, null);
                        item.Click += new EventHandler(this.DeleteViewModel);
                        item.ImageList = ClientData.MyImageList.imageList;
                        item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("ViewModel &Property", "视图模型属性(&P)", null, null);
                    item.Click += new EventHandler(this.ViewModelProperty);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    if (flag2 && flag4)
                    {
                        item = new MenuItemEx("&Edit ViewModel", "编辑视图模型(&E)", null, null);
                        item.Click += new EventHandler(this.EditViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Active ViewModel", "激活视图模型(&A)", null, null);
                        item.Click += new EventHandler(this.ActiveViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (!flag4)
                    {
                        item = new MenuItemEx("&UnActive ViewModel", "取消激活(&U)", null, null);
                        item.Click += new EventHandler(this.UnactiveViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    if (!flag2 && flag4)
                    {
                        item = new MenuItemEx("&Cancel Edit", "取消编辑(&C)", null, null);
                        item.Click += new EventHandler(this.CancelEditViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Finish Edit", "结束编辑(&F)", null, null);
                        item.Click += new EventHandler(this.FinishEditViewModel);
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("&Refresh ViewModel", "刷新(&R)", null, null);
                    item.Click += new EventHandler(this.RefreshViewModel);
                    item.ImageList = ClientData.MyImageList.imageList;
                    item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    if (flag3)
                    {
                        item = new MenuItemEx("&Save ViewModel", "保存视图模型(&S)", null, null);
                        item.Click += new EventHandler(this.SaveViewModel);
                        item.ImageList = ClientData.MyImageList.imageList;
                        item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_SAVE");
                        this.cmuCommon.MenuItems.Add(item);
                    }
                    item = new MenuItemEx("C&Lose ViewModel", "关闭视图模型(&L)", null, null);
                    item.Click += new EventHandler(this.CloseViewModel);
                    item.ImageList = ClientData.MyImageList.imageList;
                    item.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_EXIT");
                    this.cmuCommon.MenuItems.Add(item);
                    this.curContextMenu = "ViewManage";
                    goto Label_380B;
                }
                case "PPCARD":
                    if (this.allowConfigPPCardTemplate || this.allowConfigFormTemplate)
                    {
                        item = new MenuItemEx("&New…", "新建(&N)…", null, null);
                        item.Click += new EventHandler(this.NewTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.FreshTemplates);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "PPCARD";
                    }
                    goto Label_380B;

                case "OUTPUTTEMPLATE":
                    if (this.allowConfigOutputTemplate)
                    {
                        item = new MenuItemEx("&New…", "新建(&N)…", null, null);
                        item.Click += new EventHandler(this.NewTemplate);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Fresh", "刷新(&F)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                        };
                        item.Click += new EventHandler(this.FreshTemplates);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "OUTPUTTEMPLATE";
                    }
                    goto Label_380B;

                case "RULE":
                    if (this.allowDataIntegrateRule)
                    {
                        MenuItemEx ex3 = null;
                        item = new MenuItemEx("&New", "新建模版(&N)", null, null);
                        ex3 = new MenuItemEx("&ExcelRule", "数据文件规则", null, null);
                        ex3.Click += new EventHandler(this.CreateExcRule);
                        item.MenuItems.Add(ex3);
                        ex3 = new MenuItemEx("&2dCADRule", "二维提取规则", null, null);
                        ex3.Click += new EventHandler(this.Create2DRule);
                        item.MenuItems.Add(ex3);
                        if (!ConstCommon.FUNCTION_EDMS)
                        {
                            this.Create3dCadRuleMenus(item);
                            ex3 = new MenuItemEx("&2d3dCADRule", "工程图集成规则", null, null);
                            ex3.Click += new EventHandler(this.Create2D3DRule);
                            item.MenuItems.Add(ex3);
                        }
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&New", "对象属性", null, null);
                        ex3 = new MenuItemEx("&ProeParas", "属性参数", null, null);
                        ex3.Click += new EventHandler(this.Creat3DAttr);
                        item.MenuItems.Add(ex3);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&ResMapped", "资源映射", null, null);
                        item.Click += new EventHandler(this.OnResMapped);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuCommon.MenuItems.Add(item);
                        item = new MenuItemEx("&Refresh…", "刷新(&R)", null, null);
                        item.Click += new EventHandler(this.RefreshCurrentFrm);
                        this.cmuCommon.MenuItems.Add(item);
                        this.curContextMenu = "RULE";
                    }
                    goto Label_380B;

                case "PRINT":
                    item = new MenuItemEx("&New…", "新建(&N)…", null, null);
                    item.Click += new EventHandler(this.NewPrintTemplate);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx("&Fresh", "刷新(&F)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                    };
                    item.Click += new EventHandler(this.FreshTemplates);
                    this.cmuCommon.MenuItems.Add(item);
                    goto Label_380B;

                case "EXTENDED_MODEL":
                    item = new MenuItemEx {
                        Text = "创建模型"
                    };
                    item.Click += new EventHandler(this.CreateExtModel);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx {
                        Text = "刷新",
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                    };
                    item.Click += new EventHandler(this.RefreshExtModel);
                    this.cmuCommon.MenuItems.Add(item);
                    goto Label_380B;

                case "Thyt.TiPLM.DEL.Utility.DEExtendedModel":
                    item = new MenuItemEx {
                        Text = "激活模型"
                    };
                    item.Click += new EventHandler(this.ActivateModel);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx {
                        Text = "-"
                    };
                    item.Click += new EventHandler(this.ActivateModel);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE"),
                        Text = "删除"
                    };
                    item.Click += new EventHandler(this.DeleteExtModel);
                    this.cmuCommon.MenuItems.Add(item);
                    item = new MenuItemEx {
                        ImageList = ClientData.MyImageList.imageList,
                        Text = "属性",
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY")
                    };
                    item.Click += new EventHandler(this.ModifyExtModel);
                    this.cmuCommon.MenuItems.Add(item);
                    goto Label_380B;

                case "CustomException":
                    item = new MenuItemEx("Exception Class", "异常类型管理(&C)", null, null);
                    item.Click += new EventHandler(this.OnExceptionClass);
                    this.cmuCommon.MenuItems.Add(item);
                    this.curContextMenu = "CustomException";
                    goto Label_380B;

                default:
                    goto Label_380B;
            }
            this.curContextMenu = "Organization";
            this.ClearSplitMenuItem(this.cmuCommon);
        Label_380B:
            return this.cmuCommon;
        }

        private void BuildRoleGroupNode(ArrayList allRoleGroupList)
        {
            if ((allRoleGroupList != null) && (allRoleGroupList.Count > 0))
            {
                foreach (DERoleGroup group in allRoleGroupList)
                {
                    TreeNode node = new TreeNode(group.Name) {
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                        SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN"),
                        Tag = group
                    };
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Add(node);
                }
            }
        }

        public void CancelEditViewModel(object sender, EventArgs e)
        {
            DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
            if (tag == null)
            {
                MessageBox.Show("取消编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                PLViewModel model2 = new PLViewModel();
                try
                {
                    model2.ChangeVMLocker(tag.Oid, Guid.Empty);
                    tag.Locker = Guid.Empty;
                    this.tvwNavigator.SelectedNode.Tag = tag;
                    IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Key is DEViewModel)
                        {
                            DEViewModel key = (DEViewModel) enumerator.Key;
                            DEViewModel model4 = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                            if (key.Oid.Equals(model4.Oid))
                            {
                                VMFrame frame = (VMFrame) enumerator.Value;
                                frame.Show();
                                frame.Activate();
                                if (MessageBox.Show("取消编辑会丢失视图模型尚未保存的所有修改！\n您确定要取消编辑？", "视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
                                {
                                    frame.RefreshViewModel();
                                }
                                return;
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("取消编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”的操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void CheckDataModel(object objs)
        {
            IProgressCallback callback = (objs as ArrayList)[0] as IProgressCallback;
            try
            {
                callback.Begin(0, 100);
                callback.SetText("正在对数据模型进行一致性检查……");
                try
                {
                    string[] c = PLDataModel.Agent.CheckDataModel();
                    if ((c != null) && (c.Length > 0))
                    {
                        (objs as ArrayList).Add(new ArrayList(c));
                    }
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        public void ChooseClassNodeInNavigator(object cls)
        {
            if (cls is DEMetaClass)
            {
                TreeNode root = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class;
                TreeNode node2 = this.FindRightNode(root, ((DEMetaClass) cls).Oid);
                if (node2 != null)
                {
                    this.tvwNavigator.SelectedNode = node2;
                }
            }
        }

        public void ChooseRelationNodeInNavigator(DEMetaRelation rlt)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation;
            node.Expand();
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                DEMetaRelation tag = (DEMetaRelation) node.Nodes[i].Tag;
                if (tag.Oid.Equals(rlt.Oid))
                {
                    this.tvwNavigator.SelectedNode = node.Nodes[i];
                    return;
                }
            }
        }

        private void ClearSplitMenuItem(ContextMenu menu)
        {
            if ((menu.MenuItems.Count > 0) && (menu.MenuItems[menu.MenuItems.Count - 1].Text == "-"))
            {
                menu.MenuItems.RemoveAt(menu.MenuItems.Count - 1);
            }
        }

        private void CloseProcessTemplate()
        {
            if (this.IfProcessWindowActived())
            {
                ((WFTEditor) base.ActiveMdiChild).Close();
            }
        }

        private void CloseProTem()
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
            if (editor != null)
            {
                editor.Close();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strTempNotOpen"));
            }
        }

        public void CloseViewModel(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key is DEViewModel)
                {
                    DEViewModel key = (DEViewModel) enumerator.Key;
                    DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                    if (key.Oid.Equals(tag.Oid))
                    {
                        VMFrame frame = (VMFrame) enumerator.Value;
                        frame.Show();
                        frame.Activate();
                        frame.Close();
                        return;
                    }
                }
            }
            MessageBox.Show("视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”没有打开！", "关闭视图模型", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void cmuCommon_Popup(object sender, EventArgs e)
        {
            string str;
            if ((((str = this.curContextMenu) != null) && (str != "DataModel")) && ((str == "Class") || (str == "Relation")))
            {
                this.cmuCommon.MenuItems[0].Enabled = true;
            }
        }

        private void CommonRule(string ruleName, ConstUtility.RuleDefType ruleType)
        {
            ArrayList list = new ArrayList();
            FrmAddRuleName name = new FrmAddRuleName(ruleType);
            DialogResult result = name.ShowDialog();
            string importOption = null;
            switch (result)
            {
                case DialogResult.OK:
                    list.Add(name.RuleName);
                    list.Add(name.RuleDescription);
                    list.Add(name.FileName);
                    importOption = name.ImportOption;
                    break;

                case DialogResult.Cancel:
                    return;
            }
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            bool flag = false;
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmCreateRule)
                {
                    FrmCreateRule rule = (FrmCreateRule) enumerator.Value;
                    FrmCreateRule.ModifyOid = Guid.Empty;
                    rule.IsModify = false;
                    rule.defType = ruleType;
                    FrmCreateRule.FileName = (string) list[2];
                    rule.InitialRuleTypeFrm(ruleType);
                    rule.RuleDescription = (string) list[1];
                    rule.RuleName = (string) list[0];
                    rule.ImportOption = importOption;
                    rule.Text = ruleName;
                    rule.ShowTempletAttr();
                    rule.MdiParent = this;
                    rule.Show();
                    rule.Activate();
                    flag = true;
                }
            }
            if (!flag)
            {
                int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_PRJ_TASKSTAREFER");
                TreeNode node = new TreeNode("规则定义", iconIndex, iconIndex) {
                    Tag = "RULE"
                };
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.DEF_NODE_RULE = node;
                FrmCreateRule rule2 = new FrmCreateRule(this.HashMDiWindows, ruleType, 1);
                FrmCreateRule.ModifyOid = Guid.Empty;
                rule2.ToolOid = Guid.Empty;
                rule2.defType = ruleType;
                FrmCreateRule.FileName = (string) list[2];
                rule2.InitialRuleTypeFrm(ruleType);
                rule2.IsModify = false;
                rule2.ImportOption = importOption;
                rule2.RuleDescription = (string) list[1];
                rule2.RuleName = (string) list[0];
                rule2.Text = ruleName;
                rule2.ShowTempletAttr();
                this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.DEF_NODE_RULE, rule2);
                rule2.MdiParent = this;
                rule2.Show();
                rule2.Activate();
            }
            Cursor.Current = Cursors.Default;
            FrmCreateRule.FileName = "";
        }

        private void Creat3DAttr(object sender, EventArgs e)
        {
            ClientData.MyImageList.GetIconIndex("ICO_PRJ_TASKSTAREFER");
            new FrmAddTempAttr().ShowDialog();
        }

        private void Create2D3DRule(object sender, EventArgs e)
        {
            this.CommonRule("工程图集成规则", ConstUtility.RuleDefType.SWTwoDimAndThreeDimCADRule);
        }

        private void Create2DRule(object sender, EventArgs e)
        {
            this.CommonRule("二维提取规则", ConstUtility.RuleDefType.TwoDimCADRule);
        }

        private void Create3dCadRuleMenus(MenuItemEx mi)
        {
            ConstUtility.RuleDefType[] canDefineRuleType = new PLRuleDefUtil().GetCanDefineRuleType();
            if (canDefineRuleType.Length != 0)
            {
                MenuItemEx item = new MenuItemEx("&3dCADRule", "三维集成规则", null, null);
                foreach (ConstUtility.RuleDefType type in canDefineRuleType)
                {
                    string label = PLRuleDefUtil.GetLabel(type);
                    if (!string.IsNullOrEmpty(label))
                    {
                        MenuItemEx ex2 = new MenuItemEx(type.ToString(), label, null, null);
                        ex2.Click += new EventHandler(this.Create3DRule);
                        item.MenuItems.Add(ex2);
                    }
                }
                if (item.MenuItems.Count > 0)
                {
                    mi.MenuItems.Add(item);
                }
            }
        }

        private void Create3DRule(object sender, EventArgs e)
        {
            MenuItemEx ex = sender as MenuItemEx;
            if (ex != null)
            {
                this.CommonRule(ex.Text, PLRuleDefUtil.GetRuleTypeByAttrType(ex.Name));
            }
        }

        protected void CreateDockingControl()
        {
            SplashHelper.Instance.ShowSplashMessage("正在初始化主窗体的导航树……");
            this.CreateTreeView();
            Content c = TiModelerUIContainer.dockingManager.Contents.Add(this.tvwNavigator, "企业建模工具", this.ilsIcon, 0);
            TiModelerUIContainer.dockingManager.AddContentWithState(c, Crownwood.DotNetMagic.Docking.State.DockLeft);
            TiModelerUIContainer.dockingManager.OuterControl = TiModelerUIContainer.statusBar;
        }

        private void CreateDocRule(object sender, EventArgs e)
        {
        }

        protected MenuItemExCollection CreateDropDownMenu(string menuName)
        {
            string menuFileName = this.menuFileName;
            XmlDocument document = new XmlDocument();
            document.Load(menuFileName);
            XmlNodeList childNodes = document.DocumentElement.ChildNodes;
            MenuItemEx ex = null;
            foreach (XmlElement element in childNodes)
            {
                string innerXml = element.Attributes["Name"].InnerXml;
                if (innerXml.IndexOf("&") != -1)
                {
                    innerXml = innerXml.Replace("&amp;", "&");
                }
                if (innerXml == menuName)
                {
                    ex = this.ProcessMenuItem(element);
                    break;
                }
            }
            int count = ex.MenuItems.Count;
            MenuItemExCollection exs = new MenuItemExCollection();
            for (int i = 0; i < count; i++)
            {
                exs.Add((MenuItemEx) ex.MenuItems[i]);
            }
            return exs;
        }

        protected MenuItem[] CreateDropDownMenuEx(string menuName)
        {
            string menuFileName = this.menuFileName;
            XmlDocument document = new XmlDocument();
            document.Load(menuFileName);
            XmlNodeList childNodes = document.DocumentElement.ChildNodes;
            MenuItemEx ex = null;
            foreach (XmlElement element in childNodes)
            {
                string innerXml = element.Attributes["Name"].InnerXml;
                if (innerXml.IndexOf("&") != -1)
                {
                    innerXml = innerXml.Replace("&amp;", "&");
                }
                if (innerXml == menuName)
                {
                    ex = this.ProcessMenuItem(element);
                    break;
                }
            }
            int count = ex.MenuItems.Count;
            MenuItem[] itemArray = new MenuItem[count];
            for (int i = 0; i < count; i++)
            {
                itemArray[i] = ex.MenuItems[i];
            }
            return itemArray;
        }

        private void CreateExcRule(object sender, EventArgs e)
        {
            this.CommonRule("数据文件导入规则", ConstUtility.RuleDefType.ExcelRule);
        }

        private void CreateExtModel(object sender, EventArgs e)
        {
            FrmExtModel.CreateModel();
            this.RefreshExtModel();
        }

        protected MenuItem[] CreateMainMenu()
        {
            string menuFileName = this.menuFileName;
            XmlDocument document = new XmlDocument();
            document.Load(menuFileName);
            XmlNodeList childNodes = document.DocumentElement.ChildNodes;
            MenuItem[] itemArray = new MenuItem[childNodes.Count];
            int index = 0;
            foreach (XmlElement element in childNodes)
            {
                itemArray[index] = this.ProcessMenuItem(element);
                if (element.Attributes["Name"].InnerXml.IndexOf("Window") != -1)
                {
                    itemArray[index].MdiList = true;
                }
                string innerXml = element.Attributes["Name"].InnerXml;
                if (innerXml == "Data Model")
                {
                    itemArray[index].Popup += new EventHandler(this.OnDMMenuPopup);
                }
                else if (innerXml.Equals("ViewNetwork"))
                {
                    itemArray[index].Popup += new EventHandler(this.OnVMMenuPopup);
                }
                index++;
            }
            return itemArray;
        }

        private void createNewWFTEditor()
        {
            if (!this.allowCreateProcessManagement)
            {
                MessageBox.Show("您没有创建业务过程模型的权限！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                FrmProcessEdit processDlg = new FrmProcessEdit(true);
                DELProcessDefinition template = new DELProcessDefinition {
                    CreatorID = BPMClient.UserID,
                    CreatorName = BPMClient.UserName,
                    AddInitiatorAsMonitor = true
                };
                WFTEditor owner = new WFTEditor(this) {
                    MdiParent = this,
                    isNew = true,
                    viewPanel = { shapeData = { template = template } },
                    proTemplate = template
                };
                processDlg.proTemplate = template;
                owner.viewPanel.InitFrmProcessDlg(processDlg, template);
                owner.Show();
                if (processDlg.ShowDialog(owner) == DialogResult.OK)
                {
                    owner.viewPanel.ModifyProcessValue(processDlg, template);
                    DELProcessDefProperty proDef = new DELProcessDefProperty(owner.viewPanel.shapeData.template);
                    TreeNode node = new TreeNode(proDef.Name) {
                        Tag = proDef,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                        SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                    };
                    if ((this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
                    {
                        this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
                    }
                    TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                    if ((selectedNode.Tag is DELProcessClass) || (selectedNode.Parent.Tag is DELProcessClass))
                    {
                        if (selectedNode.Parent.Tag is DELProcessClass)
                        {
                            selectedNode = selectedNode.Parent;
                        }
                        int index = this.FindPosition(selectedNode, proDef);
                        selectedNode.Nodes.Insert(index, node);
                        this.tvwNavigator.SelectedNode = node;
                        this.HashMDiWindows.Add(node, owner);
                        owner.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + owner.viewPanel.shapeData.template.Name;
                        this.setNodesForProcess(node, owner.viewPanel);
                        this.AddOneProcess(node);
                        DELProcessClass tag = selectedNode.Tag as DELProcessClass;
                        BPMProcessor processor = new BPMProcessor();
                        if (processor.MoveProcessBetweenClass(proDef.ID, Guid.Empty, tag.ID))
                        {
                            tag.AddProcess(proDef.ID);
                        }
                    }
                    else if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM != null)
                    {
                        int num2 = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, proDef);
                        Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(num2, node);
                        this.tvwNavigator.SelectedNode = node;
                        this.HashMDiWindows.Add(node, owner);
                        owner.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + owner.viewPanel.shapeData.template.Name;
                        this.setNodesForProcess(node, owner.viewPanel);
                        this.AddOneProcess(node);
                    }
                }
                else
                {
                    owner.Dispose();
                }
            }
        }

        protected void CreateStatusBar()
        {
            TiModelerUIContainer.statusBar = new StatusBar();
            TiModelerUIContainer.statusBar.Dock = DockStyle.Bottom;
            TiModelerUIContainer.statusBar.ShowPanels = true;
            TiModelerUIContainer.statusBar.Size = new Size(base.Width, 0x17);
            TiModelerUIContainer.statusBar.SizingGrip = false;
            TiModelerUIContainer.sbpMain = new StatusBarPanel();
            this.sbpMiddle = new StatusBarPanel();
            this.sbpRight = new StatusBarPanel();
            this.sbpServer = new StatusBarPanel();
            TiModelerUIContainer.sbpMain.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            TiModelerUIContainer.sbpMain.Text = (string) rmTiModeler.GetObject("strReady");
            TiModelerUIContainer.sbpMain.AutoSize = StatusBarPanelAutoSize.Spring;
            this.sbpMiddle.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            this.sbpMiddle.Text = ClientData.LogonUser.Name + " [" + ClientData.LogonUser.LogId + "]  ";
            this.sbpMiddle.AutoSize = StatusBarPanelAutoSize.Contents;
            this.sbpServer.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            this.sbpServer.Text = RemoteProxy.Server + ":" + RemoteProxy.Port.ToString();
            this.sbpServer.AutoSize = StatusBarPanelAutoSize.Contents;
            this.sbpRight.BorderStyle = StatusBarPanelBorderStyle.Sunken;
            string str = "";
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    str = "星期日";
                    break;

                case DayOfWeek.Monday:
                    str = "星期一";
                    break;

                case DayOfWeek.Tuesday:
                    str = "星期二";
                    break;

                case DayOfWeek.Wednesday:
                    str = "星期三";
                    break;

                case DayOfWeek.Thursday:
                    str = "星期四";
                    break;

                case DayOfWeek.Friday:
                    str = "星期五";
                    break;

                case DayOfWeek.Saturday:
                    str = "星期六";
                    break;
            }
            this.sbpRight.Text = " " + DateTime.Today.ToString("yyyy年M月d日") + " " + str;
            this.sbpRight.AutoSize = StatusBarPanelAutoSize.Contents;
            TiModelerUIContainer.statusBar.ShowPanels = true;
            TiModelerUIContainer.statusBar.Panels.Add(TiModelerUIContainer.sbpMain);
            TiModelerUIContainer.statusBar.Panels.Add(this.sbpMiddle);
            TiModelerUIContainer.statusBar.Panels.Add(this.sbpServer);
            TiModelerUIContainer.statusBar.Panels.Add(this.sbpRight);
            TiModelerUIContainer.statusBar.Dock = DockStyle.Bottom;
            base.Controls.Add(TiModelerUIContainer.statusBar);
        }

        private void CreateTreeView()
        {
            if (ConstCommon.FUNCTION_IORS)
            {
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_ECMS_MANAGEMENT") == 1)
                {
                    this.allowEcmsDataManagement = true;
                }
                PLAdminRole role = new PLAdminRole();
                if (role.IsExistAdminRoleMember(ClientData.LogonUser.Oid))
                {
                    this.allowConfigOrgModel = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_CONFIG_PROCESS_DEFINATION") == 1)
                {
                    this.allowConfigBPM = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_DELETE") == 1)
                {
                    this.allowDelProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_CREATE") == 1)
                {
                    this.allowCreateProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_BROWSE") == 1)
                {
                    this.allowBrowseProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_MODIFY") == 1)
                {
                    this.allowModifyProcessManagement = true;
                }
            }
            else
            {
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_DATAMODEL_CONFIG") == 1)
                {
                    this.allowConfigDataModel = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_VIEW_MANAGE") == 1)
                {
                    this.allowViewManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_BROWER_CONFIG") == 1)
                {
                    this.allowConfigBrowser = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_FILETYPE_CONFIG") == 1)
                {
                    this.allowConfigFileType = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_TOOL_CONFIG") == 1)
                {
                    this.allowConfigTool = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_LIFECYCLE_MANAGEMENT") == 1)
                {
                    this.allowLifeCycleManage = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_EVENT_MANAGEMENT") == 1)
                {
                    this.allowEventManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiCAPP_FORMULA_MANAGEMENT") == 1)
                {
                    this.allowFormulaManagement = true;
                }
                if (PLGrantPerm.CanDoClassByAdminRole(ClientData.LogonUser.Oid, PLMRootClassName.INTEGRATIONRULE.ToString()))
                {
                    this.allowDataIntegrateRule = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_ADDIN_MANAGEMENT") == 1)
                {
                    this.allowAddinManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiCAPP_FORM_TEMPLATE") == 1)
                {
                    this.allowConfigFormTemplate = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiCAPP_OUTPUT_TEMPLATE") == 1)
                {
                    this.allowConfigOutputTemplate = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiCAPP_CARD_TEMPLATE") == 1)
                {
                    this.allowConfigPPCardTemplate = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_CONFIG_PROCESS_DEFINATION") == 1)
                {
                    this.allowConfigBPM = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_DELETE") == 1)
                {
                    this.allowDelProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_CREATE") == 1)
                {
                    this.allowCreateProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_BROWSE") == 1)
                {
                    this.allowBrowseProcessManagement = true;
                }
                if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, "BPM_PROCESS_DEFINITION", Guid.Empty, "ClaRel_MODIFY") == 1)
                {
                    this.allowModifyProcessManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_FOLDER_MANAGE") == 1)
                {
                    this.allowFolderManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_FOLDER_MANAGE") == 1)
                {
                    this.allowDataCheckManagement = true;
                }
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_BIZOPERATION_DEFINITION") == 1)
                {
                    this.allowOperationDefinition = true;
                }
                if (ConstCommon.FUNCTION_PLMUSEGCMS && (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_ECMS_MANAGEMENT") == 1))
                {
                    this.allowEcmsDataManagement = true;
                }
                try
                {
                    PLAdminRole role2 = new PLAdminRole();
                    if (role2.IsExistAdminRoleMember(ClientData.LogonUser.Oid))
                    {
                        this.allowConfigOrgModel = true;
                        this.allowFolderManagement = true;
                    }
                }
                catch
                {
                }
            }
            this.tvwNavigator = new TreeView();
            this.tvwNavigator.HideSelection = false;
            this.tvwNavigator.ImageList = ClientData.MyImageList.imageList;
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_TIMODELER_ROOT_ICON");
            TreeNode node = new TreeNode(this.Text, iconIndex, iconIndex) {
                Tag = "EnterpriseModel"
            };
            this.tvwNavigator.Nodes.Add(node);
            if (this.allowConfigDataModel)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_DATAMODEL");
                node = new TreeNode("数据模型定制", iconIndex, iconIndex) {
                    Tag = "DataModel"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel = node;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                TreeNode node2 = new TreeNode("类", iconIndex, iconIndex) {
                    Tag = "Class"
                };
                node.Nodes.Add(node2);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class = node2;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_RELATION");
                node2 = new TreeNode("关联", iconIndex, iconIndex) {
                    Tag = "Relation"
                };
                node.Nodes.Add(node2);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation = node2;
                if (ConstCommon.FUNCTION_CONTEXT)
                {
                    iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                    node2 = new TreeNode("上下文", iconIndex, iconIndex) {
                        Tag = "Context"
                    };
                    node.Nodes.Add(node2);
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context = node2;
                }
            }
            if (this.allowConfigOrgModel)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORGMODEL");
                node = new TreeNode("组织模型定制", iconIndex, iconIndex) {
                    Tag = "OrgModel"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_OrgModel = node;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
                TreeNode node3 = new TreeNode("组织", iconIndex, iconIndex) {
                    Tag = "Organization"
                };
                node.Nodes.Add(node3);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization = node3;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_USER");
                node3 = new TreeNode("人员", iconIndex, iconIndex) {
                    Tag = "User"
                };
                node.Nodes.Add(node3);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_User = node3;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                node3 = new TreeNode("角色", iconIndex, iconIndex) {
                    Tag = "Role"
                };
                node.Nodes.Add(node3);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role = node3;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                node3 = new TreeNode("管理角色", iconIndex, iconIndex) {
                    Tag = "AdminRole"
                };
                node.Nodes.Add(node3);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole = node3;
            }
            if (this.allowConfigBPM)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEFROOT");
                node = new TreeNode("业务过程模型定制", iconIndex, iconIndex) {
                    Tag = "BusinessPro"
                };
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM = node;
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
            }
            if (ConstCommon.FUNCTION_BIZOPERATION_DEFINITION && this.allowOperationDefinition)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_DATADICTIONARY");
                node = new TreeNode("业务操作定义", iconIndex, iconIndex) {
                    Tag = "BizOperationDefinition"
                };
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BizOperation = node;
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
            }
            if (ConstCommon.FUNCTION_MUILTIVIEW_MANAGEMENT && this.allowViewManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEWMODEL");
                node = new TreeNode("多视图管理", iconIndex, iconIndex) {
                    Tag = "ViewManage"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork = node;
            }
            if ((this.allowConfigTool || this.allowConfigBrowser) || this.allowConfigFileType)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_EXTERNAL_RESOURCE");
                node = new TreeNode("文件集成工具配置", iconIndex, iconIndex) {
                    Tag = "ExternalResource"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
            }
            TreeNode node4 = null;
            if (this.allowConfigTool)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_TOOL");
                node4 = new TreeNode("工具软件", iconIndex, iconIndex) {
                    Tag = "Tool"
                };
                node.Nodes.Add(node4);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Tool = node4;
            }
            if (this.allowConfigBrowser)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_BROWER");
                node4 = new TreeNode("浏览编辑器", iconIndex, iconIndex) {
                    Tag = "Browser"
                };
                node.Nodes.Add(node4);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Browser = node4;
            }
            if (this.allowConfigFileType)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_FILETYPE");
                node4 = new TreeNode("文件类型", iconIndex, iconIndex) {
                    Tag = "File"
                };
                node.Nodes.Add(node4);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_FileType = node4;
            }
            if (this.allowFolderManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE");
                node = new TreeNode("公共文件夹管理", iconIndex, iconIndex) {
                    Tag = "Folder"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Folder = node;
            }
            if (ConstCommon.FUNCTION_OBJECT_PRINT || ConstCommon.FUNCTION_PPM)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_PPM_TMPPACKAGE");
                node = new TreeNode("企业模板定制", iconIndex, iconIndex) {
                    Tag = "PPROOT"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                if (ConstCommon.FUNCTION_OBJECT_PRINT && this.allowConfigFormTemplate)
                {
                    iconIndex = 0;
                    TreeNode root = new TreeNode("表单输出模板", -1, -1);
                    UIDataModel.FillClassesTree(root, "FORM", 0, iconIndex, iconIndex);
                    foreach (TreeNode node6 in root.Nodes)
                    {
                        node.Nodes.Add(node6);
                        this.SetPrintIcons(node6);
                    }
                }
                if (ConstCommon.FUNCTION_PPM && this.allowConfigPPCardTemplate)
                {
                    TreeNode node7 = new TreeNode("工艺卡片输出模板", -1, -1);
                    iconIndex = ClientData.MyImageList.GetIconIndex("ICO_PPM_PPCARDTEMPLATE");
                    UIDataModel.FillClassesTree(node7, "PPCARD", 0, iconIndex, iconIndex);
                    if (node7.Nodes.Count > 0)
                    {
                        foreach (TreeNode node8 in node7.Nodes)
                        {
                            if (((DEMetaClass) node8.Tag).Name == "PPCARD")
                            {
                                node.Nodes.Add(node8);
                                this.SetPPMIcons(node8);
                                break;
                            }
                        }
                    }
                }
                if (ConstCommon.FUNCTION_PPM && this.allowConfigOutputTemplate)
                {
                    TreeNode node9 = new TreeNode("业务对象输出模板", iconIndex, iconIndex) {
                        Tag = "OUTPUTTEMPLATE"
                    };
                    node.Nodes.Add(node9);
                }
            }
            if (ConstCommon.FUNCTION_PPM)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_SYMBOL");
                node = new TreeNode("工艺符号模板", iconIndex, iconIndex) {
                    Tag = "PPSIGN_TEMPLATE"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
            }
            if (this.allowDataIntegrateRule)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_PRJ_TASKSTAREFER");
                node = new TreeNode("数据集成规则定制", iconIndex, iconIndex) {
                    Tag = "RULE"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.DEF_NODE_RULE = node;
            }
            if (this.allowAddinManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_PRJ_TASKSTAREFER");
                node = new TreeNode("插件管理", iconIndex, iconIndex) {
                    Tag = "Addin"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Addin = node;
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_RES_FENLEI");
                node = new TreeNode("扩展企业模型", iconIndex, iconIndex) {
                    Tag = "EXTENDED_MODEL"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                DEExtendedModel[] allExtendedModel = UIExtendedModel.Instance.GetAllExtendedModel();
                UIExtendedModel.Instance.FillExtendedModes(node, allExtendedModel);
            }
            if (ConstCommon.FUNCTION_FORMULA && this.allowFormulaManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_WINRPTTEMPLATE");
                node = new TreeNode("公式管理", iconIndex, iconIndex) {
                    Tag = "FormulaManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Formula = node;
            }
            if (ServiceSwitches.UseDataView)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_DATAVIEW");
                node = new TreeNode("数据视图定义", iconIndex, iconIndex) {
                    Tag = "DataViewManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataView = node;
            }
            if (ConstCommon.FUNCTION_CHART)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RPT_WINTEMPLATE");
                node = new TreeNode("统计图表定义", iconIndex, iconIndex) {
                    Tag = "ChartManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataView = node;
            }
            if (ServiceSwitches.UseDataView && this.allowDataCheckManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_DATAMODEL");
                node = new TreeNode("数据检查规则定义", iconIndex, iconIndex) {
                    Tag = "DataCheckManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataCheck = node;
            }
            if (ServiceSwitches.UseClassFormulaDef)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_DATAVIEW");
                node = new TreeNode("业务对象公式定义", iconIndex, iconIndex) {
                    Tag = "ClassFormulaManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ClassFormula = node;
            }
            if (this.allowEcmsDataManagement)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ECMS_MANAGENT");
                node = new TreeNode("编码管理", iconIndex, iconIndex) {
                    Tag = "EcmsManagement"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Ecms = node;
            }
            if (!ConstCommon.FUNCTION_IORS)
            {
                iconIndex = ClientData.MyImageList.GetIconIndex("ICO_ECMS_MANAGENT");
                node = new TreeNode("自定义异常管理", iconIndex, iconIndex) {
                    Tag = "CustomException"
                };
                this.tvwNavigator.Nodes[0].Nodes.Add(node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_CustomException = node;
            }
            this.tvwNavigator.DoubleClick += new EventHandler(this.tvwNavigator_DoubleClick);
            this.tvwNavigator.MouseUp += new MouseEventHandler(this.tvwNavigator_MouseUp);
            this.tvwNavigator.AfterSelect += new TreeViewEventHandler(this.tvwNavigator_AfterSelect);
            this.tvwNavigator.AfterLabelEdit += new NodeLabelEditEventHandler(this.tvwNavigator_AfterLabelEdit);
            this.tvwNavigator.AllowDrop = true;
            this.tvwNavigator.ItemDrag += new ItemDragEventHandler(this.tvwNavigator_ItemDrag);
            this.tvwNavigator.DragEnter += new DragEventHandler(this.tvwNavigator_DragEnter);
            this.tvwNavigator.DragOver += new DragEventHandler(this.tvwNavigator_DragOver);
            this.tvwNavigator.DragDrop += new DragEventHandler(this.tvwNavigator_DragDrop);
        }

        public void DefaultSelectMenuItem(object sender, EventArgs e)
        {
            MenuItemEx ex = (MenuItemEx) sender;
            MessageBox.Show(ex.Text);
        }

        public void DeleteClassInNavigator(DEMetaClass cls)
        {
            TreeNode root = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class;
            TreeNode node2 = this.FindRightNode(root, cls.Oid);
            if (node2 != null)
            {
                node2.Remove();
            }
        }

        private void DeleteExtModel(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("确定删除此模型吗？", "扩展企业模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
            {
                DEExtendedModel tag = this.tvwNavigator.SelectedNode.Tag as DEExtendedModel;
                UIExtendedModel.Instance.DeleteExtendedModelByOid(tag.Oid);
                this.RefreshExtModel();
            }
        }

        public void deleteProcessTemplate()
        {
            if ((this.tvwNavigator.SelectedNode == null) || !(this.tvwNavigator.SelectedNode.Tag is DELProcessDefProperty))
            {
                MessageBox.Show("请首先选中一个流程模板！", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                bool flag1 = ((DELProcessDefProperty) this.tvwNavigator.SelectedNode.Tag).CreatorID == ClientData.LogonUser.Oid;
                PLGrantPerm perm = new PLGrantPerm();
                if (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, ((DELProcessDefProperty) this.tvwNavigator.SelectedNode.Tag).ID, "BPM_PROCESS_DEFINITION", "ClaRel_DELETE", 0, Guid.Empty) == 0)
                {
                    MessageBox.Show("您没有权限删除该流程模板！", "删除提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else if (MessageBox.Show("您是否确定删除选中的模板对象？", "删除模板", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
                {
                    TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                    DELProcessDefProperty tag = null;
                    if (selectedNode.Tag is DELProcessDefProperty)
                    {
                        tag = (DELProcessDefProperty) selectedNode.Tag;
                    }
                    else if (this.frmNavigator.lvwNavigater.SelectedItems.Count > 0)
                    {
                        selectedNode = (TreeNode) this.frmNavigator.lvwNavigater.SelectedItems[0].Tag;
                        if (selectedNode.Tag is DELProcessDefProperty)
                        {
                            tag = (DELProcessDefProperty) selectedNode.Tag;
                        }
                    }
                    if (tag != null)
                    {
                        Guid iD = tag.ID;
                        BPMAdmin admin = new BPMAdmin();
                        try
                        {
                            admin.DeleteProcessDefinition(ClientData.LogonUser.Oid, iD);
                        }
                        catch (Exception exception)
                        {
                            BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                            return;
                        }
                        if (tag.InstanceCount == 0)
                        {
                            tag.IsVisible = 0;
                            tag.State = "Deleted";
                        }
                        else
                        {
                            tag.IsVisible = 0;
                        }
                        BPMAdmin admin2 = new BPMAdmin();
                        ArrayList theAllProcessRuleList = new ArrayList();
                        try
                        {
                            admin2.GetAllRulesByProcessDefinitionID(BPMClient.UserID, iD, out theAllProcessRuleList);
                            foreach (DELProcessRule rule in theAllProcessRuleList)
                            {
                                admin2.DeleteProcessRuleByOid(BPMClient.UserID, rule.ID);
                                rule.State = "DELETED";
                                rule.UpdateDate = DateTime.Now;
                                admin2.CreateProessRule(BPMClient.UserID, rule);
                            }
                        }
                        catch (Exception exception2)
                        {
                            BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception2);
                        }
                        Form form = (Form) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
                        if (form != null)
                        {
                            WFTEditor editor = (WFTEditor) form;
                            editor.isDel = true;
                            form.Close();
                        }
                        this.tvwNavigator.Nodes.Remove(selectedNode);
                        if (this.TheCuttingNode == selectedNode)
                        {
                            this.isCuttingNodeDeleted = true;
                        }
                        this.RemoveOneProcess(selectedNode);
                    }
                }
            }
        }

        public void DeleteRelationInNavigator(DEMetaRelation rlt)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation;
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                if (((DEMetaRelation) node.Nodes[i].Tag).Oid.Equals(rlt.Oid))
                {
                    node.Nodes[i].Remove();
                }
            }
        }

        public void DeleteViewModel(object sender, EventArgs e)
        {
            PLViewModel model = new PLViewModel();
            if (((DEViewModel) this.tvwNavigator.SelectedNode.Tag).IsActive == 'A')
            {
                bool flag = false;
                try
                {
                    flag = model.HasUsedViewModel(((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Oid);
                }
                catch (ViewException exception)
                {
                    MessageBox.Show(exception.Message, "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                catch
                {
                    MessageBox.Show("判断视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”是否已经与业务对象绑定失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if (flag)
                {
                    MessageBox.Show("视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”\n已经与业务对象绑定，不允许删除该视图！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
            }
            if (MessageBox.Show("确定要删除视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”吗?", "删除视图", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
            {
                try
                {
                    model.DeleteViewModel(((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Oid);
                    IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        if ((enumerator.Key is DEViewModel) && (enumerator.Key is DEViewModel))
                        {
                            DEViewModel key = (DEViewModel) enumerator.Key;
                            DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                            if (key.Oid.Equals(tag.Oid))
                            {
                                VMFrame frame = (VMFrame) enumerator.Value;
                                frame.isDeleteToClose = true;
                                frame.Close();
                                break;
                            }
                        }
                    }
                    this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork;
                }
                catch
                {
                    MessageBox.Show("删除视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”失败！", "删除视图", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        public void DelTreeNodeByShape(Shape selectedShape)
        {
            if ((selectedShape != null) && (this.tvwNavigator.SelectedNode.Tag == selectedShape))
            {
                TreeNode parent = this.tvwNavigator.SelectedNode.Parent;
                TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                this.tvwNavigator.SelectedNode = parent;
                parent.Nodes.Remove(selectedNode);
            }
        }
 
        private void DownloadORGTemp(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "电话");
                colheads.Add("2", "地址");
                colheads.Add("3", "电子邮箱");
                colheads.Add("4", "描述");
                colheads.Add("5", "扩展属性");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("zuzhi", true, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void DownloadRoleTemp(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "描述");
                colheads.Add("2", "扩展属性");
                colheads.Add("3", "角色分组");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("juese", true, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void DownloadUserTemp(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "人员标识");
                colheads.Add("1", "姓名");
                colheads.Add("2", "性别");
                colheads.Add("3", "出生日期");
                colheads.Add("4", "电子邮件");
                colheads.Add("5", "地址");
                colheads.Add("6", "联系电话");
                colheads.Add("7", "安全级别");
                colheads.Add("8", "描述");
                colheads.Add("9", "扩展属性1");
                colheads.Add("10", "扩展属性2");
                colheads.Add("11", "工号");
                colheads.Add("12", "相片路径");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("renyuan", true, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void EditOperation()
        {
            new FrmOperationEdit().ShowDialog(this);
        }

        public void EditViewModel(object sender, EventArgs e)
        {
            DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
            if (tag == null)
            {
                MessageBox.Show("编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                PLViewModel model2 = new PLViewModel();
                try
                {
                    model2.ChangeVMLocker(tag.Oid, ClientData.LogonUser.Oid);
                    tag.Locker = ClientData.LogonUser.Oid;
                    this.tvwNavigator.SelectedNode.Tag = tag;
                    VMFrame viewFrameFrm = this.GetViewFrameFrm();
                    viewFrameFrm.Tag = tag;
                    viewFrameFrm.Show();
                    viewFrameFrm.Activate();
                }
                catch
                {
                    MessageBox.Show("编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void ExportElecSign(object sender, EventArgs e)
        {
            ArrayList userids = FrmUserList2.GetUserids();
            new FrmElecSignImportOrExport(true, userids).ShowDialog();
        }

        private void ExportORG(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "电话");
                colheads.Add("2", "地址");
                colheads.Add("3", "电子邮箱");
                colheads.Add("4", "描述");
                colheads.Add("5", "扩展属性");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("zuzhi", false, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportORGUser(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "人员标识");
                colheads.Add("1", "姓名");
                colheads.Add("2", "性别");
                colheads.Add("3", "出生日期");
                colheads.Add("4", "电子邮件");
                colheads.Add("5", "地址");
                colheads.Add("6", "联系电话");
                colheads.Add("7", "安全级别");
                colheads.Add("8", "描述");
                colheads.Add("9", "扩展属性1");
                colheads.Add("10", "扩展属性2");
                colheads.Add("11", "工号");
                colheads.Add("12", "相片路径");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("renyuan", false, this.tvwNavigator.SelectedNode.Tag);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportRole(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "描述");
                colheads.Add("2", "扩展属性");
                colheads.Add("3", "角色分组");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("juese", false, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportRoleInGroup(object sender, EventArgs e)
        {
            DERoleGroup tag = this.tvwNavigator.SelectedNode.Tag as DERoleGroup;
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "描述");
                colheads.Add("2", "扩展属性");
                colheads.Add("3", "角色分组");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("juese", false, tag);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportRoleUser(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "人员标识");
                colheads.Add("1", "姓名");
                colheads.Add("2", "性别");
                colheads.Add("3", "出生日期");
                colheads.Add("4", "电子邮件");
                colheads.Add("5", "地址");
                colheads.Add("6", "联系电话");
                colheads.Add("7", "安全级别");
                colheads.Add("8", "描述");
                colheads.Add("9", "扩展属性1");
                colheads.Add("10", "扩展属性2");
                colheads.Add("11", "工号");
                colheads.Add("12", "相片路径");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("renyuan", false, this.tvwNavigator.SelectedNode.Tag);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportRoleUserInGroup(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "人员标识");
                colheads.Add("1", "姓名");
                colheads.Add("2", "性别");
                colheads.Add("3", "出生日期");
                colheads.Add("4", "电子邮件");
                colheads.Add("5", "地址");
                colheads.Add("6", "联系电话");
                colheads.Add("7", "安全级别");
                colheads.Add("8", "描述");
                colheads.Add("9", "扩展属性1");
                colheads.Add("10", "扩展属性2");
                colheads.Add("11", "工号");
                colheads.Add("12", "相片路径");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("renyuan", false, this.tvwNavigator.SelectedNode.Tag);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportSubOrg(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "名称");
                colheads.Add("1", "电话");
                colheads.Add("2", "地址");
                colheads.Add("3", "电子邮箱");
                colheads.Add("4", "描述");
                colheads.Add("5", "扩展属性");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("zuzhi", false, this.tvwNavigator.SelectedNode.Tag);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void ExportUser(object sender, EventArgs e)
        {
            string savefilename = "";
            this.saveFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.saveFileDialog1.FileName = "";
            this.saveFileDialog1.CheckPathExists = true;
            this.saveFileDialog1.InitialDirectory = Application.StartupPath;
            this.saveFileDialog1.RestoreDirectory = true;
            Hashtable colheads = new Hashtable();
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilename = this.saveFileDialog1.FileName;
                colheads.Add("0", "人员标识");
                colheads.Add("1", "姓名");
                colheads.Add("2", "性别");
                colheads.Add("3", "出生日期");
                colheads.Add("4", "电子邮件");
                colheads.Add("5", "地址");
                colheads.Add("6", "联系电话");
                colheads.Add("7", "安全级别");
                colheads.Add("8", "描述");
                colheads.Add("9", "扩展属性1");
                colheads.Add("10", "扩展属性2");
                colheads.Add("11", "工号");
                colheads.Add("12", "相片路径");
                try
                {
                    new UCModelBatchProcess(colheads, savefilename).CreateExcelFile("renyuan", false, null);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        public void FileTypeRefresh(object sender, EventArgs e)
        {
            this.GetFileWnd().FileTypeRefresh(sender, e);
        }

        private TreeNode FindClassNode(TreeNode pNode, Guid classOid)
        {
            if ((pNode != null) && (pNode.Nodes.Count > 0))
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    DEMetaClass tag = node.Tag as DEMetaClass;
                    if ((tag != null) && (tag.Oid == classOid))
                    {
                        return node;
                    }
                    TreeNode node2 = this.FindClassNode(node, classOid);
                    if (node2 != null)
                    {
                        return node2;
                    }
                }
            }
            return null;
        }

        private TreeNode FindContextNode(TreeNode pNode, Guid relOid)
        {
            if ((pNode != null) && (pNode.Nodes.Count > 0))
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    DEMetaContext tag = node.Tag as DEMetaContext;
                    if ((tag != null) && (tag.Oid == relOid))
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public TreeNode FindNode(TreeNode TheNode, DELProcessDefProperty proDef)
        {
            for (int i = 0; i < TheNode.Nodes.Count; i++)
            {
                TreeNode node = TheNode.Nodes[i];
                if (!(node.Tag is DELProcessClass) && (proDef.CompareTo(node.Tag as DELProcessDefProperty) == 0))
                {
                    return node;
                }
            }
            return null;
        }

        public int FindPosition(DELProcessClass proCls)
        {
            for (int i = 0; i < Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Count; i++)
            {
                TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes[i];
                if (node.Tag is DELProcessClass)
                {
                    if (proCls.CompareTo(node.Tag as DELProcessClass) <= 0)
                    {
                        return i;
                    }
                }
                else
                {
                    return i;
                }
            }
            return Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Count;
        }

        public int FindPosition(TreeNode TheNode, DELProcessDefProperty proDef)
        {
            for (int i = 0; i < TheNode.Nodes.Count; i++)
            {
                TreeNode node = TheNode.Nodes[i];
                if (!(node.Tag is DELProcessClass) && (proDef.CompareTo(node.Tag as DELProcessDefProperty) <= 0))
                {
                    return i;
                }
            }
            return TheNode.Nodes.Count;
        }

        private TreeNode FindRelationNode(TreeNode pNode, Guid relOid)
        {
            if ((pNode != null) && (pNode.Nodes.Count > 0))
            {
                foreach (TreeNode node in pNode.Nodes)
                {
                    DEMetaRelation tag = node.Tag as DEMetaRelation;
                    if ((tag != null) && (tag.Oid == relOid))
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        private TreeNode FindRightNode(TreeNode root, Guid oid)
        {
            for (int i = 0; i < root.Nodes.Count; i++)
            {
                DEMetaClass tag = null;
                if (root.Nodes[i].Tag != null)
                {
                    tag = (DEMetaClass) root.Nodes[i].Tag;
                    if (tag.Oid.Equals(oid))
                    {
                        return root.Nodes[i];
                    }
                    TreeNode node = this.FindRightNode(root.Nodes[i], oid);
                    if (node != null)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public int FindRoleGroupPosition(DERoleGroup deRoleGroup)
        {
            for (int i = 0; i < Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Count; i++)
            {
                TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes[i];
                if (!(node.Tag is DERoleGroup) || (((DERoleGroup) node.Tag).Name.CompareTo(deRoleGroup.Name) >= 0))
                {
                    return i;
                }
            }
            return Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Count;
        }

        private TreeNode FindTreeNodeAt(int x, int y)
        {
            Point pt = this.tvwNavigator.PointToClient(new Point(x, y));
            for (TreeNode node = this.tvwNavigator.TopNode; node != null; node = node.NextVisibleNode)
            {
                if (node.Bounds.Contains(pt))
                {
                    return node;
                }
            }
            return null;
        }

        public void FinishEditViewModel(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要结束对视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”的编辑？", "结束编辑视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
            {
                DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                if (tag == null)
                {
                    MessageBox.Show("结束编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”的操作失败！", "结束编辑视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    try
                    {
                        PLViewModel model4;
                        VMFrame frame = null;
                        IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            if (enumerator.Key is DEViewModel)
                            {
                                DEViewModel key = (DEViewModel) enumerator.Key;
                                DEViewModel model3 = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                                if (key.Oid.Equals(model3.Oid))
                                {
                                    frame = (VMFrame) enumerator.Value;
                                    frame.Show();
                                    frame.Activate();
                                }
                            }
                        }
                        if (frame != null)
                        {
                            DialogResult result = MessageBox.Show("是否保存视图模型？", "保存视图模型", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                try
                                {
                                    frame.viewPanel.saveFile();
                                    goto Label_0159;
                                }
                                catch
                                {
                                    MessageBox.Show("保存视图模型失败！", "保存视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                    return;
                                }
                            }
                            switch (result)
                            {
                                case DialogResult.No:
                                    frame.RefreshViewModel();
                                    break;

                                case DialogResult.Cancel:
                                    return;
                            }
                        }
                    Label_0159:
                        model4 = new PLViewModel();
                        model4.ChangeVMLocker(tag.Oid, Guid.Empty);
                        tag.Locker = Guid.Empty;
                        this.tvwNavigator.SelectedNode.Tag = tag;
                        if (frame != null)
                        {
                            frame.Tag = tag;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("结束编辑视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”的操作失败！", "结束编辑视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void FreshTemplates(object sender, EventArgs e)
        {
            if (this.HashMDiWindows.ContainsKey(this.tvwNavigator.SelectedNode))
            {
                Form form = (Form) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
                if (form is FrmBrowse)
                {
                    this.HashMDiWindows.Remove(this.tvwNavigator.SelectedNode);
                    this.SearchTemplates(null, null);
                    form.Close();
                }
            }
        }

        private void FrmMain_Closed(object sender, EventArgs e)
        {
        }

        private void FrmMain_Closing(object sender, CancelEventArgs e)
        {
            if (PLDataModel.NeedRebuildCusViews || ModelContext.ModelChanged)
            {
                this.OnUpdateAllViews(sender, null);
            }
            e.Cancel = MessageBox.Show("您确定要退出系统吗？", "企业建模工具", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.tvwNavigator.Nodes[0].Expand();
            this.tvwNavigator.SelectedNode = this.tvwNavigator.Nodes[0];
            this.ShowProcessPropertyList();
            this.ShowRoleGroups();
            AddinDeployment.Instance.SyncAddinsWithServer();
            this.NavigatorUpdateHandler = (RefreshNavigatorHandler) Delegate.Combine(this.NavigatorUpdateHandler, new RefreshNavigatorHandler(this.UpdateNavigator));
            ClientData.Instance.D_NavigatorUpdate = this.NavigatorUpdateHandler;
        }

        private void FrmMain_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.HashMDiWindows.GetKeyByValue(base.ActiveMdiChild) is TreeNode)
            {
                TreeNode keyByValue = (TreeNode) this.HashMDiWindows.GetKeyByValue(base.ActiveMdiChild);
                if ((keyByValue != null) && (keyByValue != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel))
                {
                    this.tvwNavigator.SelectedNode = keyByValue;
                }
            }
            else if (this.HashMDiWindows.GetKeyByValue(base.ActiveMdiChild) is DEViewModel)
            {
                DEViewModel model = (DEViewModel) this.HashMDiWindows.GetKeyByValue(base.ActiveMdiChild);
                for (int i = 0; i < Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork.Nodes.Count; i++)
                {
                    TreeNode node2 = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork.Nodes[i];
                    if (node2.Tag.Equals(model))
                    {
                        this.tvwNavigator.SelectedNode = node2;
                        return;
                    }
                }
            }
            if ((base.ActiveMdiChild != null) && (base.MdiChildren.Length > 0))
            {
                this.nextItem.Enabled = base.ActiveMdiChild != base.MdiChildren[base.MdiChildren.Length - 1];
                this.prevItem.Enabled = base.ActiveMdiChild != base.MdiChildren[0];
            }
        }

        private FrmAddin GetAddinWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmAddin)
                {
                    return (FrmAddin) enumerator.Value;
                }
            }
            FrmAddin addin = new FrmAddin(this) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode.Tag, addin);
            return addin;
        }

        private FrmAdminRoleUser GetAdminRoleWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmAdminRoleUser)
                {
                    return (FrmAdminRoleUser) enumerator.Value;
                }
            }
            FrmAdminRoleUser user = new FrmAdminRoleUser(this.tvwNavigator) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0], user);
            return user;
        }

        public static ArrayList GetAllAdminRoleOrgs(Guid adminRoleId)
        {
            PLAdminRole role = new PLAdminRole();
            ArrayList members = null;
            ArrayList list2 = new ArrayList();
            try
            {
                members = role.GetMembers(adminRoleId, "Org");
            }
            catch (ResponsibilityException exception)
            {
                MessageBox.Show(exception.Message, "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return null;
            }
            if (members != null)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    DEOrganization organization = (DEOrganization) members[i];
                    list2.Add(organization);
                }
            }
            return list2;
        }

        public bool GetAllowCreateProcessManagement(){
         return   this.allowCreateProcessManagement;
        }

        private FrmBrowser GetBrowserWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmBrowser)
                {
                    return (FrmBrowser) enumerator.Value;
                }
            }
            FrmBrowser browser = new FrmBrowser(this) {
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Browser, browser);
            return browser;
        }

        private FrmCodeManagement GetCodeManageWnd()
        {
            FrmCodeManagement management = null;
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmCodeManagement)
                {
                    management = (FrmCodeManagement) enumerator.Value;
                    if (!management.IsDisposed)
                    {
                        return management;
                    }
                    this.HashMDiWindows.Remove(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Ecms);
                }
            }
            management = new FrmCodeManagement {
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed),
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Ecms, management);
            return management;
        }

        public FrmDataModel2 GetDataModelWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel)
                {
                    Form form = (Form) enumerator.Value;
                    form.Activate();
                    form.Show();
                    return (FrmDataModel2) enumerator.Value;
                }
            }
            FrmDataModel2 model = new FrmDataModel2 {
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed),
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel, model);
            model.Show();
            return model;
        }

        private FrmFile GetFileWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmFile)
                {
                    return (FrmFile) enumerator.Value;
                }
            }
            FrmFile file = new FrmFile(this) {
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_FileType, file);
            return file;
        }

        private FrmFolder GetFolderWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmFolder)
                {
                    return (FrmFolder) enumerator.Value;
                }
            }
            FrmFolder folder = new FrmFolder(this) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Folder, folder);
            return folder;
        }

        private FrmFormula GetFormulaWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmFormula)
                {
                    return (FrmFormula) enumerator.Value;
                }
            }
            FrmFormula formula = new FrmFormula {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode.Tag, formula);
            return formula;
        }

        protected Bitmap GetIcon(string iconName)
        {
            object obj2 = rmMenus.GetObject(iconName);
            if (obj2 is Icon)
            {
                return ((Icon) obj2).ToBitmap();
            }
            return (Bitmap) obj2;
        }

        protected Thyt.TiPLM.CLT.TiModeler.FrmMain GetMethodInfo(out MethodInfo mi, string methodInfoName)
        {
            mi = base.GetType().GetMethod(methodInfoName);
            return this;
        }

        public TreeNode getopenedDELProcessClass(){
         return   this.openedDELProcessClass;
        }

        private VMFrame GetOpenedViewFrameFrm()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key is DEViewModel)
                {
                    DEViewModel key = (DEViewModel) enumerator.Key;
                    DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                    if (key.Oid.Equals(tag.Oid))
                    {
                        return (VMFrame) enumerator.Value;
                    }
                }
            }
            return null;
        }

        private FrmOrgUser GetOrgWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmOrgUser)
                {
                    return (FrmOrgUser) enumerator.Value;
                }
            }
            FrmOrgUser user = new FrmOrgUser(this.tvwNavigator) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization, user);
            return user;
        }

        private TreeNode GetParentAdminRoleNode(TreeNode rootNode, Guid childOid)
        {
            if (rootNode != null)
            {
                if (((DEAdminRole) rootNode.Tag).Oid.Equals(childOid))
                {
                    return rootNode;
                }
                for (int i = 0; i < rootNode.Nodes.Count; i++)
                {
                    TreeNode node = rootNode.Nodes[i];
                    if (((DEAdminRole) node.Tag).Oid.Equals(childOid))
                    {
                        return node;
                    }
                    for (int j = 0; j < node.Nodes.Count; j++)
                    {
                        TreeNode parentNode = this.GetParentNode(node.Nodes[j], childOid);
                        if (parentNode != null)
                        {
                            return parentNode;
                        }
                    }
                }
            }
            return null;
        }

        private TreeNode GetParentNode(TreeNode rootNode, Guid childOid)
        {
            if (rootNode != null)
            {
                if (((DEOrganization) rootNode.Tag).Oid.Equals(childOid))
                {
                    return rootNode;
                }
                for (int i = 0; i < rootNode.Nodes.Count; i++)
                {
                    TreeNode node = rootNode.Nodes[i];
                    if (((DEOrganization) node.Tag).Oid.Equals(childOid))
                    {
                        return node;
                    }
                    for (int j = 0; j < node.Nodes.Count; j++)
                    {
                        TreeNode parentNode = this.GetParentNode(node.Nodes[j], childOid);
                        if (parentNode != null)
                        {
                            return parentNode;
                        }
                    }
                }
            }
            return null;
        }

        private CLCopyData GetPasteDataList()
        {
            FrmRoleList roleWnd = this.GetRoleWnd();
            if ((roleWnd.cuttedDataList != null) && (roleWnd.cuttedDataList.Count > 0))
            {
                return roleWnd.cuttedDataList;
            }
            return null;
        }

        private FrmTemplateManager GetPPSignWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmTemplateManager)
                {
                    return (FrmTemplateManager) enumerator.Value;
                }
            }
            FrmTemplateManager manager = new FrmTemplateManager(false) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode.Tag, manager);
            return manager;
        }

        public TreeNode GetProcessNode(Guid ProcessID)
        {
            string str = ProcessID.ToString();
            for (int i = 0; i < Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Count; i++)
            {
                if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes[i].Tag.ToString().Equals(str))
                {
                    return Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes[i];
                }
            }
            return null;
        }

        private FrmRoleList GetRoleWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmRoleList)
                {
                    return (FrmRoleList) enumerator.Value;
                }
            }
            FrmRoleList list = new FrmRoleList(this.tvwNavigator) {
                MdiParent = this,
                FormClosed = new PLM_FormClosed(this.OnMdiFormClosed)
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role, list);
            return list;
        }

        private ArrayList GetRuleName(ConstUtility.RuleDefType ruleType)
        {
            ArrayList list = new ArrayList();
            FrmAddRuleName name = new FrmAddRuleName(ConstUtility.RuleDefType.TwoDimCADRule);
            if (name.ShowDialog() == DialogResult.OK)
            {
                list.Add(name.RuleName);
                list.Add(name.RuleDescription);
                list.Add(name.FileName);
            }
            return list;
        }

        public TreeNode getTheCuttingNode(){
         return   this.TheCuttingNode;
        }

        public TreeNode getTheDragingNode() {
         return   this.TheDragingNode;
        }

        protected Bitmap GetToolBarItemImage(string bmName)
        {
            object obj2 = rmToolBarItems.GetObject(bmName);
            if (obj2 is Icon)
            {
                return ((Icon) obj2).ToBitmap();
            }
            return (Bitmap) obj2;
        }

        private FrmTool GetToolWnd()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmTool)
                {
                    return (FrmTool) enumerator.Value;
                }
            }
            FrmTool tool = new FrmTool(this) {
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Tool, tool);
            return tool;
        }

        private VMFrame GetViewFrameFrm()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key is DEViewModel)
                {
                    DEViewModel key = (DEViewModel) enumerator.Key;
                    DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                    if (key.Oid.Equals(tag.Oid))
                    {
                        return (VMFrame) enumerator.Value;
                    }
                }
            }
            VMFrame frame = new VMFrame((DEViewModel) this.tvwNavigator.SelectedNode.Tag) {
                MdiParent = this
            };
            this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode.Tag, frame);
            return frame;
        }

        private FrmViewList GetViewList()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmViewList)
                {
                    return (FrmViewList) enumerator.Value;
                }
            }
            FrmViewList list = new FrmViewList(this) {
                MdiParent = this
            };
            this.HashMDiWindows.Add("ViewList", list);
            return list;
        }

        private FrmViewModel GetViewModelList()
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmViewModel)
                {
                    return (FrmViewModel) enumerator.Value;
                }
            }
            FrmViewModel model = new FrmViewModel(this) {
                MdiParent = this
            };
            this.HashMDiWindows.Add(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork, model);
            return model;
        }

        private bool IfProcessWindowActived()
        {
            object activeMdiChild = base.ActiveMdiChild;
            if (!activeMdiChild.GetType().Name.ToString().Equals("WFTEditor"))
            {
                return false;
            }
            TreeNode keyByValue = (TreeNode) this.HashMDiWindows.GetKeyByValue(activeMdiChild);
            if (keyByValue != null)
            {
                this.tvwNavigator.SelectedNode = keyByValue;
            }
            return true;
        }

        private void ImportElecSign(object sender, EventArgs e)
        {
            ArrayList userids = FrmUserList2.GetUserids();
            new FrmElecSignImportOrExport(false, userids).ShowDialog();
        }

        private void ImportORG(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportOrg(fileName, null);
            }
            else
            {
                fileName = "";
            }
        }

        private void ImportORGUser(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportUser(fileName, this.tvwNavigator.SelectedNode.Tag);
            }
            else
            {
                fileName = "";
            }
        }

        private void ImportRole(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportRole(fileName, null);
            }
            else
            {
                fileName = "";
            }
        }

        private void ImportRoleInGroup(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportRole(fileName, this.tvwNavigator.SelectedNode.Tag);
            }
            else
            {
                fileName = "";
            }
        }

        private void ImportSubOrg(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportOrg(fileName, this.tvwNavigator.SelectedNode.Tag);
            }
            else
            {
                fileName = "";
            }
        }

        private void ImportUser(object sender, EventArgs e)
        {
            string fileName;
            this.openFileDialog1.InitialDirectory = @"d:\";
            this.openFileDialog1.Filter = "xls files (*.xls)|*.xls";
            this.openFileDialog1.FilterIndex = 1;
            this.openFileDialog1.RestoreDirectory = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = this.openFileDialog1.FileName;
                new UCModelBatchProcess().ImportUser(fileName, null);
            }
            else
            {
                fileName = "";
            }
        }

        private void InitializeImageList()
        {
            ClientData.MyImageList.AddIcon("ICO_DELETE");
            ClientData.MyImageList.AddIcon("ICO_REFRESH");
            ClientData.MyImageList.AddIcon("ICO_PROPERTY");
            ClientData.MyImageList.AddIcon("ICO_DMM_DATAMODEL");
            ClientData.MyImageList.AddIcon("ICO_DMM_CLASS");
            ClientData.MyImageList.AddIcon("ICO_DMM_RELATION");
            ClientData.MyImageList.AddIcon("ICO_RSP_ORGMODEL");
            ClientData.MyImageList.AddIcon("ICO_RSP_NAV_PIC");
            ClientData.MyImageList.AddIcon("ICO_RSP_ORG");
            ClientData.MyImageList.AddIcon("ICO_RSP_USER");
            ClientData.MyImageList.AddIcon("ICO_RSP_ROLE");
            ClientData.MyImageList.AddIcon("ICO_RSP_RENAME");
            ClientData.MyImageList.AddIcon("ICO_RSP_ORG_ADD");
            ClientData.MyImageList.AddIcon("ICO_RSP_USER_ADD");
            ClientData.MyImageList.AddIcon("ICO_RSP_ROLE_ADD");
            if (ConstCommon.FUNCTION_MUILTIVIEW_MANAGEMENT)
            {
                ClientData.MyImageList.AddIcon("ICO_VIW_VIEWMODEL");
                ClientData.MyImageList.AddIcon("ICO_VIW_SAVE");
                ClientData.MyImageList.AddIcon("ICO_VIW_EXIT");
            }
            ClientData.MyImageList.AddIcon("ICO_FDL_CLOSE");
            ClientData.MyImageList.AddIcon("ICO_ENV_EXTERNAL_RESOURCE");
            ClientData.MyImageList.AddIcon("ICO_ENV_BROWER");
            ClientData.MyImageList.AddIcon("ICO_ENV_FILETYPE");
            ClientData.MyImageList.AddIcon("ICO_ENV_TOOL");
            ClientData.MyImageList.AddIcon("ICO_BPM_DEF");
            ClientData.MyImageList.AddIcon("ICO_BPM_DEFROOT");
            ClientData.MyImageList.AddIcon("ICO_BPM_START");
            ClientData.MyImageList.AddIcon("ICO_BPM_ROUTER");
            ClientData.MyImageList.AddIcon("ICO_BPM_TASK");
            ClientData.MyImageList.AddIcon("ICO_BPM_LINE");
            ClientData.MyImageList.AddIcon("ICO_BPM_END");
            ClientData.MyImageList.AddIcon("ICO_DELETE");
            ClientData.MyImageList.AddIcon("ICO_BPM_PROPERTY");
            ClientData.MyImageList.AddIcon("ICO_BPM_REFRESH");
            if (ConstCommon.FUNCTION_PPCARD_MODELING)
            {
                ClientData.MyImageList.AddIcon("ICO_PPM_TMPPACKAGE");
                ClientData.MyImageList.AddIcon("ICO_PPM_PPCARDTEMPLATE");
                ClientData.MyImageList.AddIcon("ICO_PPM_CRDTMPPROCED");
                ClientData.MyImageList.AddIcon("ICO_PPM_CRDTMPPROCESS");
                ClientData.MyImageList.AddIcon("ICO_PPM_CRDTMPROUTE");
                ClientData.MyImageList.AddIcon("ICO_PPM_CRDTMPSTOCKRATION");
                ClientData.MyImageList.AddIcon("ICO_PPM_CRDDEFAULT");
            }
        }

        protected void InitializeToolBars()
        {
            this.standardToolBar = new ToolBarEx();
            ToolBarItem item = new ToolBarItem(this.GetToolBarItemImage("Navigation"), new EventHandler(this.OnNavigatorShow), Keys.Control | Keys.H, "导航栏") {
                Tag = "Navigation",
                Style = ToolBarItemStyle.PushButton
            };
            ToolBarItem item2 = new ToolBarItem {
                Style = ToolBarItemStyle.Separator
            };
            this.standardToolBar.Items.Add(item);
            this.standardToolBar.Items.Add(item2);
            if (!ConstCommon.FUNCTION_IORS)
            {
                ToolBarItem item3 = new ToolBarItem(this.GetToolBarItemImage("NewProject"), new EventHandler(this.OnRetriveFromDB), Keys.None, "刷新数据模型");
                this.standardToolBar.Items.Add(item3);
                this.standardToolBar.Items.Add(item2);
            }
            ToolBarItem item4 = new ToolBarItem(this.GetToolBarItemImage("AllOrg"), new EventHandler(this.OnAllOrg), Keys.None, "所有组织");
            this.standardToolBar.Items.Add(item4);
            ToolBarItem item5 = new ToolBarItem(this.GetToolBarItemImage("AllUser"), new EventHandler(this.OnAllUser), Keys.None, "所有用户");
            this.standardToolBar.Items.Add(item5);
            ToolBarItem item6 = new ToolBarItem(this.GetToolBarItemImage("AllRole"), new EventHandler(this.OnAllRole), Keys.None, "所有角色");
            this.standardToolBar.Items.Add(item6);
            this.standardToolBar.Items.Add(item2);
            if (!ConstCommon.FUNCTION_IORS)
            {
                ToolBarItem item7 = new ToolBarItem(this.GetToolBarItemImage("OpenView"), new EventHandler(this.OnOpenViewNetwork), Keys.None, "显示视图模型列表");
                this.standardToolBar.Items.Add(item7);
                this.standardToolBar.Items.Add(item2);
            }
            this.prevItem = new ToolBarItem(PLMImageList.GetIcon("ICO_PREVIOUS").ToBitmap(), new EventHandler(this.OnPreviousWindow), Keys.None, "上一个窗口");
            this.standardToolBar.Items.Add(this.prevItem);
            this.nextItem = new ToolBarItem(PLMImageList.GetIcon("ICO_NEXT").ToBitmap(), new EventHandler(this.OnNextWindow), Keys.None, "下一个窗口");
            this.standardToolBar.Items.Add(this.nextItem);
            TiModelerUIContainer.reBar = new ReBar();
            TiModelerUIContainer.reBar.Bands.Add(this.standardToolBar);
            base.Controls.Add(TiModelerUIContainer.reBar);
            TiModelerUIContainer.reBar.Dock = DockStyle.Top;
        }

        private void Instance_ServerConnectionErrorForControl(object sender, ErrorEventArgs e)
        {
            if (e.GetException() is ServiceAuthorityException)
            {
                MessageBox.Show("服务器拒绝服务。程序将自动关闭。", "系统消息", MessageBoxButtons.OK);
                base.Close();
            }
        }

        private static bool IsCommonCtrl6()
        {
            Thyt.TiPLM.CLT.UIL.DeskLib.Win32.DLLVERSIONINFO dvi = new Thyt.TiPLM.CLT.UIL.DeskLib.Win32.DLLVERSIONINFO {
                cbSize = Marshal.SizeOf(typeof(Thyt.TiPLM.CLT.UIL.DeskLib.Win32.DLLVERSIONINFO))
            };
            WindowsAPI.GetCommonControlDLLVersion(ref dvi);
            return (dvi.dwMajorVersion >= 6);
        }

        private TreeNode JudgeSelectesAdminRoleNode(Guid Oid)
        {
            PLAdminRole role = new PLAdminRole();
            DEAdminRole parentAdminRole = new DEAdminRole();
            try
            {
                parentAdminRole = role.GetParentAdminRole(Oid);
            }
            catch (Exception)
            {
                MessageBox.Show("获取指定管理角色的父管理角色失败！");
            }
            TreeNode parentAdminRoleNode = null;
            TreeNode rootNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0];
            TreeNode node3 = null;
            if (parentAdminRole.Oid == Guid.Empty)
            {
                this.tvwNavigator.SelectedNode = rootNode;
                return node3;
            }
            if (parentAdminRole.Oid.Equals(((DEAdminRole) rootNode.Tag).Oid))
            {
                parentAdminRoleNode = rootNode;
            }
            else
            {
                parentAdminRoleNode = this.GetParentAdminRoleNode(rootNode, parentAdminRole.Oid);
            }
            for (int i = 0; i < parentAdminRoleNode.Nodes.Count; i++)
            {
                if (((DEAdminRole) parentAdminRoleNode.Nodes[i].Tag).Oid.Equals(Oid))
                {
                    return parentAdminRoleNode.Nodes[i];
                }
            }
            return node3;
        }

        private TreeNode JudgeSelectesNode(Guid Oid)
        {
            PLOrganization organization = new PLOrganization();
            DEOrganization parentOrg = new DEOrganization();
            try
            {
                parentOrg = organization.GetParentOrg(Oid);
            }
            catch
            {
                MessageBox.Show("获取指定组织的父组织失败！");
            }
            TreeNode parentNode = null;
            TreeNode rootNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes[0];
            TreeNode node3 = null;
            if (parentOrg == null)
            {
                this.tvwNavigator.SelectedNode = rootNode;
                return node3;
            }
            if (parentOrg.Oid.Equals(((DEOrganization) rootNode.Tag).Oid))
            {
                parentNode = rootNode;
            }
            else
            {
                parentNode = this.GetParentNode(rootNode, parentOrg.Oid);
            }
            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                if (((DEOrganization) parentNode.Nodes[i].Tag).Oid.Equals(Oid))
                {
                    return parentNode.Nodes[i];
                }
            }
            return node3;
        }

        private void ModifyExtModel(object sender, EventArgs e)
        {
            DEExtendedModel tag = this.tvwNavigator.SelectedNode.Tag as DEExtendedModel;
            if (tag != null)
            {
                FrmExtModel.ModifyModel(tag);
            }
        }

        public void NewAddin(object sender, EventArgs e)
        {
            new FrmOpenFile().ShowDialog();
        }

        public void NewAdminRole(object sender, EventArgs e)
        {
            bool isRoot = false;
            if ((this.tvwNavigator.SelectedNode.Tag is string) && (this.tvwNavigator.SelectedNode.Tag.ToString() == "AdminRole"))
            {
                isRoot = true;
            }
            FrmAdminRoleNew new2 = new FrmAdminRoleNew(isRoot);
            if (this.tvwNavigator.SelectedNode.Tag is DEAdminRole)
            {
                new2.deParentAdminRole = (DEAdminRole) this.tvwNavigator.SelectedNode.Tag;
            }
            if (new2.ShowDialog() == DialogResult.OK)
            {
                DEAdminRole deNewAdminRole = new2.deNewAdminRole;
                if (deNewAdminRole != null)
                {
                    int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                    if (isRoot)
                    {
                        this.tvwNavigator.SelectedNode.Text = deNewAdminRole.Name;
                        this.tvwNavigator.SelectedNode.Tag = deNewAdminRole;
                    }
                    else
                    {
                        TreeNode node = new TreeNode(deNewAdminRole.Name, iconIndex, iconIndex) {
                            Tag = deNewAdminRole
                        };
                        this.tvwNavigator.SelectedNode.Nodes.Add(node);
                    }
                    if (!isRoot)
                    {
                        ListViewItem item = new ListViewItem(deNewAdminRole.Name, iconIndex) {
                            Tag = deNewAdminRole
                        };
                        switch (deNewAdminRole.Status)
                        {
                            case 'D':
                                item.SubItems.Add("删除");
                                break;

                            case 'F':
                                item.SubItems.Add("冻结");
                                break;

                            case 'A':
                                item.SubItems.Add("可用");
                                break;
                        }
                        item.SubItems.Add(deNewAdminRole.Creator);
                        item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        item.SubItems.Add(deNewAdminRole.Description);
                        this.GetAdminRoleWnd().lvwSubadminRole.Items.Add(item);
                    }
                }
            }
        }

        public void NewBrowser(object sender, EventArgs e)
        {
            FrmSetBrowser browser = new FrmSetBrowser();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                this.RefreshBrowser();
                this.GetFileWnd().lvwFile.Items.Clear();
                this.GetFileWnd().CreateList();
            }
        }

        public void NewEditor(object sender, EventArgs e)
        {
            FrmSetBrowser browser = new FrmSetBrowser(false);
            if (browser.ShowDialog() == DialogResult.OK)
            {
                this.RefreshBrowser();
                this.GetFileWnd().lvwFile.Items.Clear();
                this.GetFileWnd().CreateList();
            }
        }

        public void NewFileType(object sender, EventArgs e)
        {
            this.GetFileWnd().NewFileType(sender, e);
        }

        public void NewOrg(object sender, EventArgs e)
        {
            bool isRoot = false;
            if ((this.tvwNavigator.SelectedNode.Tag is string) && (this.tvwNavigator.SelectedNode.Tag.ToString() == "Organization"))
            {
                isRoot = true;
            }
            FrmOrgNew new2 = new FrmOrgNew(isRoot);
            if (this.tvwNavigator.SelectedNode.Tag is DEOrganization)
            {
                new2.deParentOrg = (DEOrganization) this.tvwNavigator.SelectedNode.Tag;
            }
            if (new2.ShowDialog() == DialogResult.OK)
            {
                DEOrganization deNewOrg = new2.deNewOrg;
                if (deNewOrg != null)
                {
                    int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
                    TreeNode node = new TreeNode(deNewOrg.Name, iconIndex, iconIndex) {
                        Tag = deNewOrg
                    };
                    this.tvwNavigator.SelectedNode.Nodes.Add(node);
                    ListViewItem item = new ListViewItem(deNewOrg.Name, iconIndex) {
                        Tag = deNewOrg,
                        SubItems = { deNewOrg.Phone }
                    };
                    switch (deNewOrg.Status)
                    {
                        case 'D':
                            item.SubItems.Add("删除");
                            break;

                        case 'F':
                            item.SubItems.Add("冻结");
                            break;

                        case 'A':
                            item.SubItems.Add("可用");
                            break;
                    }
                    item.SubItems.Add(deNewOrg.Creator);
                    item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(deNewOrg.Description);
                    this.GetOrgWnd().lvwSuborg.Items.Add(item);
                    new2.Dispose();
                }
            }
        }

        private void NewPrintTemplate(object sender, EventArgs e)
        {
            if (this.tvwNavigator.SelectedNode.Tag.GetType().Equals(typeof(DEMetaClass)))
            {
                DlgNewGuid guid = null;
                try
                {
                    guid = new DlgNewGuid(((DEMetaClass) this.tvwNavigator.SelectedNode.Tag).Name, ClientData.LogonUser);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "打印模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (guid.ShowDialog() == DialogResult.OK)
                {
                    this.FreshTemplates(null, null);
                    Thyt.TiPLM.UIL.PPM.PPCardModeling.FrmModeling modeling = null;
                    try
                    {
                        modeling = new Thyt.TiPLM.UIL.PPM.PPCardModeling.FrmModeling(guid.TP);
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show(exception2.Message, "打印模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    modeling.MdiParent = this;
                    modeling.Show();
                }
            }
        }

        public void NewRole(object sender, EventArgs e)
        {
            FrmRoleNew new2 = new FrmRoleNew();
            if (new2.ShowDialog() == DialogResult.OK)
            {
                int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                ListViewItem item = new ListViewItem(new2.deRole.Name, iconIndex) {
                    Tag = new2.deRole
                };
                string str = "可用";
                string str2 = "通用角色";
                switch (new2.deRole.RoleType)
                {
                    case "P":
                        str2 = "项目角色";
                        break;

                    case "C":
                        str2 = "通用角色";
                        break;

                    default:
                        str2 = "通用角色";
                        break;
                }
                item.SubItems.AddRange(new string[] { str, str2, new2.deRole.Creator, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), new2.deRole.Description });
                this.GetRoleWnd().lvwRole.Items.Add(item);
            }
            new2.Dispose();
        }

        private void NewTemplate(object sender, EventArgs e)
        {
            if (this.tvwNavigator.SelectedNode.Tag.GetType().Equals(typeof(DEMetaClass)) || (this.tvwNavigator.SelectedNode.Tag.ToString() == "OUTPUTTEMPLATE"))
            {
                DlgNewGuid guid = null;
                try
                {
                    if (this.tvwNavigator.SelectedNode.Tag.ToString() == "OUTPUTTEMPLATE")
                    {
                        guid = new DlgNewGuid(this.tvwNavigator.SelectedNode.Tag.ToString(), ClientData.LogonUser);
                    }
                    else
                    {
                        guid = new DlgNewGuid(((DEMetaClass) this.tvwNavigator.SelectedNode.Tag).Name, ClientData.LogonUser);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "工艺信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (guid.ShowDialog() == DialogResult.OK)
                {
                    this.FreshTemplates(null, null);
                    Thyt.TiPLM.UIL.PPM.PPCardModeling.FrmModeling modeling = null;
                    try
                    {
                        modeling = new Thyt.TiPLM.UIL.PPM.PPCardModeling.FrmModeling(guid.TP);
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show(exception2.Message, "PPC - FrmModeling", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    modeling.MdiParent = this;
                    modeling.Show();
                }
            }
        }

        public void NewTool(object sender, EventArgs e)
        {
            FrmSetTool tool = new FrmSetTool();
            if (tool.ShowDialog() == DialogResult.OK)
            {
                string[] items = new string[] { tool.deTool.Name, tool.deTool.Creator, tool.deTool.CreateTime.ToString("yyyy-MM-dd"), tool.deTool.Description };
                ListViewItem item = new ListViewItem(items) {
                    Tag = tool.deTool,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_TOOL")
                };
                this.GetToolWnd().lvwTool.Items.Add(item);
            }
        }

        public void NewUser(object sender, EventArgs e)
        {
            FrmNewPerson person = new FrmNewPerson();
            if (person.ShowDialog() == DialogResult.OK)
            {
                FrmUserList2.ShowFrm(person.deUser.LogId);
            }
            person.Dispose();
        }

        public void NewViewModel(object sender, EventArgs e)
        {
            FrmViewModelProperty property = new FrmViewModelProperty();
            if (property.ShowDialog() == DialogResult.OK)
            {
                int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEWMODEL");
                TreeNode node = new TreeNode(property.theVM.Name, iconIndex, iconIndex) {
                    Tag = property.theVM
                };
                this.tvwNavigator.SelectedNode.Nodes.Add(node);
                this.tvwNavigator.SelectedNode = node;
                VMFrame frame = new VMFrame(property.theVM, property.selectedView) {
                    MdiParent = this
                };
                this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode.Tag, frame);
                frame.Show();
            }
            property.Dispose();
        }

        public void OnAbout(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout(PLMProductName.TiModeler);
            about.ShowDialog();
            about.Dispose();
        }

        public void OnAddinManagement(object sender, EventArgs e)
        {
        }

        public void OnAllAdminRole(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有管理角色权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes.Count == 0)
                {
                    this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole;
                }
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0];
                this.tvwNavigator.SelectedNode.Expand();
            }
        }

        public void OnAllOrg(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有组织管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Count == 0)
            {
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization;
            }
            else
            {
                this.tvwNavigator.SelectedNode = null;
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes[0];
                this.tvwNavigator.SelectedNode.Expand();
            }
        }

        public void OnAllRole(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有组织管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.tvwNavigator.SelectedNode = null;
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role;
            }
        }

        public void OnAllUser(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有组织管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.tvwNavigator.SelectedNode = null;
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_User;
            }
        }

        public void OnAttrGroupConfiguration(object sender, EventArgs e)
        {
            if (!this.allowConfigDataModel)
            {
                MessageBox.Show("您没有数据模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public void OnAuthorizeTemplate(object sender, EventArgs e)
        {
            DELProcessDefProperty tag = this.tvwNavigator.SelectedNode.Tag as DELProcessDefProperty;
            new FrmFuncObjGrant { 
                objectName = tag.Name,
                className = "BPM_PROCESS_DEFINITION",
                objectOid = tag.ID
            }.ShowDialog();
        }

        private void OnCheckDataModel(object sender, EventArgs e)
        {
            IProgressCallback progressWindow = ClientData.GetProgressWindow();
            ArrayList state = new ArrayList {
                progressWindow
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.CheckDataModel), state);
            progressWindow.ShowWindow();
            if (state.Count == 2)
            {
                ArrayList list2 = state[1] as ArrayList;
                Thyt.TiPLM.UIL.Common.FrmMessage message = new Thyt.TiPLM.UIL.Common.FrmMessage();
                foreach (string str in list2)
                {
                    message.WriteLine(str + "\r\n", Color.Black);
                }
                message.ShowDialog();
            }
            else
            {
                MessageBox.Show("数据模型一致性检查通过，没有检查到数据模型与数据库不一致的情况。", "数据模型管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void OnClassDelete(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvwNavigator.SelectedNode;
            if (((selectedNode != null) && (selectedNode.Tag is DEMetaClass)) && (MessageBox.Show("数据类删除后将不可恢复，您确定要删除选择的数据类吗？", "数据模型定义", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel))
            {
                try
                {
                    PLDataModel.Agent2.DeleteMetaClass((selectedNode.Tag as DEMetaClass).Name, ClientData.LogonUser.Oid);
                    PLDataModel.NeedRebuildCusViews = true;
                    DataModelDelegate.Instance.InvokeMetaClassDeleted(selectedNode.Tag as DEMetaClass);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
            }
        }

        public void OnClose(object sender, EventArgs e)
        {
            base.Close();
        }

        public void OnCloseProForMenu(object sender, EventArgs e)
        {
            if (this.IfProcessWindowActived())
            {
                this.CloseProcessTemplate();
            }
            else
            {
                MessageBox.Show("无活动过程模板被打开");
            }
        }

        public void OnCloseProTem(object sender, EventArgs e)
        {
            this.CloseProTem();
        }

        public void OnCutProTem(object sender, EventArgs e)
        {
            this.TheCuttingNode = this.tvwNavigator.SelectedNode;
            this.isCuttingNodeDeleted = false;
        }

        private void OnDefineDGTemplate(object sender, EventArgs e)
        {
            using (FrmTemplate template = new FrmTemplate())
            {
                template.ShowDialog();
            }
        }

        private void OnDefineDisplaySchema(object sender, EventArgs e)
        {
            using (FrmCommonDisplaySchema schema = new FrmCommonDisplaySchema())
            {
                schema.ShowDialog();
            }
        }

        private void OnDeleteContext(object sender, EventArgs e)
        {
            if ((this.tvwNavigator.SelectedNode != null) && (MessageBox.Show("您确定要删除指定的上下文吗？", "上下文管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                DEMetaContext tag = this.tvwNavigator.SelectedNode.Tag as DEMetaContext;
                if (tag != null)
                {
                    try
                    {
                        PLDataModel.DelContext(ClientData.Session, tag);
                        DataModelDelegate.Instance.InvokeMetaContextDeleted(tag);
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception, "上下文管理");
                    }
                }
            }
        }

        public void OnDeleteTemplateForMenu(object sender, EventArgs e)
        {
            if (this.IfProcessWindowActived())
            {
                this.deleteProcessTemplate();
            }
        }

        private void OnDeleteViewSchema(object sender, EventArgs e)
        {
            if ((this.tvwNavigator.SelectedNode != null) && (MessageBox.Show("您确定要删除指定的显示方案吗？", "显示方案管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                DEItemsViewSchema tag = this.tvwNavigator.SelectedNode.Tag as DEItemsViewSchema;
                if (tag != null)
                {
                    try
                    {
                        PLItemViewSchema.Agent.DeleteItemViewSchema(tag.Oid, ClientData.LogonUser.Oid);
                        if (ClientData.Instance.D_AfterViewSchemaDeleted != null)
                        {
                            ClientData.Instance.D_AfterViewSchemaDeleted(tag.Oid);
                        }
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception, "显示方案管理");
                    }
                }
            }
        }

        private void OnDelNode(object sender, EventArgs e)
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode.Parent];
            if (editor != null)
            {
                editor.deleteObject();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strTempNotOpen"));
            }
        }

        public void OnDelProClass(object sender, EventArgs e)
        {
            if ((this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
            {
                this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
            }
            DELProcessClass tag = this.tvwNavigator.SelectedNode.Tag as DELProcessClass;
            BPMProcessor processor = new BPMProcessor();
            if (processor.DelOneProcessClass(tag.ID))
            {
                for (int i = this.tvwNavigator.SelectedNode.Nodes.Count - 1; i >= 0; i--)
                {
                    TreeNode node = this.tvwNavigator.SelectedNode.Nodes[i];
                    this.tvwNavigator.SelectedNode.Nodes.Remove(node);
                    int index = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, node.Tag as DELProcessDefProperty);
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, node);
                }
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Remove(this.tvwNavigator.SelectedNode);
                this.TheProClsNodeList.Remove(tag.ID);
            }
        }

        private void OnDelProNode(object sender, EventArgs e)
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode.Parent];
            if (editor != null)
            {
                Shape tag = (Shape) this.tvwNavigator.SelectedNode.Tag;
                if (tag == editor.viewPanel.selectedShape)
                {
                    try
                    {
                        editor.viewPanel.deleteObject();
                    }
                    catch (Exception exception)
                    {
                        BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                    }
                }
            }
        }

        public void OnDelProTem(object sender, EventArgs e)
        {
            this.deleteProcessTemplate();
        }

        public void OnDMMenuPopup(object sender, EventArgs e)
        {
            if (!this.allowConfigDataModel)
            {
                MessageBox.Show("您没有数据模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MenuItem item = (MenuItem) sender;
                if (this.tvwNavigator.SelectedNode != null)
                {
                    object tag = this.tvwNavigator.SelectedNode.Tag;
                    if (((tag is DEMetaClass) && (((DEMetaClass) tag).SystemClass == 'N')) && ((DEMetaClass) tag).IsInheritable)
                    {
                        item.MenuItems[0].Enabled = true;
                    }
                    else
                    {
                        item.MenuItems[0].Enabled = false;
                    }
                }
            }
        }

        public void OnEditOperationForMenu(object sender, EventArgs e)
        {
            if (!this.allowConfigBPM)
            {
                MessageBox.Show("您没有配置过程管理系统的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                try
                {
                    new Model();
                    this.EditOperation();
                }
                catch
                {
                    MessageBox.Show("获取业务操作失败");
                }
            }
        }

        public void OnEditRuleMonitorForMenu(object sender, EventArgs e)
        {
            PLUser user = new PLUser();
            bool flag = false;
            try
            {
                flag = user.IsAdministrator(ClientData.LogonUser.Oid);
            }
            catch
            {
                return;
            }
            if (flag)
            {
                FrmEditRuleMonitor monitor = new FrmEditRuleMonitor();
                if (monitor.ShowDialog() == DialogResult.OK)
                {
                    monitor.Close();
                }
                else
                {
                    monitor.Close();
                }
            }
            else
            {
                MessageBox.Show("您没有配置移交规则监控人的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void OnExceptionClass(object sender, EventArgs e)
        {
            try
            {
                new FrmCustomExceptionClassEdit().ShowDialog();
            }
            catch (Exception exception)
            {
                PrintException.Print(exception, "自定义异常信息管理");
            }
        }

        public void OnExportEventLog(object sender, EventArgs e)
        {
            ClientData.ExportEventLog();
        }

        public void OnExportTemplate(object sender, EventArgs e)
        {
            DELProcessDefinition definition;
            BPMAdmin admin = new BPMAdmin();
            DELProcessDefProperty tag = (DELProcessDefProperty) this.tvwNavigator.SelectedNode.Tag;
            PLGrantPerm perm = new PLGrantPerm();
            bool flag = perm.CanDoObjectOperation(ClientData.LogonUser.Oid, tag.ID, "BPM_PROCESS_DEFINITION", "ClaRel_BROWSE", 0, Guid.Empty) == 1;
            WFTEditor editor = this.HashMDiWindows[this.tvwNavigator.SelectedNode] as WFTEditor;
            if ((editor != null) && editor.isNew)
            {
                definition = editor.PrepareExport();
                if (definition == null)
                {
                    return;
                }
            }
            else
            {
                if (!flag)
                {
                    MessageBox.Show("您无权浏览该过程模型！", "业务过程管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                try
                {
                    admin.GetProcessDefinition(BPMClient.UserID, tag.ID, out definition);
                }
                catch (Exception)
                {
                    MessageBox.Show("从数据库中获取模型失败");
                    return;
                }
            }
            new ProcessTemplateExport(definition).export();
        }

        private void OnFormPageSetting(object sender, EventArgs e)
        {
            DEMetaClass tag = (DEMetaClass) this.tvwNavigator.SelectedNode.Tag;
            PLDataModel model = new PLDataModel();
            DEDisplayScenario displayScenario = model.GetDisplayScenario(tag);
            if (displayScenario == null)
            {
                displayScenario = new DEDisplayScenario();
            }
            FrmDisplayScenario scenario2 = new FrmDisplayScenario();
            scenario2.SetPages(displayScenario.GetPages(), tag);
            if (scenario2.ShowDialog() == DialogResult.OK)
            {
                displayScenario.SetPages(scenario2.GetPages());
                model.SaveDisplayScenario(tag, displayScenario);
            }
        }

        public void OnImportTemplate(object sender, EventArgs e)
        {
            if (ConstCommon.FUNCTION_BPM)
            {
                if (!this.allowCreateProcessManagement)
                {
                    MessageBox.Show("您没有创建过程模板的权限！", "业务过程管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    DELProcessDefinition definition;
                    WFTEditor editor = new WFTEditor(this) {
                        MdiParent = this
                    };
                    ProcessTemplateImport import = new ProcessTemplateImport();
                    if (import.import(out definition))
                    {
                        editor.proTemplate = definition;
                        editor.viewPanel.shapeData.template = definition;
                        DELProcessDefProperty proDef = new DELProcessDefProperty(editor.proTemplate);
                        TreeNode node = new TreeNode(proDef.Name) {
                            Tag = proDef,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        if ((this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
                        {
                            this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
                        }
                        TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                        if ((selectedNode.Tag is DELProcessClass) || (selectedNode.Parent.Tag is DELProcessClass))
                        {
                            if (selectedNode.Parent.Tag is DELProcessClass)
                            {
                                selectedNode = selectedNode.Parent;
                            }
                            int index = this.FindPosition(selectedNode, proDef);
                            selectedNode.Nodes.Insert(index, node);
                            this.tvwNavigator.SelectedNode = node;
                            this.HashMDiWindows.Add(node, editor);
                            this.AddOneProcess(node);
                            editor.isNew = true;
                            editor.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor.viewPanel.shapeData.template.Name;
                            editor.Show();
                            editor.viewPanel.shapeData.templateToPicture();
                            this.AddTreeNode(editor.viewPanel.mainWindow, editor.viewPanel.shapeData.root.startNode);
                            this.AddTreeNode(editor.viewPanel.mainWindow, editor.viewPanel.shapeData.root.endNode);
                            foreach (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node3 in editor.viewPanel.shapeData.root.taskNodAry)
                            {
                                this.AddTreeNode(editor.viewPanel.mainWindow, node3);
                            }
                            foreach (RouteNode node4 in editor.viewPanel.shapeData.root.routeNodAry)
                            {
                                this.AddTreeNode(editor.viewPanel.mainWindow, node4);
                            }
                            editor.viewPanel.Refresh();
                            editor.saveToDataBase();
                            DELProcessClass tag = selectedNode.Tag as DELProcessClass;
                            BPMProcessor processor = new BPMProcessor();
                            if (processor.MoveProcessBetweenClass(proDef.ID, Guid.Empty, tag.ID))
                            {
                                tag.AddProcess(proDef.ID);
                            }
                            MessageBox.Show("成功导入业务过程模板！\n\n您可能需要对该模板进行进一步的修改。", "模板导入");
                        }
                        else if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM != null)
                        {
                            int num2 = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, proDef);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(num2, node);
                            this.tvwNavigator.SelectedNode = node;
                            this.HashMDiWindows.Add(node, editor);
                            this.AddOneProcess(node);
                            editor.isNew = true;
                            editor.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor.viewPanel.shapeData.template.Name;
                            editor.Show();
                            editor.viewPanel.shapeData.templateToPicture();
                            this.AddTreeNode(editor.viewPanel.mainWindow, editor.viewPanel.shapeData.root.startNode);
                            this.AddTreeNode(editor.viewPanel.mainWindow, editor.viewPanel.shapeData.root.endNode);
                            foreach (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node5 in editor.viewPanel.shapeData.root.taskNodAry)
                            {
                                this.AddTreeNode(editor.viewPanel.mainWindow, node5);
                            }
                            foreach (RouteNode node6 in editor.viewPanel.shapeData.root.routeNodAry)
                            {
                                this.AddTreeNode(editor.viewPanel.mainWindow, node6);
                            }
                            editor.viewPanel.Refresh();
                            editor.saveToDataBase();
                            MessageBox.Show("成功导入业务过程模板！\n\n您可能需要对该模板进行进一步的修改。", "模板导入");
                        }
                    }
                    else
                    {
                        editor.Dispose();
                    }
                }
            }
        }

        private void OnMdiFormClosed(Form form)
        {
            this.RemoveWnd(form);
        }

        public void OnMenuNewClass(object sender, EventArgs e)
        {
            if (!this.allowConfigDataModel)
            {
                MessageBox.Show("您没有数据模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((this.tvwNavigator.SelectedNode != null) && (this.tvwNavigator.SelectedNode.Tag is DEMetaClass))
            {
                this.OnNewSubClass(sender, e);
            }
        }

        public void OnMenuNewRelation(object sender, EventArgs e)
        {
            if (!this.allowConfigDataModel)
            {
                MessageBox.Show("您没有数据模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation;
                this.OnNewRelation(sender, e);
            }
        }

        private void OnMetaClassCreated(DEMetaClass mc)
        {
            TreeNode node = this.FindClassNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class, mc.Parent);
            if (node != null)
            {
                string label = mc.Label;
                TreeNode node2 = node.Nodes.Add(label);
                node2.ImageIndex = node2.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                node2.Tag = mc;
                this.tvwNavigator.SelectedNode = node2;
            }
        }

        private void OnMetaClassDeleted(DEMetaClass mc)
        {
            TreeNode node = this.FindClassNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class, mc.Oid);
            if (node != null)
            {
                TreeNode parent = node.Parent;
                node.Remove();
                this.tvwNavigator.SelectedNode = parent;
            }
        }

        private void OnMetaClassModified(DEMetaClass mc)
        {
            TreeNode node = this.FindClassNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class, mc.Oid);
            if (node != null)
            {
                string label = mc.Label;
                node.Text = label;
                node.Tag = mc;
            }
        }

        private void OnMetaContextCreated(DEMetaContext mr)
        {
            if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context != null)
            {
                TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context.Nodes.Add(mr.Label);
                node.ImageIndex = node.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                node.Tag = mr;
                this.tvwNavigator.SelectedNode = node;
            }
        }

        private void OnMetaContextDeleted(DEMetaContext mr)
        {
            TreeNode node = this.FindContextNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context, mr.Oid);
            if (node != null)
            {
                TreeNode parent = node.Parent;
                node.Remove();
                this.tvwNavigator.SelectedNode = parent;
            }
        }

        private void OnMetaContextModified(DEMetaContext mr)
        {
            TreeNode node = this.FindContextNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context, mr.Oid);
            if (node != null)
            {
                node.Text = mr.Label;
                node.Tag = mr;
            }
        }

        private void OnMetaRelationCreated(DEMetaRelation mr)
        {
            if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation != null)
            {
                TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation.Nodes.Add(mr.Label);
                node.ImageIndex = node.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_RELATION");
                node.Tag = mr;
                this.tvwNavigator.SelectedNode = node;
            }
        }

        private void OnMetaRelationDeleted(DEMetaRelation mr)
        {
            TreeNode node = this.FindRelationNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation, mr.Oid);
            if (node != null)
            {
                TreeNode parent = node.Parent;
                node.Remove();
                this.tvwNavigator.SelectedNode = parent;
            }
        }

        private void OnMetaRelationModified(DEMetaRelation mr)
        {
            TreeNode node = this.FindRelationNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation, mr.Oid);
            if (node != null)
            {
                node.Text = mr.Label;
                node.Tag = mr;
            }
        }

        public void OnNavigatorShow(object sender, EventArgs e)
        {
            if (!this.isNavigationShow)
            {
                TiModelerUIContainer.dockingManager.ShowAllContents();
                this.standardToolBar.Items[0].ToolTip = "隐藏导航树";
                this.isNavigationShow = true;
            }
            else
            {
                TiModelerUIContainer.dockingManager.HideAllContents();
                this.standardToolBar.Items[0].ToolTip = "显示导航树";
                this.isNavigationShow = false;
            }
        }

        private void OnNewClass(object sender, EventArgs e)
        {
            if (this.tvwNavigator.SelectedNode.Tag is DEMetaClass)
            {
                DEMetaClass tag = this.tvwNavigator.SelectedNode.Tag as DEMetaClass;
                if (tag != null)
                {
                    this.GetDataModelWnd().NewClass(tag);
                }
            }
        }

        private void OnNewContext(object sender, EventArgs e)
        {
            FrmItemDisplayContext.CreateItemContext(ClientData.mainForm, null);
        }

        public void OnNewProClass(object sender, EventArgs e)
        {
            DELProcessClass class2 = new DELProcessClass {
                ID = Guid.NewGuid(),
                Name = "新的分类" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")
            };
            TreeNode node = new TreeNode(class2.Name) {
                Tag = class2,
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE")
            };
            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Add(node);
            this.IsNewProClass = true;
            this.tvwNavigator.SelectedNode = node;
            this.tvwNavigator.LabelEdit = true;
            node.BeginEdit();
        }

        public void OnNewProTem(object sender, EventArgs e)
        {
            this.createNewWFTEditor();
        }

        private void OnNewRelation(object sender, EventArgs e)
        {
            this.GetDataModelWnd().NewRelation();
        }

        private void OnNewRoleGroup(object sender, EventArgs e)
        {
            DERoleGroup deRoleGroup = new DERoleGroup {
                Oid = Guid.NewGuid(),
                Name = "新的分组" + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss")
            };
            PLRole role = new PLRole();
            try
            {
                if (!role.CreateRoleGroup(deRoleGroup))
                {
                    MessageBox.Show("角色分组创建失败！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("角色分组创建失败！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TreeNode node = new TreeNode(deRoleGroup.Name) {
                Tag = deRoleGroup,
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN")
            };
            int index = this.FindRoleGroupPosition(deRoleGroup);
            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Insert(index, node);
            this.tvwNavigator.SelectedNode = node;
            this.tvwNavigator.LabelEdit = true;
            node.BeginEdit();
        }

        public void OnNewSubClass(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvwNavigator.SelectedNode;
            if ((selectedNode != null) && (selectedNode.Tag is DEMetaClass))
            {
                DEMetaClass tag = (DEMetaClass) selectedNode.Tag;
                this.GetDataModelWnd().NewClass(tag);
                Form dataModelWnd = this.GetDataModelWnd();
                if (!dataModelWnd.Visible)
                {
                    dataModelWnd.Show();
                }
            }
        }

        public void OnNewTemplateForMenu(object sender, EventArgs e)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM;
            if (node != null)
            {
                this.tvwNavigator.SelectedNode = node;
                this.createNewWFTEditor();
            }
        }

        private void OnNewViewSchema(object sender, EventArgs e)
        {
            FrmItemDisplaySchema.CreateItemViewSchema(ClientData.mainForm);
        }

        public void OnNextWindow(object sender, EventArgs e)
        {
            for (int i = 0; i < base.MdiChildren.Length; i++)
            {
                if ((base.MdiChildren[i] == base.ActiveMdiChild) && (i < (base.MdiChildren.Length - 1)))
                {
                    base.MdiChildren[i + 1].Activate();
                    return;
                }
            }
            this.nextItem.Enabled = false;
        }

        private void OnNodeProperty(object sender, EventArgs e)
        {
            ((WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode.Parent]).showNodeProperty();
        }

        public void OnOnlineHelp(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\" + ConstCommon.CURRENT_PRODUCTNAME + "S.pdf"))
            {
                try
                {
                    Process.Start(Application.StartupPath + @"\" + ConstCommon.CURRENT_PRODUCTNAME + "S.pdf");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "联机帮助", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void OnOpenContext(object sender, EventArgs e)
        {
            if (this.tvwNavigator.SelectedNode != null)
            {
                DEMetaContext tag = this.tvwNavigator.SelectedNode.Tag as DEMetaContext;
                if (tag != null)
                {
                    FrmItemDisplayContext.OpenItemContext(ClientData.mainForm, tag, null);
                }
            }
        }

        public void OnOpenDataBaseForMenu(object sender, EventArgs e)
        {
            if (!this.allowBrowseProcessManagement)
            {
                MessageBox.Show("您没有打开业务过程模板的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DELProcessDefProperty)
            {
                this.OpenProcessTemplate();
            }
            else
            {
                MessageBox.Show("未指明要打开的模板");
            }
        }

        public void OnOpenEcms(object sender, EventArgs e)
        {
            if (!this.allowEcmsDataManagement)
            {
                MessageBox.Show("您没有配置码值管理的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                FrmCodeManagement codeManageWnd = this.GetCodeManageWnd();
                codeManageWnd.Show();
                codeManageWnd.Activate();
                Cursor.Current = Cursors.Default;
            }
        }

        public void OnOpenLocalForMenu(object sender, EventArgs e)
        {
            if (!this.allowBrowseProcessManagement)
            {
                MessageBox.Show("您没有打开业务过程模板的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM;
                if (node != null)
                {
                    this.tvwNavigator.SelectedNode = node;
                    this.OpenLocalFile();
                }
            }
        }

        public void OnOpenMetaPerm(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel || (this.deCurrentAdminRole.ParentAdminRole != Guid.Empty))
            {
                MessageBox.Show("您没有自主授权规则定义的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                new FrmMetaPermManager { showAll = false }.ShowDialog();
            }
        }

        public void OnOpenMetaPerm2(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel || (this.deCurrentAdminRole.ParentAdminRole != Guid.Empty))
            {
                MessageBox.Show("您没有权限模型和策略定义的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                new FrmMetaPermPass().ShowDialog();
            }
        }

        public void OnOpenOprMgn(object sender, EventArgs e)
        {
            new FrmOperationManager().ShowDialog();
        }

        public void OnOpenProTem(object sender, EventArgs e)
        {
            this.OpenProcessTemplate();
        }

        public void OnOpenRoles(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有批量分配角色的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                PLAdminRole role = new PLAdminRole();
                Guid adminRole = role.GetAdminRoleByUserId(ClientData.LogonUser.Oid).AdminRole;
                new FrmMultiRolePermAssign(role.GetAdminRole(adminRole)).ShowDialog();
            }
        }

        public void OnOpenUsers(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有批量分配用户的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                PLAdminRole role = new PLAdminRole();
                Guid adminRole = role.GetAdminRoleByUserId(ClientData.LogonUser.Oid).AdminRole;
                new FrmMultiUserPermAssign(role.GetAdminRole(adminRole)).ShowDialog();
            }
        }

        public void OnOpenViewNetwork(object sender, EventArgs e)
        {
            if (ConstCommon.FUNCTION_MUILTIVIEW_MANAGEMENT)
            {
                if (!this.allowViewManagement)
                {
                    MessageBox.Show("您没有视图模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    if (this.tvwNavigator.SelectedNode == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork)
                    {
                        this.tvwNavigator.SelectedNode = null;
                    }
                    this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork;
                }
            }
        }

        private void OnOpenViewSchema(object sender, EventArgs e)
        {
            if (this.tvwNavigator.SelectedNode != null)
            {
                DEItemsViewSchema tag = this.tvwNavigator.SelectedNode.Tag as DEItemsViewSchema;
                if (tag != null)
                {
                    FrmItemDisplaySchema.OpenItemViewSchema(ClientData.mainForm, tag);
                }
            }
        }

        public void OnOperationModelForMenu(object sender, EventArgs e)
        {
            if (!this.allowConfigBPM)
            {
                MessageBox.Show("您没有配置过程管理系统的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                new FrmDoOperationByBPM().ShowDialog();
            }
        }

        public void OnPasteProTem(object sender, EventArgs e)
        {
            if (this.TheCuttingNode != null)
            {
                if ((this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
                {
                    this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
                }
                TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                if (this.TheCuttingNode.Parent == selectedNode)
                {
                    this.TheCuttingNode = null;
                }
                else
                {
                    BPMProcessor processor = new BPMProcessor();
                    TreeNode parent = this.TheCuttingNode.Parent;
                    DELProcessDefProperty tag = this.TheCuttingNode.Tag as DELProcessDefProperty;
                    if (selectedNode == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass class2 = parent.Tag as DELProcessClass;
                        if (processor.MoveProcessBetweenClass(tag.ID, class2.ID, Guid.Empty))
                        {
                            class2.RemoveProcess(tag.ID);
                            parent.Nodes.Remove(this.TheCuttingNode);
                            int index = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, tag);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, this.TheCuttingNode);
                        }
                    }
                    else if (selectedNode.Tag is DELProcessClass)
                    {
                        DELProcessClass class3 = selectedNode.Tag as DELProcessClass;
                        if (parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                        {
                            if (processor.MoveProcessBetweenClass(tag.ID, Guid.Empty, class3.ID))
                            {
                                parent.Nodes.Remove(this.TheCuttingNode);
                                int num2 = this.FindPosition(selectedNode, tag);
                                selectedNode.Nodes.Insert(num2, this.TheCuttingNode);
                                class3.AddProcess(tag.ID);
                            }
                        }
                        else
                        {
                            DELProcessClass class4 = parent.Tag as DELProcessClass;
                            if (processor.MoveProcessBetweenClass(tag.ID, class4.ID, class3.ID))
                            {
                                parent.Nodes.Remove(this.TheCuttingNode);
                                class4.RemoveProcess(tag.ID);
                                int num3 = this.FindPosition(selectedNode, tag);
                                selectedNode.Nodes.Insert(num3, this.TheCuttingNode);
                                class3.AddProcess(tag.ID);
                            }
                        }
                    }
                    this.tvwNavigator.SelectedNode = this.TheCuttingNode;
                    this.frmBPNavigator.UpdateListView(this.TheCuttingNode.Parent);
                    this.TheCuttingNode = null;
                }
            }
        }

        public void OnPreviousWindow(object sender, EventArgs e)
        {
            for (int i = 0; i < base.MdiChildren.Length; i++)
            {
                if ((base.MdiChildren[i] == base.ActiveMdiChild) && (i > 0))
                {
                    base.MdiChildren[i - 1].Activate();
                    return;
                }
            }
            this.prevItem.Enabled = false;
        }

        public void OnProProperty(object sender, EventArgs e)
        {
            this.showProcessPropertyDlg();
        }

        public void OnRefresh(object sender, EventArgs e)
        {
            this.ShowProcessPropertyList();
            if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator.UpdateListView();
            }
        }

        private void OnRefreshContexts(object sender, EventArgs e)
        {
            new ArrayList();
            if (ClientData.Instance.D_RefreshAllContexts != null)
            {
                ClientData.Instance.D_RefreshAllContexts();
            }
        }

        private void OnRefreshSchemas(object sender, EventArgs e)
        {
            ArrayList schemas = null;
            try
            {
                schemas = PLItemViewSchema.Agent.GetAllItemViewSchemas(ClientData.LogonUser.Oid);
            }
            catch (Exception exception)
            {
                PrintException.Print(exception, "显示方案管理");
                return;
            }
            if (ClientData.Instance.D_RefreshAllViewSchemas != null)
            {
                ClientData.Instance.D_RefreshAllViewSchemas(schemas);
            }
        }

        private void OnRelationDelete(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvwNavigator.SelectedNode;
            if (((selectedNode != null) && (selectedNode.Tag is DEMetaRelation)) && (MessageBox.Show("关联删除后将不可恢复，您确定要删除选择的关联吗？", "数据模型定义", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel))
            {
                try
                {
                    PLDataModel.Agent2.DeleteMetaRelation((selectedNode.Tag as DEMetaRelation).Name, ClientData.LogonUser.Oid);
                    DataModelDelegate.Instance.InvokeMetaRelationDeleted(selectedNode.Tag as DEMetaRelation);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
            }
        }

        public void OnRenameProClass(object sender, EventArgs e)
        {
            this.tvwNavigator.LabelEdit = true;
            this.IsNewProClass = false;
            this.tvwNavigator.SelectedNode.BeginEdit();
        }

        private void OnResMapped(object sender, EventArgs e)
        {
            new FrmResoureMapped().ShowDialog();
        }

        private void OnRetriveFromDB(object sender, EventArgs e)
        {
            if (!this.allowConfigDataModel)
            {
                MessageBox.Show("您没有数据模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    ModelContext.Retrieve();
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel.Expand();
                    this.RefreshClassTree();
                    this.RefreshRelations();
                    this.RefreshAllContexts();
                    this.loaded = true;
                    this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel;
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel.Expand();
                    FrmDataModel2 dataModelWnd = this.GetDataModelWnd();
                    dataModelWnd.Show();
                    dataModelWnd.Activate();
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    PrintException.Print(0x65, exception2);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void OnSaveProTemAs(object sender, EventArgs e)
        {
            if (!this.allowCreateProcessManagement)
            {
                MessageBox.Show("您没有创建业务过程模板的权限！", "业务过程管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                Guid iD = ((DELProcessDefProperty) this.tvwNavigator.SelectedNode.Tag).ID;
                WFTEditor editor = this.HashMDiWindows[this.tvwNavigator.SelectedNode] as WFTEditor;
                if ((editor != null) && editor.isNew)
                {
                    MessageBox.Show("请先将过程模型保存到数据库！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    DELProcessDefinition definition;
                    WFTEditor editor2 = new WFTEditor(this) {
                        MdiParent = this
                    };
                    if (editor2.SaveAsAndCreateNewWFTEditor(iD, out definition))
                    {
                        editor2.Dispose();
                        WFTEditor editor3 = new WFTEditor(this) {
                            MdiParent = this,
                            isNew = true
                        };
                        editor3.viewPanel.Dispose();
                        editor3.buildViewPanel();
                        editor3.proTemplate = definition;
                        editor3.viewPanel.shapeData.template = definition;
                        string name = definition.Name;
                        editor3.viewPanel.shapeData.DataSetTopicture();
                        definition.CreatorID = BPMClient.UserID;
                        definition.CreatorName = BPMClient.UserName;
                        definition.Name = name;
                        definition.CreationDate = DateTime.Now;
                        definition.UpdateDate = DateTime.Now;
                        DELProcessDefProperty proDef = new DELProcessDefProperty(editor3.viewPanel.shapeData.template);
                        TreeNode node = new TreeNode(proDef.Name) {
                            Tag = proDef,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        if (((this.frmBPNavigator != null) && (this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0)) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
                        {
                            this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
                        }
                        TreeNode parent = this.tvwNavigator.SelectedNode.Parent;
                        if ((parent.Tag is DELProcessClass) || (parent.Parent.Tag is DELProcessClass))
                        {
                            if (parent.Parent.Tag is DELProcessClass)
                            {
                                parent = parent.Parent;
                            }
                            int index = this.FindPosition(parent, proDef);
                            parent.Nodes.Insert(index, node);
                            this.tvwNavigator.SelectedNode = node;
                            this.HashMDiWindows.Add(node, editor3);
                            this.AddOneProcess(node);
                            DELProcessClass tag = parent.Tag as DELProcessClass;
                            BPMProcessor processor = new BPMProcessor();
                            if (processor.MoveProcessBetweenClass(proDef.ID, Guid.Empty, tag.ID))
                            {
                                tag.AddProcess(proDef.ID);
                            }
                            editor3.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor3.viewPanel.shapeData.template.Name;
                            this.setNodesForProcess(node, editor3.viewPanel);
                            editor3.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor3.viewPanel.shapeData.template.Name;
                            editor3.Show();
                            this.setNodesForProcess(node, editor3.viewPanel);
                            editor3.viewPanel.Refresh();
                            editor3.saveToDataBase();
                            try
                            {
                                BPMAdmin admin = new BPMAdmin();
                                ArrayList theAllProcessRuleList = new ArrayList();
                                admin.GetAllRulesByProcessDefinitionID(BPMClient.UserID, iD, out theAllProcessRuleList);
                                if (theAllProcessRuleList.Count != 0)
                                {
                                    for (int i = 0; i < theAllProcessRuleList.Count; i++)
                                    {
                                        DELProcessRule theProcessRule = (DELProcessRule) theAllProcessRuleList[i];
                                        theProcessRule.ID = Guid.NewGuid();
                                        theProcessRule.ProcessDefinitionID = definition.ID;
                                        theProcessRule.CreationDate = DateTime.Now;
                                        theProcessRule.UpdateDate = DateTime.Now;
                                        theProcessRule.CreatorID = BPMClient.UserID;
                                        foreach (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node3 in editor3.viewPanel.shapeData.root.taskNodAry)
                                        {
                                            DELActivityDefinition realNode = (DELActivityDefinition) node3.realNode;
                                            if (theProcessRule.getLeftActivityName().Equals(realNode.Name))
                                            {
                                                theProcessRule.leftActivityID = realNode.ID;
                                            }
                                            if (theProcessRule.getRightActivityName().Equals(realNode.Name))
                                            {
                                                theProcessRule.rightActivityID = realNode.ID;
                                            }
                                        }
                                        admin.CreateProessRule(BPMClient.UserID, theProcessRule);
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                MessageBox.Show(exception.Message);
                            }
                            MessageBox.Show("成功类似创建业务过程模板！\n\n您可能需要对该模板进行进一步的修改。", "模板类似创建");
                        }
                        else if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM != null)
                        {
                            int num3 = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, proDef);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(num3, node);
                            this.tvwNavigator.SelectedNode = node;
                            this.HashMDiWindows.Add(node, editor3);
                            this.AddOneProcess(node);
                            editor3.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor3.viewPanel.shapeData.template.Name;
                            editor3.Show();
                            this.setNodesForProcess(node, editor3.viewPanel);
                            editor3.viewPanel.Refresh();
                            editor3.saveToDataBase();
                            try
                            {
                                BPMAdmin admin2 = new BPMAdmin();
                                ArrayList list2 = new ArrayList();
                                admin2.GetAllRulesByProcessDefinitionID(BPMClient.UserID, iD, out list2);
                                if (list2.Count != 0)
                                {
                                    for (int j = 0; j < list2.Count; j++)
                                    {
                                        DELProcessRule rule2 = (DELProcessRule) list2[j];
                                        rule2.ID = Guid.NewGuid();
                                        rule2.ProcessDefinitionID = definition.ID;
                                        rule2.CreationDate = DateTime.Now;
                                        rule2.UpdateDate = DateTime.Now;
                                        rule2.CreatorID = BPMClient.UserID;
                                        foreach (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode node4 in editor3.viewPanel.shapeData.root.taskNodAry)
                                        {
                                            DELActivityDefinition definition3 = (DELActivityDefinition) node4.realNode;
                                            if (rule2.getLeftActivityName().Equals(definition3.Name))
                                            {
                                                rule2.leftActivityID = definition3.ID;
                                            }
                                            if (rule2.getRightActivityName().Equals(definition3.Name))
                                            {
                                                rule2.rightActivityID = definition3.ID;
                                            }
                                        }
                                        admin2.CreateProessRule(BPMClient.UserID, rule2);
                                    }
                                }
                            }
                            catch (Exception exception2)
                            {
                                MessageBox.Show(exception2.Message);
                            }
                            MessageBox.Show("成功类似创建业务过程模板！\n\n您可能需要对该模板进行进一步的修改。", "模板类似创建");
                        }
                    }
                    else
                    {
                        editor2.Dispose();
                    }
                }
            }
        }

        public void OnSaveToDataBaseForMenu(object sender, EventArgs e)
        {
            if (this.IfProcessWindowActived())
            {
                this.SaveToDB();
            }
        }

        public void OnSaveToDB(object sender, EventArgs e)
        {
            this.SaveToDB();
        }

        private void OnSaveToLocal(object sender, EventArgs e)
        {
            this.SaveToLocal();
        }

        public void OnSaveToLocalForMenu(object sender, EventArgs e)
        {
            if (this.IfProcessWindowActived())
            {
                this.SaveToLocal();
            }
        }

        public void OnSearchPerm(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel || (this.deCurrentAdminRole.ParentAdminRole != Guid.Empty))
            {
                MessageBox.Show("您没有权限查询的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FrmPermSearch.SearchPerm(null, this);
            }
        }

        public void OnSetPassword(object sender, EventArgs e)
        {
            new FrmSetPsnPwd(ClientData.LogonUser.Oid).ShowDialog();
        }

        private void OnTideskInitSetting(object sender, EventArgs e)
        {
            new FrmTideskInitSetting().ShowDialog();
        }

        public void OnTiEventLog(object sender, EventArgs e)
        {
            ClientData.OpenTiEventLog();
        }

        public void OnToolbarButtonClicked(object sender, EventArgs e)
        {
            if (typeof(ToolBarItem).IsInstanceOfType(sender))
            {
                ToolBarItem item = (ToolBarItem) sender;
                string text = "Clicked Event: ";
                if (((item.Text != string.Empty) && (item.Text != "")) && (item.Text != null))
                {
                    text = text + item.Text;
                }
                else if (item.ToolTip != string.Empty)
                {
                    text = text + item.ToolTip;
                }
                MessageBox.Show(text);
            }
            else if (typeof(MenuItemEx).IsInstanceOfType(sender))
            {
                MenuItemEx ex = (MenuItemEx) sender;
                MessageBox.Show(ex.Text);
            }
        }

        public void OnToolbarButtonDropDown(object sender, EventArgs e)
        {
            if (typeof(ToolBarItem).IsInstanceOfType(sender))
            {
                ToolBarItem item = (ToolBarItem) sender;
                string text = "Dropdown Event: ";
                if (((item.Text != string.Empty) && (item.Text != "")) && (item.Text != null))
                {
                    text = text + item.Text;
                }
                else if (item.ToolTip != string.Empty)
                {
                    text = text + item.ToolTip;
                }
                if (item.ToolTip == "New Project")
                {
                    ContextMenu menu = new ContextMenu(this.CreateDropDownMenuEx("&Edit"));
                    Rectangle itemRectangle = item.ItemRectangle;
                    Point pos = new Point(itemRectangle.Left, itemRectangle.Bottom);
                    menu.Show(this.standardToolBar, pos);
                }
                else if (item.ToolTip == "Add New Item")
                {
                    ContextMenu menu2 = new ContextMenu(this.CreateDropDownMenuEx("&View"));
                    Rectangle rectangle2 = item.ItemRectangle;
                    Point point2 = new Point(rectangle2.Left, rectangle2.Bottom);
                    menu2.Show(this.standardToolBar, point2);
                }
                else if (item.ToolTip == "Class View")
                {
                    ContextMenu menu3 = new ContextMenu(this.CreateDropDownMenuEx("&Project"));
                    Rectangle rectangle3 = item.ItemRectangle;
                    Point point3 = new Point(rectangle3.Left, rectangle3.Bottom);
                    menu3.Show(this.standardToolBar, point3);
                }
                else if (item.ToolTip == "Breakpoints")
                {
                    ContextMenu menu4 = new ContextMenu(this.CreateDropDownMenuEx("&File"));
                    Rectangle rectangle4 = item.ItemRectangle;
                    Point point4 = new Point(rectangle4.Left, rectangle4.Bottom);
                    menu4.Show(this.debugToolBar, point4);
                }
                else if (item.ToolTip == "Undo")
                {
                    ColorPickerDropDown down = new ColorPickerDropDown();
                    Rectangle r = item.ItemRectangle;
                    Rectangle rectangle6 = this.standardToolBar.RectangleToScreen(r);
                    down.DesktopBounds = new Rectangle(rectangle6.Left, rectangle6.Bottom, down.Width, down.Height);
                    if (!down.Visible)
                    {
                        down.Show();
                    }
                    else
                    {
                        down.Visible = false;
                    }
                }
                else if (item.ToolTip == "New Mail")
                {
                    ContextMenu menu5 = new ContextMenu(this.CreateDropDownMenuEx("&File"));
                    Rectangle rectangle7 = item.ItemRectangle;
                    Point point5 = new Point(rectangle7.Left, rectangle7.Bottom);
                    menu5.Show(this.outlookToolBar, point5);
                }
                else
                {
                    MessageBox.Show(text);
                }
            }
        }

        private void OnUpdateAllViews(object sender, EventArgs e)
        {
            IProgressCallback progressWindow = ClientData.GetProgressWindow();
            ArrayList state = new ArrayList {
                progressWindow
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.UpdateAllViews), state);
            progressWindow.ShowWindow();
        }

        public void OnVMMenuPopup(object sender, EventArgs e)
        {
            if (!this.allowViewManagement)
            {
                MessageBox.Show("您没有视图模型管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void OpenLocalFile()
        {
            new PLUser();
            bool flag = false;
            try
            {
                if (PLGrantPerm.CanDoFunctionOperation(ClientData.LogonUser.Oid, "Fun_TiModeler_CREATE_PROCESS_DEFINATION") == 1)
                {
                    flag = true;
                }
            }
            catch (ResponsibilityException exception)
            {
                MessageBox.Show(exception.Message, "权限校验", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (flag)
            {
                DELProcessDefinition template = new DELProcessDefinition();
                WFTEditor editor = new WFTEditor(template, this) {
                    viewPanel = { shapeData = { template = template } },
                    proTemplate = template,
                    MdiParent = this
                };
                try
                {
                    if (editor.openLocal())
                    {
                        editor.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor.proTemplate.Name;
                        editor.Show();
                        editor.viewPanel.Refresh();
                    }
                    else
                    {
                        editor.Dispose();
                    }
                }
                catch (Exception exception2)
                {
                    BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception2);
                    return;
                }
            }
            else
            {
                MessageBox.Show("您没有权限执行该操作", "权限校验", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void OpenProcessTemplate()
        {
            this.OpenProcessTemplate(this.tvwNavigator.SelectedNode);
        }

        public void OpenProcessTemplate(TreeNode selectedNode)
        {
            PLGrantPerm perm = new PLGrantPerm();
            WFTEditor editor = this.HashMDiWindows[this.tvwNavigator.SelectedNode] as WFTEditor;
            if (((editor != null) && !editor.isNew) && (perm.CanDoObjectOperation(ClientData.LogonUser.Oid, ((DELProcessDefProperty) this.tvwNavigator.SelectedNode.Tag).ID, "BPM_PROCESS_DEFINITION", "ClaRel_BROWSE", 0, Guid.Empty) == 0))
            {
                MessageBox.Show("您没有权限打开该流程模板！", "打开提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                if (this.HashMDiWindows.ContainsKey(selectedNode))
                {
                    ((Form) this.HashMDiWindows[selectedNode]).Activate();
                }
                else
                {
                    WFTEditor editor2 = new WFTEditor(this) {
                        MdiParent = this
                    };
                    Guid iD = ((DELProcessDefProperty) selectedNode.Tag).ID;
                    try
                    {
                        editor2.openDataBase(ClientData.LogonUser.Oid, iD);
                    }
                    catch (BPMException exception)
                    {
                        Cursor.Current = Cursors.Default;
                        BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                        return;
                    }
                    catch
                    {
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("打开过程模板失败！", "过程定义", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    editor2.proTemplate = editor2.viewPanel.shapeData.template;
                    editor2.Text = rmTiModeler.GetString("strWFTEditorName") + "   " + editor2.proTemplate.Name;
                    editor2.Show();
                    editor2.viewPanel.Refresh();
                    this.HashMDiWindows.Add(selectedNode, editor2);
                    this.setNodesForProcess(selectedNode, editor2.viewPanel);
                }
                Cursor.Current = Cursors.Default;
            }
        }

        public void OpenProTem()
        {
            if (this.isDoubleClick)
            {
                this.OpenProcessTemplate();
            }
        }

        public void OpenViewModel(object sender, EventArgs e)
        {
            VMFrame viewFrameFrm = this.GetViewFrameFrm();
            viewFrameFrm.Show();
            viewFrameFrm.Activate();
        }

        private void OrgDel(object sender, EventArgs e)
        {
            if (MessageBox.Show(OrgModelUL.rmOrgModel.GetString("OrgUser.Organ.SureDelOrg") + this.tvwNavigator.SelectedNode.Text + OrgModelUL.rmOrgModel.GetString("OrgUser.Organ.Ma"), OrgModelUL.rmOrgModel.GetString("OrgUser.Organ.OrgDel"), MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                PLOrganization organization = new PLOrganization();
                DEOrganization tag = (DEOrganization) this.tvwNavigator.SelectedNode.Tag;
                if ((((tag.Option & 1) != 1) || !organization.HasUsedProjGroup(tag.Oid)) || (MessageBox.Show("该项目组已经与业务对象绑定了，如果您要删除该项目组，这些与之绑定的业务对象将不再受项目组权限的控制，且绑定关系不可恢复！您确定要删除该项目组吗？", "删除项目组", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel))
                {
                    organization.Delete(ClientData.LogonUser.LogId, tag.Oid);
                    if (this.tvwNavigator.SelectedNode.Parent.Tag.Equals("OrgModel"))
                    {
                        this.tvwNavigator.SelectedNode.Text = "组织";
                        this.tvwNavigator.SelectedNode.Tag = "Organization";
                    }
                    else
                    {
                        TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                        this.tvwNavigator.SelectedNode = this.tvwNavigator.SelectedNode.Parent;
                        this.tvwNavigator.Nodes.Remove(selectedNode);
                    }
                }
            }
        }

        private void OrgProp(object sender, EventArgs e)
        {
            AdminOperStatus status = new AdminOperStatus(this.deCurrentAdminRole);
            FrmOrgProperty property = new FrmOrgProperty {
                deAdminRole = this.deCurrentAdminRole,
                deOrganization = ((DEOrganization) this.tvwNavigator.SelectedNode.Tag).Clone()
            };
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmOrgUser)
                {
                    property.frmOrgUser = (FrmOrgUser) enumerator.Value;
                    break;
                }
            }
            property.displayCreateOrg = status.displayCreateOrg;
            property.displayDeleteOrg = status.displayDeleteOrg;
            property.displayModifyOrg = status.displayModifyOrg;
            property.displayAddUser = status.displayAddOrgUser;
            property.displayAddRole = status.displayAddOrgRole;
            property.displayDeleteUser = status.displayDeleteOrgUser;
            property.displayDeleteRole = status.displayDeleteOrgRole;
            property.ShowDialog();
            property.Dispose();
        }

        private void OrgRefresh(object sender, EventArgs e)
        {
            Guid oid = ((DEOrganization) this.tvwNavigator.SelectedNode.Tag).Oid;
            try
            {
                this.RefreshOrgTree();
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("组织模型不存在！"))
                {
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Text = "组织";
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Tag = "Organization";
                }
                MessageBox.Show(exception.Message, "组织模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            this.GetOrgWnd().lvwOrgUser.Items.Clear();
            this.GetOrgWnd().lvwSuborg.Items.Clear();
            this.GetOrgWnd().ShowOrg(this.tvwNavigator.SelectedNode.Tag);
            this.tvwNavigator.SelectedNode = this.JudgeSelectesNode(oid);
        }

        private void OrgRename(object sender, EventArgs e)
        {
            this.tvwNavigator.LabelEdit = true;
            if (((DEOrganization) this.tvwNavigator.SelectedNode.Tag).ParentOrg.Equals(Guid.Empty))
            {
                this.tvwNavigator.SelectedNode.Text = ((DEOrganization) this.tvwNavigator.SelectedNode.Tag).Name;
            }
            this.tvwNavigator.SelectedNode.BeginEdit();
        }

        private void PassWordRule(object sender, EventArgs e)
        {
            new FrmPassWordRules().ShowDialog();
        }

        public void PasteProTem()
        {
            if (this.TheCuttingNode != null)
            {
                if ((this.frmBPNavigator.lvwNavigater.SelectedItems.Count != 0) && (this.tvwNavigator.SelectedNode != ((TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag)))
                {
                    this.tvwNavigator.SelectedNode = (TreeNode) this.frmBPNavigator.lvwNavigater.SelectedItems[0].Tag;
                }
                TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                if (this.TheCuttingNode.Parent == selectedNode)
                {
                    this.TheCuttingNode = null;
                }
                else
                {
                    BPMProcessor processor = new BPMProcessor();
                    TreeNode parent = this.TheCuttingNode.Parent;
                    DELProcessDefProperty tag = this.TheCuttingNode.Tag as DELProcessDefProperty;
                    if (selectedNode == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass class2 = parent.Tag as DELProcessClass;
                        if (processor.MoveProcessBetweenClass(tag.ID, class2.ID, Guid.Empty))
                        {
                            class2.RemoveProcess(tag.ID);
                            parent.Nodes.Remove(this.TheCuttingNode);
                            int index = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, tag);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, this.TheCuttingNode);
                        }
                    }
                    else if (selectedNode.Tag is DELProcessClass)
                    {
                        DELProcessClass class3 = selectedNode.Tag as DELProcessClass;
                        if (parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                        {
                            if (processor.MoveProcessBetweenClass(tag.ID, Guid.Empty, class3.ID))
                            {
                                parent.Nodes.Remove(this.TheCuttingNode);
                                int num2 = this.FindPosition(selectedNode, tag);
                                selectedNode.Nodes.Insert(num2, this.TheCuttingNode);
                                class3.AddProcess(tag.ID);
                            }
                        }
                        else
                        {
                            DELProcessClass class4 = parent.Tag as DELProcessClass;
                            if (processor.MoveProcessBetweenClass(tag.ID, class4.ID, class3.ID))
                            {
                                parent.Nodes.Remove(this.TheCuttingNode);
                                class4.RemoveProcess(tag.ID);
                                int num3 = this.FindPosition(selectedNode, tag);
                                selectedNode.Nodes.Insert(num3, this.TheCuttingNode);
                                class3.AddProcess(tag.ID);
                            }
                        }
                    }
                    this.tvwNavigator.SelectedNode = this.TheCuttingNode;
                    this.frmBPNavigator.UpdateListView(this.TheCuttingNode.Parent);
                    this.TheCuttingNode = null;
                }
            }
        }

        public void PasteRoles(object sender, EventArgs e)
        {
            try
            {
                CLCopyData pasteDataList = this.GetPasteDataList();
                if (((pasteDataList != null) && (pasteDataList.Count != 0)) && ((pasteDataList.Operation == "剪切") && (pasteDataList.Count != 0)))
                {
                    for (int i = 0; i < pasteDataList.Count; i++)
                    {
                        if (!(pasteDataList[i] is DERole))
                        {
                            pasteDataList.RemoveAt(i);
                            i--;
                        }
                    }
                    if (pasteDataList.Count != 0)
                    {
                        TreeNode selectedNode = this.tvwNavigator.SelectedNode;
                        Guid empty = Guid.Empty;
                        Guid newGroupOid = Guid.Empty;
                        if (pasteDataList.Parent is DERoleGroup)
                        {
                            empty = ((DERoleGroup) pasteDataList.Parent).Oid;
                        }
                        if (selectedNode.Tag is DERoleGroup)
                        {
                            newGroupOid = ((DERoleGroup) selectedNode.Tag).Oid;
                        }
                        if (empty != newGroupOid)
                        {
                            PLRole role = new PLRole();
                            foreach (DERole role2 in pasteDataList)
                            {
                                role.MoveRoleBetweenGroup(role2.Oid, empty, newGroupOid);
                            }
                            FrmRoleList roleWnd = this.GetRoleWnd();
                            if (selectedNode.Tag is DERoleGroup)
                            {
                                roleWnd.CurrentIsGroup = true;
                                roleWnd.CurrentRoleGroup = selectedNode.Tag as DERoleGroup;
                                roleWnd.RoleGroupRefresh((DERoleGroup) selectedNode.Tag);
                            }
                            else
                            {
                                roleWnd.CurrentIsGroup = false;
                                roleWnd.CurrentRoleGroup = null;
                                roleWnd.RoleRefresh(null, null);
                            }
                            roleWnd.cuttedDataList = null;
                            selectedNode.Expand();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                PrintException.Print(exception, "角色模型");
            }
        }

        protected MenuItemEx ProcessMenuItem(XmlElement xmlelement)
        {
            string innerXml = xmlelement.Attributes["Name"].InnerXml;
            if (innerXml.IndexOf("&") != -1)
            {
                innerXml = innerXml.Replace("&amp;", "&");
            }
            string name = xmlelement.Attributes["Shortcut"].InnerXml;
            string iconName = xmlelement.Attributes["Icon"].InnerXml;
            string str4 = xmlelement.Attributes["Checked"].InnerXml;
            string str5 = xmlelement.Attributes["Radio"].InnerXml;
            string str6 = xmlelement.Attributes["Enabled"].InnerXml;
            string str7 = xmlelement.Attributes["EventHandler"].InnerXml;
            Shortcut none = Shortcut.None;
            if (name != "")
            {
                none = (Shortcut) Shortcut.F1.GetType().InvokeMember(name, BindingFlags.GetField, null, Shortcut.F1, new object[0]);
            }
            MenuItemEx ex = new MenuItemEx(innerXml, new EventHandler(this.SelectMenuItem), none);
            if (xmlelement.Attributes[Thread.CurrentThread.CurrentUICulture.Name] == null)
            {
                ex.Text = xmlelement.Attributes["zh-CN"].InnerXml.Replace("&amp;", "&");
            }
            else
            {
                ex.Text = xmlelement.Attributes[Thread.CurrentThread.CurrentUICulture.Name].InnerXml.Replace("&amp;", "&");
            }
            if (iconName != "")
            {
                ex.Icon = this.GetIcon(iconName);
            }
            if (str4 == "True")
            {
                ex.Checked = true;
            }
            if (str6 == "False")
            {
                ex.Enabled = false;
            }
            if (str5 == "True")
            {
                ex.RadioCheck = true;
            }
            if (str7 != null)
            {
                ex.EventHandlerName = str7;
            }
            XmlNodeList childNodes = xmlelement.ChildNodes;
            MenuItem[] itemArray = null;
            if (childNodes.Count > 0)
            {
                itemArray = new MenuItem[childNodes.Count];
                int index = 0;
                foreach (XmlElement element in childNodes)
                {
                    itemArray[index] = this.ProcessMenuItem(element);
                    if (itemArray[index] != null)
                    {
                        ex.MenuItems.Add(itemArray[index]);
                    }
                    index++;
                }
            }
            return ex;
        }

        public void RefreshAdminRoleTree()
        {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            AdminRoleUL.FillSubAdminRoleTree(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0], iconIndex);
        }

        private void RefreshAllContexts()
        {
            TreeNode root = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Context;
            if ((root != null) && (((string) root.Tag) == "Context"))
            {
                ArrayList lst = new ArrayList(PLDataModel.GetAllContexts(ClientData.Session));
                FrmItemDisplayContext.InitContextTreeNodes(root, ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE"), ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE"), lst);
            }
        }

        public void RefreshAllViewModel(object sender, EventArgs e)
        {
            this.ShowAllViewModelsInTree();
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEWMODEL");
            ViewModelUL.FillViewModels(this.GetViewModelList().lvwViewModel, false, iconIndex);
        }

        private void RefreshAllViewSchemas(ArrayList schemas)
        {
            TreeNode root = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewSchema;
            if ((root != null) && (((string) root.Tag) == "ViewSchema"))
            {
                FrmItemDisplaySchema.InitSchemaTreeNodes(root, ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE"), ClientData.MyImageList.GetIconIndex("ICO_DMM_FORMCUSTOMIZE"));
            }
        }

        public void RefreshBrowser()
        {
            this.GetBrowserWnd().lvwBrowser.Items.Clear();
            this.GetBrowserWnd().CreatList();
        }

        private void RefreshClassTree()
        {
            this.GetDataModelWnd();
            UIDataModel.FillRealtimeClassesTree(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class);
        }

        private void RefreshCurrentFrm(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            bool flag = false;
            while (enumerator.MoveNext())
            {
                if (enumerator.Value is FrmRuleList)
                {
                    FrmRuleList list = (FrmRuleList) enumerator.Value;
                    list.FreshRuleList();
                    list.Activate();
                    flag = true;
                }
            }
            if (!flag)
            {
                FrmRuleList list2 = new FrmRuleList(this.HashMDiWindows) {
                    MdiParent = this
                };
                list2.Show();
                list2.Activate();
                TreeNode key = new TreeNode("规则列表");
                this.HashMDiWindows.Add(key, list2);
            }
        }

        private void RefreshExtModel()
        {
            DEExtendedModel[] allExtendedModel = UIExtendedModel.Instance.GetAllExtendedModel();
            foreach (TreeNode node in this.tvwNavigator.Nodes[0].Nodes)
            {
                string tag = node.Tag as string;
                if (tag == "EXTENDED_MODEL")
                {
                    UIExtendedModel.Instance.FillExtendedModes(node, allExtendedModel);
                    break;
                }
            }
        }

        private void RefreshExtModel(object sender, EventArgs e)
        {
            this.RefreshExtModel();
        }

        public void RefreshNode(TreeNode TheNode)
        {
            if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator.SetSelectItem(TheNode, true);
            }
        }

        public void RefreshOrgTree()
        {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
            OrgModelUL.FillOrgTree(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes[0], iconIndex);
        }

        private void refreshProcessDefPropertyNodes(DELBPMEntityList MyProcessDefPropertyList, DELBPMEntityList MyProcessClassList)
        {
            Cursor.Current = Cursors.WaitCursor;
            Hashtable hashtable = new Hashtable();
            foreach (DELProcessDefProperty property in MyProcessDefPropertyList)
            {
                hashtable.Add(property.ID, property);
            }
            ArrayList list = new ArrayList(this.TheProClsNodeList.Values);
            foreach (TreeNode node in list)
            {
                DELProcessClass tag = node.Tag as DELProcessClass;
                bool flag = false;
                foreach (DELProcessClass class3 in MyProcessClassList)
                {
                    if (tag.ID == class3.ID)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.TheProClsNodeList.Remove(tag.ID);
                    node.Parent.Nodes.Remove(node);
                }
            }
            list.Clear();
            list.AddRange(this.TheProDefNodeList.Values);
            foreach (TreeNode node2 in list)
            {
                DELProcessDefProperty property2 = node2.Tag as DELProcessDefProperty;
                if (!hashtable.Contains(property2.ID))
                {
                    this.TheProDefNodeList.Remove(property2.ID);
                    if (node2.Parent != null)
                    {
                        node2.Parent.Nodes.Remove(node2);
                    }
                }
            }
            for (int i = 0; i < MyProcessClassList.Count; i++)
            {
                TreeNode node3;
                DELProcessClass proCls = MyProcessClassList[i] as DELProcessClass;
                if (!this.TheProClsNodeList.Contains(proCls.ID))
                {
                    node3 = new TreeNode(proCls.Name) {
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                        SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE"),
                        Tag = proCls
                    };
                    int index = this.FindPosition(proCls);
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, node3);
                    this.TheProClsNodeList.Add(proCls.ID, node3);
                }
                else
                {
                    node3 = this.TheProClsNodeList[proCls.ID] as TreeNode;
                    node3.Tag = proCls;
                    if (node3.Text != proCls.Name)
                    {
                        node3.Text = proCls.Name;
                    }
                }
                foreach (Guid guid in proCls.ProcessIDList)
                {
                    DELProcessDefProperty proDef = hashtable[guid] as DELProcessDefProperty;
                    if (proDef != null)
                    {
                        TreeNode node4 = this.TheProDefNodeList[guid] as TreeNode;
                        if (node4 == null)
                        {
                            node4 = this.FindNode(node3, proDef);
                            if (node4 == null)
                            {
                                node4 = new TreeNode(proDef.Name) {
                                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                                    Tag = proDef
                                };
                                int num3 = this.FindPosition(node3, proDef);
                                node3.Nodes.Insert(num3, node4);
                            }
                            this.TheProDefNodeList.Add(guid, node4);
                        }
                        else if (node4.Parent == node3)
                        {
                            node4.Tag = proDef;
                            if (node4.Text != proDef.Name)
                            {
                                node4.Text = proDef.Name;
                            }
                        }
                        else
                        {
                            node4.Parent.Nodes.Remove(node4);
                            node4.Tag = proDef;
                            if (node4.Text != proDef.Name)
                            {
                                node4.Text = proDef.Name;
                            }
                            int num4 = this.FindPosition(node3, proDef);
                            node3.Nodes.Insert(num4, node4);
                        }
                        MyProcessDefPropertyList.Remove(proDef);
                    }
                }
            }
            for (int j = 0; j < MyProcessDefPropertyList.Count; j++)
            {
                DELProcessDefProperty property4 = (DELProcessDefProperty) MyProcessDefPropertyList[j];
                if (((property4.Reserve2 != "FORPPM") && (property4.IsVisible == 1)) && !property4.State.Equals("Deleted"))
                {
                    bool flag2 = true;
                    foreach (TreeNode node5 in Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes)
                    {
                        if (!(node5.Tag is DELProcessClass) && ((DELProcessDefProperty) node5.Tag).ID.Equals(property4.ID))
                        {
                            flag2 = false;
                            node5.Tag = property4;
                            if (((WFTEditor) this.HashMDiWindows[node5]) == null)
                            {
                            }
                            break;
                        }
                    }
                    if (flag2)
                    {
                        TreeNode node6 = new TreeNode(property4.Name) {
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF"),
                            Tag = property4
                        };
                        int num6 = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, property4);
                        Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(num6, node6);
                        this.TheProDefNodeList.Add(property4.ID, node6);
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void RefreshRelations()
        {
            this.GetDataModelWnd();
            UIDataModel.FillRealtimeRelations(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation);
        }

        public void RefreshViewModel(object sender, EventArgs e)
        {
            VMFrame viewFrameFrm = this.GetViewFrameFrm();
            viewFrameFrm.RefreshViewModel();
            viewFrameFrm.Show();
            viewFrameFrm.Activate();
        }

        public void RelationList_ItemActivated(DEMetaRelation rlt)
        {
            TreeNode node = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation;
            node.Expand();
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                DEMetaRelation tag = (DEMetaRelation) node.Nodes[i].Tag;
                if (tag.Oid.Equals(rlt.Oid))
                {
                    this.tvwNavigator.SelectedNode = node.Nodes[i];
                    return;
                }
            }
        }

        internal void RemoveOneProcess(TreeNode TheNode)
        {
            if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator.DeleteOneProcess(TheNode);
            }
        }

        public void RemoveWindows(Form frm)
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value == frm)
                {
                    this.HashMDiWindows.Remove(enumerator.Key);
                    return;
                }
            }
        }

        public void RemoveWnd(Form frm)
        {
            IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value == frm)
                {
                    this.HashMDiWindows.Remove(enumerator.Key);
                    return;
                }
            }
        }

        private void RoleRefresh(object sender, EventArgs e)
        {
            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Clear();
            this.ShowRoleGroups();
            this.GetRoleWnd().lvwRole.Items.Clear();
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
            if (this.deCurrentAdminRole != null)
            {
                if (this.deCurrentAdminRole.ParentAdminRole == Guid.Empty)
                {
                    AdminRoleUL.FillAllRoles(this.GetRoleWnd().lvwRole, iconIndex, false, true);
                }
                else
                {
                    AdminRoleUL.FillAdminRoleRoles(this.GetRoleWnd().lvwRole, iconIndex, this.deCurrentAdminRole.Oid, true);
                }
            }
        }

        private bool SameGroupExist(string groupName)
        {
            PLRole role = new PLRole();
            foreach (DERoleGroup group in role.GetAllRoleGroups())
            {
                if (group.Name.ToUpper().Equals(groupName.ToUpper()))
                {
                    return true;
                }
            }
            return false;
        }

        private void SaveToDB()
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
            if (editor != null)
            {
                editor.saveToDataBase();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strTempNotOpen"));
            }
        }

        private void SaveToLocal()
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
            if (editor != null)
            {
                editor.saveToLocal();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strTempNotOpen"));
            }
        }

        public void SaveViewModel(object sender, EventArgs e)
        {
            VMFrame viewFrameFrm = this.GetViewFrameFrm();
            viewFrameFrm.Show();
            viewFrameFrm.Activate();
            try
            {
                viewFrameFrm.viewPanel.saveFile();
                MessageBox.Show("保存视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”成功！", "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("保存视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”失败！", "保存视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void SearchPermission(object sender, EventArgs e)
        {
            if (!this.allowConfigOrgModel)
            {
                MessageBox.Show("您没有组织管理权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void SearchTemplates(object sender, EventArgs e)
        {
            DEMetaClass tag = null;
            string tmpType = "";
            if (this.tvwNavigator.SelectedNode.Tag is DEMetaClass)
            {
                tag = this.tvwNavigator.SelectedNode.Tag as DEMetaClass;
                if (tag == null)
                {
                    return;
                }
                tmpType = tag.Name;
            }
            else if (this.tvwNavigator.SelectedNode.Tag is string)
            {
                tmpType = this.tvwNavigator.SelectedNode.Tag as string;
            }
            if (this.HashMDiWindows.ContainsKey(this.tvwNavigator.SelectedNode))
            {
                Form form = (Form) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
                if (form.Visible)
                {
                    form.Activate();
                    return;
                }
                this.HashMDiWindows.Remove(this.tvwNavigator.SelectedNode);
            }
            FrmBrowse browse = null;
            try
            {
                browse = new FrmBrowse(false, tmpType, ClientData.LogonUser);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.HashMDiWindows.Add(this.tvwNavigator.SelectedNode, browse);
            browse.MdiParent = this;
            browse.WindowState = FormWindowState.Maximized;
            browse.Show();
        }

        public void SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox box = (ComboBox) sender;
            ComboBox imageComboBox = this.imageComboBox;
            ComboBox textComboBox = this.textComboBox;
            ComboBox colorComboBox = this.colorComboBox;
        }

        protected void SelectMenuItem(object sender, EventArgs e)
        {
            try
            {
                if (typeof(MenuItemEx).IsInstanceOfType(sender))
                {
                    MenuItemEx ex = (MenuItemEx) sender;
                    MethodInfo method = base.GetType().GetMethod(ex.EventHandlerName);
                    object[] parameters = new object[] { sender, e };
                    method.Invoke(this, parameters);
                }
            }
            catch
            {
            }
        }

        public void setNodesForProcess(TreeNode RootNode, ViewPanel MyViewPanel)
        {
            Picture root = MyViewPanel.shapeData.root;
            RootNode.Nodes.Clear();
            TreeNode node = new TreeNode(root.startNode.realNode.Name) {
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_START"),
                SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_START"),
                Tag = root.startNode
            };
            RootNode.Nodes.Add(node);
            node = new TreeNode(root.endNode.realNode.Name) {
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_END"),
                SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_END"),
                Tag = root.endNode
            };
            RootNode.Nodes.Add(node);
            root.taskNodAry.Sort();
            for (int i = 0; i < root.taskNodAry.Count; i++)
            {
                node = new TreeNode(((Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) root.taskNodAry[i]).realNode.Name) {
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_TASK"),
                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_TASK"),
                    Tag = (Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) root.taskNodAry[i]
                };
                RootNode.Nodes.Add(node);
            }
            for (int j = 0; j < root.routeNodAry.Count; j++)
            {
                node = new TreeNode(((RouteNode) root.routeNodAry[j]).realNode.Name) {
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_ROUTER"),
                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_ROUTER"),
                    Tag = (RouteNode) root.routeNodAry[j]
                };
                RootNode.Nodes.Add(node);
            }
        }

        private void SetPPMIcons(TreeNode node)
        {
            if (node.Tag is DEMetaClass)
            {
                int objectImage = ClientData.ItemImages.GetObjectImage(((DEMetaClass) node.Tag).Name, "release");
                ClientData.MyImageList.imageList.Images.Add(ClientData.ItemImages.ImgList.Images[objectImage]);
                if (((DEMetaClass) node.Tag).Name != "PPSIGNTEMPLATE")
                {
                    node.Text = node.Text + "模板";
                }
                node.ImageIndex = ClientData.MyImageList.imageList.Images.Count - 1;
                node.SelectedImageIndex = ClientData.MyImageList.imageList.Images.Count - 1;
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.SetPPMIcons(node2);
                }
            }
        }

        private void SetPrintIcons(TreeNode node)
        {
            if (node.Tag is DEMetaClass)
            {
                int objectImage = ClientData.ItemImages.GetObjectImage(((DEMetaClass) node.Tag).Name, "release");
                ClientData.MyImageList.imageList.Images.Add(ClientData.ItemImages.ImgList.Images[objectImage]);
                node.Text = node.Text + "打印模板";
                node.ImageIndex = ClientData.MyImageList.imageList.Images.Count - 1;
                node.SelectedImageIndex = ClientData.MyImageList.imageList.Images.Count - 1;
                foreach (TreeNode node2 in node.Nodes)
                {
                    this.SetPrintIcons(node2);
                }
            }
        }

        private void SetSelectedShapeInEditor()
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode.Parent];
            if (editor != null)
            {
                if (editor.viewPanel.selectedShape != null)
                {
                    editor.viewPanel.selectedShape.isSelected = false;
                }
                editor.viewPanel.selectedShape = (Shape) this.tvwNavigator.SelectedNode.Tag;
                editor.viewPanel.selectedShape.isSelected = true;
                editor.viewPanel.Refresh();
            }
        }

        public void setTheCuttingNode(TreeNode aNode)
        {
            this.TheCuttingNode = aNode;
        }

        public void setTheDragingNode(TreeNode aNode)
        {
            this.TheDragingNode = aNode;
        }

        private void ShowAllViewModelsInTree()
        {
            ArrayList allViewModel = new ArrayList();
            PLViewModel model = new PLViewModel();
            try
            {
                allViewModel = model.GetAllViewModel();
            }
            catch
            {
                MessageBox.Show("获取所有视图模型失败！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            this.tvwNavigator.SelectedNode.Nodes.Clear();
            if ((allViewModel != null) && (allViewModel.Count != 0))
            {
                int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_VIEWMODEL");
                foreach (DEViewModel model2 in allViewModel)
                {
                    TreeNode node = new TreeNode(model2.Name, iconIndex, iconIndex) {
                        Tag = model2
                    };
                    this.tvwNavigator.SelectedNode.Nodes.Add(node);
                }
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_ViewNetwork.ExpandAll();
            }
        }

        public void ShowAllViews(object sender, EventArgs e)
        {
            FrmViewList viewList = this.GetViewList();
            viewList.MdiParent = this;
            viewList.Show();
            viewList.Activate();
        }

        private void ShowBPNavigator()
        {
            Cursor.Current = Cursors.WaitCursor;
            if ((this.frmBPNavigator == null) || this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator = new FrmBPNavigator(this);
                this.frmBPNavigator.MdiParent = this;
            }
            this.frmBPNavigator.UpdateListView(this.tvwNavigator.SelectedNode);
            this.frmBPNavigator.WindowState = FormWindowState.Maximized;
            this.frmBPNavigator.Show();
            this.frmBPNavigator.Activate();
            Cursor.Current = Cursors.Default;
        }

        private void showProcessPropertyDlg()
        {
            WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
            if (editor != null)
            {
                editor.ShowProPropertyDlg();
            }
            else
            {
                MessageBox.Show(rmTiModeler.GetString("strTempNotOpen"));
            }
        }

        public void ShowProcessPropertyList()
        {
            if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM != null)
            {
                BPMAdmin admin = new BPMAdmin();
                BPMProcessor processor = new BPMProcessor();
                try
                {
                    DELBPMEntityList list;
                    admin.GetProcessDefPropertyList(ClientData.LogonUser.Oid, out list);
                    DELBPMEntityList allProcessClasses = processor.GetAllProcessClasses();
                    this.refreshProcessDefPropertyNodes(list, allProcessClasses);
                }
                catch (BPMException exception)
                {
                    BPMModelExceptionHandle.InvokeBPMExceptionHandler(exception);
                }
            }
        }

        public void ShowRoleGroups()
        {
            if (Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role != null)
            {
                PLRole role = new PLRole();
                try
                {
                    ArrayList allRoleGroups = role.GetAllRoleGroups();
                    this.BuildRoleGroupNode(allRoleGroups);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "角色模型");
                }
            }
        }

        public void ToolRefresh(object sender, EventArgs e)
        {
            this.GetToolWnd().lvwTool.Items.Clear();
            this.GetToolWnd().CreatList();
        }

        private void tvwNavigator_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            DELProcessClass class3;
            int num2;
            if (e.Node.Tag is DEOrganization)
            {
                if (e.Label == null)
                {
                    if (((DEOrganization) e.Node.Tag).ParentOrg == Guid.Empty)
                    {
                        e.Node.Text = ((DEOrganization) e.Node.Tag).Name;
                    }
                    return;
                }
                if (e.Label.Trim() == "")
                {
                    MessageBox.Show("组织名称不允许为空！", "组织模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    this.tvwNavigator.LabelEdit = false;
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                DEOrganization tag = (DEOrganization) e.Node.Tag;
                string name = tag.Name;
                string str2 = e.Label.Trim();
                tag.Name = str2;
                PLOrganization organization2 = new PLOrganization();
                try
                {
                    organization2.Modify(ClientData.LogonUser.LogId, tag);
                    e.CancelEdit = true;
                    if (tag.ParentOrg.Equals(Guid.Empty))
                    {
                        e.Node.Text = tag.Name;
                    }
                    else
                    {
                        e.Node.Text = tag.Name;
                    }
                }
                catch (ResponsibilityException exception)
                {
                    MessageBox.Show(exception.Message, "组织模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    tag.Name = name;
                }
                catch
                {
                    MessageBox.Show("无法修改指定的组织名称！", "组织模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    tag.Name = name;
                }
            }
            if (e.Node.Tag is DEAdminRole)
            {
                if (e.Label == null)
                {
                    if (((DEAdminRole) e.Node.Tag).ParentAdminRole == Guid.Empty)
                    {
                        e.Node.Text = ((DEAdminRole) e.Node.Tag).Name;
                    }
                    return;
                }
                if (e.Label.Trim() == "")
                {
                    MessageBox.Show("管理角色名称不允许为空！", "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    this.tvwNavigator.LabelEdit = false;
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                DEAdminRole admRole = (DEAdminRole) e.Node.Tag;
                string str3 = admRole.Name;
                string str4 = e.Label.Trim();
                admRole.Name = str4;
                PLAdminRole role2 = new PLAdminRole();
                try
                {
                    role2.Modify(ClientData.LogonUser.LogId, admRole);
                    e.CancelEdit = true;
                    if (admRole.ParentAdminRole.Equals(Guid.Empty))
                    {
                        e.Node.Text = admRole.Name;
                    }
                    else
                    {
                        e.Node.Text = admRole.Name;
                    }
                }
                catch (ResponsibilityException exception2)
                {
                    MessageBox.Show(exception2.Message, "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    admRole.Name = str3;
                }
                catch
                {
                    MessageBox.Show("无法修改指定的管理角色名称！", "管理角色模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    e.CancelEdit = true;
                    admRole.Name = str3;
                }
            }
            if (e.Node.Tag is DERoleGroup)
            {
                DERoleGroup deRoleGroup = e.Node.Tag as DERoleGroup;
                string groupName = e.Node.Text.Trim();
                if (e.Label != null)
                {
                    groupName = e.Label.Trim();
                }
                if ((groupName.Length == 0) || (groupName.Length > 100))
                {
                    MessageBox.Show("角色分组名称长度必须在1到100之间！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.CancelEdit = true;
                    return;
                }
                if ((groupName.ToUpper() != e.Node.Text) && this.SameGroupExist(groupName))
                {
                    MessageBox.Show("已存在重名的角色分组！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.CancelEdit = true;
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                deRoleGroup.Name = groupName;
                PLRole role3 = new PLRole();
                try
                {
                    if (!role3.ReNameRoleGroup(deRoleGroup))
                    {
                        MessageBox.Show("角色分组重命名失败！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        deRoleGroup.Name = e.Node.Text;
                        e.CancelEdit = true;
                        return;
                    }
                    e.Node.Text = groupName;
                    e.CancelEdit = true;
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Remove(e.Node);
                    int index = this.FindRoleGroupPosition(deRoleGroup);
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Role.Nodes.Insert(index, e.Node);
                    this.tvwNavigator.SelectedNode = e.Node;
                }
                catch (Exception)
                {
                    MessageBox.Show("角色分组重命名失败！", "角色模型", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    deRoleGroup.Name = e.Node.Text;
                    e.CancelEdit = true;
                    return;
                }
            }
            if (!(e.Node.Tag is DELProcessClass))
            {
                this.tvwNavigator.LabelEdit = false;
                Cursor.Current = Cursors.Default;
                return;
            }
            e.Node.Text.Trim();
            string str6 = e.Node.Text.Trim();
            if (e.Label != null)
            {
                str6 = e.Label.Trim();
            }
            if ((str6.Length == 0) || (str6.Length > 0x20))
            {
                MessageBox.Show("过程分类名称长度必须为1到32之间！", ConstCommon.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.CancelEdit = true;
            }
            foreach (TreeNode node in this.TheProClsNodeList.Values)
            {
                DELProcessClass class2 = node.Tag as DELProcessClass;
                if ((str6.ToUpper() == class2.Name.ToUpper()) && (e.Node != node))
                {
                    MessageBox.Show("已存在重名的过程分类。", ConstCommon.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    e.CancelEdit = true;
                }
            }
            if (!e.CancelEdit)
            {
                e.Node.Text = str6;
                class3 = e.Node.Tag as DELProcessClass;
                if (this.IsNewProClass)
                {
                    class3.Name = str6;
                    this.IsNewProClass = false;
                    BPMProcessor processor = new BPMProcessor();
                    try
                    {
                        if (!processor.CreateOneProcessClass(class3))
                        {
                            if (!e.CancelEdit)
                            {
                                MessageBox.Show("过程分类创建失败！");
                            }
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Remove(e.Node);
                            return;
                        }
                        this.TheProClsNodeList.Add(class3.ID, e.Node);
                        goto Label_070F;
                    }
                    catch (Exception)
                    {
                        if (!e.CancelEdit)
                        {
                            MessageBox.Show("过程分类创建失败！");
                        }
                        Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Remove(e.Node);
                        return;
                    }
                }
                class3.Name = str6;
                BPMProcessor processor2 = new BPMProcessor();
                try
                {
                    if (!processor2.ModifyOneProcessClass(class3))
                    {
                        if (!e.CancelEdit)
                        {
                            MessageBox.Show("过程分类重命名失败！");
                        }
                        class3.Name = e.Node.Text;
                        e.CancelEdit = true;
                        return;
                    }
                }
                catch (Exception)
                {
                    if (!e.CancelEdit)
                    {
                        MessageBox.Show("过程分类重命名失败！");
                    }
                    class3.Name = e.Node.Text;
                    e.CancelEdit = true;
                    return;
                }
            }
            else
            {
                this.tvwNavigator.LabelEdit = true;
                e.Node.BeginEdit();
                return;
            }
        Label_070F:
            num2 = this.FindPosition(class3);
            if (e.Node.Index != num2)
            {
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Remove(e.Node);
                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(num2, e.Node);
            }
            this.ShowProcessPropertyList();
            if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
            {
                this.frmBPNavigator.UpdateListView();
            }
        }

        public void tvwNavigator_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FrmOrgUser orgWnd = null;
            FrmAdminRoleUser adminRoleWnd = null;
            FrmDataModel2 dataModelWnd = null;
            bool flag = false;
            int imageIndex = 0;
            if (e.Node.Tag is string)
            {
                FrmOperationDef current;
                IEnumerator enumerator;
                switch (e.Node.Tag.ToString())
                {
                    case "Organization":
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            PLOrganization organization = new PLOrganization();
                            if (organization.GetRootOrg() == null)
                            {
                                MessageBox.Show("组织模型尚未建立！", "组织模型");
                                return;
                            }
                            DEAdminRole adminRole = new PLAdminRole().GetAdminRole(this.deCurrentAdminRole.Oid);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Clear();
                            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
                            if (adminRole.ParentAdminRole == Guid.Empty)
                            {
                                TreeNode node = new TreeNode("无子组织", iconIndex, iconIndex);
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Add(node);
                                AdminRoleUL.FillOrgTree(node, iconIndex, false);
                            }
                            else
                            {
                                ArrayList allAdminRoleOrgs = new ArrayList();
                                allAdminRoleOrgs = GetAllAdminRoleOrgs(adminRole.Oid);
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Clear();
                                for (int i = 0; i < allAdminRoleOrgs.Count; i++)
                                {
                                    TreeNode node2 = new TreeNode("无子组织", iconIndex, iconIndex);
                                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Add(node2);
                                    node2.Tag = allAdminRoleOrgs[i];
                                    AdminRoleUL.FillSubOrgTree(node2, iconIndex, false);
                                }
                            }
                        }
                        catch (ResponsibilityException exception)
                        {
                            MessageBox.Show(exception.Message, "显示组织树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("获取组织树失败！", "显示组织树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        orgWnd = this.GetOrgWnd();
                        if (e.Node.Nodes.Count > 0)
                        {
                            orgWnd.lvwOrgUser.Items.Clear();
                            orgWnd.lvwSuborg.Items.Clear();
                        }
                        orgWnd.Show();
                        orgWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    case "User":
                        Cursor.Current = Cursors.WaitCursor;
                        FrmUserList2.ShowFrm(null);
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    case "Role":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmRoleList roleWnd = this.GetRoleWnd();
                        roleWnd.CurrentIsGroup = false;
                        roleWnd.CurrentRoleGroup = null;
                        roleWnd.RoleRefresh(null, null);
                        roleWnd.Show();
                        roleWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "AdminRole":
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            if (this.adminRoleFirstSelect)
                            {
                                TreeNode node3 = null;
                                imageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                                node3 = new TreeNode("无子管理角色", imageIndex, imageIndex) {
                                    Tag = "AdminRole"
                                };
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes.Add(node3);
                                this.adminRoleFirstSelect = false;
                            }
                            DEAdminRole role4 = new PLAdminRole().GetAdminRole(this.deCurrentAdminRole.Oid);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0].Tag = role4;
                            if (role4 == null)
                            {
                                if (MessageBox.Show("管理角色模型尚未建立！是否现在进行新建工作？", "管理角色模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    this.NewAdminRole(sender, e);
                                }
                            }
                            else
                            {
                                this.RefreshAdminRoleTree();
                            }
                        }
                        catch (ResponsibilityException exception2)
                        {
                            MessageBox.Show(exception2.Message, "显示管理角色树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("获取管理角色树失败！", "显示管理角色树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        adminRoleWnd = this.GetAdminRoleWnd();
                        if (e.Node.Nodes.Count > 0)
                        {
                            adminRoleWnd.lvwAdminRoleUser.Items.Clear();
                            adminRoleWnd.lvwSubadminRole.Items.Clear();
                        }
                        adminRoleWnd.Show();
                        adminRoleWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    case "DataModel":
                        Cursor.Current = Cursors.WaitCursor;
                        dataModelWnd = this.GetDataModelWnd();
                        if (!this.loaded)
                        {
                            this.RefreshClassTree();
                            this.RefreshRelations();
                            this.RefreshAllContexts();
                            this.loaded = true;
                        }
                        this.tvwNavigator.SelectedNode = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel;
                        Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_DataModel.Expand();
                        dataModelWnd.DisplayAllList();
                        dataModelWnd.Show();
                        dataModelWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    case "Class":
                        if (!this.loaded)
                        {
                            this.RefreshClassTree();
                            this.RefreshRelations();
                            this.RefreshAllContexts();
                            this.loaded = true;
                        }
                        dataModelWnd = this.GetDataModelWnd();
                        dataModelWnd.DisplayClasses();
                        dataModelWnd.Show();
                        dataModelWnd.Activate();
                        goto Label_0FFF;

                    case "Relation":
                        if (!this.loaded)
                        {
                            this.RefreshClassTree();
                            this.RefreshRelations();
                            this.RefreshAllContexts();
                            this.loaded = true;
                        }
                        dataModelWnd = this.GetDataModelWnd();
                        dataModelWnd.DisplayRelations();
                        dataModelWnd.Show();
                        dataModelWnd.Activate();
                        goto Label_0FFF;

                    case "Context":
                        if (!this.loaded)
                        {
                            this.RefreshClassTree();
                            this.RefreshRelations();
                            this.RefreshAllContexts();
                            this.loaded = true;
                        }
                        dataModelWnd = this.GetDataModelWnd();
                        dataModelWnd.DisplayContexts();
                        dataModelWnd.Show();
                        dataModelWnd.Activate();
                        goto Label_0FFF;

                    case "ViewSchema":
                        dataModelWnd = this.GetDataModelWnd();
                        dataModelWnd.DisplaySchemas();
                        dataModelWnd.Show();
                        dataModelWnd.Activate();
                        goto Label_0FFF;

                    case "ViewManage":
                    {
                        this.ShowAllViewModelsInTree();
                        FrmViewModel viewModelList = this.GetViewModelList();
                        this.RefreshAllViewModel(new object(), new EventArgs());
                        viewModelList.Show();
                        viewModelList.Activate();
                        goto Label_0FFF;
                    }
                    case "BizOperationDefinition":
                        Cursor.Current = Cursors.WaitCursor;
                        current = null;
                        enumerator = Application.OpenForms.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            if (enumerator.Current is FrmOperationDef)
                            {
                                current = enumerator.Current as FrmOperationDef;
                                break;
                            }
                        }
                        break;

                    case "File_Model":
                        Cursor.Current = Cursors.WaitCursor;
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    case "Tool":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmTool toolWnd = this.GetToolWnd();
                        toolWnd.Show();
                        toolWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "Browser":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmBrowser browserWnd = this.GetBrowserWnd();
                        browserWnd.Show();
                        browserWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "File":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmFile fileWnd = this.GetFileWnd();
                        fileWnd.Show();
                        fileWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "Folder":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmFolder folderWnd = this.GetFolderWnd();
                        folderWnd.Show();
                        folderWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "PPROOT":
                        flag = true;
                        goto Label_0FFF;

                    case "BusinessPro":
                        this.ShowBPNavigator();
                        this.openedDELProcessClass = Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM;
                        goto Label_0FFF;

                    case "RULE":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        IDictionaryEnumerator enumerator2 = this.HashMDiWindows.GetEnumerator();
                        bool flag2 = false;
                        bool flag3 = false;
                        while (enumerator2.MoveNext())
                        {
                            if (enumerator2.Value is FrmRuleList)
                            {
                                FrmRuleList list3 = (FrmRuleList) enumerator2.Value;
                                list3.Show();
                                list3.Activate();
                                flag2 = true;
                            }
                            if (enumerator2.Value is FrmCreateRule)
                            {
                                flag3 = true;
                            }
                        }
                        if (!flag2)
                        {
                            FrmRuleList list4 = new FrmRuleList(this.HashMDiWindows) {
                                MdiParent = this
                            };
                            TreeNode key = new TreeNode("规则列表");
                            this.HashMDiWindows.Add(key, list4);
                            list4.Show();
                            list4.Activate();
                        }
                        if (!flag3)
                        {
                            FrmCreateRule rule = new FrmCreateRule(this.HashMDiWindows, ConstUtility.RuleDefType.ExcelRule, 1) {
                                MdiParent = this
                            };
                            TreeNode node5 = new TreeNode("规则定义");
                            this.HashMDiWindows.Add(node5, rule);
                        }
                        goto Label_0FFF;
                    }
                    case "Addin":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmAddin addinWnd = this.GetAddinWnd();
                        addinWnd.Show();
                        addinWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "FormulaManagement":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmFormula formulaWnd = this.GetFormulaWnd();
                        formulaWnd.Show();
                        formulaWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "OUTPUTTEMPLATE":
                        if (this.allowConfigOutputTemplate)
                        {
                            this.SearchTemplates(null, null);
                            goto Label_0FFF;
                        }
                        MessageBox.Show("您没有定制业务对象输出模板的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;

                    case "PPSIGN_TEMPLATE":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmTemplateManager pPSignWnd = this.GetPPSignWnd();
                        pPSignWnd.Show();
                        pPSignWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;
                    }
                    case "DataViewManagement":
                        FrmDataViewList.ShowDataViewList(this);
                        goto Label_0FFF;

                    case "ChartManagement":
                        FrmChartList.ShowFrm(this);
                        goto Label_0FFF;

                    case "DataCheckManagement":
                        FrmRuleDetails.ShowDataCheckList(this);
                        goto Label_0FFF;

                    case "ClassFormulaManagement":
                        FrmClassFormulaList.ShowClassFormulaList(this);
                        goto Label_0FFF;

                    case "EcmsManagement":
                        if (!ConstCommon.FUNCTION_IORS)
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            FrmCodeManagement codeManageWnd = this.GetCodeManageWnd();
                            codeManageWnd.Show();
                            codeManageWnd.Activate();
                            Cursor.Current = Cursors.Default;
                        }
                        goto Label_0FFF;

                    case "CustomException":
                        Cursor.Current = Cursors.WaitCursor;
                        if (this.exceptionList == null)
                        {
                            this.exceptionList = new FrmCustomExceptionList();
                            this.exceptionList.MdiParent = this;
                        }
                        this.exceptionList.Show();
                        this.exceptionList.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0FFF;

                    default:
                        flag = true;
                        goto Label_0FFF;
                }
                enumerator.Reset();
                if (current == null)
                {
                    current = new FrmOperationDef {
                        MdiParent = this
                    };
                }
                current.Show();
                current.Activate();
                Cursor.Current = Cursors.Default;
            }
            else if (e.Node.Tag is DEOrganization)
            {
                orgWnd = this.GetOrgWnd();
                orgWnd.lvwOrgUser.Items.Clear();
                orgWnd.lvwSuborg.Items.Clear();
                orgWnd.ShowOrg(e.Node.Tag);
                orgWnd.Show();
                orgWnd.Activate();
            }
            else if (e.Node.Tag is DERoleGroup)
            {
                FrmRoleList list5 = this.GetRoleWnd();
                list5.CurrentIsGroup = true;
                list5.CurrentRoleGroup = e.Node.Tag as DERoleGroup;
                list5.RoleGroupRefresh((DERoleGroup) e.Node.Tag);
                list5.Show();
                list5.Activate();
                this.tvwNavigator.SelectedNode = e.Node;
            }
            else if (e.Node.Tag is DEAdminRole)
            {
                adminRoleWnd = this.GetAdminRoleWnd();
                adminRoleWnd.lvwAdminRoleUser.Items.Clear();
                adminRoleWnd.lvwSubadminRole.Items.Clear();
                adminRoleWnd.ShowAdminRole(e.Node.Tag);
                adminRoleWnd.Show();
                adminRoleWnd.Activate();
            }
            else if (e.Node.Tag is DEViewModel)
            {
                VMFrame openedViewFrameFrm = this.GetOpenedViewFrameFrm();
                if (openedViewFrameFrm != null)
                {
                    openedViewFrameFrm.RefreshViewModel();
                    openedViewFrameFrm.Show();
                    openedViewFrameFrm.Activate();
                }
                else
                {
                    flag = true;
                }
            }
            else if (e.Node.Tag is DEMetaClass)
            {
                TreeNode parent = e.Node.Parent;
                while (parent.Parent.Tag.ToString() != "EnterpriseModel")
                {
                    parent = parent.Parent;
                }
                if (parent.Tag.Equals("DataModel"))
                {
                    dataModelWnd = this.GetDataModelWnd();
                    dataModelWnd.DisplayClass(e.Node.Tag as DEMetaClass);
                    dataModelWnd.Show();
                    dataModelWnd.Activate();
                }
                if (parent.Tag.Equals("PPROOT"))
                {
                    if ((((DEMetaClass) e.Node.Tag).Name == "PPCARD") && !this.allowConfigPPCardTemplate)
                    {
                        MessageBox.Show("您没有定制卡片模板的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    if ((((DEMetaClass) e.Node.Tag).Name == "FORM") && !this.allowConfigFormTemplate)
                    {
                        MessageBox.Show("您没有定制表单打印模板的权限！", "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    this.SearchTemplates(null, null);
                }
                if (parent.Tag.Equals("PRINTROOT"))
                {
                    this.SearchTemplates(null, null);
                }
            }
            else if (e.Node.Tag is DEMetaRelation)
            {
                dataModelWnd = this.GetDataModelWnd();
                dataModelWnd.DisplayRelation(e.Node.Tag as DEMetaRelation);
                dataModelWnd.Show();
                dataModelWnd.Activate();
            }
            else if (e.Node.Tag is DEMetaContext)
            {
                DEMetaContext tag = e.Node.Tag as DEMetaContext;
                if (tag != null)
                {
                    FrmItemDisplayContext.OpenItemContext(ClientData.mainForm, tag, null);
                }
            }
            else if (e.Node.Tag is DEItemsViewSchema)
            {
                DEItemsViewSchema schema = e.Node.Tag as DEItemsViewSchema;
                if (schema != null)
                {
                    FrmItemDisplaySchema.OpenItemViewSchema(ClientData.mainForm, schema);
                }
            }
            else if (e.Node.Tag is DELProcessDefProperty)
            {
                if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    foreach (ListViewItem item in this.frmBPNavigator.lvwNavigater.Items)
                    {
                        item.Selected = false;
                    }
                    this.frmBPNavigator.SetSelectItem(e.Node, false);
                    Cursor.Current = Cursors.Default;
                    this.openedDELProcessClass = e.Node.Parent;
                    if (this.HashMDiWindows[this.tvwNavigator.SelectedNode] != null)
                    {
                        WFTEditor editor = (WFTEditor) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
                        if ((editor.viewPanel != null) && (editor.viewPanel.selectedShape != null))
                        {
                            editor.viewPanel.selectedShape.isSelected = false;
                            editor.viewPanel.selectedShape = null;
                            editor.viewPanel.Refresh();
                        }
                    }
                }
            }
            else if (e.Node.Tag is DELProcessClass)
            {
                this.ShowBPNavigator();
                this.openedDELProcessClass = e.Node;
            }
            else if (((e.Node.Tag is StartNode) || (e.Node.Tag is EndNode)) || ((e.Node.Tag is Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) || (e.Node.Tag is RouteNode)))
            {
                this.SetSelectedShapeInEditor();
            }
            else
            {
                flag = true;
            }
        Label_0FFF:
            if (this.frmNavigator == null)
            {
                this.frmNavigator = new FrmNavigator(this);
                this.frmNavigator.MdiParent = this;
            }
            this.frmNavigator.UpdateListView(e.Node);
            if (flag)
            {
                this.frmNavigator.Show();
                this.frmNavigator.Activate();
            }
            else if (this.frmNavigator != null)
            {
                this.frmNavigator.Hide();
            }
        }

        private void tvwNavigator_DoubleClick(object sender, EventArgs e)
        {
            FrmOrgUser orgWnd = null;
            FrmAdminRoleUser adminRoleWnd = null;
            bool flag = false;
            int imageIndex = 0;
            if (this.tvwNavigator.SelectedNode.Tag is string)
            {
                switch (this.tvwNavigator.SelectedNode.Tag.ToString())
                {
                    case "Organization":
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            PLOrganization organization = new PLOrganization();
                            if (organization.GetRootOrg() == null)
                            {
                                MessageBox.Show("组织模型尚未建立！", "组织模型");
                                return;
                            }
                            DEAdminRole adminRole = new PLAdminRole().GetAdminRole(this.deCurrentAdminRole.Oid);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Clear();
                            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ORG");
                            if (adminRole.ParentAdminRole == Guid.Empty)
                            {
                                TreeNode node = new TreeNode("无子组织", iconIndex, iconIndex);
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Add(node);
                                AdminRoleUL.FillOrgTree(node, iconIndex, false);
                            }
                            else
                            {
                                ArrayList allAdminRoleOrgs = new ArrayList();
                                allAdminRoleOrgs = GetAllAdminRoleOrgs(adminRole.Oid);
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Clear();
                                for (int i = 0; i < allAdminRoleOrgs.Count; i++)
                                {
                                    TreeNode node2 = new TreeNode("无子组织", iconIndex, iconIndex);
                                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Add(node2);
                                    node2.Tag = allAdminRoleOrgs[i];
                                    AdminRoleUL.FillSubOrgTree(node2, iconIndex, false);
                                }
                            }
                        }
                        catch (ResponsibilityException exception)
                        {
                            MessageBox.Show(exception.Message, "显示组织树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("获取组织树失败！", "显示组织树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        orgWnd = this.GetOrgWnd();
                        if (this.tvwNavigator.SelectedNode.Nodes.Count > 0)
                        {
                            orgWnd.lvwOrgUser.Items.Clear();
                            orgWnd.lvwSuborg.Items.Clear();
                        }
                        orgWnd.Show();
                        orgWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0630;

                    case "User":
                        Cursor.Current = Cursors.WaitCursor;
                        FrmUserList2.ShowFrm(null);
                        Cursor.Current = Cursors.Default;
                        goto Label_0630;

                    case "Role":
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        FrmRoleList roleWnd = this.GetRoleWnd();
                        roleWnd.CurrentIsGroup = false;
                        roleWnd.CurrentRoleGroup = null;
                        roleWnd.RoleRefresh(null, null);
                        roleWnd.Show();
                        roleWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0630;
                    }
                    case "AdminRole":
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            if (this.adminRoleFirstSelect)
                            {
                                TreeNode node3 = null;
                                imageIndex = ClientData.MyImageList.GetIconIndex("ICO_RSP_ROLE");
                                node3 = new TreeNode("无子管理角色", imageIndex, imageIndex) {
                                    Tag = "AdminRole"
                                };
                                Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes.Add(node3);
                                this.adminRoleFirstSelect = false;
                            }
                            DEAdminRole role4 = new PLAdminRole().GetAdminRole(this.deCurrentAdminRole.Oid);
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_AdminRole.Nodes[0].Tag = role4;
                            if (role4 == null)
                            {
                                if (MessageBox.Show("管理角色模型尚未建立！是否现在进行新建工作？", "管理角色模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    this.NewAdminRole(sender, e);
                                }
                            }
                            else
                            {
                                this.RefreshAdminRoleTree();
                            }
                        }
                        catch (ResponsibilityException exception2)
                        {
                            MessageBox.Show(exception2.Message, "显示管理角色树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("获取管理角色树失败！", "显示管理角色树", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        adminRoleWnd = this.GetAdminRoleWnd();
                        if (this.tvwNavigator.SelectedNode.Nodes.Count > 0)
                        {
                            adminRoleWnd.lvwAdminRoleUser.Items.Clear();
                            adminRoleWnd.lvwSubadminRole.Items.Clear();
                        }
                        adminRoleWnd.Show();
                        adminRoleWnd.Activate();
                        Cursor.Current = Cursors.Default;
                        goto Label_0630;
                }
                flag = true;
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DEOrganization)
            {
                orgWnd = this.GetOrgWnd();
                orgWnd.lvwOrgUser.Items.Clear();
                orgWnd.lvwSuborg.Items.Clear();
                orgWnd.ShowOrg(this.tvwNavigator.SelectedNode.Tag);
                orgWnd.Show();
                orgWnd.Activate();
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DERoleGroup)
            {
                FrmRoleList list3 = this.GetRoleWnd();
                list3.CurrentIsGroup = true;
                list3.CurrentRoleGroup = this.tvwNavigator.SelectedNode.Tag as DERoleGroup;
                list3.RoleGroupRefresh((DERoleGroup) this.tvwNavigator.SelectedNode.Tag);
                list3.Show();
                list3.Activate();
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DEAdminRole)
            {
                adminRoleWnd = this.GetAdminRoleWnd();
                adminRoleWnd.lvwAdminRoleUser.Items.Clear();
                adminRoleWnd.lvwSubadminRole.Items.Clear();
                adminRoleWnd.ShowAdminRole(this.tvwNavigator.SelectedNode.Tag);
                adminRoleWnd.Show();
                adminRoleWnd.Activate();
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DELProcessDefProperty)
            {
                Form form = (Form) this.HashMDiWindows[this.tvwNavigator.SelectedNode];
                if (form != null)
                {
                    form.Activate();
                }
                else
                {
                    this.OpenProcessTemplate();
                    if ((this.frmBPNavigator != null) && !this.frmBPNavigator.IsDisposed)
                    {
                        this.frmBPNavigator.SetSelectItem(this.tvwNavigator.SelectedNode, false);
                    }
                }
            }
            else if (this.tvwNavigator.SelectedNode.Tag is DELProcessClass)
            {
                this.ShowBPNavigator();
            }
            else if (((this.tvwNavigator.SelectedNode.Tag is StartNode) || (this.tvwNavigator.SelectedNode.Tag is EndNode)) || ((this.tvwNavigator.SelectedNode.Tag is Thyt.TiPLM.CLT.Admin.BPM.Modeler.TaskNode) || (this.tvwNavigator.SelectedNode.Tag is RouteNode)))
            {
                this.SetSelectedShapeInEditor();
            }
            else
            {
                flag = true;
            }
        Label_0630:
            if (flag)
            {
                if (base.ActiveMdiChild == null)
                {
                    if (this.frmNavigator == null)
                    {
                        this.frmNavigator = new FrmNavigator(this);
                        this.frmNavigator.MdiParent = this;
                    }
                    this.frmNavigator.UpdateListView(this.tvwNavigator.SelectedNode);
                    this.frmNavigator.Show();
                    this.frmNavigator.Activate();
                    return;
                }
            }
            else if (this.frmNavigator != null)
            {
                this.frmNavigator.Hide();
            }
        }

        protected void tvwNavigator_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            TreeNode theNode = this.FindTreeNodeAt(e.X, e.Y);
            if (theNode != null)
            {
                bool flag = false;
                bool flag2 = false;
                CLCopyData data = (CLCopyData) e.Data.GetData(typeof(CLCopyData));
                IEnumerator enumerator = data.GetEnumerator();
                bool flag3 = false;
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is TreeNode)
                    {
                        this.TheDragingNode = (TreeNode) enumerator.Current;
                        if (this.TheDragingNode.Parent == theNode)
                        {
                            break;
                        }
                        if (this.TheDragingNode == this.TheCuttingNode)
                        {
                            flag2 = true;
                        }
                        if (this.TheDragingNode.Tag is DELProcessDefProperty)
                        {
                            BPMProcessor processor = new BPMProcessor();
                            TreeNode parent = this.TheDragingNode.Parent;
                            DELProcessDefProperty tag = this.TheDragingNode.Tag as DELProcessDefProperty;
                            if (theNode == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                            {
                                DELProcessClass class2 = parent.Tag as DELProcessClass;
                                if (processor.MoveProcessBetweenClass(tag.ID, class2.ID, Guid.Empty))
                                {
                                    class2.RemoveProcess(tag.ID);
                                    parent.Nodes.Remove(this.TheDragingNode);
                                    int index = this.FindPosition(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM, tag);
                                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, this.TheDragingNode);
                                    flag = true;
                                }
                            }
                            else if (theNode.Tag is DELProcessClass)
                            {
                                DELProcessClass class3 = theNode.Tag as DELProcessClass;
                                if (parent == Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                                {
                                    if (processor.MoveProcessBetweenClass(tag.ID, Guid.Empty, class3.ID))
                                    {
                                        parent.Nodes.Remove(this.TheDragingNode);
                                        int num2 = this.FindPosition(theNode, tag);
                                        theNode.Nodes.Insert(num2, this.TheDragingNode);
                                        class3.AddProcess(tag.ID);
                                        flag = true;
                                    }
                                }
                                else
                                {
                                    DELProcessClass class4 = parent.Tag as DELProcessClass;
                                    if (processor.MoveProcessBetweenClass(tag.ID, class4.ID, class3.ID))
                                    {
                                        parent.Nodes.Remove(this.TheDragingNode);
                                        class4.RemoveProcess(tag.ID);
                                        int num3 = this.FindPosition(theNode, tag);
                                        theNode.Nodes.Insert(num3, this.TheDragingNode);
                                        class3.AddProcess(tag.ID);
                                        flag = true;
                                    }
                                }
                            }
                            if (flag2)
                            {
                                this.TheCuttingNode = null;
                            }
                            this.tvwNavigator.SelectedNode = this.TheDragingNode;
                            if (flag)
                            {
                                this.frmBPNavigator.UpdateListView(this.TheDragingNode.Parent);
                            }
                        }
                        else if (this.TheDragingNode.Tag is DEOrganization)
                        {
                            if (!(theNode.Tag is DEOrganization))
                            {
                                return;
                            }
                            DEOrganization org = this.TheDragingNode.Tag as DEOrganization;
                            DEOrganization organization2 = theNode.Tag as DEOrganization;
                            TreeNode node3 = this.TheDragingNode.Parent;
                            org.ParentOrg = organization2.Oid;
                            PLOrganization organization3 = new PLOrganization();
                            try
                            {
                                organization3.Modify(ClientData.LogonUser.LogId, org);
                                node3.Nodes.Remove(this.TheDragingNode);
                                theNode.Nodes.Add(this.TheDragingNode);
                                this.tvwNavigator.SelectedNode = theNode;
                            }
                            catch (Exception exception)
                            {
                                PrintException.Print(exception);
                            }
                        }
                    }
                    else if (enumerator.Current is DERole)
                    {
                        if ((!(theNode.Tag is string) || (theNode.Tag.ToString() != "Role")) && !(theNode.Tag is DERoleGroup))
                        {
                            return;
                        }
                        if (!flag3)
                        {
                            flag3 = true;
                        }
                        DERole current = enumerator.Current as DERole;
                        Guid empty = Guid.Empty;
                        Guid g = Guid.Empty;
                        if (data.Parent is DERoleGroup)
                        {
                            empty = ((DERoleGroup) data.Parent).Oid;
                        }
                        if (theNode.Tag is DERoleGroup)
                        {
                            g = ((DERoleGroup) theNode.Tag).Oid;
                        }
                        if (empty.Equals(g))
                        {
                            return;
                        }
                        PLRole role2 = new PLRole();
                        if (role2.MoveRoleBetweenGroup(current.Oid, empty, g))
                        {
                            this.GetRoleWnd().DeleteOrMoveCuttedRoles(current);
                        }
                    }
                }
                if (flag3)
                {
                    this.tvwNavigator.SelectedNode = theNode;
                }
            }
        }

        protected void tvwNavigator_DragEnter(object sender, DragEventArgs e)
        {
            this.tvwNavigator.AllowDrop = true;
            e.Effect = DragDropEffects.Move;
            TreeNode node = this.FindTreeNodeAt(e.X, e.Y);
            if (node == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                IEnumerator enumerator = ((CLCopyData) e.Data.GetData(typeof(CLCopyData))).GetEnumerator();
                if (enumerator.MoveNext())
                {
                    if (enumerator.Current is TreeNode)
                    {
                        this.TheDragingNode = (TreeNode) enumerator.Current;
                    }
                    else if (enumerator.Current is DERole)
                    {
                        e.Effect = DragDropEffects.Move;
                        return;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
                if (this.TheDragingNode.Parent == node)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    if (node != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass tag = node.Tag as DELProcessClass;
                    }
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        protected void tvwNavigator_DragOver(object sender, DragEventArgs e)
        {
            TreeNode node = this.FindTreeNodeAt(e.X, e.Y);
            if (node != null)
            {
                IEnumerator enumerator = ((CLCopyData) e.Data.GetData(typeof(CLCopyData))).GetEnumerator();
                if (enumerator.MoveNext() && (enumerator.Current is DERole))
                {
                    e.Effect = DragDropEffects.Move;
                }
                else if ((this.TheDragingNode != null) && (this.TheDragingNode.Parent != node))
                {
                    if (node != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass tag = node.Tag as DELProcessClass;
                    }
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        protected void tvwNavigator_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode item = e.Item as TreeNode;
            bool flag = (item.Tag is DELProcessDefProperty) && this.allowModifyProcessManagement;
            if (flag)
            {
                CLCopyData data = new CLCopyData();
                data.Add(item);
                base.DoDragDrop(data, DragDropEffects.Move);
                if ((this.frmBPNavigator != null) && flag)
                {
                    this.frmBPNavigator.lvwNavigater.AllowDrop = true;
                }
            }
            if (((item.Tag is DEOrganization) && (item != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization)) && ((Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes.Count > 0) && (item != Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Organization.Nodes[0])))
            {
                CLCopyData data2 = new CLCopyData();
                data2.Add(item);
                base.DoDragDrop(data2, DragDropEffects.Move);
            }
        }

        private void tvwNavigator_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode nodeAt = this.tvwNavigator.GetNodeAt(e.X, e.Y);
                Point pos = new Point(e.X + 10, e.Y);
                if (nodeAt != null)
                {
                    this.tvwNavigator.SelectedNode = nodeAt;
                    Cursor.Current = Cursors.WaitCursor;
                    if (e.Button == MouseButtons.Right)
                    {
                        string menuName = null;
                        if (nodeAt.Tag is string)
                        {
                            if (((string) nodeAt.Tag).StartsWith("LCD"))
                            {
                                menuName = "LCD";
                            }
                            else
                            {
                                menuName = nodeAt.Tag.ToString();
                            }
                        }
                        else if (nodeAt.Tag is DEMetaClass)
                        {
                            TreeNode parent = nodeAt;
                            while (((parent != null) && (parent.Parent != null)) && (parent.Parent.Tag.ToString() != "EnterpriseModel"))
                            {
                                parent = parent.Parent;
                            }
                            if (parent.Tag.Equals("PPROOT"))
                            {
                                menuName = "PPCARD";
                                if (((DEMetaClass) nodeAt.Tag).Name == "PPCARD")
                                {
                                    menuName = "PPROOT";
                                }
                            }
                            else if (parent.Tag.Equals("RPTROOT"))
                            {
                                menuName = "RPTROOT";
                            }
                            else if (parent.Tag.Equals("PRINTROOT"))
                            {
                                menuName = "PRINT";
                            }
                            else
                            {
                                menuName = nodeAt.Tag.GetType().ToString();
                            }
                        }
                        else
                        {
                            menuName = nodeAt.Tag.GetType().ToString();
                        }
                        this.BuildMenu(menuName, nodeAt);
                        if (this.cmuCommon.MenuItems.Count > 0)
                        {
                            this.cmuCommon.Show(this.tvwNavigator, pos);
                        }
                    }
                }
            }
        }

        public void UnactiveViewModel(object sender, EventArgs e)
        {
            PLViewModel model = new PLViewModel();
            bool flag = false;
            try
            {
                flag = model.HasUsedViewModel(((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Oid);
            }
            catch (ViewException exception)
            {
                MessageBox.Show(exception.Message, "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            catch
            {
                MessageBox.Show("判断视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”是否已经与业务对象绑定失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (!flag)
            {
                if (MessageBox.Show("取消激活后，该视图模型将暂时无法绑定。您是否要继续？", "取消激活", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }
            else if (MessageBox.Show("已存在零部件与视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”绑定，且取消激活后，该视图模型将暂时无法绑定。您是否要继续？", "视图模型", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }
            try
            {
                DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                model.ActiveViewModelOrNot(tag.Oid, false);
                tag.IsActive = 'U';
                this.tvwNavigator.SelectedNode.Tag = tag;
                IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key is DEViewModel)
                    {
                        DEViewModel key = (DEViewModel) enumerator.Key;
                        DEViewModel model4 = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                        if (key.Oid.Equals(model4.Oid))
                        {
                            VMFrame frame = (VMFrame) enumerator.Value;
                            frame.Show();
                            frame.Activate();
                            frame.Tag = tag;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("取消激活视图模型“" + ((DEViewModel) this.tvwNavigator.SelectedNode.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        private void UpdateAllViews(object objs)
        {
            IProgressCallback callback = (objs as ArrayList)[0] as IProgressCallback;
            try
            {
                callback.Begin(0, 100);
                callback.SetText("正在重新建立业务对象类相应的数据库视图……");
                try
                {
                    new PLDataModel().UpdateAllViews();
                    MessageBox.Show("完成业务对象视图的重建。", "数据模型管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "数据模型管理");
                }
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        public static void UpdateBackground(object sender, EventArgs e)
        {
            Thyt.TiPLM.CLT.TiModeler.FrmMain main = (Thyt.TiPLM.CLT.TiModeler.FrmMain) sender;
            if (IsCommonCtrl6())
            {
                main.BackColor = SystemColors.Control;
            }
            else
            {
                main.BackColor = ColorUtil.VSNetControlColor;
            }
        }

        private void UpdateNavigator(bool isNewClass, bool isNewRelation, DEMetaClass cls, DEMetaRelation rlt)
        {
            if (cls != null)
            {
                if (isNewClass)
                {
                    int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
                    TreeNode node = new TreeNode(cls.Label, iconIndex, iconIndex) {
                        Tag = cls
                    };
                    if (cls.Parent.Equals(Guid.Empty))
                    {
                        Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class.Nodes.Add(node);
                    }
                    else
                    {
                        this.FindRightNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class, cls.Parent).Nodes.Add(node);
                    }
                    this.tvwNavigator.SelectedNode = node;
                }
                else
                {
                    this.FindRightNode(Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Class, cls.Oid).Text = cls.Label;
                }
            }
            if (rlt != null)
            {
                if (isNewRelation)
                {
                    int imageIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_RELATION");
                    TreeNode node4 = new TreeNode(rlt.Label, imageIndex, imageIndex) {
                        Tag = rlt
                    };
                    Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation.Nodes.Add(node4);
                    this.tvwNavigator.SelectedNode = node4;
                }
                else
                {
                    for (int i = 0; i < Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation.Nodes.Count; i++)
                    {
                        DEMetaRelation tag = (DEMetaRelation) Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation.Nodes[i].Tag;
                        if (rlt.Oid.Equals(tag.Oid))
                        {
                            Thyt.TiPLM.CLT.TiModeler.TagForTiModeler.TreeNode_Relation.Nodes[i].Text = rlt.Label;
                            return;
                        }
                    }
                }
            }
        }

        private void UserRefresh(object sender, EventArgs e)
        {
            FrmUserList2.ShowFrm(null);
        }

        public void ViewModelProperty(object sender, EventArgs e)
        {
            FrmViewModelProperty property = new FrmViewModelProperty(true, (DEViewModel) this.tvwNavigator.SelectedNode.Tag);
            if (property.ShowDialog() == DialogResult.OK)
            {
                this.tvwNavigator.SelectedNode.Tag = property.theVM;
                this.tvwNavigator.SelectedNode.Text = property.theVM.Name;
                IDictionaryEnumerator enumerator = this.HashMDiWindows.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Key is DEViewModel)
                    {
                        DEViewModel key = (DEViewModel) enumerator.Key;
                        DEViewModel tag = (DEViewModel) this.tvwNavigator.SelectedNode.Tag;
                        if (key.Oid.Equals(tag.Oid))
                        {
                            VMFrame frame = (VMFrame) enumerator.Value;
                            frame.Show();
                            frame.Activate();
                            frame.Tag = property.theVM;
                            frame.Text = property.theVM.Name;
                            return;
                        }
                    }
                }
            }
        }

        public StatusBarPanel SbpMain
        {
            get{
             return  TiModelerUIContainer.sbpMain;
            }
                set
            {
                TiModelerUIContainer.sbpMain = value;
            }
        }
    }
}

