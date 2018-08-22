namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Common;

    public class CodeAttrInput : UltraTextEditor
    {
        private ArrayList AL_unCodeMetaAttr;
        private bool b_IsAdd = true;
        private bool b_ReadOnly;
        private DECodeAttribute ca_input;
        private Guid ClassOid;
        private Container components;
        private CodeAttrDropListHandler dlhandler;
        private DEMetaClass theCls;
        private UCCodeAttrPicker ucUser;

        public event SelectCodeAttrHandler CodeAttrTextChanged;

        public event CodeAttrDropListHandler DropListChanged;

        public CodeAttrInput()
        {
            this.InitializeComponent();
            this.InitializeConfig();
        }

        private void CodeAttrCombo_BeforeDropDown(object sender, BeforeEditorButtonDropDownEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ucUser.ReLoad(this.ClassOid, this.AL_unCodeMetaAttr, this.ca_input, this.b_IsAdd);
            Cursor.Current = Cursors.Default;
        }

        private void CodeAttrCombo_TextChanged(object sender, EventArgs e)
        {
            if (this.CodeAttrTextChanged != null)
            {
                this.CodeAttrTextChanged(this.ca_input);
            }
        }

        private void CodeAttrComboInput_KeyUp(object sender, KeyEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (this.dlhandler != null)
            {
                this.ucUser.CodeAttrSelected -= this.dlhandler;
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private string GetClsAttr2ResAttrLbl(DECodeAttribute ca_in)
        {
            ArrayList list = new ArrayList();
            new ArrayList();
            new ArrayList();
            new ArrayList();
            if (this.theCls != null)
            {
                foreach (DEMetaAttribute attribute in PLDataModel.Agent2.GetClassAllAttributes(this.theCls.Name))
                {
                    if ((attribute.IsViewable && !attribute.IsGrid) && !attribute.IsVirtualClass)
                    {
                        list.Add(attribute);
                    }
                }
                if (list.Count == 0)
                {
                    return "";
                }
                foreach (DEMetaAttribute attribute2 in list)
                {
                    if (attribute2.Oid == ca_in.ClassAttrOid)
                    {
                        DEMetaClass metaClass = PLDataModel.Agent2.GetMetaClass(ca_in.ResClsOid);
                        List<DEMetaAttribute> list3 = new List<DEMetaAttribute>();
                        if (metaClass != null)
                        {
                            ArrayList resAttrs = new ResFunc().GetResAttrs(metaClass);
                            if ((resAttrs != null) && (resAttrs.Count > 0))
                            {
                                list3.AddRange((DEMetaAttribute[]) resAttrs.ToArray(typeof(DEMetaAttribute)));
                            }
                        }
                        if ((list3 == null) || (list3.Count <= 0))
                        {
                            return (attribute2.Label + " -> ");
                        }
                        foreach (DEMetaAttribute attribute3 in list3)
                        {
                            if (attribute3.Oid == ca_in.ResAttrOid)
                            {
                                return (attribute2.Label + " -> " + attribute3.Label);
                            }
                        }
                    }
                }
            }
            return "";
        }

        private string GetClsAttr2ResAttrName(DECodeAttribute ca_in)
        {
            ArrayList list = new ArrayList();
            new ArrayList();
            new ArrayList();
            new ArrayList();
            if (this.theCls != null)
            {
                foreach (DEMetaAttribute attribute in PLDataModel.Agent2.GetClassAllAttributes(this.theCls.Name))
                {
                    if ((attribute.IsViewable && !attribute.IsGrid) && !attribute.IsVirtualClass)
                    {
                        list.Add(attribute);
                    }
                }
                if (list.Count == 0)
                {
                    return "";
                }
                foreach (DEMetaAttribute attribute2 in list)
                {
                    if (attribute2.Oid == ca_in.ClassAttrOid)
                    {
                        DEMetaClass metaClass = PLDataModel.Agent2.GetMetaClass(ca_in.ResClsOid);
                        if (metaClass == null)
                        {
                            return (attribute2.Name + ":");
                        }
                        List<DEMetaAttribute> list3 = new List<DEMetaAttribute>();
                        ArrayList resAttrs = new ResFunc().GetResAttrs(metaClass);
                        if ((resAttrs != null) && (resAttrs.Count > 0))
                        {
                            list3.AddRange((DEMetaAttribute[]) resAttrs.ToArray(typeof(DEMetaAttribute)));
                        }
                        if ((list3 != null) && (list3.Count > 0))
                        {
                            foreach (DEMetaAttribute attribute3 in list3)
                            {
                                if (attribute3.Oid == ca_in.ResAttrOid)
                                {
                                    return (attribute2.Name + ":" + attribute3.Name);
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }

        public string GetClsAttr2ResAttrRlt()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return "";
            }
            return this.GetClsAttr2ResAttrName(this.ca_input);
        }

        public DECodeAttribute GetCodeAttrValue()
        {
            if (this.Text.Trim().Length == 0)
            {
                return null;
            }
            return this.ca_input;
        }

        private void InitializeComponent()
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectCodeAttr") {
                RightAlignDropDown = DefaultableBoolean.False
            };
            base.ButtonsRight.Add(button);
            base.NullText = "";
            this.b_ReadOnly = true;
            base.Size = new Size(100, 0x15);
        }

        private void InitializeConfig()
        {
            this.ucUser = new UCCodeAttrPicker(this.ca_input);
            DropDownEditorButton button = base.ButtonsRight["SelectCodeAttr"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.dlhandler = new CodeAttrDropListHandler(this.ucUser_CodeAttrSelected);
            this.ucUser.CodeAttrSelected += this.dlhandler;
            base.BeforeEditorButtonDropDown += new BeforeEditorButtonDropDownEventHandler(this.CodeAttrCombo_BeforeDropDown);
            this.AllowDrop = true;
            base.TextChanged += new EventHandler(this.CodeAttrCombo_TextChanged);
            base.KeyUp += new KeyEventHandler(this.CodeAttrComboInput_KeyUp);
        }

        private void SetEditText(DECodeAttribute ca_in)
        {
            this.Text = "";
            if (this.b_IsAdd)
            {
                if (ca_in != null)
                {
                    this.Text = this.GetClsAttr2ResAttrLbl(ca_in);
                }
            }
            else if (ca_in != null)
            {
                this.Text = this.GetClsAttr2ResAttrLbl(ca_in);
            }
        }

        public void SetInput(Guid ClsOid, ArrayList al_unCodeMetaAttr, DECodeAttribute codeAttr, bool isAdd)
        {
            this.ClassOid = ClsOid;
            this.theCls = PLDataModel.Agent2.GetMetaClass(ClsOid);
            this.AL_unCodeMetaAttr = al_unCodeMetaAttr;
            this.b_IsAdd = isAdd;
            this.ca_input = codeAttr;
            this.SetEditText(codeAttr);
        }

        private void ucUser_CodeAttrSelected(DECodeAttribute ca_in)
        {
            bool flag = false;
            if ((base.Tag == null) && (ca_in != this.ca_input))
            {
                flag = true;
            }
            this.ca_input = ca_in;
            this.SetEditText(ca_in);
            if (flag && (this.DropListChanged != null))
            {
                this.DropListChanged(ca_in);
                base.CloseEditorButtonDropDowns();
            }
        }
    }
}

