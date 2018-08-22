using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Xml;
using Thyt.TiPLM.PLL.PPM.Card;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgPageSetup : Form {
        
        public CLPageSetup CltPageSetup;
        
        private bool NotZoomButFittoTallWide;
       
        public DlgPageSetup() {
            this.CltPageSetup = new CLPageSetup();
            this.InitializeComponent();
            this.rdb_Portrait.Checked = true;
            this.rdb_Zoom.Checked = true;
            this.rdb_ListFirst.Checked = true;
            PrintDocument document = new PrintDocument();
            foreach (PaperSize size in document.PrinterSettings.PaperSizes) {
                this.cbb_PageSize.Items.Add(size.PaperName);
            }
            this.cbb_PageSize.Text = "A4";
            this.cbb_PaintQuality.SelectedIndex = 1;
            this.cbb_Header.SelectedIndex = 0;
            this.cbb_Footer.SelectedIndex = 0;
            this.cbb_Postil.SelectedIndex = 0;
            this.cbb_PrintWrongCell.SelectedIndex = 0;
            this.txb_Header.Clear();
            this.txb_Footer.Clear();
        }

        public DlgPageSetup(string pageSetupScript) {
            this.CltPageSetup = new CLPageSetup();
            this.InitializeComponent();
            PrintDocument document = new PrintDocument();
            foreach (PaperSize size in document.PrinterSettings.PaperSizes) {
                this.cbb_PageSize.Items.Add(size.PaperName);
            }
            this.cbb_PageSize.Text = "A4";
            if (pageSetupScript == "") {
                this.InitializeComponent();
                this.rdb_Landscape.Checked = true;
                this.rdb_Zoom.Checked = true;
                this.rdb_RowFirst.Checked = true;
                this.cbb_PaintQuality.SelectedIndex = 1;
                this.cbb_PaintQuality.Text = "600 点/英寸";
                this.cbb_Header.SelectedIndex = 0;
                this.cbb_Footer.SelectedIndex = 0;
                this.cbb_Postil.SelectedIndex = 0;
                this.cbb_PrintWrongCell.SelectedIndex = 0;
                this.txb_Header.Clear();
                this.txb_Footer.Clear();
            } else {
                XmlDocument doc = new XmlDocument();
                try {
                    doc.LoadXml(pageSetupScript);
                } catch {
                    this.rdb_ListFirst.Checked = true;
                    this.cbb_PaintQuality.SelectedIndex = 1;
                    this.cbb_Header.SelectedIndex = 0;
                    this.cbb_Footer.SelectedIndex = 0;
                    this.cbb_Postil.SelectedIndex = 0;
                    this.cbb_PrintWrongCell.SelectedIndex = 0;
                    this.txb_Header.Clear();
                    this.txb_Footer.Clear();
                    return;
                }
                bool blackAndWhite = this.GetBlackAndWhite(doc);
                double bottomMargin = this.GetBottomMargin(doc);
                string centerFooter = null;
                if (this.GetCenterFooter(doc) != "") {
                    centerFooter = this.GetCenterFooter(doc);
                }
                string centerHeader = null;
                if (this.GetCenterHeader(doc) != "") {
                    centerHeader = this.GetCenterHeader(doc);
                }
                bool centerHorizontally = this.GetCenterHorizontally(doc);
                bool centerVertically = this.GetCenterVertically(doc);
                bool draft = this.GetDraft(doc);
                long firstPageNumber = this.GetFirstPageNumber(doc);
                double footerMargin = this.GetFooterMargin(doc);
                double headerMargin = this.GetHeaderMargin(doc);
                string leftFooter = null;
                if (this.GetLeftFooter(doc) != "") {
                    leftFooter = this.GetLeftFooter(doc);
                }
                string leftHeader = null;
                if (this.GetLeftHeader(doc) != "") {
                    leftHeader = this.GetLeftHeader(doc);
                }
                double leftMargin = this.GetLeftMargin(doc);
                string order = this.GetOrder(doc);
                string orientation = this.GetOrientation(doc);
                string paperSize = this.GetPaperSize(doc);
                bool printGridlines = this.GetPrintGridlines(doc);
                bool printHeadings = this.GetPrintHeadings(doc);
                string rightFooter = null;
                if (this.GetRightFooter(doc) != "") {
                    rightFooter = this.GetRightFooter(doc);
                }
                string rightHeader = null;
                if (this.GetRightHeader(doc) != "") {
                    rightHeader = this.GetRightHeader(doc);
                }
                double rightMargin = this.GetRightMargin(doc);
                double topMargin = this.GetTopMargin(doc);
                int zoom = this.GetZoom(doc);
                int fitToPagesTall = this.GetFitToPagesTall(doc);
                int fitToPagesWide = this.GetFitToPagesWide(doc);
                int printQuality = this.GetPrintQuality(doc);
                string printComments = this.GetPrintComments(doc);
                string printErrors = this.GetPrintErrors(doc);
                bool shrinkToFit = this.GetShrinkToFit(doc);
                this.ckb_SingleColor.Checked = blackAndWhite;
                this.npd_BottomMargin.Value = (decimal)bottomMargin;
                string str12 = leftHeader + centerHeader + rightHeader;
                string str13 = null;
                switch (str12) {
                    case "":
                        str13 = "(无)";
                        break;

                    case "第 &P 页":
                        str13 = "第 1 页";
                        break;

                    case "第 &P 页，共 &N 页":
                        str13 = "第 1 页，共 ？页";
                        break;

                    case "&A":
                        str13 = "Sheet1";
                        break;

                    case "Microsoft 机密&D第 &P 页":
                        str13 = "Microsoft 机密，2003-10-31，第 1 页";
                        break;

                    case "&F":
                        str13 = "Book1";
                        break;

                    case "&A第 &P 页":
                        str13 = "Sheet1,第 1 页";
                        break;

                    case "&AMicrosoft 机密第 &P 页":
                        str13 = "Sheet1,Microsoft 机密,第 1 页";
                        break;

                    case "&F 第 &P 页":
                        str13 = "Book1,第 1 页";
                        break;

                    case "第 &P 页&A":
                        str13 = "第 1 页,Sheet1";
                        break;

                    case "第 &P 页&F":
                        str13 = "第 1 页,Book1";
                        break;
                }
                this.cbb_Header.SelectedIndex = 0;
                for (int i = 0; i <= (this.cbb_Header.Items.Count - 1); i++) {
                    if (this.cbb_Header.GetItemText(this.cbb_Header.Items[i]) == str13) {
                        this.cbb_Header.SelectedIndex = i;
                    }
                }
                if (this.cbb_Header.SelectedIndex != 0) {
                    this.txb_Header.Text = str13;
                } else {
                    this.txb_Header.Text = "(无)";
                }
                string str14 = leftFooter + centerFooter + rightFooter;
                string str15 = null;
                switch (str14) {
                    case "":
                        str15 = "(无)";
                        break;

                    case "第 &P 页":
                        str15 = "第 1 页";
                        break;

                    case "第 &P 页，共 &N 页":
                        str15 = "第 1 页，共 ？页";
                        break;

                    case "&A":
                        str15 = "Sheet1";
                        break;

                    case "Microsoft 机密&D第 &P 页":
                        str15 = "Microsoft 机密，2003-10-31，第 1 页";
                        break;

                    case "&F":
                        str15 = "Book1";
                        break;

                    case "&A第 &P 页":
                        str15 = "Sheet1,第 1 页";
                        break;

                    case "&AMicrosoft 机密第 &P 页":
                        str15 = "Sheet1,Microsoft 机密,第 1 页";
                        break;

                    case "&F 第 &P 页":
                        str15 = "Book1,第 1 页";
                        break;

                    case "第 &P 页&A":
                        str15 = "第 1 页,Sheet1";
                        break;

                    case "第 &P 页&F":
                        str15 = "第 1 页,Book1";
                        break;
                }
                this.cbb_Footer.SelectedIndex = 0;
                for (int j = 0; j <= (this.cbb_Footer.Items.Count - 1); j++) {
                    if (this.cbb_Footer.GetItemText(this.cbb_Header.Items[j]) == str15) {
                        this.cbb_Footer.SelectedIndex = j;
                    }
                }
                if (this.cbb_Footer.SelectedIndex != 0) {
                    this.txb_Footer.Text = str15;
                } else {
                    this.txb_Footer.Text = "(无)";
                }
                this.ckb_Level.Checked = centerHorizontally;
                this.ckb_Vertical.Checked = centerVertically;
                this.ckb_PrintAsDraft.Checked = draft;
                this.txb_StartPage.Text = firstPageNumber.ToString();
                this.npd_FooterMargin.Value = (decimal)footerMargin;
                this.npd_HeaderMargin.Value = (decimal)headerMargin;
                this.npd_LeftMargin.Value = (decimal)leftMargin;
                if (order == "xlDownThenOver") {
                    this.rdb_ListFirst.Checked = true;
                }
                if (order == "xlOverThenDown") {
                    this.rdb_RowFirst.Checked = true;
                }
                if (orientation == "xlPortrait") {
                    this.rdb_Portrait.Checked = true;
                }
                if (orientation == "xlLandscape") {
                    this.rdb_Landscape.Checked = true;
                }
                if (paperSize.StartsWith("xlPaper")) {
                    switch (paperSize) {
                        case "xlPaperLetter":
                            paperSize = "Letter";
                            break;

                        case "xlPaperLegal":
                            paperSize = "Legal";
                            break;

                        case "xlPaperExecutive":
                            paperSize = "Executive";
                            break;

                        case "xlPaperA3":
                            paperSize = "A3";
                            break;

                        case "xlPaperA4":
                            paperSize = "A4";
                            break;

                        case "xlPaperA5":
                            paperSize = "A5";
                            break;

                        case "xlPaperB4":
                            paperSize = "B4(JIS)";
                            break;

                        case "xlPaperB5":
                            paperSize = "B5(JIS)";
                            break;

                        case "xlPaper11x17":
                            paperSize = "11x17";
                            break;

                        case "xlPaperEnvelope10":
                            paperSize = "Envelope #10";
                            break;

                        case "xlPaperEnvelopeDL":
                            paperSize = "Evenlope DL";
                            break;

                        case "xlPaperEnvelopeC5":
                            paperSize = "Evenlope C5";
                            break;

                        case "xlPaperEnvelopeB5":
                            paperSize = "Evenlope B5";
                            break;

                        case "xlPaperEnvelopeMonarch":
                            paperSize = "Evenlope Monarch";
                            break;

                        case "xlPaperEnvelopeC3":
                            paperSize = "大号明信片";
                            break;

                        case "xlPaperEnvelopePersonal":
                            paperSize = "明信片";
                            break;

                        case "xlPaperFolio":
                            paperSize = "8.5x13";
                            break;
                    }
                }
                if ((paperSize != "") && (paperSize != null)) {
                    int index = this.cbb_PageSize.Items.IndexOf(paperSize);
                    if (index < 0) {
                        MessageBox.Show("严重警告：当前的页面设置中的纸张大小“" + paperSize + "”和当前的打印机不兼容，请重新设置纸张大小或者默认打印机！");
                    } else {
                        this.cbb_PageSize.SelectedIndex = index;
                    }
                }
                this.ckb_GridLine.Checked = printGridlines;
                this.ckb_RowList.Checked = printHeadings;
                this.npd_RightMargin.Value = (decimal)rightMargin;
                this.npd_TopMargin.Value = (decimal)topMargin;
                if (zoom != 100) {
                    this.npd_Zoom.Value = zoom;
                }
                if ((fitToPagesTall != 1) || (fitToPagesWide != 1)) {
                    this.npd_PageHigh.Value = fitToPagesTall;
                    this.npd_PageWidth.Value = fitToPagesWide;
                }
                if (this.NotZoomButFittoTallWide) {
                    this.rdb_Adjust.Checked = true;
                } else {
                    this.rdb_Zoom.Checked = true;
                }
                switch (printQuality) {
                    case 0x4b0:
                        this.cbb_PaintQuality.SelectedIndex = 0;
                        break;

                    case 600:
                        this.cbb_PaintQuality.SelectedIndex = 1;
                        break;

                    case 0:
                        this.cbb_PaintQuality.SelectedIndex = 1;
                        break;
                }
                string str19 = printComments;
                if (str19 != null) {
                    if (str19 == "xlPrintInPlace") {
                        this.cbb_Postil.SelectedIndex = 2;
                    } else if (str19 == "xlPrintNoComments") {
                        this.cbb_Postil.SelectedIndex = 0;
                    } else if (str19 == "xlPrintSheetEnd") {
                        this.cbb_Postil.SelectedIndex = 1;
                    }
                }
                if (printComments == "") {
                    this.cbb_Postil.SelectedIndex = 0;
                }
                string str20 = printErrors;
                if (str20 != null) {
                    if (str20 == "xlPrintErrorsBlank") {
                        this.cbb_PrintWrongCell.SelectedIndex = 1;
                    } else if (str20 == "xlPrintErrorsDash") {
                        this.cbb_PrintWrongCell.SelectedIndex = 2;
                    } else if (str20 == "xlPrintErrorsDisplayed") {
                        this.cbb_PrintWrongCell.SelectedIndex = 0;
                    } else if (str20 == "xlPrintErrorsNA") {
                        this.cbb_PrintWrongCell.SelectedIndex = 3;
                    }
                }
                if (printErrors == "") {
                    this.cbb_PrintWrongCell.SelectedIndex = 0;
                }
                this.chk_ShrinkToFit.Checked = shrinkToFit;
            }
        }

        private void btn_Ensure_Click(object sender, EventArgs e) {
            try {
                if (this.tpg_Page_Exception() && this.tpg_Margin_Exception()) {
                    this.tpg_Page_Save();
                    this.tpg_Margin_Save();
                    this.tpg_HeaderFooter_Save();
                    this.tpg_Table_Save();
                    base.DialogResult = DialogResult.OK;
                } else {
                    base.DialogResult = DialogResult.None;
                }
            } catch {
            }
        }

        private void cbb_Footer_SelectedIndexChanged(object sender, EventArgs e) {
            this.txb_Footer.Text = this.cbb_Footer.Text;
        }

        private void cbb_Header_SelectedIndexChanged(object sender, EventArgs e) {
            this.txb_Header.Text = this.cbb_Header.Text;
        }

        private void ckb_Level_Click(object sender, EventArgs e) {
        }

        private void ckb_Vertical_Click(object sender, EventArgs e) {
        }

        public bool GetBlackAndWhite(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("单色打印")[0];
            if (element != null) {
                return XmlConvert.ToBoolean(element.InnerText);
            }
            return true;
        }

        public double GetBottomMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("底端边距")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 0.5;
        }

        public string GetCenterFooter(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页脚中心内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public string GetCenterHeader(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页眉中心内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public bool GetCenterHorizontally(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("水平居中打印")[0];
            if (element != null) {
                return XmlConvert.ToBoolean(element.InnerText);
            }
            return true;
        }

        public bool GetCenterVertically(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("垂直居中打印")[0];
            if (element != null) {
                return XmlConvert.ToBoolean(element.InnerText);
            }
            return true;
        }

        public bool GetDraft(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("按草稿方式打印")[0];
            return ((element != null) && XmlConvert.ToBoolean(element.InnerText));
        }

        public long GetFirstPageNumber(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("第一页页号")[0];
            if (element != null) {
                return XmlConvert.ToInt64(element.InnerText);
            }
            return 1L;
        }

        public int GetFitToPagesTall(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("缩放使用的页高")[0];
            if (element == null) {
                return 1;
            }
            int num = XmlConvert.ToInt32(element.InnerText);
            if (num == 0) {
                return 1;
            }
            this.NotZoomButFittoTallWide = true;
            return num;
        }

        public int GetFitToPagesWide(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("缩放使用的页宽")[0];
            if (element == null) {
                return 1;
            }
            int num = XmlConvert.ToInt32(element.InnerText);
            if (num == 0) {
                return 1;
            }
            this.NotZoomButFittoTallWide = true;
            return num;
        }

        public double GetFooterMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页脚到页面底端距离")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 0.0;
        }

        public double GetHeaderMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页面顶端到页眉距离")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 0.0;
        }

        public string GetLeftFooter(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页脚左边内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public string GetLeftHeader(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页眉左边内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public double GetLeftMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("左边距")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 2.0;
        }

        public string GetOrder(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("打印顺序")[0];
            return element.InnerText;
        }

        public string GetOrientation(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("方向")[0];
            return element.InnerText;
        }

        public string GetPaperSize(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("纸张大小")[0];
            return element.InnerText;
        }

        public string GetPrintComments(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("批注打印方式")[0];
            return element.InnerText;
        }

        public string GetPrintErrors(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("打印错误方式")[0];
            return element.InnerText;
        }

        public bool GetPrintGridlines(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("打印单元格网格线")[0];
            return ((element != null) && XmlConvert.ToBoolean(element.InnerText));
        }

        public bool GetPrintHeadings(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("打印行标题和列标题")[0];
            return ((element != null) && XmlConvert.ToBoolean(element.InnerText));
        }

        public int GetPrintQuality(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("打印质量")[0];
            if (element.InnerText == "1200 点/英寸") {
                return 0x4b0;
            }
            return 600;
        }

        public string GetRightFooter(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页脚右边内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public string GetRightHeader(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("页眉右边内容")[0];
            if (element != null) {
                return element.InnerText;
            }
            return null;
        }

        public double GetRightMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("右边距")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 0.5;
        }

        public bool GetShrinkToFit(XmlDocument doc) {
            try {
                XmlElement element = (XmlElement)doc.GetElementsByTagName("缩小字体填充")[0];
                return ((element == null) || XmlConvert.ToBoolean(element.InnerText));
            } catch {
                return true;
            }
        }

        public double GetTopMargin(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("上边距")[0];
            if (element != null) {
                return XmlConvert.ToDouble(element.InnerText);
            }
            return 0.5;
        }

        public int GetZoom(XmlDocument doc) {
            XmlElement element = (XmlElement)doc.GetElementsByTagName("缩放比例")[0];
            if (element == null) {
                return 100;
            }
            int num = XmlConvert.ToInt32(element.InnerText);
            if (num == 0) {
                return 100;
            }
            return num;
        }

        private void npd_BottomMargin_Click(object sender, EventArgs e) {
        }

        private void npd_FooterMargin_Click(object sender, EventArgs e) {
        }

        private void npd_HeaderMargin_Click(object sender, EventArgs e) {
        }

        private void npd_LeftMargin_Click(object sender, EventArgs e) {
        }

        private void npd_PageHigh_ValueChanged(object sender, EventArgs e) {
            this.rdb_Adjust.Checked = true;
        }

        private void npd_PageWidth_ValueChanged(object sender, EventArgs e) {
            this.rdb_Adjust.Checked = true;
        }

        private void npd_RightMargin_Click(object sender, EventArgs e) {
        }

        private void npd_TopMargin_Click(object sender, EventArgs e) {
        }

        private void npd_Zoom_ValueChanged(object sender, EventArgs e) {
            this.rdb_Zoom.Checked = true;
        }

        private void rdb_Adjust_Click(object sender, EventArgs e) {
            this.npd_PageWidth.Focus();
            int length = this.npd_PageWidth.Value.ToString().Length;
            this.npd_PageWidth.Select(0, length);
        }

        private void rdb_Landscape_Click(object sender, EventArgs e) {
            this.rdb_Landscape.Checked = true;
        }

        private void rdb_ListFirst_Click(object sender, EventArgs e) {
            this.rdb_ListFirst.Checked = true;
        }

        private void rdb_Portrait_Click(object sender, EventArgs e) {
            this.rdb_Portrait.Checked = true;
        }

        private void rdb_RowFirst_Click(object sender, EventArgs e) {
            this.rdb_RowFirst.Checked = true;
        }

        private void rdb_Zoom_Click(object sender, EventArgs e) {
            this.npd_Zoom.Focus();
            int length = this.npd_Zoom.Value.ToString().Length;
            this.npd_Zoom.Select(0, length);
        }

        private void tcl_PageSetting_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tcl_PageSetting.SelectedIndex != 0) {
                this.tpg_Page_Exception();
            }
            if (this.tcl_PageSetting.SelectedIndex != 1) {
                this.tpg_Margin_Exception();
            }
            if (this.tcl_PageSetting.SelectedIndex != 2) {
                this.tpg_HeaderFooter_Exception();
            }
            if (this.tcl_PageSetting.SelectedIndex != 3) {
                this.tpg_Table_Exception();
            }
        }

        private void tpg_HeaderFooter_Exception() {
        }

        private void tpg_HeaderFooter_Leave(object sender, EventArgs e) {
            this.tpg_HeaderFooter_Exception();
        }

        private void tpg_HeaderFooter_Save() {
            switch (this.txb_Header.Text) {
                case "(无)":
                    this.CltPageSetup.CenterHeader = "";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "";
                    break;

                case "第 1 页":
                    this.CltPageSetup.CenterHeader = "第 &P 页";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "";
                    break;

                case "第 1 页，共 ？页":
                    this.CltPageSetup.CenterHeader = "第 &P 页，共 &N 页";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "";
                    break;

                case "Sheet1":
                    this.CltPageSetup.CenterHeader = "&A";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "";
                    break;

                case "Microsoft 机密，2003-10-31，第 1 页":
                    this.CltPageSetup.CenterHeader = "&D";
                    this.CltPageSetup.LeftHeader = "Microsoft 机密";
                    this.CltPageSetup.RightHeader = "第 &P 页";
                    break;

                case "Book1":
                    this.CltPageSetup.CenterHeader = "&F";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "";
                    break;

                case "Sheet1,第 1 页":
                    this.CltPageSetup.CenterHeader = "&A";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "第 &P 页";
                    break;

                case "Sheet1,Microsoft 机密,第 1 页":
                    this.CltPageSetup.CenterHeader = "Microsoft 机密";
                    this.CltPageSetup.LeftHeader = "&A";
                    this.CltPageSetup.RightHeader = "第 &P 页";
                    break;

                case "Book1,第 1 页":
                    this.CltPageSetup.CenterHeader = "&F";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = " 第 &P 页";
                    break;

                case "第 1 页,Sheet1":
                    this.CltPageSetup.CenterHeader = "第 &P 页";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "&A";
                    break;

                case "第 1 页,Book1":
                    this.CltPageSetup.CenterHeader = "第 &P 页";
                    this.CltPageSetup.LeftHeader = "";
                    this.CltPageSetup.RightHeader = "&F";
                    break;
            }
            switch (this.txb_Footer.Text) {
                case "(无)":
                    this.CltPageSetup.CenterFooter = "";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "";
                    return;

                case "第 1 页":
                    this.CltPageSetup.CenterFooter = "第 &P 页";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "";
                    return;

                case "第 1 页，共 ？页":
                    this.CltPageSetup.CenterFooter = "第 &P 页，共 &N 页";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "";
                    return;

                case "Sheet1":
                    this.CltPageSetup.CenterFooter = "&A";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "";
                    return;

                case "Microsoft 机密，2003-10-31，第 1 页":
                    this.CltPageSetup.CenterFooter = "&D";
                    this.CltPageSetup.LeftFooter = "Microsoft 机密";
                    this.CltPageSetup.RightFooter = "第 &P 页";
                    return;

                case "Book1":
                    this.CltPageSetup.CenterFooter = "&F";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "";
                    return;

                case "Sheet1,第 1 页":
                    this.CltPageSetup.CenterFooter = "&A";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "第 &P 页";
                    return;

                case "Sheet1,Microsoft 机密,第 1 页":
                    this.CltPageSetup.CenterFooter = "Microsoft 机密";
                    this.CltPageSetup.LeftFooter = "&A";
                    this.CltPageSetup.RightFooter = "第 &P 页";
                    return;

                case "Book1,第 1 页":
                    this.CltPageSetup.CenterFooter = "&F";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "第 &P 页";
                    return;

                case "第 1 页,Sheet1":
                    this.CltPageSetup.CenterFooter = "第 &P 页";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "&A";
                    return;

                case "第 1 页,Book1":
                    this.CltPageSetup.CenterFooter = "第 &P 页";
                    this.CltPageSetup.LeftFooter = "";
                    this.CltPageSetup.RightFooter = "&F";
                    return;
            }
        }

        private bool tpg_Margin_Exception() {
            bool flag = true;
            if (this.npd_TopMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_TopMargin.Focus();
                    int length = this.npd_TopMargin.Value.ToString().Length;
                    this.npd_TopMargin.Select(0, length);
                }
                flag = false;
            } else if ((this.npd_TopMargin.Value < 0M) || (this.npd_TopMargin.Value > 8M)) {
                if (DialogResult.OK == MessageBox.Show("边界设置不适合指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_TopMargin.Focus();
                    int num2 = this.npd_TopMargin.Value.ToString().Length;
                    this.npd_TopMargin.Select(0, num2);
                }
                flag = false;
            }
            if (this.npd_BottomMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_BottomMargin.Focus();
                    int num3 = this.npd_BottomMargin.Value.ToString().Length;
                    this.npd_BottomMargin.Select(0, num3);
                }
                flag = false;
            } else if ((this.npd_BottomMargin.Value < 0M) || (this.npd_BottomMargin.Value > 8M)) {
                if (DialogResult.OK == MessageBox.Show("边界设置不适合指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_BottomMargin.Focus();
                    int num4 = this.npd_BottomMargin.Value.ToString().Length;
                    this.npd_BottomMargin.Select(0, num4);
                }
                flag = false;
            }
            if (this.npd_LeftMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_LeftMargin.Focus();
                    int num5 = this.npd_LeftMargin.Value.ToString().Length;
                    this.npd_LeftMargin.Select(0, num5);
                }
                flag = false;
            } else if ((this.npd_LeftMargin.Value < 0M) || (this.npd_LeftMargin.Value > 8M)) {
                if (DialogResult.OK == MessageBox.Show("边界设置不适合指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_LeftMargin.Focus();
                    int num6 = this.npd_LeftMargin.Value.ToString().Length;
                    this.npd_LeftMargin.Select(0, num6);
                }
                flag = false;
            }
            if (this.npd_RightMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_RightMargin.Focus();
                    int num7 = this.npd_RightMargin.Value.ToString().Length;
                    this.npd_RightMargin.Select(0, num7);
                }
                flag = false;
            } else if ((this.npd_RightMargin.Value < 0M) || (this.npd_RightMargin.Value > 8M)) {
                if (DialogResult.OK == MessageBox.Show("边界设置不适合指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_RightMargin.Focus();
                    int num8 = this.npd_RightMargin.Value.ToString().Length;
                    this.npd_RightMargin.Select(0, num8);
                }
                flag = false;
            }
            if (this.npd_HeaderMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_HeaderMargin.Focus();
                    int num9 = this.npd_HeaderMargin.Value.ToString().Length;
                    this.npd_HeaderMargin.Select(0, num9);
                }
                flag = false;
            } else if ((this.npd_HeaderMargin.Value < 0M) || (this.npd_HeaderMargin.Value > 8M)) {
                if (DialogResult.OK == MessageBox.Show("边界设置不是何止指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_HeaderMargin.Focus();
                    int num10 = this.npd_HeaderMargin.Value.ToString().Length;
                    this.npd_HeaderMargin.Select(0, num10);
                }
                flag = false;
            }
            if (this.npd_FooterMargin.Value.GetType() != typeof(decimal)) {
                if (DialogResult.OK == MessageBox.Show("输入不正确。要求输入为整数或小数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                    this.tcl_PageSetting.SelectedIndex = 1;
                    this.npd_FooterMargin.Focus();
                    int num11 = this.npd_FooterMargin.Value.ToString().Length;
                    this.npd_FooterMargin.Select(0, num11);
                }
                return false;
            }
            if ((this.npd_FooterMargin.Value >= 0M) && (this.npd_FooterMargin.Value <= 8M)) {
                return flag;
            }
            if (DialogResult.OK == MessageBox.Show("边界设置不适合指定的纸张大小。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                this.tcl_PageSetting.SelectedIndex = 1;
                this.npd_FooterMargin.Focus();
                int num12 = this.npd_FooterMargin.Value.ToString().Length;
                this.npd_FooterMargin.Select(0, num12);
            }
            return false;
        }

        private void tpg_Margin_Leave(object sender, EventArgs e) {
            this.tpg_Margin_Exception();
        }

        private void tpg_Margin_Save() {
            this.CltPageSetup.TopMargin = (double)this.npd_TopMargin.Value;
            this.CltPageSetup.BottomMargin = (double)this.npd_BottomMargin.Value;
            this.CltPageSetup.LeftMargin = (double)this.npd_LeftMargin.Value;
            this.CltPageSetup.RightMargin = (double)this.npd_RightMargin.Value;
            this.CltPageSetup.HeaderMargin = (double)this.npd_HeaderMargin.Value;
            this.CltPageSetup.FooterMargin = (double)this.npd_FooterMargin.Value;
            if (this.ckb_Level.Checked) {
                this.CltPageSetup.CenterHorizontally = true;
            } else {
                this.CltPageSetup.CenterHorizontally = false;
            }
            if (this.ckb_Vertical.Checked) {
                this.CltPageSetup.CenterVertically = true;
            } else {
                this.CltPageSetup.CenterVertically = false;
            }
        }

        private bool tpg_Page_Exception() {
            if (((this.txb_StartPage.Text != "自动") && (this.txb_StartPage.Text != "")) && (this.txb_StartPage.Text != "1")) {
                try {
                    int num = int.Parse(this.txb_StartPage.Text);
                    if ((num < -32765) || (num > 0x7fff)) {
                        if (DialogResult.OK == MessageBox.Show("非有效整数。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                            this.tcl_PageSetting.SelectedIndex = 0;
                            this.txb_StartPage.Focus();
                            this.txb_StartPage.SelectAll();
                        }
                        return false;
                    }
                    return true;
                } catch (Exception exception) {
                    if (DialogResult.OK == MessageBox.Show("非有效整数。" + exception.Message, "注意", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)) {
                        this.tcl_PageSetting.SelectedIndex = 0;
                        this.txb_StartPage.Focus();
                        this.txb_StartPage.SelectAll();
                        return false;
                    }
                    return true;
                }
            }
            return true;
        }

        private void tpg_Page_Leave(object sender, EventArgs e) {
            this.tpg_Page_Exception();
        }

        private void tpg_Page_Save() {
            if (this.rdb_Portrait.Checked) {
                this.CltPageSetup.Orientation = "xlPortrait";
            } else if (this.rdb_Landscape.Checked) {
                this.CltPageSetup.Orientation = "xlLandscape";
            }
            if (this.rdb_Zoom.Checked) {
                this.CltPageSetup.Zoom = this.npd_Zoom.Value;
            } else {
                this.CltPageSetup.Zoom = 0M;
            }
            if (this.rdb_Adjust.Checked) {
                this.CltPageSetup.FitToPagesTall = (int)this.npd_PageHigh.Value;
                this.CltPageSetup.FitToPagesWide = (int)this.npd_PageWidth.Value;
            } else {
                this.CltPageSetup.FitToPagesTall = 0;
                this.CltPageSetup.FitToPagesWide = 0;
            }
            this.CltPageSetup.PaperSize = this.cbb_PageSize.Text;
            if (this.cbb_PaintQuality.Text == "1200 点/英寸") {
                this.CltPageSetup.PrintQuality = "1200 点/英寸";
            } else if (this.cbb_PaintQuality.Text == "600 点/英寸") {
                this.CltPageSetup.PrintQuality = "600 点/英寸";
            }
            if (this.txb_StartPage.Text == "自动") {
                this.CltPageSetup.FirstPageNumber = 1L;
            } else {
                int num = int.Parse(this.txb_StartPage.Text);
                this.CltPageSetup.FirstPageNumber = num;
            }
        }

        private void tpg_Table_Exception() {
        }

        private void tpg_Table_Leave(object sender, EventArgs e) {
            this.tpg_Table_Exception();
        }

        private void tpg_Table_Save() {
            if (this.ckb_GridLine.Checked) {
                this.CltPageSetup.PrintGridlines = true;
            } else {
                this.CltPageSetup.PrintGridlines = false;
            }
            if (this.ckb_SingleColor.Checked) {
                this.CltPageSetup.BlackAndWhite = true;
            } else {
                this.CltPageSetup.BlackAndWhite = false;
            }
            if (this.ckb_PrintAsDraft.Checked) {
                this.CltPageSetup.Draft = true;
            } else {
                this.CltPageSetup.Draft = false;
            }
            if (this.ckb_RowList.Checked) {
                this.CltPageSetup.PrintHeadings = true;
            } else {
                this.CltPageSetup.PrintHeadings = false;
            }
            string text = this.cbb_Postil.Text;
            if (text != null) {
                if (text == "(无)") {
                    this.CltPageSetup.PrintComments = "xlPrintNoComments";
                } else if (text == "工作表末尾") {
                    this.CltPageSetup.PrintComments = "xlPrintSheetEnd";
                } else if (text == "如同工作表中的显示") {
                    this.CltPageSetup.PrintComments = "xlPrintInPlace";
                }
            }
            string str2 = this.cbb_PrintWrongCell.Text;
            if (str2 != null) {
                if (str2 == "显示值") {
                    this.CltPageSetup.PrintErrors = "xlPrintErrorsDisplayed";
                } else if (str2 == "<空白>") {
                    this.CltPageSetup.PrintErrors = "xlPrintErrorsBlank";
                } else if (str2 == "--") {
                    this.CltPageSetup.PrintErrors = "xlPrintErrorsDash";
                } else if (str2 == "#N/A") {
                    this.CltPageSetup.PrintErrors = "xlPrintErrorsNA";
                }
            }
            if (this.rdb_ListFirst.Checked) {
                this.CltPageSetup.Order = "xlDownThenOver";
            } else {
                this.CltPageSetup.Order = "xlOverThenDown";
            }
            this.CltPageSetup.ShrinkToFit = this.chk_ShrinkToFit.Checked;
        }
    }
}

