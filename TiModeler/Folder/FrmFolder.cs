
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.ObjectPerm;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler.Folder {
    public partial class FrmFolder : Form
    {
        
        public PLM_FormClosed FormClosed;
        private bool isNewFolder;
        private const string Public_Folder = "folder_pub";

        public FrmFolder(FrmMain main)
        {
            this.InitializeComponent();
            this.frmMain = main;
            this.InitializeImageList();
            base.Icon = PLMImageList.GetIcon("ICO_FDL_OPEN");
            this.tvwFolder.ImageList = ClientData.MyImageList.imageList;
            this.tvwFolder.SelectedImageIndex = this.tvwFolder.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE");
            this.tvwFolder.Refresh();
        }

        private void BulidMenu(TreeNode tn)
        {
            this.cmuFolder.MenuItems.Clear();
            MenuItemEx item = null;
            if (tn.Tag is string)
            {
                if (PLGrantPerm.Agent.IsSystemAdminRole(ClientData.LogonUser.Oid) == 1)
                {
                    item = new MenuItemEx("&New Folder", "新建子文件夹(&N)", null, null);
                    item.Click += new EventHandler(this.NewFolder);
                    this.cmuFolder.MenuItems.Add(item);
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuFolder.MenuItems.Add(item);
                }
                item = new MenuItemEx("Folder Re&Fresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                item.Click += new EventHandler(this.FolderRefresh);
                this.cmuFolder.MenuItems.Add(item);
            }
            else
            {
                item = new MenuItemEx("&New Folder", "新建子文件夹(&N)", null, null);
                item.Click += new EventHandler(this.NewFolder);
                this.cmuFolder.MenuItems.Add(item);
                item = new MenuItemEx("Folder &Delete", "删除(&D)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                };
                item.Click += new EventHandler(this.FolderDelete);
                this.cmuFolder.MenuItems.Add(item);
                item = new MenuItemEx("Folder &Rename", "重命名(&R)", null, null);
                item.Click += new EventHandler(this.FolderRename);
                this.cmuFolder.MenuItems.Add(item);
                item = new MenuItemEx("Folder Re&Fresh", "刷新(&F)", null, null) {
                    ImageList = ClientData.MyImageList.imageList,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                };
                item.Click += new EventHandler(this.FolderRefresh);
                this.cmuFolder.MenuItems.Add(item);
                if (PLGrantPerm.Agent.CanDoAssignFolderPerms(ClientData.LogonUser.Oid, ((DEFolder2) tn.Tag).Oid) == 1)
                {
                    item = new MenuItemEx("-", "-", null, null);
                    this.cmuFolder.MenuItems.Add(item);
                    item = new MenuItemEx("&Set Resposibility", "设置权限(&S)", null, null);
                    item.Click += new EventHandler(this.SetResposibility);
                    this.cmuFolder.MenuItems.Add(item);
                }
            }
        }

        private void FolderDelete(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要删除文件夹“" + this.tvwFolder.SelectedNode.Text + "”吗？", "文件夹定制", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                DEFolder2 tag = (DEFolder2) this.tvwFolder.SelectedNode.Tag;
                try
                {
                    Hashtable result = null;
                    PLFolder.RemotingAgent.DeleteFolder(ClientData.LogonUser.Oid, tag.Oid, true, false, out result);
                    this.tvwFolder.SelectedNode.Remove();
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
            }
        }

        private void FolderRefresh(object sender, EventArgs e)
        {
            this.tvwFolder.Nodes.Clear();
            this.initialFolderTree(this.tvwFolder);
            this.tvwFolder.Nodes[0].Expand();
        }

        private void FolderRename(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.tvwFolder.SelectedNode;
            if (!(this.tvwFolder.SelectedNode.Tag is string))
            {
                this.isNewFolder = false;
                this.tvwFolder.LabelEdit = true;
                selectedNode.BeginEdit();
            }
        }

        private void FrmFolder_Activated(object sender, EventArgs e)
        {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE");
            int num2 = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN");
            TreeNode rootNode = this.tvwFolder.Nodes[0];
            this.SetIconIndex(rootNode, iconIndex, num2);
        }

        private void FrmFolder_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                this.FormClosed(this);
            }
            catch
            {
            }
        }

        private void FrmFolder_Load(object sender, EventArgs e)
        {
            this.initialFolderTree(this.tvwFolder);
            this.tvwFolder.Nodes[0].Expand();
        }

        private void initialFolderTree(TreeView tvw)
        {
            TreeNode node = new TreeNode("公共文件夹") {
                Tag = "folder_pub"
            };
            tvw.Nodes.Add(node);
            FolderUL.FillFolderTreeFilterByPower(node);
        }


        private void InitializeImageList()
        {
            ClientData.MyImageList.AddIcon("ICO_FDL_OPEN");
            ClientData.MyImageList.AddIcon("ICO_FDL_CLOSE");
        }

        private void NewFolder(object sender, EventArgs e)
        {
            TreeNode node = this.tvwFolder.SelectedNode.Nodes.Add("新文件夹");
            this.tvwFolder.SelectedNode = node;
            this.isNewFolder = true;
            this.tvwFolder.LabelEdit = true;
            node.BeginEdit();
        }

        private void SetIconIndex(TreeNode rootNode, int index1, int index2)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                node.SelectedImageIndex = node.ImageIndex = index1;
                if (node.IsExpanded)
                {
                    node.SelectedImageIndex = node.ImageIndex = index2;
                }
                this.SetIconIndex(node, index1, index2);
            }
        }

        private void SetResposibility(object sender, EventArgs e)
        {
            if ((this.tvwFolder.SelectedNode != null) && (this.tvwFolder.SelectedNode.Tag.GetType().Name == "DEFolder2"))
            {
                try
                {
                    FrmFolderPerm perm = new FrmFolderPerm {
                        deFolder = (DEFolder2) this.tvwFolder.SelectedNode.Tag
                    };
                    perm.ShowDialog();
                    perm.Dispose();
                    perm = null;
                }
                catch (ResponsibilityException)
                {
                }
            }
        }

        private void tvwFolder_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            e.Node.SelectedImageIndex = e.Node.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_CLOSE");
        }

        private void tvwFolder_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.SelectedImageIndex = e.Node.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN");
        }

        private void tvwFolder_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag is string)
            {
                e.CancelEdit = true;
                this.tvwFolder.LabelEdit = false;
                return;
            }
            if (this.tvwFolder.SelectedNode == null)
            {
                return;
            }
            string label = e.Label;
            if ((e.Node.Tag == null) && (label == null))
            {
                label = e.Node.Text.Trim();
            }
            else
            {
                if (label == null)
                {
                    return;
                }
                label = label.Trim();
                if (!this.isNewFolder && label.Equals(e.Node.Text.Trim()))
                {
                    e.CancelEdit = true;
                    e.Node.Text = label;
                    return;
                }
            }
            if (label == "")
            {
                MessageBox.Show("文件夹的名称不允许为空！", "文件夹", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                e.CancelEdit = true;
                if (this.tvwFolder.SelectedNode.Tag == null)
                {
                    this.tvwFolder.SelectedNode = e.Node.Parent;
                    e.Node.Remove();
                }
                return;
            }
            if (label == this.tvwFolder.Nodes[0].Text)
            {
                MessageBox.Show("文件夹的名称不允许和根文件夹相同！", "文件夹", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                e.CancelEdit = true;
                if (this.tvwFolder.SelectedNode.Tag == null)
                {
                    this.tvwFolder.SelectedNode = e.Node.Parent;
                    e.Node.Remove();
                }
                return;
            }
            if (label.Length > 0x40)
            {
                MessageBox.Show("文件夹名字的长度不能超过64！", "文件夹", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                e.CancelEdit = true;
                if (this.tvwFolder.SelectedNode.Tag == null)
                {
                    this.tvwFolder.SelectedNode = e.Node.Parent;
                    e.Node.Remove();
                }
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            if (e.Node.Tag == null)
            {
                DEFolder2 folder = new DEFolder2 {
                    Name = label,
                    Creator = ClientData.LogonUser.LogId
                };
                if (this.tvwFolder.SelectedNode.Parent.Tag is string)
                {
                    folder.Parent = Guid.Empty;
                    folder.FolderType = 'C';
                }
                else
                {
                    folder.Parent = ((DEFolder2) this.tvwFolder.SelectedNode.Parent.Tag).Oid;
                    folder.FolderType = 'A';
                }
                folder.Owner = Guid.Empty;
                try
                {
                    folder = PLFolder.CreateFolder(folder, ClientData.LogonUser.Oid);
                    e.CancelEdit = true;
                    e.Node.Text = folder.Name;
                    e.Node.Tag = folder;
                    goto Label_0382;
                }
                catch (ResponsibilityException exception)
                {
                    MessageBox.Show(exception.Message, "文件夹", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.tvwFolder.SelectedNode = e.Node.Parent;
                    e.Node.Remove();
                    return;
                }
                catch (Exception exception2)
                {
                    MessageBox.Show(exception2.Message, "文件夹", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    this.tvwFolder.SelectedNode = e.Node.Parent;
                    e.Node.Remove();
                    return;
                }
            }
            this.tvwFolder.SelectedNode.EndEdit(false);
            DEFolder2 tag = (DEFolder2) e.Node.Tag;
            tag.Name = label;
            try
            {
                PLFolder.RemotingAgent.RenameFolder(ClientData.LogonUser.Oid, tag.Oid, tag.Name);
                e.CancelEdit = false;
            }
            catch (Exception exception3)
            {
                PrintException.Print(exception3);
                e.CancelEdit = true;
                this.tvwFolder.LabelEdit = false;
                return;
            }
        Label_0382:
            this.tvwFolder.LabelEdit = false;
            Cursor.Current = Cursors.Default;
        }

        private void tvwFolder_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode nodeAt = this.tvwFolder.GetNodeAt(e.X, e.Y);
            Point pt = new Point(e.X + 10, e.Y);
            if (this.tvwFolder.GetNodeAt(pt) != null)
            {
                this.tvwFolder.SelectedNode = nodeAt;
                if (e.Button == MouseButtons.Right)
                {
                    this.BulidMenu(nodeAt);
                    if (this.cmuFolder.MenuItems.Count > 0)
                    {
                        this.cmuFolder.Show(this.tvwFolder, pt);
                    }
                }
            }
        }
    }
}

