    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Project2;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCSelectPrin : UltraTextEditor
    {
        private SelectPrinHandler handler;
        private IBizItem iBizItem;
        private bool inputAllow;
        private IBizItem iPBizItem;
        private bool isOnlyShowUser;
        public bool showSysAdmin;
        private UCOrgs ucOrg;
        private UCRoles ucRole;
        private UCUsers ucUser;
        private SelectPrinWay way;

        public event SelectPrinHandler SelectPrinChanged;

        public UCSelectPrin()
        {
            this.showSysAdmin = true;
            this.InitializeComponent();
            base.AfterEnterEditMode += new EventHandler(this.UCSelectPrin_AfterEnterEditMode);
            base.AfterExitEditMode += new EventHandler(this.UCSelectPrin_AfterExitEditMode);
            base.KeyDown += new KeyEventHandler(this.UCSelectPrin_KeyDown);
        }

        public UCSelectPrin(IContainer container) : this()
        {
            container.Add(this);
        }

        public UCSelectPrin(SelectPrinWay way) : this()
        {
            this.way = way;
            switch (way)
            {
                case SelectPrinWay.SelectUser:
                {
                    this.ucUser = new UCUsers();
                    this.ucUser.showSysAdmin = this.showSysAdmin;
                    DropDownEditorButton button = base.ButtonsRight["SelectUser"] as DropDownEditorButton;
                    button.Control = this.ucUser;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucUser.PrinSelected += this.handler;
                    return;
                }
                case SelectPrinWay.SelectOrg:
                {
                    this.ucOrg = new UCOrgs();
                    DropDownEditorButton button2 = base.ButtonsRight["SelectUser"] as DropDownEditorButton;
                    button2.Control = this.ucOrg;
                    this.ucOrg.LoadOrgs();
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucOrg.PrinSelected += this.handler;
                    return;
                }
                case SelectPrinWay.SelectRole:
                {
                    this.ucRole = new UCRoles();
                    this.ucRole.LoadRoles();
                    this.ucRole.RoleRefresh(null, null);
                    DropDownEditorButton button3 = base.ButtonsRight["SelectUser"] as DropDownEditorButton;
                    button3.Control = this.ucRole;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    return;
                }
                case SelectPrinWay.SelectPrjUser:
                {
                    this.ucRole = new UCRoles();
                    DropDownEditorButton button5 = base.ButtonsRight["SelectUser"] as DropDownEditorButton;
                    base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.ResCombo_BeforeDropDown);
                    button5.Control = this.ucRole;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    this.ucRole.Height = 0;
                    this.ucRole.Width = base.Width;
                    return;
                }
                case SelectPrinWay.SelectPrjOrg:
                {
                    this.ucRole = new UCRoles();
                    DropDownEditorButton button4 = base.ButtonsRight["SelectUser"] as DropDownEditorButton;
                    base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.ResCombo_BeforeDropDown);
                    button4.Control = this.ucRole;
                    this.handler = new SelectPrinHandler(this.ucControl_PrinSelected);
                    this.ucRole.PrinSelected += this.handler;
                    this.ucRole.Height = 0;
                    this.ucRole.Width = base.Width;
                    return;
                }
            }
        }

        private void DoPrjOrg()
        {
            object[] objArray = new object[] { this.iBizItem, this.iPBizItem, this };
            if (PLProject.Instance.ProjDelegateInstance != null)
            {
                object obj2 = PLProject.Instance.ProjDelegateInstance(ProjDelegeteType.ProjOrgCollect, objArray);
                if (obj2 is DEPrincipal)
                {
                    this.Text = (obj2 as DEPrincipal).Name;
                    base.Tag = obj2;
                    if (this.SelectPrinChanged != null)
                    {
                        this.SelectPrinChanged(obj2 as DEPrincipal);
                    }
                }
            }
        }

        private void DoPrjUser()
        {
            object[] objArray = new object[] { this.iBizItem, this.iPBizItem, this };
            if (PLProject.Instance.ProjDelegateInstance != null)
            {
                object obj2 = PLProject.Instance.ProjDelegateInstance(ProjDelegeteType.ProjUserCollect, objArray);
                if (obj2 is DEPrincipal)
                {
                    this.Text = (obj2 as DEPrincipal).Name;
                    base.Tag = obj2;
                    if (this.SelectPrinChanged != null)
                    {
                        this.SelectPrinChanged(obj2 as DEPrincipal);
                    }
                }
            }
        }

        private void ResCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            if (this.way == SelectPrinWay.SelectPrjOrg)
            {
                this.DoPrjOrg();
            }
            if (this.way == SelectPrinWay.SelectPrjUser)
            {
                this.DoPrjUser();
            }
        }

        public void SetBizItem(IBizItem item)
        {
            this.iBizItem = item;
        }

        public void SetParentItem(IBizItem pitem)
        {
            this.iPBizItem = pitem;
        }

        private void ucControl_PrinSelected(DEPrincipal prin)
        {
            bool flag = false;
            if (base.Tag == null)
            {
                if (prin != null)
                {
                    flag = true;
                }
            }
            else if (((DEPrincipal) base.Tag).Oid != prin.Oid)
            {
                flag = true;
            }
            if (prin != null)
            {
                if (prin is DEUser)
                {
                    DEUser user = prin as DEUser;
                    if (this.isOnlyShowUser)
                    {
                        this.Text = user.Name;
                    }
                    else
                    {
                        this.Text = user.Name + "(" + user.LogId + ")";
                    }
                }
                else
                {
                    this.Text = prin.Name;
                }
                base.Tag = prin;
            }
            if (flag && (this.SelectPrinChanged != null))
            {
                this.SelectPrinChanged(prin);
            }
        }

        private void UCSelectPrin_AfterEnterEditMode(object sender, EventArgs e)
        {
            base.ReadOnly = !this.inputAllow;
        }

        private void UCSelectPrin_AfterExitEditMode(object sender, EventArgs e)
        {
            base.ReadOnly = false;
        }

        private void UCSelectPrin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.Text = "";
                base.Tag = null;
                if (this.SelectPrinChanged != null)
                {
                    this.SelectPrinChanged(null);
                }
            }
        }

        private void UCSelectPrin_KeyUp(object sender, KeyEventArgs e)
        {
            base.Tag = null;
            this.Text = "";
            if (this.SelectPrinChanged != null)
            {
                this.SelectPrinChanged(null);
            }
        }

        public bool InputAllow
        {
            get {
                return this.inputAllow;
            }
            set
            {
                this.inputAllow = value;
                base.ReadOnly = !value;
            }
        }

        public bool OnlyShowUserName
        {
            set
            {
                this.isOnlyShowUser = value;
            }
        }

        public bool SetshowSysAdmin
        {
            set
            {
                this.ucUser.showSysAdmin = value;
            }
        }

        public SelectPrinWay Way{
            get{
           return this.way;
            }}
        public enum SelectPrinWay
        {
            SelectUser,
            SelectOrg,
            SelectRole,
            SelectPrjUser,
            SelectPrjOrg
        }
    }
}

