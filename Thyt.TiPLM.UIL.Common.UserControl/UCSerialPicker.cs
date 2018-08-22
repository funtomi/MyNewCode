using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCSerialPicker : UserControlPLM {
        private bool b_start;
        private DateTime dt_click;
        
        private string str_input;

        public event SerialDropListHandler SerialSelected;

        public UCSerialPicker() {
            this.b_start = true;
            this.str_input = "";
            this.dt_click = DateTime.Now;
            this.InitializeComponent();
        }

        public UCSerialPicker(string str_in)
            : this() {
            this.str_input = str_in;
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            if (this.SerialSelected != null) {
                string strSerial = "";
                strSerial = this.GetSerialValue();
                this.SerialSelected(strSerial);
            }
            base.Parent.Hide();
        }

        private string GetSerialValue() {
            return ("D" + this.numCodePosition.Value.ToString() + "S" + this.numStart.Value.ToString() + "P" + this.numSplit.Value.ToString());
        }

        public void ReLoad(string str_in) {
            this.str_input = str_in;
            this.SetSerialValue(str_in);
        }

        private void SetSerialValue(string str_def) {
            if (!string.IsNullOrEmpty(str_def) && ((str_def.Length > 0) && str_def.StartsWith("D"))) {
                string str = str_def.Substring(0, str_def.IndexOf("S"));
                this.numCodePosition.Value = Convert.ToInt32(str.Substring(1, str.Length - 1));
                string str2 = str_def.Substring(str.Length, str_def.Length - str.Length);
                if (str2.StartsWith("S")) {
                    string str3 = str2.Substring(0, str2.IndexOf("P"));
                    this.numStart.Value = Convert.ToInt32(str3.Substring(1, str3.Length - 1));
                    string str4 = str2.Substring(str3.Length, str2.Length - str3.Length);
                    if (str4.StartsWith("P")) {
                        this.numSplit.Value = Convert.ToInt32(str4.Substring(1, str4.Length - 1));
                    }
                }
            }
        }

        private void UCTimePicker_Enter(object sender, EventArgs e) {
            this.b_start = false;
        }
    }
}

