
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Environment;
namespace Thyt.TiPLM.CLT.TiModeler.FileModel {
    public partial class FrmBrowser : Form
    {
        private MenuItemEx cmiDel;
        private MenuItemEx cmiEdit;
        
        private ImageList ilsB;
        private bool isItem = true;

        public FrmBrowser(FrmMain main)
        {
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_ENV_BROWER");
            this.frmMain = main;
        }

        public void BuildMenu()
        {
            this.cmuBrowser.MenuItems.Clear();
            if (this.isItem)
            {
                this.cmiEdit = new MenuItemEx("&Property", "属性(&P)", null, null);
                this.cmiEdit.DefaultItem = true;
                this.cmiEdit.ImageList = ClientData.MyImageList.imageList;
                this.cmiEdit.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                this.cmiEdit.Click += new EventHandler(this.cmiEdit_Click);
                this.cmuBrowser.MenuItems.Add(this.cmiEdit);
                if (!((DEBrowser) this.lvwBrowser.SelectedItems[0].Tag).IsSystem)
                {
                    MenuItemEx item = new MenuItemEx("-", "-", null, null);
                    this.cmuBrowser.MenuItems.Add(item);
                    this.cmiDel = new MenuItemEx("&Delete", "删除(&D)", null, null);
                    this.cmiDel.ImageList = ClientData.MyImageList.imageList;
                    this.cmiDel.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                    this.cmiDel.Click += new EventHandler(this.cmiDel_Click);
                    this.cmuBrowser.MenuItems.Add(this.cmiDel);
                }
            }
            else
            {
                MenuItemEx ex2 = new MenuItemEx("New &Browser", "新增浏览器(&B)", null, null);
                ex2.Click += new EventHandler(this.frmMain.NewBrowser);
                this.cmuBrowser.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("New &Editor", "新增编辑器(&E)", null, null);
                ex2.Click += new EventHandler(this.frmMain.NewEditor);
                this.cmuBrowser.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("-", "-", null, null);
                this.cmuBrowser.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("Browser Re&Fresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                ex2.Click += new EventHandler(this.frmMain.BrowserRefresh);
                this.cmuBrowser.MenuItems.Add(ex2);
            }
        }

        private void cmiDel_Click(object sender, EventArgs e)
        {
            DEBrowser tag = (DEBrowser) this.lvwBrowser.SelectedItems[0].Tag;
            if ((tag.Option & 8) == 8)
            {
                if (MessageBox.Show("是否删除选中浏览器？", "删除浏览器", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    PLBrowser browser2 = new PLBrowser();
                    try
                    {
                        tag.Creator = ClientData.LogonUser.LogId;
                        if (browser2.Delete(tag))
                        {
                            this.lvwBrowser.SelectedItems[0].Remove();
                            this.frmMain.RefreshBrowser();
                            this.frmMain.FileTypeRefresh(sender, e);
                        }
                    }
                    catch (EnvironmentException exception)
                    {
                        MessageBox.Show(exception.Message, "删除浏览器", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    catch
                    {
                        MessageBox.Show("删除浏览器失败！", "删除浏览器", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            else if (((tag.Option & 0x10) == 0x10) && (MessageBox.Show("是否删除选中编辑器？", "删除编辑器", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
            {
                PLBrowser browser3 = new PLBrowser();
                try
                {
                    tag.Creator = ClientData.LogonUser.LogId;
                    if (browser3.Delete(tag))
                    {
                        this.lvwBrowser.SelectedItems[0].Remove();
                        this.frmMain.RefreshBrowser();
                        this.frmMain.FileTypeRefresh(sender, e);
                    }
                }
                catch (EnvironmentException exception2)
                {
                    MessageBox.Show(exception2.Message, "删除编辑器", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                catch
                {
                    MessageBox.Show("删除编辑器失败！", "删除编辑器", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void cmiEdit_Click(object sender, EventArgs e)
        {
            DEBrowser tag = (DEBrowser) this.lvwBrowser.SelectedItems[0].Tag;
            FrmSetBrowser browser2 = new FrmSetBrowser(tag, true);
            if (browser2.ShowDialog() == DialogResult.OK)
            {
                DEBrowser deB = new DEBrowser();
                deB = browser2.deB;
                this.lvwBrowser.SelectedItems[0].SubItems[0].Text = deB.Name;
                if ((deB.Option & 1) == 1)
                {
                    foreach (ListViewItem item in this.lvwBrowser.Items)
                    {
                        item.SubItems[1].Text = "";
                    }
                    this.lvwBrowser.SelectedItems[0].SubItems[1].Text = "是";
                }
                else
                {
                    this.lvwBrowser.SelectedItems[0].SubItems[1].Text = "";
                }
                this.lvwBrowser.SelectedItems[0].SubItems[2].Text = deB.Creator;
                this.lvwBrowser.SelectedItems[0].SubItems[3].Text = deB.CreatTime.ToString("yyyy-MM-dd");
                this.lvwBrowser.SelectedItems[0].SubItems[4].Text = deB.Description;
                this.lvwBrowser.SelectedItems[0].Tag = deB;
                this.frmMain.RefreshBrowser();
                this.frmMain.FileTypeRefresh(sender, e);
            }
        }

        public void CreatList()
        {
            PLBrowser browser = new PLBrowser();
            ArrayList allBrowsers = null;
            ArrayList allEditors = null;
            try
            {
                allBrowsers = browser.GetAllBrowsers();
                allEditors = browser.GetAllEditors();
            }
            catch (EnvironmentException exception)
            {
                MessageBox.Show(exception.Message, "编辑浏览器管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            catch
            {
                MessageBox.Show("显示编辑器、浏览器信息失败！", "编辑浏览器管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            for (int i = 0; i < allBrowsers.Count; i++)
            {
                DEBrowser browser2 = (DEBrowser) allBrowsers[i];
                string str = "";
                string str2 = "";
                if ((browser2.Option & 1) == 1)
                {
                    str = "缺省浏览器";
                }
                if ((browser2.Option & 4) == 4)
                {
                    str2 = "系统浏览器";
                }
                else
                {
                    str2 = "定制浏览器";
                }
                string[] items = new string[] { browser2.Name, str, str2, browser2.Creator, browser2.CreatTime.ToString("yyyy-MM-dd"), browser2.Description };
                ListViewItem item = new ListViewItem(items) {
                    Tag = browser2,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_BROWER")
                };
                this.lvwBrowser.Items.Add(item);
            }
            for (int j = 0; j < allEditors.Count; j++)
            {
                DEBrowser browser3 = (DEBrowser) allEditors[j];
                string str3 = "";
                string str4 = "";
                if ((browser3.Option & 1) == 1)
                {
                    str3 = "缺省编辑器";
                }
                if ((browser3.Option & 0x10) == 0x10)
                {
                    str4 = "定制编辑器";
                }
                string[] strArray2 = new string[] { browser3.Name, str3, str4, browser3.Creator, browser3.CreatTime.ToString("yyyy-MM-dd"), browser3.Description };
                ListViewItem item2 = new ListViewItem(strArray2) {
                    Tag = browser3,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_BROWER")
                };
                this.lvwBrowser.Items.Add(item2);
            }
        }

        private void FrmBrower_Closing(object sender, CancelEventArgs e)
        {
            this.frmMain.RemoveWnd(this);
        }

        private void FrmBrowser_Load(object sender, EventArgs e)
        {
            this.lvwBrowser.SmallImageList = ClientData.MyImageList.imageList;
            this.lvwBrowser.Columns.Add("名称", 200, HorizontalAlignment.Left);
            this.lvwBrowser.Columns.Add("缺省", 150, HorizontalAlignment.Left);
            this.lvwBrowser.Columns.Add("类型", 150, HorizontalAlignment.Left);
            this.lvwBrowser.Columns.Add("创建人", 100, HorizontalAlignment.Left);
            this.lvwBrowser.Columns.Add("创建时间", 150, HorizontalAlignment.Left);
            this.lvwBrowser.Columns.Add("描述", 300, HorizontalAlignment.Left);
            this.CreatList();
        }

        private void lvwBrowser_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem itemAt = this.lvwBrowser.GetItemAt(e.X, e.Y);
                this.isItem = itemAt != null;
                if (itemAt != null)
                {
                    itemAt.Selected = true;
                }
                this.BuildMenu();
                this.cmuBrowser.Show(this.lvwBrowser, new Point(e.X + 5, e.Y));
            }
        }
    }
}

