    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class UCHasLinkedObject : UserControlPLM, IUCAgent
    {
        private bool b_isItemActive;
        
        private string str_RightClsName = "";

        public UCHasLinkedObject()
        {
            this.InitializeComponent();
        }

       

        public string GetValue()
        {
            string name = "";
            string str2 = "false";
            string str3 = "HasParentRelObject(";
            if (this.chkBox_HasRelObj.Checked)
            {
                str2 = "true";
            }
            if (this.lv_relation.CheckedItems.Count == 0)
            {
                name = " ";
            }
            else
            {
                DEMetaRelation tag = this.lv_relation.CheckedItems[0].Tag as DEMetaRelation;
                name = tag.Name;
            }
            if (name.Trim().Length == 0)
            {
                return "";
            }
            string str4 = str3;
            return (str4 + str2 + "," + name + ")");
        }


        private void InitRelationHeader()
        {
            this.lv_relation.Columns.Clear();
            ColumnHeader header = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            header.Text = "关系名称";
            header.Width = 140;
            header2.Text = "左类名称";
            header2.Width = 80;
            this.lv_relation.Columns.AddRange(new ColumnHeader[] { header, header2 });
        }

        private void LoadRelationData(string str_clsname)
        {
            ArrayList relations = ModelContext.MetaModel.GetRelations();
            DEMetaClass parent = ModelContext.MetaModel.GetParent(str_clsname);
            this.lv_relation.Items.Clear();
            foreach (DEMetaRelation relation in relations)
            {
                if (parent == null)
                {
                    if (relation.RightClassName == this.str_RightClsName)
                    {
                        ListViewItem item = new ListViewItem {
                            Text = relation.Label,
                            SubItems = { relation.LeftClassLabel },
                            Tag = relation
                        };
                        this.lv_relation.Items.Add(item);
                    }
                }
                else if ((relation.RightClassName == this.str_RightClsName) || (relation.RightClassName == parent.Name))
                {
                    ListViewItem item2 = new ListViewItem {
                        Text = relation.Label,
                        SubItems = { relation.LeftClassLabel },
                        Tag = relation
                    };
                    this.lv_relation.Items.Add(item2);
                }
            }
        }

        private void LocationRelData(string str_relName)
        {
            if (this.lv_relation.Items.Count != 0)
            {
                foreach (ListViewItem item in this.lv_relation.Items)
                {
                    DEMetaRelation tag = item.Tag as DEMetaRelation;
                    if (tag.Name == str_relName)
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void lv_relation_ItemActivate(object sender, EventArgs e)
        {
            this.b_isItemActive = true;
            if (this.lv_relation.SelectedItems.Count != 0)
            {
                int index = this.lv_relation.SelectedItems[0].Index;
                foreach (ListViewItem item in this.lv_relation.CheckedItems)
                {
                    item.Checked = false;
                }
                this.lv_relation.Items[index].Checked = true;
                this.b_isItemActive = false;
            }
        }

        private void lv_relation_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!this.b_isItemActive && (this.lv_relation.CheckedItems.Count == 1))
            {
                e.Item.Checked = false;
            }
        }

        public string ParseValue(string str_funcvalue) {
            return "wyl";
        }
        public void SetInput(object o_in)
        {
            if (((o_in != null) && (o_in is ArrayList)) && ((o_in as ArrayList).Count >= 2))
            {
                string str = (o_in as ArrayList)[0].ToString();
                if (str.Trim().Length > 0)
                {
                    this.str_RightClsName = str;
                    this.InitRelationHeader();
                    this.LoadRelationData(str);
                }
                string str2 = (o_in as ArrayList)[1].ToString();
                if (str2.StartsWith("HasParentRelObject(") && str2.EndsWith(")"))
                {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in)
        {
            string str = str_in.Substring("HasParentRelObject".Length + 1, (str_in.Length - "HasParentRelObject".Length) - 2);
            if ((str != null) && (str.Trim().Length > 0))
            {
                char[] separator = ",".ToCharArray();
                string[] strArray = str.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    string str3 = strArray[i].ToString();
                    if ((i == 0) && (str3 == "true"))
                    {
                        this.chkBox_HasRelObj.Checked = true;
                    }
                    if ((i == 1) && (str3.Trim().Length > 0))
                    {
                        this.LocationRelData(str3);
                    }
                }
            }
        }
    }
}

