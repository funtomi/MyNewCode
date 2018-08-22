    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCClsRelFunction : UserControlPLM
    {
        private string _className;
        private int _dataType = 1;
        private ArrayList _funcList = new ArrayList();
        private string _funcName;
        private object _funcValue;
        private Guid _Oid = Guid.NewGuid();
        private OperatorType _operType;
        private Guid _position = Guid.NewGuid();
        private bool b_start = true;
        

        public UCClsRelFunction()
        {
            this.InitializeComponent();
        }

        private void cb_funcname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_funcname.SelectedIndex > -1)
            {
                string str = (string) this._funcList[this.cb_funcname.SelectedIndex];
                if (!this.b_start)
                {
                    this._funcValue = "";
                }
                this.SetValueOCX(str);
            }
        }
 

        public void LoadFunction(string str_clsname, bool b_isCls)
        {
            if (b_isCls)
            {
                this._dataType = 1;
            }
            else
            {
                this._dataType = 2;
            }
            this._className = str_clsname;
            this._funcValue = "";
            this._funcName = "";
            this.SetFuncOcxLst();
        }

        public void LoadFunctionByItem(DEConditionItem theItem)
        {
            this.LoadFunctionByItem(theItem.ClassName, theItem.ConditionAttrName.IsClassAttr, theItem.Oid, theItem.ClassPosition, theItem.AttrValue.ToString());
        }

        public void LoadFunctionByItem(string className, bool b_isCls, Guid Oid, Guid ClassPosition, string str_funcvalue)
        {
            this.b_start = true;
            if ((str_funcvalue != null) && (str_funcvalue.IndexOf("(") >= 2))
            {
                if (b_isCls)
                {
                    this._dataType = 1;
                }
                else
                {
                    this._dataType = 2;
                }
                this._Oid = Oid;
                this._position = ClassPosition;
                this._funcValue = str_funcvalue;
                this._operType = OperatorType.Function;
                if (!className.Equals(this._className))
                {
                    this._className = className;
                }
                if ((str_funcvalue != null) && (str_funcvalue.Length > 0))
                {
                    this._funcName = str_funcvalue.Substring(0, str_funcvalue.IndexOf("(")).Trim();
                    this.SetFuncOcxLst();
                }
                this.SetValueOCX(this._funcName);
                this.b_start = false;
            }
        }

        private void SetFuncOcxLst()
        {
            this.cb_funcname.Properties.Items.Clear();
            this.cb_funcname.Text = "";
            this._funcList.Clear();
            this._funcList = UCAgent.Instance.GetFunctionNameSet(this._dataType);
            if (this._funcList.Count == 0)
            {
                this.panel_value.Controls.Clear();
            }
            else
            {
                if ((this._funcName == null) || (this._funcName.Length == 0))
                {
                    this._funcName = this._funcList[0].ToString();
                }
                foreach (string str in this._funcList)
                {
                    this.cb_funcname.Properties.Items.Add(UCAgent.Instance.GetFunctionLabel(str));
                }
                for (int i = 0; i < this._funcList.Count; i++)
                {
                    if (this._funcList[i].ToString() == this._funcName)
                    {
                        this.cb_funcname.SelectedIndex = i;
                    }
                }
            }
        }

        private void SetValueOCX(string str_funcname)
        {
            if (this.panel_value.Controls.Count == 0)
            {
                RuleComboPLM oplm = new RuleComboPLM(str_funcname) {
                    Dock = DockStyle.Fill
                };
                this.panel_value.Controls.Add(oplm);
                ArrayList list = new ArrayList {
                    this._className,
                    this._funcValue
                };
                oplm.FunctionValue = this._funcValue.ToString();
                oplm.SetInput(list);
            }
            else
            {
                ((RuleComboPLM) this.panel_value.Controls[0]).ReLoad(str_funcname);
                ArrayList list2 = new ArrayList {
                    this._className,
                    this._funcValue
                };
                ((RuleComboPLM) this.panel_value.Controls[0]).FunctionValue = this._funcValue.ToString();
                ((RuleComboPLM) this.panel_value.Controls[0]).SetInput(list2);
            }
        }

        public DEConditionItem ConditionItem
        {
            get
            {
                string name = "";
                if (this._dataType == 1)
                {
                    name = "C." + this._className;
                }
                else
                {
                    name = "R." + this._className;
                }
                return new DEConditionItem(new ConditionAttributeName(name), this.Operator, this.FunctionValue) { 
                    Oid = this._Oid,
                    ClassPosition = this._position,
                    Option = 0
                };
            }
        }

        public object FunctionValue
        {
            get
            {
                if (this.panel_value.Controls.Count == 0)
                {
                    return null;
                }
                if (this.panel_value.Controls[0] is RuleComboPLM)
                {
                    this._funcValue = ((RuleComboPLM) this.panel_value.Controls[0]).FunctionValue;
                }
                return this._funcValue;
            }
        }

        public Guid Oid
        {
            get{return
                this._Oid;
            }set
            {
                this._Oid = value;
            }
        }

        public OperatorType Operator
        {
            get
            {
                if ((this._funcValue != null) && (this._funcValue.ToString().Trim().Length == 0))
                {
                    this._operType = OperatorType.Function;
                }
                return this._operType;
            }
        }

        public Guid Position
        {
            get {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }
    }
}

