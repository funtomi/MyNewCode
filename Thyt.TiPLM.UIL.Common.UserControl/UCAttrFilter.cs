    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Admin.View;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCAttrFilter : UserControlPLM {
        private ArrayList _attrList = new ArrayList();
        private ConditionAttributeName _attrName;
        private object _attrValue;
        private string _className;
        private GenericAttribute _curAttr;
        private bool _isClsAttrSet = true;
        private bool _isSchDefine;
        private Guid _Oid = Guid.NewGuid();
        private ArrayList _operList = new ArrayList();
        private OperatorType _operType;
        private int _option = 1;
        private Guid _position = Guid.NewGuid();

        public static Hashtable htProcessList = new Hashtable();
        private bool isLoading = true;
        private bool isStarting = true;

        private static ArrayList lstView = null;
        public static DELBPMEntityList TheProcessList = null;

        public UCAttrFilter() {
            this.InitializeComponent();
        }

        private void cb_bool_SelectedIndexChanged(object sender, EventArgs e) {
            if (this._curAttr.DataType == PLMDataType.Bool) {
                if (((ComboBoxEditPLM)this.panel_value.Controls[0]).SelectedItem == "是") {
                    this._attrValue = Convert.ToString(true);
                    this.isStarting = false;
                } else if (((ComboBoxEditPLM)this.panel_value.Controls[0]).SelectedItem == "否") {
                    this._attrValue = Convert.ToString(false);
                    this.isStarting = false;
                } else {
                    this._attrValue = null;
                }
            }
        }

        private void cb_name_SelectedIndexChanged(object sender, EventArgs e) {
            this.isLoading = true;
            this.ckbLoginoid.Visible = this.ckbLoginoid.Checked = false;
            if (this.cb_name.SelectedIndex > -1) {
                char attrType = Convert.ToChar("I");
                this._curAttr = (GenericAttribute)this._attrList[this.cb_name.SelectedIndex];
                if (this._curAttr.Category == "M") {
                    attrType = Convert.ToChar("M");
                }
                if (this._curAttr.Category == "R") {
                    attrType = Convert.ToChar("R");
                }
                if (this._isClsAttrSet) {
                    this._attrName = new ConditionAttributeName(this._className, attrType, this._curAttr.Name);
                } else {
                    this._attrName = new ConditionAttributeName(this._className, this._curAttr.Name);
                }
                this.SetOperatorOcxLst(this._curAttr.DataType);
                this.SetValueOCX(this._curAttr);
                this.SetOption(this._curAttr.DataType, 0);
            } else {
                this.SetOption(PLMDataType.Unknown, 0);
            }
            this.isLoading = false;
        }

        private void cb_operator_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.cb_operator.SelectedIndex > -1) {
                PLMOperator @operator = (PLMOperator)this._operList[this.cb_operator.SelectedIndex];
                this._operType = @operator.OperType;
                if ((@operator.OperType == OperatorType.IsNull) || (@operator.OperType == OperatorType.IsNotNull)) {
                    this.panel_value.Visible = false;
                    this.ckbLoginoid.Visible = false;
                } else {
                    this.panel_value.Visible = true;
                    for (Control control = base.Parent; control != null; control = control.Parent) {
                        if (control is UCDataViewCondition) {
                            ArrayList list = new ArrayList(new string[] { "M.HOLDER", "R.CREATOR", "R.RELEASER", "I.CREATOR", "I.LATESTUPDATOR" });
                            if (list.Contains(this._curAttr.Category + "." + this._curAttr.Name)) {
                                this.ckbLoginoid.Visible = true;
                            } else {
                                DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute(this._className, this._curAttr.Name);
                                if (((attribute != null) && (attribute.SpecialType2 == PLMSpecialType.UserType)) && (attribute.DataType2 == PLMDataType.Guid)) {
                                    this.ckbLoginoid.Visible = true;
                                }
                            }
                        }
                    }
                }
                if (!this.isLoading) {
                    this.isStarting = false;
                }
            }
        }

        public static bool CheckConditionValueValid(OperatorType optyoe, object objvalue) {
            if ((optyoe == OperatorType.IsNull) || (optyoe == OperatorType.IsNotNull)) {
                return true;
            }
            if (objvalue == null) {
                return false;
            }
            return true;
        }

        private void chkCaseSensitive_CheckedChanged(object sender, EventArgs e) {
            if (this.chkCaseSensitive.Checked && ((this._option & 1) == 0)) {
                this._option++;
            } else if (!this.chkCaseSensitive.Checked && ((this._option & 1) > 0)) {
                this._option--;
            }
        }

        private void CreateOcx(PLMDataType dataType) {
            switch (dataType) {
                case PLMDataType.Int16: {
                        TextEditPLM tplm6 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm6);
                        tplm6.Dock = DockStyle.Fill;
                        tplm6.Text = "0";
                        tplm6.KeyUp += new KeyEventHandler(this.num_value_KeyUp);
                        tplm6.TextChanged += new EventHandler(this.num_value_ValueChanged);
                        return;
                    }
                case PLMDataType.Int32: {
                        TextEditPLM tplm7 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm7);
                        tplm7.Dock = DockStyle.Fill;
                        tplm7.Text = "0";
                        tplm7.KeyUp += new KeyEventHandler(this.num_value_KeyUp);
                        tplm7.TextChanged += new EventHandler(this.num_value_ValueChanged);
                        return;
                    }
                case PLMDataType.Int64: {
                        TextEditPLM tplm8 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm8);
                        tplm8.Dock = DockStyle.Fill;
                        tplm8.Text = "0";
                        tplm8.KeyUp += new KeyEventHandler(this.num_value_KeyUp);
                        tplm8.TextChanged += new EventHandler(this.num_value_ValueChanged);
                        return;
                    }
                case PLMDataType.Char: {
                        TextEditPLM tplm2 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm2);
                        tplm2.Dock = DockStyle.Fill;
                        tplm2.KeyPress += new KeyPressEventHandler(this.txtValue_KeyPress);
                        return;
                    }
                case PLMDataType.String: {
                        TextEditPLM tplm3 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm3);
                        tplm3.Dock = DockStyle.Fill;
                        tplm3.KeyPress += new KeyPressEventHandler(this.txtValue_KeyPress);
                        return;
                    }
                case PLMDataType.Bool: {
                        ComboBoxEditPLM tplm = new ComboBoxEditPLM();
                        this.panel_value.Controls.Add(tplm);
                        tplm.Properties.Items.Add("是");
                        tplm.Properties.Items.Add("否");
                        tplm.Text = "";
                        this._attrValue = true;
                        tplm.Dock = DockStyle.Fill;
                        tplm.DropDownStyle = ComboBoxStyle.DropDownList;
                        tplm.KeyPress += new KeyPressEventHandler(this.txtValue_KeyPress);
                        tplm.SelectedIndexChanged += new EventHandler(this.cb_bool_SelectedIndexChanged);
                        return;
                    }
                case PLMDataType.Decimal: {
                        TextEditPLM tplm5 = new TextEditPLM();
                        this.panel_value.Controls.Add(tplm5);
                        tplm5.Dock = DockStyle.Fill;
                        tplm5.Text = "0";
                        tplm5.KeyUp += new KeyEventHandler(this.num_value_KeyUp);
                        tplm5.TextChanged += new EventHandler(this.num_value_ValueChanged);
                        return;
                    }
                case PLMDataType.DateTime: {
                        DateEditPLM tplm4 = new DateEditPLM {
                            CustomFormat = "yyyy-MM-dd",
                            Format = DateTimePickerFormat.Custom
                        };
                        this.panel_value.Controls.Add(tplm4);
                        tplm4.Dock = DockStyle.Fill;
                        tplm4.TextChanged += new EventHandler(this.dateTimePicker1_TextChanged);
                        return;
                    }
            }
            TextEditPLM tplm9 = new TextEditPLM();
            this.panel_value.Controls.Add(tplm9);
            tplm9.Dock = DockStyle.Fill;
            tplm9.KeyPress += new KeyPressEventHandler(this.txtValue_KeyPress);
        }

        private void CreateOcx(GenericAttribute deattr) {
            DEMeta meta = this.GetClass();
            if (meta != null) {
                DEMetaClass cls = meta as DEMetaClass;
                DEMetaRelation relation = meta as DEMetaRelation;
                if (((cls != null) && (deattr.Name == "STATE")) && (deattr.Category == "M")) {
                    this.InitTyptUC();
                }
                if (deattr.Name == "EFFECTIVE") {
                    this.InitEffective();
                } else if (((cls != null) && (cls.SystemClass == 'Y')) && (deattr.Name == "STATUS")) {
                    this.InitStatusCtl(cls);
                } else if (deattr.Name == "VIEW") {
                    if (relation != null) {
                        this.InitViewCtrl();
                    }
                } else if (deattr.Name == "GENDER") {
                    this.InitGENDERCtrl();
                } else if ((deattr.Name == ConstFixedAttr.FIXED_ATTR_NAME_SUSPENDED) || (deattr.Name == "INPROCESS")) {
                    this.InitBoolCtrl();
                } else if (deattr.DataType == PLMDataType.Char) {
                    if (((this._className != "ADM2_ORGANIZATION") && (this._className != "ADM2_USER")) || (("PLM_" + deattr.Name) != "PLM_ISINHERIT")) {
                        this.CreateOcx(deattr.DataType);
                    } else {
                        this.InitBoolCtrl();
                    }
                } else {
                    this.CreateOcx(deattr.DataType);
                }
            }
        }

        private void CreateSpecOcx(DEMetaAttribute attr) {
            UCSelectPrinPLM nplm = null;
            UCSelectProcessTemplatePLM eplm = null;
            switch (attr.SpecialType2) {
                case PLMSpecialType.UserType:
                    nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectUser) {
                        showSysAdmin = true
                    };
                    break;

                case PLMSpecialType.OrganizationType:
                    nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectOrg);
                    break;

                case PLMSpecialType.RoleType:
                    nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectRole);
                    break;

                case PLMSpecialType.ProcessTemplate:
                    eplm = new UCSelectProcessTemplatePLM();
                    break;
            }
            if (nplm != null) {
                this.panel_value.Controls.Add(nplm);
                nplm.Dock = DockStyle.Fill;
            } else if (eplm != null) {
                this.panel_value.Controls.Add(eplm);
                eplm.Dock = DockStyle.Fill;
            }
        }

        private void dateTimePicker1_TextChanged(object sender, EventArgs e) {
            this._attrValue = ((DateEditPLM)this.panel_value.Controls[0]).Value;
            this.isStarting = false;
        }

        private void Effec_KeyPress(object sender, KeyPressEventArgs e) {
            this.GetEffecValue();
            this.isStarting = false;
        }

        private void Effec_SelectedIndexChanged(object sender, EventArgs e) {
            this.GetEffecValue();
        }

        private GenericAttribute[] FilterAttrs(GenericAttribute[] elements) {
            if (elements == null) {
                return null;
            }
            ArrayList list = new ArrayList();
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this._className);
            GenericAttribute[] attributeArray = elements;
            for (int i = 0; i < attributeArray.Length; i++) {
                object obj2 = attributeArray[i];
                GenericAttribute attribute = obj2 as GenericAttribute;
                if (((attribute != null) && ((!(attribute.Attach is DEMetaAttribute) || !this._isSchDefine) || IsViewable(class2.Name, (DEMetaAttribute)attribute.Attach))) && (((class2 == null) || !class2.IsResClass) || (attribute.Attach != null))) {
                    list.Add(attribute);
                }
            }
            if (((!this._isSchDefine || class2.IsResClass) || (class2.SystemClass == 'Y')) && (this._isSchDefine && class2.IsResClass)) {
                DEMetaAttribute attr = new DEMetaAttribute();
                attr = new DEMetaAttribute {
                    Name = "ID",
                    Oid = new Guid("11111111-1111-1111-1111-111111111111"),
                    Label = "代号",
                    DataType = 4,
                    Option = 5
                };
                GenericAttribute attribute3 = GenericAttribute.CreateAttribute(this._className, attr);
                attribute3.Category = "I";
                list.Add(attribute3);
            }
            return (GenericAttribute[])list.ToArray(typeof(GenericAttribute));
        }

        private void GetBoolValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            if (!string.IsNullOrEmpty(text)) {
                if (text == "是") {
                    this._attrValue = Convert.ToString(true);
                    this.isStarting = false;
                } else if (text == "否") {
                    this._attrValue = Convert.ToString(false);
                    this.isStarting = false;
                } else {
                    this._attrValue = Convert.ToString(false);
                }
            }
        }

        private void GetCharValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            if (!string.IsNullOrEmpty(text)) {
                if (text == "是") {
                    this._attrValue = "Y";
                } else if (text == "否") {
                    this._attrValue = "N";
                } else {
                    this._attrValue = text;
                }
                this.isStarting = false;
            } else {
                this._attrValue = "";
            }
        }

        private DEMeta GetClass() {
            DEMeta relation = ModelContext.MetaModel.GetClass(this._className);
            if (relation == null) {
                relation = ModelContext.MetaModel.GetRelation(this._className);
            }
            return relation;
        }

        public static ArrayList GetClassSchGenericAttr(string clsName) {
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(clsName);
            ArrayList list = new ArrayList();
            string[] c = new string[] { "CREATOR", "HOLDER", "RELEASER", "LATESTUPDATOR" };
            ArrayList list2 = new ArrayList(c);
            ArrayList attributes = ModelContext.MetaModel.GetAttributes(clsName);
            ArrayList attrs = new ArrayList();
            foreach (DEMetaAttribute attribute in attributes) {
                if (IsViewable(clsName, attribute)) {
                    attrs.Add(attribute);
                }
            }
            foreach (FixedAttribute attribute2 in FixedAttribute.GetAllSearchableFixedAttrs()) {
                if ((attribute2.AttrName != "OID") && (((attribute2.AttrType != PLMDataType.Guid) || list2.Contains(attribute2.AttrName)) || (attribute2.AttrName == "GROUP"))) {
                    list.Add(GenericAttribute.CreateAttribute(attribute2));
                }
            }
            if ((class2.SystemClass == 'Y') || class2.IsResClass) {
                list.Clear();
                attrs.Clear();
                ArrayList list6 = ModelContext.MetaModel.GetAttributes(clsName);
                if (list6.Count > 0) {
                    attrs.AddRange(list6);
                }
            }
            foreach (DEMetaAttribute attribute3 in FixedAttribute.GetOrderAttributes(attrs)) {
                if (((attribute3.Name == "OID") || (attribute3.DataType2 == PLMDataType.Card)) || ((attribute3.DataType2 == PLMDataType.Blob) || (attribute3.DataType2 == PLMDataType.Grid))) {
                    continue;
                }
                if (attribute3.DataType2 == PLMDataType.Guid) {
                    if (class2.SystemClass == 'Y') {
                        if ((class2.Name == "BPM_PROCESS_INSTANCE") && (attribute3.Name == "PROCESSDEFINITIONID")) {
                            goto Label_0241;
                        }
                        continue;
                    }
                    if ((!list2.Contains(attribute3.Name) && (attribute3.SpecialType <= 0)) && (attribute3.Name != "GROUP")) {
                        continue;
                    }
                }
            Label_0241:
                if ((class2.SystemClass != 'Y') || (attribute3.Name != "REVSTATE")) {
                    list.Add(GenericAttribute.CreateAttribute(class2.Name, attribute3));
                }
            }
            return list;
        }

        private void GetEffecValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            this._attrValue = 0;
            switch (text) {
                case "始终有效":
                    this._attrValue = 0;
                    break;

                case "指定时间内有效":
                    this._attrValue = 1;
                    break;

                case "指定序号范围内有效":
                    this._attrValue = 2;
                    break;
            }
        }

        private void GetManValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            this._attrValue = text;
        }

        private void GetProcessInstanceStateValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            this._attrValue = "";
            if (!string.IsNullOrEmpty(text)) {
                this._attrValue = text;
            }
        }

        private static void GetProcessList() {
            if (TheProcessList == null) {
                new BPMAdmin().GetProcessDefPropertyList(ClientData.LogonUser.Oid, out TheProcessList);
                foreach (DELProcessDefProperty property in TheProcessList) {
                    htProcessList.Add(property.ID, property);
                }
            }
        }

        public static ArrayList GetRelationSchGenericAttr(string clsName) {
            ArrayList list = new ArrayList();
            DEMetaRelation relation = ModelContext.MetaModel.GetRelation(clsName);
            ArrayList relationAttributes = ModelContext.MetaModel.GetRelationAttributes(relation.Oid);
            ArrayList list3 = new ArrayList();
            foreach (DEMetaAttribute attribute in relationAttributes) {
                if (IsRltViewable(relation.Name, attribute)) {
                    list3.Add(attribute);
                }
            }
            if (list3.Count != 0) {
                foreach (DEMetaAttribute attribute2 in list3) {
                    if ((attribute2.Name != "OID") && (attribute2.DataType2 != PLMDataType.Guid)) {
                        list.Add(GenericAttribute.CreateRelationAttribute(relation.Name, attribute2));
                    }
                }
            }
            return list;
        }

        public static ArrayList GetSchClassAttrs(DEMetaClass theCls) {
            if (!theCls.HasStatus && (theCls.SystemClass == 'N')) {
                return null;
            }
            string[] c = new string[] { "CREATOR", "HOLDER", "RELEASER", "LATESTUPDATOR" };
            ArrayList list = new ArrayList(c);
            ArrayList attributes = ModelContext.MetaModel.GetAttributes(theCls.Name);
            ArrayList list3 = new ArrayList();
            foreach (DEMetaAttribute attribute in attributes) {
                if (IsViewable(theCls.Name, attribute)) {
                    list3.Add(attribute);
                }
            }
            ArrayList fixedAttrListBySearch = FixedAttribute.GetFixedAttrListBySearch();
            if (list3.Count > 0) {
                fixedAttrListBySearch.AddRange(list3);
            }
            if ((theCls.SystemClass == 'Y') || theCls.IsResClass) {
                fixedAttrListBySearch.Clear();
                ArrayList list5 = ModelContext.MetaModel.GetAttributes(theCls.Name);
                if (list5.Count > 0) {
                    fixedAttrListBySearch.AddRange(list5);
                }
            }
            ArrayList orderAttributes = FixedAttribute.GetOrderAttributes(fixedAttrListBySearch);
            ArrayList list7 = new ArrayList();
            foreach (DEMetaAttribute attribute2 in orderAttributes) {
                if (((attribute2.DataType2 == PLMDataType.Card) || (attribute2.DataType2 == PLMDataType.Blob)) || ((attribute2.DataType2 == PLMDataType.Grid) || (attribute2.Name == "OID"))) {
                    continue;
                }
                if (attribute2.DataType2 == PLMDataType.Guid) {
                    if (theCls.SystemClass == 'Y') {
                        if ((theCls.Name == "BPM_PROCESS_INSTANCE") && (attribute2.Name == "PROCESSDEFINITIONID")) {
                            goto Label_01DA;
                        }
                        continue;
                    }
                    if ((!list.Contains(attribute2.Name) && (attribute2.SpecialType <= 0)) && (attribute2.Name != "GROUP")) {
                        continue;
                    }
                }
            Label_01DA:
                if ((theCls.SystemClass != 'Y') || (attribute2.Name != "REVSTATE")) {
                    list7.Add(attribute2);
                }
            }
            return list7;
        }

        private void GetStateValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            this._attrValue = "";
            if (!string.IsNullOrEmpty(text)) {
                if (text == "初始化") {
                    this._attrValue = "Initial";
                } else if (text == "运行中") {
                    this._attrValue = "Running";
                } else if (text == "已完成") {
                    this._attrValue = "Completed";
                } else if (text == "已取消") {
                    this._attrValue = "Aborted";
                } else if (text == "已挂起") {
                    this._attrValue = "Suspended";
                } else if (text == "已分配") {
                    this._attrValue = "ReAssigned";
                } else if (text == "再分配") {
                    this._attrValue = "Assigned";
                } else if (text == "已接受") {
                    this._attrValue = "Accepted";
                } else if (text == "已删除") {
                    this._attrValue = "Deleted";
                } else if (text == "已发布") {
                    this._attrValue = "P";
                } else if (text == "未发布") {
                    this._attrValue = "U";
                }
            }
        }

        private void GetStatusValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            if (string.IsNullOrEmpty(text)) {
                this._attrValue = "";
            }
            if (text == "计划中") {
                this._attrValue = 0;
            }
            if (text == "运行中") {
                this._attrValue = 1;
            }
            if (text == "审核中") {
                this._attrValue = 2;
            }
            if (text == "暂停中") {
                this._attrValue = 3;
            }
            if (text == "已完成") {
                this._attrValue = 4;
            }
            if (text == "已取消") {
                this._attrValue = 5;
            }
            if (text == "可用") {
                this._attrValue = "A";
            }
            if (text == "冻结") {
                this._attrValue = "F";
            }
        }

        private string GetTypeShow(char chr_type) {
            switch (chr_type) {
                case 'N':
                    return "未知状态";

                case 'O':
                    return "检出";

                case 'R':
                    return "定版";

                case 'I':
                    return "检入";

                case 'A':
                    return "废弃";
            }
            return "未知状态";
        }

        private string GetTypeValue(string str_type) {
            switch (str_type) {
                case "检入":
                    return Convert.ToString('I');

                case "检出":
                    return Convert.ToString('O');

                case "定版":
                    return Convert.ToString('R');

                case "废弃":
                    return Convert.ToString('A');

                case "未知状态":
                    return string.Empty;
            }
            return string.Empty;
        }

        private void GetValueOfProcess() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            if (!string.IsNullOrEmpty(text)) {
                GetProcessList();
                foreach (DELProcessDefProperty property in TheProcessList) {
                    if (property.Name == text) {
                        this._attrValue = property.ID.ToString();
                        break;
                    }
                }
            }
        }

        private void GetViews() {
            if (lstView == null) {
                lstView = PLView.GetUserViewList(ClientData.LogonUser.Oid);
            }
        }

        private void GetViewsValue() {
            string text = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
            if (!string.IsNullOrEmpty(text)) {
                for (int i = 0; i < lstView.Count; i++) {
                    DEView view = lstView[i] as DEView;
                    if (view.Label == text) {
                        this._attrValue = view.GetName();
                    }
                }
            } else {
                this._attrValue = "";
            }
        }

        private void InitBoolCtrl() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            this.panel_value.Controls.Add(tplm);
            tplm.Properties.Items.Add("是");
            tplm.Properties.Items.Add("否");
            tplm.Text = "";
            this._attrValue = true;
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.txtValue_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.cb_bool_SelectedIndexChanged);
        }

        private void InitEffective() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            this.panel_value.Controls.Add(tplm);
            tplm.Properties.Items.Add("始终有效");
            tplm.Properties.Items.Add("指定时间内有效");
            tplm.Properties.Items.Add("指定序号范围内有效");
            tplm.Text = "始终有效";
            this._attrValue = 0;
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.Effec_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.Effec_SelectedIndexChanged);
        }

        private void InitGENDERCtrl() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            this.panel_value.Controls.Add(tplm);
            tplm.Properties.Items.Add("男");
            tplm.Properties.Items.Add("女");
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.Man_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.Man_SelectedIndexChanged);
        }

        private void InitHumanUC() {
            UCSelectPrinPLM nplm = new UCSelectPrinPLM(UCSelectPrinPLM.SelectPrinWay.SelectUser);
            this.panel_value.Controls.Add(nplm);
            nplm.Dock = DockStyle.Fill;
        }


        private void InitIDCtrl() {
            IDInputPLM tplm = new IDInputPLM();
            this.panel_value.Controls.Add(tplm);
            tplm.Dock = DockStyle.Fill;
        }

        private void InitObjectClsUC() {
            UCSelectClassPLM splm = new UCSelectClassPLM(this._className, false, SelectClassConstraint.BusinessItemClass) {
                Dock = DockStyle.Fill
            };
            this.panel_value.Controls.Add(splm);
        }

        private void InitProcessCtrl() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            GetProcessList();
            this.panel_value.Controls.Add(tplm);
            foreach (DELProcessDefProperty property in TheProcessList) {
                tplm.Properties.Items.Add(property.Name);
            }
            tplm.Text = "";
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.Process_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.Process_SelectedIndexChanged);
        }

        private void InitProjectCtrl() {
            ProjectInputPLM tplm = new ProjectInputPLM();
            this.panel_value.Controls.Add(tplm);
            tplm.Dock = DockStyle.Fill;
        }

        private void InitStateCtrl(DEMetaClass cls) {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            this.panel_value.Controls.Add(tplm);
            if (cls.Name == "BPM_PROCESS_INSTANCE") {
                tplm.Properties.Items.Add("初始化");
                tplm.Properties.Items.Add("运行中");
                tplm.Properties.Items.Add("已完成");
                tplm.Properties.Items.Add("已取消");
                tplm.Properties.Items.Add("已挂起");
            } else if (cls.Name == "BPM_WORKITEM") {
                tplm.Properties.Items.Add("初始化");
                tplm.Properties.Items.Add("已分配");
                tplm.Properties.Items.Add("已接受");
                tplm.Properties.Items.Add("运行中");
                tplm.Properties.Items.Add("已完成");
                tplm.Properties.Items.Add("已取消");
                tplm.Properties.Items.Add("再分配");
                tplm.Properties.Items.Add("已暂停");
            } else if (cls.Name == "BPM_PROCESS_DEFINITION") {
                tplm.Properties.Items.Add("初始化");
                tplm.Properties.Items.Add("已删除");
            } else {
                tplm.Properties.Items.Add("已发布");
                tplm.Properties.Items.Add("未发布");
            }
            tplm.Text = "";
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.State_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.State_SelectedIndexChanged);
        }

        private void InitStatusCtl(DEMetaClass cls) {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            this.panel_value.Controls.Add(tplm);
            this.cb_name.Text = "";
            if ((cls.Name == "PRJ_MSP_PROJECTS") || (cls.Name == "PRJ_MSP_TASKS")) {
                tplm.Properties.Items.Add("计划中");
                tplm.Properties.Items.Add("运行中");
                tplm.Properties.Items.Add("审核中");
                tplm.Properties.Items.Add("暂停中");
                tplm.Properties.Items.Add("已完成");
                tplm.Properties.Items.Add("已取消");
            } else {
                if (((cls.Name != "ADM2_ORGANIZATION") && (cls.Name != "ADM_VIEW_PRINCIPAL")) && ((cls.Name != "ADM2_ROLE") && (cls.Name != "ADM2_USER"))) {
                    return;
                }
                tplm.Properties.Items.Add("可用");
                if (cls.Name == "ADM2_USER") {
                    tplm.Properties.Items.Add("冻结");
                }
            }
            tplm.Dock = DockStyle.Fill;
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            tplm.KeyPress += new KeyPressEventHandler(this.Status_KeyPress);
            tplm.SelectedIndexChanged += new EventHandler(this.Status_SelectedIndexChanged);
        }

        private void InitTyptUC() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            tplm.Properties.Items.Add("检入");
            tplm.Properties.Items.Add("检出");
            tplm.Properties.Items.Add("定版");
            tplm.Properties.Items.Add("废弃");
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            this.panel_value.Controls.Add(tplm);
            tplm.Dock = DockStyle.Fill;
        }

        private void InitTyptUC2() {
            ComboBoxEditPLM tplm = new ComboBoxEditPLM();
            tplm.Properties.Items.Add("");
            tplm.DropDownStyle = ComboBoxStyle.DropDownList;
            this.panel_value.Controls.Add(tplm);
            tplm.Dock = DockStyle.Fill;
        }

        private void InitViewCtrl() {
            this.GetViews();
            if ((lstView != null) && (lstView.Count != 0)) {
                ComboBoxEditPLM tplm = new ComboBoxEditPLM();
                this.panel_value.Controls.Add(tplm);
                DEView view = null;
                for (int i = 0; i < lstView.Count; i++) {
                    view = lstView[i] as DEView;
                    tplm.Properties.Items.Add(view.Label);
                }
                tplm.Text = "";
                tplm.Dock = DockStyle.Fill;
                tplm.DropDownStyle = ComboBoxStyle.DropDownList;
                tplm.KeyPress += new KeyPressEventHandler(this.View_KeyPress);
                tplm.SelectedIndexChanged += new EventHandler(this.View_SelectedIndexChanged);
            }
        }

        public static bool IsRltViewable(string strClsName, DEMetaAttribute attr) {
            if ((attr.Option & 0x40) > 0) {
                return false;
            }
            return true;
        }

        public static bool IsViewable(string strClsName, DEMetaAttribute attr) {
            if ((attr.Option & 0x40) > 0) {
                return false;
            }
            return true;
        }

        public void LoadOCXByClsName(string str_clsname, bool isclsset) {
            this._isClsAttrSet = isclsset;
            this._className = str_clsname;
            this.SetNameOcxLst();
            this.isLoading = false;
        }

        public void LoadOCXByItem(DEConditionItem theItem) {
            string attrValue = "";
            if (theItem.AttrValue != null) {
                attrValue = theItem.AttrValue.ToString();
            }
            this.chkParameter.Checked = theItem.IsParam;
            this.LoadOCXByItem(theItem.ClassName, theItem.ConditionAttrName.IsClassAttr, theItem.Oid, theItem.ClassPosition, theItem.ConditionAttrName.GetAttrName(), theItem.AttrType, theItem.Operator, attrValue, theItem.Option);
        }

        public void LoadOCXByItem(string className, bool isClass, Guid Oid, Guid ClassPosition, string attrName, ItemMatchType itp, OperatorType oper, object attrValue, int option) {
            this._isClsAttrSet = isClass;
            this._Oid = Oid;
            this._position = ClassPosition;
            if (!className.Equals(this._className)) {
                this._className = className;
                this.SetNameOcxLst();
            }
            for (int i = 0; i < this._attrList.Count; i++) {
                GenericAttribute attribute = (GenericAttribute)this._attrList[i];
                if (((attrName == attribute.Name) && ((itp != ItemMatchType.Master) || (attribute.Category == "M"))) && (((itp != ItemMatchType.Revision) || (attribute.Category == "R")) && ((itp != ItemMatchType.Iteration) || (attribute.Category == "I")))) {
                    this.cb_name.SelectedIndex = i;
                    this._option = option;
                    this.SetOption(attribute.DataType, option);
                    break;
                }
            }
            for (int j = 0; j < this._operList.Count; j++) {
                PLMOperator @operator = this._operList[j] as PLMOperator;
                if (@operator.OperType == oper) {
                    this.cb_operator.SelectedIndex = j;
                    break;
                }
            }
            if (this.panel_value.Controls.Count != 0) {
                if (this.ckbLoginoid.Visible) {
                    if ((attrValue != null) && (attrValue.ToString() == "<USEROID>")) {
                        this.ckbLoginoid.Checked = true;
                        return;
                    }
                    this.ckbLoginoid.Checked = false;
                }
                if (this.panel_value.Controls[0] is IDInputPLM) {
                    ((IDInputPLM)this.panel_value.Controls[0]).Text = PLSystemParam.ParameterIDCaseSensitive ? Convert.ToString(attrValue).Trim() : Convert.ToString(attrValue).ToUpper().Trim();
                }
                if (this.panel_value.Controls[0] is ProjectInputPLM) {
                    ((ProjectInputPLM)this.panel_value.Controls[0]).SetInput(new Guid(attrValue.ToString()));
                }
                if ((this.panel_value.Controls[0] is UCSelectProcessTemplatePLM) && (this._curAttr.Attach != null)) {
                    if ((attrValue == null) || string.IsNullOrEmpty(attrValue.ToString())) {
                        return;
                    }
                    DEMetaAttribute attach = this._curAttr.Attach as DEMetaAttribute;
                    if (attach.DataType2 == PLMDataType.Guid) {
                        Guid theProcessDefinitionID = new Guid(attrValue.ToString());
                        if (attach.SpecialType > 0) {
                            DELProcessDefProperty property;
                            new BPMProcessor().GetProcessDefProperty(ClientData.LogonUser.Oid, theProcessDefinitionID, out property);
                            if (property != null) {
                                this.panel_value.Controls[0].Text = property.Name;
                                this.panel_value.Controls[0].Tag = property;
                            }
                        }
                    } else if (attach.DataType2 == PLMDataType.String) {
                        this.panel_value.Controls[0].Text = attrValue.ToString();
                    }
                }
                if (this.panel_value.Controls[0] is ResComboPLM) {
                    ((ResComboPLM)this.panel_value.Controls[0]).Text = Convert.ToString(attrValue);
                }
                if (this.panel_value.Controls[0] is UCSelectPrinPLM) {
                    if (this._curAttr.Attach == null) {
                        if ((attrValue == null) || string.IsNullOrEmpty(attrValue.ToString())) {
                            return;
                        }
                        DEUser userByOid = new DEUser();
                        userByOid = new PLUser().GetUserByOid(new Guid(attrValue.ToString()));
                        ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = userByOid.Name + "(" + userByOid.LogId + ")";
                        ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = userByOid;
                    } else {
                        if ((attrValue == null) || string.IsNullOrEmpty(attrValue.ToString())) {
                            return;
                        }
                        DEMetaAttribute attribute3 = this._curAttr.Attach as DEMetaAttribute;
                        if (attribute3.SpecialType <= 0) {
                            DEUser user4 = new DEUser();
                            user4 = new PLUser().GetUserByOid(new Guid(attrValue.ToString()));
                            ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = user4.Name + "(" + user4.LogId + ")";
                            ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = user4;
                        } else {
                            if (attribute3.SpecialType2 == PLMSpecialType.UserType) {
                                DEUser logonUser = new DEUser();
                                PLUser user2 = new PLUser();
                                if (attribute3.DataType2 == PLMDataType.Guid) {
                                    if (attrValue.ToString() == "<USEROID>") {
                                        logonUser = ClientData.LogonUser;
                                    } else {
                                        logonUser = user2.GetUserByOid(new Guid(attrValue.ToString()));
                                    }
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = logonUser.Name + "(" + logonUser.LogId + ")";
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = logonUser;
                                }
                                if (attribute3.DataType2 == PLMDataType.String) {
                                    string str = Convert.ToString(attrValue);
                                    if (str == "<USEROID>") {
                                        logonUser = ClientData.LogonUser;
                                        attrValue = logonUser.Name + "(" + logonUser.LogId + ")";
                                    } else {
                                        ArrayList allUsers = user2.GetAllUsers();
                                        if (allUsers != null) {
                                            foreach (DEUser user3 in allUsers) {
                                                if ((user3.Name + "(" + user3.LogId + ")") == str) {
                                                    logonUser = user3;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = attrValue.ToString();
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = logonUser;
                                }
                            }
                            if (attribute3.SpecialType2 == PLMSpecialType.RoleType) {
                                DERole roleByOid = new DERole();
                                PLRole role2 = new PLRole();
                                if (attribute3.DataType2 == PLMDataType.Guid) {
                                    roleByOid = role2.GetRoleByOid(new Guid(attrValue.ToString()));
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = roleByOid.Name;
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = roleByOid;
                                }
                                if (attribute3.DataType2 == PLMDataType.String) {
                                    string str3 = Convert.ToString(attrValue);
                                    ArrayList allRoles = role2.GetAllRoles();
                                    if (allRoles != null) {
                                        foreach (DERole role3 in allRoles) {
                                            if (role3.Name == str3) {
                                                roleByOid = role3;
                                                break;
                                            }
                                        }
                                    }
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = attrValue.ToString();
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = roleByOid;
                                }
                            }
                            if (attribute3.SpecialType2 == PLMSpecialType.OrganizationType) {
                                DEOrganization orgByOid = new DEOrganization();
                                PLOrganization organization2 = new PLOrganization();
                                if (attribute3.DataType2 == PLMDataType.Guid) {
                                    orgByOid = organization2.GetOrgByOid(new Guid(attrValue.ToString()));
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = orgByOid.Name;
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = orgByOid;
                                }
                                if (attribute3.DataType2 == PLMDataType.String) {
                                    string str4 = Convert.ToString(attrValue);
                                    ArrayList allOrganizations = organization2.GetAllOrganizations();
                                    if (allOrganizations != null) {
                                        foreach (DEOrganization organization3 in allOrganizations) {
                                            if (organization3.Name == str4) {
                                                orgByOid = organization3;
                                                break;
                                            }
                                        }
                                    }
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Text = attrValue.ToString();
                                    ((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag = orgByOid;
                                }
                            }
                        }
                    }
                }
                if (this.panel_value.Controls[0] is UCSelectClassPLM) {
                    this.panel_value.Controls.Clear();
                    UCSelectClassPLM splm = new UCSelectClassPLM(this._className, false, SelectClassConstraint.BusinessItemClass);
                    splm.SetValue(attrValue.ToString());
                    splm.Dock = DockStyle.Fill;
                    this.panel_value.Controls.Add(splm);
                }
                if (this.panel_value.Controls[0] is TextEditPLM) {
                    string str5 = Convert.ToString(attrValue);
                    if (!string.IsNullOrEmpty(str5)) {
                        str5 = str5.Trim();
                    }
                    ((TextEditPLM)this.panel_value.Controls[0]).Text = str5;
                }
                if (this.panel_value.Controls[0] is ComboBoxEditPLM) {
                    if ((this._curAttr.Name == "STATE") && (this._curAttr.Category == "M")) {
                        if ((attrValue != null) && (attrValue.ToString() != "")) {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = this.GetTypeShow(Convert.ToChar(attrValue));
                        }
                    } else if (attrName == "STATE") {
                        this.SetStateCtrl(attrValue);
                    } else if (attrName == "EFFECTIVE") {
                        this.SetEffective(attrValue);
                    } else if (attrName == "STATUS") {
                        this.SetStatusValue(attrValue);
                    } else if (attrName == "VIEW") {
                        this.SetViewCtrlValue(attrValue);
                    } else if (attrName == "GENDER") {
                        this.SetGENDERCtrl(attrValue);
                    } else if (this._curAttr.DataType == PLMDataType.Char) {
                        if (attrValue.ToString().ToUpper() == "Y".ToUpper()) {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = "是";
                            this._attrValue = "Y";
                        } else if (attrValue.ToString().ToUpper() == "N".ToUpper()) {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = "否";
                            this._attrValue = "N";
                        } else {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = attrValue.ToString().Trim();
                            this._attrValue = attrValue.ToString().Trim();
                        }
                    } else if (this._curAttr.DataType == PLMDataType.Bool) {
                        if (attrValue.ToString().ToUpper() == "True".ToUpper()) {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = "是";
                            this._attrValue = Convert.ToString(true);
                        } else if (attrValue.ToString().ToUpper() == "False".ToUpper()) {
                            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = "否";
                            this._attrValue = Convert.ToString(false);
                        }
                    }
                }
                if (this.panel_value.Controls[0] is DateEditPLM) {
                    DateTime time = Convert.ToDateTime(attrValue);
                    time = new DateTime(time.Year, time.Month, time.Day);
                    DateEditPLM tplm = (DateEditPLM)this.panel_value.Controls[0];
                    tplm.Value = time;
                    try {
                        tplm.Text = time.ToString(tplm.Properties.DisplayFormat.FormatString);
                    } catch (Exception) {
                        tplm.Text = time.ToString();
                    }
                }
                this.isLoading = false;
            }
        }

        public void LocationByName(string strLabel) {
            if (this.cb_name.Properties.Items.Count != 0) {
                for (int i = 0; i < this.cb_name.Properties.Items.Count; i++) {
                    if (this.cb_name.Properties.Items[i].ToString() == strLabel) {
                        this.cb_name.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private void Man_KeyPress(object sender, KeyPressEventArgs e) {
            this.GetManValue();
            this.isStarting = false;
        }

        private void Man_SelectedIndexChanged(object sender, EventArgs e) {
            this.GetManValue();
        }

        private void num_value_KeyUp(object sender, KeyEventArgs e) {
            string text = this.panel_value.Controls[0].Text;
            if (string.IsNullOrEmpty(text)) {
                this._attrValue = "";
            } else {
                if (this._curAttr.DataType == PLMDataType.Int16) {
                    short num;
                    try {
                        num = Convert.ToInt16(text);
                    } catch {
                        MessageBoxPLM.Show("输入内容不是短整数");
                        this.panel_value.Controls[0].Text = "0";
                        this._attrValue = 0;
                        return;
                    }
                    this._attrValue = num;
                }
                if (this._curAttr.DataType == PLMDataType.Int32) {
                    int num2;
                    try {
                        num2 = Convert.ToInt32(text);
                    } catch {
                        MessageBoxPLM.Show("输入内容不是整数");
                        this.panel_value.Controls[0].Text = "0";
                        this._attrValue = 0;
                        return;
                    }
                    this._attrValue = num2;
                }
                if (this._curAttr.DataType == PLMDataType.Int64) {
                    long num3;
                    try {
                        num3 = Convert.ToInt64(text);
                    } catch {
                        MessageBoxPLM.Show("输入内容不是长整数");
                        this.panel_value.Controls[0].Text = "0";
                        this._attrValue = 0;
                        return;
                    }
                    this._attrValue = num3;
                }
                if (this._curAttr.DataType == PLMDataType.Decimal) {
                    decimal num4;
                    try {
                        num4 = Convert.ToDecimal(text);
                    } catch {
                        MessageBoxPLM.Show("输入内容不是数字");
                        this.panel_value.Controls[0].Text = "0";
                        this._attrValue = 0;
                        return;
                    }
                    this._attrValue = num4;
                }
                this.isStarting = false;
            }
        }

        private void num_value_ValueChanged(object sender, EventArgs e) {
            string text = this.panel_value.Controls[0].Text;
            if (string.IsNullOrWhiteSpace(text)) {
                this._attrValue = "";
            } else {
                try {
                    Convert.ToDecimal(text);
                } catch {
                    this.panel_value.Controls[0].Text = "0";
                    this._attrValue = 0;
                    return;
                }
                if (this._curAttr.DataType == PLMDataType.Int16) {
                    this._attrValue = Convert.ToInt16(text);
                }
                if (this._curAttr.DataType == PLMDataType.Int32) {
                    this._attrValue = Convert.ToInt32(text);
                }
                if (this._curAttr.DataType == PLMDataType.Int64) {
                    this._attrValue = Convert.ToInt64(text);
                }
                if (this._curAttr.DataType == PLMDataType.Decimal) {
                    this._attrValue = Convert.ToDecimal(text);
                }
                this.isStarting = false;
            }
        }

        private void Process_KeyPress(object sender, KeyPressEventArgs e) {
            this.GetValueOfProcess();
            this.isStarting = false;
        }

        private void Process_SelectedIndexChanged(object sender, EventArgs e) {
            this.GetValueOfProcess();
            this.isStarting = false;
        }

        private void SetEffective(object attrValue) {
            string str = attrValue.ToString();
            string str2 = "";
            switch (str) {
                case "0":
                    str2 = "始终有效";
                    this._attrValue = 0;
                    break;

                case "指定时间内有效":
                    str2 = "冻结";
                    this._attrValue = 1;
                    break;

                case "2":
                    str2 = "指定序号范围内有效";
                    this._attrValue = 2;
                    break;
            }
            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = str2;
        }

        private void SetGENDERCtrl(object attrValue) {
            string str = attrValue.ToString();
            string str2 = "";
            switch (str) {
                case "M":
                case "男":
                    str2 = "男";
                    break;

                default:
                    str2 = "女";
                    break;
            }
            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = str2;
            this._attrValue = str2;
        }

        private void SetNameOcxLst() {
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
            if (this._isClsAttrSet) {
                elements = (GenericAttribute[])GetClassSchGenericAttr(this._className).ToArray(typeof(GenericAttribute));
            } else {
                elements = (GenericAttribute[])GetRelationSchGenericAttr(this._className).ToArray(typeof(GenericAttribute));
            }
            if (elements != null) {
                GenericAttribute[] c = this.FilterAttrs(elements);
                this._attrList.AddRange(c);
            }
            foreach (GenericAttribute attribute in this._attrList) {
                this.cb_name.Properties.Items.Add(attribute.Label.ToString());
            }
        }

        private void SetOperatorOcxLst(PLMDataType dataType) {
            this.cb_operator.Properties.Items.Clear();
            this.cb_operator.Text = "";
            this._operList.Clear();
            this._operList = SelectionOperator.GetOperator(dataType);
            foreach (PLMOperator @operator in this._operList) {
                this.cb_operator.Properties.Items.Add(@operator.ToString());
            }
            if (this.cb_operator.Properties.Items.Count > 0) {
                this.cb_operator.Text = this.cb_operator.Properties.Items[0].ToString();
            }
        }

        private void SetOption(PLMDataType dataType, int option) {
            if (((dataType == PLMDataType.Char) || (dataType == PLMDataType.Card)) || ((dataType == PLMDataType.Clob) || (dataType == PLMDataType.String))) {
                this.chkCaseSensitive.Visible = true;
                if (option == 0) {
                    this.chkCaseSensitive.Checked = true;
                } else {
                    this.chkCaseSensitive.Checked = (option & 1) > 0;
                }
            } else {
                this.chkCaseSensitive.Visible = false;
                this.chkCaseSensitive.Checked = false;
            }
        }

        private void SetProcessValue(object attrValue) {
            string str = attrValue.ToString();
            if (!string.IsNullOrEmpty(str)) {
                GetProcessList();
                foreach (DELProcessDefProperty property in TheProcessList) {
                    if (property.ID.ToString() == str) {
                        this._attrValue = str;
                        ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = property.Name;
                        break;
                    }
                }
            }
        }

        private void SetStateCtrl(object attrValue) {
            string str = "";
            if (attrValue != null) {
                string str2 = attrValue.ToString();
                this._attrValue = attrValue.ToString();
                if (!string.IsNullOrEmpty(str2)) {
                    if (str2 == "Initial") {
                        str = "初始化";
                    } else if (str2 == "Running") {
                        str = "运行中";
                    } else if (str2 == "Completed") {
                        str = "已完成";
                    } else if (str2 == "Aborted") {
                        str = "已取消";
                    } else if (str2 == "Suspended") {
                        str = "暂停中";
                    } else if (str2 == "Assigned") {
                        str = "已分配";
                    } else if (str2 == "Accepted") {
                        str = "已接受";
                    } else if (str2 == "P") {
                        str = "已发布";
                    } else if (str2 == "U") {
                        str = "未发布";
                    } else if (str2 == "初始化") {
                        str = "初始化";
                    } else if (str2 == "运行中") {
                        str = "运行中";
                    } else if (str2 == "已完成") {
                        str = "已完成";
                    } else if (str2 == "已取消") {
                        str = "已取消";
                    } else if (str2 == "已挂起") {
                        str = "已挂起";
                    }
                }
                if (!string.IsNullOrEmpty(str)) {
                    ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = str;
                }
            }
        }

        private void SetStatusValue(object attrValue) {
            string str = attrValue.ToString();
            string str2 = "";
            if (string.IsNullOrEmpty(str)) {
                this._attrValue = "";
                str2 = "";
            }
            if (str == "A") {
                str2 = "可用";
                this._attrValue = "A";
            } else if (str == "F") {
                str2 = "冻结";
                this._attrValue = "F";
            } else if (str == "0") {
                str2 = "计划中";
                this._attrValue = 0;
            } else if (str == "运行中") {
                str2 = "冻结";
                this._attrValue = 1;
            } else if (str == "2") {
                str2 = "审核中";
                this._attrValue = 2;
            } else if (str == "3") {
                str2 = "暂停中";
                this._attrValue = 3;
            }
            if (str == "4") {
                str2 = "已完成";
                this._attrValue = 4;
            } else if (str == "5") {
                str2 = "已取消";
                this._attrValue = 5;
            }
            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = str2;
        }

        private void SetValueOCX(GenericAttribute deattr) {
            this.panel_value.Controls.Clear();
            if ((deattr.Category == "I") || (deattr.Category == "S")) {
                if (deattr.Attach != null) {
                    DEMetaAttribute attach = (DEMetaAttribute)deattr.Attach;
                    if (((attach.Combination != null) && (attach.Combination != "")) && (attach.LinkedResClass != Guid.Empty)) {
                        ResComboPLM oplm = new ResComboPLM(attach.LinkedResClass, attach);
                        this.panel_value.Controls.Add(oplm);
                        oplm.Dock = DockStyle.Fill;
                        oplm.Text = "";
                    } else if (attach.SpecialType > 0) {
                        this.CreateSpecOcx(attach);
                    } else if (FixedAttribute.IsUserAttribute(attach.Name) && (attach.DataType2 == PLMDataType.Guid)) {
                        this.InitHumanUC();
                    } else {
                        this.CreateOcx(deattr);
                    }
                }
            } else if (FixedAttribute.IsUserAttribute(deattr.Name)) {
                this.InitHumanUC();
            } else if ((deattr.Name == "CLASS") && (deattr.Category == "M")) {
                this.InitObjectClsUC();
            } else if ((deattr.Name == "STATE") && (deattr.Category == "M")) {
                this.InitTyptUC();
            } else if ((deattr.Category == "M") && (deattr.Name == "ID")) {
                this.InitIDCtrl();
            } else if ((deattr.Category == "M") && (deattr.Name == "GROUP")) {
                this.InitProjectCtrl();
            } else {
                this.CreateOcx(deattr);
            }
        }

        private void SetViewCtrlValue(object attrValue) {
            this.GetViews();
            string str = attrValue.ToString();
            this._attrValue = str;
            string label = "";
            if (!string.IsNullOrEmpty(str)) {
                for (int i = 0; i < lstView.Count; i++) {
                    DEView view = lstView[i] as DEView;
                    if (view.GetName() == str) {
                        label = view.Label;
                        break;
                    }
                }
            } else {
                this._attrValue = "";
            }
            ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text = label;
        }

        private void State_KeyPress(object sender, KeyPressEventArgs e) {
            if ((this._className == "BPM_PROCESS_INSTANCE") && this.IsSearchDefine) {
                this.GetProcessInstanceStateValue();
            } else {
                this.GetStateValue();
            }
            this.isStarting = false;
        }

        private void State_SelectedIndexChanged(object sender, EventArgs e) {
            if ((this._className == "BPM_PROCESS_INSTANCE") && this.IsSearchDefine) {
                this.GetProcessInstanceStateValue();
            } else {
                this.GetStateValue();
            }
        }

        private void Status_KeyPress(object sender, KeyPressEventArgs e) {
            this.GetStatusValue();
            this.isStarting = false;
        }

        private void Status_SelectedIndexChanged(object sender, EventArgs e) {
            this.GetStatusValue();
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e) {
            if ((this._curAttr.DataType == PLMDataType.Char) || (this._curAttr.DataType == PLMDataType.String)) {
                this._attrValue = ((TextEditPLM)this.panel_value.Controls[0]).Text;
            }
            if (this._curAttr.DataType == PLMDataType.DateTime) {
                this._attrValue = ((DateEditPLM)this.panel_value.Controls[0]).Value;
            }
            if (this._curAttr.DataType == PLMDataType.Bool) {
                if (((ComboBoxEditPLM)this.panel_value.Controls[0]).Text == "是") {
                    this._attrValue = Convert.ToString(true);
                }
                if (((ComboBoxEditPLM)this.panel_value.Controls[0]).Text == "否") {
                    this._attrValue = Convert.ToString(false);
                } else {
                    this._attrValue = null;
                }
            }
            this.isStarting = false;
        }

        private void View_KeyPress(object sender, KeyPressEventArgs e) {
            this.GetViewsValue();
            this.isStarting = false;
        }

        private void View_SelectedIndexChanged(object sender, EventArgs e) {
            this.GetViewsValue();
        }

        public bool AllowParameteDefine {
            get {
                return
                this.chkParameter.Visible;
            }
            set {
                this.chkParameter.Visible = value;
            }
        }

        public string AttrName {
            get {
                return
                    this._attrName.GetAttrName();
            }
        }
        public bool AttrNameEnable {
            get {
                return
                this.cb_name.Enabled;
            }
            set {
                this.cb_name.Enabled = value;
            }
        }

        public string AttrText {
            get {
                if (this.cb_name.Text.Trim().Length == 0) {
                    return null;
                }
                return this.cb_name.Text;
            }
        }

        public object AttrValue {
            get {
                if (this.panel_value.Controls.Count == 0) {
                    return null;
                }
                if (this.ckbLoginoid.Visible && this.ckbLoginoid.Checked) {
                    this._attrValue = "<USEROID>";
                    return this._attrValue;
                }
                if (this.panel_value.Controls[0] is ResComboPLM) {
                    this._attrValue = ((ResComboPLM)this.panel_value.Controls[0]).ResValue;
                }
                if (this.panel_value.Controls[0] is IDInputPLM) {
                    this._attrValue = ((IDInputPLM)this.panel_value.Controls[0]).Text.Trim();
                }
                if (this.panel_value.Controls[0] is ProjectInputPLM) {
                    if (((ProjectInputPLM)this.panel_value.Controls[0]).GetProjectValue() == Guid.Empty) {
                        this._attrValue = null;
                    } else {
                        this._attrValue = ((ProjectInputPLM)this.panel_value.Controls[0]).GetProjectValue();
                    }
                }
                if (this.panel_value.Controls[0] is UCSelectProcessTemplatePLM) {
                    if (((UCSelectProcessTemplatePLM)this.panel_value.Controls[0]).Tag == null) {
                        return null;
                    }
                    DELProcessDefProperty tag = (DELProcessDefProperty)this.panel_value.Controls[0].Tag;
                    if (this._curAttr.DataType == PLMDataType.Guid) {
                        this._attrValue = tag.ID.ToString();
                    }
                    if (this._curAttr.DataType == PLMDataType.String) {
                        this._attrValue = tag.Name;
                    }
                }
                if (this.panel_value.Controls[0] is UCSelectPrinPLM) {
                    if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag == null) {
                        return null;
                    }
                    if (this._curAttr.Attach != null) {
                        DEMetaAttribute attach = this._curAttr.Attach as DEMetaAttribute;
                        if (attach.SpecialType > 0) {
                            if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag is DEUser) {
                                DEUser user = (DEUser)((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag;
                                if (this._curAttr.DataType == PLMDataType.Guid) {
                                    this._attrValue = user.Oid;
                                }
                                if (this._curAttr.DataType == PLMDataType.String) {
                                    this._attrValue = user.Name + "(" + user.LogId + ")";
                                }
                            }
                            if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag is DERole) {
                                DERole role = (DERole)((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag;
                                if (this._curAttr.DataType == PLMDataType.Guid) {
                                    this._attrValue = role.Oid;
                                }
                                if (this._curAttr.DataType == PLMDataType.String) {
                                    this._attrValue = role.Name;
                                }
                            }
                            if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag is DEOrganization) {
                                DEOrganization organization = (DEOrganization)((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag;
                                if (this._curAttr.DataType == PLMDataType.Guid) {
                                    this._attrValue = organization.Oid;
                                }
                                if (this._curAttr.DataType == PLMDataType.String) {
                                    this._attrValue = organization.Name;
                                }
                            }
                        } else {
                            if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag == null) {
                                return null;
                            }
                            DEUser user2 = (DEUser)((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag;
                            this._attrValue = user2.Oid.ToString();
                        }
                    } else {
                        if (((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag == null) {
                            return null;
                        }
                        DEUser user3 = (DEUser)((UCSelectPrinPLM)this.panel_value.Controls[0]).Tag;
                        this._attrValue = user3.Oid.ToString();
                    }
                }
                if (this.panel_value.Controls[0] is UCSelectClassPLM) {
                    if (((UCSelectClassPLM)this.panel_value.Controls[0]).Tag == null) {
                        return null;
                    }
                    DEMetaClass class2 = (DEMetaClass)((UCSelectClassPLM)this.panel_value.Controls[0]).Tag;
                    this._attrValue = class2.Name;
                }
                if (this.panel_value.Controls[0] is TextEditPLM) {
                    this._attrValue = ((TextEditPLM)this.panel_value.Controls[0]).Text.Trim();
                }
                if (this.panel_value.Controls[0] is ComboBoxEditPLM) {
                    DEMeta meta = this.GetClass();
                    if (meta == null) {
                        return null;
                    }
                    DEMetaClass class3 = meta as DEMetaClass;
                    DEMetaRelation relation = meta as DEMetaRelation;
                    if ((this._className == "BPM_PROCESS_INSTANCE") && (this._curAttr.Name == "PROCESSDEFINITIONID")) {
                        this.GetValueOfProcess();
                        return this._attrValue;
                    }
                    if (((this._curAttr.Name == "STATE") && (class3 != null)) && (class3.IsResClass || (class3.SystemClass == 'Y'))) {
                        if ((this._className == "BPM_PROCESS_INSTANCE") && this.IsSearchDefine) {
                            this.GetProcessInstanceStateValue();
                        } else {
                            this.GetStateValue();
                        }
                        return this._attrValue;
                    }
                    if (this._curAttr.Name == "EFFECTIVE") {
                        this.GetEffecValue();
                        return this._attrValue;
                    }
                    if (((class3 != null) && (class3.SystemClass == 'Y')) && (this._curAttr.Name == "STATUS")) {
                        this.GetStatusValue();
                        return this._attrValue;
                    }
                    if (this._curAttr.Name == "VIEW") {
                        if (relation != null) {
                            this.GetViewsValue();
                            return this._attrValue;
                        }
                    } else {
                        if (this._curAttr.Name == "GENDER") {
                            this.GetManValue();
                            return this._attrValue;
                        }
                        if ((this._curAttr.Name == "STATE") && (this._curAttr.Category == "M")) {
                            string typeValue = this.GetTypeValue(((ComboBoxEditPLM)this.panel_value.Controls[0]).Text);
                            if (typeValue == string.Empty) {
                                return null;
                            }
                            this._attrValue = typeValue[0];
                        } else {
                            if (this._curAttr.DataType == PLMDataType.Char) {
                                this.GetCharValue();
                                return this._attrValue;
                            }
                            if (this._curAttr.DataType != PLMDataType.Bool) {
                                this._attrValue = ((ComboBoxEditPLM)this.panel_value.Controls[0]).Text;
                            } else if (this._isSchDefine && this.isStarting) {
                                return null;
                            }
                        }
                    }
                }
                if (this.panel_value.Controls[0] is DateEditPLM) {
                    if (this._isSchDefine && this.isStarting) {
                        return null;
                    }
                    this._attrValue = ((DateEditPLM)this.panel_value.Controls[0]).Value.ToString("yyyy-MM-dd");
                }
                if (this.panel_value.Controls[0] is SpinEditPLM) {
                    if (this._isSchDefine && this.isStarting) {
                        return null;
                    }
                    if (string.IsNullOrEmpty(this.panel_value.Controls[0].Text)) {
                        return null;
                    }
                    this._attrValue = ((SpinEditPLM)this.panel_value.Controls[0]).Value;
                }
                return this._attrValue;
            }
        }

        public ConditionAttributeName ConditionAttrName {
            get {
                if (this._attrName == null) {
                    return null;
                }
                return this._attrName;
            }
        }

        public DEConditionItem ConditionItem {
            get {
                DEConditionItem item = new DEConditionItem(this._attrName, this._operType, this.AttrValue) {
                    Oid = this._Oid,
                    ClassPosition = this._position,
                    Option = this._option
                };
                if (this.chkParameter.Visible) {
                    item.IsParam = this.chkParameter.Checked;
                }
                return item;
            }
        }

        public bool IsParamterItem {
            get {
                return
                    this.chkParameter.Checked;
            }
        }

        public bool IsSearchDefine {
            get {
                return
                this._isSchDefine;
            }
            set {
                this._isSchDefine = value;
            }
        }

        public Guid Oid {
            get {
                return
                    this._Oid;
            }
            set {
                this._Oid = value;
            }
        }

        public OperatorType Operator {
            get {
                return
                    this._operType;
            }
        }
        public int Option {
            get {
                return
                    this._option;
            }
        }
        public Guid Position {
            get {
                return
                this._position;
            }
            set {
                this._position = value;
            }
        }
    }
}

