
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
    public partial class FrmTool : Form
    {
        private MenuItemEx cmiDel;
        private MenuItemEx cmiEdit;
       
        private bool isItem;

        public FrmTool()
        {
            this.isItem = true;
            this.InitializeComponent();
        }

        public FrmTool(FrmMain main)
        {
            this.isItem = true;
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_ENV_TOOL");
            this.frmMain = main;
        }

        public void BuildMenu()
        {
            this.cmuTool.MenuItems.Clear();
            if (this.isItem)
            {
                this.cmiEdit = new MenuItemEx("Property", "属性", null, null);
                this.cmiEdit.DefaultItem = true;
                this.cmiEdit.ImageList = ClientData.MyImageList.imageList;
                this.cmiEdit.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                this.cmiEdit.Click += new EventHandler(this.cmiEdit_Click);
                this.cmuTool.MenuItems.Add(this.cmiEdit);
                MenuItemEx item = new MenuItemEx("-", "-", null, null);
                this.cmuTool.MenuItems.Add(item);
                this.cmiDel = new MenuItemEx("Delete", "删除", null, null);
                this.cmiDel.ImageList = ClientData.MyImageList.imageList;
                this.cmiDel.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE");
                this.cmiDel.Click += new EventHandler(this.cmiDel_Click);
                this.cmuTool.MenuItems.Add(this.cmiDel);
            }
            else
            {
                MenuItemEx ex2 = new MenuItemEx("&New Tool", "新增工具软件(&N)", null, null);
                ex2.Click += new EventHandler(this.frmMain.NewTool);
                this.cmuTool.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("-", "-", null, null);
                this.cmuTool.MenuItems.Add(ex2);
                ex2 = new MenuItemEx("Tool Re&Fresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                ex2.Click += new EventHandler(this.frmMain.ToolRefresh);
                this.cmuTool.MenuItems.Add(ex2);
            }
        }

        private void cmiDel_Click(object sender, EventArgs e)
        {
            if (this.lvwTool.SelectedItems[0].Tag != null)
            {
                DETool tag = (DETool) this.lvwTool.SelectedItems[0].Tag;
                if (MessageBox.Show("是否删除选中工具软件？", "删除工具软件", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    PLTool tool2 = new PLTool();
                    try
                    {
                        tool2.Delete(ClientData.LogonUser.LogId, tag.Oid);
                        this.lvwTool.SelectedItems[0].Remove();
                    }
                    catch (EnvironmentException exception)
                    {
                        MessageBox.Show(exception.Message, "删除工具软件", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    catch
                    {
                        MessageBox.Show("删除工具软件失败！", "删除工具软件", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
        }

        private void cmiEdit_Click(object sender, EventArgs e)
        {
            if (this.lvwTool.SelectedItems[0].Tag != null)
            {
                DETool tag = (DETool) this.lvwTool.SelectedItems[0].Tag;
                FrmSetTool tool2 = new FrmSetTool(tag, true);
                if (tool2.ShowDialog() == DialogResult.OK)
                {
                    DETool deTool = new DETool();
                    deTool = tool2.deTool;
                    this.lvwTool.SelectedItems[0].SubItems[0].Text = deTool.Name;
                    this.lvwTool.SelectedItems[0].SubItems[1].Text = deTool.Creator;
                    this.lvwTool.SelectedItems[0].SubItems[2].Text = deTool.CreateTime.ToString("yyyy-MM-dd");
                    this.lvwTool.SelectedItems[0].SubItems[3].Text = deTool.Description;
                    this.lvwTool.SelectedItems[0].Tag = deTool;
                }
                tool2.Dispose();
            }
        }

        public void CreatList()
        {
            PLTool tool = new PLTool();
            ArrayList allTools = null;
            try
            {
                allTools = tool.GetAllTools();
            }
            catch (EnvironmentException exception)
            {
                MessageBox.Show(exception.Message, "工具软件管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            catch
            {
                MessageBox.Show("显示工具软件信息失败！", "工具软件管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            for (int i = 0; i < allTools.Count; i++)
            {
                DETool tool2 = (DETool) allTools[i];
                string[] items = new string[] { tool2.Name, tool2.Creator, tool2.CreateTime.ToString("yyyy-MM-dd"), tool2.Description };
                ListViewItem item = new ListViewItem(items) {
                    Tag = tool2,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_TOOL")
                };
                this.lvwTool.Items.Add(item);
            }
        }
         

        private void FrmTool_Closing(object sender, CancelEventArgs e)
        {
            this.frmMain.RemoveWnd(this);
        }

        private void FrmTool_Load(object sender, EventArgs e)
        {
            this.lvwTool.SmallImageList = ClientData.MyImageList.imageList;
            this.lvwTool.Columns.Add("名称", 200, HorizontalAlignment.Left);
            this.lvwTool.Columns.Add("创建人", 100, HorizontalAlignment.Left);
            this.lvwTool.Columns.Add("创建时间", 150, HorizontalAlignment.Left);
            this.lvwTool.Columns.Add("描述", 300, HorizontalAlignment.Left);
            this.CreatList();
        }


        private void lvwTool_ItemActivate(object sender, EventArgs e)
        {
            if (this.lvwTool.SelectedItems.Count > 0)
            {
                this.cmiEdit_Click(sender, e);
            }
        }

        private void lvwTool_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem itemAt = this.lvwTool.GetItemAt(e.X, e.Y);
                this.isItem = itemAt != null;
                if (itemAt != null)
                {
                    itemAt.Selected = true;
                }
                this.BuildMenu();
                this.cmuTool.Show(this.lvwTool, new Point(e.X, e.Y));
            }
        }
    }
}

