    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCParamMain : UserControlPLM
    {
        private bool b_DoubleClisk;
        private bool b_start;

        public event RuleDropListHandler RuleSelected;

        public UCParamMain()
        {
            this.b_start = true;
            this.b_DoubleClisk = true;
            this.InitializeComponent();
        }

        public UCParamMain(string str_funcname) : this()
        {
            this.LoadRuleUC(str_funcname);
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.CloseParent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!this.b_start)
            {
                if ((this.RuleSelected != null) && (this.panel_Param.Controls.Count > 0))
                {
                    IUCAgent agent = (IUCAgent) this.panel_Param.Controls[0];
                    if (agent.GetValue() != null)
                    {
                        this.RuleSelected(agent.GetValue());
                    }
                }
                this.CloseParent();
            }
        }

        private void CloseParent()
        {
            if (base.Parent != null)
            {
                if (base.Parent is PopupContainerControl)
                {
                    PopupContainerControl parent = base.Parent as PopupContainerControl;
                    if ((parent != null) && (parent.OwnerEdit != null))
                    {
                        parent.OwnerEdit.ClosePopup();
                    }
                }
                else
                {
                    base.Parent.Hide();
                }
            }
        }

        private void LoadRuleUC(string str_funcname)
        {
            this.panel_Param.Controls.Clear();
            UserControlPLM funcUC = UCAgent.Instance.GetFuncUC(str_funcname);
            funcUC.Dock = DockStyle.Fill;
            this.panel_Param.Controls.Add(funcUC);
        }

        public void ReLoad(string str_funcname)
        {
            this.LoadRuleUC(str_funcname);
        }

        public void SetInput(object o_in)
        {
            if (this.panel_Param.Controls.Count != 0)
            {
                ((IUCAgent) this.panel_Param.Controls[0]).SetInput(o_in);
            }
        }

        private void UCParamMain_Enter(object sender, EventArgs e)
        {
            this.b_start = false;
        }

        public bool AllowDoubleClick
        {
            get {
                return
                this.b_DoubleClisk;
            }
            set
            {
                this.b_DoubleClisk = value;
            }
        }
    }
}

