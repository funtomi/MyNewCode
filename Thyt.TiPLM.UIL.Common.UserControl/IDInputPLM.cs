    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public class IDInputPLM : PopupContainerEditPLM
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private Container components;
        private string curInput = "";
        private IDDropListHandler dlhandler;
        private PopupContainerControl popupContainer;
        private UCIDPicker ucUser;

        public event IDDropListHandler DropListChanged;

        public event SelectIDHandler IDTextChanged;

        public IDInputPLM()
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

        private void IDCombo_QueryPopUp(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            base.Properties.PopupControl.Width = base.Width;
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
            base.Properties.TextEditStyle = TextEditStyles.Standard;
            this.popupContainer = new PopupContainerControl();
            base.Properties.PopupControl = this.popupContainer;
            this.NullText = "";
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCIDPicker(this.curInput);
            this.popupContainer.Controls.Add(this.ucUser);
            base.Properties.PopupControl.Size = new Size(base.Width, this.ucUser.Height);
            this.ucUser.Dock = DockStyle.Fill;
            this.dlhandler = new IDDropListHandler(this.ucUser_IDSelected);
            this.ucUser.IDSelected += this.dlhandler;
            this.QueryPopUp += new CancelEventHandler(this.IDCombo_QueryPopUp);
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
                this.ClosePopup();
            }
        }

        public string NullText
        {
            get {
                return
                base.Properties.NullText;
            }
            set
            {
                base.Properties.NullText = value;
            }
        }
    }
}

