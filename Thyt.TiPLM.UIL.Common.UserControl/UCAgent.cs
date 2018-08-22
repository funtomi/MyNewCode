namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;

    public class UCAgent
    {
        private ArrayList al_attrnamelst = new ArrayList();
        private ArrayList al_clsnamelst = new ArrayList();
        private ArrayList al_relnamelst = new ArrayList();
        private Hashtable ht_ucinfolst = new Hashtable();
        private static UCAgent instance;

        public UCAgent()
        {
            this.InitParam();
        }

        public string GetFunctionLabel(string str_name)
        {
            string str = "";
            if (!this.ht_ucinfolst.Contains(str_name))
            {
                return str;
            }
            DEUCInfo info = (DEUCInfo) this.ht_ucinfolst[str_name];
            return info.PLM_LABEL;
        }

        public ArrayList GetFunctionNameSet(int int_type)
        {
            ArrayList list = new ArrayList();
            switch (int_type)
            {
                case 1:
                    if (this.al_clsnamelst.Count > 0)
                    {
                        foreach (string str in this.al_clsnamelst)
                        {
                            list.Add(str);
                        }
                    }
                    return list;

                case 2:
                    if (this.al_relnamelst.Count > 0)
                    {
                        foreach (string str2 in this.al_relnamelst)
                        {
                            list.Add(str2);
                        }
                    }
                    return list;

                case 3:
                    if (this.al_attrnamelst.Count > 0)
                    {
                        foreach (string str3 in this.al_attrnamelst)
                        {
                            list.Add(str3);
                        }
                    }
                    return list;
            }
            return list;
        }

        public UserControlPLM GetFuncUC(string str_name)
        {
            if (!this.ht_ucinfolst.Contains(str_name))
            {
                return null;
            }
            DEUCInfo info = (DEUCInfo) this.ht_ucinfolst[str_name];
            return (UserControlPLM) Activator.CreateInstance(Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + info.PLM_FrmDLLName).GetType(info.PLM_FrmClsName));
        }

        private void InitParam()
        {
            this.al_clsnamelst.Clear();
            this.al_relnamelst.Clear();
            this.al_attrnamelst.Clear();
            this.al_clsnamelst.Add("HasFile");
            this.al_clsnamelst.Add("HasRelObject");
            this.al_clsnamelst.Add("HasParentRelObject");
            this.al_attrnamelst.Add("AttrUser2Org");
            DEUCInfo info = new DEUCInfo {
                PLM_ID = 1,
                PLM_NAME = "UCHasFile",
                PLM_LABEL = "对象是否存在文件",
                PLM_FuncName = "HasFile",
                PLM_FrmClsName = "Thyt.TiPLM.UIL.Common.UserControl.UCHasFile",
                PLM_FrmDLLName = "Thyt.TiPLM.UIL.Common.UserControl.dll",
                PLM_TYPE = 1
            };
            if (!this.ht_ucinfolst.ContainsKey("HasFile"))
            {
                this.ht_ucinfolst.Add("HasFile", info);
            }
            DEUCInfo info2 = new DEUCInfo {
                PLM_ID = 2,
                PLM_NAME = "UCHasRelObject",
                PLM_LABEL = "是否存在关联对象",
                PLM_FuncName = "HasRelObject",
                PLM_FrmClsName = "Thyt.TiPLM.UIL.Common.UserControl.UCHasRelObject",
                PLM_FrmDLLName = "Thyt.TiPLM.UIL.Common.UserControl.dll",
                PLM_TYPE = 1
            };
            if (!this.ht_ucinfolst.ContainsKey("HasRelObject"))
            {
                this.ht_ucinfolst.Add("HasRelObject", info2);
            }
            DEUCInfo info3 = new DEUCInfo {
                PLM_ID = 3,
                PLM_NAME = "UCHasLinkedObject",
                PLM_LABEL = "是否存在被关联对象",
                PLM_FuncName = "SearchInProject",
                PLM_FrmClsName = "Thyt.TiPLM.UIL.Common.UserControl.UCHasLinkedObject",
                PLM_FrmDLLName = "Thyt.TiPLM.UIL.Common.UserControl.dll",
                PLM_TYPE = 1
            };
            if (!this.ht_ucinfolst.ContainsKey("HasParentRelObject"))
            {
                this.ht_ucinfolst.Add("HasParentRelObject", info3);
            }
            DEUCInfo info4 = new DEUCInfo {
                PLM_ID = 4,
                PLM_NAME = "UCAttrUser2Org",
                PLM_LABEL = "和登录用户组织的隶属关系",
                PLM_FuncName = "AttrUser2Org",
                PLM_FrmClsName = "Thyt.TiPLM.UIL.Common.UserControl.UCAttrUser2Org",
                PLM_FrmDLLName = "Thyt.TiPLM.UIL.Common.UserControl.dll",
                PLM_TYPE = 3
            };
            if (!this.ht_ucinfolst.ContainsKey("AttrUser2Org"))
            {
                this.ht_ucinfolst.Add("AttrUser2Org", info4);
            }
        }

        public static UCAgent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UCAgent();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
}

