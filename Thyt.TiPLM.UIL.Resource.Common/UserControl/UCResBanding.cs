    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.UserControl {
    public partial class UCResBanding : UserControlPLM
    {
        private UltraTextEditor active_utEditor;
        private ArrayList al_all;
        private ArrayList al_line;
        private ArrayList al_pos;
        private string clsName;
        private SelectResHandler handler;
        private int i_all;
        private int i_index;
        private string str_field;
        private UCRes ucUser;
        private UltraTextEditor[] utEditor;

        public event SelectResHandler SelectResChanged;

        public UCResBanding()
        {
            this.al_line = new ArrayList();
            this.al_all = new ArrayList();
            this.al_pos = new ArrayList();
            this.InitializeComponent();
        }

        public UCResBanding(string str_clsname, ArrayList al_in) : this()
        {
            this.clsName = str_clsname;
            this.al_all = al_in;
            this.InitUC(al_in);
        }

        private void ActiveDropDownUC(UltraTextEditor txtEditor)
        {
            DEMetaAttribute metaAttr = new DEMetaAttribute {
                LinkType = 1,
                Combination = this.str_field
            };
            this.ucUser = new UCRes(this.clsName, metaAttr);
            DropDownEditorButton button = txtEditor.ButtonsRight["SelectRes"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.handler = new SelectResHandler(this.ucUser_ResSelected);
            this.ucUser.ResSelected += this.handler;
        }

 
        private void DrawDropDownTBtn(UltraTextEditor txtEditor, int i_dpindex)
        {
            DropDownEditorButton button = new DropDownEditorButton("SelectRes") {
                Key = "SelectRes",
                RightAlignDropDown = DefaultableBoolean.False
            };
            txtEditor.ButtonsRight.Add(button);
            txtEditor.NullText = "(无)";
            txtEditor.Tag = this.al_all[i_dpindex];
            this.ActiveDropDownUC(txtEditor);
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

        private void InitControl(ArrayList al_in, int i_len, int i_jg, bool b_isdrwaline)
        {
            this.utEditor[i_jg] = new UltraTextEditor();
            this.utEditor[i_jg].BorderStyle = UIElementBorderStyle.None;
            this.utEditor[i_jg].Location = new Point(i_len + (4 * i_jg), 2);
            this.utEditor[i_jg].Text = al_in[0].ToString();
            this.utEditor[i_jg].Size = new Size((int) al_in[2], 0x15);
            this.utEditor[i_jg].Appearance.FontData.SizeInPoints = (int) al_in[3];
            this.utEditor[i_jg].SupportThemes = false;
            if (al_in[1].Equals("UltraTextBox"))
            {
                this.DrawDropDownTBtn(this.utEditor[i_jg], i_jg);
            }
            base.Controls.Add(this.utEditor[i_jg]);
            if (b_isdrwaline)
            {
                this.DrawLine(((i_len + ((int) al_in[2])) + (4 * i_jg)) + 1, 0, 2, base.Height);
            }
        }


        private void InitUC(ArrayList al_in)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            bool flag = true;
            this.i_all = al_in.Count;
            this.utEditor = new UltraTextEditor[this.i_all];
            this.active_utEditor = new UltraTextEditor();
            foreach (ArrayList list in al_in)
            {
                if (list[4] != null)
                {
                    this.str_field = this.str_field + "{" + ((DEMetaAttribute) list[4]).Combination + "}";
                    this.al_pos.Add(num2);
                }
                num2++;
            }
            foreach (ArrayList list2 in al_in)
            {
                if ((num + 1) == al_in.Count)
                {
                    flag = false;
                }
                this.InitControl(list2, num3, num, flag);
                num3 += ((int) list2[2]) + 4;
                list2.Add(num3 + 1);
                num++;
            }
        }

        private void ucUser_ResSelected(string str_res)
        {
            char[] separator = "}".ToCharArray();
            string[] strArray = null;
            bool flag = false;
            if ((base.Tag == null) && (str_res != null))
            {
                strArray = str_res.Split(separator);
                flag = true;
            }
            for (int i = 0; i < this.al_pos.Count; i++)
            {
                this.utEditor[(int) this.al_pos[i]].Text = strArray[i].ToString().Substring(1, strArray[i].ToString().Length - 1);
            }
            if (flag && (this.SelectResChanged != null))
            {
                this.SelectResChanged(str_res);
            }
        }
    }
}

