    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCIDPicker : UserControlPLM
    {
        private bool b_start;
        
        private string curInput;

        public event IDDropListHandler IDSelected;

        public UCIDPicker()
        {
            this.b_start = true;
            this.curInput = "";
            this.InitializeComponent();
        }

        public UCIDPicker(string str_in) : this()
        {
            this.curInput = str_in;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (this.IDSelected != null)
            {
                this.IDSelected("");
            }
            this.CloseParent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (this.IDSelected != null)
            {
                string strID = "";
                strID = this.GetIDValue();
                this.IDSelected(strID);
            }
            this.CloseParent();
        }

        private void CloseParent()
        {
            if (base.Parent != null)
            {
                if (base.Parent is PopupContainerControl)
                {
                    PopupContainerControl parent = base.Parent as PopupContainerControl;
                    if ((parent != null) && (parent.OwnerEdit != null))
                    {
                        parent.OwnerEdit.ClosePopup();
                    }
                }
                else
                {
                    base.Parent.Hide();
                }
            }
        }
 
        private string GetIDValue()
        {
            if (string.IsNullOrEmpty(this.txtEditID.Text))
            {
                return "";
            }
            if ((this.txtEditID.Text != null) && (this.txtEditID.Text.Length > 0x7d0))
            {
                MessageBoxPLM.Show("输入的代号数据超长，请重新修改后输入。", "代号超长", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return "";
            }
            string str = "";
            char[] separator = "\r\n".ToCharArray();
            string[] strArray = this.txtEditID.Text.Split(separator);
            for (int i = 0; i < strArray.Length; i++)
            {
                string str3 = strArray[i].ToString();
                if (!string.IsNullOrEmpty(str3))
                {
                    str = str + str3 + ",";
                }
            }
            if (str.EndsWith(","))
            {
                str = str.Substring(0, str.Length - 1);
            }
            return str;
        }


        public void ReLoad(string str_in)
        {
            this.curInput = str_in;
            this.SetIDValue(str_in);
        }

        private void SetIDValue(string str_in)
        {
            if (string.IsNullOrEmpty(str_in))
            {
                this.txtEditID.Text = "";
            }
            else if (str_in.Length > 0x7d0)
            {
                MessageBoxPLM.Show("输入的代号数据超长，请重新修改后输入。", "代号超长", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.txtEditID.Text = "";
            }
            else
            {
                string str = "";
                char[] separator = ",".ToCharArray();
                string[] strArray = str_in.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = strArray[i].ToString();
                    if (!string.IsNullOrEmpty(str3))
                    {
                        str = str + str3 + "\r\n";
                    }
                }
                this.txtEditID.Text = str;
            }
        }

        private void UCIDPicker_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }
    }
}

