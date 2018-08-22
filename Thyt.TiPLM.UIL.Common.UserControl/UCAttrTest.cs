    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCAttrTest : UserControlPLM, IUCAgent
    {

        public UCAttrTest()
        {
            this.InitializeComponent();
        }
         
        public string GetValue()
        {
            string str = "false";
            string str2 = "AttrTest(";
            if (this.chkBox_Test.Checked)
            {
                str = "true";
            }
            return (str2 + str + ")");
        }


        public string ParseValue(string str_funcvalue) {
            return "wyl";
        }
        public void SetInput(object o_in)
        {
            if (((o_in != null) && (o_in is ArrayList)) && ((o_in as ArrayList).Count >= 2))
            {
                string str = (o_in as ArrayList)[1].ToString();
                if (str.StartsWith("AttrTest(") && str.EndsWith(")"))
                {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in)
        {
            string str = str_in.Substring("AttrTest".Length + 1, (str_in.Length - "AttrTest".Length) - 2);
            if (((str != null) && (str.Trim().Length > 0)) && (str == "true"))
            {
                this.chkBox_Test.Checked = true;
            }
        }
    }
}

