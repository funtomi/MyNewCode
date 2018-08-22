using AxOWC;
using OWC;
using System;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgChar : Form {
        public PLMSimpleDelegate D_InsertChar;

        public DlgChar() {
            this.InitializeComponent();
            this.axSpreadsheet1.Select();
            this.axSpreadsheet1.LostFocus += new EventHandler(this.axSpreadsheet1_LostFocus);
        }

        private void axSpreadsheet1_DblClick(object sender, IWebCalcEventSink_DblClickEvent e) {
            Clipboard.SetDataObject(e.eventInfo.Range.Text.ToString());
            if (this.D_InsertChar != null) {
                this.D_InsertChar(e.eventInfo.Range.Text.ToString());
            }
        }

        private void axSpreadsheet1_LostFocus(object sender, EventArgs e) {
            base.Visible = false;
        }

        private void DlgChar_Load(object sender, EventArgs e) {
            string text = new StreamReader(Application.StartupPath + @"\specific_chars.txt", Encoding.Default).ReadToEnd();
            this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(1, 1).ParseText(text, "\t", false, "");
            this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(1, 1).Select();
            this.axSpreadsheet1.ActiveSheet.ViewableRange = "A1:Z30";
            //this.GetRange("A1", "Z30").RowHeight = 20;
            // this.GetRange("A1", "Z30").ColumnWidth=40;
            this.GetRange("A1", "Z30").set_ColumnWidth(40);
            this.GetRange("A1", "Z30").set_RowHeight(20);
            this.axSpreadsheet1.ActiveSheet.Protection.AllowDeletingColumns = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowDeletingRows = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowFiltering = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowInsertingColumns = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowInsertingRows = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowSizingAllColumns = false;
            this.axSpreadsheet1.ActiveSheet.Protection.AllowSizingAllRows = false;
            this.axSpreadsheet1.AllowAboutDialog = false;
            this.axSpreadsheet1.EnableAutoCalculate = false;
            this.axSpreadsheet1.ActiveSheet.Protection.Enabled = true;
            this.axSpreadsheet1.ActiveSheet.UsedRange.Cells.set_Locked(true);
//            this.axSpreadsheet1.ActiveSheet.UsedRange.Cells.Locked = 1;
        }

        private Range GetRange(string address) {
            if (address.IndexOf(":") < 0) {
                return this.GetRange(PPCConvert.Address2Row(address), PPCConvert.Address2Col(address));
            }
            return this.GetRange(address.Substring(0, address.IndexOf(":")), address.Substring(address.IndexOf(":") + 1));
        }

        private Range GetRange(int row, int col) {
            return this.axSpreadsheet1.ActiveSheet.UsedRange.get_Item(row, col);
        }
        private Range GetRange(string firstCell, string lastCell) {
            if (firstCell == lastCell) {
                return this.GetRange(firstCell).MergeArea;
            }
            return this.axSpreadsheet1.ActiveSheet.UsedRange.get_Range(this.GetRange(firstCell), this.GetRange(lastCell));
        }


    }
}

