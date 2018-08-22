    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCResGrid : UserControlPLM {
        private int AlertPageNum;
        private ArrayList attrFilterList;
        private ArrayList attrList;
        private ArrayList attrOuter;
        private ArrayList attrShow;
        private ArrayList attrSort;
        private bool b_byCAPP;
        private bool b_DoubleClisk;
        private bool b_first;
        private bool b_refType;
        private bool b_resize;
        private bool b_start;
        private bool b_stateShow;
        private bool b_TreeCanView;
        private bool b_TreeShow;

        private string className;
        private Guid classOid;
        public bool CloseWhenActivated;
        private string clsName;
        private DEResFolder curFolder;
        private DataSet ds;
        private emResourceType emResType;
        private Hashtable HT_AttrIsView;
        private int i_orgHeight;
        private int i_orgWidth;
        private int i_startX;
        private int i_startY;
        private int int_rowCount;
        private int int_SelectedItemCol;
        private bool isAllBtnClick;
        private bool IsValTree;
        private ListViewItem lastHighLightItem;
        private DEResModel myModel;
        private DEResFolder myResFolder;
        private DataView myView;
        private DataView orgView;
        private int pageNum;

        private int showNum;

        private string str_CurClsLbl;
        private string strFilter;
        private string strSort;
        private DataSet theDataSet;

        private int TotalPageNum;

        private Guid userOid;

        public event ResDropListHandler ResSelected;

        public UCResGrid() {
            this.CloseWhenActivated = true;
            this.IsValTree = true;
            this.b_start = true;
            this.curFolder = new DEResFolder();
            this.myResFolder = new DEResFolder();
            this.b_stateShow = true;
            this.b_TreeShow = true;
            this.b_TreeCanView = true;
            this.b_DoubleClisk = true;
            this.str_CurClsLbl = "";
            this.attrList = new ArrayList();
            this.theDataSet = new DataSet();
            this.strFilter = "";
            this.pageNum = 1;
            this.showNum = 50;
            this.AlertPageNum = 10;
            this.strSort = "";
            this.myView = new DataView();
            this.orgView = new DataView();
            this.clsName = "";
            this.attrFilterList = new ArrayList();
            this.attrSort = new ArrayList();
            this.attrOuter = new ArrayList();
            this.attrShow = new ArrayList();
            this.HT_AttrIsView = new Hashtable();
            this.InitializeComponent();
        }

        public UCResGrid(Guid g_clsoid)
            : this() {
            this.classOid = g_clsoid;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
            this.className = class2.Name;
            this.AddIcon();
            this.userOid = ClientData.LogonUser.Oid;
            this.InitObject();
        }

        public UCResGrid(string clsName)
            : this() {
            this.className = clsName;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
            if (class2 != null) {
                this.classOid = class2.Oid;
                this.AddIcon();
                this.userOid = ClientData.LogonUser.Oid;
                this.emResType = emResourceType.Customize;
                this.InitObject();
            }
        }

        private void AddChildNode(DEResModelPrimaryKey defathermpk, ArrayList al_modelpks, TreeNode theNode) {
            foreach (DEResModelPrimaryKey key in al_modelpks) {
                if (defathermpk.PLM_OID == key.PLM_FATHEROID) {
                    TreeNode node = new TreeNode {
                        Text = key.PLM_SHOWNAME,
                        Tag = key
                    };
                    theNode.Nodes.Add(node);
                }
            }
        }

        private void AddChildNodeVT(DEResModelPrimaryKey defathermpk, ArrayList al_modelpks, TreeNode theNode) {
            foreach (DEResModelPrimaryKey key in al_modelpks) {
                if ((defathermpk.PLM_OID == key.PLM_FATHEROID) && key.PLM_ISVIRTUAL) {
                    TreeNode node = new TreeNode {
                        Text = key.PLM_SHOWNAME
                    };
                    ArrayList list = new ArrayList {
                        key
                    };
                    node.Tag = list;
                    theNode.Nodes.Add(node);
                }
            }
        }

        private void AddIcon() {
            string[] resNames = new string[] { "ICO_EVT_MONITOR", "ICO_FDL_OPEN", "ICO_FDL_CLOSE", "ICO_RES_PRE", "ICO_RES_NXT", "ICO_RES_CLEAR", "ICO_RES_REFRESH", "ICO_RES_FDL_OPEN", "ICO_RES_FDL", "ICO_RES_NODE" };
            ClientData.MyImageList.AddIcons(resNames);
            this.toolStrip1.ImageList = ClientData.MyImageList.imageList;
            this.btnHide.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_NAVIGATOR");
            this.btnSort.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_EVT_MONITOR");
            this.btnShowMPage.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN");
            this.btnPrevPage.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PRE");
            this.btnNextPage.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NXT");
        }

        private void BingdingData() {
            if (this.theDataSet == null) {
                this.SetTurnPageUnState();
                this.SetUnMpageState();
            } else if (this.int_rowCount == 0) {
                this.SetTurnPageUnState();
                this.SetUnMpageState();
                this.SetStatusInfo(0, 0, 0);
                this.lvw.Items.Clear();
                if (this.b_TreeShow) {
                    int width = Screen.PrimaryScreen.Bounds.Width / 2;
                    this.SetPopupContainerWidth(width);
                    base.Width = width;
                }
            } else {
                this.orgView = this.GetOrgDataView(this.curFolder);
                this.myView = this.orgView;
                this.InitLvShowHeader();
                if ((string.IsNullOrEmpty(this.curFolder.FilterString) && this.b_TreeShow) && ((this.TV_Class.Nodes.Count > 0) && !this.b_start)) {
                    this.TV_Class.SelectedNode = this.TV_Class.Nodes[0];
                }
                this.showNum = 50;
                this.FirstDisplayData(this.orgView);
            }
        }

        private void btnHide_Click(object sender, EventArgs e) {
            if (this.b_TreeShow) {
                if (this.panel1.Visible) {
                    this.panel1.Visible = false;
                } else {
                    this.panel1.Visible = true;
                }
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e) {
            this.b_first = false;
            this.pageNum++;
            int count = 0;
            if (this.b_byCAPP) {
                count = this.theDataSet.Tables[0].DefaultView.Table.Rows.Count;
            } else {
                count = this.int_rowCount;
            }
            if (count <= (this.showNum * this.pageNum)) {
                this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), count);
                this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                new DECopyData();
                new CLCopyData();
                this.SetTurnPageEnd();
            } else {
                this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), this.showNum + (this.showNum * (this.pageNum - 1)));
                this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                this.SetTurnPageMiddle();
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e) {
            this.b_first = false;
            this.pageNum--;
            if (this.pageNum < 1) {
                MessageBoxPLM.Show("PageNum < 1 ", "工程资源", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            } else {
                this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), this.showNum + (this.showNum * (this.pageNum - 1)));
                this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                new DECopyData();
                new CLCopyData();
                if (this.pageNum == 1) {
                    this.btnPrevPage.Enabled = false;
                }
                this.btnNextPage.Enabled = true;
            }
        }

        private void btnShowMPage_Click(object sender, EventArgs e) {
            this.b_first = false;
            if (this.btnShowMPage.Checked) {
                if (this.int_rowCount > (this.AlertPageNum * 50)) {
                    this.btnShowMPage.Checked = false;
                    MessageBoxPLM.Show("总记录数超过" + ((this.AlertPageNum * 50)).ToString() + "条时，不能全部显示！", "普通提示");
                } else {
                    this.isAllBtnClick = true;
                    this.SetUnMpageState();
                    this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                    if (this.int_rowCount == 0) {
                        this.SetStatusInfo(0, 0, 0);
                    } else {
                        this.SetStatusInfo(1, 1, this.int_rowCount);
                    }
                    this.isAllBtnClick = false;
                }
            } else {
                this.FirstDisplayData(this.orgView);
            }
        }

        private void btnSort_Click(object sender, EventArgs e) {
            FrmSortDef def;
            this.b_first = false;
            string optionName = "";
            if (this.b_refType) {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                def = new FrmSortDef(ModelContext.MetaModel.GetClass(class2.RefClass).Oid, true, this.curFolder.Oid, true);
            } else {
                def = new FrmSortDef(this.curFolder.ClassOid, true, this.curFolder.Oid);
            }
            if (def.ShowDialog() == DialogResult.OK) {
                this.attrSort = def.SaveSortAttr;
                optionName = this.userOid + this.curFolder.Oid.ToString();
                if (this.attrSort.Count > 0) {
                    string optionValue = "";
                    string str3 = "";
                    this.InitSortList(false);
                    optionValue = this.strSort;
                    ClientData.SetUserOption(optionName, optionValue);
                    for (int i = 0; i < this.attrSort.Count; i++) {
                        DEDefSort sort = (DEDefSort)this.attrSort[i];
                        string str4 = "";
                        str4 = sort.ISASC ? "asc" : "desc";
                        if (i == 0) {
                            str3 = sort.ATTROID.ToString() + "|" + str4;
                        } else {
                            string str5 = str3;
                            str3 = str5 + "," + sort.ATTROID.ToString() + "|" + str4;
                        }
                    }
                    ClientData.SetUserOption(optionName + "_def", str3);
                } else {
                    ClientData.SetUserOption(optionName, "nothing");
                    ClientData.SetUserOption(optionName + "_def", "nothing");
                }
                try {
                    this.InitData();
                } catch (PLMException exception) {
                    PrintException.Print(exception);
                } catch (Exception exception2) {
                    MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.BingdingData();
            }
        }

        private void CloseParent() {
            if (base.Parent != null) {
                if (base.Parent is PopupContainerControl) {
                    PopupContainerControl parent = base.Parent as PopupContainerControl;
                    if ((parent != null) && (parent.OwnerEdit != null)) {
                        parent.OwnerEdit.ClosePopup();
                    }
                } else {
                    base.Parent.Hide();
                }
            }
        }

        private void cobPageSize_SelectedIndexChanged(object sender, EventArgs e) {
            if (!this.isAllBtnClick) {
                this.SetDpDnMPageState(this.cobPageSize.Text);
                if (this.cobPageSize.SelectedIndex == 0) {
                    if (this.int_rowCount > (this.AlertPageNum * 50)) {
                        this.btnShowMPage.Checked = false;
                        MessageBoxPLM.Show("总记录数超过" + ((this.AlertPageNum * 50)).ToString() + "条时，不能全部显示！", "普通提示");
                        this.cobPageSize.SelectedIndex = 1;
                    } else {
                        this.SetUnMpageState();
                        this.SetTurnPageUnState();
                        this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                        this.SetStatusInfo(1, 1, this.int_rowCount);
                    }
                } else {
                    if (this.int_rowCount > this.showNum) {
                        this.TotalPageNum = this.ComputTotalPageNum(this.int_rowCount, this.showNum);
                        this.SetTurnPageStart();
                    } else {
                        this.TotalPageNum = 1;
                        this.SetTurnPageUnState();
                    }
                    this.pageNum = 1;
                    this.SetLiveMpageState(this.cobPageSize.SelectedIndex);
                    this.ShowNumDataView(this.orgView, 0, this.showNum);
                    this.SetStatusInfo(1, this.TotalPageNum, this.int_rowCount);
                }
            }
        }

        private int ComputTotalPageNum(int i_total, int i_show) {
            return (((i_total % i_show) == 0) ? (i_total / i_show) : ((i_total / i_show) + 1));
        }

        private DEResFolder CreateFolder() {
            DEResFolder folder = new DEResFolder();
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
            if (class2.IsRefResClass) {
                class2 = ModelContext.MetaModel.GetClass(class2.RefClass);
            }
            folder.Oid = class2.Oid;
            folder.ClassOid = class2.Oid;
            folder.ClassName = class2.Name;
            return folder;
        }

        private DEResFolder CreateOrgFolder() {
            DEResFolder folder = new DEResFolder();
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.classOid);
            folder.Oid = class2.Oid;
            folder.ClassOid = class2.Oid;
            folder.ClassName = class2.Name;
            return folder;
        }

        public void DisplayResourceData() {
            if ((this.clsName != "") && (this.curFolder != null)) {
                try {
                    this.InitResStatus();
                    this.InitSortList(true);
                    this.SetAttrDataType();
                    this.InitData();
                } catch (Exception exception) {
                    MessageBoxPLM.Show("读取数据集发生错误" + exception.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                if (this.theDataSet == null) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.lvw.Items.Clear();
                } else if (this.int_rowCount == 0) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.SetStatusInfo(0, 0, 0);
                    this.lvw.Items.Clear();
                } else {
                    this.orgView = this.GetOrgDataView(this.curFolder);
                    this.myView = this.orgView;
                    this.InitLvShowHeader();
                    this.showNum = 50;
                    this.FirstDisplayData(this.orgView);
                }
            }
        }

        private void FilterData() {
            try {
                this.InitData();
                this.orgView = this.theDataSet.Tables[0].DefaultView;
                this.BingdingData();
            } catch (Exception exception) {
                MessageBoxPLM.Show("过滤数据发生错误:请检查输入的数据类型是否正确:" + exception.Message, "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void FindByKeyword() {
            try {
                if (this.curFolder.FilterString == null) {
                    this.curFolder.FilterString = "";
                }
                string[] strArray = this.curFolder.FilterString.Split(new string[] { "@#qwjs:" }, StringSplitOptions.None);
                if (!string.IsNullOrEmpty(this.txtKeyword.Text.Trim())) {
                    this.curFolder.FilterString = strArray[0] + "@#qwjs:" + this.txtKeyword.Text.Trim();
                } else {
                    this.curFolder.FilterString = strArray[0];
                }
                this.InitData();
                this.BingdingData();
            } catch {
                int num = -1;
                if (this.lvw.SelectedIndices.Count > 0) {
                    num = this.lvw.SelectedIndices[this.lvw.SelectedIndices.Count - 1];
                    if (num == (this.lvw.Items.Count - 1)) {
                        num = -1;
                    }
                }
                bool flag = false;
                foreach (ListViewItem item in this.lvw.Items) {
                    if (item.Index > num) {
                        for (int i = 0; i < item.SubItems.Count; i++) {
                            if (item.SubItems[i].Text.ToUpper().Contains(this.txtKeyword.Text.Trim().ToUpper())) {
                                item.SubItems[i].Font = new Font(item.Font.FontFamily, item.Font.Size, FontStyle.Italic | FontStyle.Bold);
                                flag = true;
                            }
                        }
                        if (flag) {
                            item.Selected = true;
                            this.lvw.EnsureVisible(item.Index);
                            if ((this.lastHighLightItem != null) && (this.lastHighLightItem != item)) {
                                foreach (ListViewItem.ListViewSubItem item2 in this.lastHighLightItem.SubItems) {
                                    if (item2.Font.Bold) {
                                        item2.Font = new Font(item.Font.FontFamily, item.Font.Size);
                                    }
                                }
                            }
                            this.lastHighLightItem = item;
                            return;
                        }
                    }
                }
                if (!flag && (this.lvw.SelectedItems.Count > 0)) {
                    this.lvw.SelectedItems[0].Selected = false;
                    this.FindByKeyword();
                }
            }
        }

        private void FirstDisplayData(DataView dv) {
            if (dv.Count == 0) {
                this.lvw.Items.Clear();
                this.SetTurnPageUnState();
                this.SetUnMpageState();
                this.SetStatusInfo(0, 0, 0);
            } else {
                this.LiveToolBarState();
                this.TotalPageNum = this.ComputTotalPageNum(this.int_rowCount, this.showNum);
                this.pageNum = 1;
                if (this.int_rowCount <= this.showNum) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                    if (this.int_rowCount == 0) {
                        this.SetStatusInfo(0, 0, 0);
                    } else {
                        this.SetStatusInfo(1, 1, this.int_rowCount);
                    }
                }
                if (this.int_rowCount > this.showNum) {
                    if (this.emResType == emResourceType.PLM) {
                        this.SetTurnPageUnState();
                        this.SetUnMpageState();
                        this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                        this.SetStatusInfo(1, 1, this.int_rowCount);
                    } else {
                        this.SetTurnPageStart();
                        this.SetLiveMpageState(1);
                        this.ShowNumDataView(this.orgView, 0, this.showNum);
                        this.SetStatusInfo(1, this.TotalPageNum, this.int_rowCount);
                    }
                }
            }
        }

        private void Get2LayerLeafNodeDataVT(TreeNode theNode) {
            ArrayList list = new ArrayList();
            ArrayList tag = (ArrayList)theNode.Tag;
            DEResModelPrimaryKey key = (DEResModelPrimaryKey)tag[0];
            DEMetaAttribute metaAttr = this.GetMetaAttr(key.PLM_ATTROID);
            this.GetParentNodeInfoVT(this.TV_Class.SelectedNode);
            foreach (string str in ResFunc.GetClassifiedDS(this.curFolder, key.PLM_ATTROID, this.curFolder.ClassName, metaAttr.Name, this.userOid, this.emResType, this.attrList, this.attrOuter)) {
                TreeNode node = new TreeNode {
                    Text = (str.Length == 0) ? "[空]" : str
                };
                ArrayList list3 = new ArrayList {
                    key,
                    metaAttr
                };
                node.Tag = list3;
                theNode.Nodes.Add(node);
            }
        }

        private void Get2LayerNodeData(TreeNode theNode) {
            ArrayList list = new ArrayList();
            new ArrayList();
            foreach (DEResModelPrimaryKey key in this.GetRoot(this.myModel.PLM_MODELPKLIST)) {
                TreeNode node = new TreeNode {
                    Text = key.PLM_SHOWNAME,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL"),
                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL_OPEN"),
                    Tag = key
                };
                theNode.Nodes.Add(node);
            }
        }

        private void Get2LayerNodeDataVT(TreeNode theNode) {
            ArrayList list = new ArrayList();
            new ArrayList();
            foreach (DEResModelPrimaryKey key in this.GetRoot(this.myModel.PLM_MODELPKLIST)) {
                TreeNode node = new TreeNode {
                    Text = key.PLM_SHOWNAME,
                    ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL"),
                    SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL_OPEN")
                };
                ArrayList list2 = new ArrayList {
                    key
                };
                node.Tag = list2;
                theNode.Nodes.Add(node);
            }
        }

        private ArrayList GetAttributes(string classname) {
            return ModelContext.MetaModel.GetAttributes(classname);
        }

        public string GetAttrValue(string str_lbl) {
            if (this.lvw.SelectedItems != null) {
                foreach (ListViewItem item in this.lvw.SelectedItems) {
                    DataRowView tag = (DataRowView)item.Tag;
                    try {
                        for (int i = 0; i < this.orgView.Table.Columns.Count; i++) {
                            foreach (DEMetaAttribute attribute in this.attrList) {
                                if (attribute.Label.Equals(str_lbl) && (this.orgView.Table.Columns[i].ColumnName == ("PLM_" + attribute.Name))) {
                                    string str2 = "PLM_" + attribute.Name;
                                    return tag[str2].ToString();
                                }
                            }
                        }
                    } catch (PLMException exception) {
                        PrintException.Print(exception);
                    } catch (Exception exception2) {
                        PrintException.Print(exception2);
                    }
                }
            }
            return "";
        }

        public string GetAttrValue(DEMetaAttribute deMetaAttri) {
            string str = "";
            try {
                if (this.lvw.SelectedItems != null) {
                    DataRowView tag = (DataRowView)this.lvw.SelectedItems[0].Tag;
                    if (tag == null) {
                        return str;
                    }
                    if (deMetaAttri == null) {
                        return "";
                    }
                    if (((deMetaAttri != null) && (deMetaAttri.LinkType == 1)) && (deMetaAttri.Combination != "")) {
                        string combination = deMetaAttri.Combination;
                        string str4 = combination;
                        foreach (DEMetaAttribute attribute in this.attrList) {
                            if (deMetaAttri.Combination.IndexOf("[" + attribute.Name + "]") > -1) {
                                string str3 = "PLM_" + attribute.Name;
                                combination = combination.Replace("[" + attribute.Name + "]", Convert.ToString(tag[str3]));
                                str4 = str4.Replace("[" + attribute.Name + "]", "");
                            }
                        }
                        if (this.b_refType) {
                            string str5 = "PLM_M_ID";
                            combination = combination.Replace("[ID]", Convert.ToString(tag[str5]));
                            str4 = str4.Replace("[ID]", "");
                        }
                        if (combination == str4) {
                            combination = "";
                        }
                        return combination;
                    }
                    if ((deMetaAttri.LinkType == 0) && (deMetaAttri.DataType == 8)) {
                        str = tag["PLM_ID"].ToString();
                    }
                }
                return str;
            } catch (PLMException exception) {
                PrintException.Print(exception);
            } catch (Exception exception2) {
                PrintException.Print(exception2);
            }
            return str;
        }

        public string GetClassLable() {
            return this.str_CurClsLbl;
        }
        private int GetDefAttrWidth(DEMetaAttribute demattr) {
            if (this.attrShow.Count > 0) {
                foreach (DEDefAttr attr in this.attrShow) {
                    if (demattr.Oid == attr.ATTROID) {
                        return attr.WIDTH;
                    }
                }
            }
            return 0;
        }

        private ArrayList GetItemLST(string strSBLTAB) {
            ArrayList list = new ArrayList();
            ArrayList itemMasters = PLItem.Agent.GetItemMasters(strSBLTAB, ClientData.LogonUser.Oid);
            ArrayList masterOids = new ArrayList(itemMasters.Count);
            ArrayList revNums = new ArrayList(itemMasters.Count);
            foreach (DEItemMaster2 master in itemMasters) {
                masterOids.Add(master.Oid);
                revNums.Add(0);
            }
            Guid curView = ClientData.UserGlobalOption.CurView;
            return PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
        }

        private void GetLeafNodeData(TreeNode theNode) {
            ArrayList list = new ArrayList();
            DEResModelPrimaryKey tag = (DEResModelPrimaryKey)theNode.Tag;
            DEMetaAttribute metaAttr = this.GetMetaAttr(tag.PLM_ATTROID);
            this.GetParentNodeInfo(this.TV_Class.SelectedNode);
            foreach (string str in ResFunc.GetClassifiedDS(this.curFolder, tag.PLM_ATTROID, this.curFolder.ClassName, metaAttr.Name, this.userOid, this.emResType, this.attrList, this.attrOuter)) {
                TreeNode node = new TreeNode {
                    Text = (str.Length == 0) ? "[空]" : str,
                    Tag = metaAttr
                };
                theNode.Nodes.Add(node);
            }
        }

        private void GetLeafNodeDataVT(TreeNode theNode, DEResModelPrimaryKey dmpk) {
            ArrayList list = new ArrayList();
            DEMetaAttribute metaAttr = this.GetMetaAttr(dmpk.PLM_ATTROID);
            this.GetParentNodeInfoVT(this.TV_Class.SelectedNode);
            foreach (string str in ResFunc.GetClassifiedDS(this.curFolder, dmpk.PLM_ATTROID, this.curFolder.ClassName, metaAttr.Name, this.userOid, this.emResType, this.attrList, this.attrOuter)) {
                TreeNode node = new TreeNode {
                    Text = (str.Length == 0) ? "[空]" : str
                };
                ArrayList list2 = new ArrayList {
                    dmpk,
                    metaAttr
                };
                node.Tag = list2;
                theNode.Nodes.Add(node);
            }
        }

        private Form GetMainFrom() {
            return new Form();
        }
        private DEMetaAttribute GetMetaAttr(Guid g_oid) {
            foreach (DEMetaAttribute attribute in this.attrList) {
                if (attribute.Oid == g_oid) {
                    return attribute;
                }
            }
            return new DEMetaAttribute();
        }

        private DEMetaAttribute GetMetaAttrInNode(ArrayList al_in) {
            if (al_in.Count == 2) {
                return (DEMetaAttribute)al_in[1];
            }
            return null;
        }

        private int GetMouseIndex(int curX) {
            int x = this.lvw.GetItemRect(0).Location.X;
            int num2 = 0;
            this.Cursor = Cursor.Current;
            foreach (ColumnHeader header in this.lvw.Columns) {
                num2 = x + header.Width;
                if ((curX > x) && (curX < num2)) {
                    return header.Index;
                }
                x += header.Width;
            }
            return -1;
        }

        private ArrayList GetNodeChildModelPK(DEResModelPrimaryKey defathermpk, ArrayList al_modelpks) {
            ArrayList list = new ArrayList();
            foreach (DEResModelPrimaryKey key in al_modelpks) {
                if (defathermpk.PLM_OID == key.PLM_FATHEROID) {
                    list.Add(key);
                }
            }
            return list;
        }

        private DataView GetOrgDataView(DEResFolder curFolder) {
            DataView view = new DataView();
            return this.theDataSet.Tables[0].DefaultView;
        }

        private void GetParentNodeInfo(TreeNode theNode) {
            if (theNode.Parent != null) {
                if (theNode.Tag is DEMetaAttribute) {
                    DEMetaAttribute tag = (DEMetaAttribute)theNode.Tag;
                    this.SetNodeFilterInfo(theNode.Text, tag);
                }
                this.GetParentNodeInfo(theNode.Parent);
            }
        }

        private void GetParentNodeInfoVT(TreeNode theNode) {
            if (theNode.Parent != null) {
                if (theNode.Tag is ArrayList) {
                    ArrayList tag = (ArrayList)theNode.Tag;
                    if (tag.Count == 2) {
                        DEMetaAttribute deattr = (DEMetaAttribute)tag[1];
                        this.SetNodeFilterInfo(theNode.Text, deattr);
                    }
                }
                this.GetParentNodeInfoVT(theNode.Parent);
            }
        }

        private ArrayList GetRoot(ArrayList al_modelpks) {
            ArrayList list = new ArrayList();
            foreach (DEResModelPrimaryKey key in al_modelpks) {
                if (key.PLM_FATHEROID == Guid.Empty) {
                    list.Add(key);
                }
            }
            return list;
        }

        private ArrayList GetSplitString(string str_pos, string str_fh) {
            char[] separator = str_fh.ToCharArray();
            string[] strArray = null;
            ArrayList list = new ArrayList();
            if (str_pos != null) {
                strArray = str_pos.Split(separator);
            }
            for (int i = 0; i < strArray.Length; i++) {
                list.Add(strArray[i].ToString());
            }
            return list;
        }

        private void InitAllOcxState() {
            this.InitOrgToolBarState();
            this.SetStatusInfo(0, 0, 0);
            this.lvw.Items.Clear();
        }

        private void InitControlData(DEResFolder defolder, Guid g_clsid) {
            if (this.str_CurClsLbl != ModelContext.MetaModel.GetClass(g_clsid).Label) {
                this.str_CurClsLbl = ModelContext.MetaModel.GetClass(g_clsid).Label;
                string name = ModelContext.MetaModel.GetClass(g_clsid).Name;
                this.clsName = name;
                this.myResFolder = defolder;
                if (this.curFolder == null) {
                    this.curFolder = new DEResFolder();
                }
                this.curFolder.Oid = this.myResFolder.Oid;
                this.curFolder.ClassOid = this.myResFolder.ClassOid;
                this.curFolder.ClassName = this.myResFolder.ClassName;
                this.curFolder.Filter = this.myResFolder.Filter;
                this.curFolder.FilterString = this.myResFolder.FilterString;
                this.curFolder.FilterValue = this.myResFolder.FilterValue;
                this.InitResStatus();
                ArrayList showAttrList = ResFunc.GetShowAttrList(this.curFolder, emTreeType.NodeTree);
                this.attrList = ResFunc.CloneMetaAttrLst(showAttrList);
                this.attrSort = ResFunc.GetSortAttrList(this.curFolder);
                this.attrOuter = ResFunc.GetOuterAttr(this.curFolder);
                this.InitSortList(true);
                this.InitShowAttrLst();
                this.SetAttrDataType();
                this.myView = null;
                try {
                    this.InitData();
                } catch (PLMException exception) {
                    PrintException.Print(exception);
                } catch (Exception exception2) {
                    MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.BingdingData();
            }
        }

        private void InitData() {
            if (this.curFolder != null) {
                if (ResFunc.IsOnlineOutRes(this.curFolder.ClassOid)) {
                    this.emResType = emResourceType.OutSystem;
                    this.int_rowCount = ResFunc.GetDataCount(this.curFolder, this.attrList, this.attrOuter, emResourceType.OutSystem);
                    ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, 0, this.showNum, this.strSort);
                    ResFunc.ConvertOuterDSHead(this.theDataSet, this.attrList, this.attrOuter);
                } else if (ResFunc.IsRefRes(this.curFolder.ClassOid)) {
                    this.emResType = emResourceType.Standard;
                    PLSPL plspl = new PLSPL();
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                    DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                    this.clsName = class3.Name;
                    this.curFolder.ClassName = class3.Name;
                    ArrayList attrList = new ArrayList();
                    foreach (DEMetaAttribute attribute in this.attrList) {
                        if (this.ISDefAttrViewable(attribute)) {
                            attrList.Add(attribute);
                        } else if (ModelContext.IsInAttrResLink(this.curFolder.ClassOid, attribute)) {
                            attrList.Add(attribute);
                        }
                    }
                    if (this.b_stateShow) {
                        this.int_rowCount = plspl.GetSPLCount(class3.Name, attrList, this.userOid, this.curFolder.FilterString, this.curFolder.FilterValue);
                        this.theDataSet = plspl.GetSPLNumDataSet(class3.Name, attrList, this.userOid, 0, this.showNum, this.curFolder.FilterString, this.curFolder.FilterValue, this.strSort);
                    } else {
                        this.int_rowCount = plspl.GetSPLCountForAvailable(class3.Name, attrList, this.userOid, this.curFolder.FilterString, this.curFolder.FilterValue);
                        this.theDataSet = plspl.GetSPLNumDataSetForAvailable(class3.Name, attrList, this.userOid, 0, this.showNum, this.curFolder.FilterString, this.curFolder.FilterValue, this.strSort);
                    }
                    this.SetTurnPageStart();
                    this.b_refType = true;
                } else if (ResFunc.IsTabRes(this.curFolder.ClassOid)) {
                    this.emResType = emResourceType.Customize;
                    this.int_rowCount = ResFunc.GetDataCount(this.curFolder, this.attrList, this.attrOuter, emResourceType.Customize);
                    ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, 0, this.showNum, this.strSort);
                } else {
                    this.emResType = emResourceType.PLM;
                    this.theDataSet = new DataSet();
                    ArrayList items = new ArrayList();
                    items = this.GetItemLST(this.curFolder.ClassName);
                    DataTable dataSource = DataSourceMachine.GetDataSource(this.curFolder.ClassName, items);
                    this.theDataSet.Tables.Add(dataSource);
                    this.int_rowCount = this.theDataSet.Tables[0].DefaultView.Table.Rows.Count;
                }
            }
        }

        private void InitLvShowHeader() {
            int num = 0;
            this.lvw.Columns.Clear();
            for (int i = 0; i < this.myView.Table.Columns.Count; i++) {
                foreach (DEMetaAttribute attribute in this.attrList) {
                    if (this.ISDefAttrViewable(attribute) && (this.myView.Table.Columns[i].ColumnName == ("PLM_" + attribute.Name))) {
                        int defAttrWidth = 0;
                        ColumnHeader header = new ColumnHeader {
                            Text = attribute.Label
                        };
                        defAttrWidth = this.GetDefAttrWidth(attribute);
                        if (defAttrWidth > 0) {
                            header.Width = defAttrWidth;
                        }
                        this.lvw.Columns.Add(header);
                        num++;
                        break;
                    }
                }
            }
            if (this.b_TreeShow) {
                int width = (Screen.PrimaryScreen.WorkingArea.Width * 4) / 5;
                this.SetPopupContainerWidth(width);
                base.Width = width;
                int height = Screen.PrimaryScreen.WorkingArea.Height / 2;
                this.SetPopupContainerHeight(height);
                base.Height = height;
                this.panel1.Height = Screen.PrimaryScreen.WorkingArea.Height / 2;
                this.i_orgWidth = base.Width;
                this.i_orgHeight = base.Height;
            } else {
                if (num <= 3) {
                    int num6 = 10;
                    int num7 = num6;
                    foreach (ColumnHeader header2 in this.lvw.Columns) {
                        if (num < 3) {
                            num7 = (base.Width > 200) ? base.Width : 200;
                            this.SetPopupContainerWidth(num7);
                            base.Width = num7;
                        } else {
                            num7 = (base.Width > 300) ? base.Width : 300;
                            this.SetPopupContainerWidth(num7);
                            base.Width = num7;
                        }
                        header2.Width = (base.Width - num6) / num;
                        this.Refresh();
                    }
                } else {
                    int num8 = (Screen.PrimaryScreen.Bounds.Width * 2) / 3;
                    int num9 = (base.Width > num8) ? base.Width : num8;
                    this.SetPopupContainerWidth(num9);
                    base.Width = num9;
                }
                if (this.int_rowCount < 10) {
                    int num10 = (((this.int_rowCount * 20) + this.stBar_PageInfo.Height) + this.toolStrip1.Height) + 50;
                    this.SetPopupContainerHeight(num10);
                    base.Height = num10;
                } else {
                    int num11 = Screen.PrimaryScreen.Bounds.Height / 2;
                    this.SetPopupContainerHeight(num11);
                    base.Height = num11;
                }
                this.i_orgWidth = base.Width;
                this.i_orgHeight = base.Height;
            }
        }

        private void InitModelTree() {
            this.TV_Class.ImageList = ClientData.MyImageList.imageList;
            this.TV_Class.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NODE");
            TreeNode node = new TreeNode {
                Text = "分类目录",
                ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL"),
                SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_FDL_OPEN")
            };
            this.TV_Class.Nodes.Add(node);
        }

        private void InitObject() {
            this.b_first = true;
            this.b_refType = false;
            this.LoadModelData();
            if (this.IsHasChildCls()) {
                this.b_TreeShow = true;
                this.btnHide.Enabled = true;
                this.panel1.Visible = true;
                if (ModelContext.MetaModel.GetClass(this.className) != null) {
                    this.InitModelTree();
                }
            } else {
                this.b_TreeShow = false;
                this.btnHide.Enabled = false;
                this.panel1.Visible = false;
            }
        }

        private void InitOrgToolBarState() {
            this.btnSort.Enabled = false;
            this.SetTurnPageUnState();
            this.SetUnMpageState();
        }

        private void InitResStatus() {
            if (ResFunc.IsOnlineOutRes(this.curFolder.ClassOid)) {
                this.emResType = emResourceType.OutSystem;
            } else if (ResFunc.IsRefRes(this.curFolder.ClassOid)) {
                this.emResType = emResourceType.Standard;
                this.b_refType = true;
            } else if (ResFunc.IsTabRes(this.curFolder.ClassOid)) {
                this.emResType = emResourceType.Customize;
            } else {
                this.emResType = emResourceType.PLM;
            }
        }

        private void InitShowAttrLst() {
            this.attrShow = new PLReference().GetDefAttrLst(this.curFolder.Oid, emTreeType.NodeTree);
        }

        private void InitSortList(bool b_first) {
            this.strSort = "";
            string userOption = (string)ClientData.GetUserOption(this.userOid + this.curFolder.Oid.ToString());
            if ((b_first && (userOption != null)) && (userOption.Trim().Length > 0)) {
                if (userOption != "nothing") {
                    this.strSort = userOption;
                }
            } else {
                int num = 0;
                if (this.attrSort.Count > 0) {
                    for (int i = 0; i < this.attrSort.Count; i++) {
                        DEDefSort sort = (DEDefSort)this.attrSort[i];
                        switch (this.emResType) {
                            case emResourceType.PLM:
                                foreach (DEMetaAttribute attribute4 in this.attrList) {
                                    if (attribute4.Oid == sort.ATTROID) {
                                        string str8 = sort.ISASC ? " asc" : " desc";
                                        string str9 = "PLM_CUS_" + this.curFolder.ClassName;
                                        if (num == 0) {
                                            this.strSort = " order by " + str9 + ".PLM_" + attribute4.Name + str8;
                                        } else {
                                            string strSort = this.strSort;
                                            this.strSort = strSort + " , " + str9 + ".PLM_" + attribute4.Name + str8;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.Customize:
                                foreach (DEMetaAttribute attribute in this.attrList) {
                                    if (attribute.Oid == sort.ATTROID) {
                                        string str2 = sort.ISASC ? " asc" : " desc";
                                        string str3 = "PLM_CUS_" + this.curFolder.ClassName;
                                        if (num == 0) {
                                            this.strSort = " order by " + str3 + ".PLM_" + attribute.Name + str2;
                                        } else {
                                            string str10 = this.strSort;
                                            this.strSort = str10 + " , " + str3 + ".PLM_" + attribute.Name + str2;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.OutSystem:
                                foreach (DEOuterAttribute attribute2 in this.attrOuter) {
                                    if (attribute2.FieldOid == sort.ATTROID) {
                                        string str4 = sort.ISASC ? " asc" : " desc";
                                        if (num == 0) {
                                            this.strSort = " order by " + attribute2.OuterAttrName + str4;
                                        } else {
                                            this.strSort = this.strSort + " , " + attribute2.OuterAttrName + str4;
                                        }
                                        num++;
                                        break;
                                    }
                                }
                                break;

                            case emResourceType.Standard: {
                                    new PLSPL();
                                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                                    DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                                    if (sort.ATTROID == Guid.Empty) {
                                        ClientData.SetUserOption(this.userOid + this.curFolder.Oid.ToString(), "nothing");
                                        ClientData.SetUserOption(this.userOid + this.curFolder.Oid.ToString() + "_def", "nothing");
                                        return;
                                    }
                                    foreach (DEMetaAttribute attribute3 in this.attrList) {
                                        if (attribute3.Oid == sort.ATTROID) {
                                            string str5 = sort.ISASC ? " asc" : " desc";
                                            string str6 = "PLM_CUS_".Replace("_CUS_", "_CUSV_") + class3.Name;
                                            string str7 = "PLM_PSM_ITEMMASTER_REVISION";
                                            if (num == 0) {
                                                if (attribute3.Name.StartsWith("M_") || attribute3.Name.StartsWith("R_")) {
                                                    this.strSort = " order by " + str7 + ".PLM_" + attribute3.Name + str5;
                                                } else {
                                                    this.strSort = " order by " + str6 + ".PLM_" + attribute3.Name + str5;
                                                }
                                            } else if (attribute3.Name.StartsWith("M_") || attribute3.Name.StartsWith("R_")) {
                                                string str11 = this.strSort;
                                                this.strSort = str11 + " , " + str7 + ".PLM_" + attribute3.Name + str5;
                                            } else {
                                                string str12 = this.strSort;
                                                this.strSort = str12 + " , " + str6 + ".PLM_" + attribute3.Name + str5;
                                            }
                                            num++;
                                            break;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
        }

        private bool ISDefAttrViewable(DEMetaAttribute demattr) {
            bool flag = false;
            if (this.attrShow.Count > 0) {
                if (this.HT_AttrIsView.ContainsKey(demattr.Oid)) {
                    return Convert.ToBoolean(this.HT_AttrIsView[demattr.Oid]);
                }
                foreach (DEDefAttr attr in this.attrShow) {
                    if (demattr.Oid == attr.ATTROID) {
                        this.HT_AttrIsView.Add(demattr.Oid, attr.ISVISUAL);
                        return attr.ISVISUAL;
                    }
                }
                return flag;
            }
            return demattr.IsViewable;
        }

        private bool IsHasChildCls() {
            bool flag = false;
            if (((this.myModel != null) && (this.myModel.PLM_OID != Guid.Empty)) && (this.myModel.PLM_MODELPKLIST.Count > 0)) {
                flag = true;
            }
            return flag;
        }

        private bool IsHasChildNodeVT(DEResModelPrimaryKey defathermpk, ArrayList al_modelpks, TreeNode theNode) {
            foreach (DEResModelPrimaryKey key in al_modelpks) {
                if (defathermpk.PLM_OID == key.PLM_FATHEROID) {
                    return true;
                }
            }
            return false;
        }

        private bool IsValidKey(string strKey) {
            try {
                Convert.ToDecimal(strKey);
                return true;
            } catch {
                return false;
            }
        }

        private void LiveToolBarState() {
            this.btnSort.Enabled = true;
        }

        public void Load(Hashtable ht_param) {
            DEResFolder myFolder = this.CreateFolder();
            this.SetFolder(myFolder, ht_param);
            DEResFolder defolder = this.CreateOrgFolder();
            defolder.Filter = myFolder.Filter;
            defolder.FilterString = myFolder.FilterString;
            defolder.FilterValue = myFolder.FilterValue;
            this.InitControlData(defolder, this.classOid);
            this.b_first = false;
        }

        private void LoadModelData() {
            this.myModel = new PLResModel().GetModel(this.classOid);
        }

        private void lvw_ItemActivate(object sender, EventArgs e) {
            if (!this.b_start && (((this.ResSelected != null) && (this.lvw.SelectedItems != null)) && ((this.lvw.SelectedItems.Count > 0) && (this.lvw.SelectedItems[0].Tag != null)))) {
                DataRowView tag = (DataRowView)this.lvw.SelectedItems[0].Tag;
                this.ResSelected(tag);
                if (this.CloseWhenActivated && (base.Parent != null)) {
                    this.CloseParent();
                }
            }
        }

        private int NodeType(ArrayList al_in) {
            int num = 0;
            if (al_in.Count == 2) {
                DEResModelPrimaryKey key = (DEResModelPrimaryKey)al_in[0];
                if (key.PLM_ISSHOWDATA) {
                    num = 1;
                }
                if (!key.PLM_ISSHOWDATA) {
                    num = 2;
                }
            }
            if (al_in.Count == 1) {
                DEResModelPrimaryKey key2 = (DEResModelPrimaryKey)al_in[0];
                if (!key2.PLM_ISVIRTUAL) {
                    num = 3;
                }
                if (key2.PLM_ISVIRTUAL) {
                    num = 4;
                }
            }
            return num;
        }

        public void ReleaseDataSet() {
            if (this.theDataSet != null) {
                this.theDataSet.Dispose();
                this.theDataSet = null;
                GC.Collect();
            }
        }

        public void ReLoad(DEResFolder defolder) {
            this.InitControlData(defolder, this.classOid);
            this.b_first = false;
        }

        public void ReloadModel(DEResModel themodel) {
            this.myModel = themodel;
            this.TV_Class.Nodes.Clear();
            if (this.IsHasChildCls()) {
                this.b_TreeShow = true;
                this.btnHide.Enabled = true;
                this.panel1.Visible = true;
                if (ModelContext.MetaModel.GetClass(this.className) != null) {
                    this.InitModelTree();
                }
            } else {
                this.b_TreeShow = false;
                this.panel1.Visible = false;
                this.btnHide.Enabled = false;
            }
        }

        private void SelectNodeFT() {
            if (this.TV_Class.SelectedNode != null) {
                this.lvw.Items.Clear();
                if (this.TV_Class.SelectedNode.Parent == null) {
                    if (this.TV_Class.SelectedNode.GetNodeCount(false) == 0) {
                        this.Get2LayerNodeData(this.TV_Class.Nodes[0]);
                    }
                } else if (this.TV_Class.SelectedNode.GetNodeCount(false) == 0) {
                    if (this.TV_Class.SelectedNode.Tag is DEResModelPrimaryKey) {
                        DEResModelPrimaryKey tag = (DEResModelPrimaryKey)this.TV_Class.SelectedNode.Tag;
                        if (tag.PLM_ISVIRTUAL) {
                            this.AddChildNode(tag, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                        } else {
                            this.GetLeafNodeData(this.TV_Class.SelectedNode);
                        }
                    } else if (this.TV_Class.SelectedNode.Tag is DEMetaAttribute) {
                        DEResModelPrimaryKey defathermpk = (DEResModelPrimaryKey)this.TV_Class.SelectedNode.Parent.Tag;
                        DEMetaAttribute deattr = (DEMetaAttribute)this.TV_Class.SelectedNode.Tag;
                        this.AddChildNode(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                        if (defathermpk.PLM_ISSHOWDATA) {
                            this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, deattr);
                            this.GetParentNodeInfo(this.TV_Class.SelectedNode.Parent);
                            this.FilterData();
                        } else if (this.TV_Class.SelectedNode.Nodes.Count == 0) {
                            this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, deattr);
                            this.GetParentNodeInfo(this.TV_Class.SelectedNode);
                            this.FilterData();
                        }
                    }
                } else if (this.TV_Class.SelectedNode.Tag is DEMetaAttribute) {
                    DEResModelPrimaryKey key3 = (DEResModelPrimaryKey)this.TV_Class.SelectedNode.Parent.Tag;
                    DEMetaAttribute attribute2 = (DEMetaAttribute)this.TV_Class.SelectedNode.Tag;
                    if (key3.PLM_ISSHOWDATA) {
                        this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, attribute2);
                        this.GetParentNodeInfo(this.TV_Class.SelectedNode.Parent);
                        this.FilterData();
                    }
                }
            }
        }

        private void SelectNodeVT() {
            if (this.TV_Class.SelectedNode != null) {
                this.lvw.Items.Clear();
                if (this.TV_Class.SelectedNode.Parent == null) {
                    if (this.TV_Class.SelectedNode.GetNodeCount(false) == 0) {
                        this.Get2LayerNodeDataVT(this.TV_Class.Nodes[0]);
                    }
                } else if (this.TV_Class.SelectedNode.GetNodeCount(false) == 0) {
                    ArrayList tag = (ArrayList)this.TV_Class.SelectedNode.Tag;
                    DEResModelPrimaryKey defathermpk = (DEResModelPrimaryKey)tag[0];
                    if (this.TV_Class.SelectedNode.Parent.Parent == null) {
                        if (defathermpk.PLM_ISVIRTUAL) {
                            if (this.IsHasChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode)) {
                                foreach (DEResModelPrimaryKey key2 in this.GetNodeChildModelPK(defathermpk, this.myModel.PLM_MODELPKLIST)) {
                                    if (!key2.PLM_ISVIRTUAL) {
                                        this.GetLeafNodeDataVT(this.TV_Class.SelectedNode, key2);
                                    }
                                }
                            }
                        } else {
                            this.Get2LayerLeafNodeDataVT(this.TV_Class.SelectedNode);
                        }
                    } else {
                        switch (this.NodeType(tag)) {
                            case 1: {
                                    DEMetaAttribute deattr = (DEMetaAttribute)tag[1];
                                    this.AddChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                                    if (this.IsHasChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode)) {
                                        foreach (DEResModelPrimaryKey key3 in this.GetNodeChildModelPK(defathermpk, this.myModel.PLM_MODELPKLIST)) {
                                            if (!key3.PLM_ISVIRTUAL) {
                                                this.GetLeafNodeDataVT(this.TV_Class.SelectedNode, key3);
                                            }
                                        }
                                    }
                                    this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, deattr);
                                    this.GetParentNodeInfoVT(this.TV_Class.SelectedNode);
                                    this.FilterData();
                                    return;
                                }
                            case 2:
                                this.AddChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                                if (this.IsHasChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode)) {
                                    foreach (DEResModelPrimaryKey key4 in this.GetNodeChildModelPK(defathermpk, this.myModel.PLM_MODELPKLIST)) {
                                        if (!key4.PLM_ISVIRTUAL) {
                                            this.GetLeafNodeDataVT(this.TV_Class.SelectedNode, key4);
                                        }
                                    }
                                }
                                if (this.TV_Class.SelectedNode.GetNodeCount(false) == 0) {
                                    DEMetaAttribute attribute2 = (DEMetaAttribute)tag[1];
                                    this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, attribute2);
                                    this.GetParentNodeInfoVT(this.TV_Class.SelectedNode);
                                    this.FilterData();
                                }
                                return;

                            case 3:
                                this.AddChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                                if (this.IsHasChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode)) {
                                    foreach (DEResModelPrimaryKey key5 in this.GetNodeChildModelPK(defathermpk, this.myModel.PLM_MODELPKLIST)) {
                                        if (!key5.PLM_ISVIRTUAL) {
                                            this.GetLeafNodeDataVT(this.TV_Class.SelectedNode, key5);
                                        }
                                    }
                                }
                                return;

                            case 4:
                                this.AddChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode);
                                if (this.IsHasChildNodeVT(defathermpk, this.myModel.PLM_MODELPKLIST, this.TV_Class.SelectedNode)) {
                                    foreach (DEResModelPrimaryKey key6 in this.GetNodeChildModelPK(defathermpk, this.myModel.PLM_MODELPKLIST)) {
                                        if (!key6.PLM_ISVIRTUAL) {
                                            this.GetLeafNodeDataVT(this.TV_Class.SelectedNode, key6);
                                        }
                                    }
                                }
                                return;
                        }
                    }
                } else {
                    ArrayList list7 = (ArrayList)this.TV_Class.SelectedNode.Tag;
                    if (this.NodeType(list7) == 1) {
                        DEMetaAttribute attribute3 = (DEMetaAttribute)list7[1];
                        this.SetNodeFilterInfo(this.TV_Class.SelectedNode.Text, attribute3);
                        this.GetParentNodeInfoVT(this.TV_Class.SelectedNode.Parent);
                        this.FilterData();
                    }
                }
            }
        }

        private void SetAttrDataType() {
            foreach (DEMetaAttribute attribute in this.attrList) {
                foreach (DEOuterAttribute attribute2 in this.attrOuter) {
                    if (attribute.Oid == attribute2.FieldOid) {
                        attribute.DataType = attribute2.DataType;
                        break;
                    }
                }
            }
        }

        public void SetClassLable(string str_in) {
            this.str_CurClsLbl = str_in;
        }

        private void SetDpDnMPageState(string str_PageRecNum) {
            switch (str_PageRecNum) {
                case "全部":
                    this.btnShowMPage.Checked = true;
                    this.btnShowMPage.Enabled = true;
                    this.btnShowMPage.ToolTipText = "全部显示";
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.showNum = 50;
                    return;

                case "50":
                    this.btnShowMPage.Checked = false;
                    this.btnShowMPage.Enabled = true;
                    this.btnShowMPage.ToolTipText = "分页显示";
                    this.showNum = 50;
                    this.SetTurnPageStart();
                    return;

                case "100":
                    this.btnShowMPage.Checked = false;
                    this.btnShowMPage.Enabled = true;
                    this.btnShowMPage.ToolTipText = "分页显示";
                    this.SetTurnPageStart();
                    this.showNum = 100;
                    return;

                case "200":
                    this.btnShowMPage.Checked = false;
                    this.btnShowMPage.Enabled = true;
                    this.btnShowMPage.ToolTipText = "分页显示";
                    this.showNum = 200;
                    this.SetTurnPageStart();
                    return;
            }
            this.btnShowMPage.Checked = true;
            this.btnShowMPage.Enabled = true;
            this.btnShowMPage.ToolTipText = "全部显示";
            this.SetTurnPageUnState();
            this.SetUnMpageState();
            this.showNum = 50;
        }

        private void SetFilter(DEResFolder myFolder, Hashtable ht_param, string str_oldfilter, string str_oldfilterval, out string str_filter, out string str_filterval) {
            str_filter = str_oldfilter;
            str_filterval = str_oldfilterval;
            if (ht_param.Count != 0) {
                string str = "";
                string str2 = "";
                ArrayList attributes = this.GetAttributes(myFolder.ClassName);
                if (ModelContext.MetaModel.GetClass(this.className).IsRefResClass) {
                    ArrayList fixedAttrList = ResFunc.GetFixedAttrList();
                    attributes.AddRange(fixedAttrList);
                }
                if ((attributes != null) && (attributes.Count > 0)) {
                    foreach (DEMetaAttribute attribute in attributes) {
                        if (ht_param.ContainsKey(attribute.Name)) {
                            string str3 = "";
                            string str4 = "";
                            this.SetFilterInfo(myFolder, ht_param[attribute.Name].ToString(), attribute, out str3, out str4);
                            if ((str != null) && (str.Trim().Length > 0)) {
                                str = str + " AND " + str3;
                            } else {
                                str = str3;
                            }
                            if ((str2 != null) && (str2.Trim().Length > 0)) {
                                if ((str4 != null) && (str4.Trim().Length > 0)) {
                                    str2 = str2 + "," + str4;
                                }
                            } else if ((str4 != null) && (str4.Trim().Length > 0)) {
                                str2 = str4;
                            }
                        }
                    }
                }
                if ((str_oldfilter == null) || (str_oldfilter == "")) {
                    str_filter = str;
                    str_filterval = str2;
                } else if ((str != null) || (str.Trim().Length > 0)) {
                    str_filter = "(" + str_filter + ") AND (" + str + ")";
                    if (str_oldfilterval == null) {
                        str_filterval = str2;
                    } else if (str_oldfilterval.Trim().Length == 0) {
                        str_filterval = str2;
                    } else {
                        str_filterval = str_oldfilterval + "," + str2;
                    }
                }
            }
        }

        private void SetFilterInfo(DEResFolder myFolder, string str_value, DEMetaAttribute deattr, out string str_pasfilter, out string str_pasfilterval) {
            str_pasfilter = "";
            str_pasfilterval = "";
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            string strFilterStr = "";
            string strFilterVal = "";
            TextBox txtBox = new TextBox();
            if (deattr.DataType == 7) {
                txtBox.Text = Convert.ToDateTime(str_value).ToShortDateString();
                txtBox.Tag = deattr;
            } else {
                txtBox.Text = str_value;
                txtBox.Tag = deattr;
            }
            System.Windows.Forms.ComboBox cob = new System.Windows.Forms.ComboBox {
                Text = "等于"
            };
            ArrayList txtList = new ArrayList();
            if (str_value != "[空]") {
                ResFunc.CreateCondition(cob, txtBox, txtList, this.emResType, this.attrOuter, myFolder, out strFilterStr, out strFilterVal);
            } else {
                flag = true;
                switch (this.emResType) {
                    case emResourceType.Customize:
                        builder2.Append("PLM_");
                        builder2.Append(deattr.Name);
                        break;

                    case emResourceType.OutSystem:
                        foreach (DEOuterAttribute attribute in this.attrOuter) {
                            if (deattr.Oid == attribute.FieldOid) {
                                builder2.Append(attribute.OuterAttrName);
                                break;
                            }
                        }
                        break;

                    case emResourceType.Standard: {
                            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.className);
                            DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                            string str3 = "PLM_CUSV_" + class3.Name;
                            if (deattr.Name.StartsWith("M_") || deattr.Name.StartsWith("R_")) {
                                builder2.Append("PLM_PSM_ITEMMASTER_REVISION.PLM_" + deattr.Name.ToUpper());
                            } else {
                                builder2.Append(str3);
                                builder2.Append(".");
                                builder2.Append("PLM_");
                                builder2.Append(deattr.Name);
                            }
                            break;
                        }
                }
                builder2.Append(" is null ");
            }
            builder2.Append(strFilterStr);
            builder.Append(builder2.ToString());
            builder3.Append(strFilterVal);
            try {
                str_pasfilter = builder.ToString();
                if (!flag) {
                    str_pasfilterval = builder3.ToString();
                }
            } catch (Exception exception) {
                MessageBox.Show("过滤数据发生错误:请检查传递的参数否正确:" + exception.Message, "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

 

 
  
        private void SetFolder(DEResFolder myFolder, Hashtable ht_param) {
            ArrayList clsTreeCls = new ArrayList();
            PLReference reference = new PLReference();
            Hashtable hashtable = ht_param;
            clsTreeCls = reference.GetClsTreeCls(this.classOid);
            if (clsTreeCls.Count > 0) {
                DEDefCls cls = (DEDefCls)clsTreeCls[0];
                myFolder.Filter = cls.FILTER;
                string str = "";
                string str2 = "";
                this.SetFilter(myFolder, hashtable, cls.FILTERSTRING, cls.FILTERVALUE, out str, out str2);
                myFolder.FilterString = str;
                myFolder.FilterValue = str2;
            } else {
                string str3 = "";
                string str4 = "";
                this.SetFilter(myFolder, hashtable, "", "", out str3, out str4);
                myFolder.FilterString = str3;
                myFolder.FilterValue = str4;
            }
        }

        private void SetLiveMpageState(int i_ShowPageIndex) {
            this.btnShowMPage.Checked = false;
            this.btnShowMPage.Enabled = true;
            this.btnShowMPage.ToolTipText = "分页显示";
            this.cobPageSize.SelectedIndex = i_ShowPageIndex;
            this.cobPageSize.Enabled = true;
        }

        private void SetNodeFilterInfo(string str_value, DEMetaAttribute deattr) {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            string strFilterStr = "";
            string strFilterVal = "";
            if (this.myView != null) {
                TextBox txtBox = new TextBox();
                if (deattr.DataType == 7) {
                    txtBox.Text = Convert.ToDateTime(str_value).ToShortDateString();
                    txtBox.Tag = deattr;
                } else {
                    txtBox.Text = str_value;
                    txtBox.Tag = deattr;
                }
                System.Windows.Forms.ComboBox cob = new System.Windows.Forms.ComboBox {
                    Text = "等于"
                };
                ArrayList txtList = new ArrayList();
                if (str_value != "[空]") {
                    ResFunc.CreateCondition(cob, txtBox, txtList, this.emResType, this.attrOuter, this.curFolder, out strFilterStr, out strFilterVal);
                } else {
                    flag = true;
                    switch (this.emResType) {
                        case emResourceType.Customize:
                            builder2.Append("PLM_");
                            builder2.Append(deattr.Name);
                            break;

                        case emResourceType.OutSystem:
                            foreach (DEOuterAttribute attribute in this.attrOuter) {
                                if (deattr.Oid == attribute.FieldOid) {
                                    builder2.Append(attribute.OuterAttrName);
                                    break;
                                }
                            }
                            break;

                        case emResourceType.Standard: {
                                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.curFolder.ClassOid);
                                DEMetaClass class3 = ModelContext.MetaModel.GetClass(class2.RefClass);
                                string str3 = "PLM_CUSV_" + class3.Name;
                                if (deattr.Name.StartsWith("M_") || deattr.Name.StartsWith("R_")) {
                                    builder2.Append("PLM_PSM_ITEMMASTER_REVISION.PLM_" + deattr.Name.ToUpper());
                                } else {
                                    builder2.Append(str3);
                                    builder2.Append(".");
                                    builder2.Append("PLM_");
                                    builder2.Append(deattr.Name);
                                }
                                break;
                            }
                    }
                    builder2.Append(" is null ");
                }
                builder2.Append(strFilterStr);
                builder.Append(builder2.ToString());
                builder3.Append(strFilterVal);
                try {
                    if ((this.curFolder.FilterString == null) || (this.curFolder.FilterString == "")) {
                        this.curFolder.FilterString = builder.ToString();
                        if (!flag) {
                            this.curFolder.FilterValue = builder3.ToString();
                        }
                    } else if ((builder == null) || (builder.ToString().Trim() == "")) {
                        this.curFolder.FilterString = this.myResFolder.FilterString.ToString();
                        if (!string.IsNullOrEmpty(this.myResFolder.FilterValue)) {
                            this.curFolder.FilterValue = this.myResFolder.FilterValue.ToString();
                        } else {
                            this.curFolder.FilterValue = "";
                        }
                    } else {
                        this.curFolder.FilterString = "(" + this.curFolder.FilterString.ToString() + ") AND (" + builder.ToString() + ")";
                        if (!flag) {
                            if (this.curFolder.FilterValue == null) {
                                this.curFolder.FilterValue = builder3.ToString();
                            } else if (this.curFolder.FilterValue.Trim().Length == 0) {
                                this.curFolder.FilterValue = builder3.ToString();
                            } else {
                                this.curFolder.FilterValue = this.curFolder.FilterValue.ToString() + "," + builder3.ToString();
                            }
                        }
                    }
                } catch (Exception exception) {
                    MessageBox.Show("过滤数据发生错误:请检查输入的数据类型是否正确:" + exception.Message, "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

 

        private void SetPopupContainerHeight(int height) {
            if ((base.Parent != null) && (base.Parent is PopupContainerControl)) {
                base.Parent.Height = height;
            }
        }

        private void SetPopupContainerWidth(int width) {
            if ((base.Parent != null) && (base.Parent is PopupContainerControl)) {
                base.Parent.Width = width;
            }
        }

        private void SetStatusInfo(int i_curPage, int i_TotalPage, int i_sumRecNum) {
            string str = "";
            str = i_curPage.ToString() + "/" + i_TotalPage.ToString() + "页, 共" + i_sumRecNum.ToString() + "条记录";
            this.stBar_PageInfo.Text = str;
        }

        private void SetTurnPageEnd() {
            this.btnPrevPage.Enabled = true;
            this.btnNextPage.Enabled = false;
        }

        private void SetTurnPageMiddle() {
            this.btnPrevPage.Enabled = true;
            this.btnNextPage.Enabled = true;
        }

        private void SetTurnPageStart() {
            this.btnPrevPage.Enabled = false;
            this.btnNextPage.Enabled = true;
        }

        private void SetTurnPageUnState() {
            this.btnPrevPage.Enabled = false;
            this.btnNextPage.Enabled = false;
        }

        private void SetUnMpageState() {
            this.btnShowMPage.Checked = true;
            this.btnShowMPage.Enabled = true;
            this.btnShowMPage.ToolTipText = "全部显示";
            this.cobPageSize.SelectedIndex = 0;
            this.cobPageSize.Enabled = false;
        }

        private void ShowNumDataView(DataView dv, int i_start, int i_end) {
            ArrayList list = new ArrayList();
            DataTable table = new DataTable();
            this.lvw.Items.Clear();
            if (((dv == null) || (dv.Table == null)) || (dv.Table.Rows.Count == 0)) {
                this.SetStatusInfo(0, 0, 0);
            } else {
                if (this.b_first) {
                    dv = this.theDataSet.Tables[0].DefaultView;
                } else {
                    switch (this.emResType) {
                        case emResourceType.Customize:
                            ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, i_start, i_end, this.strSort);
                            break;

                        case emResourceType.OutSystem:
                            ResFunc.GetNumDS(out this.theDataSet, this.curFolder, this.attrList, i_start, i_end, this.strSort);
                            ResFunc.ConvertOuterDSHead(this.theDataSet, this.attrList, this.attrOuter);
                            break;

                        case emResourceType.Standard: {
                                PLSPL plspl = new PLSPL();
                                ArrayList attrList = new ArrayList();
                                foreach (DEMetaAttribute attribute in this.attrList) {
                                    if (this.ISDefAttrViewable(attribute)) {
                                        attrList.Add(attribute);
                                    } else if (ModelContext.IsInAttrResLink(this.curFolder.ClassOid, attribute)) {
                                        attrList.Add(attribute);
                                    }
                                }
                                if (this.b_stateShow) {
                                    this.theDataSet = plspl.GetSPLNumDataSet(this.clsName, attrList, this.userOid, i_start, i_end, this.curFolder.FilterString, this.curFolder.FilterValue, this.strSort);
                                } else {
                                    this.theDataSet = plspl.GetSPLNumDataSetForAvailable(this.clsName, attrList, this.userOid, i_start, i_end, this.curFolder.FilterString, this.curFolder.FilterValue, this.strSort);
                                }
                                break;
                            }
                    }
                    dv = this.theDataSet.Tables[0].DefaultView;
                }
                if (((dv != null) && (dv.Table != null)) && (dv.Table.Rows.Count != 0)) {
                    int num = 0;
                    foreach (DataRowView view in dv) {
                        int num2 = 0;
                        int num3 = 0;
                        string columnName = "";
                        table.NewRow();
                        if (num == i_end) {
                            return;
                        }
                        ListViewItem item = new ListViewItem {
                            UseItemStyleForSubItems = false
                        };
                        for (int i = 0; i < dv.Table.Columns.Count; i++) {
                            foreach (DEMetaAttribute attribute2 in this.attrList) {
                                if (this.ISDefAttrViewable(attribute2) && (dv.Table.Columns[i].ColumnName == ("PLM_" + attribute2.Name))) {
                                    columnName = "PLM_" + attribute2.Name;
                                    if ((((attribute2.DataType == 0) || (attribute2.DataType == 1)) || ((attribute2.DataType == 2) || (attribute2.DataType == 6))) && !list.Contains(num3)) {
                                        list.Add(num3);
                                    }
                                    num3++;
                                    if (this.b_refType) {
                                        if (((attribute2.Name == "R_CREATOR") || (attribute2.Name == "LATESTUPDATOR")) && (attribute2.DataType == 8)) {
                                            string principalName = PrincipalRepository.GetPrincipalName(new Guid((byte[])view[columnName]));
                                            if (num2 == 0) {
                                                item.Text = principalName;
                                            } else {
                                                item.SubItems.Add(principalName);
                                            }
                                        } else if (attribute2.Name == "M_CLASS") {
                                            string classname = view[columnName].ToString();
                                            if (num2 == 0) {
                                                item.Text = ModelContext.MetaModel.GetClassLabel(classname);
                                            } else {
                                                item.SubItems.Add(ModelContext.MetaModel.GetClassLabel(classname));
                                            }
                                        } else if (attribute2.Name == "R_REVSTATE") {
                                            string revStateLabel = DEItemRevision2.GetRevStateLabel(Convert.ToChar(view[columnName].ToString().Trim()));
                                            if (num2 == 0) {
                                                item.Text = revStateLabel;
                                            } else {
                                                item.SubItems.Add(revStateLabel);
                                            }
                                            if (Convert.ToChar(view[columnName].ToString()).Equals('F')) {
                                                item.ForeColor = Color.Red;
                                            } else {
                                                item.ForeColor = Color.Black;
                                            }
                                        } else if (attribute2.Name == "M_STATE") {
                                            string str5 = view[columnName].ToString().Trim();
                                            string text = "无状态";
                                            switch (str5) {
                                                case "I":
                                                    text = "检入";
                                                    break;

                                                case "O":
                                                    text = "检出";
                                                    break;

                                                case "A":
                                                    text = "废弃";
                                                    break;

                                                case "R":
                                                    text = "定版";
                                                    break;

                                                case "N":
                                                    text = "无状态";
                                                    break;
                                            }
                                            if (num2 == 0) {
                                                item.Text = text;
                                            } else {
                                                item.SubItems.Add(text);
                                            }
                                        } else if (attribute2.DataType2 == PLMDataType.Guid) {
                                            string str7 = "";
                                            if (((view[columnName] != null) && !view.Row.IsNull(columnName)) && (((attribute2.SpecialType2 == PLMSpecialType.OrganizationType) || (attribute2.SpecialType2 == PLMSpecialType.RoleType)) || (attribute2.SpecialType2 == PLMSpecialType.UserType))) {
                                                str7 = PrincipalRepository.GetPrincipalName(new Guid((byte[])view[columnName]));
                                            }
                                            if (num2 == 0) {
                                                item.Text = str7;
                                            } else {
                                                item.SubItems.Add(str7);
                                            }
                                        } else {
                                            string str8 = view[columnName].ToString().Trim();
                                            if ((attribute2.DataType2 == PLMDataType.DateTime) && !string.IsNullOrEmpty(str8)) {
                                                bool flag = true;
                                                try {
                                                    DateTime time = Convert.ToDateTime(str8);
                                                    if ((time == DateTime.MinValue) || (time == DateTime.MaxValue)) {
                                                        str8 = "";
                                                        flag = false;
                                                    }
                                                } catch {
                                                    flag = false;
                                                }
                                                if (flag) {
                                                    DEMetaAttribute exAttributeByOid = ModelContext.GetExAttributeByOid(this.clsName, attribute2.Oid);
                                                    if (((exAttributeByOid != null) && (exAttributeByOid.GetEditorSetup().format != null)) && (exAttributeByOid.GetEditorSetup().format.Length > 0)) {
                                                        string format = "yyyy年MM月dd日";
                                                        format = exAttributeByOid.GetEditorSetup().format.Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                                                        str8 = Convert.ToDateTime(str8).ToString(format);
                                                    }
                                                }
                                            }
                                            if (num2 == 0) {
                                                item.Text = str8;
                                            } else {
                                                item.SubItems.Add(str8);
                                            }
                                        }
                                    } else if (num2 == 0) {
                                        item.Text = view[columnName].ToString().Trim();
                                    } else {
                                        item.SubItems.Add(view[columnName].ToString().Trim());
                                    }
                                    item.Tag = view;
                                    num2++;
                                    break;
                                }
                            }
                        }
                        if (this.emResType == emResourceType.PLM) {
                            if ((i_start <= num) && (num <= i_end)) {
                                this.lvw.Items.Add(item);
                            }
                        } else {
                            this.lvw.Items.Add(item);
                        }
                        num++;
                    }
                    ResFunc.SetListViewColumnNum(this.lvw, list);
                }
            }
        }

        private void stBar_PageInfo_MouseDown(object sender, MouseEventArgs e) {
            if ((e.X < this.stBar_PageInfo.Width) && (e.X > (this.stBar_PageInfo.Width - 30))) {
                this.b_resize = true;
            }
            this.i_startX = e.X;
            this.i_startY = e.Y;
        }

        private void stBar_PageInfo_MouseMove(object sender, MouseEventArgs e) {
            if (this.b_resize) {
                int width = this.i_orgWidth + (e.X - this.i_startX);
                int height = this.i_orgHeight + (e.Y - this.i_startY);
                if (width > this.i_orgWidth) {
                    this.SetPopupContainerWidth(width);
                    base.Width = width;
                }
                if (height > this.i_orgHeight) {
                    this.SetPopupContainerHeight(height);
                    base.Height = height;
                }
                this.Refresh();
            }
        }

        private void stBar_PageInfo_MouseUp(object sender, MouseEventArgs e) {
            if (this.b_resize) {
                this.b_resize = false;
            }
        }

        private void TV_Class_AfterSelect(object sender, TreeViewEventArgs e) {
            if (!this.b_start) {
                this.curFolder.FilterString = this.myResFolder.FilterString;
                this.curFolder.FilterValue = this.myResFolder.FilterValue;
                if (this.IsValTree) {
                    this.SelectNodeVT();
                } else {
                    this.SelectNodeFT();
                }
            }
        }

        private void txtKeyword_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                this.FindByKeyword();
            }
        }

        private void UCRes_Enter(object sender, EventArgs e) {
            this.b_start = false;
        }

        private void UCResGrid_SizeChanged(object sender, EventArgs e) {
            if (this.lvw.Width < 320) {
                int width = base.Width + (320 - this.lvw.Width);
                this.SetPopupContainerWidth(width);
                base.Width = width;
            }
        }

        public bool AllowDoubleClick {
            get {
                return this.b_DoubleClisk;

            }
            set {
                this.b_DoubleClisk = value;
            }
        }

        public bool IsShowTree {
            get {
                return this.b_TreeCanView;
            }
            set {
                this.b_TreeCanView = value;
                if (this.b_TreeShow && this.b_TreeCanView) {
                    this.panel1.Visible = true;
                }
                if (this.b_TreeShow && !this.b_TreeCanView) {
                    this.panel1.Visible = false;
                }
                if (!this.b_TreeShow && !this.b_TreeCanView) {
                    this.panel1.Visible = false;
                }
                int width = Screen.PrimaryScreen.Bounds.Width / 2;
                this.SetPopupContainerWidth(width);
                base.Width = width;
            }
        }
    }
}

