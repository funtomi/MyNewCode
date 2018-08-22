    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public class IDInput : UltraTextEditor
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private Container components;
        private string curInput = "";
        private IDDropListHandler dlhandler;
        private UCIDPicker ucUser;

        public event IDDropListHandler DropListChanged;

        public event SelectIDHandler IDTextChanged;

        public IDInput()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.dlhandler != null)
            {
                this.ucUser.IDSelected -= this.dlhandler;
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetIDValue()
        {
            if (this.Text.Trim().Length == 0)
            {
                return "";
            }
            return this.curInput;
        }

        private void IDCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (base.Width > 0x113)
            {
                if (this.ucUser != null)
                {
                    this.ucUser.Width = base.Width;
                }
            }
            else if (this.ucUser != null)
            {
                this.ucUser.Width = 0x110;
            }
            if (this.ucUser != null)
            {
                this.ucUser.ReLoad(this.Text);
            }
            Cursor.Current = Cursors.Default;
        }

        private void IDCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.IDTextChanged != null)
            {
                this.IDTextChanged(this.curInput);
            }
        }

        private void IDComboInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.b_ReadOnly)
            {
                this.Text = this._locValue;
            }
        }

        private void InitializeComponent()
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectID") {
                RightAlignDropDown = DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "";
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCIDPicker(this.curInput);
            DropDownEditorButton button = base.ButtonsRight["SelectID"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new IDDropListHandler(this.ucUser_IDSelected);
            this.ucUser.IDSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.IDCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.IDCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.IDComboInput_KeyUp);
        }

        private void SetEditText(string str_in)
        {
            this._locValue = str_in;
            this.Text = str_in;
        }

        public void SetInput(string str_in)
        {
            this.curInput = str_in;
            this.SetEditText(str_in);
        }

        private void ucUser_IDSelected(string str_in)
        {
            bool flag = false;
            if ((base.Tag == null) && (str_in != this.curInput))
            {
                flag = true;
            }
            this.curInput = str_in;
            this.SetEditText(str_in);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(str_in);
                base.CloseEditorButtonDropDowns();
            }
        }
    }
}

