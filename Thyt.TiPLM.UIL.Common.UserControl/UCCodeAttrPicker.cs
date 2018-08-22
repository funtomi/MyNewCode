using DevExpress.XtraEditors.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCCodeAttrPicker : UserControlPLM {
        private ArrayList AL_unCodeMetaAttr;
        private bool b_isAdd;
        private bool b_start;
        private DECodeAttribute ca_input;
        private Guid ClassOid;

        public event CodeAttrDropListHandler CodeAttrSelected;

        public UCCodeAttrPicker() {
            this.b_start = true;
            this.b_isAdd = true;
            this.InitializeComponent();
        }

        public UCCodeAttrPicker(DECodeAttribute ca_in)
            : this() {
            this.ca_input = ca_in;
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            if (this.CodeAttrSelected != null) {
                DECodeAttribute codeAttrValue = this.GetCodeAttrValue();
                this.CodeAttrSelected(codeAttrValue);
            }
            base.Parent.Hide();
        }

        private void comBoxAttrName_SelectedIndexChanged(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(this.comBoxAttrName.Text)) {
                string text = this.comBoxAttrName.Text;
                this.pnlValue.Controls.Clear();
                ComboBoxEditPLM tplm = new ComboBoxEditPLM();
                DEMetaAttribute metaAttr = this.GetMetaAttr(text);
                if (metaAttr != null) {
                    DEMetaClass myCls = ModelContext.MetaModel.GetClass(metaAttr.LinkedResClass);
                    if (myCls != null) {
                        ResFunc func = new ResFunc();
                        foreach (DEMetaAttribute attribute2 in func.GetResAttrs(myCls)) {
                            if ((!attribute2.IsSystem && (attribute2.DataType2 == PLMDataType.String)) && (attribute2.Name != "OID")) {
                                tplm.Properties.Items.Add(attribute2.Label);
                            }
                        }
                        if (tplm.Properties.Items.Count > 0) {
                            this.pnlValue.Controls.Add(tplm);
                            tplm.Dock = DockStyle.Fill;
                        }
                    }
                }
            }
        }

        private DECodeAttribute GetCodeAttrValue() {
            string text = this.comBoxAttrName.Text;
            DEMetaAttribute metaAttr = this.GetMetaAttr(text);
            if (metaAttr != null) {
                if (this.pnlValue.Controls.Count == 0) {
                    this.ca_input.ClassOid = this.ClassOid;
                    this.ca_input.ClassAttrOid = metaAttr.Oid;
                    this.ca_input.ResAttrOid = Guid.Empty;
                    this.ca_input.ResClsOid = Guid.Empty;
                    return this.ca_input;
                }
                ComboBoxEditPLM tplm = this.pnlValue.Controls[0] as ComboBoxEditPLM;
                string str2 = tplm.Text;
                DEMetaClass myCls = ModelContext.MetaModel.GetClass(metaAttr.LinkedResClass);
                ArrayList resAttrs = new ResFunc().GetResAttrs(myCls);
                if (resAttrs == null) {
                    return null;
                }
                if (resAttrs.Count == 0) {
                    return null;
                }
                foreach (DEMetaAttribute attribute2 in resAttrs) {
                    if (attribute2.Label == str2) {
                        this.ca_input.ClassOid = this.ClassOid;
                        this.ca_input.ClassAttrOid = metaAttr.Oid;
                        this.ca_input.ResAttrOid = attribute2.Oid;
                        this.ca_input.ResClsOid = myCls.Oid;
                        return this.ca_input;
                    }
                }
            }
            return null;
        }

        private DEMetaAttribute GetMetaAttr(string strAttrLbl) {
            foreach (DEMetaAttribute attribute in this.AL_unCodeMetaAttr) {
                if (attribute.Label == strAttrLbl) {
                    return attribute;
                }
            }
            if (!this.b_isAdd) {
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.ClassOid);
                if (class2 == null) {
                    foreach (DEMetaAttribute attribute2 in ModelContext.MetaModel.GetAttributes(class2.Name)) {
                        if (attribute2.Label == strAttrLbl) {
                            return attribute2;
                        }
                    }
                }
            }
            return null;
        }


        private void LoadUnCodeMetaAttr() {
            this.comBoxAttrName.Properties.Items.Clear();
            if ((this.AL_unCodeMetaAttr != null) && (this.AL_unCodeMetaAttr.Count != 0)) {
                int num = -1;
                int num2 = -1;
                foreach (DEMetaAttribute attribute in this.AL_unCodeMetaAttr) {
                    this.comBoxAttrName.Properties.Items.Add(attribute.Label);
                    num2++;
                    if (!this.b_isAdd && (attribute.Oid == this.ca_input.ClassAttrOid)) {
                        num = num2;
                    }
                }
                if (num != -1) {
                    this.comBoxAttrName.SelectedIndex = num;
                }
            }
        }

        public void ReLoad(Guid ClsOid, ArrayList al_unCodeMetaAttr, DECodeAttribute ca_in, bool isAdd) {
            this.ClassOid = ClsOid;
            this.AL_unCodeMetaAttr = al_unCodeMetaAttr;
            this.b_isAdd = isAdd;
            this.ca_input = ca_in;
            this.LoadUnCodeMetaAttr();
            this.SetCodeAttrValue(ca_in);
        }

        private void SetCodeAttrValue(DECodeAttribute ca_def) {
            if ((ca_def != null) && (ca_def.ResClsOid != Guid.Empty)) {
                DEMetaClass myCls = ModelContext.MetaModel.GetClass(ca_def.ResClsOid);
                if (myCls != null) {
                    ArrayList resAttrs = new ResFunc().GetResAttrs(myCls);
                    if (((resAttrs != null) && (resAttrs.Count != 0)) && (this.pnlValue.Controls.Count != 0)) {
                        ComboBoxEditPLM tplm = this.pnlValue.Controls[0] as ComboBoxEditPLM;
                        foreach (DEMetaAttribute attribute in resAttrs) {
                            if (attribute.Oid == ca_def.ResAttrOid) {
                                tplm.Text = attribute.Label;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}

