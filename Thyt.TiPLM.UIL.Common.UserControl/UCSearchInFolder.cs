    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.UIL.DeskLib.WinControls;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.PPM.Card;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCSearchInFolder : UserControlPLM, IUCAgent
    {
        private DEMetaClass curClass;
        private TreeNode currentNode;
        private ObjectImageList folderImage;
        private string FolderName = "";
        private Guid folderOid = Guid.Empty;
        private int iFolderType;
        private bool isAfterPrivExpand;
        private bool IsShowCheckOutClass = true;
        private bool IsShowMenu = true;
        private bool IsShowPrivateFolder = true;
        private bool IsShowPublicFolder = true;
        private bool IsShowRecycleBin = true;
        private ArrayList NodesByAll = new ArrayList();
        private Hashtable NodesExpanded = new Hashtable();
        private TreeNode SelectedNode;

        public UCSearchInFolder()
        {
            if (ClientData.MyImageList == null)
            {
                ClientData.MyImageList = new ObjectImageList();
            }
            this.folderImage = ClientData.MyImageList;
            this.InitializeComponent();
            this.dropDownFolderTree.Imagelist = this.folderImage.imageList;
            this.LoadFolderTree();
            this.InitializeUCEvent();
        }

        private void ClsNodeAddTmpNode(TreeNode root)
        {
            ArrayList classNames = new ArrayList();
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Tag is DEMetaClass)
                {
                    classNames.Add(((DEMetaClass) node.Tag).Name);
                }
            }
            Hashtable checkedOutItemsCount = PLItem.Agent.GetCheckedOutItemsCount(classNames, ClientData.LogonUser.Oid);
            foreach (TreeNode node2 in root.Nodes)
            {
                if (((node2.Tag is DEMetaClass) && checkedOutItemsCount.ContainsKey(((DEMetaClass) node2.Tag).Name)) && (Convert.ToInt32(checkedOutItemsCount[((DEMetaClass) node2.Tag).Name]) > 0))
                {
                    node2.Nodes.Add(new TreeNode());
                }
            }
        }

        private void DisPlayCheckOutFolder(TreeNode checkRoot)
        {
            checkRoot.Nodes.Clear();
            ArrayList list = new ArrayList { 
                "OBJECTSTATE",
                "PPCLASSIFY",
                "PPCRDTEMPLATE",
                "PPROUTENODE",
                "PPSIGNTEMPLATE",
                "RESOURCE",
                "FORM"
            };
            ArrayList roots = new ArrayList();
            if (checkRoot.Tag is string)
            {
                roots = UIDataModel.GetRoots();
            }
            foreach (DEMetaClass class2 in roots)
            {
                if (!list.Contains(class2.Name) && (class2.SystemClass == 'N'))
                {
                    TreeNode node = new TreeNode(class2.Label, this.folderImage.GetIconIndex("ICO_FDL_CHECKOUT_CLOSE"), this.folderImage.GetIconIndex("ICO_FDL_CHECKOUT_OPEN")) {
                        Tag = class2
                    };
                    checkRoot.Nodes.Add(node);
                }
            }
            this.ClsNodeAddTmpNode(checkRoot);
            checkRoot.Expand();
        }

        private void DisPlayFolder(Guid folderOid)
        {
            try
            {
                DEFolder2 folder = PLFolder.RemotingAgent.GetFolder(folderOid);
                string resName = "ICO_FDL_PUBLIC_CLOSE";
                string str2 = "ICO_FDL_PUBLIC_OPEN";
                if (folder.FolderEffway == RevisionEffectivityWay.PreciseIter)
                {
                    resName = "ICO_FDL_PRECISEITER_CLOSE";
                    str2 = "ICO_FDL_PRECISEITER_OPEN";
                }
                else if (folder.FolderEffway == RevisionEffectivityWay.LastReleasedRev)
                {
                    resName = "ICO_FDL_LASTRELEASEREV_CLOSE";
                    str2 = "ICO_FDL_LASTRELEASEREV_OPEN";
                }
                TreeNode node = new TreeNode(folder.Name, this.folderImage.GetIconIndex(resName), this.folderImage.GetIconIndex(str2)) {
                    Tag = folder
                };
                if (folder.CanExpand)
                {
                    node.Nodes.Add(new TreeNode());
                }
                this.dropDownFolderTree.Nodes.Add(node);
                this.SelectedNode = null;
                this.SelectedNode = node;
                node.Expand();
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
            }
            catch (Exception exception2)
            {
                PrintException.Print(exception2);
            }
        }

        private void DisPlayFolder(string FolderType)
        {
            if (FolderType == "个人文件夹")
            {
                this.DisPlayPrivFolder(this.PrvRoot);
            }
            else
            {
                this.DisPlayPubvFolder(this.PubRoot);
            }
        }

        private void DisPlayPrivFolder(TreeNode priRoot)
        {
            priRoot.Nodes.Clear();
            ArrayList topFolders = new ArrayList();
            try
            {
                topFolders = PLFolder.RemotingAgent.GetTopFolders(ClientData.LogonUser.Oid, false);
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
                return;
            }
            catch (Exception exception2)
            {
                PrintException.Print(exception2);
                return;
            }
            string sortType = ClientData.GetSortType("SORT_FOLDER");
            IComparer comparer = null;
            if (sortType != "")
            {
                comparer = new SortArraylist(sortType, "SORT_FOLDER");
            }
            else
            {
                comparer = new SortArraylist("NAME", "SORT_FOLDER");
            }
            topFolders.Sort(comparer);
            foreach (DEFolder2 folder in topFolders)
            {
                string resName = "ICO_FDL_PUBLIC_CLOSE";
                string str3 = "ICO_FDL_PUBLIC_OPEN";
                if (folder.FolderEffway == RevisionEffectivityWay.PreciseIter)
                {
                    resName = "ICO_FDL_PRECISEITER_CLOSE";
                    str3 = "ICO_FDL_PRECISEITER_OPEN";
                }
                else if (folder.FolderEffway == RevisionEffectivityWay.LastReleasedRev)
                {
                    resName = "ICO_FDL_LASTRELEASEREV_CLOSE";
                    str3 = "ICO_FDL_LASTRELEASEREV_OPEN";
                }
                TreeNode node = new TreeNode(folder.Name, this.folderImage.GetIconIndex(resName), this.folderImage.GetIconIndex(str3)) {
                    Tag = folder
                };
                if (folder.CanExpand)
                {
                    node.Nodes.Add(new TreeNode());
                }
                priRoot.Nodes.Add(node);
            }
            if (!priRoot.IsExpanded)
            {
                this.isAfterPrivExpand = true;
            }
            priRoot.Expand();
        }

        private void DisPlayPubvFolder(TreeNode pubRoot)
        {
            pubRoot.Nodes.Clear();
            ArrayList topFolders = new ArrayList();
            try
            {
                topFolders = PLFolder.RemotingAgent.GetTopFolders(ClientData.LogonUser.Oid, true);
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
                return;
            }
            catch (Exception exception2)
            {
                PrintException.Print(exception2);
                return;
            }
            string sortType = ClientData.GetSortType("SORT_FOLDER");
            IComparer comparer = null;
            if (sortType != "")
            {
                comparer = new SortArraylist(sortType, "SORT_FOLDER");
            }
            else
            {
                comparer = new SortArraylist("NAME", "SORT_FOLDER");
            }
            topFolders.Sort(comparer);
            foreach (DEFolder2 folder in topFolders)
            {
                string resName = "ICO_FDL_PUBLIC_CLOSE";
                string str3 = "ICO_FDL_PUBLIC_OPEN";
                if (folder.FolderEffway == RevisionEffectivityWay.PreciseIter)
                {
                    resName = "ICO_FDL_PRECISEITER_CLOSE";
                    str3 = "ICO_FDL_PRECISEITER_OPEN";
                }
                else if (folder.FolderEffway == RevisionEffectivityWay.LastReleasedRev)
                {
                    resName = "ICO_FDL_LASTRELEASEREV_CLOSE";
                    str3 = "ICO_FDL_LASTRELEASEREV_OPEN";
                }
                TreeNode node = new TreeNode(folder.Name, this.folderImage.GetIconIndex(resName), this.folderImage.GetIconIndex(str3)) {
                    Tag = folder
                };
                if (folder.CanExpand)
                {
                    node.Nodes.Add(new TreeNode());
                }
                pubRoot.Nodes.Add(node);
            }
            pubRoot.Expand();
        }

        private void DisPlaySubFolder(TreeNode pNode)
        {
            if (((pNode != null) && !(pNode.Tag is string)) && (pNode.Tag is DEFolder2))
            {
                Cursor.Current = Cursors.WaitCursor;
                DEFolder2 tag = (DEFolder2) pNode.Tag;
                pNode.Nodes.Clear();
                try
                {
                    ArrayList subFolders = PLFolder.RemotingAgent.GetSubFolders(ClientData.LogonUser.Oid, tag.Oid);
                    string sortType = ClientData.GetSortType("SORT_FOLDER");
                    IComparer comparer = null;
                    if (sortType != "")
                    {
                        comparer = new SortArraylist(sortType, "SORT_FOLDER");
                    }
                    else
                    {
                        comparer = new SortArraylist("NAME", "SORT_FOLDER");
                    }
                    subFolders.Sort(comparer);
                    foreach (DEFolder2 folder2 in subFolders)
                    {
                        TreeNode node = new TreeNode(folder2.Name, this.folderImage.GetObjectImage("folder", "close"), this.folderImage.GetObjectImage("folder", "open")) {
                            Tag = folder2
                        };
                        pNode.Nodes.Add(node);
                        if (folder2.CanExpand)
                        {
                            node.Nodes.Add(new TreeNode());
                        }
                    }
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    PrintException.Print(exception2);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void DisposeUCEvent()
        {
            this.dropDownFolderTree.treeView.BeforeCollapse -= new TreeViewCancelEventHandler(this.PLMFolderTree_BeforeCollapse);
            this.dropDownFolderTree.treeView.AfterSelect -= new TreeViewEventHandler(this.PLMFolderTree_AfterSelect);
            this.dropDownFolderTree.treeView.MouseDown -= new MouseEventHandler(this.PLMFolderTree_MouseDown);
        }

        private TreeNode FindRootNode(string FolderType)
        {
            foreach (TreeNode node in this.dropDownFolderTree.Nodes)
            {
                if (node.Tag.Equals(FolderType))
                {
                    return node;
                }
            }
            return null;
        }

        public string GetValue()
        {
            string str = "SearchInFolder(";
            if (string.IsNullOrEmpty(this.dropDownFolderTree.TextValue))
            {
                MessageBoxPLM.Show("文件夹路径没有定义", "在文件夹中查询对象函数", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
            }
            if (this.dropDownFolderTree.treeView.SelectedNode.Parent == null)
            {
                if (this.dropDownFolderTree.treeView.SelectedNode.FullPath == "公共文件夹")
                {
                    str = str + Guid.Empty.ToString("N");
                }
                else
                {
                    str = str + ClientData.LogonUser.Oid.ToString("N");
                }
            }
            else
            {
                DEFolder2 tag = this.dropDownFolderTree.treeView.SelectedNode.Tag as DEFolder2;
                str = str + tag.Oid.ToString("N");
            }
            if (this.chContainSubFolder.Checked)
            {
                str = str + ",true";
            }
            else
            {
                str = str + ",false";
            }
            if (this.iFolderType == 0)
            {
                str = str + ",private,";
            }
            else
            {
                str = str + ",public,";
            }
            if (this.dropDownFolderTree.treeView.SelectedNode.Parent == null)
            {
                if (this.dropDownFolderTree.treeView.SelectedNode.FullPath == "公共文件夹")
                {
                    str = str + "C";
                }
                else
                {
                    str = str + "P";
                }
            }
            else
            {
                DEFolder2 folder2 = this.dropDownFolderTree.treeView.SelectedNode.Tag as DEFolder2;
                str = str + folder2.Status.ToString();
            }
            return ((str + "," + ClientData.LogonUser.Oid.ToString("N")) + ")");
        }

        protected void InitializeUCEvent()
        {
            this.dropDownFolderTree.treeView.BeforeCollapse += new TreeViewCancelEventHandler(this.PLMFolderTree_BeforeCollapse);
            this.dropDownFolderTree.treeView.AfterSelect += new TreeViewEventHandler(this.PLMFolderTree_AfterSelect);
            this.dropDownFolderTree.treeView.MouseDown += new MouseEventHandler(this.PLMFolderTree_MouseDown);
        }

        private void LoadFolderTree()
        {
            this.IsShowRecycleBin = false;
            this.IsShowCheckOutClass = this.IsShowCheckOutClass;
            this.IsShowPrivateFolder = this.IsShowPrivateFolder;
            this.IsShowPublicFolder = this.IsShowPublicFolder;
            this.IsShowMenu = this.IsShowMenu;
            this.dropDownFolderTree.treeView.ImageList = this.folderImage.imageList;
            if (this.IsShowPrivateFolder)
            {
                this.DisPlayPrivFolder(this.PrvRoot);
                this.PrvRoot.Collapse();
            }
            if (this.IsShowPublicFolder)
            {
                this.DisPlayPubvFolder(this.PubRoot);
                this.PubRoot.Collapse();
            }
            if (this.IsShowRecycleBin)
            {
                TreeNode reRoot = this.ReRoot;
            }
        }

        private bool LocationChdFolderNode(TreeNode tvMain, Guid folderOid)
        {
            if (tvMain.Nodes.Count > 0)
            {
                foreach (TreeNode node in tvMain.Nodes)
                {
                    if (!string.IsNullOrEmpty(node.Text))
                    {
                        this.dropDownFolderTree.treeView.SelectedNode = node;
                        DEFolder2 tag = node.Tag as DEFolder2;
                        if (tag.Oid == folderOid)
                        {
                            this.dropDownFolderTree.TextValue = node.Text;
                            return true;
                        }
                        if (this.LocationChdFolderNode(node, folderOid))
                        {
                            node.Expand();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void LocationFolderNode(TreeView tvMain, Guid folderOid)
        {
            if (tvMain.Nodes.Count > 0)
            {
                foreach (TreeNode node in tvMain.Nodes)
                {
                    this.dropDownFolderTree.treeView.SelectedNode = node;
                    bool flag = false;
                    if (node.Text == "个人文件夹")
                    {
                        if (ClientData.LogonUser.Oid == folderOid)
                        {
                            this.dropDownFolderTree.TextValue = node.Text;
                            break;
                        }
                        flag = true;
                    }
                    if (node.Text == "公共文件夹")
                    {
                        if (Guid.Empty == folderOid)
                        {
                            this.dropDownFolderTree.TextValue = node.Text;
                            break;
                        }
                        flag = true;
                    }
                    if (!flag)
                    {
                        DEFolder2 tag = node.Tag as DEFolder2;
                        if (tag.Oid == folderOid)
                        {
                            tvMain.SelectedNode = node;
                            this.dropDownFolderTree.TextValue = node.Text;
                            break;
                        }
                    }
                    if (this.LocationChdFolderNode(node, folderOid))
                    {
                        node.Expand();
                        break;
                    }
                }
            }
        }

        public string ParseValue(string str_funcvalue) {
            return "wyl";
        }
        private void PLMFolderTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                try
                {
                    this.currentNode = e.Node;
                    if (this.currentNode.Tag is DEFolder2)
                    {
                        if (this.NodesExpanded[this.currentNode] == null)
                        {
                            this.NodesExpanded.Add(this.currentNode, true);
                        }
                        this.DisPlaySubFolder(this.currentNode);
                        DEFolder2 tag = this.currentNode.Tag as DEFolder2;
                        switch (tag.FolderType)
                        {
                            case 'A':
                                this.iFolderType = 1;
                                return;

                            case 'B':
                                this.iFolderType = 0;
                                return;

                            case 'C':
                                this.iFolderType = 1;
                                return;

                            case 'P':
                                this.iFolderType = 0;
                                return;
                        }
                    }
                    else if (this.currentNode.Tag is string)
                    {
                        if (this.NodesExpanded[this.currentNode] == null)
                        {
                            this.NodesExpanded.Add(this.currentNode, true);
                        }
                        if (this.currentNode == this.PubRoot)
                        {
                            this.DisPlayPubvFolder(this.currentNode);
                            this.iFolderType = 1;
                        }
                        else if (this.currentNode == this.PrvRoot)
                        {
                            this.DisPlayPrivFolder(this.currentNode);
                            this.iFolderType = 0;
                        }
                    }
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
            }
        }

        private void PLMFolderTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if ((e.Node != null) && this.isAfterPrivExpand)
            {
                this.isAfterPrivExpand = false;
                e.Cancel = true;
            }
        }

        private void PLMFolderTree_MouseDown(object sender, MouseEventArgs e)
        {
            this.dropDownFolderTree.Focus();
            this.currentNode = this.dropDownFolderTree.treeView.GetNodeAt(new Point(e.X, e.Y));
            this.dropDownFolderTree.SelectedNode = this.currentNode;
        }

        public void SetInput(object o_in)
        {
            if (((o_in != null) && (o_in is ArrayList)) && ((o_in as ArrayList).Count != 0))
            {
                string str = (o_in as ArrayList)[1].ToString();
                if (str.StartsWith("SearchInFolder(") && str.EndsWith(")"))
                {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in)
        {
            string str = str_in.Substring("SearchInFolder".Length + 1, (str_in.Length - "SearchInFolder".Length) - 2);
            if (!string.IsNullOrEmpty(str))
            {
                char[] separator = ",".ToCharArray();
                string[] strArray = str.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = strArray[i].ToString();
                    if ((i == 0) && !string.IsNullOrEmpty(str3))
                    {
                        Guid folderOid = new Guid(str3);
                        this.LocationFolderNode(this.dropDownFolderTree.treeView, folderOid);
                    }
                    if ((i == 1) && (str3.ToLower().Trim() == "true"))
                    {
                        this.chContainSubFolder.Checked = true;
                    }
                }
            }
        }

        private TreeNode PrvRoot
        {
            get
            {
                TreeNode node = this.FindRootNode("个人文件夹");
                if (node == null)
                {
                    node = new TreeNode("个人文件夹", this.folderImage.GetIconIndex("ICO_FDL_PUBLIC_CLOSE"), this.folderImage.GetIconIndex("ICO_FDL_PUBLIC_OPEN")) {
                        Tag = "个人文件夹"
                    };
                    this.dropDownFolderTree.Nodes.Add(node);
                }
                return node;
            }
        }

        private TreeNode PubRoot
        {
            get
            {
                TreeNode node = this.FindRootNode("公共文件夹");
                if (node == null)
                {
                    node = new TreeNode("公共文件夹", this.folderImage.GetIconIndex("ICO_FDL_PUBLIC_CLOSE"), this.folderImage.GetIconIndex("ICO_FDL_PUBLIC_OPEN")) {
                        Tag = "公共文件夹"
                    };
                    this.dropDownFolderTree.Nodes.Add(node);
                }
                return node;
            }
        }

        public TreeNode ReRoot
        {
            get
            {
                TreeNode node = this.FindRootNode("回收站");
                if (node == null)
                {
                    string resName = "ICO_FDL_RECYCLE_FULL";
                    if (!PLFolder.RemotingAgent.HasRecylceItems(ClientData.LogonUser.Oid))
                    {
                        resName = "ICO_FDL_RECYCLE_EMPTY";
                    }
                    node = new TreeNode("回收站", this.folderImage.GetIconIndex(resName), this.folderImage.GetIconIndex(resName)) {
                        Tag = "回收站"
                    };
                    this.dropDownFolderTree.Nodes.Add(node);
                }
                return node;
            }
        }

        private class SortArraylist : IComparer
        {
            private string ArributeName;
            private bool ascending;
            private string ClassName;

            public SortArraylist(string ArributeName, string ClassName) : this(ArributeName, ClassName, ClientData.IsAscending)
            {
            }

            public SortArraylist(string ArributeName, string ClassName, bool ascending)
            {
                this.ascending = true;
                this.ArributeName = ArributeName;
                this.ClassName = ClassName;
                this.ascending = ascending;
            }

            private int CompareBizItem(object x, object y)
            {
                switch (this.ArributeName)
                {
                    case "ID":
                        if (this.ascending)
                        {
                            return ((DEBusinessItem) x).Master.Id.CompareTo(((DEBusinessItem) y).Master.Id);
                        }
                        return ((DEBusinessItem) y).Master.Id.CompareTo(((DEBusinessItem) x).Master.Id);

                    case "CLASS":
                        if (this.ascending)
                        {
                            return this.LabelByClass(((DEBusinessItem) x).Master.ClassName).CompareTo(this.LabelByClass(((DEBusinessItem) y).Master.ClassName));
                        }
                        return this.LabelByClass(((DEBusinessItem) y).Master.ClassName).CompareTo(this.LabelByClass(((DEBusinessItem) x).Master.ClassName));

                    case "STATE":
                        if (this.ascending)
                        {
                            return ((DEBusinessItem) x).Master.Status.ToString().CompareTo(((DEBusinessItem) y).Master.Status.ToString());
                        }
                        return ((DEBusinessItem) y).Master.Status.ToString().CompareTo(((DEBusinessItem) x).Master.Status.ToString());

                    case "CREATETIME":
                        if (this.ascending)
                        {
                            return ((DEBusinessItem) x).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DEBusinessItem) y).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        return ((DEBusinessItem) y).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DEBusinessItem) x).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return 0;
            }

            private int CompareSBizItem(object x, object y)
            {
                switch (this.ArributeName)
                {
                    case "ID":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).Id.CompareTo(((DESmartBizItem) y).Id);
                        }
                        return ((DESmartBizItem) y).Id.CompareTo(((DESmartBizItem) x).Id);

                    case "CLASS":
                        if (this.ascending)
                        {
                            return this.LabelByClass(((DESmartBizItem) x).ClassName).CompareTo(this.LabelByClass(((DESmartBizItem) y).ClassName));
                        }
                        return this.LabelByClass(((DESmartBizItem) y).ClassName).CompareTo(this.LabelByClass(((DESmartBizItem) x).ClassName));

                    case "STATE":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).Status.ToString().CompareTo(((DESmartBizItem) y).Status.ToString());
                        }
                        return ((DESmartBizItem) y).Status.ToString().CompareTo(((DESmartBizItem) x).Status.ToString());

                    case "CREATETIME":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DESmartBizItem) y).CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        return ((DESmartBizItem) y).CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DESmartBizItem) x).CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return 0;
            }

            private int CompareSbzBzItem(object x, object y)
            {
                switch (this.ArributeName)
                {
                    case "ID":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).Id.CompareTo(((DEBusinessItem) y).Master.Id);
                        }
                        return ((DEBusinessItem) y).Master.Id.CompareTo(((DESmartBizItem) x).Id);

                    case "CLASS":
                        if (this.ascending)
                        {
                            return this.LabelByClass(((DESmartBizItem) x).ClassName).CompareTo(this.LabelByClass(((DEBusinessItem) y).Master.ClassName));
                        }
                        return this.LabelByClass(((DEBusinessItem) y).Master.ClassName).CompareTo(this.LabelByClass(((DESmartBizItem) x).ClassName));

                    case "STATE":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).Status.ToString().CompareTo(((DEBusinessItem) y).Master.Status.ToString());
                        }
                        return ((DEBusinessItem) y).Master.Status.ToString().CompareTo(((DESmartBizItem) x).Status.ToString());

                    case "CREATETIME":
                        if (this.ascending)
                        {
                            return ((DESmartBizItem) x).CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DEBusinessItem) y).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        return ((DEBusinessItem) y).Revision.CreateTime.ToString("yyyy-MM-dd HH:mm:ss").CompareTo(((DESmartBizItem) x).CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                return 0;
            }

            private string LabelByClass(string clsName){
               return ModelContext.MetaModel.GetClassLabel(clsName);
            }

            int IComparer.Compare(object x, object y)
            {
                switch (this.ClassName)
                {
                    case "SORT_FOLDER":
                        switch (this.ArributeName)
                        {
                            case "NAME":
                                if (this.ascending)
                                {
                                    return ((DEFolder2) x).Name.CompareTo(((DEFolder2) y).Name);
                                }
                                return ((DEFolder2) y).Name.CompareTo(((DEFolder2) x).Name);

                            case "CREATETIME":
                                if (this.ascending)
                                {
                                    return ((DEFolder2) x).CreateTime.CompareTo(((DEFolder2) y).CreateTime);
                                }
                                return ((DEFolder2) y).CreateTime.CompareTo(((DEFolder2) x).CreateTime);
                        }
                        return 0;

                    case "SORT_CLITEM":
                        if ((x is DEBusinessItem) && (y is DEBusinessItem))
                        {
                            return this.CompareBizItem(x, y);
                        }
                        if ((x is DESmartBizItem) && (y is DESmartBizItem))
                        {
                            return this.CompareSBizItem(x, y);
                        }
                        if ((x is DESmartBizItem) && (y is DEBusinessItem))
                        {
                            return this.CompareSbzBzItem(x, y);
                        }
                        if (((x is DEBusinessItem) && (y is DESmartBizItem)) && (this.CompareSbzBzItem(y, x) == 0))
                        {
                            return 1;
                        }
                        return 0;

                    case "PictureInfo":
                        if (this.ArributeName != "LeftToCell")
                        {
                            return 0;
                        }
                        if (this.ascending)
                        {
                            return ((PictureInfo) x).LeftToCell.CompareTo(((PictureInfo) y).LeftToCell);
                        }
                        return ((PictureInfo) y).LeftToCell.CompareTo(((PictureInfo) x).LeftToCell);
                }
                return 0;
            }
        }
    }
}

