    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC {
    public partial class UCResRelation : UserControlPLM
    {
        private UltraTextEditor active_utEditor;
        private ArrayList al_all;
        private ArrayList al_control;
        private ArrayList al_line;
        private ArrayList al_pos;
        private ArrayList al_retvalue;
        private string clsName;
        private SelectResHandler handler;
        private int i_all;
        private int i_index;
        private int i_lock;
        private UCResGrid ucUser;
        private UltraTextEditor[] utEditor;

        public event SelectResHandler SelectResChanged;

        public UCResRelation()
        {
            this.al_line = new ArrayList();
            this.al_all = new ArrayList();
            this.al_control = new ArrayList();
            this.al_pos = new ArrayList();
            this.al_retvalue = new ArrayList();
            this.InitializeComponent();
        }

        public UCResRelation(string str_clsname, ArrayList al_in) : this()
        {
            this.clsName = str_clsname;
            this.al_all = new CRefSet(str_clsname).SetFilterRef(al_in);
            this.InitUC(this.al_all);
        }

        private void ActiveDropDownUC(UltraTextEditor txtEditor, string str_clsname, bool b_isTree, int i_pos)
        {
            DEMetaAttribute metaAttr = new DEMetaAttribute();
            DEBoxObject obj2 = (DEBoxObject) this.al_all[i_pos];
            metaAttr = (DEMetaAttribute) obj2.MetaObject;
            if (b_isTree)
            {
                ArrayList list = new ArrayList();
                Guid empty = Guid.Empty;
                list.Add(empty);
                string refTableName = this.GetRefTableName(obj2, i_pos);
                list.Add(refTableName);
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(metaAttr.LinkedResClass);
                list.Add(class2.Name);
                UCResTree tree = new UCResTree(list, i_pos);
                this.al_control.Add(tree);
                DropDownEditorButton button = txtEditor.ButtonsRight["SelectRes"] as DropDownEditorButton;
                button.Control = tree;
                this.handler = new SelectResHandler(this.ucUser_ResSelected);
                tree.ResSelected += this.handler;
            }
            else
            {
                this.ucUser = new UCResGrid(str_clsname, metaAttr, i_pos);
                this.al_control.Add(this.ucUser);
                DropDownEditorButton button2 = txtEditor.ButtonsRight["SelectRes"] as DropDownEditorButton;
                button2.Control = this.ucUser;
                this.handler = new SelectResHandler(this.ucUser_ResSelected);
                this.ucUser.ResSelected += this.handler;
            }
        }
 
        private void DrawDropDownTBtn(UltraTextEditor txtEditor, string str_clsname, int i_dpindex)
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectRes") {
                Key = "SelectRes",
                RightAlignDropDown = DefaultableBoolean.False
            };
            txtEditor.ButtonsRight.Add(button);
            txtEditor.NullText = "(无)";
            txtEditor.Tag = this.al_all[i_dpindex];
            DEBoxObject obj2 = (DEBoxObject) this.al_all[i_dpindex];
            if (obj2.ShowType.Equals("UCTree"))
            {
                this.ActiveDropDownUC(txtEditor, str_clsname, true, i_dpindex);
            }
            else
            {
                this.ActiveDropDownUC(txtEditor, str_clsname, false, i_dpindex);
            }
        }

        private void DrawLine(int i_x, int i_y, int i_width, int i_height)
        {
            PanelPLM lplm = new PanelPLM {
                BackColor = SystemColors.ControlText,
                Location = new Point(i_x, i_y),
                Name = "panel1",
                Size = new Size(i_width, i_height),
                TabIndex = 0
            };
            base.Controls.Add(lplm);
        }

        private void FilterControl(Guid g_mid, DEBoxObject o_box, int i_pos)
        {
            string filterPos = "";
            if (o_box.FilterType == 1)
            {
                filterPos = o_box.FilterPos;
                if (filterPos.Trim().Length > 0)
                {
                    ArrayList posValue = new ArrayList();
                    posValue = this.GetPosValue(filterPos);
                    for (int i = 0; i < posValue.Count; i++)
                    {
                        int num2 = (int) posValue[i];
                        DEBoxObject obj2 = (DEBoxObject) this.al_all[num2];
                        if (obj2.ShowType == "UCGrid")
                        {
                            DEMetaAttribute metaObject = (DEMetaAttribute) o_box.MetaObject;
                            DEMetaClass class2 = ModelContext.MetaModel.GetClass(metaObject.LinkedResClass);
                            UCResGrid grid = (UCResGrid) this.al_control[num2];
                            string filterRefTableName = this.GetFilterRefTableName(o_box, num2);
                            grid.ReLoad(g_mid, class2.Name, filterRefTableName, false);
                        }
                    }
                }
            }
        }

        private string GetFilterRefTableName(DEBoxObject o_box, int i_pos)
        {
            i_pos.ToString();
            ArrayList splitString = new ArrayList();
            ArrayList list2 = new ArrayList();
            splitString = this.GetSplitString(o_box.FilterPos);
            list2 = this.GetSplitString(o_box.RelationTableName);
            for (int i = 0; i < splitString.Count; i++)
            {
                if (splitString[i].ToString().Equals(i_pos.ToString()))
                {
                    return list2[i].ToString();
                }
            }
            return "";
        }

        private ArrayList GetPosValue(string str_pos)
        {
            char[] separator = ",".ToCharArray();
            string[] strArray = null;
            ArrayList list = new ArrayList();
            if (str_pos != null)
            {
                strArray = str_pos.Split(separator);
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                list.Add(Convert.ToInt32(strArray[i].ToString()));
            }
            return list;
        }

        private string GetRefTableName(DEBoxObject o_box, int i_pos)
        {
            string str2 = i_pos.ToString();
            ArrayList splitString = new ArrayList();
            ArrayList list2 = new ArrayList();
            splitString = this.GetSplitString(o_box.FilterPos);
            list2 = this.GetSplitString(o_box.RelationTableName);
            for (int i = 0; i < splitString.Count; i++)
            {
                if (splitString[i].ToString().Equals(str2))
                {
                    return list2[i].ToString();
                }
            }
            return "";
        }

        private ArrayList GetSplitString(string str_pos)
        {
            char[] separator = ",".ToCharArray();
            string[] strArray = null;
            ArrayList list = new ArrayList();
            if (str_pos != null)
            {
                strArray = str_pos.Split(separator);
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                list.Add(strArray[i].ToString());
            }
            return list;
        }

        private void InitControl(DEBoxObject o_box, int i_len, int i_jg, bool b_isdrwaline)
        {
            this.utEditor[i_jg] = new UltraTextEditor();
            this.utEditor[i_jg].BorderStyle = UIElementBorderStyle.None;
            this.utEditor[i_jg].Location = new Point(i_len + (4 * i_jg), 0);
            this.utEditor[i_jg].Text = o_box.CONVALUE.ToString();
            this.utEditor[i_jg].Size = new Size(o_box.Width, 0x15);
            this.utEditor[i_jg].Appearance.FontData.SizeInPoints = o_box.Height - 4;
            this.utEditor[i_jg].SupportThemes = false;
            new ArrayList();
            if (o_box.ControlType.Equals("UltraTextBox"))
            {
                DEMetaAttribute metaObject = (DEMetaAttribute) o_box.MetaObject;
                DEMetaClass class2 = ModelContext.MetaModel.GetClass(metaObject.LinkedResClass);
                this.DrawDropDownTBtn(this.utEditor[i_jg], class2.Name, i_jg);
            }
            else
            {
                this.al_control.Add(this.utEditor[i_jg]);
            }
            base.Controls.Add(this.utEditor[i_jg]);
            if (b_isdrwaline)
            {
                this.DrawLine(((i_len + o_box.Width) + (4 * i_jg)) + 1, 0, 2, base.Height);
            }
        }

        private void InitUC(ArrayList al_in)
        {
            int num = 0;
            int num2 = 0;
            bool flag = true;
            this.i_all = al_in.Count;
            this.utEditor = new UltraTextEditor[this.i_all];
            this.active_utEditor = new UltraTextEditor();
            foreach (DEBoxObject obj2 in al_in)
            {
                if ((num + 1) == al_in.Count)
                {
                    flag = false;
                }
                this.InitControl(obj2, num2, num, flag);
                num2 += obj2.Width + 4;
                this.al_line.Add(num2 + 1);
                num++;
            }
        }

        private bool IsReflexFilter(string str_filterpos, string str_filterreflex, int i_pos)
        {
            ArrayList posValue = new ArrayList();
            ArrayList list2 = new ArrayList();
            if ((str_filterpos == null) || (str_filterpos.Length == 0))
            {
                return false;
            }
            posValue = this.GetPosValue(str_filterpos);
            list2 = this.GetPosValue(str_filterreflex);
            for (int i = 0; i < posValue.Count; i++)
            {
                if ((((int) posValue[i]) == i_pos) && (((int) list2[i]) == 1))
                {
                    return true;
                }
            }
            return false;
        }

        private void ReflexControl(Guid g_mid, DEBoxObject o_box, int i_pos)
        {
            string filterPos = "";
            string filterAttribue = "";
            for (int i = 0; i < this.al_all.Count; i++)
            {
                DEBoxObject obj2 = (DEBoxObject) this.al_all[i];
                filterPos = obj2.FilterPos;
                filterAttribue = obj2.FilterAttribue;
                if ((((filterAttribue != null) && (filterAttribue.Length > 0)) && ((filterAttribue.IndexOf("1") != -1) && (obj2.ShowType == "UCGrid"))) && this.IsReflexFilter(filterPos, filterAttribue, i_pos))
                {
                    DEMetaAttribute metaObject = (DEMetaAttribute) obj2.MetaObject;
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(metaObject.LinkedResClass);
                    UCResGrid grid = (UCResGrid) this.al_control[i];
                    string filterRefTableName = this.GetFilterRefTableName(obj2, i_pos);
                    grid.ReLoad(g_mid, class2.Name, filterRefTableName, true);
                }
            }
        }

        private void SetGridValue(string str_res, int i_pos)
        {
            Guid empty = Guid.Empty;
            ArrayList list = new ArrayList();
            string conAttrShowPos = "";
            char[] separator = "}".ToCharArray();
            string[] strArray = null;
            if ((base.Tag == null) && (str_res != null))
            {
                strArray = str_res.Split(separator);
            }
            DEBoxObject obj2 = (DEBoxObject) this.al_all[i_pos];
            conAttrShowPos = obj2.ConAttrShowPos;
            if (conAttrShowPos.Trim().Length > 0)
            {
                ArrayList posValue = new ArrayList();
                posValue = this.GetPosValue(conAttrShowPos);
                for (int i = 0; i < posValue.Count; i++)
                {
                    list.Add(strArray[i].ToString().Substring(1, strArray[i].ToString().Length - 1));
                }
                for (int j = 0; j < posValue.Count; j++)
                {
                    int index = (int) posValue[j];
                    this.utEditor[index].Text = list[j].ToString();
                }
                if (obj2.IsReflex || (obj2.FilterType != 0))
                {
                    string g = strArray[posValue.Count].ToString().Substring(1, strArray[posValue.Count].ToString().Length - 1);
                    Guid iterOid = new Guid(g);
                    DEMetaAttribute metaObject = (DEMetaAttribute) obj2.MetaObject;
                    DEMetaClass class2 = ModelContext.MetaModel.GetClass(metaObject.LinkedResClass);
                    Guid curView = ClientData.UserGlobalOption.CurView;
                    DEBusinessItem item = (DEBusinessItem) PLItem.Agent.GetBizItemByIteration(iterOid, class2.Name, curView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                    empty = item.MasterOid;
                    if (obj2.FilterType == 1)
                    {
                        this.FilterControl(empty, obj2, i_pos);
                    }
                    if (obj2.IsReflex)
                    {
                        this.ReflexControl(empty, obj2, i_pos);
                    }
                }
            }
        }

        private void SetTreeValue(string str_res, int i_pos)
        {
            string str = str_res;
            Guid empty = Guid.Empty;
            string str2 = "";
            char[] separator = "}".ToCharArray();
            string[] strArray = null;
            new ArrayList();
            DEBoxObject obj2 = (DEBoxObject) this.al_all[i_pos];
            if (str_res != null)
            {
                strArray = str.Split(separator);
            }
            if (obj2.IsReflex || (obj2.FilterType != 0))
            {
                string g = strArray[0].ToString().Substring(1, strArray[0].ToString().Length - 1);
                empty = new Guid(g);
                str2 = strArray[1].ToString().Substring(1, strArray[1].ToString().Length - 1);
                this.utEditor[i_pos].Text = str2;
                if (obj2.FilterType == 1)
                {
                    this.FilterControl(empty, obj2, i_pos);
                }
                if (obj2.IsReflex)
                {
                    this.ReflexControl(empty, obj2, i_pos);
                }
            }
        }

        private void ucUser_ResSelected(string str_res, int i_type, int i_pos)
        {
            bool flag = false;
            if ((base.Tag == null) && (str_res != null))
            {
                if (i_type == 1)
                {
                    this.SetGridValue(str_res, i_pos);
                }
                if (i_type == 2)
                {
                    this.SetTreeValue(str_res, i_pos);
                }
                flag = true;
            }
            if (flag && (this.SelectResChanged != null))
            {
                this.SelectResChanged(str_res, i_type, i_pos);
            }
        }

        public ArrayList ResValue
        {
            get
            {
                foreach (UltraTextEditor editor in this.al_control)
                {
                    this.al_retvalue.Add(editor.Text);
                }
                return this.al_retvalue;
            }
        }
    }
}

