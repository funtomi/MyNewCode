
    using DevExpress.XtraEditors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler {
    public partial class FrmBPNavigator : Form
    {
        
        private int currentIndex = -1;
        private FrmMain frmMain;
        private List<int> listIndex = new List<int>();
        public SortableListView lvwNavigater;
        private PanelControl panelControl1;
        private TreeNode treeNodeProcessRoot;
        private TextEdit txtSearch;

        public FrmBPNavigator(FrmMain frmMain)
        {
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_TIMODELER_NAV_PIC");
            this.frmMain = frmMain;
            this.lvwNavigater.SmallImageList = ClientData.MyImageList.imageList;
        }

        internal void AddOneProcess(TreeNode subNode)
        {
            if (subNode.Tag is DELProcessClass)
            {
                DELProcessClass tag = (DELProcessClass) subNode.Tag;
                this.lvwNavigater.Items.Add(tag.Name, subNode.ImageIndex).Tag = subNode;
            }
            else
            {
                DELProcessDefProperty property = (DELProcessDefProperty) subNode.Tag;
                PLUser user = new PLUser();
                ListViewItem item2 = this.lvwNavigater.Items.Add(subNode.Text, subNode.ImageIndex);
                try
                {
                    item2.SubItems.Add(user.GetUserByOid(property.CreatorID).Name);
                }
                catch (Exception)
                {
                    item2.SubItems.Add("");
                }
                item2.SubItems.Add(property.CreationDate.ToString("yyyy-MM-dd HH:mm:ss"));
                item2.SubItems.Add(property.UpdateDate.ToString("yyyy-MM-dd HH:mm:ss"));
                item2.SubItems.Add(property.Duration.ToString() + property.DurationUnit);
                item2.SubItems.Add(property.Description);
                item2.Tag = subNode;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.currentIndex++;
            this.lvwNavigater.EnsureVisible(this.listIndex[this.currentIndex]);
            this.lvwNavigater.SelectedItems.Clear();
            this.lvwNavigater.Items[this.listIndex[this.currentIndex]].Selected = true;
            this.SetButtonState();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            this.currentIndex--;
            this.lvwNavigater.EnsureVisible(this.listIndex[this.currentIndex]);
            this.lvwNavigater.SelectedItems.Clear();
            this.lvwNavigater.Items[this.listIndex[this.currentIndex]].Selected = true;
            this.SetButtonState();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.DoSearch();
        }

        internal void DeleteOneProcess(TreeNode node)
        {
            foreach (ListViewItem item in this.lvwNavigater.Items)
            {
                if (((TreeNode) item.Tag).Equals(node))
                {
                    this.lvwNavigater.Items.Remove(item);
                    break;
                }
            }
        }

        private void DoSearch()
        {
            this.listIndex.Clear();
            this.currentIndex = -1;
            string text = this.txtSearch.Text;
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text.Trim()))
            {
                MessageBox.Show("请先输入搜索文本！", "提示");
            }
            else
            {
                string str2 = text.Trim();
                for (int i = 0; i < this.lvwNavigater.Items.Count; i++)
                {
                    ListViewItem item = this.lvwNavigater.Items[i];
                    TreeNode tag = item.Tag as TreeNode;
                    if (item.Text.Trim().Contains(str2))
                    {
                        item.ForeColor = Color.Black;
                        this.listIndex.Add(i);
                    }
                    else
                    {
                        item.ForeColor = Color.Gray;
                    }
                    if (tag.Tag is DELProcessClass)
                    {
                        bool flag = false;
                        foreach (TreeNode node2 in tag.Nodes)
                        {
                            if (node2.Text.Contains(str2))
                            {
                                flag = true;
                                this.listIndex.Add(i);
                                item.ForeColor = Color.Black;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            item.ForeColor = Color.Gray;
                        }
                    }
                }
                if (this.listIndex.Count > 0)
                {
                    this.currentIndex = 0;
                    this.lvwNavigater.EnsureVisible(this.listIndex[this.currentIndex]);
                    this.lvwNavigater.SelectedItems.Clear();
                    this.lvwNavigater.Items[this.listIndex[this.currentIndex]].Selected = true;
                }
                else
                {
                    MessageBox.Show("没有符合条件的属性！", "提示");
                    return;
                }
                this.SetButtonState();
                for (int j = 0; j < this.listIndex.Count; j++)
                {
                }
            }
        }

        private void lvwNavigater_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            TreeNode tag = (TreeNode) this.lvwNavigater.Items[e.Item].Tag;
            string str = tag.Text.Trim();
            if (e.Label != null)
            {
                str = e.Label.Trim();
            }
            if ((str.Length == 0) || (str.Length > 0x20))
            {
                MessageBox.Show("过程分类名称长度必须为1到32之间。", ConstCommon.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                e.CancelEdit = true;
            }
            foreach (TreeNode node2 in this.frmMain.TheProClsNodeList.Values)
            {
                DELProcessClass class2 = node2.Tag as DELProcessClass;
                if ((str.ToUpper() == class2.Name.ToUpper()) && (tag != node2))
                {
                    MessageBox.Show("已存在重名的过程分类。", ConstCommon.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    e.CancelEdit = true;
                }
            }
            if (!e.CancelEdit)
            {
                tag.Text = str;
            }
            else
            {
                this.lvwNavigater.LabelEdit = true;
                this.lvwNavigater.SelectedItems[0].BeginEdit();
                return;
            }
            DELProcessClass processClass = tag.Tag as DELProcessClass;
            processClass.Name = str;
            BPMProcessor processor = new BPMProcessor();
            try
            {
                if (!processor.ModifyOneProcessClass(processClass))
                {
                    MessageBox.Show("过程分类重命名失败！");
                    processClass.Name = tag.Text;
                    e.CancelEdit = true;
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("过程分类重命名失败！");
                processClass.Name = tag.Text;
                e.CancelEdit = true;
                return;
            }
            int index = this.frmMain.FindPosition(processClass);
            if (tag.Index != index)
            {
                TagForTiModeler.TreeNode_BPM.Nodes.Remove(tag);
                TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, tag);
            }
            e.CancelEdit = true;
            this.lvwNavigater.LabelEdit = false;
            Cursor.Current = Cursors.Default;
            this.frmMain.ShowProcessPropertyList();
            if ((this.frmMain.frmBPNavigator != null) && !this.frmMain.frmBPNavigator.IsDisposed)
            {
                this.frmMain.frmBPNavigator.UpdateListView();
            }
        }

        private void lvwNavigater_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if ((this.lvwNavigater.SelectedItems.Count != 0) && (this.lvwNavigater.SelectedItems.Count == 1))
            {
                TreeNode tag = this.lvwNavigater.SelectedItems[0].Tag as TreeNode;
                if (!(tag.Tag is DELProcessClass))
                {
                    this.frmMain.tvwNavigator.SelectedNode = (TreeNode) this.lvwNavigater.SelectedItems[0].Tag;
                }
            }
        }

        private void lvwNavigater_KeyUp(object sender, KeyEventArgs e)
        {
            if (((e.KeyCode == Keys.Enter) && (this.lvwNavigater.SelectedItems.Count != 0)) && (this.frmMain != null))
            {
                TreeNode tag = this.lvwNavigater.SelectedItems[0].Tag as TreeNode;
                if (tag.Tag is DELProcessClass)
                {
                    this.frmMain.tvwNavigator.SelectedNode = tag;
                }
                else
                {
                    this.OpenProTemplate();
                }
            }
        }

        private void lvwNavigater_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.currentIndex = -1;
            this.listIndex.Clear();
            this.SetButtonState();
            this.OpenProTemplate();
        }

        private void lvwNavigater_MouseUp(object sender, MouseEventArgs e)
        {
            if (((e.Button == MouseButtons.Right) && (this.lvwNavigater.SelectedItems.Count == 1)) && (this.frmMain != null))
            {
                TreeNode tag = (TreeNode) this.lvwNavigater.SelectedItems[0].Tag;
                ContextMenu menu = new ContextMenu();
                if (tag.Tag is DELProcessClass)
                {
                    MenuItemEx item = new MenuItemEx("&RenameProClass", "重命名(&R)", null, null);
                    item.Click += new EventHandler(this.RenameProClass);
                    menu.MenuItems.Add(item);
                    item = new MenuItemEx("New&ProTem", "新建模板(&P)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                    };
                    item.Click += new EventHandler(this.frmMain.OnNewProTem);
                    menu.MenuItems.Add(item);
                    item = new MenuItemEx("&ImportTemplate", "导入模板(&I)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                    };
                    item.Click += new EventHandler(this.frmMain.OnImportTemplate);
                    menu.MenuItems.Add(item);
                    item = new MenuItemEx("&OnPasteProTem", "粘贴模版(&V)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_PASTE")
                    };
                    if (this.frmMain.getTheCuttingNode() == null)
                    {
                        item.Enabled = false;
                    }
                    else if (this.frmMain.isCuttingNodeDeleted)
                    {
                        item.Enabled = false;
                    }
                    else if (this.frmMain.getTheCuttingNode().Parent == tag)
                    {
                        item.Enabled = false;
                    }
                    else
                    {
                        item.Enabled = true;
                    }
                    item.Click += new EventHandler(this.frmMain.OnPasteProTem);
                    menu.MenuItems.Add(item);
                    item = new MenuItemEx("&DelProClass", "删除(&D)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_DELETE")
                    };
                    item.Click += new EventHandler(this.frmMain.OnDelProClass);
                    menu.MenuItems.Add(item);
                }
                if (tag.Tag is DELProcessDefProperty)
                {
                    MenuItemEx ex2;
                    this.frmMain.tvwNavigator.SelectedNode = tag;
                    menu = this.frmMain.BuildMenu("Thyt.TiPLM.DEL.Admin.BPM.DELProcessDefProperty", this.frmMain.tvwNavigator.SelectedNode);
                    if (this.frmMain.GetAllowCreateProcessManagement())
                    {
                        ex2 = new MenuItemEx("-", "-", null, null);
                        menu.MenuItems.Add(ex2);
                        ex2 = new MenuItemEx("New&ProTem", "新建模板(&P)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        ex2.Click += new EventHandler(this.frmMain.OnNewProTem);
                        menu.MenuItems.Add(ex2);
                        ex2 = new MenuItemEx("&ImportTemplate", "导入模板(&I)", null, null) {
                            ImageList = ClientData.MyImageList.imageList,
                            ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_BPM_DEF")
                        };
                        ex2.Click += new EventHandler(this.frmMain.OnImportTemplate);
                        menu.MenuItems.Add(ex2);
                    }
                    ex2 = new MenuItemEx("&Refresh", "刷新(&R)", null, null) {
                        ImageList = ClientData.MyImageList.imageList,
                        ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH")
                    };
                    ex2.Click += new EventHandler(this.frmMain.OnRefresh);
                    menu.MenuItems.Add(ex2);
                }
                if (menu.MenuItems.Count > 0)
                {
                    menu.Show(this.lvwNavigater, new Point(e.X, e.Y));
                }
            }
        }

        private void lvwNavigator_DragDrop(object sender, DragEventArgs e)
        {
            bool flag = false;
            bool flag2 = false;
            IEnumerator enumerator = ((CLCopyData) e.Data.GetData(typeof(CLCopyData))).GetEnumerator();
            Point p = new Point(e.X, e.Y);
            p = base.PointToClient(p);
            ListViewItem itemAt = this.lvwNavigater.GetItemAt(p.X, p.Y);
            if (itemAt != null)
            {
                TreeNode tag = (TreeNode) itemAt.Tag;
                while (enumerator.MoveNext())
                {
                    TreeNode current = (TreeNode) enumerator.Current;
                    this.frmMain.setTheDragingNode(current);
                    if (this.frmMain.getTheDragingNode().Parent == tag)
                    {
                        break;
                    }
                    if (this.frmMain.getTheCuttingNode() == current)
                    {
                        flag2 = true;
                    }
                    BPMProcessor processor2 = new BPMProcessor();
                    TreeNode parent = this.frmMain.getTheDragingNode().Parent;
                    DELProcessDefProperty proDef = this.frmMain.getTheDragingNode().Tag as DELProcessDefProperty;
                    if (tag == TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass class5 = parent.Tag as DELProcessClass;
                        if (processor2.MoveProcessBetweenClass(proDef.ID, class5.ID, Guid.Empty))
                        {
                            class5.RemoveProcess(proDef.ID);
                            parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                            int index = this.frmMain.FindPosition(TagForTiModeler.TreeNode_BPM, proDef);
                            TagForTiModeler.TreeNode_BPM.Nodes.Insert(index, this.frmMain.getTheDragingNode());
                            flag = true;
                        }
                    }
                    else
                    {
                        if ((tag.Parent == TagForTiModeler.TreeNode_BPM) && !(tag.Tag is DELProcessClass))
                        {
                            DELProcessClass class6 = parent.Tag as DELProcessClass;
                            if (processor2.MoveProcessBetweenClass(proDef.ID, class6.ID, Guid.Empty))
                            {
                                class6.RemoveProcess(proDef.ID);
                                parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                                int num5 = this.frmMain.FindPosition(TagForTiModeler.TreeNode_BPM, proDef);
                                TagForTiModeler.TreeNode_BPM.Nodes.Insert(num5, this.frmMain.getTheDragingNode());
                                flag = true;
                            }
                            continue;
                        }
                        if (tag.Tag is DELProcessClass)
                        {
                            DELProcessClass class7 = tag.Tag as DELProcessClass;
                            if (parent == TagForTiModeler.TreeNode_BPM)
                            {
                                if (processor2.MoveProcessBetweenClass(proDef.ID, Guid.Empty, class7.ID))
                                {
                                    parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                                    int num6 = this.frmMain.FindPosition(tag, proDef);
                                    tag.Nodes.Insert(num6, this.frmMain.getTheDragingNode());
                                    class7.AddProcess(proDef.ID);
                                    flag = true;
                                }
                            }
                            else
                            {
                                DELProcessClass class8 = parent.Tag as DELProcessClass;
                                if (processor2.MoveProcessBetweenClass(proDef.ID, class8.ID, class7.ID))
                                {
                                    parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                                    class8.RemoveProcess(proDef.ID);
                                    int num7 = this.frmMain.FindPosition(tag, proDef);
                                    tag.Nodes.Insert(num7, this.frmMain.getTheDragingNode());
                                    class7.AddProcess(proDef.ID);
                                    flag = true;
                                }
                            }
                            continue;
                        }
                        if (tag.Parent.Tag is DELProcessClass)
                        {
                            DELProcessClass class9 = tag.Parent.Tag as DELProcessClass;
                            if (parent == TagForTiModeler.TreeNode_BPM)
                            {
                                if (processor2.MoveProcessBetweenClass(proDef.ID, Guid.Empty, class9.ID))
                                {
                                    parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                                    int num8 = this.frmMain.FindPosition(tag.Parent, proDef);
                                    tag.Parent.Nodes.Insert(num8, this.frmMain.getTheDragingNode());
                                    class9.AddProcess(proDef.ID);
                                    flag = true;
                                }
                                continue;
                            }
                            DELProcessClass class10 = parent.Tag as DELProcessClass;
                            if (processor2.MoveProcessBetweenClass(proDef.ID, class10.ID, class9.ID))
                            {
                                parent.Nodes.Remove(this.frmMain.getTheDragingNode());
                                class10.RemoveProcess(proDef.ID);
                                int num9 = this.frmMain.FindPosition(tag.Parent, proDef);
                                tag.Parent.Nodes.Insert(num9, this.frmMain.getTheDragingNode());
                                class9.AddProcess(proDef.ID);
                                flag = true;
                            }
                        }
                    }
                }
            }
            else if ((this.frmMain.frmBPNavigator != null) && !this.frmMain.frmBPNavigator.IsDisposed)
            {
                while (enumerator.MoveNext())
                {
                    TreeNode aNode = (TreeNode) enumerator.Current;
                    this.frmMain.setTheDragingNode(aNode);
                    if (this.frmMain.getTheDragingNode().Parent == this.frmMain.getopenedDELProcessClass())
                    {
                        break;
                    }
                    if (this.frmMain.getTheCuttingNode() == aNode)
                    {
                        flag2 = true;
                    }
                    BPMProcessor processor = new BPMProcessor();
                    TreeNode node2 = this.frmMain.getTheDragingNode().Parent;
                    DELProcessDefProperty property = this.frmMain.getTheDragingNode().Tag as DELProcessDefProperty;
                    if (this.frmMain.getopenedDELProcessClass() == TagForTiModeler.TreeNode_BPM)
                    {
                        DELProcessClass class2 = node2.Tag as DELProcessClass;
                        if (processor.MoveProcessBetweenClass(property.ID, class2.ID, Guid.Empty))
                        {
                            class2.RemoveProcess(property.ID);
                            node2.Nodes.Remove(this.frmMain.getTheDragingNode());
                            int num = this.frmMain.FindPosition(TagForTiModeler.TreeNode_BPM, property);
                            TagForTiModeler.TreeNode_BPM.Nodes.Insert(num, this.frmMain.getTheDragingNode());
                            flag = true;
                        }
                    }
                    else if (this.frmMain.getopenedDELProcessClass().Tag is DELProcessClass)
                    {
                        DELProcessClass class3 = this.frmMain.getopenedDELProcessClass().Tag as DELProcessClass;
                        if (node2 == TagForTiModeler.TreeNode_BPM)
                        {
                            if (processor.MoveProcessBetweenClass(property.ID, Guid.Empty, class3.ID))
                            {
                                node2.Nodes.Remove(this.frmMain.getTheDragingNode());
                                int num2 = this.frmMain.FindPosition(this.frmMain.getopenedDELProcessClass(), property);
                                this.frmMain.getopenedDELProcessClass().Nodes.Insert(num2, this.frmMain.getTheDragingNode());
                                class3.AddProcess(property.ID);
                                flag = true;
                            }
                            continue;
                        }
                        DELProcessClass class4 = node2.Tag as DELProcessClass;
                        if (processor.MoveProcessBetweenClass(property.ID, class4.ID, class3.ID))
                        {
                            node2.Nodes.Remove(this.frmMain.getTheDragingNode());
                            class4.RemoveProcess(property.ID);
                            int num3 = this.frmMain.FindPosition(this.frmMain.getopenedDELProcessClass(), property);
                            this.frmMain.getopenedDELProcessClass().Nodes.Insert(num3, this.frmMain.getTheDragingNode());
                            class3.AddProcess(property.ID);
                            flag = true;
                        }
                    }
                }
            }
            if (flag2)
            {
                this.frmMain.setTheCuttingNode(null);
            }
            this.frmMain.tvwNavigator.SelectedNode = this.frmMain.getTheDragingNode();
            if (flag)
            {
                this.frmMain.frmBPNavigator.UpdateListView(this.frmMain.getTheDragingNode().Parent);
            }
        }

        private void lvwNavigator_DragEnter(object sender, DragEventArgs e)
        {
            this.lvwNavigater.AllowDrop = true;
            e.Effect = DragDropEffects.Move;
        }

        private void lvwNavigator_DragOver(object sender, DragEventArgs e)
        {
            this.lvwNavigater.AllowDrop = true;
            e.Effect = DragDropEffects.Move;
        }

        private void lvwNavigator_ItemDrag(object sender, ItemDragEventArgs e)
        {
            this.lvwNavigater.AllowDrop = true;
            CLCopyData data = new CLCopyData();
            for (int i = 0; i < this.lvwNavigater.SelectedItems.Count; i++)
            {
                TreeNode tag = this.lvwNavigater.SelectedItems[i].Tag as TreeNode;
                if (!(tag.Tag is DELProcessDefProperty))
                {
                    break;
                }
                data.Add(tag);
            }
            if (data.Count > 0)
            {
                base.DoDragDrop(data, DragDropEffects.Move);
            }
        }

        private void OpenProTemplate()
        {
            if ((this.lvwNavigater.SelectedItems.Count != 0) && (this.frmMain != null))
            {
                TreeNode tag = this.lvwNavigater.SelectedItems[0].Tag as TreeNode;
                if (tag.Tag is DELProcessClass)
                {
                    this.frmMain.tvwNavigator.SelectedNode = tag;
                }
                else
                {
                    this.frmMain.OpenProcessTemplate((TreeNode) this.lvwNavigater.SelectedItems[0].Tag);
                    this.frmMain.tvwNavigator.SelectedNode = (TreeNode) this.lvwNavigater.SelectedItems[0].Tag;
                }
            }
        }

        public void RenameProClass(object sender, EventArgs e)
        {
            this.lvwNavigater.LabelEdit = true;
            this.frmMain.IsNewProClass = false;
            this.lvwNavigater.SelectedItems[0].BeginEdit();
        }

        private void SetButtonState()
        {
            if (this.currentIndex < 0)
            {
                this.btnPre.Enabled = false;
                this.btnNext.Enabled = false;
            }
            else if (this.currentIndex == 0)
            {
                if (this.listIndex.Count > 1)
                {
                    this.btnPre.Enabled = false;
                    this.btnNext.Enabled = true;
                }
                else
                {
                    this.btnPre.Enabled = false;
                    this.btnNext.Enabled = false;
                }
            }
            else if (this.currentIndex == (this.listIndex.Count - 1))
            {
                this.btnPre.Enabled = true;
                this.btnNext.Enabled = false;
            }
            else
            {
                this.btnPre.Enabled = true;
                this.btnNext.Enabled = true;
            }
        }

        internal void SetSelectItem(TreeNode node, bool isChanged)
        {
            if (node.Parent != this.treeNodeProcessRoot)
            {
                this.UpdateListView(node.Parent);
            }
            foreach (ListViewItem item in this.lvwNavigater.Items)
            {
                if (((TreeNode) item.Tag).Equals(node))
                {
                    if (!item.Selected)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                    }
                    if (isChanged)
                    {
                        DELProcessDefProperty tag = node.Tag as DELProcessDefProperty;
                        item.Text = tag.Name;
                        item.SubItems[3].Text = tag.UpdateDate.ToString();
                        item.SubItems[4].Text = tag.Duration.ToString() + tag.DurationUnit;
                        item.SubItems[5].Text = tag.Description;
                    }
                    break;
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DoSearch();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtSearch.Text))
            {
                for (int i = 0; i < this.lvwNavigater.Items.Count; i++)
                {
                    ListViewItem item = this.lvwNavigater.Items[i];
                    item.ForeColor = Color.Black;
                }
            }
            this.currentIndex = -1;
            this.listIndex.Clear();
            this.SetButtonState();
        }

        internal void UpdateListView()
        {
            if (this.treeNodeProcessRoot != null)
            {
                this.UpdateListView(this.treeNodeProcessRoot);
            }
        }

        internal void UpdateListView(TreeNode node)
        {
            this.currentIndex = -1;
            this.listIndex.Clear();
            this.SetButtonState();
            this.treeNodeProcessRoot = node;
            this.lvwNavigater.Items.Clear();
            foreach (TreeNode node2 in node.Nodes)
            {
                this.AddOneProcess(node2);
            }
        }
    }
}

