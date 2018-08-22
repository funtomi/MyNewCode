
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public class SerialInput : UltraTextEditor
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private Container components;
        private SerialDropListHandler dlhandler;
        private string str_input = "";
        private UCSerialPicker ucUser;

        public event SerialDropListHandler DropListChanged;

        public event SelectSerialHandler SerialTextChanged;

        public SerialInput()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.dlhandler != null)
            {
                this.ucUser.SerialSelected -= this.dlhandler;
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetSerialValue()
        {
            if (this.Text.Trim().Length == 0)
            {
                return "";
            }
            return this.str_input;
        }

        private void InitializeComponent()
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectSerial") {
                RightAlignDropDown = DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCSerialPicker(this.str_input);
            DropDownEditorButton button = base.ButtonsRight["SelectSerial"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new SerialDropListHandler(this.ucUser_SerialSelected);
            this.ucUser.SerialSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.SerialCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.SerialCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.SerialComboInput_KeyUp);
        }

        private void SerialCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this.str_input);
            Cursor.Current = Cursors.Default;
        }

        private void SerialCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.SerialTextChanged != null)
            {
                this.SerialTextChanged(this.str_input);
            }
        }

        private void SerialComboInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.b_ReadOnly)
            {
                this.Text = this._locValue;
            }
        }

        private void SetEditText(string str_in)
        {
            this._locValue = str_in;
            this.Text = str_in;
        }

        public void SetInput(string str_in)
        {
            this.str_input = str_in;
            this.SetEditText(str_in);
        }

        private void ucUser_SerialSelected(string str_in)
        {
            bool flag = false;
            if ((base.Tag == null) && (str_in != this.str_input))
            {
                flag = true;
            }
            this.str_input = str_in;
            this.SetEditText(str_in);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(str_in);
                base.CloseEditorButtonDropDowns();
            }
        }
    }
}

