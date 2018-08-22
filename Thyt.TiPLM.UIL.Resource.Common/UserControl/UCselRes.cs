    using Infragistics.Win;
    using Infragistics.Win.UltraWinEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Threading;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Resource.Common;
namespace Thyt.TiPLM.UIL.Resource.Common.UserControl
{
    public partial class UCselRes : UltraTextEditor
    {
        private ArrayList Attrs;
        private bool b_ReadOnly;
        private string className;
        private Guid classOid;
        private DEMetaAttribute deMetaAttri;
        private Thyt.TiPLM.UIL.Resource.Common.UserControl.SelectResHandler handler;
        private Guid resOid;
        private UCRes ucUser;

        public event Thyt.TiPLM.UIL.Resource.Common.UserControl.SelectResHandler SelectResChanged;

        public UCselRes()
        {
            this.Attrs = new ArrayList();
            this.resOid = Guid.Empty;
            this.InitializeComponent();
        }

        public UCselRes(IContainer container) : this()
        {
            container.Add(this);
        }

        public UCselRes(Guid classOid, DEMetaAttribute metaAttr)
        {
            this.Attrs = new ArrayList();
            this.resOid = Guid.Empty;
            this.classOid = classOid;
            this.deMetaAttri = metaAttr;
            this.InitializeComponent();
            this.ucUser = new UCRes(classOid, metaAttr);
            DropDownEditorButton button = base.ButtonsRight["SelectRes"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.handler = new Thyt.TiPLM.UIL.Resource.Common.UserControl.SelectResHandler(this.ucUser_ResSelected);
            this.ucUser.ResSelected += this.handler;
        }

        public UCselRes(string clsName, DEMetaAttribute metaAttr)
        {
            this.Attrs = new ArrayList();
            this.resOid = Guid.Empty;
            this.className = clsName;
            this.deMetaAttri = metaAttr;
            this.InitializeComponent();
            this.ucUser = new UCRes(clsName, metaAttr);
            DropDownEditorButton button = base.ButtonsRight["SelectRes"] as DropDownEditorButton;
            button.Control = this.ucUser;
            this.handler = new Thyt.TiPLM.UIL.Resource.Common.UserControl.SelectResHandler(this.ucUser_ResSelected);
            this.ucUser.ResSelected += this.handler;
            this.Attrs = this.GetAttributes(this.className);
        }


        private ArrayList GetAttributes(string classname){
           return ModelContext.MetaModel.GetAttributes(classname);
        }
        public string GetResourceID(Guid resOID, string clsName)
        {
            string str = "PLM_CUS_" + clsName;
            string str2 = "";
            ulong stamp = 0L;
            if (resOID != Guid.Empty)
            {
                DataSet set;
                this.Attrs = this.GetAttributes(clsName);
                UCCusResource.GetData(out stamp, out set, clsName, this.Attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return str2;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return str2;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        Guid guid = new Guid((byte[]) row["PLM_OID"]);
                        if (resOID == guid)
                        {
                            return row["PLM_ID"].ToString();
                        }
                    }
                    return str2;
                }
                catch (Exception exception)
                {
                    throw new Exception("error!" + exception.Message);
                }
            }
            return str2;
        }

        public Guid GetResourceOID(string id, string clsName)
        {
            string str = "PLM_CUS_" + clsName;
            Guid empty = Guid.Empty;
            ulong stamp = 0L;
            if (id != "")
            {
                DataSet set;
                this.Attrs = this.GetAttributes(clsName);
                UCCusResource.GetData(out stamp, out set, clsName, this.Attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return empty;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return empty;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        string str2 = row["PLM_ID"].ToString();
                        if (id == str2)
                        {
                            return new Guid((byte[]) row["PLM_OID"]);
                        }
                    }
                    return empty;
                }
                catch (Exception exception)
                {
                    throw new Exception("error!" + exception.Message);
                }
            }
            return empty;
        }

   

        public bool IsInDataSet(string str){
           return this.ucUser.IsInDataSet(str);
        }
        private void ucUser_ResSelected(string str_res)
        {
            bool flag = false;
            if ((base.Tag == null) && (str_res != null))
            {
                flag = true;
            }
            this.Text = str_res;
            if (flag && (this.SelectResChanged != null))
            {
                this.SelectResChanged(str_res);
                base.CloseEditorButtonDropDowns();
            }
        }

        public bool readOnly
        {
            get{
              return  this.b_ReadOnly;
            }set
            {
                this.b_ReadOnly = value;
            }
        }

        public Guid ResourceOid
        {
            get{ 
               return this.resOid;
            }set
            {
                this.resOid = value;
            }
        }

        public string ResValue
        {
            get {
                return this.Text.Trim();
            }
            set
            {
                this.Text = value;
            }
        }
    }
}

