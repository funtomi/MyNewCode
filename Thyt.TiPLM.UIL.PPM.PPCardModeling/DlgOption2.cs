using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.PLL.Utility;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgOption2 : Form {
        private string address;
        private PPCardArea area;
        public string BlurLabel;
        public static PPCardCell ccFavorite;
        public PPCardCell CCInit;
        private int cobIndex;
        public static bool HasKey = false;
        public static bool HasSubKey = false;
        public bool IsPrintTemplate;
        private CLCardTemplate m_tp;
        private bool needClearRel;
        private bool readOnly;
        private RichTextBox rtxScript;
        public const string TAG_BINDING = "Binding";
        public const string TAG_COMMON = "Common";
        public const string TAG_RELATION = "Relation";
        public const string TAG_SCRIPT = "Script";

        public DlgOption2(PPCardCell cc, bool isCAPP, CLCardTemplate tp) {
            this.address = "";
            this.cobIndex = -2;
            this.m_tp = tp;
            this.IsPrintTemplate = !isCAPP;
            this.InitializeComponent();
            this.InitializeImageList();
            this.InitBarcodeTemplete();
            this.readOnly = true;
            this.CCInit = cc;
        }

        public DlgOption2(string script, PPCardArea area, bool readOnly, string address, bool isCAPP, string tmpType, CLCardTemplate tp) {
            this.address = "";
            this.cobIndex = -2;
            this.m_tp = tp;
            this.IsPrintTemplate = !isCAPP;
            this.readOnly = readOnly;
            this.area = area;
            this.InitializeComponent();
            this.InitializeImageList();
            this.InitBarcodeTemplete();
            if (!readOnly) {
                this.CreateTypeTree(this.tvwType);
            }
            this.CCInit = ccFavorite;
            if (this.CCInit == null) {
                this.CCInit = new PPCardCell();
            }
            this.CCInit.Address = address;
            this.address = address;
            this.CCInit.IsKey = false;
            this.CCInit.IsSubKey = false;
            this.CCInit.AttributeName = "";
            if ((script != "") && (script.IndexOf("<") >= 0)) {
                PPCardCompiler.ExplainXml(script, this.CCInit);
                this.area = this.CCInit.Area;
            }
            if ((this.CCInit.ClassName == "") && (this.area == PPCardArea.Head)) {
                this.CCInit.ClassName = tmpType;
            }
            this.tbcData.SelectedTab = this.tabBinding;
            if (this.CCInit.IsKey) {
                this.chkKey.Enabled = true;
                this.chkSubKey.Enabled = false;
            } else if (this.CCInit.IsSubKey) {
                this.chkKey.Enabled = false;
                this.chkSubKey.Enabled = true;
            } else {
                this.chkKey.Enabled = !HasKey;
                this.chkSubKey.Enabled = !HasSubKey;
            }
            if (this.area == PPCardArea.Mid) {
                this.rbtMid.Checked = true;
                this.CCInit.Area = PPCardArea.Mid;
            } else {
                this.chkKey.Checked = false;
                this.chkKey.Enabled = false;
                this.chkSubKey.Checked = false;
                this.chkSubKey.Enabled = false;
                this.rbtMid.Checked = false;
                this.CCInit.Area = PPCardArea.Head;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            PPCardCell cardCell = this.CardCell;
            if ((cardCell.AttributeName == "") || (cardCell.ClassName == "")) {
                MessageBox.Show("有关属性没有定制或者没有正确定制，请检查并更正。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else if ((!this.IsPrintTemplate && (((this.m_tp.HeadClass != ((string)this.txtField.Tag)) && ModelContext.MetaModel.IsCard(this.txtField.Tag as string)) || this.rbtMid.Checked)) && ((cardCell.LeftClassName == "") || (cardCell.LeftRelName == ""))) {
                MessageBox.Show("关联关系没有定制，请检查并更正。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else if (cardCell.IsBarcode && (cardCell.BarcodeTemplate == "")) {
                MessageBox.Show("条码输出没有选择条码模板，请检查并更正。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else if ((!this.IsPrintTemplate && (cardCell.DataType == 9)) && (cardCell.Area != PPCardArea.Head)) {
                MessageBox.Show("只有表头才能绑定网格类型，请检查并更正。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                if (this.pnlGrid.Visible) {
                    if (!CLPPFile.CheckAddress(cardCell.Address)) {
                        MessageBox.Show("网格的起始单元格格式不对。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (!CLPPFile.CheckAddress(cardCell.Address2)) {
                        MessageBox.Show("网格的结束单元格格式不对。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (cardCell.IsSubKey) {
                    if (!HasKey) {
                        MessageBox.Show("您还没有设置关键列，不能设置子关键列。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (cardCell.IsKey) {
                        MessageBox.Show("一列不能既设置成关键列，又设置成子关键列。", "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                this.rtxScript.Text = PPCardCompiler.CreateXML(cardCell);
                ccFavorite = new PPCardCell(cardCell);
                ccFavorite.Script = "";
                if (cardCell.IsKey) {
                    HasKey = true;
                } else if (cardCell.IsSubKey) {
                    HasSubKey = true;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnSubKeyExplain_Click(object sender, EventArgs e) {
            string text = "★ 子关键列：用以确定子对象的作用范围。\n\n★ 这里的子对象指的是表中主对象（由关键列确定）的子对象。如：表中主对象为加工顺序，子对象为工步（关联关系为：加工顺序与工步的关联）。\n\n★ 如果子对象某绑定属性在卡片编辑时需要自动换行（如：工步名称），则可设置子关键列，编辑和检入卡片时，将以子关键列确定子对象的作用范围\n\n★ 如果子对象没有任何绑定属性需要换行，则无需设置子关键列。系统默认子对象的作用范围为一行。";
            MessageBox.Show(text, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void chkBarcode_CheckedChanged(object sender, EventArgs e) {
            this.cmbTemplate.Enabled = this.chkBarcode.Checked;
        }

        private void chkKey_CheckedChanged(object sender, EventArgs e) {
            if (this.chkKey.Checked) {
                this.chkSubKey.Enabled = false;
            } else {
                HasKey = false;
                this.chkSubKey.Enabled = !HasSubKey;
            }
        }

        private void chkReturn_CheckedChanged(object sender, EventArgs e) {
            if (this.IsPrintTemplate) {
                this.numJumpPage.Enabled = this.chkReturn.Checked;
            } else if (this.rbtHead.Checked) {
                this.numReturnRows.Enabled = this.chkReturn.Checked;
            }
        }

        private void chkSubKey_CheckedChanged(object sender, EventArgs e) {
            if (this.chkSubKey.Checked) {
                this.chkKey.Enabled = false;
            } else {
                HasSubKey = false;
                this.chkKey.Enabled = !HasKey;
            }
        }

        private void cobAttrList_SelectionChangeCommitted(object sender, EventArgs e) {
            if (this.cobAttrList.SelectedValue != null) {
                this.cobIndex = this.cobAttrList.SelectedIndex;
                string selectedValue = (string)this.cobAttrList.SelectedValue;
                foreach (GenericAttribute attribute in (ArrayList)this.cobAttrList.DataSource) {
                    if (attribute.Name.Equals(selectedValue)) {
                        this.txtType.Text = attribute.DataTypeLabel;
                        this.txtType.Tag = attribute.DataType;
                        this.DisplayTypePanel(attribute.DataType);
                        break;
                    }
                }
            }
        }

        private void CreateRelationList(SortableListView lv, string rightClassName) {
            int iconIndex = ClientData.MyImageList.GetIconIndex("ICO_DMM_RELATION");
            ArrayList relationsByRightClass = this.GetRelationsByRightClass(rightClassName);
            lv.Items.Clear();
            foreach (DEMetaRelation relation in relationsByRightClass) {
                ListViewItem item = new ListViewItem(relation.Label, iconIndex) {
                    SubItems = { 
                        relation.LeftClassLabel,
                        relation.RightClassLabel
                    },
                    Tag = relation
                };
                lv.Items.Add(item);
            }
        }

        private void CreateTypeTree(TreeView tv) {
            ClientData.MyImageList.GetIconIndex("ICO_DMM_CLASS");
            if (this.IsPrintTemplate) {
                ArrayList namefilter = new ArrayList { 
                    "OBJECTSTATE",
                    "RESOURCE",
                    "PPCLASSIFY",
                    "COLLECTION",
                    "PPCALCULATE",
                    "FORMULA",
                    "VARIABLESOURCE",
                    "CONDITION",
                    "PPSIGNTEMPLATE"
                };
                UIDataModel.FillCustomizedClassTree(tv, 0, 0, namefilter);
                foreach (TreeNode node in tv.Nodes) {
                    if (((DEMetaClass)node.Tag).Name == "FORM") {
                        node.ExpandAll();
                        break;
                    }
                }
            } else {
                ArrayList list2 = new ArrayList { 
                    "OBJECTSTATE",
                    "PRODUCT",
                    "FORM",
                    "RESOURCE",
                    "PPCLASSIFY",
                    "COLLECTION",
                    "PPCALCULATE",
                    "FORMULA",
                    "VARIABLESOURCE",
                    "CONDITION",
                    "PPSIGNTEMPLATE"
                };
                UIDataModel.FillCustomizedClassTree(tv, 0, 0, list2);
                foreach (TreeNode node2 in tv.Nodes) {
                    if (((DEMetaClass)node2.Tag).Name == "PPOBJECT") {
                        node2.ExpandAll();
                        break;
                    }
                }
            }
        }

        private void DisplayAttributes(TreeNode selectedNode) {
            if ((selectedNode != null) && (selectedNode.Tag is DEMetaClass)) {
                this.txtField.Text = selectedNode.Text;
                this.txtField.Tag = ((DEMetaClass)selectedNode.Tag).Name;
                ArrayList attris = new ArrayList(ModelContext.MetaModel.GetAllAttributes(((DEMetaClass)selectedNode.Tag).Name));
                FixedAttribute fixedAttr = new FixedAttribute("OID", "主对象唯一标识", "M", PLMDataType.Guid);
                GenericAttribute attribute2 = GenericAttribute.CreateAttribute(fixedAttr);
                attris.Add(attribute2);
                string tag = this.ultraEditor.Tag as string;
                GenericAttribute[] c = new GenericAttribute[0];
                if (tag != null) {
                    c = ModelContext.MetaModel.GetRelationGenericAttributes(tag);
                    for (int i = 0; i < c.Length; i++) {
                        c[i].Label = "(关联)" + c[i].Label;
                    }
                    attris.AddRange(c);
                }
                this.FilterAttributes(attris, ((DEMetaClass)selectedNode.Tag).Name);
                if ((attris != null) && (attris.Count > 0)) {
                    this.cobAttrList.DataSource = attris;
                    this.cobAttrList.ValueMember = "Name";
                    this.cobAttrList.DisplayMember = "Label";
                }
            }
        }

        private void DisplayTypePanel(PLMDataType attrType) {
            switch (attrType) {
                case PLMDataType.String:
                case PLMDataType.Guid:
                case PLMDataType.Clob:
                case PLMDataType.Card:
                    this.pnlBool.Visible = false;
                    this.pnlGrid.Visible = false;
                    this.pnlString.Visible = true;
                    if ((this.ultraEditor.Tag != null) && ModelContext.MetaModel.IsCard(this.ultraEditor.Tag.ToString())) {
                        this.chkReturn.Checked = true;
                    }
                    if (this.IsPrintTemplate) {
                        this.numReturnRows.Visible = false;
                        return;
                    }
                    this.label11.Text = "换行行数";
                    this.label11.AutoEllipsis = true;
                    this.label11.AutoSize = true;
                    this.numJumpPage.Visible = false;
                    return;

                case PLMDataType.Bool:
                    this.pnlBool.Visible = this.IsPrintTemplate;
                    this.pnlGrid.Visible = false;
                    this.pnlString.Visible = false;
                    return;

                case PLMDataType.Grid:
                    this.txtGridStart.Text = this.address;
                    if (!this.IsPrintTemplate) {
                        this.chkGridReturn.Text = "是否换页";
                    }
                    this.pnlBool.Visible = false;
                    this.pnlGrid.Visible = true;
                    this.pnlString.Visible = false;
                    return;
            }
            this.pnlBool.Visible = false;
            this.pnlGrid.Visible = false;
            this.pnlString.Visible = false;
        }

        private void DlgOption2_Load(object sender, EventArgs e) {
            if (this.cobIndex > -2) {
                this.cobAttrList.SelectedIndex = this.cobIndex;
            }
            if (this.IsPrintTemplate) {
                this.InitBoolComBox();
            }
            this.InitUltraEditor();
            try {
                this.CardCell = this.CCInit;
            } catch (Exception exception) {
                MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                base.Close();
                return;
            }
            this.CCInit = null;
        }

        private void FilterAttributes(ArrayList attris, string className) {
            for (int i = 0; i < attris.Count; i++) {
                GenericAttribute attribute = (GenericAttribute)attris[i];
                if ((attribute.Category == "I") || (attribute.Category == "S")) {
                    DEMetaAttribute attach = attribute.Attach as DEMetaAttribute;
                    if (!attach.IsViewable) {
                        attris.Remove(attribute);
                        i--;
                    }
                }
            }
        }

        private bool FilterClass(System.Type tp) {
            return (tp.GetProperties().Length == 0);
        }

        private ArrayList GetRelationsByRightClass(string rightClassName) {
            return ModelContext.MetaModel.GetAllRelationsByRightClassName(rightClassName);
        }

        private void InitBarcodeTemplete() {
            List<string> configNames = new PLBarCode().GetConfigNames();
            if (configNames != null) {
                for (int i = 0; i < configNames.Count; i++) {
                    this.cmbTemplate.Items.Add(configNames[i]);
                }
            }
        }

        private void InitBoolComBox() {
            this.cobBool.Items.Add("√,空白");
            this.cobBool.Items.Add("V,X");
            this.cobBool.Items.Add("是,否");
            this.cobBool.Items.Add("T,F");
            this.cobBool.Items.Add("True,False");
            this.cobBool.Items.Add("Yes,No");
            this.cobBool.Items.Add("1,0");
            this.cobBool.Items.Add("Y,N");
            if (this.CCInit.DataType == 5) {
                switch (this.CCInit.TrueString) {
                    case "√":
                        this.cobBool.SelectedIndex = 0;
                        return;

                    case "V":
                        this.cobBool.SelectedIndex = 1;
                        return;

                    case "是":
                        this.cobBool.SelectedIndex = 2;
                        return;

                    case "T":
                        this.cobBool.SelectedIndex = 3;
                        return;

                    case "True":
                        this.cobBool.SelectedIndex = 4;
                        return;

                    case "Yes":
                        this.cobBool.SelectedIndex = 5;
                        return;

                    case "1":
                        this.cobBool.SelectedIndex = 6;
                        return;

                    case "Y":
                        this.cobBool.SelectedIndex = 7;
                        return;
                }
                this.cobBool.SelectedIndex = -1;
            }
        }

        private void InitializeImageList() {
            ClientData.MyImageList.AddIcon("ICO_DMM_CLASS");
            ClientData.MyImageList.AddIcon("ICO_DMM_RELATION");
            this.tvwType.ImageList = ClientData.MyImageList.imageList;
        }

        private Control InitRelationList() {
            SortableListView view = new SortableListView {
                SmallImageList = ClientData.MyImageList.imageList
            };
            ColumnHeader header = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();
            view.AllowColumnReorder = true;
            view.BorderStyle = BorderStyle.FixedSingle;
            view.Columns.AddRange(new ColumnHeader[] { header, header2, header3 });
            view.FullRowSelect = true;
            view.HideSelection = false;
            view.MultiSelect = false;
            view.Activation = ItemActivation.TwoClick;
            view.Size = new Size(0x13b, 0x88);
            view.View = View.Details;
            view.ItemActivate += new EventHandler(this.lvwRelations_ItemActivate);
            header.Text = "关联名称";
            header.Width = 0x80;
            header2.Text = "左对象";
            header2.Width = 0x63;
            header3.Text = "右对象";
            header3.Width = 80;
            return view;
        }

        private void InitUltraEditor() {
            if (!this.readOnly) {
                DropDownEditorButton button = new DropDownEditorButton("SelectedRel") {
                    RightAlignDropDown = DefaultableBoolean.False
                };
                this.ultraEditor.ButtonsRight.Add(button);
                if ((this.m_tp.HeadClass != ((string)this.txtField.Tag)) || this.rbtMid.Checked) {
                    DropDownEditorButton button2 = this.ultraEditor.ButtonsRight["SelectedRel"] as DropDownEditorButton;
                    button2.Control = this.InitRelationList();
                }
                this.ultraEditor.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.ultraEditor_BeforeEditorButtonDropDown);
            }
        }

        private void lvwRelations_ItemActivate(object sender, EventArgs e) {
            SortableListView view = sender as SortableListView;
            if ((view.SelectedItems.Count >= 1) && (view.SelectedItems[0].Tag is DEMetaRelation)) {
                this.ultraEditor.Text = ((DEMetaRelation)view.SelectedItems[0].Tag).Label;
                this.ultraEditor.Tag = ((DEMetaRelation)view.SelectedItems[0].Tag).Name;
                bool needClearRel = this.needClearRel;
                this.needClearRel = false;
                this.DisplayAttributes(this.tvwType.SelectedNode);
                this.needClearRel = needClearRel;
                view.Parent.Visible = false;
                this.SmartSelectAttr();
            }
        }

        private void rbtMid_CheckedChanged(object sender, EventArgs e) {
            this.chkKey.Enabled = this.rbtMid.Checked;
            if (!this.rbtMid.Checked) {
                this.chkKey.Checked = false;
            }
        }

        public void RefreshDlgText() {
            string str = this.rbtHead.Checked ? ":表头" : (":表中，" + (this.chkKey.Checked ? "关键列" : "非关键列"));
            int index = this.Text.IndexOf(':');
            if (index >= 0) {
                this.Text = this.Text.Substring(0, index) + str;
            } else {
                this.Text = this.Text + str;
            }
        }

        private bool SetNodeSelected(TreeNode node, string className) {
            if (this.needClearRel) {
                return true;
            }
            DEMetaClass tag = node.Tag as DEMetaClass;
            if ((tag != null) && (tag.Name == className)) {
                this.DisplayAttributes(node);
                this.needClearRel = true;
                return true;
            }
            foreach (TreeNode node2 in node.Nodes) {
                if (this.SetNodeSelected(node2, className)) {
                    return true;
                }
            }
            return false;
        }

        private void SmartSelectAttr() {
            double num = 0.0;
            int num2 = 0;
            if ((this.BlurLabel != null) && (this.BlurLabel != "")) {
                for (int i = 0; i < this.cobAttrList.Items.Count; i++) {
                    GenericAttribute attribute = this.cobAttrList.Items[i] as GenericAttribute;
                    double num4 = PSStart.BlurCompare(attribute.Label, this.BlurLabel);
                    if (num4 > num) {
                        num = num4;
                        num2 = i;
                    }
                }
            }
            if (num == 0.0) {
                this.cobAttrList.SelectedIndex = -1;
                this.cobIndex = -1;
                this.txtType.Text = "";
            } else {
                this.cobIndex = num2;
                this.cobAttrList.SelectedIndex = num2;
                this.txtType.Text = ((GenericAttribute)this.cobAttrList.SelectedItem).DataTypeLabel;
                this.DisplayTypePanel(((GenericAttribute)this.cobAttrList.SelectedItem).DataType);
            }
        }

        private void tbcData_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tbcData.SelectedTab.Tag.ToString() == "Script") {
                this.rtxScript.LoadFile(PPCardCompiler.DisplayXML(true, this.CardCell), RichTextBoxStreamType.UnicodePlainText);
                this.tbcData.SelectedTab = this.tabScript;
            } else if ((this.tbcData.SelectedTab.Tag.ToString() == "Binding") && (this.cobIndex > -2)) {
                this.cobAttrList.SelectedIndex = this.cobIndex;
            }
        }

        private void tvwType_AfterSelect(object sender, TreeViewEventArgs e) {
            if ((e.Node != null) && (e.Node.Tag is DEMetaClass)) {
                if (((this.m_tp.HeadClass != ((DEMetaClass)e.Node.Tag).Name) && !ModelContext.MetaModel.IsCard(((DEMetaClass)e.Node.Tag).Name)) || this.rbtMid.Checked) {
                    this.txtField.Text = e.Node.Text;
                    this.txtField.Tag = ((DEMetaClass)e.Node.Tag).Name;
                    DropDownEditorButton button = this.ultraEditor.ButtonsRight["SelectedRel"] as DropDownEditorButton;
                    button.Control = this.InitRelationList();
                    this.cobAttrList.DataSource = null;
                    this.txtType.Text = "";
                } else {
                    this.DisplayAttributes(e.Node);
                    if (this.ultraEditor.ButtonsRight.Count > 0) {
                        DropDownEditorButton button2 = this.ultraEditor.ButtonsRight["SelectedRel"] as DropDownEditorButton;
                        button2.Control = null;
                    }
                    this.SmartSelectAttr();
                }
                this.cobIndex = this.cobAttrList.SelectedIndex;
                this.ultraEditor.Text = "";
                this.ultraEditor.Tag = null;
            }
        }

        private void ultraEditor_BeforeEditorButtonDropDown(object sender, BeforeEditorButtonDropDownEventArgs e) {
            if ((this.txtField.Tag != null) && (this.txtField.Text != "")) {
                DropDownEditorButton button = this.ultraEditor.ButtonsRight["SelectedRel"] as DropDownEditorButton;
                SortableListView control = button.Control as SortableListView;
                if (!this.readOnly && !ModelContext.MetaModel.IsChild("PPCARD", this.txtField.Tag.ToString())) {
                    this.CreateRelationList(control, (string)this.txtField.Tag);
                }
            }
        }

        public PPCardCell CardCell {
            get {
                PPCardCell cell = new PPCardCell();
                if (this.txtField.Tag != null) {
                    cell.ClassName = (string)this.txtField.Tag;
                }
                if (this.ultraEditor.Tag != null) {
                    cell.LeftRelName = this.ultraEditor.Tag.ToString();
                }
                if (this.cobAttrList.SelectedValue != null) {
                    cell.AttributeName = (string)this.cobAttrList.SelectedValue;
                    cell.AttrClass = ((GenericAttribute)this.cobAttrList.SelectedItem).Category;
                }
                if (this.pnlGrid.Visible || (cell.DataType == 9)) {
                    cell.Address2 = this.txtGridEnd.Text.Trim();
                    cell.allowReturn = this.chkGridReturn.Checked;
                }
                if (this.pnlBool.Visible || (cell.DataType == 5)) {
                    if ((this.cobBool.SelectedItem == null) || (this.cobBool.SelectedItem.ToString() == "")) {
                        cell.TrueString = "√";
                        cell.FalseString = "";
                    } else {
                        string[] strArray = this.cobBool.SelectedItem.ToString().Split(new char[] { ',' });
                        cell.TrueString = strArray[0];
                        cell.FalseString = strArray[1];
                    }
                }
                if ((this.pnlString.Visible || (cell.DataType == 4)) || (((cell.DataType == 11) || (cell.DataType == 12)) || (cell.DataType == 8))) {
                    cell.allowReturn = this.chkReturn.Checked;
                    cell.JumpPageCount = Convert.ToInt32(this.numJumpPage.Value);
                    cell.ReturnRowsCount = Convert.ToInt32(this.numReturnRows.Value);
                    cell.IsBarcode = this.chkBarcode.Checked;
                    cell.BarcodeTemplate = this.cmbTemplate.Text;
                }
                if (this.ultraEditor.Tag != null) {
                    cell.LeftRelName = this.ultraEditor.Tag.ToString();
                    cell.LeftClassName = ModelContext.MetaModel.GetRelation(cell.LeftRelName).LeftClassName;
                }
                cell.IsKey = this.chkKey.Checked;
                cell.IsSubKey = this.chkSubKey.Checked;
                cell.Area = this.CellArea;
                cell.Script = this.rtxScript.Text;
                return cell;
            }
            set {
                PPCardCell cell = value;
                if (cell.LeftRelName != "") {
                    this.ultraEditor.Tag = cell.LeftRelName;
                }
                DEMetaRelation relation = ModelContext.MetaModel.GetRelation(cell.LeftRelName);
                if (relation != null) {
                    this.ultraEditor.Text = relation.Label;
                }
                if (!this.readOnly) {
                    if (cell.ClassName != "") {
                        this.needClearRel = false;
                        foreach (TreeNode node in this.tvwType.Nodes) {
                            if (this.SetNodeSelected(node, cell.ClassName)) {
                                break;
                            }
                        }
                    }
                } else {
                    this.txtField.Tag = cell.ClassName;
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(cell.ClassName);
                    if (class2 != null) {
                        this.txtField.Text = class2.Label;
                    }
                }
                if (this.readOnly) {
                    GenericAttribute relationGenericAttribute = null;
                    if (cell.AttrClass == "S") {
                        relationGenericAttribute = ModelContext.MetaModel.GetRelationGenericAttribute(cell.LeftRelName, cell.AttributeName);
                        relationGenericAttribute.Label = "(关联)" + relationGenericAttribute.Label;
                    } else {
                        if (cell.AttrClass == "") {
                            cell.AttrClass = "I";
                        }
                        relationGenericAttribute = ModelContext.MetaModel.GetAttribute(cell.ClassName, cell.AttributeName, cell.AttrClass);
                    }
                    if (relationGenericAttribute != null) {
                        ArrayList list = new ArrayList {
                            relationGenericAttribute
                        };
                        this.cobAttrList.DataSource = list;
                        this.cobAttrList.ValueMember = "Name";
                        this.cobAttrList.DisplayMember = "Label";
                        this.cobAttrList.SelectedIndex = 0;
                        this.txtType.Text = relationGenericAttribute.DataTypeLabel;
                        switch (relationGenericAttribute.DataType) {
                            case PLMDataType.String:
                            case PLMDataType.Guid:
                            case PLMDataType.Clob:
                            case PLMDataType.Card:
                                this.chkBarcode.Checked = cell.IsBarcode;
                                this.cmbTemplate.Text = cell.BarcodeTemplate;
                                this.chkReturn.Checked = cell.allowReturn;
                                if (!this.IsPrintTemplate) {
                                    decimal num3 = Convert.ToDecimal(cell.ReturnRowsCount);
                                    this.numReturnRows.Value = (num3 >= 1M) ? num3 : 1M;
                                    this.numReturnRows.Enabled = false;
                                    this.numJumpPage.Visible = false;
                                    this.label11.Text = "换行行数";
                                    this.label11.AutoSize = true;
                                    this.label11.AutoEllipsis = true;
                                } else {
                                    this.numJumpPage.Enabled = false;
                                    this.numJumpPage.Value = Convert.ToDecimal(cell.JumpPageCount);
                                    this.numReturnRows.Visible = false;
                                }
                                this.pnlBool.Visible = false;
                                this.pnlGrid.Visible = false;
                                this.pnlString.Visible = true;
                                this.pnlString.Enabled = false;
                                goto Label_070F;

                            case PLMDataType.Bool:
                                if (this.IsPrintTemplate) {
                                    this.cobBool.SelectedValue = cell.TrueString + "," + cell.FalseString;
                                }
                                this.pnlBool.Visible = this.IsPrintTemplate;
                                this.pnlGrid.Visible = false;
                                this.pnlString.Visible = false;
                                this.pnlBool.Enabled = false;
                                goto Label_070F;

                            case PLMDataType.Grid:
                                this.txtGridStart.Text = cell.Address;
                                this.txtGridEnd.Text = cell.Address2;
                                if (!this.IsPrintTemplate) {
                                    this.chkGridReturn.Text = "是否换页";
                                }
                                this.chkGridReturn.Checked = cell.allowReturn;
                                this.pnlBool.Visible = false;
                                this.pnlGrid.Visible = true;
                                this.pnlString.Visible = false;
                                this.pnlGrid.Enabled = false;
                                goto Label_070F;
                        }
                        this.pnlBool.Visible = false;
                        this.pnlGrid.Visible = false;
                        this.pnlString.Visible = false;
                    }
                    goto Label_070F;
                }
                if (cell.AttributeName == "") {
                    if (this.cobIndex < 1) {
                        this.cobAttrList.SelectedIndex = -1;
                    }
                } else {
                    this.cobAttrList.SelectedValue = cell.AttributeName;
                    this.txtType.Text = cell.DataTypeLabel;
                    int dataType = 4;
                    try {
                        dataType = cell.DataType;
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "工艺", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    switch (dataType) {
                        case 4:
                        case 8:
                        case 11:
                        case 12:
                            this.chkBarcode.Checked = cell.IsBarcode;
                            this.cmbTemplate.Text = cell.BarcodeTemplate;
                            this.chkReturn.Checked = cell.allowReturn;
                            if (!this.IsPrintTemplate) {
                                decimal num2 = Convert.ToDecimal(cell.ReturnRowsCount);
                                this.numReturnRows.Value = (num2 >= 1M) ? num2 : 1M;
                                this.numReturnRows.Enabled = this.chkReturn.Checked && this.rbtHead.Checked;
                                this.numJumpPage.Visible = false;
                                this.label11.Text = "换行行数";
                                this.label11.AutoEllipsis = true;
                                this.label11.AutoSize = true;
                                break;
                            }
                            this.numJumpPage.Value = Convert.ToDecimal(cell.JumpPageCount);
                            this.numJumpPage.Enabled = this.chkReturn.Checked;
                            this.numReturnRows.Visible = false;
                            break;

                        case 5:
                            if (this.IsPrintTemplate) {
                                this.cobBool.SelectedValue = cell.TrueString + "," + cell.FalseString;
                            }
                            this.pnlBool.Visible = this.IsPrintTemplate;
                            this.pnlGrid.Visible = false;
                            this.pnlString.Visible = false;
                            goto Label_03C4;

                        case 9:
                            this.txtGridStart.Text = cell.Address;
                            this.txtGridEnd.Text = cell.Address2;
                            this.chkGridReturn.Checked = cell.allowReturn;
                            if (!this.IsPrintTemplate) {
                                this.chkGridReturn.Text = "是否换页";
                            }
                            this.pnlBool.Visible = false;
                            this.pnlGrid.Visible = true;
                            this.pnlString.Visible = false;
                            goto Label_03C4;

                        default:
                            this.pnlBool.Visible = false;
                            this.pnlGrid.Visible = false;
                            this.pnlString.Visible = false;
                            goto Label_03C4;
                    }
                    this.pnlBool.Visible = false;
                    this.pnlGrid.Visible = false;
                    this.pnlString.Visible = true;
                }
            Label_03C4:
                this.cobIndex = this.cobAttrList.SelectedIndex;
            Label_070F:
                this.chkKey.Checked = cell.IsKey;
                this.chkSubKey.Checked = cell.IsSubKey;
                this.CellArea = cell.Area;
                this.rtxScript.Text = cell.Script;
                if (this.readOnly) {
                    this.chkKey.Enabled = false;
                    this.grpArea.Enabled = false;
                    this.tvwType.Visible = false;
                    this.grpBinding.Left -= 0x5f;
                    this.grpArea.Left -= 0x5f;
                    this.cobAttrList.Enabled = false;
                    this.btnOK.Visible = false;
                    this.btnCancel.Text = "确定(&C)";
                    this.tbcData.TabPages.Remove(this.tabScript);
                }
            }
        }

        private PPCardArea CellArea {
            get {
                if (this.rbtHead.Checked) {
                    return PPCardArea.Head;
                }
                if (this.rbtMid.Checked) {
                    return PPCardArea.Mid;
                }
                if (this.rbtTail.Checked) {
                    return PPCardArea.Tail;
                }
                return PPCardArea.Label;
            }
            set {
                if (value == PPCardArea.Head) {
                    this.rbtHead.Checked = true;
                }
                if (value == PPCardArea.Mid) {
                    this.rbtMid.Checked = true;
                }
                if (value == PPCardArea.Tail) {
                    this.rbtTail.Checked = true;
                }
            }
        }
    }
}

