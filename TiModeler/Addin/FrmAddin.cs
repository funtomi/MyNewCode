
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.DEL.Addin;
    using Thyt.TiPLM.PLL.Addin;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Addin;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.ObjectPerm;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler.Addin {
    public partial class FrmAddin : Form
    {
        private MenuItemEx cmiDel;
        private MenuItemEx cmiEdit;
        public PLM_FormClosed FormClosed;
        private CompareAddinClass IC;
        private bool IsAdminRole;
        private bool isItem;
        private ArrayList modules;

        public FrmAddin()
        {
            this.isItem = true;
            this.InitializeComponent();
        }

        public FrmAddin(FrmMain main)
        {
            this.isItem = true;
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_ENV_TOOL");
            this.frmMain = main;
            this.lvwAddin.SmallImageList = ClientData.MyImageList.imageList;
            this.lvwAddin.Columns.AddRange(UIAddinHelper.Instance.AddinListColumns);
        }

        public void BuildMenu()
        {
            this.cmuAddin.MenuItems.Clear();
            MenuItemEx item = null;
            if (this.isItem)
            {
                if ((this.lvwAddin.SelectedItems.Count == 1) && (this.lvwAddin.SelectedItems[0].Tag is DEAddinReg))
                {
                    this.cmiEdit = new MenuItemEx("Addin Property", "属性", null, null);
                    this.cmiEdit.DefaultItem = true;
                    this.cmiEdit.ImageList = ClientData.MyImageList.imageList;
                    this.cmiEdit.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                    this.cmiEdit.Click += new EventHandler(this.OnShowProperty);
                    this.cmuAddin.MenuItems.Add(this.cmiEdit);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuAddin.MenuItems.Add(item);
                    bool flag = false;
                    if (!(this.lvwAddin.SelectedItems[0].Tag as DEAddinReg).IsActivated)
                    {
                        item = new MenuItemEx("Addin Valid", "立即生效", null, null) {
                            ImageList = ClientData.MyImageList.imageList
                        };
                        item.Click += new EventHandler(this.OnValid);
                        this.cmuAddin.MenuItems.Add(item);
                        flag = true;
                    }
                    int num = ((DEAddinReg) this.lvwAddin.SelectedItems[0].Tag).Module;
                    DEModule addinModule = this.GetAddinModule(num);
                    if ((addinModule != null) && addinModule.IsNeedGrant)
                    {
                        item = new MenuItemEx("Authorize", "授权", null, null);
                        item.Click += new EventHandler(this.OnAuthorize);
                        this.cmuAddin.MenuItems.Add(item);
                        flag = true;
                    }
                    if (flag)
                    {
                        item = new MenuItemEx("-", "-", null, null);
                        this.cmuAddin.MenuItems.Add(item);
                    }
                }
                item = new MenuItemEx("&Addin New", "新增插件(&N)", null, null);
                item.Click += new EventHandler(this.OnNewAddin);
                this.cmuAddin.MenuItems.Add(item);
                item = new MenuItemEx("Addin &Export", "导出插件(&E)", null, null);
                item.Click += new EventHandler(this.OnExportAddin);
                this.cmuAddin.MenuItems.Add(item);
                item = new MenuItemEx("Addin &Import", "导入插件(&I)", null, null);
                item.Click += new EventHandler(this.OnImport);
                this.cmuAddin.MenuItems.Add(item);
                item = new MenuItemEx("-", "-", null, null);
                this.cmuAddin.MenuItems.Add(item);
                this.cmiDel = new MenuItemEx("Addin Delete", "删除插件", null, null);
                this.cmiDel.ImageList = ClientData.MyImageList.imageList;
                this.cmiDel.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                this.cmiDel.Click += new EventHandler(this.OnDelete);
                this.cmuAddin.MenuItems.Add(this.cmiDel);
            }
            else
            {
                item = new MenuItemEx("Addin &New", "新增插件(&N)", null, null);
                item.Click += new EventHandler(this.OnNewAddin);
                this.cmuAddin.MenuItems.Add(item);
                item = new MenuItemEx("-", "-", null, null);
                this.cmuAddin.MenuItems.Add(item);
                item = new MenuItemEx("Addin &Import", "导入插件(&I)", null, null);
                item.Click += new EventHandler(this.OnImport);
                this.cmuAddin.MenuItems.Add(item);
            }
            item = new MenuItemEx("Addin Re&Fresh", "刷新(&F)", null, null) {
                ImageList = ClientData.MyImageList.imageList,
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
            };
            item.Click += new EventHandler(this.OnRefresh);
            this.cmuAddin.MenuItems.Add(item);
        }

        public void DisplayList(bool complete)
        {
            ArrayList list = new ArrayList(AddinFramework.Instance.GetAllAddins());
            list.Sort(this.IC);
            DEAddinReg[] addins = (DEAddinReg[]) list.ToArray(typeof(DEAddinReg));
            UIAddinHelper.Instance.FillAddinList(this.lvwAddin, addins, View.Details);
        }
         

        private void FrmAddin_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                this.FormClosed(this);
            }
            catch
            {
            }
        }

        private void FrmAddin_Load(object sender, EventArgs e)
        {
            this.IC = new CompareAddinClass();
            this.modules = PLAddins.RemoteAgent.GetAllModules(ClientData.LogonUser.Oid);
            this.DisplayList(true);
            PLAdminRole role = new PLAdminRole();
            if (role.GetAdminRoleByUserId(ClientData.LogonUser.Oid) == null)
            {
                this.IsAdminRole = false;
            }
            else
            {
                this.IsAdminRole = true;
            }
        }

        private DEModule GetAddinModule(int i_mod)
        {
            DEModule module = null;
            for (int i = 0; i < this.modules.Count; i++)
            {
                module = this.modules[i] as DEModule;
                if (module.ModuleId == i_mod)
                {
                    return module;
                }
            }
            return null;
        }

        private void lvwAddin_ItemActivate(object sender, EventArgs e)
        {
            if (this.lvwAddin.SelectedItems.Count > 0)
            {
                this.OnShowProperty(sender, e);
            }
        }

        private void lvwAddin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem itemAt = this.lvwAddin.GetItemAt(e.X, e.Y);
                this.isItem = itemAt != null;
                if (itemAt != null)
                {
                    itemAt.Selected = true;
                }
                this.BuildMenu();
                this.cmuAddin.Show(this.lvwAddin, new Point(e.X, e.Y));
            }
        }

        private void OnAuthorize(object sender, EventArgs e)
        {
            if (!this.IsAdminRole)
            {
                MessageBox.Show("你不是管理角色成员，不能进行插件授权工作！");
            }
            else
            {
                DEAddinReg tag = this.lvwAddin.SelectedItems[0].Tag as DEAddinReg;
                new FrmFuncObjGrant { 
                    objectName = tag.Name,
                    className = "ADDIN",
                    objectOid = tag.Oid
                }.ShowDialog();
            }
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if ((((this.lvwAddin.SelectedItems.Count != 0) && (this.lvwAddin.SelectedItems[0].Tag != null)) && (this.lvwAddin.SelectedItems[0].Tag is DEAddinReg)) && (MessageBox.Show("您确认要删除插件吗？", "插件管理", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel))
            {
                ArrayList list = new ArrayList();
                for (int i = 0; i < this.lvwAddin.SelectedItems.Count; i++)
                {
                    list.Add(this.lvwAddin.SelectedItems[i]);
                }
                for (int j = 0; j < list.Count; j++)
                {
                    ListViewItem item = list[j] as ListViewItem;
                    try
                    {
                        if (item.Tag is DEAddinReg)
                        {
                            AddinRegistration.Instance.DeleteAddin(((DEAddinReg) item.Tag).Oid);
                            this.lvwAddin.Items.Remove(item);
                        }
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception, "插件管理");
                        break;
                    }
                }
            }
        }

        private void OnExportAddin(object sender, EventArgs e)
        {
            if ((this.lvwAddin.SelectedItems.Count != 0) && (this.lvwAddin.SelectedItems[0].Tag != null))
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog {
                    Description = "导出指定插件"
                };
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    int num = 0;
                    try
                    {
                        for (int i = 0; i < this.lvwAddin.SelectedItems.Count; i++)
                        {
                            DEAddinReg tag = this.lvwAddin.SelectedItems[i].Tag as DEAddinReg;
                            if (tag == null)
                            {
                                continue;
                            }
                            string name = tag.Name;
                            if (!Directory.Exists(selectedPath + @"\" + name))
                            {
                                goto Label_00DB;
                            }
                            if (num == 0)
                            {
                                switch (MessageBox.Show("指定目录插件已存在，是否覆盖？", "发现同名插件", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Cancel:
                                        MessageBox.Show("插件导出取消");
                                        return;

                                    case DialogResult.Yes:
                                        num = 1;
                                        goto Label_00CC;
                                }
                                num = 2;
                            }
                        Label_00CC:
                            if (num == 1)
                            {
                                UIAddinUtil.ExportAddin(tag, selectedPath);
                                continue;
                            }
                        Label_00DB:
                            UIAddinUtil.ExportAddin(tag, selectedPath);
                        }
                    }
                    catch (Exception)
                    {
                        return;
                    }
                    MessageBox.Show("插件导出完毕");
                }
            }
        }

        private void OnImport(object sender, EventArgs e)
        {
            new FrmImpExpAddin().ShowDialog();
            this.DisplayList(false);
        }

        private void OnNewAddin(object sender, EventArgs e)
        {
            if (FrmOpenFile.CreateAddin() != null)
            {
                this.DisplayList(false);
            }
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            this.DisplayList(false);
        }

        private void OnShowProperty(object sender, EventArgs e)
        {
            if ((this.lvwAddin.SelectedItems.Count != 0) && (this.lvwAddin.SelectedItems[0].Tag != null))
            {
                DEAddinReg tag = this.lvwAddin.SelectedItems[0].Tag as DEAddinReg;
                if ((tag != null) && FrmOpenFile.ModifyAddin(tag))
                {
                    this.DisplayList(true);
                    foreach (ListViewItem item in this.lvwAddin.Items)
                    {
                        if ((item.Tag as DEAddinReg).Oid == tag.Oid)
                        {
                            item.Selected = true;
                            this.lvwAddin.EnsureVisible(item.Index);
                            break;
                        }
                    }
                }
            }
        }

        private void OnValid(object sender, EventArgs e)
        {
            if (((this.lvwAddin.SelectedItems.Count != 0) && (this.lvwAddin.SelectedItems[0].Tag != null)) && (this.lvwAddin.SelectedItems[0].Tag is DEAddinReg))
            {
                DEAddinReg tag = (DEAddinReg) this.lvwAddin.SelectedItems[0].Tag;
                if (tag.IsDeployed)
                {
                    DEAddinReg reg2 = tag.Clone() as DEAddinReg;
                    reg2.IsActivated = !reg2.IsActivated;
                    reg2 = AddinRegistration.Instance.UpdateAddinStatus(reg2.Oid, reg2.Status);
                    reg2.Updator = ClientData.LogonUser.Oid;
                    reg2.UpdateTime = DateTime.Now;
                    if (reg2 == null)
                    {
                        reg2 = tag;
                    }
                    this.lvwAddin.SelectedItems[0].Tag = reg2;
                    UIAddinHelper.Instance.UpdateAddinListViewItem(this.lvwAddin.SelectedItems[0], false);
                }
                else
                {
                    MessageBox.Show("插件在服务器端还未部署，不允许激活", "插件框架", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        internal class CompareAddinClass : IComparer
        {
            public int Compare(object x, object y)
            {
                string name = ((DEAddinReg) x).Name;
                string strB = ((DEAddinReg) y).Name;
                return string.Compare(name, strB);
            }
        }
    }
}

