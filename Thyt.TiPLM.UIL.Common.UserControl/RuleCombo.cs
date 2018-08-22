
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class RuleCombo : UltraTextEditor
    {
        private object _o_input;
        private RuleDropListHandler dlhandler;
        private string str_funcname;
        private string str_funcvalue;
        private UCParamMain ucUser;
        private Guid userOid;

        public event RuleDropListHandler DropListChanged;

        public event SelectRuleHandler ResTextChanged;

        public RuleCombo()
        {
            this.str_funcvalue = "";
            this.str_funcname = "";
            this.InitializeComponent();
            this.userOid = ClientData.LogonUser.Oid;
        }

        public RuleCombo(string _str_funcname) : this()
        {
            this.str_funcname = _str_funcname;
            this.InitializeConfig();
        }

  

        private void InitializeConfig()
        {
            this.ucUser = new UCParamMain(this.str_funcname);
            DropDownEditorButton button = base.ButtonsRight["SelectRule"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new RuleDropListHandler(this.ucUser_RuleSelected);
            this.ucUser.RuleSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.RuleCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.KeyUp += new KeyEventHandler(this.RuleCombo_KeyUp);
            base.Resize += new EventHandler(this.RuleCombo_Resize);
        }

        public void ReLoad(string _str_funcname)
        {
            if (this.ucUser != null)
            {
                this.str_funcname = _str_funcname;
                this.ucUser.ReLoad(_str_funcname);
            }
        }

        private void RuleCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this.str_funcname);
            this.ucUser.SetInput(this._o_input);
            Cursor.Current = Cursors.Default;
        }

        private void RuleCombo_KeyUp(object obj, KeyEventArgs kpeargs)
        {
            this.Text = SchFunc.GetFuncLabel(this.str_funcvalue, ClientData.LogonUser.Oid);
        }

        private void RuleCombo_Resize(object sender, EventArgs e)
        {
            if (this.ucUser.Width < base.Width)
            {
                this.ucUser.Width = base.Width;
            }
        }

        private void SetEditText(string str_funcvalue)
        {
            this.Text = SchFunc.GetFuncLabel(str_funcvalue, ClientData.LogonUser.Oid);
            if (((this._o_input != null) && (this._o_input is ArrayList)) && ((this._o_input as ArrayList).Count >= 2))
            {
                (this._o_input as ArrayList)[1] = str_funcvalue;
            }
        }

        public void SetInput(object o_in)
        {
            if (this.ucUser != null)
            {
                if (o_in is ArrayList)
                {
                    this.Text = SchFunc.GetFuncLabel((o_in as ArrayList)[1].ToString(), ClientData.LogonUser.Oid);
                    this.str_funcvalue = (o_in as ArrayList)[1].ToString();
                    this._o_input = o_in;
                }
                this.ucUser.SetInput(o_in);
            }
        }

        private void ucUser_RuleSelected(string str_in)
        {
            bool flag = false;
            if ((base.Tag == null) && (str_in != null))
            {
                flag = true;
            }
            this.str_funcvalue = str_in;
            this.SetEditText(str_in);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(str_in);
                base.CloseEditorButtonDropDowns();
            }
        }

        public string FunctionValue
        {
            get {
                return
                this.str_funcvalue;
            }
            set
            {
                this.str_funcvalue = value;
            }
        }
    }
}

