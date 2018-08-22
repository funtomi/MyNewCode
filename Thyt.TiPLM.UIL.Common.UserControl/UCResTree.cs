using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.DEL.Resource;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Environment;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.PLL.Resource;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Common.UserControl.Properties;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCResTree : UserControlPLM, IResControl {
        private int AlertPageNum;
        private ArrayList attrFilterList;
        private ArrayList attrList;
        private ArrayList attrOuter;
        private ArrayList attrShow;
        private ArrayList attrSort;
        private bool B_CanDisplayFile;
        private bool b_first;
        private bool b_historySate;
        private bool b_refType;
        private bool b_stateShow;

        public static SelectCAPPResHandler CAPPResHandler = null;
        private string clsName;
        private MenuItemEx cmiA;
        private MenuItemEx cmiB;
        private MenuItemEx cmiClone;
        private MenuItemEx cmiViewFile;
        private ContextMenu cmuDgd;
        private ArrayList cobList;
        private DEResFolder curFolder;
        private string curStepInfo;
        private emResourceType emResType;

        private TreeViewEventHandler handler;
        private Hashtable HT_AttrIsView;
        private Hashtable HT_FilterInfo;
        private Hashtable HT_StepInfo;
        private int i_SelectedItemCol;
        private int I_StepNum;
        private int int_rowCount;
        private int int_SelectedItemCol;
        private bool isAllBtnClick;
        private bool isLoadingImage;
        private ArrayList lblList;
        public ThumbnailListView lvw;
        private EventHandler lvwDblClickHandler;
        private DEResFolder myResFolder;
        private int numViewData;
        private DataView myView;

        private DataView orgView;
        private int pageNum;

        private string resName;
        private UCResNodeTree resNodeTree;
        private static Control ResTreeInstance;
        private int showNum;
        private static ulong Stamp = 0L;
        private string str_CurClsLbl;
        private string strFilter;
        private string strSort;
        private ToolStrip tb;
        private ToolStripSeparator tbn_split;
        private ToolStripSeparator tbn_split1;
        private ToolStripSeparator tbn_split2;
        private ToolStripButton tbnClear;
        private ToolStripButton tbnNext;
        private ToolStripButton tBnOutput;
        private ToolStripButton tbnPre;
        private ToolStripButton tbnRefresh;
        private ToolStripButton tbnShowMPage;
        private ToolStripButton tbnSort;
        private DataSet theDataSet;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolTip toolTip1;
        private int TotalPageNum;
        private ArrayList txtBoxList;
        private ArrayList txtList;
        private Guid userOid;

        public event SelectCAPPResHandler SelectCAPPResChanged;

        public UCResTree() {
            this.curFolder = new DEResFolder();
            this.myResFolder = new DEResFolder();
            this.b_stateShow = true;
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
            this.txtBoxList = new ArrayList();
            this.cobList = new ArrayList();
            this.txtList = new ArrayList();
            this.lblList = new ArrayList();
            this.HT_StepInfo = new Hashtable();
            this.curStepInfo = "";
            this.HT_FilterInfo = new Hashtable();
            this.attrFilterList = new ArrayList();
            this.attrSort = new ArrayList();
            this.attrOuter = new ArrayList();
            this.attrShow = new ArrayList();
            this.HT_AttrIsView = new Hashtable();
            this.InitializeComponent();
            this.lvwDblClickHandler = new EventHandler(this.lvw_DoubleClick);
            this.dropDownPicker = new UltraTextEditor();
            DropDownEditorButton button = new DropDownEditorButton("SelectClass") {
                Key = "SelectClass",
                RightAlignDropDown = DefaultableBoolean.False
            };
            button.BeforeDropDown += new BeforeEditorButtonDropDownEventHandler(this.dropDownEditorButton1_BeforeDropDown);
            this.dropDownPicker.ButtonsRight.Add(button);
            this.dropDownPicker.ReadOnly = true;
            this.pnlPicker.Controls.Add(this.dropDownPicker);
            this.dropDownPicker.Dock = DockStyle.Fill;
            this.resNodeTree = new UCResNodeTree();
            button.Control = this.resNodeTree;
            this.resNodeTree.ResNodeSelected += new SelectResNodeHandler(this.resNodeTree_ResNodeSelected);
            this.AddIcon();
            this.userOid = ClientData.LogonUser.Oid;
            this.InitializeContextMenu();
            this.AddThumCtrl();
        }

        public UCResTree(bool b_visable)
            : this() {
            this.b_stateShow = b_visable;
        }

        private void AddFilter(ArrayList attrList) {
            DEMetaAttribute attribute;
            bool flag = false;
            this.attrFilterList.Clear();
            int count = 0;
            for (int i = 0; i < attrList.Count; i++) {
                attribute = (DEMetaAttribute)attrList[i];
                if (attribute.IsViewable && attribute.IsFilterable) {
                    this.attrFilterList.Add(attribute);
                }
            }
            this.txtBoxList.Clear();
            this.cobList.Clear();
            this.gbFilter.Controls.Clear();
            this.txtList.Clear();
            this.lblList.Clear();
            if (this.attrFilterList.Count <= 0) {
                this.gbFilter.Visible = false;
                this.splitContainer.Panel1Collapsed = true;
                this.tbnClear.Enabled = false;
            } else {
                this.gbFilter.Visible = true;
                this.splitContainer.Panel1Collapsed = false;
                this.tbnClear.Enabled = true;
                count = this.attrFilterList.Count;
                if (this.attrFilterList.Count > 5) {
                    count = 5;
                }
                base.Width = 300;
                this.gbFilter.Size = new Size(base.Width - 2, 0x1a + (this.attrFilterList.Count * 0x17));
                this.splitContainer.SplitterDistance = 0x1a + (count * 0x17);
                int num4 = 0;
                int num2 = 0;
                while (num2 < this.attrFilterList.Count) {
                    attribute = (DEMetaAttribute)this.attrFilterList[num2];
                    LabelPLM lplm = new LabelPLM {
                        Top = 0x18 + (num2 * 0x16),
                        Left = 5,
                        Name = "lbl_" + attribute.Name
                    };
                    string attrLblByLong = this.GetAttrLblByLong(attribute.Label.Trim());
                    lplm.Text = attrLblByLong;
                    lplm.Tag = attribute.Label.Trim();
                    lplm.AutoSize = true;
                    lplm.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                    lplm.MouseHover += new EventHandler(this.LabelFilter_MouseHover);
                    this.lblList.Add(lplm);
                    this.gbFilter.Controls.Add(lplm);
                    if (num4 < lplm.Width) {
                        num4 = lplm.Width;
                    }
                    ComboBoxEditPLM tplm3 = new ComboBoxEditPLM {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Left = lplm.Right + 5,
                        BackColor = SystemColors.Window,
                        Top = 0x18 + (num2 * 0x16),
                        Height = 20,
                        Width = 80
                    };
                    tplm3.BringToFront();
                    tplm3.MouseHover += new EventHandler(this.ComboBoxFilter_MouseHover);
                    if (attribute.DataType == 9) {
                        tplm3.Properties.Items.AddRange(new object[] { "为空", "不为空" });
                        tplm3.Text = "为空";
                    } else if ((attribute.DataType == 11) || (attribute.DataType == 12)) {
                        tplm3.Properties.Items.AddRange(new object[] { "包含", "前几字符是", "后几字符是", "不包含", "前几字符不包含", "后几字符不包含", "为空", "不为空" });
                        tplm3.Text = "包含";
                    } else if (((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6))) {
                        tplm3.Properties.Items.AddRange(new object[] { "等于", "不等于", "大于", "小于", "大于等于", "小于等于", "为空", "不为空" });
                        tplm3.Text = "等于";
                    } else if (attribute.DataType == 8) {
                        tplm3.Properties.Items.AddRange(new object[] { "等于", "不等于", "为空", "不为空" });
                        tplm3.Text = "等于";
                    } else if (((attribute.DataType == 4) || (attribute.DataType == 3)) || (attribute.DataType == 5)) {
                        tplm3.Properties.Items.AddRange(new object[] { "包含", "前几字符是", "后几字符是", "不包含", "前几字符不包含", "后几字符不包含", "等于", "不等于", "为空", "不为空" });
                        tplm3.Text = "包含";
                    } else if (attribute.DataType == 7) {
                        tplm3.Properties.Items.AddRange(new object[] { "等于", "不等于", "大于", "大于等于", "小于", "小于等于", "为空", "不为空" });
                        tplm3.Text = "小于";
                    } else {
                        tplm3.Properties.Items.AddRange(new object[] { "包含", "前几字符是", "后几字符是", "不包含", "前几字符不包含", "后几字符不包含", "等于", "大于", "小于", "大于等于", "小于等于", "不等于", "为空", "不为空" });
                    }
                    tplm3.Name = "comb_" + attribute.Name;
                    if (tplm3.Text == "") {
                        tplm3.Text = "包含";
                    }
                    tplm3.Tag = 0;
                    tplm3.SelectedIndexChanged += new EventHandler(this.comb_SelectedIndexChanged);
                    this.gbFilter.Controls.Add(tplm3);
                    if ((tplm3.Right + 80) > base.Width) {
                        this.gbFilter.Width = 300;
                        this.gbFilter.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        flag = true;
                    } else {
                        this.gbFilter.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        flag = false;
                    }
                    if (attribute.IsResLinkable && (attribute.LinkedResClass != Guid.Empty)) {
                        DEMetaClass class2 = ModelContext.MetaModel.GetClass(attribute.LinkedResClass);
                        if (class2 == null) {
                            return;
                        }
                        ResComboPLM oplm = new ResComboPLM(class2.Name, attribute);
                        oplm.SetFromEdit(false);
                        oplm.Parent = this;
                        oplm.Tag = attribute;
                        oplm.NullText = "";
                        oplm.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        oplm.KeyPress += new KeyPressEventHandler(this.ResViewer_KeyPress);
                        oplm.SetBounds(tplm3.Right + 5, 0x18 + (num2 * 0x16), 80, 0x15);
                        this.txtBoxList.Add(oplm);
                        this.gbFilter.Controls.Add(oplm);
                        oplm.ResValue = "";
                        oplm.Visible = true;
                        oplm.Focus();
                        oplm.BringToFront();
                    } else if (attribute.DataType2 == PLMDataType.DateTime) {
                        TimeComboInputPLM tplm2 = new TimeComboInputPLM();
                        tplm2.SetStyle("");
                        tplm2.ReadOnly = false;
                        tplm2.Name = "dateTimePicker1";
                        tplm2.Size = new Size(80, 0x15);
                        tplm2.Left = tplm3.Right + 5;
                        tplm2.Top = 0x18 + (num2 * 0x16);
                        tplm2.Tag = attribute;
                        tplm2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        tplm2.KeyPress += new KeyPressEventHandler(this.dtpicker_KeyPress);
                        this.txtBoxList.Add(tplm2);
                        this.gbFilter.Controls.Add(tplm2);
                    } else if (attribute.SpecialType2 != PLMSpecialType.Unknown) {
                        UCSelectPrinPLM nplm = null;
                        switch (attribute.SpecialType2) {
                            case PLMSpecialType.UserType:
                                nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectUser);
                                break;

                            case PLMSpecialType.OrganizationType:
                                nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectOrg);
                                break;

                            case PLMSpecialType.RoleType:
                                nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectRole);
                                break;
                        }
                        if (attribute.DataType2 == PLMDataType.String) {
                            nplm.InputAllow = true;
                        } else {
                            nplm.InputAllow = false;
                        }
                        nplm.Text = "";
                        nplm.KeyPress += new KeyPressEventHandler(this.ucPrin_KeyPress);
                        nplm.Size = new Size(80, 0x15);
                        nplm.Left = tplm3.Right + 5;
                        nplm.Top = 0x18 + (num2 * 0x16);
                        nplm.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        this.txtBoxList.Add(nplm);
                        this.gbFilter.Controls.Add(nplm);
                    } else if (attribute.Name == "M_STATE") {
                        ComboBoxEditPLM tplm4 = new ComboBoxEditPLM {
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        tplm4.Properties.Items.Add("全部");
                        tplm4.Properties.Items.Add("检入");
                        tplm4.Properties.Items.Add("检出");
                        tplm4.Properties.Items.Add("定版");
                        tplm4.SelectedIndex = 0;
                        tplm4.Left = tplm3.Right + 5;
                        tplm4.Top = 0x18 + (num2 * 0x16);
                        tplm4.Name = "cmbValue_" + attribute.Name;
                        tplm4.Size = new Size(80, 0x15);
                        tplm4.Tag = attribute;
                        tplm4.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        tplm4.KeyUp += new KeyEventHandler(this.cmb_KeyUp);
                        this.txtBoxList.Add(tplm4);
                        this.gbFilter.Controls.Add(tplm4);
                    } else {
                        TextEditPLM tplm = new TextEditPLM {
                            Left = tplm3.Right + 5,
                            BackColor = SystemColors.Window,
                            Top = 0x18 + (num2 * 0x16),
                            Name = "txtValue_" + attribute.Name,
                            Size = new Size(80, 0x15),
                            Text = "",
                            Tag = attribute,
                            Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top
                        };
                        tplm.KeyUp += new KeyEventHandler(this.txtValue_KeyUp);
                        this.txtBoxList.Add(tplm);
                        this.gbFilter.Controls.Add(tplm);
                        if (attribute.DataType2 == PLMDataType.Grid) {
                            tplm.Visible = false;
                        }
                    }
                    this.cobList.Add(tplm3);
                    num2++;
                }
                int width = base.Width;
                if (flag) {
                    width = 300;
                }
                for (num2 = 0; num2 < this.cobList.Count; num2++) {
                    ComboBoxEditPLM tplm5 = (ComboBoxEditPLM)this.cobList[num2];
                    tplm5.Left = (num4 < 0x2d) ? 50 : (num4 + 5);
                    tplm5.Width = 0x58;
                    Control control = this.txtBoxList[num2] as Control;
                    control.Left = tplm5.Right + 5;
                    if (count == 5) {
                        control.Width = (width - control.Left) - 0x19;
                    } else {
                        control.Width = (width - control.Left) - 10;
                    }
                }
            }
        }

        private void AddIcon() {
            string[] resNames = new string[] { "ICO_EVT_MONITOR", "ICO_FDL_OPEN", "ICO_FDL_CLOSE", "ICO_RES_PRE", "ICO_RES_NXT", "ICO_PREVIOUS", "ICO_NEXT", "ICO_VIW_SAVE", "ICO_ENV_TOOL", "ICO_PPM_BROWSE", "ICO_RES_CLEAR", "ICO_RES_REFRESH", "ICO_RES_FDL_OPEN", "ICO_RES_FDL", "ICO_RES_NODE" };
            ClientData.MyImageList.AddIcons(resNames);
            this.tb.ImageList = ClientData.MyImageList.imageList;
            this.tbnSort.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_EVT_MONITOR");
            this.tbnShowMPage.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_FDL_OPEN");
            this.tbnPre.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PRE");
            this.tbnNext.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NXT");
            this.tbnClear.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_CLEAR");
            this.tbnRefresh.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_REFRESH");
            this.btn_upStep.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PREVIOUS");
            this.btn_downStep.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_NEXT");
            this.btn_viewFile.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PPM_BROWSE");
            this.btn_Float.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_ENV_TOOL");
        }

        private void AddThumCtrl() {
            if (this.lvw.ThumSetting == null) {
                this.lvw.AddThumCtrl(this.pnlLvw, PLMLocation.ResourceList.ToString(), true, false);
                this.lvw.OnDisplayStyleChangedHander = new DisplayStyleChangedHandler(this.OnDisplayStyleChanged);
                this.lvw.OnScroll += new ThumbnailListView.ListViewScroll(this.OnScrollChanged);
                Control control = this.pnlLvw.Controls[0];
                control.Dock = DockStyle.Fill;
                control.Width = this.pnlLvw.Width;
                control.Height = this.pnlLvw.Height;
            }
        }

        private void BingdingData() {
            this.AddFilter(this.attrList);
            if (this.theDataSet == null) {
                this.SetTurnPageUnState();
                this.SetUnMpageState();
            } else if (this.int_rowCount == 0) {
                this.SetTurnPageUnState();
                this.SetUnMpageState();
                this.SetStatusInfo(0, 0, 0);
            } else {
                this.orgView = this.GetOrgDataView(this.curFolder);
                this.myView = this.orgView;
                this.InitLvShowHeader();
                this.showNum = 50;
                this.FirstDisplayData(this.orgView);
            }
        }

        private void btn_downStep_Click(object sender, EventArgs e) {
            this.b_historySate = true;
            if (this.I_StepNum >= 1) {
                this.btn_upStep.Enabled = true;
            }
            if ((this.I_StepNum + 1) == this.HT_StepInfo.Count) {
                this.btn_downStep.Enabled = false;
            }
            this.I_StepNum++;
            StepInfo historyInfo = this.GetHistoryInfo(this.I_StepNum);
            this.LocationNode(historyInfo);
        }

        private void btn_Float_Click(object sender, EventArgs e) {
            if (this.btn_Float.Checked) {
                this.btn_Float.Checked = false;
                this.btn_Float.ToolTipText = "源文件分屏显示(分屏/浮动)";
            } else {
                this.btn_Float.Checked = true;
                this.btn_Float.ToolTipText = "源文件浮动窗口显示(浮动/分屏)";
            }
            if (ClientData.Instance.ResViewFileHander != null) {
                ClientData.Instance.ResViewFileHander(this.curFolder, null, this.btn_Float.Checked);
            }
        }

        private void btn_upStep_Click(object sender, EventArgs e) {
            this.b_historySate = true;
            if (this.I_StepNum <= this.HT_StepInfo.Count) {
                this.btn_downStep.Enabled = true;
            }
            if (this.I_StepNum == 2) {
                this.btn_upStep.Enabled = false;
            }
            this.I_StepNum--;
            StepInfo historyInfo = this.GetHistoryInfo(this.I_StepNum);
            this.LocationNode(historyInfo);
        }

        private void btn_viewFile_Click(object sender, EventArgs e) {
            this.ViewResFile();
        }

        private void cB_pageSize_SelectedIndexChanged(object sender, EventArgs e) {
            if (!this.isAllBtnClick) {
                this.SetDpDnMPageState(this.cB_pageSize.Text);
                if (this.cB_pageSize.SelectedIndex == 0) {
                    if (this.int_rowCount > (this.AlertPageNum * 50)) {
                        this.tbnShowMPage.Checked = false;
                        MessageBoxPLM.Show("总记录数超过" + ((this.AlertPageNum * 50)).ToString() + "条时，不能全部显示！", "普通提示");
                        this.cB_pageSize.SelectedIndex = 1;
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
                    this.SetLiveMpageState(this.cB_pageSize.SelectedIndex);
                    this.ShowNumDataView(this.orgView, 0, this.showNum);
                    this.SetStatusInfo(1, this.TotalPageNum, this.int_rowCount);
                }
            }
        }

        private void ClearConditon() {
            this.ClearFilterCtrl();
            try {
                if (((this.myResFolder != null) && (this.myResFolder.Filter != null)) && (this.myResFolder.Filter != "")) {
                    this.curFolder.FilterString = this.myResFolder.FilterString;
                    this.curFolder.FilterValue = this.myResFolder.FilterValue;
                } else {
                    this.curFolder.FilterString = "";
                    this.curFolder.FilterValue = "";
                }
                this.InitData();
                this.orgView = this.GetOrgDataView(this.curFolder);
                this.showNum = 50;
                this.FirstDisplayData(this.orgView);
            } catch (Exception exception) {
                MessageBoxPLM.Show("过滤数据发生错误" + exception.ToString(), "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void ClearFilterCtrl() {
            for (int i = 0; i < this.txtBoxList.Count; i++) {
                if (((DEMetaAttribute)this.attrFilterList[i]).DataType == 7) {
                    TimeComboInputPLM tplm2 = (TimeComboInputPLM)this.txtBoxList[i];
                    tplm2.Text = "";
                } else if (((DEMetaAttribute)this.attrFilterList[i]).IsResLinkable) {
                    ResComboPLM oplm = (ResComboPLM)this.txtBoxList[i];
                    oplm.ResValue = "";
                } else if (((DEMetaAttribute)this.attrFilterList[i]).SpecialType2 != PLMSpecialType.Unknown) {
                    UCSelectPrinPLM nplm = this.txtBoxList[i] as UCSelectPrinPLM;
                    if (nplm != null) {
                        nplm.Text = "";
                    }
                } else if (((DEMetaAttribute)this.attrFilterList[i]).Name == "M_STATE") {
                    ComboBoxEditPLM tplm4 = (ComboBoxEditPLM)this.txtBoxList[i];
                    tplm4.SelectedIndex = 0;
                } else {
                    TextEditPLM tplm = (TextEditPLM)this.txtBoxList[i];
                    tplm.Text = "";
                    ComboBoxEditPLM tplm3 = (ComboBoxEditPLM)this.cobList[i];
                    if ((tplm3.Text == "在..之间") || (tplm3.Text == "不在..之间")) {
                        tplm.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                        tplm.Width = (base.Width - tplm.Left) - 5;
                        tplm3.Tag = 0;
                        for (int j = 0; j < this.lblList.Count; j++) {
                            LabelPLM lplm = (LabelPLM)this.lblList[j];
                            TextEditPLM tplm5 = (TextEditPLM)this.txtList[j];
                            if (lplm.Tag == tplm.Tag) {
                                this.gbFilter.Controls.Remove(lplm);
                                this.lblList.Remove(lplm);
                            }
                            if (tplm5.Tag == tplm.Tag) {
                                this.gbFilter.Controls.Remove(tplm5);
                                this.txtList.Remove(tplm5);
                            }
                        }
                    }
                    tplm3.Text = "包含";
                }
            }
        }

        private void cmb_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.FilterData();
            }
        }

        public void cmi_Click(object sender, EventArgs e) {
            this.UpdateFlag(Convert.ToString(((MenuItemEx)sender).Name));
        }

        public void cmi_Clone(object sender, EventArgs e) {
            if (this.lvw.SelectedItems != null) {
                if (this.lvw.SelectedItems.Count > 1) {
                    MessageBoxPLM.Show("类似创建只能选择一个对象", "引用资源", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else {
                    try {
                        foreach (ListViewItem item in this.lvw.SelectedItems) {
                            DataRowView tag = (DataRowView)item.Tag;
                            try {
                                if (this.b_refType) {
                                    DEBusinessItem busObj = PLItem.Agent.GetBizItemByMaster(new Guid((byte[])tag[0]), 0, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                                    if ((busObj != null) && (ClientData.Instance.D_CloneRefResOB != null)) {
                                        ClientData.Instance.D_CloneRefResOB(busObj);
                                    }
                                }
                            } catch (PLMException exception) {
                                PrintException.Print(exception);
                                return;
                            }
                        }
                    } catch (Exception exception2) {
                        PrintException.Print(exception2);
                    }
                }
            }
        }

        public void cmi_ViewFile(object sender, EventArgs e) {
            this.ViewResFile();
        }

        private void comb_SelectedIndexChanged(object sender, EventArgs e) {
            TextEditPLM tplm2;
            int num = -1;
            for (int i = 0; i < this.cobList.Count; i++) {
                ComboBoxEditPLM tplm = (ComboBoxEditPLM)this.cobList[i];
                if (tplm.ContainsFocus) {
                    if (tplm.Text.IndexOf("空") != -1) {
                        if (this.txtBoxList[i] is TextEditPLM) {
                            (this.txtBoxList[i] as TextEditPLM).Visible = false;
                            (this.txtBoxList[i] as TextEditPLM).Text = "";
                            this.FilterData();
                        }
                        if (this.txtBoxList[i] is TimeComboInputPLM) {
                            (this.txtBoxList[i] as TimeComboInputPLM).Visible = false;
                        }
                        if (this.txtBoxList[i] is ResComboPLM) {
                            (this.txtBoxList[i] as ResComboPLM).ResValue = "";
                            (this.txtBoxList[i] as ResComboPLM).Visible = false;
                        }
                    } else {
                        if (this.txtBoxList[i] is TextEditPLM) {
                            (this.txtBoxList[i] as TextEditPLM).Visible = true;
                        }
                        if (this.txtBoxList[i] is TimeComboInputPLM) {
                            (this.txtBoxList[i] as TimeComboInputPLM).Visible = true;
                        }
                        if (this.txtBoxList[i] is ResComboPLM) {
                            (this.txtBoxList[i] as ResComboPLM).Visible = true;
                        }
                    }
                    int tag = (int)tplm.Tag;
                    if ((tplm.Text == "在..之间") || (tplm.Text == "不在..之间")) {
                        if (tag == 1) {
                            return;
                        }
                        num = i;
                        tplm.Tag = 1;
                    } else {
                        switch (tag) {
                            case 0:
                                return;

                            case 1:
                                for (int j = 0; j < this.lblList.Count; j++) {
                                    tplm2 = (TextEditPLM)this.txtBoxList[i];
                                    tplm2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                                    tplm2.Width = (base.Width - tplm2.Left) - 5;
                                    tplm.Tag = 0;
                                    LabelPLM lplm = (LabelPLM)this.lblList[j];
                                    TextEditPLM tplm3 = (TextEditPLM)this.txtList[j];
                                    if (lplm.Tag == tplm2.Tag) {
                                        this.gbFilter.Controls.Remove(lplm);
                                        this.lblList.Remove(lplm);
                                    }
                                    if (tplm3.Tag == tplm2.Tag) {
                                        this.gbFilter.Controls.Remove(tplm3);
                                        this.txtList.Remove(tplm3);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            if (num >= 0) {
                tplm2 = (TextEditPLM)this.txtBoxList[num];
                tplm2.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                tplm2.Width = ((base.Width - tplm2.Left) - 50) / 2;
                LabelPLM lplm2 = new LabelPLM {
                    Top = tplm2.Top,
                    Left = tplm2.Right + 5,
                    Text = " AND ",
                    Width = 0x23,
                    Tag = tplm2.Tag
                };
                this.lblList.Add(lplm2);
                TextEditPLM tplm4 = new TextEditPLM {
                    Top = tplm2.Top,
                    Left = lplm2.Right + 5,
                    Text = ""
                };
                tplm4.Width = (base.Width - 5) - tplm4.Left;
                tplm4.Tag = tplm2.Tag;
                tplm4.BackColor = SystemColors.Window;
                tplm4.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                tplm4.KeyUp += new KeyEventHandler(this.txtValue_KeyUp);
                this.txtList.Add(tplm4);
                this.gbFilter.Controls.Add(lplm2);
                this.gbFilter.Controls.Add(tplm4);
            }
        }

        private void ComboBoxFilter_MouseHover(object sender, EventArgs e) {
            ComboBoxEditPLM control = sender as ComboBoxEditPLM;
            if (!string.IsNullOrEmpty(control.Text)) {
                string text = control.Text;
                this.toolTip1.SetToolTip(control, text);
            }
        }

        private int ComputTotalPageNum(int i_total, int i_show) {
            return (((i_total % i_show) == 0) ? (i_total / i_show) : ((i_total / i_show) + 1));
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
                this.AddFilter(this.attrList);
                if (this.theDataSet == null) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                } else if (this.int_rowCount == 0) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.SetStatusInfo(0, 0, 0);
                } else {
                    this.orgView = this.GetOrgDataView(this.curFolder);
                    this.myView = this.orgView;
                    this.InitLvShowHeader();
                    this.showNum = 50;
                    this.FirstDisplayData(this.orgView);
                }
            }
        }

        private void dropDownEditorButton1_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e) {
            this.resNodeTree.ResNodeTree.Dock = DockStyle.Fill;
            if (this.dropDownPicker.Width > 150) {
                this.resNodeTree.Width = this.dropDownPicker.Width;
            } else {
                this.resNodeTree.Width = 150;
            }
        }

        private void dtpicker_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                this.FilterData();
            }
        }

        protected void FillItemImage() {
            if (this.lvw.ThumSetting.DisplayStyle != DisplayStyle.Detail) {
                lock (typeof(bool)) {
                    if (this.isLoadingImage) {
                        return;
                    }
                    this.isLoadingImage = true;
                }
                try {
                    ThumbnailListView.FillItemImage(this.lvw);
                } finally {
                    lock (typeof(bool)) {
                        this.isLoadingImage = false;
                    }
                }
            }
        }

        private void FilterData() {
            int num3 = 0;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            string strFilterStr = "";
            string strFilterVal = "";
            if (this.myView != null) {
                for (int i = 0; i < this.txtBoxList.Count; i++) {
                    TextBox box;
                    ComboBox cob = (ComboBox)this.cobList[i];
                    if (((DEMetaAttribute)this.attrFilterList[i]).DataType == 7) {
                        TimeComboInput input = (TimeComboInput)this.txtBoxList[i];
                        box = new TextBox();
                        DateTime time = new DateTime();
                        if (input.Text.Trim().Length != 0) {
                            try {
                                box.Text = Convert.ToDateTime(input.Text).ToShortDateString();
                            } catch {
                                input.Text = "";
                                box.Text = "";
                            }
                        }
                        box.Tag = this.attrFilterList[i];
                        goto Label_02D1;
                    }
                    if (((DEMetaAttribute)this.attrFilterList[i]).IsResLinkable) {
                        ResCombo combo = (ResCombo)this.txtBoxList[i];
                        box = new TextBox {
                            Text = combo.ResValue,
                            Tag = this.attrFilterList[i]
                        };
                        goto Label_02D1;
                    }
                    if (((DEMetaAttribute)this.attrFilterList[i]).SpecialType2 != PLMSpecialType.Unknown) {
                        UCSelectPrinPLM nplm = this.txtBoxList[i] as UCSelectPrinPLM;
                        box = new TextBox();
                        if (((DEMetaAttribute)this.attrFilterList[i]).DataType2 == PLMDataType.String) {
                            box.Text = nplm.Text;
                        } else if ((((DEMetaAttribute)this.attrFilterList[i]).DataType2 == PLMDataType.Guid) && (nplm.Tag is DEPrincipal)) {
                            box.Text = (nplm.Tag as DEPrincipal).Oid.ToString();
                        }
                        box.Tag = this.attrFilterList[i];
                        goto Label_02D1;
                    }
                    if (((DEMetaAttribute)this.attrFilterList[i]).Name != "M_STATE") {
                        goto Label_02BE;
                    }
                    ComboBox box4 = (ComboBox)this.txtBoxList[i];
                    box = new TextBox {
                        Text = ""
                    };
                    string str3 = box4.SelectedItem.ToString();
                    if (str3 != null) {
                        if (str3 != "检入") {
                            if (str3 == "检出") {
                                goto Label_0291;
                            }
                            if (str3 == "定版") {
                                goto Label_029E;
                            }
                        } else {
                            box.Text = "I";
                        }
                    }
                    goto Label_02A9;
                Label_0291:
                    box.Text = "O";
                    goto Label_02A9;
                Label_029E:
                    box.Text = "R";
                Label_02A9:
                    box.Tag = this.attrFilterList[i];
                    goto Label_02D1;
                Label_02BE:
                    box = (TextBox)this.txtBoxList[i];
                Label_02D1:
                    if (cob.Text.IndexOf("空") != -1) {
                        num3++;
                        if (num3 > 1) {
                            builder.Append(" AND ");
                        }
                        ResFunc.CreateCondition(cob, box, this.txtList, this.emResType, this.attrOuter, this.curFolder, out strFilterStr, out strFilterVal);
                        builder.Append(strFilterStr);
                    } else if ((box.Text != null) && (box.Text != "")) {
                        if ((cob.Text == "在..之间") || (cob.Text == "不在..之间")) {
                            DEMetaAttribute tag = (DEMetaAttribute)box.Tag;
                            for (int j = 0; j < this.txtList.Count; j++) {
                                TextBox box2 = (TextBox)this.txtList[j];
                                DEMetaAttribute attribute2 = (DEMetaAttribute)box2.Tag;
                                if ((tag.Oid == attribute2.Oid) && (box2.Text != "")) {
                                    num3++;
                                    if (num3 > 1) {
                                        builder.Append(" AND ");
                                        if (builder2.Length > 0) {
                                            builder2.Append(",");
                                        }
                                    }
                                    ResFunc.CreateCondition(cob, box, this.txtList, this.emResType, this.attrOuter, this.curFolder, out strFilterStr, out strFilterVal);
                                    builder.Append(strFilterStr);
                                    builder2.Append(strFilterVal);
                                }
                            }
                        } else {
                            num3++;
                            if (num3 > 1) {
                                builder.Append(" AND ");
                                if (builder2.Length > 0) {
                                    builder2.Append(",");
                                }
                            }
                            ResFunc.CreateCondition(cob, box, this.txtList, this.emResType, this.attrOuter, this.curFolder, out strFilterStr, out strFilterVal);
                            builder.Append(strFilterStr);
                            builder2.Append(strFilterVal);
                        }
                    }
                }
                try {
                    this.curFolder.FilterValue = this.myResFolder.FilterValue;
                    if ((this.myResFolder.Filter == null) || (this.myResFolder.Filter == "")) {
                        this.curFolder.FilterString = builder.ToString();
                        this.curFolder.FilterValue = builder2.ToString();
                    } else if ((builder == null) || (builder.ToString().Trim() == "")) {
                        this.curFolder.FilterString = this.myResFolder.FilterString.ToString();
                        if (!string.IsNullOrEmpty(this.myResFolder.FilterValue)) {
                            this.curFolder.FilterValue = this.myResFolder.FilterValue.ToString();
                        } else {
                            this.curFolder.FilterValue = "";
                        }
                    } else {
                        this.curFolder.FilterString = "(" + this.myResFolder.FilterString.ToString() + ") AND (" + builder.ToString() + ")";
                        if (!string.IsNullOrEmpty(builder2.ToString())) {
                            if (string.IsNullOrEmpty(this.myResFolder.FilterValue)) {
                                this.curFolder.FilterValue = builder2.ToString();
                            } else {
                                this.curFolder.FilterValue = this.myResFolder.FilterValue.ToString() + "," + builder2.ToString();
                            }
                        }
                    }
                    this.InitData();
                    this.orgView = this.theDataSet.Tables[0].DefaultView;
                    this.showNum = 50;
                    this.FirstDisplayData(this.orgView);
                } catch (Exception exception) {
                    MessageBox.Show("过滤数据发生错误:请检查输入的数据类型是否正确:" + exception.Message, "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
                this.b_first = true;
                if (this.int_rowCount <= this.showNum) {
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                    this.SetStatusInfo(1, 1, this.int_rowCount);
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
                if (this.lvw.SelectedItems.Count == 0) {
                    this.lvw.PreviewClear();
                } else {
                    this.lvw.PreviewFile();
                }
            }
        }

        private ArrayList GetAttributes(string classname) {
            return ModelContext.MetaModel.GetAttributes(classname);
        }
        private string GetAttrLblByLong(string str_lbl) {
            bool flag3;
            ArrayList list = new ArrayList();
            for (int i = 0x21; i <= 0x7f; i++) {
                list.Add(((char)i).ToString());
            }
            string str = str_lbl;
            if (str.Length <= 20) {
                flag3 = true;
                for (int j = 0; j < str.Length; j++) {
                    bool flag4 = false;
                    string item = str.Substring(j, 1);
                    if (list.Contains(item)) {
                        flag4 = true;
                    }
                    if (!flag4) {
                        flag3 = false;
                        break;
                    }
                }
            } else {
                bool flag = true;
                for (int k = 0; k < 20; k++) {
                    bool flag2 = false;
                    string str2 = str.Substring(k, 1);
                    if (list.Contains(str2)) {
                        flag2 = true;
                    }
                    if (!flag2) {
                        flag = false;
                        break;
                    }
                }
                if (flag) {
                    return (str.Substring(0, 20) + "...");
                }
                return (str.Substring(0, 10) + "...");
            }
            if (!flag3 && (str.Length > 10)) {
                str = str.Substring(0, 10) + "...";
            }
            return str;
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

        private StepInfo GetHistoryInfo(int iStep) {
            if (iStep <= this.HT_StepInfo.Count) {
                return (this.HT_StepInfo[iStep] as StepInfo);
            }
            return null;
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

        private DataView GetOrgDataView(DEResFolder curFolder) {
            DataView view = new DataView();
            return this.theDataSet.Tables[0].DefaultView;
        }

        private DataView GetOrgDataViewByCAPP() {
            DataView view = new DataView();
            return this.theDataSet.Tables[0].DefaultView;
        }

        private void GetPathInfo(TreeNode theNode, ArrayList al_path) {
            if (theNode.Parent != null) {
                al_path.Insert(0, theNode.Parent.Text);
                this.GetPathInfo(theNode.Parent, al_path);
            }
        }

        public static Control GetResTreeUCtrl(string nodeName, Hashtable ht_param) {
            ResTreeInstance = null;
            if (ResTreeInstance == null) {
                ResTreeInstance = new UCResTree();
                (ResTreeInstance as UCResTree).SetParameter(nodeName, ht_param);
                ResTreeInstance.Show();
            } else {
                (ResTreeInstance as UCResTree).SetParameter(nodeName, ht_param);
                (ResTreeInstance as UCResTree).LocationNode();
            }
            return ResTreeInstance;
        }

        private Hashtable GetViewFile(DEResFolder theFolder) {
            Hashtable hashtable = new Hashtable();
            if (ModelContext.MetaModel.GetClass(theFolder.ClassName).IsTableResClass) {
                PLFiles files = new PLFiles();
                ArrayList objList = new ArrayList();
                DEResObj obj2 = new DEResObj();
                DataRowView tag = (DataRowView)this.lvw.SelectedItems[0].Tag;
                obj2.Oid = new Guid((byte[])tag["PLM_OID"]);
                objList.Add(obj2);
                ArrayList allFiles = files.GetAllFiles(objList);
                if ((allFiles != null) && (allFiles.Count > 0)) {
                    hashtable.Add("RESFILES", allFiles);
                }
                return hashtable;
            }
            if (this.lvw.SelectedItems.Count > 0) {
                for (int i = 0; i < this.lvw.Columns.Count; i++) {
                    ColumnHeader header = this.lvw.Columns[i];
                    foreach (DEMetaAttribute attribute in this.attrList) {
                        if ((attribute.Label == header.Text) && !hashtable.ContainsKey(attribute)) {
                            hashtable.Add(attribute, this.lvw.SelectedItems[0].SubItems[i].Text);
                        }
                    }
                }
            }
            return hashtable;
        }

        private void InitAllOcxState() {
            this.gbFilter.Visible = false;
            this.splitContainer.Panel1Collapsed = true;
            this.InitOrgToolBarState();
            this.SetStatusInfo(0, 0, 0);
            this.lvw.Items.Clear();
            if (this.numViewData > 0) {
                this.gbData.Controls.Clear();
                this.gbData.Controls.Add(this.pnlLvw);
                this.pnlLvw.Dock = DockStyle.Fill;
                this.lvw.Dock = DockStyle.Fill;
                this.lvw.Items.Clear();
                this.lvw.Columns.Clear();
                this.InitializeContextMenu();
            }
        }

        private void InitCAPPData(Guid g_clsid) {
            if (this.str_CurClsLbl != ModelContext.MetaModel.GetClass(g_clsid).Label) {
                this.myResFolder = new DEResFolder();
                this.int_rowCount = 0;
                this.b_first = true;
                this.b_refType = false;
                this.emResType = emResourceType.Customize;
                this.str_CurClsLbl = ModelContext.MetaModel.GetClass(g_clsid).Label;
                string name = ModelContext.MetaModel.GetClass(g_clsid).Name;
                this.gbFilter.Visible = false;
                this.splitContainer.Panel1Collapsed = true;
                this.clsName = name;
                this.resName = name;
                this.dropDownPicker.Text = this.str_CurClsLbl;
                if (this.curFolder == null) {
                    this.curFolder = new DEResFolder();
                }
                this.curFolder.Oid = g_clsid;
                this.curFolder.ClassOid = g_clsid;
                this.curFolder.Name = "byCAPP";
                this.curFolder.ClassName = name;
                this.curFolder.Filter = "";
                this.curFolder.FilterString = "";
                this.curFolder.FilterValue = "";
                this.myResFolder.Oid = g_clsid;
                this.myResFolder.ClassOid = g_clsid;
                this.myResFolder.Name = "byCAPP";
                this.myResFolder.ClassName = name;
                this.myResFolder.Filter = "";
                this.myResFolder.FilterString = "";
                this.myResFolder.FilterValue = "";
                this.InitAllOcxState();
                ArrayList showAttrList = ResFunc.GetShowAttrList(this.curFolder, emTreeType.NodeTree);
                this.attrList = ResFunc.CloneMetaAttrLst(showAttrList);
                this.attrSort = ResFunc.GetSortAttrList(this.curFolder);
                this.attrOuter = ResFunc.GetOuterAttr(this.curFolder);
                this.InitResStatus();
                this.InitSortList(true);
                this.InitShowAttrLst();
                this.SetAttrDataType();
                this.myView = null;
                try {
                    ArrayList clsTreeCls = new ArrayList();
                    clsTreeCls = new PLReference().GetClsTreeCls(this.curFolder.Oid);
                    if (clsTreeCls.Count > 0) {
                        DEDefCls cls = (DEDefCls)clsTreeCls[0];
                        this.curFolder.Filter = cls.FILTER;
                        this.curFolder.FilterString = cls.FILTERSTRING;
                        this.curFolder.FilterValue = cls.FILTERVALUE;
                        this.myResFolder.Filter = cls.FILTER;
                        this.myResFolder.FilterString = cls.FILTERSTRING;
                        this.myResFolder.FilterValue = cls.FILTERVALUE;
                    } else {
                        this.curFolder.Filter = "";
                        this.curFolder.FilterString = "";
                        this.curFolder.FilterValue = "";
                        this.myResFolder.Filter = "";
                        this.myResFolder.FilterString = "";
                        this.myResFolder.FilterValue = "";
                    }
                    this.Cursor = Cursors.WaitCursor;
                    this.InitData();
                    this.Cursor = Cursors.Default;
                } catch (PLMException exception) {
                    PrintException.Print(exception);
                } catch (Exception exception2) {
                    MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                this.tbnClear.Enabled = false;
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
                    this.resName = class2.Name;
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
                    ArrayList list2 = new ArrayList();
                    this.int_rowCount = ResFunc.GetDataCount(this.curFolder, list2, this.attrOuter, emResourceType.Customize);
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

        private void InitializeContextMenu() {
            this.lvw.DoubleClick -= this.lvwDblClickHandler;
            if (this.b_stateShow) {
                this.lvw.DoubleClick += this.lvwDblClickHandler;
            }
            this.cmiA = new MenuItemEx("复制记录", new EventHandler(this.cmi_Click), null);
            this.cmiA.Name = "RecordCopy";
            this.cmiB = new MenuItemEx("复制取值", new EventHandler(this.cmi_Click), null);
            this.cmiB.Name = "AttrValCopy";
            this.cmiClone = new MenuItemEx("类似创建", new EventHandler(this.cmi_Clone), null);
            this.cmiClone.Name = "Clone";
            this.cmiViewFile = new MenuItemEx("显示源文件", new EventHandler(this.cmi_ViewFile), null);
            this.cmiViewFile.Name = "ViewFile";
            this.cmuDgd = new ContextMenu();
            this.cmuDgd.MenuItems.AddRange(new MenuItem[] { this.cmiB, this.cmiClone, this.cmiViewFile });
            this.cmiB.Visible = true;
        }

        private void InitLvShowHeader() {
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            this.lvw.Columns.Clear();
            int item = 0;
            for (int i = 0; i < this.myView.Table.Columns.Count; i++) {
                foreach (DEMetaAttribute attribute in this.attrList) {
                    if (this.ISDefAttrViewable(attribute) && (this.myView.Table.Columns[i].ColumnName == ("PLM_" + attribute.Name))) {
                        if ((((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6))) && !list.Contains(item)) {
                            list.Add(item);
                        }
                        if ((attribute.DataType == 7) && !list2.Contains(item)) {
                            list2.Add(item);
                        }
                        ColumnHeader header = new ColumnHeader {
                            Text = attribute.Label
                        };
                        int defAttrWidth = this.GetDefAttrWidth(attribute);
                        if (defAttrWidth > 0) {
                            header.Width = defAttrWidth;
                        }
                        this.lvw.Columns.Add(header);
                        item++;
                        break;
                    }
                }
            }
            if (item <= 3) {
                foreach (ColumnHeader header2 in this.lvw.Columns) {
                    header2.Width = (base.Width - 10) / item;
                }
            }
            ResFunc.SetListViewColumnNum(this.lvw, list);
            ResFunc.SetListViewColumnDate(this.lvw, list2);
            this.SetImageList();
        }

        private void InitOrgToolBarState() {
            this.tbnSort.Enabled = false;
            this.tBnOutput.Enabled = false;
            this.SetTurnPageUnState();
            this.SetUnMpageState();
            this.tbnRefresh.Enabled = false;
            this.tbnClear.Enabled = false;
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
            if ((b_first && (userOption != null)) && ((userOption.Trim().Length > 0) && (userOption != "nothing"))) {
                this.strSort = userOption;
            }
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

        private bool ISDefAttrViewable(DEMetaAttribute demattr) {
            bool flag = false;
            if (this.attrShow.Count > 0) {
                if (this.HT_AttrIsView.ContainsKey(this.curFolder.Oid + "|" + demattr.Oid)) {
                    return Convert.ToBoolean(this.HT_AttrIsView[this.curFolder.Oid + "|" + demattr.Oid]);
                }
                foreach (DEDefAttr attr in this.attrShow) {
                    if (demattr.Oid == attr.ATTROID) {
                        this.HT_AttrIsView.Add(this.curFolder.Oid + "|" + demattr.Oid, attr.ISVISUAL);
                        return attr.ISVISUAL;
                    }
                }
                return flag;
            }
            return demattr.IsViewable;
        }

        private bool IsValidKey(string strKey) {
            try {
                Convert.ToDecimal(strKey);
                return true;
            } catch {
                return false;
            }
        }

        private void LabelFilter_MouseHover(object sender, EventArgs e) {
            LabelPLM control = sender as LabelPLM;
            if (control.Tag != null) {
                string tag = control.Tag as string;
                this.toolTip1.SetToolTip(control, tag);
            }
        }

        private void LiveToolBarState() {
            this.tbnSort.Enabled = true;
            this.tBnOutput.Enabled = true;
            this.tbnRefresh.Enabled = true;
            this.tbnClear.Enabled = true;
        }

        private void LoadHistoryInfo() {
            DateTime now = DateTime.Now;
            string paramName = "ResStep-" + ClientData.LogonUser.LogId;
            object systemParameter = new PLSystemParam().GetSystemParameter(paramName);
            TestReport.TestProgram(Guid.Empty, "LoadHistoryInfo", "GetSystemParameter", now);
            DateTime time2 = DateTime.Now;
            if (systemParameter != null) {
                StepInfo stepinfo = new StepInfo();
                stepinfo.ConvertToPathInfo(systemParameter.ToString());
                this.resNodeTree.Show();
                this.LocationNode(stepinfo);
            }
            TestReport.TestProgram(Guid.Empty, "LoadHistoryInfo", "this.LocationNode(spinfo)", time2);
        }

        private void LoadHistoryInfoByName(string nodeName) {
            StepInfo stepinfo = new StepInfo();
            stepinfo.ConvertToPathInfo(nodeName);
            this.resNodeTree.Show();
            this.LocationNode(stepinfo);
        }

        public void LocationNode() {
            this.ClearFilterCtrl();
            if (string.IsNullOrEmpty(this.curStepInfo)) {
                if (this.curFolder.ClassOid != Guid.Empty) {
                    this.FilterData();
                }
            } else {
                bool flag = false;
                StepInfo info = new StepInfo();
                info.ConvertToPathInfo(this.curStepInfo);
                if (!info.isLocationNode(this.resNodeTree.ResNodeTree)) {
                    this.LoadHistoryInfoByName(this.curStepInfo);
                } else {
                    flag = true;
                }
                this.SetFilterInCtrl();
                if ((this.HT_FilterInfo.Count == 0) && flag) {
                    this.FilterData();
                }
            }
        }

        private void LocationNode(StepInfo stepinfo) {
            stepinfo.Location(this.resNodeTree.ResNodeTree);
        }

        private void lvw_DoubleClick(object sender, EventArgs e) {
            if ((this.lvw.SelectedItems.Count != 0) && (this.b_refType && (ClientData.Instance.D_CallRefResOB != null))) {
                DataRowView tag = (DataRowView)this.lvw.SelectedItems[0].Tag;
                string str = "PLM_MASTER_OID";
                ClientData.Instance.D_CallRefResOB(new Guid((byte[])tag[str]));
            }
        }

        private void lvw_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        private void lvw_ItemDrag(object sender, ItemDragEventArgs e) {
            if (this.lvw.SelectedItems != null) {
                DECopyData data = new DECopyData();
                CLCopyData data2 = new CLCopyData();
                data.ClassName = this.resName;
                data2.ResClassName = this.resName;
                foreach (ListViewItem item in this.lvw.SelectedItems) {
                    DataRowView tag = (DataRowView)item.Tag;
                    try {
                        if (this.b_refType) {
                            DEBusinessItem item2 = PLItem.Agent.GetBizItemByMaster(new Guid((byte[])tag[0]), 0, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                            if (item2 != null) {
                                item2.ClientData = tag;
                                data2.Add(item2);
                            }
                        } else {
                            data.ItemList.Add(tag);
                        }
                    } catch (PLMException exception) {
                        PrintException.Print(exception);
                        return;
                    } catch (Exception exception2) {
                        MessageBoxPLM.Show("拖动资源数据出错：" + exception2.Message, "工程资源");
                        return;
                    }
                }
                if (data.ItemList.Count > 0) {
                    data2.Add(data);
                }
                this.lvw.DoDragDrop(data2, DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void lvw_MouseDown(object sender, MouseEventArgs e) {
            if ((this.lvw.SelectedItems != null) && (this.lvw.Items.Count != 0)) {
                try {
                    this.int_SelectedItemCol = this.GetMouseIndex(e.X);
                } catch {
                }
            }
        }

        private void lvw_MouseUp(object sender, MouseEventArgs e) {
            if ((e.Button == MouseButtons.Right) && (this.lvw.SelectedItems.Count != 0)) {
                ListViewItem itemAt = this.lvw.GetItemAt(e.X, e.Y);
                if (itemAt != null) {
                    itemAt.Selected = true;
                }
                Point pos = new Point(e.X, e.Y);
                try {
                    this.i_SelectedItemCol = this.GetMouseIndex(e.X);
                    if (this.b_refType) {
                        this.cmiClone.Visible = true;
                    } else {
                        this.cmiClone.Visible = false;
                    }
                    if (this.lvw.SelectedItems.Count == 1) {
                        this.cmiClone.Enabled = true;
                    } else {
                        this.cmiClone.Enabled = false;
                    }
                    if (this.B_CanDisplayFile) {
                        this.cmiViewFile.Enabled = true;
                    } else {
                        this.cmiViewFile.Enabled = false;
                    }
                    this.cmiB.Enabled = true;
                    if (((itemAt != null) && (this.i_SelectedItemCol >= 0)) && (this.i_SelectedItemCol < itemAt.SubItems.Count)) {
                        if (itemAt.SubItems[this.i_SelectedItemCol].Text.Trim() != "") {
                            string text = itemAt.SubItems[this.i_SelectedItemCol].Text;
                            if (text.Length > 10) {
                                text = text.Substring(0, 10) + "...";
                            }
                            this.cmiB.Text = "复制取值“" + text + "”";
                        } else {
                            this.cmiB.Text = "复制取值";
                            this.cmiB.Enabled = false;
                        }
                    } else {
                        this.cmiB.Text = "复制取值";
                    }
                    this.cmuDgd.Show(this.lvw, pos);
                } catch (Exception exception) {
                    MessageBoxPLM.Show(exception.Message);
                }
            }
        }

        protected void OnDisplayStyleChanged(object sender, ThumToolsEventArgs e) {
            this.SetImageList();
            this.FillItemImage();
        }

        protected void OnScrollChanged(object sender, bool vscroll) {
            try {
                this.FillItemImage();
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        public void ReleaseDataSet() {
            if (this.theDataSet != null) {
                this.theDataSet.Dispose();
                this.theDataSet = null;
                GC.Collect();
            }
        }

        private void resNodeTree_ResNodeSelected(DEResFolder resFolder) {
            if (resFolder == null) {
                this.gbFilter.Visible = false;
                this.splitContainer.Panel1Collapsed = true;
            } else if (resFolder.NodeType != 0) {
                DateTime now = DateTime.Now;
                this.emResType = emResourceType.Customize;
                this.b_first = true;
                this.b_refType = false;
                this.int_rowCount = 0;
                this.InitAllOcxState();
                this.str_CurClsLbl = "";
                if ((this.resNodeTree.SelectedNode.Parent != null) && (this.resNodeTree.SelectedNode.Text != this.clsName)) {
                    this.myResFolder = (DEResFolder)this.resNodeTree.SelectedNode.Tag;
                    StepInfo info = new StepInfo {
                        ResFolder = this.myResFolder
                    };
                    ArrayList list = new ArrayList {
                        this.resNodeTree.SelectedNode.Text
                    };
                    this.GetPathInfo(this.resNodeTree.SelectedNode, list);
                    info.PathInfo = list;
                    this.SetHistoryInfo(info);
                    this.curFolder.Oid = this.myResFolder.Oid;
                    this.curFolder.Name = this.myResFolder.Name;
                    this.curFolder.ClassOid = this.myResFolder.ClassOid;
                    this.curFolder.ClassName = this.myResFolder.ClassName;
                    this.curFolder.Filter = this.myResFolder.Filter;
                    this.curFolder.FilterString = this.myResFolder.FilterString;
                    this.curFolder.FilterValue = this.myResFolder.FilterValue;
                    this.curFolder.NodeType = this.myResFolder.NodeType;
                    this.curFolder.CreateTime = this.myResFolder.CreateTime;
                    this.curFolder.Description = this.myResFolder.Description;
                    this.curFolder.DefineXML = this.myResFolder.DefineXML;
                    if (this.myResFolder != null) {
                        this.SetViewFileState(this.myResFolder);
                        this.str_CurClsLbl = ModelContext.MetaModel.GetClassLabel(this.myResFolder.ClassName);
                        if (this.myResFolder.NodeType == 1) {
                            this.gbFilter.Visible = false;
                            this.splitContainer.Panel1Collapsed = true;
                            this.clsName = this.curFolder.ClassName;
                            this.resName = this.curFolder.ClassName;
                            this.dropDownPicker.Text = this.resNodeTree.SelectedNode.Text;
                            this.InitResStatus();
                            ArrayList showAttrList = ResFunc.GetShowAttrList(this.curFolder, emTreeType.NodeTree);
                            this.attrList = ResFunc.CloneMetaAttrLst(showAttrList);
                            this.attrSort = ResFunc.GetSortAttrList(this.curFolder);
                            this.attrOuter = ResFunc.GetOuterAttr(this.curFolder);
                            this.InitSortList(true);
                            this.InitShowAttrLst();
                            this.SetAttrDataType();
                            this.myView = null;
                            this.Cursor = Cursors.WaitCursor;
                            try {
                                DateTime time2 = DateTime.Now;
                                this.InitData();
                                TestReport.TestProgram(Guid.Empty, "resNodeTree_AfterSelect", "InitData", time2);
                            } catch (PLMException exception) {
                                PrintException.Print(exception);
                            } catch (Exception exception2) {
                                MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                            DateTime time3 = DateTime.Now;
                            this.BingdingData();
                            this.Cursor = Cursors.Default;
                            TestReport.TestProgram(Guid.Empty, "resNodeTree_AfterSelect", "BingdingData", time3);
                            TestReport.TestProgram(Guid.Empty, "resNodeTree_AfterSelect", "NodeSelectedEvent", now);
                        } else {
                            this.gbFilter.Visible = false;
                            this.splitContainer.Panel1Collapsed = true;
                        }
                    }
                }
            }
        }

        private void ResViewer_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                this.FilterData();
            }
        }

        private void SaveHistoryInfo(StepInfo In_stepinfo) {
            PLSystemParam param = new PLSystemParam();
            string paramName = "ResStep-" + ClientData.LogonUser.LogId;
            if ((In_stepinfo.PathInfo != null) && (In_stepinfo.PathInfo.Count > 0)) {
                string pathText = In_stepinfo.GetPathText();
                if ((pathText != null) && (pathText.Length > 0)) {
                    param.SetSystemParameter(paramName, pathText);
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

        private void SetDpDnMPageState(string str_PageRecNum) {
            switch (str_PageRecNum) {
                case "全部":
                    this.tbnShowMPage.Checked = true;
                    this.tbnShowMPage.Enabled = true;
                    this.tbnShowMPage.ToolTipText = "分页显示";
                    this.SetTurnPageUnState();
                    this.SetUnMpageState();
                    this.showNum = 50;
                    return;

                case "50":
                    this.tbnShowMPage.Checked = false;
                    this.tbnShowMPage.Enabled = true;
                    this.tbnShowMPage.ToolTipText = "全部显示";
                    this.showNum = 50;
                    this.SetTurnPageStart();
                    return;

                case "100":
                    this.tbnShowMPage.Checked = false;
                    this.tbnShowMPage.Enabled = true;
                    this.tbnShowMPage.ToolTipText = "全部显示";
                    this.SetTurnPageStart();
                    this.showNum = 100;
                    return;

                case "200":
                    this.tbnShowMPage.Checked = false;
                    this.tbnShowMPage.Enabled = true;
                    this.tbnShowMPage.ToolTipText = "全部显示";
                    this.showNum = 200;
                    this.SetTurnPageStart();
                    return;
            }
            this.tbnShowMPage.Checked = true;
            this.tbnShowMPage.Enabled = true;
            this.tbnShowMPage.ToolTipText = "分页显示";
            this.SetTurnPageUnState();
            this.SetUnMpageState();
            this.showNum = 50;
        }

        private void SetFilterInCtrl() {
            bool flag = false;
            if (this.lblList.Count > 0) {
                foreach (string str in this.HT_FilterInfo.Keys) {
                    for (int i = 0; i < this.lblList.Count; i++) {
                        if ((this.lblList[i] as LabelPLM).Text == str) {
                            if (this.txtBoxList[i] is TextEditPLM) {
                                (this.txtBoxList[i] as TextEditPLM).Text = this.HT_FilterInfo[str] as string;
                            }
                            if (this.txtBoxList[i] is ResComboPLM) {
                                (this.txtBoxList[i] as ResComboPLM).ResValue = this.HT_FilterInfo[str] as string;
                            }
                            if (this.txtBoxList[i] is TimeComboInputPLM) {
                                (this.txtBoxList[i] as TimeComboInputPLM).Text = this.HT_FilterInfo[str] as string;
                            }
                            flag = true;
                        }
                    }
                }
            }
            if (flag) {
                this.FilterData();
            }
        }

        private void SetHistoryInfo(StepInfo In_stepinfo) {
            StepInfo info;
            this.SaveHistoryInfo(In_stepinfo);
            this.btn_upStep.ToolTipText = this.btn_downStep.ToolTipText = "";
            if (!this.b_historySate) {
                this.HT_StepInfo.Add(this.HT_StepInfo.Count + 1, In_stepinfo);
                this.I_StepNum = this.HT_StepInfo.Count;
                if (this.I_StepNum == 1) {
                    this.btn_downStep.Enabled = false;
                    this.btn_upStep.Enabled = false;
                } else {
                    this.btn_downStep.Enabled = false;
                    this.btn_upStep.Enabled = true;
                    info = this.HT_StepInfo[this.I_StepNum - 1] as StepInfo;
                    this.btn_upStep.ToolTipText = "后退到“" + info.GetNodeText() + "”";
                }
            } else {
                this.b_historySate = false;
                if (this.I_StepNum == 1) {
                    info = this.HT_StepInfo[2] as StepInfo;
                    this.btn_downStep.ToolTipText = "前进到“" + info.GetNodeText() + "”";
                } else if (this.I_StepNum == this.HT_StepInfo.Count) {
                    info = this.HT_StepInfo[this.I_StepNum - 1] as StepInfo;
                    this.btn_upStep.ToolTipText = "后退到“" + info.GetNodeText() + "”";
                } else {
                    info = this.HT_StepInfo[this.I_StepNum - 1] as StepInfo;
                    this.btn_upStep.ToolTipText = "后退到“" + info.GetNodeText() + "”";
                    this.btn_downStep.ToolTipText = "前进到“" + (this.HT_StepInfo[this.I_StepNum + 1] as StepInfo).GetNodeText() + "”";
                }
            }
        }

        private void SetImageList() {
            if (this.curFolder != null) {
                if (ResFunc.IsRefRes(this.curFolder.ClassOid)) {
                    this.lvw.SetToolBarVisible(true);
                    if (this.lvw.ThumSetting.DisplayStyle != DisplayStyle.Detail) {
                        this.lvw.SmallImageList = this.lvw.ThumImageList;
                        if (this.lvw.SmallImageList != null) {
                            this.lvw.SmallImageList.ImageSize = new Size(PLSystemParam.ParamThumWdith, PLSystemParam.ParamThumHeight);
                            this.lvw.ThumImageList.Images.Clear();
                        }
                        if (this.lvw.Columns.Count > 0) {
                            this.lvw.Columns[0].Width = 150 + PLSystemParam.ParamThumWdith;
                        }
                    } else {
                        if (this.lvw.SmallImageList != null) {
                            this.lvw.SmallImageList.ImageSize = new Size(1, 1);
                        }
                        this.lvw.SmallImageList = null;
                        this.lvw.View = View.SmallIcon;
                        this.lvw.View = View.Details;
                        if (this.lvw.Columns.Count > 0) {
                            this.lvw.Columns[0].Width = 150;
                        }
                    }
                } else {
                    this.lvw.SetToolBarVisible(false);
                    if (this.lvw.SmallImageList != null) {
                        this.lvw.SmallImageList.ImageSize = new Size(1, 1);
                    }
                    this.lvw.SmallImageList = null;
                    this.lvw.View = View.SmallIcon;
                    this.lvw.View = View.Details;
                    if (this.lvw.Columns.Count > 0) {
                        this.lvw.Columns[0].Width = 150;
                    }
                }
            }
        }

        private void SetLiveMpageState(int i_ShowPageIndex) {
            this.tbnShowMPage.Checked = false;
            this.tbnShowMPage.Enabled = true;
            this.tbnShowMPage.ToolTipText = "全部显示";
            this.cB_pageSize.SelectedIndex = i_ShowPageIndex;
            this.cB_pageSize.Enabled = true;
        }

        public void SetParameter(string nodeName, Hashtable ht_param) {
            this.curStepInfo = "";
            this.HT_FilterInfo.Clear();
            if (!string.IsNullOrEmpty(nodeName)) {
                this.curStepInfo = "工程资源\a" + nodeName + "\a";
            }
            if ((ht_param != null) && (ht_param.Count > 0)) {
                this.HT_FilterInfo = ht_param;
            }
        }

        private void SetStatusInfo(int i_curPage, int i_TotalPage, int i_sumRecNum) {
            string str = "";
            str = i_curPage.ToString() + "/" + i_TotalPage.ToString() + "页, 共" + i_sumRecNum.ToString() + "条记录";
            this.gbData.Text = "资源数据: " + str;
        }

        private void SetTurnPageEnd() {
            this.tbnPre.Enabled = true;
            this.tbnNext.Enabled = false;
        }

        private void SetTurnPageMiddle() {
            this.tbnPre.Enabled = true;
            this.tbnNext.Enabled = true;
        }

        private void SetTurnPageStart() {
            this.tbnPre.Enabled = false;
            this.tbnNext.Enabled = true;
        }

        private void SetTurnPageUnState() {
            this.tbnPre.Enabled = false;
            this.tbnNext.Enabled = false;
        }

        private void SetUnMpageState() {
            this.tbnShowMPage.Checked = true;
            this.tbnShowMPage.Enabled = true;
            this.tbnShowMPage.ToolTipText = "分页显示";
            this.cB_pageSize.SelectedIndex = 0;
            this.cB_pageSize.Enabled = false;
        }

        private void SetViewFileState(DEResFolder theFolder) {
            this.B_CanDisplayFile = false;
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(theFolder.ClassOid);
            if (class2.HasFile) {
                if (class2.IsTableResClass) {
                    this.btn_viewFile.Enabled = true;
                    this.btn_Float.Enabled = true;
                    this.B_CanDisplayFile = true;
                    return;
                }
                if (!string.IsNullOrEmpty(theFolder.DefineXML)) {
                    this.btn_viewFile.Enabled = true;
                    this.btn_Float.Enabled = true;
                    this.B_CanDisplayFile = true;
                    return;
                }
            }
            this.btn_viewFile.Enabled = false;
            this.btn_Float.Enabled = false;
        }

        private void ShowNumDataView(DataView dv, int i_start, int i_end) {
            DataTable table = new DataTable();
            this.lvw.Items.Clear();
            if (dv.Table.Rows.Count == 0) {
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
                if (dv.Table.Rows.Count != 0) {
                    int num = 0;
                    foreach (DataRowView view in dv) {
                        int num2 = 0;
                        string columnName = "";
                        table.NewRow();
                        if (num == i_end) {
                            return;
                        }
                        ListViewItem item = new ListViewItem();
                        for (int i = 0; i < dv.Table.Columns.Count; i++) {
                            foreach (DEMetaAttribute attribute2 in this.attrList) {
                                if (this.ISDefAttrViewable(attribute2) && (dv.Table.Columns[i].ColumnName == ("PLM_" + attribute2.Name))) {
                                    columnName = "PLM_" + attribute2.Name;
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
                                            string str8 = Convert.ToString(view[columnName]);
                                            if (attribute2.DataType2 == PLMDataType.DateTime) {
                                                if ((view[columnName] != null) && !view.Row.IsNull(columnName)) {
                                                    DEMetaAttribute exAttributeByOid = ModelContext.GetExAttributeByOid(this.clsName, attribute2.Oid);
                                                    string format = "yyyy年MM月dd日 HH:mm:ss";
                                                    if (((exAttributeByOid != null) && (exAttributeByOid.GetEditorSetup().format != null)) && (exAttributeByOid.GetEditorSetup().format.Length > 0)) {
                                                        format = exAttributeByOid.GetEditorSetup().format.Replace("Y", "y").Replace("D", "d").Replace("S", "s");
                                                    }
                                                    str8 = Convert.ToDateTime(view[columnName]).ToString(format);
                                                } else {
                                                    str8 = "";
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
                    this.b_first = false;
                    this.numViewData++;
                    this.FillItemImage();
                }
            }
        }

        private void tb_ButtonClick(string str_btnName) {
            this.b_first = false;
            if (str_btnName == "tbnPre") {
                this.pageNum--;
                if (this.pageNum < 1) {
                    MessageBoxPLM.Show("PageNum < 1 ", "工程资源", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                } else {
                    this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), this.showNum + (this.showNum * (this.pageNum - 1)));
                    this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                    new DECopyData();
                    new CLCopyData();
                    if (this.pageNum == 1) {
                        this.tbnPre.Enabled = false;
                    }
                    this.tbnNext.Enabled = true;
                }
            } else if (str_btnName == "tbnNext") {
                this.pageNum++;
                int num = 0;
                num = this.int_rowCount;
                if (num <= (this.showNum * this.pageNum)) {
                    this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), num);
                    this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                    new DECopyData();
                    new CLCopyData();
                    this.SetTurnPageEnd();
                } else {
                    this.ShowNumDataView(this.orgView, this.showNum * (this.pageNum - 1), this.showNum + (this.showNum * (this.pageNum - 1)));
                    this.SetStatusInfo(this.pageNum, this.TotalPageNum, this.int_rowCount);
                    this.SetTurnPageMiddle();
                }
            } else if (str_btnName == "tbnClear") {
                if (this.orgView != null) {
                    this.ClearConditon();
                }
            } else if (str_btnName == "tbnRefresh") {
                if ((this.myView != null) && (this.clsName != "")) {
                    try {
                        this.InitData();
                    } catch (Exception exception) {
                        MessageBoxPLM.Show("读取数据集发生错误" + exception.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    if (this.theDataSet != null) {
                        if (this.theDataSet.Tables.Count == 0) {
                            this.SetTurnPageUnState();
                            this.SetUnMpageState();
                        } else {
                            if (this.theDataSet.Tables[0].Rows.Count == 0) {
                                this.SetTurnPageUnState();
                                this.SetUnMpageState();
                            }
                            this.orgView = this.theDataSet.Tables[0].DefaultView;
                            if (((this.emResType == emResourceType.PLM) && (this.myResFolder != null)) && ((this.myResFolder.Filter != null) && (this.myResFolder.Filter != ""))) {
                                this.orgView.RowFilter = this.myResFolder.Filter;
                            }
                            this.showNum = 50;
                            this.FirstDisplayData(this.orgView);
                        }
                    }
                }
            } else if (str_btnName == "tbnShowMPage") {
                if (this.tbnShowMPage.Checked) {
                    if (this.int_rowCount > (this.AlertPageNum * 50)) {
                        this.tbnShowMPage.Checked = false;
                        MessageBoxPLM.Show("总记录数超过" + ((this.AlertPageNum * 50)).ToString() + "条时，不能全部显示！", "普通提示");
                    } else {
                        this.isAllBtnClick = true;
                        this.SetUnMpageState();
                        this.ShowNumDataView(this.orgView, 0, this.int_rowCount);
                        this.SetStatusInfo(1, 1, this.int_rowCount);
                        this.isAllBtnClick = false;
                    }
                } else {
                    this.FirstDisplayData(this.orgView);
                }
            } else if (str_btnName == "tbnSort") {
                FrmSortDef def;
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
                    } catch (PLMException exception2) {
                        PrintException.Print(exception2);
                    } catch (Exception exception3) {
                        MessageBoxPLM.Show("读取数据集发生错误" + exception3.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    this.BingdingData();
                }
            }
        }

        private void tbnClear_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnClear");
        }

        private void tbnNext_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnNext");
        }

        private void tBnOutput_Click(object sender, EventArgs e) {
            if (PLGrantPerm.CanDoClassOperation(ClientData.LogonUser.Oid, this.curFolder.ClassName, Guid.Empty, "ClaRel_EXPORT") == 1) {
                new ResDataToExcel(this.showNum).Output(this.curFolder);
            } else {
                MessageBoxPLM.Show("您没有导出该类资源数据的权限，请联系系统管理员。", "资源数据导出", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tbnPre_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnPre");
        }

        private void tbnRefresh_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnRefresh");
        }

        private void tbnShowMPage_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnShowMPage");
        }

        private void tbnSort_Click(object sender, EventArgs e) {
            this.tb_ButtonClick("tbnSort");
        }

        private void txtValue_KeyUp(object sender, KeyEventArgs e) {
            DEMetaAttribute tag = (DEMetaAttribute)((TextEditPLM)sender).Tag;
            if ((((tag.DataType == 0) || (tag.DataType == 1)) || ((tag.DataType == 2) || (tag.DataType == 6))) && !this.IsValidKey(((TextEditPLM)sender).Text)) {
                ((TextEditPLM)sender).Text = "";
            }
            if (e.KeyValue == 13) {
                this.FilterData();
            }
        }

        private void ucPrin_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r') {
                this.FilterData();
            }
        }

        private void UCResTree_Load(object sender, EventArgs e) {
            this.curFolder = new DEResFolder();
            this.myResFolder = new DEResFolder();
            this.InitAllOcxState();
            this.emResType = emResourceType.Customize;
            CAPPResHandler = new SelectCAPPResHandler(this.ucUser_ResSelected);
            this.SelectCAPPResChanged += CAPPResHandler;
            DateTime now = DateTime.Now;
            if (!string.IsNullOrEmpty(this.curStepInfo)) {
                this.LoadHistoryInfoByName(this.curStepInfo);
            } else {
                this.LoadHistoryInfo();
            }
            TestReport.TestProgram(Guid.Empty, "UCTree", "LocalNode", now);
            if (this.HT_FilterInfo.Count > 0) {
                this.SetFilterInCtrl();
            }
        }

        private void ucUser_ResSelected(Guid g_res) {
            bool flag = false;
            if (g_res != Guid.Empty) {
                this.InitCAPPData(g_res);
            }
            if (flag && (this.SelectCAPPResChanged != null)) {
                this.SelectCAPPResChanged(g_res);
            }
        }

        private void UpdateFlag(string state) {
            if (this.lvw.SelectedItems != null) {
                try {
                    ListViewItem item3;
                    string str2 = state;
                    if (str2 != null) {
                        if (str2 == "RecordCopy") {
                            DECopyData data = new DECopyData();
                            CLCopyData data2 = new CLCopyData();
                            data.ClassName = this.resName;
                            data2.ResClassName = this.resName;
                            foreach (ListViewItem item in this.lvw.SelectedItems) {
                                DataRowView tag = (DataRowView)item.Tag;
                                try {
                                    if (this.b_refType) {
                                        DEBusinessItem item2 = PLItem.Agent.GetBizItemByMaster(new Guid((byte[])tag[0]), 0, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                                        if (item2 != null) {
                                            item2.ClientData = tag;
                                            data2.Add(item2);
                                        }
                                    } else {
                                        data.ItemList.Add(tag);
                                    }
                                } catch (PLMException exception) {
                                    PrintException.Print(exception);
                                    return;
                                }
                            }
                            if (data.ItemList.Count > 0) {
                                data2.Add(data);
                            }
                        } else if (str2 == "AttrValCopy") {
                            goto Label_013E;
                        }
                    }
                    return;
                Label_013E:
                    item3 = new ListViewItem();
                    item3 = this.lvw.SelectedItems[0];
                    Clipboard.SetDataObject(item3.SubItems[this.i_SelectedItemCol].Text.ToString());
                } catch (Exception exception2) {
                    PrintException.Print(exception2, "工程资源管理");
                }
            }
        }

        private void ViewResFile() {
            if (this.lvw.SelectedItems.Count != 0) {
                Hashtable viewFile = this.GetViewFile(this.curFolder);
                if (ClientData.Instance.ResViewFileHander != null) {
                    ClientData.Instance.ResViewFileHander(this.curFolder, viewFile, this.btn_Float.Checked);
                }
            }
        }

        private class StepInfo {
            public ArrayList PathInfo;
            public DEResFolder ResFolder;

            public void ConvertToPathInfo(string str_in) {
                string str = "\a";
                this.PathInfo = this.SetAttrObject(str_in, str);
            }

            public string GetNodeText() {
                string str = "";
                if (this.PathInfo.Count > 0) {
                    foreach (string str2 in this.PathInfo) {
                        str = str2;
                    }
                }
                return str;
            }

            public string GetPathText() {
                string str = "";
                string str2 = "\a";
                if (this.PathInfo.Count > 0) {
                    foreach (string str3 in this.PathInfo) {
                        str = str + str3 + str2;
                    }
                }
                return str;
            }

            public bool isLocationNode(TreeView treeview) {
                if ((this.PathInfo == null) || (this.PathInfo.Count == 0)) {
                    return false;
                }
                TreeNode theNode = treeview.Nodes[0];
                int num = 0;
                foreach (string str in this.PathInfo) {
                    if (num == 0) {
                        if (theNode.Text != str) {
                            return false;
                        }
                    } else {
                        theNode = this.LocTreeNode(theNode, str);
                        if (theNode == null) {
                            return false;
                        }
                    }
                    num++;
                }
                return (((theNode != null) && (treeview.SelectedNode != null)) && (treeview.SelectedNode.Text == theNode.Text));
            }

            public void Location(TreeView treeview) {
                if ((this.PathInfo != null) && (this.PathInfo.Count != 0)) {
                    TreeNode theNode = treeview.Nodes[0];
                    int num = 0;
                    foreach (string str in this.PathInfo) {
                        if (num == 0) {
                            if (theNode.Text != str) {
                                return;
                            }
                        } else {
                            theNode = this.LocTreeNode(theNode, str);
                            if (theNode == null) {
                                return;
                            }
                        }
                        num++;
                    }
                    if (theNode != null) {
                        treeview.SelectedNode = theNode;
                    }
                }
            }

            private TreeNode LocTreeNode(TreeNode theNode, string str_childNodeText) {
                TreeNode node = null;
                if (theNode.Nodes.Count > 0) {
                    foreach (TreeNode node2 in theNode.Nodes) {
                        if (node2.Text == str_childNodeText) {
                            node = node2;
                        }
                    }
                }
                return node;
            }

            private ArrayList SetAttrObject(string str_in, string str_delim) {
                ArrayList list = new ArrayList();
                string str = str_delim;
                string str2 = str_in;
                char[] separator = str.ToCharArray();
                foreach (string str3 in str2.Split(separator)) {
                    string strTemp = str.Trim();
                    if (strTemp.Length > 0) {
                        list.Add(strTemp);
                    }
                }
                return list;
            }
        }
    }
}

