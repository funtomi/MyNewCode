using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Project2;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCSelectPrinPLM : PopupContainerEditPLM {
        private SelectPrinHandler handler;
        private IBizItem iBizItem;
        private bool inputAllow;
        private IBizItem iPBizItem;
        private bool isOnlyShowUser;
        private PopupContainerControl popupContainer;
        public bool showSysAdmin;
        private UCOrgs ucOrg;
        private UCRoles ucRole;
        private UCUsers ucUser;
        private SelectPrinWay way;

        public event SelectPrinHandler SelectPrinChanged;

        public UCSelectPrinPLM() {
            this.showSysAdmin = true;
            this.InitializeComponent();
            base.Properties.TextEditStyle = TextEditStyles.Standard;
            this.popupContainer = new PopupContainerControl();
            base.Properties.PopupControl = this.popupContainer;
            base.KeyDown += new KeyEventHandler(this.UCSelectPrin_KeyDown);
        }

        public UCSelectPrinPLM(IContainer container)
            : this() {
            container.Add(this);
        }

        public UCSelectPrinPLM(SelectPrinWay way)
            : this() {
            this.way = way;
            switch (way) {
                case SelectPrinWay.SelectUser:
                    this.ucUser = new UCUsers();
                    this.ucUser.showSysAdmin = this.showSysAdmin;
                    this.popupContainer.Controls.Add(this.ucUser);
                    base.Properties.PopupControl.Size = new Size(this.ucUser.Width, this.ucUser.Height);
                    this.ucUser.Dock = DockStyle.Fill;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucUser.PrinSelected += this.handler;
                    return;

                case SelectPrinWay.SelectOrg:
                    this.ucOrg = new UCOrgs();
                    this.popupContainer.Controls.Add(this.ucOrg);
                    base.Properties.PopupControl.Size = new Size(this.ucOrg.Width, this.ucOrg.Height);
                    this.ucOrg.Dock = DockStyle.Fill;
                    this.ucOrg.LoadOrgs();
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucOrg.PrinSelected += this.handler;
                    return;

                case SelectPrinWay.SelectRole:
                    this.ucRole = new UCRoles();
                    this.ucRole.LoadRoles();
                    this.ucRole.RoleRefresh(null, null);
                    this.popupContainer.Controls.Add(this.ucRole);
                    base.Properties.PopupControl.Size = new Size(this.ucRole.Width, this.ucRole.Height);
                    this.ucRole.Dock = DockStyle.Fill;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    return;

                case SelectPrinWay.SelectPrjUser:
                    this.ucRole = new UCRoles();
                    this.popupContainer.Controls.Add(this.ucRole);
                    base.Properties.PopupControl.Size = new Size(this.ucRole.Width, this.ucRole.Height);
                    this.ucRole.Dock = DockStyle.Fill;
                    this.QueryPopUp += new CancelEventHandler(this.UCSelectPrin_QueryPopUp);
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    this.ucRole.Height = 0;
                    this.ucRole.Width = base.Width;
                    return;

                case SelectPrinWay.SelectPrjOrg:
                    this.ucRole = new UCRoles();
                    this.popupContainer.Controls.Add(this.ucRole);
                    base.Properties.PopupControl.Size = new Size(this.ucRole.Width, this.ucRole.Height);
                    this.ucRole.Dock = DockStyle.Fill;
                    this.QueryPopUp += new CancelEventHandler(this.UCSelectPrin_QueryPopUp);
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    this.ucRole.Height = 0;
                    this.ucRole.Width = base.Width;
                    return;
            }
        }

        private void DoPrjOrg() {
            object[] objArray = new object[] { this.iBizItem, this.iPBizItem, this };
            if (PLProject.Instance.ProjDelegateInstance != null) {
                object obj2 = PLProject.Instance.ProjDelegateInstance(ProjDelegeteType.ProjOrgCollect, objArray);
                if (obj2 is DEPrincipal) {
                    this.Text = (obj2 as DEPrincipal).Name;
                    base.Tag = obj2;
                    if (this.SelectPrinChanged != null) {
                        this.SelectPrinChanged(obj2 as DEPrincipal);
                    }
                }
            }
        }

        private void DoPrjUser() {
            object[] objArray = new object[] { this.iBizItem, this.iPBizItem, this };
            if (PLProject.Instance.ProjDelegateInstance != null) {
                object obj2 = PLProject.Instance.ProjDelegateInstance(ProjDelegeteType.ProjUserCollect, objArray);
                if (obj2 is DEPrincipal) {
                    this.Text = (obj2 as DEPrincipal).Name;
                    base.Tag = obj2;
                    if (this.SelectPrinChanged != null) {
                        this.SelectPrinChanged(obj2 as DEPrincipal);
                    }
                }
            }
        }

        public void SetBizItem(IBizItem item) {
            this.iBizItem = item;
        }

        public void SetParentItem(IBizItem pitem) {
            this.iPBizItem = pitem;
        }

        private void ucControl_PrinSelected(DEPrincipal prin) {
            bool flag = false;
            if (base.Tag == null) {
                if (prin != null) {
                    flag = true;
                }
            } else if (((DEPrincipal)base.Tag).Oid != prin.Oid) {
                flag = true;
            }
            if (prin != null) {
                if (prin is DEUser) {
                    DEUser user = prin as DEUser;
                    if (this.isOnlyShowUser) {
                        this.Text = user.Name;
                    } else {
                        this.Text = user.Name + "(" + user.LogId + ")";
                    }
                } else {
                    this.Text = prin.Name;
                }
                base.Tag = prin;
            }
            if (flag && (this.SelectPrinChanged != null)) {
                this.SelectPrinChanged(prin);
            }
        }

        private void UCSelectPrin_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                this.Text = "";
                base.Tag = null;
                if (this.SelectPrinChanged != null) {
                    this.SelectPrinChanged(null);
                }
            }
        }

        private void UCSelectPrin_QueryPopUp(object sender, CancelEventArgs e) {
            if (this.way == SelectPrinWay.SelectPrjOrg) {
                this.DoPrjOrg();
            }
            if (this.way == SelectPrinWay.SelectPrjUser) {
                this.DoPrjUser();
            }
            base.Properties.PopupSizeable = false;
            base.Properties.ShowPopupCloseButton = false;
            base.Properties.ShowPopupShadow = false;
            base.Properties.PopupFormMinSize = new Size(10, 0);
            base.Properties.PopupControl.Width = 10;
            base.Properties.PopupControl.Height = 10;
        }

        public bool AcceptsTab { get; set; }

        public bool InputAllow {
            get {
                return this.inputAllow;
            }
            set {
                this.inputAllow = value;
                this.ReadOnly = !value;
            }
        }

        public bool OnlyShowUserName {
            set {
                this.isOnlyShowUser = value;
            }
        }

        public bool ReadOnly {
            get {
                if (base.Properties.TextEditStyle == TextEditStyles.Standard) {
                    return false;
                }
                return true;
            }
            set {
                if (value) {
                    base.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                } else {
                    base.Properties.TextEditStyle = TextEditStyles.Standard;
                }
            }
        }

        public bool SetshowSysAdmin {
            set {
                this.ucUser.showSysAdmin = value;
            }
        }

        public SelectPrinWay Way {
            get {
                return this.way;
            }
        }
        public enum SelectPrinWay {
            SelectUser,
            SelectOrg,
            SelectRole,
            SelectPrjUser,
            SelectPrjOrg
        }
    }
}

