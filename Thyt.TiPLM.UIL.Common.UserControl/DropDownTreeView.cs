    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public class DropDownTreeView : UltraTextEditor {
        private bool _dropdown = true;
        private ImageList _imagelist;
        private bool _IsAollowUseParentNode;
        private string _locValue = "";
        private TreeNode _SelectedNode;
        private bool _TextReadOnly;
        private TreeView _treeview;
        private bool b_ReadOnly;
        private Container components;
        private TreeViewDropListHandler dlhandler;
        private TreeNode tn_input;
        private UCTreeViewPicker ucUser;

        public event TreeViewDropListHandler DropListChanged;

        public event NewAfterNodeSelected TreeNodeSelect;

        public event SelectTreeViewHandler TreeViewTextChanged;

        public DropDownTreeView() {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        protected override void Dispose(bool disposing) {
            if (this.dlhandler != null) {
                this.ucUser.TreeViewSelected -= this.dlhandler;
            }
            if (disposing && (this.components != null)) {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            DropDownEditorButton button = new DropDownEditorButton("SelectSerial") {
                RightAlignDropDown = DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig() {
            this.ucUser = new UCTreeViewPicker(this._treeview);
            DropDownEditorButton button = base.ButtonsRight["SelectTreeView"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new TreeViewDropListHandler(this.ucUser_TreeViewSelected);
            this.ucUser.TreeViewSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.TreeViewCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.TreeViewCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.SerialComboInput_KeyUp);
        }

        private void SerialComboInput_KeyUp(object sender, KeyEventArgs e) {
            if (this.b_ReadOnly) {
                this.Text = this._locValue;
            }
        }

        private void SetEditText(TreeNode tn_in) {
            this._SelectedNode = tn_in;
            this.Text = tn_in.Text;
        }

        public void SetInput(TreeNode tn_in) {
            this._SelectedNode = tn_in;
            this.SetEditText(tn_in);
        }

        private void TreeViewCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e) {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this._treeview);
            Cursor.Current = Cursors.Default;
        }

        private void TreeViewCombo_TextChanged(object sender, EventArgs e) {
            if (this.TreeViewTextChanged != null) {
                this.TreeViewTextChanged(this.tn_input);
            }
        }

        private void ucUser_TreeViewSelected(TreeNode tn_in) {
            bool flag = false;
            if ((base.Tag == null) && (tn_in != this.tn_input)) {
                flag = true;
            }
            this.tn_input = tn_in;
            this.SetEditText(tn_in);
            if (flag && (this.DropListChanged != null)) {
                this.DropListChanged(tn_in);
                base.CloseEditorButtonDropDowns();
            }
        }

        public bool DropDown {
            get {
                return
                this._dropdown;
            }
            set {
                this._dropdown = value;
            }
        }

        public ImageList Imagelist {
            get {
                return
                this._imagelist;
            }
            set {
                this._imagelist = value;
            }
        }

        public bool IsAollowUseParentNode {
            get {
                return
                this._IsAollowUseParentNode;
            }
            set {
                this._IsAollowUseParentNode = value;
            }
        }

        public TreeNode SelectedNode {
            get {
                return
                    this._SelectedNode;
            }
            set {
                this._SelectedNode = value;
            }
        }

        public bool TextReadOnly {
            get {
                return
                    this._TextReadOnly;
            }
            set {
                this._TextReadOnly = value;
            }
        }

        public string TextValue {
            get {
                return
                    this.Text;
            }
            set {
                this.Text = value;
            }
        }

        public TreeView treeview {
            get {
                return
                    this._treeview;
            }
            set {
                this._treeview = value;
            }
        }
    }
}

