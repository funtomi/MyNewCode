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
    public partial class TimeComboInputPLM : PopupContainerEditPLM
    {
        private string _locValue = "";
        private bool b_ReadOnly;
        private TimeDropListHandler dlhandler;
        private DateTime dt_input = DateTime.Now;
        private DateTimePicker picker;
        private string str_styleFormat = "yyyy年MM月dd日";
        private UCTimePicker ucUser;

        public event TimeDropListHandler DropListChanged;

        public event SelectTimeHandler TimeTextChanged;

        public TimeComboInputPLM()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        public DateTime GetDateTimeValue()
        {
            if (this.Text.Trim().Length == 0)
            {
                return DateTime.Now;
            }
            return this.dt_input;
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCTimePicker(this.dt_input);
            this.popupContainer.Controls.Add(this.ucUser);
            base.Properties.PopupControl.Size = new Size(this.ucUser.Width, this.ucUser.Height);
            this.ucUser.Dock = DockStyle.Fill;
            this.dlhandler = new TimeDropListHandler(this.ucUser_TimeSelected);
            this.ucUser.TimeSelected += this.dlhandler;
            this.QueryPopUp += new CancelEventHandler(this.TimeCombo_QueryPopUp);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.TimeCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.TimeComboInput_KeyUp);
        }

        private void SetEditText(DateTime dt_in)
        {
            string text = "";
            try
            {
                this.picker.Value = dt_in;
                text = this.picker.Text;
            }
            catch
            {
                text = "";
            }
            this._locValue = text;
            this.Text = text;
        }

        public void SetInput(DateTime dt_in)
        {
            this.dt_input = dt_in;
            this.SetEditText(dt_in);
        }

        public void SetMaxDateTime(DateTime dt_in)
        {
            if (this.ucUser != null)
            {
                this.ucUser.SetMaxDateTime(dt_in);
            }
        }

        public void SetMinDateTime(DateTime dt_in)
        {
            if (this.ucUser != null)
            {
                this.ucUser.SetMinDateTime(dt_in);
            }
        }

        public void SetStyle(string conFormat)
        {
            if (this.picker == null)
            {
                this.picker = new DateTimePicker();
            }
            this.picker.Format = DateTimePickerFormat.Custom;
            if (conFormat == "")
            {
                conFormat = this.str_styleFormat;
            }
            this.picker.CustomFormat = conFormat;
            if (!base.Controls.Contains(this.picker))
            {
                base.Controls.Add(this.picker);
            }
            this.picker.Visible = false;
            this.picker.Show();
            this.picker.Hide();
            if ((conFormat != null) && (conFormat.Trim().Length != 0))
            {
                this.str_styleFormat = conFormat;
                this.str_styleFormat = this.str_styleFormat.Replace("D", "d");
                this.str_styleFormat = this.str_styleFormat.Replace("Y", "y");
                this.str_styleFormat = this.str_styleFormat.Replace("S", "s");
                this.str_styleFormat = this.str_styleFormat.Replace("h", "H");
            }
        }

        private void TimeCombo_QueryPopUp(object sender, CancelEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this.dt_input);
            if ((this.str_styleFormat.IndexOf("H") < 0) && (this.str_styleFormat.IndexOf("H") < 0))
            {
                this.ucUser.SetHide("Hour");
            }
            if (this.str_styleFormat.IndexOf("m") < 0)
            {
                this.ucUser.SetHide("Minute");
            }
            if ((this.str_styleFormat.IndexOf("s") < 0) && (this.str_styleFormat.IndexOf("s") < 0))
            {
                this.ucUser.SetHide("Second");
            }
            Cursor.Current = Cursors.Default;
        }

        private void TimeCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.TimeTextChanged != null)
            {
                this.TimeTextChanged(this.dt_input);
            }
        }

        private void TimeComboInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.b_ReadOnly)
            {
                this.Text = this._locValue;
            }
        }

        private void ucUser_TimeSelected(DateTime dt_in)
        {
            bool flag = false;
            if ((base.Tag == null) && (dt_in != this.dt_input))
            {
                flag = true;
            }
            this.dt_input = dt_in;
            this.SetEditText(dt_in);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(dt_in);
                this.ClosePopup();
            }
        }

        public string NullText
        {
            get{return
                base.Properties.NullText;
            }set
            {
                base.Properties.NullText = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                if (base.Properties.TextEditStyle == TextEditStyles.Standard)
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value)
                {
                    base.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                }
                else
                {
                    base.Properties.TextEditStyle = TextEditStyles.Standard;
                }
            }
        }
    }
}

