    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCAttrFunction : UserControlPLM
    {
        private ArrayList _attrList = new ArrayList();
        private ConditionAttributeName _attrName;
        private object _attrValue;
        private string _className;
        private GenericAttribute _curAttr;
        private bool _isClsAttrSet = true;
        private Guid _Oid = Guid.NewGuid();
        private ArrayList _operList = new ArrayList();
        private OperatorType _operType;
        private int _option;
        private Guid _position = Guid.NewGuid();
        
        private UCSelectClass tvClass;

        public UCAttrFunction()
        {
            this.InitializeComponent();
        }

        private void cb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_name.SelectedIndex > -1)
            {
                char attrType = Convert.ToChar("I");
                this._curAttr = (GenericAttribute) this._attrList[this.cb_name.SelectedIndex];
                if (this._curAttr.Category == "M")
                {
                    attrType = Convert.ToChar("M");
                }
                if (this._curAttr.Category == "R")
                {
                    attrType = Convert.ToChar("R");
                }
                if (this._isClsAttrSet)
                {
                    this._attrName = new ConditionAttributeName(this._className, attrType, this._curAttr.Name);
                }
                else
                {
                    this._attrName = new ConditionAttributeName(this._className, this._curAttr.Name);
                }
                this.SetOperatorOcxLst();
            }
        }

        private void cb_operator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cb_operator.SelectedIndex > -1)
            {
                this._operType = OperatorType.Function;
                string str = (string) this._operList[this.cb_operator.SelectedIndex];
                this.SetValueOCX(str);
            }
        }
 

        private GenericAttribute[] FilterAttrs(GenericAttribute[] elements)
        {
            if (elements == null)
            {
                return null;
            }
            ArrayList list = new ArrayList();
            GenericAttribute[] attributeArray = elements;
            for (int i = 0; i < attributeArray.Length; i++)
            {
                object obj2 = attributeArray[i];
                GenericAttribute attribute = obj2 as GenericAttribute;
                if ((attribute != null) && (!(attribute.Attach is DEMetaAttribute) || (attribute.Attach as DEMetaAttribute).IsViewable))
                {
                    list.Add(attribute);
                }
            }
            return (GenericAttribute[]) list.ToArray(typeof(GenericAttribute));
        }


        public void LoadFunctionByItem(DEConditionItem theItem)
        {
            this.LoadFunctionByItem(theItem.ClassName, theItem.ConditionAttrName.IsClassAttr, theItem.Oid, theItem.ClassPosition, theItem.ConditionAttrName.GetAttrName(), theItem.Operator, theItem.AttrValue.ToString(), theItem.Option);
        }

        public void LoadFunctionByItem(string className, bool isClass, Guid Oid, Guid ClassPosition, string attrName, OperatorType oper, object attrValue, int option)
        {
            if ((attrValue != null) && (attrValue.ToString().IndexOf("(") >= 2))
            {
                this._isClsAttrSet = isClass;
                this._Oid = Oid;
                this._position = ClassPosition;
                this._attrValue = attrValue;
                this._operType = OperatorType.Function;
                if (!className.Equals(this._className))
                {
                    this._className = className;
                    this.SetNameOcxLst();
                    this.SetOperatorOcxLst();
                }
                for (int i = 0; i < this._attrList.Count; i++)
                {
                    GenericAttribute attribute = (GenericAttribute) this._attrList[i];
                    if (attrName == attribute.Name)
                    {
                        this.cb_name.SelectedIndex = i;
                        this._option = option;
                        break;
                    }
                }
                if (this._operList.Count != 0)
                {
                    for (int j = 0; j < this._operList.Count; j++)
                    {
                        string str = this._operList[j] as string;
                        if ((this._attrValue != null) && (this._attrValue != ""))
                        {
                            string str2 = this._attrValue.ToString().Substring(0, this._attrValue.ToString().IndexOf("("));
                            if (str == str2)
                            {
                                this.cb_operator.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                    if ((this.panel_value.Controls.Count != 0) && (this.panel_value.Controls[0] is RuleComboPLM))
                    {
                        this.panel_value.Controls.Clear();
                        RuleComboPLM oplm = new RuleComboPLM(this._operList[this.cb_operator.SelectedIndex].ToString());
                        if (this._attrValue != null)
                        {
                            ArrayList list = new ArrayList {
                                0,
                                this._attrValue
                            };
                            oplm.SetInput(list);
                            oplm.Dock = DockStyle.Fill;
                            this.panel_value.Controls.Add(oplm);
                        }
                    }
                }
            }
        }

        public void LoadOCXByClsName(string str_clsname, bool isclsset)
        {
            this._isClsAttrSet = isclsset;
            this._className = str_clsname;
            this.SetNameOcxLst();
            this.SetOperatorOcxLst();
        }

        public void LocationByName(string strLabel)
        {
            if (this.cb_name.Properties.Items.Count != 0)
            {
                for (int i = 0; i < this.cb_name.Properties.Items.Count; i++)
                {
                    if (this.cb_name.Properties.Items[i].ToString() == strLabel)
                    {
                        this.cb_name.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void SetNameOcxLst()
        {
            this._attrList.Clear();
            this._curAttr = null;
            this._operList.Clear();
            this.cb_name.Properties.Items.Clear();
            this.cb_operator.Properties.Items.Clear();
            this.panel_value.Controls.Clear();
            this.cb_name.Text = "";
            this.cb_operator.Text = "";
            this._attrName = null;
            this._operType = OperatorType.Unknown;
            this._attrList.Clear();
            GenericAttribute[] elements = null;
            if (this._isClsAttrSet)
            {
                elements = ModelContext.MetaModel.GetAllSearchableAttributes(this._className);
            }
            else
            {
                elements = ModelContext.MetaModel.GetRelationGenericAttributes(this._className);
            }
            if (elements != null)
            {
                GenericAttribute[] c = this.FilterAttrs(elements);
                this._attrList.AddRange(c);
            }
            foreach (GenericAttribute attribute in this._attrList)
            {
                this.cb_name.Properties.Items.Add(attribute.Label.ToString());
            }
        }

        private void SetOperatorOcxLst()
        {
            this.cb_operator.Properties.Items.Clear();
            this.cb_operator.Text = "";
            this._operList.Clear();
            this._operList = UCAgent.Instance.GetFunctionNameSet(3);
            foreach (string str in this._operList)
            {
                bool flag = false;
                if (((this.cb_name.Text == "版本创建人") || (this.cb_name.Text == "持有人")) || (this.cb_name.Text == "定版人"))
                {
                    flag = true;
                }
                if (((this._curAttr != null) && (this._curAttr.Attach != null)) && (this._curAttr.Attach is DEMetaAttribute))
                {
                    DEMetaAttribute attach = this._curAttr.Attach as DEMetaAttribute;
                    if ((attach.SpecialType2 == PLMSpecialType.UserType) && (attach.DataType2 == PLMDataType.Guid))
                    {
                        flag = true;
                    }
                }
                if (flag && (str.ToString() == "AttrUser2Org"))
                {
                    this.cb_operator.Properties.Items.Add(UCAgent.Instance.GetFunctionLabel(str.ToString()));
                }
                else
                {
                    this._attrValue = "";
                }
            }
            if (this.cb_operator.Properties.Items.Count > 0)
            {
                this.cb_operator.Text = this.cb_operator.Properties.Items[0].ToString();
                this.SetValueOCX(this._operList[0].ToString());
            }
            else
            {
                this.panel_value.Controls.Clear();
            }
        }

        private void SetValueOCX(string str_funcname)
        {
            ArrayList list = new ArrayList();
            if (this.panel_value.Controls.Count == 0)
            {
                RuleComboPLM oplm = new RuleComboPLM(str_funcname) {
                    Dock = DockStyle.Fill
                };
                this.panel_value.Controls.Add(oplm);
                if (this._attrValue != null)
                {
                    list.Add(0);
                    list.Add(this._attrValue);
                    oplm.SetInput(list);
                }
            }
            else
            {
                ((RuleComboPLM) this.panel_value.Controls[0]).ReLoad(str_funcname);
                if (this._attrValue != null)
                {
                    list.Add(0);
                    list.Add(this._attrValue);
                    ((RuleComboPLM) this.panel_value.Controls[0]).SetInput(list);
                }
            }
        }

        public string AttrName{
         get{return   this._attrName.GetAttrName();
        }}
        public bool AttrNameEnable
        {
            get {
                return this.cb_name.Enabled;
            }
            set
            {
                this.cb_name.Enabled = value;
            }
        }

        public string AttrText
        {
            get
            {
                if (this.cb_name.Text.Trim().Length == 0)
                {
                    return null;
                }
                return this.cb_name.Text;
            }
        }

        public object AttrValue
        {
            get
            {
                if (this.panel_value.Controls.Count == 0)
                {
                    return null;
                }
                if (this.panel_value.Controls[0] is RuleComboPLM)
                {
                    this._attrValue = ((RuleComboPLM) this.panel_value.Controls[0]).FunctionValue;
                }
                return this._attrValue;
            }
        }

        public ConditionAttributeName ConditionAttrName
        {
            get
            {
                if (this._attrName == null)
                {
                    return null;
                }
                return this._attrName;
            }
        }

        public DEConditionItem ConditionItem{
           get{return new DEConditionItem(this._attrName, this._operType, this.AttrValue) { 
                Oid=this._Oid,
                ClassPosition=this._position,
                Option=this._option
            };}}

        public Guid Oid
        {
            get{
               return this._Oid;}
            set
            {
                this._Oid = value;
            }
        }

        public OperatorType Operator{get{return
            this._operType;
        }}
        public int Option{get{return
            this._option;}}

        public Guid Position
        {
            get{return
                this._position;
            }set
            {
                this._position = value;
            }
        }
    }
}

