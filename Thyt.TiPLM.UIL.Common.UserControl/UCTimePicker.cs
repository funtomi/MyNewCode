using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCTimePicker : UserControlPLM {
        private bool b_start;

        private DateTime dt_click;
        private DateTime dt_input;
        public event TimeDropListHandler TimeSelected;

        public UCTimePicker() {
            this.b_start = true;
            this.dt_input = DateTime.Now;
            this.dt_click = DateTime.Now;
            this.InitializeComponent();
        }

        public UCTimePicker(DateTime dt_in)
            : this() {
            this.dt_input = dt_in;
        }

        private void btn_clear_Click(object sender, EventArgs e) {
            if (this.TimeSelected != null) {
                DateTime minValue = DateTime.MinValue;
                this.TimeSelected(minValue);
            }
            this.CloseParent();
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            if (this.TimeSelected != null) {
                DateTime now = DateTime.Now;
                now = this.GetTimeValue();
                this.TimeSelected(now);
            }
            this.CloseParent();
        }

        private void btn_today_Click(object sender, EventArgs e) {
            DateTime now = DateTime.Now;
            int minute = now.Minute;
            int second = now.Second;
            if (minute == 0) {
                minute++;
            } else {
                minute--;
            }
            if (second == 0) {
                second++;
            } else {
                second--;
            }
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, minute, second);
            this.TimeSelected(now);
            this.CloseParent();
        }

        private void CloseParent() {
            if (base.Parent != null) {
                if (base.Parent is PopupContainerControl) {
                    PopupContainerControl parent = base.Parent as PopupContainerControl;
                    if ((parent != null) && (parent.OwnerEdit != null)) {
                        parent.OwnerEdit.ClosePopup();
                    }
                } else {
                    base.Parent.Hide();
                }
            }
        }

        public DateTime GetTimeValue() {
            DateTime now = DateTime.Now;
            return new DateTime(this.mon_ymd.SelectionEnd.Year, this.mon_ymd.SelectionEnd.Month, this.mon_ymd.SelectionEnd.Day, Convert.ToInt32(this.num_hh.Value), Convert.ToInt32(this.num_mm.Value), Convert.ToInt32(this.num_ss.Value));
        }

        private void mon_ymd_DateChanged(object sender, DateRangeEventArgs e) {
            if (!this.b_start && (this.TimeSelected != null)) {
                DateTime now = DateTime.Now;
                now = this.GetTimeValue();
                this.TimeSelected(now);
            }
        }

        private void mon_ymd_MouseUp(object sender, MouseEventArgs e) {
            if ((e.Button == MouseButtons.Left) && (e.Y >= 20)) {
                int millisecond = DateTime.Now.Millisecond;
                int num2 = this.dt_click.Millisecond;
                if ((this.dt_click.ToString("yyyy/MM/dd HH:mm:ss") == DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")) && (millisecond != num2)) {
                    if (this.TimeSelected != null) {
                        DateTime now = DateTime.Now;
                        now = this.GetTimeValue();
                        this.TimeSelected(now);
                    }
                    this.CloseParent();
                } else {
                    this.dt_click = DateTime.Now;
                }
            }
        }

        private void num_hh_ValueChanged(object sender, EventArgs e) {
            if (!this.b_start && (this.TimeSelected != null)) {
                DateTime now = DateTime.Now;
                now = this.GetTimeValue();
                this.TimeSelected(now);
            }
        }

        private void num_mm_ValueChanged(object sender, EventArgs e) {
            if (!this.b_start && (this.TimeSelected != null)) {
                DateTime now = DateTime.Now;
                now = this.GetTimeValue();
                this.TimeSelected(now);
            }
        }

        private void num_ss_ValueChanged(object sender, EventArgs e) {
            if (!this.b_start && (this.TimeSelected != null)) {
                DateTime now = DateTime.Now;
                now = this.GetTimeValue();
                this.TimeSelected(now);
            }
        }

        public void ReLoad(DateTime dt_in) {
            this.dt_input = dt_in;
            this.SetTimeValue(dt_in);
        }

        public void SetHide(string conType) {
            string str = conType;
            if (str != null) {
                if (str != "Hour") {
                    if (str != "Minute") {
                        if (str == "Second") {
                            this.label3.Visible = false;
                            this.num_ss.Visible = false;
                            this.num_ss.Value = 0M;
                        }
                        return;
                    }
                } else {
                    this.label1.Visible = false;
                    this.num_hh.Visible = false;
                    this.num_hh.Value = 0M;
                    return;
                }
                this.label2.Visible = false;
                this.num_mm.Visible = false;
                this.num_mm.Value = 0M;
            }
        }

        public void SetMaxDateTime(DateTime dt_in) {
            this.mon_ymd.MaxDate = dt_in;
        }

        public void SetMinDateTime(DateTime dt_in) {
            this.mon_ymd.MinDate = dt_in;
        }

        private void SetTimeValue(DateTime dt_in) {
            if (dt_in == DateTime.MinValue) {
                this.mon_ymd.SetDate(DateTime.Now);
            } else {
                this.mon_ymd.SetDate(dt_in);
            }
            if (this.TimeCompareInBound(dt_in, DateTime.Now)) {
                this.num_hh.Value = 0M;
                this.num_mm.Value = 0M;
                this.num_ss.Value = 0M;
            } else {
                this.num_hh.Value = Convert.ToDecimal(dt_in.Hour);
                this.num_mm.Value = Convert.ToDecimal(dt_in.Minute);
                this.num_ss.Value = Convert.ToDecimal(dt_in.Second);
            }
        }

        private bool TimeCompareInBound(DateTime dt_one, DateTime dt_two) {
            bool flag = false;
            TimeSpan span = (TimeSpan)(dt_one - dt_two);
            return (((span.TotalSeconds >= -5.0) && (span.TotalSeconds <= 5.0)) || flag);
        }

        private void UCTimePicker_Enter(object sender, EventArgs e) {
            this.b_start = false;
        }
    }
}

