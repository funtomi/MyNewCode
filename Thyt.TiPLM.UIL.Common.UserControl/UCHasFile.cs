using DevExpress.XtraEditors.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCHasFile : UserControlPLM, IUCAgent {
        private ArrayList al_values = new ArrayList();

        public UCHasFile() {
            this.InitializeComponent();
        }

        public string GetValue() {
            string str = "false";
            string str2 = "HasFile(";
            if (this.chkBox_hasFile.Checked) {
                str = "true";
                if (this.numUpDown.Value == 0M) {
                    this.numUpDown.Value = 1M;
                }
            } else {
                this.numUpDown.Value = 0M;
            }
            object obj2 = str2;
            return string.Concat(new object[] { obj2, str, ",", this.numUpDown.Value, ")" });
        }

        public string ParseValue(string str_funcvalue) {
            return "wyl";
        }
        public void SetInput(object o_in) {
            if (((o_in != null) && (o_in is ArrayList)) && ((o_in as ArrayList).Count >= 2)) {
                string str = (o_in as ArrayList)[1].ToString();
                if (str.StartsWith("HasFile(") && str.EndsWith(")")) {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in) {
            string str = str_in.Substring("HasFile".Length + 1, (str_in.Length - "HasFile".Length) - 2);
            if ((str != null) && (str.Trim().Length > 0)) {
                char[] separator = ",".ToCharArray();
                string[] strArray = str.Split(separator);
                for (int i = 0; i < strArray.Length; i++) {
                    string str3 = strArray[i].ToString();
                    if ((i == 0) && (str3 == "true")) {
                        this.chkBox_hasFile.Checked = true;
                    }
                    if (i == 1) {
                        this.numUpDown.Value = Convert.ToInt32(str3);
                    }
                }
            }
        }
    }
}

