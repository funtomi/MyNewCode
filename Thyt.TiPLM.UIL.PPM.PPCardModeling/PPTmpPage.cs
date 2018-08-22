using AxOWC;
using OWC;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Product.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class PPTmpPage : UserControl {
        public int countColumns;
        public int countRows;
        private string curCell;
        private string filePath;
        private ArrayList lstHistory;
        private ArrayList lstProcess;
        private CLCardTemplate m_tp;
        public string midBegin;
        public string midEnd;
        private bool readOnly;

        public PPTmpPage() {
            this.filePath = "";
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public PPTmpPage(CLCardTemplate tp, int countRows, int countColumns, string midBegin, string minEnd, bool readOnly, string text) {
            this.filePath = "";
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.InitializeData(tp, countRows, countColumns, midBegin, minEnd, readOnly);
            this.TemplateText = text;
            this.InitializeSheet(readOnly);
            this.InitializeProcess();
            this.InitializeHistory();
        }

        public PPTmpPage(CLCardTemplate tp, string path, int countRows, int countColumns, string midBegin, string minEnd, bool readOnly) {
            this.filePath = "";
            this.InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.InitializeData(tp, countRows, countColumns, midBegin, minEnd, readOnly);
            this.TemplateUrl = path;
            this.InitializeSheet(readOnly);
            this.InitializeProcess();
            this.InitializeHistory();
        }

        private void axSpreadsheet1_DblClick(object sender, IWebCalcEventSink_DblClickEvent e) {
            this.m_tp.IsSaved = false;
            this.curCell = this.GetFirstCell(e.eventInfo.Range.Address);
            switch (this.CheckEditStyle(this.curCell)) {
                case PPCellAttach.Option:
                    this.OnShowCellProperty(null, null);
                    return;

                case PPCellAttach.Process:
                    this.OnInsertProcessRecord(null, null);
                    return;

                case PPCellAttach.History:
                    this.OnInsertModifyRecord(null, null);
                    return;

                case PPCellAttach.PageIndex:
                    this.OnInsertPageIndex(null, null);
                    return;

                case PPCellAttach.PagesCount:
                    this.OnInsertPagesCount(null, null);
                    return;

                case PPCellAttach.FormSign:
                case PPCellAttach.Remark:
                case PPCellAttach.Other:
                    this.OnFormSignature(null, null);
                    return;

                case PPCellAttach.Picture:
                    if (PPCConvert.ToPicType(PPCardCompiler.GetXmlAttr(this.GetCellValue(this.curCell), "类型")) != InterPicType.BarCode) {
                        this.OnInsertPicture(null, null);
                        return;
                    }
                    this.OnInsertBarcode(null, null);
                    return;

                case PPCellAttach.Function:
                    this.OnFunction(null, null);
                    return;

                case PPCellAttach.Unknow:
                    this.BuildContextMenu().Show(this, new Point(e.eventInfo.X, e.eventInfo.Y));
                    return;
            }
        }

        private void axSpreadsheet1_StartEdit(object sender, IWebCalcEventSink_StartEditEvent e) {
            this.m_tp.IsSaved = false;
            this.curCell = this.GetFirstCell(e.eventInfo.Range.Address);
            PPCellAttach none = PPCellAttach.None;
            none = this.CheckEditStyle(this.curCell);
            if (((none != PPCellAttach.Label) && (none != PPCellAttach.FormSign)) && (none != PPCellAttach.Remark)) {
                e.eventInfo.ReturnValue = false;
            }
        }

        private ContextMenu BuildContextMenu() {
            ContextMenu menu = new ContextMenu();
            MenuItemEx item = null;
            item = new MenuItemEx("Property", "单元格属性", null, null) {
                Icon = PLMImageList.GetIcon("ICO_PPM_PPCARDCELLPROPERTY").ToBitmap()
            };
            item.Click += new EventHandler(this.OnShowCellProperty);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Input", "输入文本", null, null);
            item.Click += new EventHandler(this.OnInputText);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("-", "-", null, null);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Insert page index", "插入页码", null, null);
            item.Click += new EventHandler(this.OnInsertPageIndex);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Insert count of pages", "插入总页数", null, null);
            item.Click += new EventHandler(this.OnInsertPagesCount);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Insert page index", "插入版本号", null, null);
            item.Click += new EventHandler(this.OnInsertCurrentVersion);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Insert page index", "插入当时日期", null, null);
            item.Click += new EventHandler(this.OnInsertCurrentTime);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("Insert count of pages", "插入当时用户签名", null, null);
            item.Click += new EventHandler(this.OnInsertLogonUser);
            menu.MenuItems.Add(item);
            item = new MenuItemEx("-", "-", null, null);
            menu.MenuItems.Add(item);
            if (!ModelContext.MetaModel.IsForm(this.m_tp.TemplateType)) {
                item = new MenuItemEx("Insert modify records", "插入修改记录", null, null) {
                    Icon = PLMImageList.GetIcon("ICO_PSM_REVISIONHISTORY").ToBitmap()
                };
                item.Click += new EventHandler(this.OnInsertModifyRecord);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("Insert process record", "插入流程记录", null, null) {
                    Icon = PLMImageList.GetIcon("ICO_BPM_DEFROOT").ToBitmap()
                };
                item.Click += new EventHandler(this.OnInsertProcessRecord);
                menu.MenuItems.Add(item);
            } else {
                item = new MenuItemEx("Insert form signature", "插入表单签字", null, null);
                item.Click += new EventHandler(this.OnFormSignature);
                menu.MenuItems.Add(item);
            }
            item = new MenuItemEx("Insert picture", "插入图片", null, null);
            item.Click += new EventHandler(this.OnInsertPicture);
            menu.MenuItems.Add(item);
            if (base.Parent.Text != ConstCAPP.COVERLABEL) {
                MenuItemEx ex2 = new MenuItemEx("Function", "预定义公式", null, null);
                MenuItemEx ex3 = new MenuItemEx("SinglePageSum", "单页列小计", null, null);
                ex3.Click += new EventHandler(this.OnFunction);
                ex2.MenuItems.Add(ex3);
                ex3 = new MenuItemEx("AllPageSum", "全部页列总计", null, null);
                ex3.Click += new EventHandler(this.OnFunction);
                ex2.MenuItems.Add(ex3);
                menu.MenuItems.Add(ex2);
            }
            return menu;
        }

        private PPCellAttach CheckEditStyle(string cell) {
            string cellValue = this.GetCellValue(cell);
            int num = PPCConvert.Address2Row(cell);
            if (CLCard.InRange(this.midBegin, this.midEnd, this.curCell)) {
                if (num == PPCConvert.Address2Row(this.midBegin)) {
                    return PPCellAttach.Option;
                }
                return PPCellAttach.None;
            }
            if ((cellValue != "") && (cellValue[0] != '<')) {
                if (this.readOnly) {
                    return PPCellAttach.None;
                }
                return PPCellAttach.Label;
            }
            if (cellValue == string.Empty) {
                if (this.readOnly) {
                    return PPCellAttach.None;
                }
                return PPCellAttach.Unknow;
            }
            if (cellValue[0] == '<') {
                return PPCardCompiler.GetXmlEditStyle(cellValue);
            }
            return PPCellAttach.None;
        }

        private void CompareSameRange(PPTmpPage comparedPage, int rows, int cols) {
            string cellValue = null;
            string str2 = null;
            for (int i = 1; i <= rows; i++) {
                for (int j = 1; j <= cols; j++) {
                    cellValue = this.GetCellValue(i, j);
                    str2 = comparedPage.GetCellValue(i, j);
                    if (cellValue != str2) {
                        if (((cellValue == "") && (str2 != "")) || ((cellValue != "") && (str2 == ""))) {
                            this.SetFontColor(this.GetRange(i, j), "Red");
                            comparedPage.SetFontColor(comparedPage.GetRange(i, j), "Red");
                        } else {
                            this.SetFontColor(this.GetRange(i, j), "Blue");
                            comparedPage.SetFontColor(comparedPage.GetRange(i, j), "Blue");
                        }
                    }
                }
            }
        }

        public void Export(string exportFileName, bool inExcel) {
            if (Path.GetExtension(exportFileName).ToLower() == ".xlsx") {
                string tempFilePath = this.GetTempFilePath(base.Parent.Text);
                StreamWriter writer = null;
                try {
                    writer = new StreamWriter(tempFilePath, false, Encoding.Unicode, 0x1000);
                    writer.Write(this.ReadSheetText());
                    writer.Flush();
                } finally {
                    writer.Close();
                }
                ArrayList subFiles = new ArrayList {
                    tempFilePath
                };
                if (OfficeWrap.Instance == null) {
                    OfficeWrap.Instance = new OfficeWrap();
                }
                OfficeWrap.Instance.Export(exportFileName, subFiles);
            } else {
                SheetExportActionEnum exportAction = inExcel ? SheetExportActionEnum.ssExportActionOpenInExcel : SheetExportActionEnum.ssExportActionNone;
                this.axSpreadsheet1.ActiveSheet.Export(exportFileName, exportAction);
            }
        }

        private OWC.Range Find(string partValue, OWC.Range after) {
            return this.axSpreadsheet1.ActiveSheet.UsedRange.Cells.Find(partValue, after, SheetFindLookInEnum.ssValues, SheetFindLookAtEnum.ssPart, SheetSearchOrderEnum.ssByRows, SheetSearchDirectionEnum.ssNext, false);
        }

        private string GetCellValue(string address) {
            return this.GetRange(address).Text.ToString().Trim();
        }

        private string GetCellValue(int row, int col) {
            if ((row >= 1) && (col >= 1)) {
                return this.GetRange(row, col).Text.ToString().Trim();
            }
            return "";
        }

        private string GetCellValueEx(int row, int col) {
            if ((row < 1) || (col < 1)) {
                return "";
            }
            OWC.Range mergeArea = this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(row, col).MergeArea;
            int index = mergeArea.Address.IndexOf(':');
            if (index > 0) {
                return this.GetRange(mergeArea.Address.Substring(0, index)).Text.ToString().Trim();
            }
            return mergeArea.Text.ToString().Trim();
        }

        private void GetColList(ArrayList midColLstOfMain, ArrayList midColLstOfNext) {
            try {
                if (this.m_tp != null) {
                    int num = PPCConvert.Address2Col(this.m_tp.MidBeginOfMain);
                    int num2 = PPCConvert.Address2Col(this.m_tp.MidEndOfMain);
                    int num3 = PPCConvert.Address2Col(this.m_tp.MidBeginOfNext);
                    int num4 = PPCConvert.Address2Col(this.m_tp.MidEndOfNext);
                    int row = PPCConvert.Address2Row(this.m_tp.MidBeginOfMain);
                    int num6 = PPCConvert.Address2Row(this.m_tp.MidBeginOfNext);
                    if (base.Parent.Text == "首页") {
                        for (int i = num; i <= num2; i++) {
                            if (this.GetFirstCell(PPCConvert.RowCol2Address(row, i)) == PPCConvert.RowCol2Address(row, i)) {
                                midColLstOfMain.Add(PPCConvert.Int2ABC(i));
                                midColLstOfNext.Add(PPCConvert.Int2ABC(i));
                            }
                        }
                    } else if (base.Parent.Text == "续页") {
                        for (int j = num3; j <= num4; j++) {
                            if (this.GetFirstCell(PPCConvert.RowCol2Address(num6, j)) == PPCConvert.RowCol2Address(num6, j)) {
                                midColLstOfNext.Add(PPCConvert.Int2ABC(j));
                                midColLstOfMain.Add(PPCConvert.Int2ABC(j));
                            }
                        }
                    }
                }
            } catch (Exception exception) {
                throw new Exception("获取首页或续页的表中有效列出错。" + exception.Message);
            }
        }

        private string GetFirstCell(string cellInMerageCells) {
            if (cellInMerageCells.IndexOf(":") > 0) {
                return cellInMerageCells.Substring(0, cellInMerageCells.IndexOf(":"));
            }
            OWC.Range mergeArea = this.GetRange(cellInMerageCells).MergeArea;
            if (mergeArea.Address.IndexOf(":") > 0) {
                return mergeArea.Address.Substring(0, mergeArea.Address.IndexOf(":"));
            }
            return cellInMerageCells;
        }

        public string GetMainItem() {
            int row = PPCConvert.Address2Row(this.midBegin);
            int num2 = PPCConvert.Address2Col(this.midBegin);
            int num3 = PPCConvert.Address2Col(this.midEnd);
            for (int i = num2; i <= num3; i++) {
                string cellValue = this.GetCellValue(row, i);
                if ((cellValue != null) && (cellValue != "")) {
                    PPCardCell cc = new PPCardCell();
                    PPCardCompiler.ExplainXml(cellValue, cc);
                    if (((cc != null) && (cc.Area == PPCardArea.Mid)) && (cc.Attachment == PPCellAttach.Option)) {
                        return cc.ClassName;
                    }
                }
            }
            return "";
        }

        public ArrayList GetMidBindCells() {
            ArrayList list = new ArrayList();
            if ((this.midBegin != null) && (this.midEnd != null)) {
                int row = PPCConvert.Address2Row(this.midBegin);
                int num2 = PPCConvert.Address2Col(this.midBegin);
                int num3 = PPCConvert.Address2Col(this.midEnd);
                for (int i = num2; i <= num3; i++) {
                    if ((this.GetCellValue(row, i) != null) && (this.GetCellValue(row, i) != string.Empty)) {
                        PPCardCell cc = new PPCardCell();
                        PPCardCompiler.ExplainXml(this.GetCellValue(row, i), cc);
                        list.Add(cc);
                    }
                }
            }
            return list;
        }

        public ArrayList GetMidColNameList() {
            ArrayList list = new ArrayList();
            int row = PPCConvert.Address2Row(this.midBegin);
            int num2 = PPCConvert.Address2Col(this.midBegin);
            int num3 = PPCConvert.Address2Col(this.midEnd);
            for (int i = num2; i <= num3; i++) {
                string address = this.GetRange(row, i).MergeArea.Address;
                if (address.IndexOf(":") > 0) {
                    string firstCell = this.GetFirstCell(address);
                    if (PPCConvert.Address2Col(firstCell) == i) {
                        list.Add(PPCConvert.Address2ColName(firstCell));
                    }
                } else {
                    list.Add(PPCConvert.Address2ColName(address));
                }
            }
            return list;
        }

        private OWC.Range GetRange(string address) {
            return this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(PPCConvert.Address2Row(address), PPCConvert.Address2Col(address));
        }

        private OWC.Range GetRange(int row, int col) {
            return this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(row, col);
        }

        private OWC.Range GetRange(string firstCell, string lastCell) {
            if (firstCell == lastCell) {
                return this.GetRange(firstCell).MergeArea;
            }
            return this.axSpreadsheet1.ActiveSheet.UsedRange.get_Range(this.GetRange(firstCell), this.GetRange(lastCell));
        }

        public string GetTempFilePath(string label) {
            string path = BizOperationHelper.Instance.GetTempFilePath() + this.m_tp.Item.Id;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
                DirectoryInfo info = new DirectoryInfo(path) {
                    Attributes = FileAttributes.Hidden
                };
            }
            return (path + @"\" + label + ".xls");
        }


        private void InitializeData(CLCardTemplate tp, int countRows, int countColumns, string midBegin, string minEnd, bool readOnly) {
            this.m_tp = tp;
            this.countRows = countRows;
            this.countColumns = countColumns;
            this.midBegin = midBegin;
            this.midEnd = minEnd;
        }

        private void InitializeHistory() {
            this.lstHistory = new ArrayList();
            OWC.Range after = this.GetRange(1, 1);
        Label_0014:
            after = this.Find("<修改记录>", after);
            if (after != null) {
                CLHistory history = new CLHistory();
                try {
                    PPCardCompiler.ExplainXml(after.Text.ToString(), history);
                } catch {
                    goto Label_0014;
                }
                this.lstHistory.Add(history);
                goto Label_0014;
            }
            this.lstHistory.Sort();
        }

        private void InitializeProcess() {
            this.lstProcess = new ArrayList();
            OWC.Range after = this.GetRange(1, 1);
        Label_0014:
            after = this.Find("<流程记录>", after);
            if (after == null) {
                return;
            }
            CLState state = new CLState {
                CellSign = after.Address
            };
            try {
                PPCardCompiler.ExplainXml(after.Text.ToString(), state);
            } catch {
                goto Label_0014;
            }
            this.lstProcess.Add(state);
            goto Label_0014;
        }

        private void InitializeSheet(bool readOnly) {
            try {
                string str = "A1:" + PPCConvert.RowCol2Address(this.countRows, this.countColumns);
                this.axSpreadsheet1.ViewableRange = str;
            } catch {
                this.axSpreadsheet1.ViewableRange = "A1:CZ100";
                MessageBox.Show("模板的总行数或者总列数不合理。有效区域已重置，请仔细检查，并重新设置有效区域。");
            }
            this.ReadOnly = readOnly;
        }

        private void OnFormSignature(object sender, EventArgs e) {
            string cellValue = this.GetCellValue(this.curCell);
            if (cellValue.Trim() == "") {
                cellValue = "<表单签字><表单签字>";
            }
            DlgFormSignature signature = new DlgFormSignature(cellValue, this.readOnly);
            if (signature.ShowDialog() == DialogResult.OK) {
                this.SetCellValue(this.curCell, signature.Script);
            }
        }

        private void OnFunction(object sender, EventArgs e) {
            ArrayList midColLstOfMain = new ArrayList();
            ArrayList midColLstOfNext = new ArrayList();
            this.GetColList(midColLstOfMain, midColLstOfNext);
            CLFunction func = new CLFunction();
            string cellValue = this.GetCellValue(this.curCell);
            if ((cellValue != null) && (cellValue != "")) {
                PPCardCompiler.ExplainXml(cellValue, func);
            } else {
                func.ColInMainPage = func.ColInNextPage = PPCConvert.Address2ColName(this.GetFirstCell(this.curCell));
                func.FuncType = ((MenuItemEx)sender).Text;
                func.DisplayInLastPage = true;
                if (func.FuncType == "单页列小计") {
                    func.DisplayInLastPage = false;
                }
            }
            DlgFunction function2 = new DlgFunction(this.m_tp, midColLstOfMain, midColLstOfNext, base.Parent.Text == "首页", func, this.readOnly);
            if ((function2.ShowDialog() == DialogResult.OK) && (function2.script != null)) {
                this.SetCellValue(this.curCell, function2.script);
            }
        }

        private void OnInputText(object sender, EventArgs e) {
            this.GetRange(this.curCell).Value = "请输入文本";
            this.GetRange(this.curCell).Select();
        }

        private void OnInsertBarcode(object sender, EventArgs e) {
            string cellValue = this.GetCellValue(this.curCell);
            DlgBarcode barcode = new DlgBarcode(cellValue, this.m_tp.HeadClass, this.readOnly);
            if (PPCardCompiler.GetXmlEditStyle(cellValue) == PPCellAttach.None) {
                barcode.CellStart = this.curCell;
            }
            if (barcode.ShowDialog() == DialogResult.OK) {
                this.SetCellValue(this.curCell, barcode.Script);
            }
        }

        private void OnInsertCurrentTime(object sender, EventArgs e) {
            this.SetCellValue(this.curCell, "<其他>当时日期</其他>");
        }

        private void OnInsertCurrentVersion(object sender, EventArgs e) {
            this.SetCellValue(this.curCell, "<其他>当前版本</其他>");
        }

        private void OnInsertLogonUser(object sender, EventArgs e) {
            this.SetCellValue(this.curCell, "<其他>当时用户</其他>");
        }

        private void OnInsertModifyRecord(object sender, EventArgs e) {
            ArrayList list = new ArrayList(this.lstHistory.Count);
            foreach (CLHistory history in this.lstHistory) {
                list.Add(history.CellSign);
            }
            DlgHistoryList list2 = new DlgHistoryList(this.lstHistory, this.readOnly);
            list2.ShowDialog();
            if (!this.readOnly) {
                foreach (string str in list) {
                    this.SetCellValue(str, "");
                }
                this.lstHistory = list2.Histories;
                foreach (CLHistory history2 in this.lstHistory) {
                    this.SetCellValue(history2.CellSign, PPCardCompiler.CreateXML(history2));
                }
            }
        }

        private void OnInsertPageIndex(object sender, EventArgs e) {
            DlgPageIndex index = new DlgPageIndex(this.GetCellValue(this.curCell), true, this.readOnly);
            if (index.ShowDialog() == DialogResult.OK) {
                this.SetCellValue(this.curCell, index.Script);
            }
        }

        private void OnInsertPagesCount(object sender, EventArgs e) {
            DlgPageIndex index = new DlgPageIndex(this.GetCellValue(this.curCell), false, this.readOnly);
            if (index.ShowDialog() == DialogResult.OK) {
                this.SetCellValue(this.curCell, index.Script);
            }
        }

        private void OnInsertPicture(object sender, EventArgs e) {
            string cellValue = this.GetCellValue(this.curCell);
            DlgPicture picture = new DlgPicture(cellValue, this.readOnly);
            if (PPCardCompiler.GetXmlEditStyle(cellValue) == PPCellAttach.None) {
                picture.CellStart = this.curCell;
            }
            if (picture.ShowDialog() == DialogResult.OK) {
                if ((picture.PicType != InterPicType.ResourcePic) && (base.Parent.Text == "封面")) {
                    MessageBox.Show("封面只能插入资源图片，请重新设置。");
                } else {
                    this.SetCellValue(this.curCell, picture.Script);
                }
            }
        }

        private void OnInsertProcessRecord(object sender, EventArgs e) {
            ArrayList list = new ArrayList(this.lstProcess.Count);
            foreach (CLState state in this.lstProcess) {
                list.Add(state.CellSign);
            }
            DlgProcessList list2 = new DlgProcessList(this.lstProcess, this.readOnly);
            list2.ShowDialog();
            if (!this.readOnly) {
                foreach (string str in list) {
                    this.SetCellValue(str, "");
                }
                this.lstProcess = list2.Processes;
                foreach (CLState state2 in this.lstProcess) {
                    this.SetCellValue(state2.CellSign, PPCardCompiler.CreateXML(state2));
                }
            }
        }

        private void OnShowCellProperty(object sender, EventArgs e) {
            if ((this.countColumns == 0x4e) && (this.countRows == 100)) {
                MessageBox.Show("必须先设置本页的有效区域和表中区域。\n请先选中有效区域，然后在模板的快捷菜单中点击“设为有效区域”命令。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else if ((this.midBegin == "AZ100") && (this.midBegin == "AZ100")) {
                MessageBox.Show("必须先设置本页的有效区域和表中区域。\n请先选中表中区域，然后在模板的快捷菜单中点击“设为表中区域”命令。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } else {
                string cellValue = this.GetCellValue(this.curCell);
                PPCardArea head = PPCardArea.Head;
                if ((sender == null) && (e == null)) {
                    head = PPCardArea.Mid;
                }
                bool isCAPP = ModelContext.MetaModel.IsCard(this.m_tp.TemplateType);
                this.SetOptionHasKeyOrSbuKey();
                DlgOption2 option = new DlgOption2(cellValue, head, this.readOnly, this.curCell, isCAPP, this.m_tp.TemplateType, this.m_tp);
                try {
                    if (PPCConvert.Address2Row(this.curCell) > 1) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell) - 1, PPCConvert.Address2Col(this.curCell));
                    }
                    if ((option.BlurLabel == "") && (PPCConvert.Address2Row(this.curCell) > 2)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell) - 2, PPCConvert.Address2Col(this.curCell));
                    } else if (((option.BlurLabel.Length > 0) && (option.BlurLabel[0] == '<')) && (PPCConvert.Address2Col(this.curCell) > 1)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell), PPCConvert.Address2Col(this.curCell) - 1);
                    }
                    if ((option.BlurLabel == "") && (PPCConvert.Address2Row(this.curCell) > 3)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell) - 3, PPCConvert.Address2Col(this.curCell));
                    } else if (((option.BlurLabel.Length > 0) && (option.BlurLabel[0] == '<')) && (PPCConvert.Address2Col(this.curCell) > 1)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell), PPCConvert.Address2Col(this.curCell) - 1);
                    }
                    if ((option.BlurLabel == "") && (PPCConvert.Address2Row(this.curCell) > 4)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell) - 4, PPCConvert.Address2Col(this.curCell));
                    } else if (((option.BlurLabel.Length > 0) && (option.BlurLabel[0] == '<')) && (PPCConvert.Address2Col(this.curCell) > 1)) {
                        option.BlurLabel = this.GetCellValueEx(PPCConvert.Address2Row(this.curCell), PPCConvert.Address2Col(this.curCell) - 1);
                    }
                    if ((option.BlurLabel != "") && (option.BlurLabel[0] == '<')) {
                        option.BlurLabel = "";
                    }
                } catch {
                }
                if (option.BlurLabel != null) {
                    option.BlurLabel.Replace(" ", "");
                }
                option.Text = this.curCell + "单元格属性";
                if ((option.CCInit.AttributeName != "") || !this.readOnly) {
                    try {
                        if (option.ShowDialog() == DialogResult.OK) {
                            this.GetRange(this.curCell).Value = option.CardCell.Script.Trim();
                        }
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message);
                    }
                }
            }
        }

        private void OnSignatureRemark(object sender, EventArgs e) {
            string cellValue = this.GetCellValue(this.curCell);
            if (cellValue.Trim() == "") {
                cellValue = "<批注><批注>";
            }
            DlgFormSignature signature = new DlgFormSignature(cellValue, this.readOnly);
            if (signature.ShowDialog() == DialogResult.OK) {
                this.SetCellValue(this.curCell, signature.Script);
            }
        }

        public void PageCompare(PPTmpPage comparedPage) {
            this.SetDefaultColor(comparedPage);
            int countRows = this.countRows;
            int rowEnd = comparedPage.countRows;
            int countColumns = this.countColumns;
            int col = comparedPage.countColumns;
            if ((countRows == rowEnd) && (countColumns == col)) {
                this.CompareSameRange(comparedPage, countRows, countColumns);
            } else if (countRows < rowEnd) {
                comparedPage.SetRowColor(countRows, rowEnd, col);
                if (countColumns == col) {
                    this.CompareSameRange(comparedPage, countRows, countColumns);
                } else if (countColumns > col) {
                    this.CompareSameRange(comparedPage, countRows, col);
                    this.SetColColor(col + 1, countColumns, countRows);
                } else {
                    this.CompareSameRange(comparedPage, countRows, countColumns);
                    comparedPage.SetColColor(countColumns + 1, col, countRows);
                }
            } else {
                this.SetRowColor(rowEnd + 1, countRows, countColumns);
                if (countColumns == col) {
                    this.CompareSameRange(comparedPage, rowEnd, countColumns);
                } else if (countColumns > col) {
                    this.CompareSameRange(comparedPage, rowEnd, col);
                    this.SetColColor(col + 1, countColumns, rowEnd);
                } else {
                    this.CompareSameRange(comparedPage, rowEnd, countColumns);
                    comparedPage.SetColColor(countColumns + 1, col, rowEnd);
                }
            }
        }

        private string ReadSheetText() {
            string str = this.axSpreadsheet1.HTMLData.Replace("protection:locked", "protection:unlocked");
            return this.axSpreadsheet1.HTMLData;
        }

        public void ResetViewRange() {
            this.axSpreadsheet1.ViewableRange = "A1:CZ100";
        }

        private void SetCellValue(string address, object val) {
            this.GetRange(address).Value = val;
        }

        private void SetColColor(int colStart, int colEnd, int row) {
            OWC.Range r = this.GetRange(PPCConvert.RowCol2Address(1, colStart), PPCConvert.RowCol2Address(row, colEnd));
            this.SetFontColor(r, "Fuchsia");
        }

        private void SetDefaultColor(PPTmpPage comparedPage) {
            this.SetFontColor(this.GetRange("A1", PPCConvert.RowCol2Address(this.countRows, this.countColumns)), "Black");
            comparedPage.SetFontColor(comparedPage.GetRange("A1", PPCConvert.RowCol2Address(comparedPage.countRows, comparedPage.countColumns)), "Black");
        }

        public void SetFontColor(OWC.Range r, string color) {
            try {
                object obj2 = color;
                if (r.get_Locked().ToString() != "False") {
                    r.set_Locked(false);
                    r.Font.set_Color(ref obj2);
                    r.set_Locked(true);
                } else {
                    r.Font.set_Color(ref obj2);
                }
            } catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        public bool SetMidTable(string tabText) {
            string address = this.axSpreadsheet1.Selection.Address;
            if (address.IndexOf(":") < 0) {
                MessageBox.Show("选择区域不合理。不能只是一个单元格。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            string str2 = address.Substring(0, address.IndexOf(":"));
            string str3 = address.Substring(address.IndexOf(":") + 1);
            if (!CLPPFile.CheckAddress(str2) || !CLPPFile.CheckAddress(str3)) {
                MessageBox.Show("选择区域不合理。不能只是一行或者一列。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            this.midBegin = str2;
            this.midEnd = str3;
            string str4 = tabText;
            if (str4 != null) {
                if (str4 == "封面") {
                    this.m_tp.MidBeginOfCover = str2;
                    this.m_tp.MidEndOfCover = str3;
                } else if (str4 == "首页") {
                    this.m_tp.MidBeginOfMain = str2;
                    this.m_tp.MidEndOfMain = str3;
                } else if (str4 == "续页") {
                    this.m_tp.MidBeginOfNext = str2;
                    this.m_tp.MidEndOfNext = str3;
                }
            }
            return true;
        }

        public void SetOptionHasKeyOrSbuKey() {
            if (base.Parent.Text == "封面") {
                DlgOption2.HasKey = true;
                DlgOption2.HasSubKey = true;
            } else {
                DlgOption2.HasKey = false;
                DlgOption2.HasSubKey = false;
                foreach (PPCardCell cell in this.GetMidBindCells()) {
                    if (cell.IsKey) {
                        DlgOption2.HasKey = true;
                    } else if (cell.IsSubKey) {
                        DlgOption2.HasSubKey = true;
                    }
                    if (DlgOption2.HasKey && DlgOption2.HasSubKey) {
                        break;
                    }
                }
            }
        }

        public bool SetPageRange(string tabText) {
            string address = this.axSpreadsheet1.Selection.Address;
            if (address.IndexOf(":") < 0) {
                MessageBox.Show("选择区域不合理。不能只是一个单元格。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            string str2 = address.Substring(0, address.IndexOf(":"));
            string str3 = address.Substring(address.IndexOf(":") + 1);
            if (!CLPPFile.CheckAddress(str2) || !CLPPFile.CheckAddress(str3)) {
                MessageBox.Show("选择区域不合理。不能只是一行或者一列。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (str2 != "A1") {
                MessageBox.Show("必须从A1单元格开始选择区域。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            try {
                this.axSpreadsheet1.ViewableRange = address;
            } catch {
                MessageBox.Show("选择区域不合理。要确保选择区域所有单元格(特别是合并形成的单元格)都是完整的。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (MessageBox.Show(tabText + "有效区域已经重新设置，是否保存这些设置？请注意：模板定制时如果插入、删除行和列，必须重新设置模板的有效区域和表中区域。", "卡片模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                string str4 = "A1:" + PPCConvert.RowCol2Address(this.countRows, this.countColumns);
                this.axSpreadsheet1.ViewableRange = str4;
                return false;
            }
            this.countRows = PPCConvert.Address2Row(str3);
            this.countColumns = PPCConvert.Address2Col(str3);
            string str5 = tabText;
            if (str5 != null) {
                if (str5 == "封面") {
                    this.m_tp.RowCountOfCover = this.countRows;
                    this.m_tp.ColCountOfCover = this.countColumns;
                } else if (str5 == "首页") {
                    this.m_tp.RowCountOfMain = this.countRows;
                    this.m_tp.ColCountOfMain = this.countColumns;
                } else if (str5 == "续页") {
                    this.m_tp.RowCountOfNext = this.countRows;
                    this.m_tp.ColCountOfNext = this.countColumns;
                }
            }
            return true;
        }

        private void SetRowColor(int rowStart, int rowEnd, int col) {
            OWC.Range r = this.GetRange(PPCConvert.RowCol2Address(rowStart, 1), PPCConvert.RowCol2Address(rowEnd, col));
            this.SetFontColor(r, "Fuchsia");
        }

        [Category("数据"), Description("是否只读")]
        public bool ReadOnly {
            get {
                return this.readOnly;
            }
            set {
                this.readOnly = value;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowSizingAllColumns = true;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowSizingAllRows = true;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowFiltering = false;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowSorting = false;
                this.axSpreadsheet1.AllowAboutDialog = false;
                this.axSpreadsheet1.EnableAutoCalculate = false;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowDeletingColumns = !value;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowDeletingRows = !value;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowInsertingColumns = !value;
                this.axSpreadsheet1.ActiveSheet.Protection.AllowInsertingRows = !value;
                this.axSpreadsheet1.ActiveSheet.Protection.Enabled = true;
                this.axSpreadsheet1.ActiveSheet.Cells.set_Locked(value);
            }
        }

        public string TemplateText {
            get {
                this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(1, 1).Select();
                return this.axSpreadsheet1.HTMLData;
            }
            set {
                if (value != "<html><html/>") {
                    this.axSpreadsheet1.HTMLData = value;
                }
            }
        }

        [Category("数据"), Description("模板文件的路径"), Editor(typeof(TemplateUrlEditor), typeof(UITypeEditor))]
        public string TemplateUrl {
            get {
                return this.filePath;
            }
            set {
                if (File.Exists(value)) {
                    this.axSpreadsheet1.HTMLURL = value;
                    this.filePath = value;
                }
            }
        }

        private class TemplateUrlEditor : UITypeEditor {
            public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value) {
                OpenFileDialog dialog = new OpenFileDialog {
                    Filter = "模板文件(*.htm)|*.htm",
                    Title = "指定模板文件路径",
                    DefaultExt = "htm"
                };
                if (dialog.ShowDialog() == DialogResult.OK) {
                    value = dialog.FileName;
                }
                return value;
            }

            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
                return UITypeEditorEditStyle.Modal;
            }
        }
    }
}

