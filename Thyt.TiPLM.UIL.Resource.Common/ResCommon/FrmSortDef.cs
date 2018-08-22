    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.ResCommon
{
    public partial class FrmSortDef : FormPLM
    {
        private ArrayList al_NewAttr;
        private ArrayList al_OldAttr;
        private ArrayList al_SaveAttr;
        private ArrayList al_UnDefAttr;
        private bool b_isLoading;
        private bool b_isLocalhost;
        private bool b_isRefCls;
       
        private Guid g_ClsOid;
        private Guid g_SaveOid;
        
        private DEMetaClass myCls;

        public FrmSortDef(Guid g_clsoid, bool b_isLocalhost, Guid g_saveoid)
        {
            this.InitializeComponent();
            this.g_ClsOid = g_clsoid;
            this.g_SaveOid = g_saveoid;
            this.b_isLocalhost = b_isLocalhost;
            this.myCls = ModelContext.MetaModel.GetClass(g_clsoid);
        }

        public FrmSortDef(Guid g_clsoid, bool b_isLocalhost, Guid g_saveoid, bool b_isrefcls)
        {
            this.InitializeComponent();
            this.g_ClsOid = g_clsoid;
            this.g_SaveOid = g_saveoid;
            this.b_isLocalhost = b_isLocalhost;
            this.b_isRefCls = b_isrefcls;
            this.myCls = ModelContext.MetaModel.GetClass(g_clsoid);
        }

        public FrmSortDef(Guid g_clsoid, bool b_isLocalhost, Guid g_saveoid, bool b_isrefcls, ArrayList al_sortAttr)
        {
            this.InitializeComponent();
            this.g_ClsOid = g_clsoid;
            this.g_SaveOid = g_saveoid;
            this.b_isLocalhost = b_isLocalhost;
            this.b_isRefCls = b_isrefcls;
            this.al_NewAttr = al_sortAttr;
            this.myCls = ModelContext.MetaModel.GetClass(g_clsoid);
        }

        private void AddLVObj(string str_label, bool b_isAsc, DEDefSort deSort)
        {
            ListViewItem item = new ListViewItem(str_label) {
                SubItems = { b_isAsc ? "升" : "降" },
                Tag = deSort
            };
            this.LV_Sort.Items.Add(item);
        }

        private void btnAddAsc_Click(object sender, EventArgs e)
        {
            if ((this.LB_Old.SelectedItems.Count != 0) && (this.LV_Sort.Items.Count <= 5))
            {
                string str = this.LB_Old.SelectedItem.ToString();
                this.SetSelAttr(str, true);
                this.InitUnDefineAttrLst();
                this.LoadAttrLstData();
                this.SortNewLst();
            }
        }

        private void btnAddDesc_Click(object sender, EventArgs e)
        {
            if ((this.LB_Old.SelectedItems.Count != 0) && (this.LV_Sort.Items.Count <= 5))
            {
                string str = this.LB_Old.SelectedItem.ToString();
                this.SetSelAttr(str, false);
                this.InitUnDefineAttrLst();
                this.LoadAttrLstData();
                this.SortNewLst();
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if ((this.LV_Sort.SelectedItems.Count != 0) && (this.LV_Sort.SelectedItems[0].Index < (this.LV_Sort.Items.Count - 1)))
            {
                string text = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                int index = 0;
                index = this.LV_Sort.SelectedItems[0].Index;
                DEDefSort tag = new DEDefSort();
                DEDefSort sort2 = new DEDefSort();
                text = this.LV_Sort.Items[index].Text;
                str3 = this.LV_Sort.Items[index].SubItems[1].Text;
                tag = (DEDefSort) this.LV_Sort.Items[index].Tag;
                str2 = this.LV_Sort.Items[index + 1].Text;
                str4 = this.LV_Sort.Items[index + 1].SubItems[1].Text;
                sort2 = (DEDefSort) this.LV_Sort.Items[index + 1].Tag;
                this.LV_Sort.Items[index].Text = str2;
                this.LV_Sort.Items[index].SubItems[1].Text = str4;
                this.LV_Sort.Items[index].Tag = sort2;
                this.LV_Sort.Items[index + 1].Text = text;
                this.LV_Sort.Items[index + 1].SubItems[1].Text = str3;
                this.LV_Sort.Items[index + 1].Tag = tag;
                this.ReFreshLVState();
                this.LV_Sort.Items[index + 1].Selected = true;
                this.LV_Sort.Items[index + 1].ForeColor = Color.White;
                this.LV_Sort.Items[index + 1].BackColor = Color.Blue;
                this.SortNewLst();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.al_SaveAttr = this.al_NewAttr;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.LV_Sort.SelectedItems.Count != 0)
            {
                DEDefSort tag = (DEDefSort) this.LV_Sort.SelectedItems[0].Tag;
                this.DelSelAttr(tag);
                this.InitUnDefineAttrLst();
                this.LoadAttrLstData();
                if (this.LV_Sort.Items.Count > 0)
                {
                    this.LV_Sort.Items[0].Selected = true;
                    this.LV_Sort.SelectedItems[0].ForeColor = Color.White;
                    this.LV_Sort.SelectedItems[0].BackColor = Color.Blue;
                }
                this.SortNewLst();
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if ((this.LV_Sort.SelectedItems.Count != 0) && (this.LV_Sort.SelectedItems[0].Index > 0))
            {
                string text = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                int index = 0;
                index = this.LV_Sort.SelectedItems[0].Index;
                DEDefSort tag = new DEDefSort();
                DEDefSort sort2 = new DEDefSort();
                text = this.LV_Sort.Items[index - 1].Text;
                str3 = this.LV_Sort.Items[index - 1].SubItems[1].Text;
                tag = (DEDefSort) this.LV_Sort.Items[index - 1].Tag;
                str2 = this.LV_Sort.Items[index].Text;
                str4 = this.LV_Sort.Items[index].SubItems[1].Text;
                sort2 = (DEDefSort) this.LV_Sort.Items[index].Tag;
                this.LV_Sort.Items[index - 1].Text = str2;
                this.LV_Sort.Items[index - 1].SubItems[1].Text = str4;
                this.LV_Sort.Items[index - 1].Tag = sort2;
                this.LV_Sort.Items[index].Text = text;
                this.LV_Sort.Items[index].SubItems[1].Text = str3;
                this.LV_Sort.Items[index].Tag = tag;
                this.ReFreshLVState();
                this.LV_Sort.Items[index - 1].Selected = true;
                this.LV_Sort.Items[index - 1].ForeColor = Color.White;
                this.LV_Sort.Items[index - 1].BackColor = Color.Blue;
                this.SortNewLst();
            }
        }

        private void delLVObj()
        {
            this.LV_Sort.SelectedItems[0].Remove();
        }

        private void DelSelAttr(DEDefSort deSort)
        {
            foreach (DEDefSort sort in this.al_NewAttr)
            {
                if (sort.OID == deSort.OID)
                {
                    this.al_NewAttr.Remove(sort);
                    this.delLVObj();
                    break;
                }
            }
        }
         

        private void FrmSortDef_Load(object sender, EventArgs e)
        {
            this.al_OldAttr = new ArrayList();
            this.al_UnDefAttr = new ArrayList();
            if (!this.b_isLoading)
            {
                this.al_NewAttr = new ArrayList();
            }
            this.al_SaveAttr = new ArrayList();
            this.InitOldAttrLstData();
            this.InitNewAttrLstData();
            this.InitUnDefineAttrLst();
            this.LoadAttrLstData();
            this.LoadDefLstData();
        }

        private ArrayList GetAttributes(string classname)
        {
            ArrayList list = new ArrayList();
            foreach (DEMetaAttribute attribute in ModelContext.MetaModel.GetAttributes(classname))
            {
                if ((((attribute.DataType2 != PLMDataType.Blob) && (attribute.DataType2 != PLMDataType.Card)) && ((attribute.DataType2 != PLMDataType.Clob) && (attribute.DataType2 != PLMDataType.Grid))) && (attribute.DataType2 != PLMDataType.Guid))
                {
                    list.Add(attribute);
                }
            }
            return list;
        }

        private DEMetaAttribute GetMetaAttr(Guid g_oid)
        {
            foreach (DEMetaAttribute attribute in this.al_OldAttr)
            {
                if (attribute.Oid == g_oid)
                {
                    return attribute;
                }
            }
            return new DEMetaAttribute();
        }

        private DEMetaAttribute GetMetaAttr(string str_label)
        {
            foreach (DEMetaAttribute attribute in this.al_OldAttr)
            {
                if (attribute.Label == str_label)
                {
                    return attribute;
                }
            }
            return new DEMetaAttribute();
        }


        private void InitLocalDefAttr(string str_sort)
        {
            if (((str_sort != null) && (str_sort.Trim().Length > 0)) && (str_sort != "nothing"))
            {
                str_sort = str_sort.Trim();
                char[] separator = ",".ToCharArray();
                string[] strArray = str_sort.Split(separator);
                for (int i = 0; i < strArray.Length; i++)
                {
                    char[] chArray2 = "|".ToCharArray();
                    string[] strArray2 = strArray[i].Split(chArray2);
                    string str3 = strArray2[1].ToString();
                    DEDefSort sort = new DEDefSort {
                        OID = Guid.NewGuid(),
                        CLSOID = this.g_ClsOid,
                        ATTROID = new Guid(strArray2[0].ToString()),
                        ISASC = (str3 == "asc") ? true : false,
                        POSITION = i
                    };
                    this.al_NewAttr.Add(sort);
                }
            }
        }

        private void InitNewAttrLstData()
        {
            if (!this.b_isLoading)
            {
                PLReference reference = new PLReference();
                if (this.b_isLocalhost)
                {
                    string userOption = "";
                    userOption = (string) ClientData.GetUserOption(ClientData.LogonUser.Oid + this.g_SaveOid.ToString() + "_def");
                    this.InitLocalDefAttr(userOption);
                }
                else
                {
                    this.al_NewAttr = reference.GetSortAttrLst(this.g_SaveOid);
                }
            }
        }

        private void InitOldAttrLstData()
        {
            this.al_OldAttr = this.GetAttributes(this.myCls.Name);
            if (this.b_isRefCls)
            {
                ArrayList c = new ArrayList();
                c = ResFunc.GetFixedAttrList();
                this.al_OldAttr.AddRange(c);
            }
        }

        private void InitUnDefineAttrLst()
        {
            this.al_UnDefAttr.Clear();
            bool flag = false;
            foreach (DEMetaAttribute attribute in this.al_OldAttr)
            {
                foreach (DEDefSort sort in this.al_NewAttr)
                {
                    if (attribute.Oid == sort.ATTROID)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.al_UnDefAttr.Add(attribute);
                }
                flag = false;
            }
        }

        private void LB_Old_DoubleClick(object sender, EventArgs e)
        {
            if ((this.LB_Old.SelectedItems.Count != 0) && (this.LV_Sort.Items.Count <= 5))
            {
                string str = this.LB_Old.SelectedItem.ToString();
                this.SetSelAttr(str, true);
                this.InitUnDefineAttrLst();
                this.LoadAttrLstData();
                this.SortNewLst();
            }
        }

        private void LoadAttrLstData()
        {
            this.LB_Old.Items.Clear();
            if (this.al_OldAttr.Count != 0)
            {
                foreach (DEMetaAttribute attribute in this.al_UnDefAttr)
                {
                    this.LB_Old.Items.Add(attribute.Label);
                }
            }
        }

        private void LoadDefLstData()
        {
            this.LV_Sort.Items.Clear();
            if (this.al_NewAttr.Count > 0)
            {
                foreach (DEDefSort sort in this.al_NewAttr)
                {
                    string text = "";
                    text = this.GetMetaAttr(sort.ATTROID).Label.ToString();
                    if (text.Length > 0)
                    {
                        ListViewItem item = new ListViewItem(text) {
                            SubItems = { sort.ISASC ? "升" : "降" },
                            Tag = sort
                        };
                        this.LV_Sort.Items.Add(item);
                    }
                }
            }
        }

        private void LV_Sort_ItemActivate(object sender, EventArgs e)
        {
            if (this.LV_Sort.Items.Count != 0)
            {
                this.ReFreshLVState();
                if (this.LV_Sort.SelectedItems.Count != 0)
                {
                    this.LV_Sort.SelectedItems[0].ForeColor = Color.White;
                    this.LV_Sort.SelectedItems[0].BackColor = Color.Blue;
                }
            }
        }

        private void ReFreshLVState()
        {
            foreach (ListViewItem item in this.LV_Sort.Items)
            {
                item.ForeColor = Color.Black;
                item.BackColor = Color.White;
            }
        }

        private void SetSelAttr(string str_label, bool b_isAsc)
        {
            DEMetaAttribute metaAttr = this.GetMetaAttr(str_label);
            DEDefSort deSort = new DEDefSort {
                OID = Guid.NewGuid(),
                ATTROID = metaAttr.Oid,
                CLSOID = this.g_SaveOid,
                ISASC = b_isAsc ? true : false,
                POSITION = 0x7fffffff
            };
            this.AddLVObj(str_label, b_isAsc, deSort);
            this.al_NewAttr.Add(deSort);
        }

        private void SortNewLst()
        {
            if (this.al_NewAttr.Count != 0)
            {
                this.al_NewAttr.Clear();
                for (int i = 0; i < this.LV_Sort.Items.Count; i++)
                {
                    DEDefSort tag = (DEDefSort) this.LV_Sort.Items[i].Tag;
                    tag.POSITION = i;
                    this.al_NewAttr.Add(tag);
                }
            }
        }

        public bool IsCanLoad
        {
            set
            {
                this.b_isLoading = value;
            }
        }

        public ArrayList SaveSortAttr
        {
            get {
                return this.al_SaveAttr;
            }
            set
            {
                this.al_SaveAttr = value;
            }
        }
    }
}

