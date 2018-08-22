using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCTreeViewPicker : UserControlPLM {
        private bool b_start;

        public event TreeViewDropListHandler TreeViewSelected;

        public UCTreeViewPicker() {
            this.b_start = true;
            this.InitializeComponent();
        }

        public UCTreeViewPicker(TreeView tv_in)
            : this() {
            this.tvInput = tv_in;
        }

        public void ReLoad(TreeView tv_in) {
            this.tvInput = tv_in;
            this.SetTreeViewValue(tv_in);
        }

        private void SetTreeViewValue(TreeView tv_in) {
        }

        private void tvInput_AfterSelect(object sender, TreeViewEventArgs e) {
            if (this.TreeViewSelected != null) {
                this.TreeViewSelected(this.tvInput.SelectedNode);
            }
        }

        private void tvInput_DoubleClick(object sender, EventArgs e) {
            if (this.TreeViewSelected != null) {
                this.TreeViewSelected(this.tvInput.SelectedNode);
            }
            base.Parent.Hide();
        }

        private void UCTimePicker_Enter(object sender, EventArgs e) {
            this.b_start = false;
        }
    }
}

