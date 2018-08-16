
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
    public partial class FrmFile : Form
    {
        private ArrayList allEditor = new ArrayList();
        private ArrayList allTools = new ArrayList();
        private MenuItemEx cmiDel;
        private MenuItemEx cmiEdit;
        private MenuItemEx cmiRefreshFiles;
        private MenuItemEx cmiRelat;
        
        private bool isItem = true;

        public FrmFile(FrmMain main)
        {
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_ENV_FILETYPE");
            this.frmMain = main;
            this.GetBaseToolAndEdit();
        }

        public void BuildMenu()
        {
            this.cmuFile.MenuItems.Clear();
            if (this.isItem)
            {
                this.cmiRelat = new MenuItemEx("Relation", "与数据类型关联", null, null);
                this.cmiRelat.Click += new EventHandler(this.cmiRelat_Click);
                this.cmuFile.MenuItems.Add(this.cmiRelat);
                MenuItemEx item = new MenuItemEx("-", "-", null, null);
                this.cmuFile.MenuItems.Add(item);
                this.cmiEdit = new MenuItemEx("Edit", "属性", null, null);
                this.cmiEdit.ImageList = ClientData.MyImageList.imageList;
                this.cmiEdit.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                this.cmiEdit.Click += new EventHandler(this.cmiEdit_Click);
                this.cmuFile.MenuItems.Add(this.cmiEdit);
                if (this.lvwFile.SelectedItems.Count > 0)
                {
                    this.cmiRefreshFiles = new MenuItemEx("Refresh Files", "更新业务对象文件类型", null, null);
                    this.cmiRefreshFiles.ImageList = ClientData.MyImageList.imageList;
                    this.cmiRefreshFiles.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
                    this.cmiRefreshFiles.Click += new EventHandler(this.cmiRefreshFiles_Click);
                    this.cmuFile.MenuItems.Add(this.cmiRefreshFiles);
                }
                item = new MenuItemEx("-", "-", null, null);
                this.cmuFile.MenuItems.Add(item);
                this.cmiDel = new MenuItemEx("Delete", "删除", null, null);
                this.cmiDel.ImageList = ClientData.MyImageList.imageList;
                this.cmiDel.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                this.cmiDel.Click += new EventHandler(this.cmiDel_Click);
                this.cmuFile.MenuItems.Add(this.cmiDel);
            }
            else
            {
                MenuItemEx ex2 = new MenuItemEx("&New Browser", "新建文件类型(&N)", null, null);
                ex2.Click += new EventHandler(this.NewFileType);
                this.cmuFile.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("-", "-", null, null);
                this.cmuFile.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("Refresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                ex2.Click += new EventHandler(this.FileTypeRefresh);
                this.cmuFile.MenuItems.Add(ex2);
            }
        }

        private void cmiDel_Click(object sender, EventArgs e)
        {
            DEFileType tag = (DEFileType) this.lvwFile.SelectedItems[0].Tag;
            if (MessageBox.Show("是否删除选中文件类型？", "删除文件类型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                PLFileType type2 = new PLFileType();
                try
                {
                    if (type2.Delete(ClientData.LogonUser.LogId, tag.Oid))
                    {
                        this.lvwFile.SelectedItems[0].Remove();
                    }
                }
                catch (EnvironmentException exception)
                {
                    MessageBox.Show(exception.Message, "删除文件类型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                catch (Exception exception2)
                {
                    PrintException.Print(exception2, "删除文件类型");
                }
            }
        }

        private void cmiEdit_Click(object sender, EventArgs e)
        {
            DEFileType tag = (DEFileType) this.lvwFile.SelectedItems[0].Tag;
            FrmFTDialog dialog = new FrmFTDialog(tag, true);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DEFileType deF = dialog.deF;
                this.lvwFile.SelectedItems[0].SubItems[0].Text = deF.Name;
                this.lvwFile.SelectedItems[0].SubItems[1].Text = deF.Postfix;
                if (deF.ToolOid == Guid.Empty)
                {
                    this.lvwFile.SelectedItems[0].SubItems[2].Text = "";
                }
                else
                {
                    this.lvwFile.SelectedItems[0].SubItems[2].Text = dialog.StrTool;
                }
                this.lvwFile.SelectedItems[0].SubItems[3].Text = deF.DefaultBrowserName;
                string name = "";
                for (int i = 0; i < this.allEditor.Count; i++)
                {
                    DEBrowser browser = (DEBrowser) this.allEditor[i];
                    if (browser.Oid.Equals(tag.DefaultEditorOid))
                    {
                        name = browser.Name;
                    }
                }
                this.lvwFile.SelectedItems[0].SubItems[4].Text = name;
                if ((deF.Option & 1) == 1)
                {
                    this.lvwFile.SelectedItems[0].SubItems[5].Text = "是";
                }
                else
                {
                    this.lvwFile.SelectedItems[0].SubItems[5].Text = "否";
                }
                this.lvwFile.SelectedItems[0].SubItems[6].Text = deF.Creator;
                this.lvwFile.SelectedItems[0].SubItems[7].Text = deF.CreatTime.ToString("yyyy-MM-dd HH:mm:ss");
                this.lvwFile.SelectedItems[0].SubItems[8].Text = deF.Description;
                this.lvwFile.SelectedItems[0].Tag = deF;
            }
            dialog.Dispose();
        }

        private void cmiRefreshFiles_Click(object sender, EventArgs e)
        {
            if ((this.lvwFile.SelectedItems.Count != 0) && (MessageBox.Show("更新业务对象文件类型会将系统中未标识文件类型的源文件根据当前文件类型定义进行标识，您是否确定要继续？", "文件类型管理", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No))
            {
                DEFileType tag = this.lvwFile.SelectedItems[0].Tag as DEFileType;
                if (tag != null)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int num = new PLFileType().UpdateItemsFileType(tag.Oid, ClientData.LogonUser.Oid);
                        MessageBox.Show("共更新了" + num.ToString() + "个业务对象源文件的文件类型。", "文件类型管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception);
                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void cmiRelat_Click(object sender, EventArgs e)
        {
            if (this.lvwFile.SelectedItems[0].Tag != null)
            {
                FrmSetFT2Cls cls = new FrmSetFT2Cls((DEFileType) this.lvwFile.SelectedItems[0].Tag);
                cls.ShowDialog();
                cls.Dispose();
            }
        }

        public void CreateList()
        {
            this.lvwFile.Items.Clear();
            PLFileType type = new PLFileType();
            ArrayList allFileTypes = null;
            try
            {
                allFileTypes = type.GetAllFileTypes();
            }
            catch (EnvironmentException exception)
            {
                MessageBox.Show(exception.Message, "文件类型管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            catch
            {
                MessageBox.Show("显示文件类型信息失败！", "文件类型管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            for (int i = 0; i < allFileTypes.Count; i++)
            {
                DEFileType type2 = (DEFileType) allFileTypes[i];
                string str = "";
                if ((type2.Option & 1) == 1)
                {
                    str = "是";
                }
                else
                {
                    str = "否";
                }
                string name = "";
                for (int j = 0; j < this.allTools.Count; j++)
                {
                    DETool tool = (DETool) this.allTools[j];
                    if (tool.Oid.Equals(type2.ToolOid))
                    {
                        name = tool.Name;
                    }
                }
                string str3 = "";
                for (int k = 0; k < this.allEditor.Count; k++)
                {
                    DEBrowser browser = (DEBrowser) this.allEditor[k];
                    if (browser.Oid.Equals(type2.DefaultEditorOid))
                    {
                        str3 = browser.Name;
                    }
                }
                string[] items = new string[] { type2.Name, type2.Postfix, name, type2.DefaultBrowserName, str3, str, type2.Creator, type2.CreatTime.ToString("yyyy-MM-dd HH:mm:ss"), type2.Description };
                ListViewItem item = new ListViewItem(items) {
                    Tag = type2,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_FILETYPE")
                };
                this.lvwFile.Items.Add(item);
            }
        }

        public void FileTypeRefresh(object sender, EventArgs e)
        {
            this.lvwFile.Items.Clear();
            this.CreateList();
        }

        private void FrmFile_Closing(object sender, CancelEventArgs e)
        {
            this.frmMain.RemoveWnd(this);
        }

        private void FrmFile_Load(object sender, EventArgs e)
        {
            this.lvwFile.SmallImageList = ClientData.MyImageList.imageList;
            this.lvwFile.Columns.Add("名称", 150, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("扩展名", 80, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("工具软件", 100, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("使用的浏览器", 150, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("默认编辑器", 150, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("压缩", 50, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("创建人", 80, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("创建时间", 100, HorizontalAlignment.Left);
            this.lvwFile.Columns.Add("描述", 200, HorizontalAlignment.Left);
            this.CreateList();
        }

        private void GetBaseToolAndEdit()
        {
            PLTool tool = new PLTool();
            try
            {
                this.allTools = tool.GetAllTools();
            }
            catch
            {
                MessageBox.Show("获取工具软件信息失败", "文件类型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            PLBrowser browser = new PLBrowser();
            try
            {
                this.allEditor = browser.GetAllEditors();
            }
            catch
            {
                MessageBox.Show("获取编辑器信息失败", "文件类型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void lvwFile_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem itemAt = this.lvwFile.GetItemAt(e.X, e.Y);
                this.isItem = itemAt != null;
                if (itemAt != null)
                {
                    itemAt.Selected = true;
                }
                this.BuildMenu();
                this.cmuFile.Show(this.lvwFile, new Point(e.X, e.Y));
            }
        }

        public void NewFileType(object sender, EventArgs e)
        {
            FrmFTDialog dialog = new FrmFTDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DEFileType deF = dialog.deF;
                this.GetBaseToolAndEdit();
                string[] items = new string[9];
                items[0] = deF.Name;
                items[1] = deF.Postfix;
                bool flag = false;
                for (int i = 0; i < this.allTools.Count; i++)
                {
                    DETool tool = (DETool) this.allTools[i];
                    if (tool.Oid.Equals(deF.ToolOid))
                    {
                        items[2] = tool.Name;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    items[2] = "";
                }
                flag = false;
                items[3] = deF.DefaultBrowserName;
                for (int j = 0; j < this.allEditor.Count; j++)
                {
                    DEBrowser browser = (DEBrowser) this.allEditor[j];
                    if (browser.Oid.Equals(deF.DefaultEditorOid))
                    {
                        items[4] = browser.Name;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    items[4] = "";
                }
                if ((deF.Option & 1) == 1)
                {
                    items[5] = "是";
                }
                else
                {
                    items[5] = "否";
                }
                items[6] = deF.Creator;
                items[7] = deF.CreatTime.ToString("yyyy-MM-dd HH:mm:ss");
                items[8] = deF.Description;
                ListViewItem item = new ListViewItem(items) {
                    Tag = deF,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_FILETYPE")
                };
                this.lvwFile.Items.Add(item);
                int count = this.lvwFile.Items.Count;
                this.lvwFile.Items[count - 1].Selected = true;
            }
            dialog.Dispose();
        }
    }
}

