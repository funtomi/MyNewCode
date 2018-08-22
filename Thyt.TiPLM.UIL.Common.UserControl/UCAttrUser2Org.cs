    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCAttrUser2Org : UserControlPLM, IUCAgent
    {

        public UCAttrUser2Org()
        {
            this.InitializeComponent();
        }
         
        public string GetValue()
        {
            string str = "=";
            string str2 = "AttrUser2Org(";
            if (this.cb_operator.Text == "包含")
            {
                str = ">";
            }
            if (this.cb_operator.Text == "包含或等同")
            {
                str = ">=";
            }
            if (this.cb_operator.Text == "属于")
            {
                str = "<";
            }
            if (this.cb_operator.Text == "属于或等同")
            {
                str = "<=";
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
                string str2 = "AttrUser2Org(";
                if (str.StartsWith(str2) && str.EndsWith(")"))
                {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in)
        {
            string str = "AttrUser2Org";
            string str2 = str_in.Substring(str.Length + 1, (str_in.Length - str.Length) - 2);
            if ((str2 != null) && (str2.Trim().Length > 0))
            {
                if (str2.Trim() == "=")
                {
                    this.cb_operator.Text = "等同";
                }
                if (str2.Trim() == ">")
                {
                    this.cb_operator.Text = "包含";
                }
                if (str2.Trim() == "<")
                {
                    this.cb_operator.Text = "属于";
                }
                if (str2.Trim() == ">=")
                {
                    this.cb_operator.Text = "包含或等同";
                }
                if (str2.Trim() == "<=")
                {
                    this.cb_operator.Text = "属于或等同";
                }
            }
        }
    }
}

