namespace Thyt.TiPLM.UIL.Resource.Common.NEWUC
{
    using System;
    using System.Collections;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.DataModel;

    public class CRefSet
    {
        private ArrayList al_all = new ArrayList();
        private ArrayList al_reference = new ArrayList();
        private string ClsName = "";

        public CRefSet(string str_clsname)
        {
            this.ClsName = str_clsname;
        }

        private ArrayList GetAllReferenceLst()
        {
            ArrayList list = new ArrayList();
            PLDataModel model = new PLDataModel();
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(this.ClsName);
            return model.GetReferences(class2.Oid);
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

        private ArrayList LoadConAttrLst()
        {
            ArrayList list = new ArrayList();
            foreach (DEReference reference in this.al_reference)
            {
                if (!reference.IsIdentity)
                {
                    continue;
                }
                bool flag = false;
                bool flag2 = false;
                int num = 0;
                Guid referencingAttr = reference.ReferencingAttr;
                Guid targetAttr = reference.TargetAttr;
                ArrayList list2 = new ArrayList();
                if (list2.Count == 0)
                {
                    list2.Add(referencingAttr);
                    list2.Add(targetAttr);
                    list.Add(list2);
                    continue;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList list3 = (ArrayList) list[i];
                    num = i;
                    foreach (Guid guid3 in list3)
                    {
                        if (guid3 == referencingAttr)
                        {
                            flag = true;
                        }
                        if (guid3 == targetAttr)
                        {
                            flag2 = true;
                        }
                    }
                    if (flag || flag2)
                    {
                        break;
                    }
                }
                if (!flag && flag2)
                {
                    ((ArrayList) list[num]).Add(referencingAttr);
                }
                if (!flag2 && flag)
                {
                    ((ArrayList) list[num]).Add(targetAttr);
                }
                if (!flag && !flag2)
                {
                    list2 = new ArrayList {
                        referencingAttr,
                        targetAttr
                    };
                    list.Add(list2);
                }
            }
            return list;
        }

        private void SetAllCombinationFG()
        {
            for (int i = 0; i < this.al_all.Count; i++)
            {
                DEBoxObject obj2 = (DEBoxObject) this.al_all[i];
                if (obj2.ControlType != "TextEditPLM")
                {
                    DEMetaAttribute metaObject = (DEMetaAttribute) obj2.MetaObject;
                    metaObject.Combination = "{" + metaObject.Combination + "}";
                    obj2.ConAttrShowPos = i.ToString();
                    obj2.FilterType = 0;
                    obj2.FilterPos = "";
                    obj2.FilterAttribue = "";
                    obj2.IsConAttrType = false;
                    obj2.IsReflex = false;
                }
            }
        }

        private void SetConAttrObject(ArrayList al_capos, string str_filterpos, string str_filterval, string str_filterrefex)
        {
            foreach (string str in al_capos)
            {
                if (Convert.ToInt32(str) < this.al_all.Count)
                {
                    DEBoxObject obj2 = (DEBoxObject) this.al_all[Convert.ToInt32(str)];
                    obj2.FilterType = 1;
                    obj2.FilterAttribue = str_filterrefex;
                    obj2.FilterPos = str_filterpos;
                    obj2.RelationTableName = str_filterval;
                    this.SetReflexAttr(str_filterrefex, str_filterpos);
                }
            }
        }

        private void SetConAttrShowPos()
        {
            ArrayList list = new ArrayList();
            list = this.LoadConAttrLst();
            if (list.Count != 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList list2 = (ArrayList) list[i];
                    string combination = "";
                    string str2 = "";
                    foreach (Guid guid in list2)
                    {
                        for (int j = 0; j < this.al_all.Count; j++)
                        {
                            DEBoxObject obj2 = (DEBoxObject) this.al_all[j];
                            if (obj2.ControlType != "TextEditPLM")
                            {
                                DEMetaAttribute metaObject = (DEMetaAttribute) obj2.MetaObject;
                                if (metaObject.Oid == guid)
                                {
                                    if (str2.Length == 0)
                                    {
                                        str2 = j.ToString();
                                        combination = metaObject.Combination;
                                    }
                                    else
                                    {
                                        str2 = str2 + "," + j.ToString();
                                        combination = combination + metaObject.Combination;
                                    }
                                }
                            }
                        }
                    }
                    foreach (Guid guid2 in list2)
                    {
                        foreach (DEBoxObject obj3 in this.al_all)
                        {
                            if (obj3.ControlType != "TextEditPLM")
                            {
                                DEMetaAttribute attribute2 = (DEMetaAttribute) obj3.MetaObject;
                                if (attribute2.Oid == guid2)
                                {
                                    attribute2.Combination = combination;
                                    obj3.MetaObject = attribute2;
                                    obj3.ConAttrShowPos = str2;
                                    obj3.IsConAttrType = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetFilter()
        {
            foreach (DEReference reference in this.al_reference)
            {
                if (!reference.IsIdentity)
                {
                    Guid referencingAttr = reference.ReferencingAttr;
                    Guid targetAttr = reference.TargetAttr;
                    ArrayList list = new ArrayList();
                    ArrayList list2 = new ArrayList();
                    ArrayList list3 = new ArrayList();
                    list.Add(targetAttr);
                    DEMetaRelation relation = ModelContext.MetaModel.GetRelation(reference.ByRelaton);
                    list2.Add(relation.Name);
                    if (reference.IsReflex)
                    {
                        list3.Add(1);
                    }
                    else
                    {
                        list3.Add(0);
                    }
                    foreach (DEReference reference2 in this.al_reference)
                    {
                        if (reference2.IsIdentity)
                        {
                            continue;
                        }
                        Guid guid3 = reference2.ReferencingAttr;
                        Guid guid4 = reference2.TargetAttr;
                        bool flag = false;
                        bool flag2 = false;
                        if (referencingAttr == guid3)
                        {
                            foreach (Guid guid5 in list)
                            {
                                if (guid5 == guid4)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            flag2 = true;
                        }
                        if (!flag && flag2)
                        {
                            list.Add(guid4);
                            DEMetaRelation relation2 = ModelContext.MetaModel.GetRelation(reference2.ByRelaton);
                            list2.Add(relation2.Name);
                            if (reference2.IsReflex)
                            {
                                list3.Add(1);
                            }
                            else
                            {
                                list3.Add(0);
                            }
                        }
                    }
                    this.SetOBoxObject(referencingAttr, list, list2, list3);
                }
            }
        }

        public ArrayList SetFilterRef(ArrayList al_in)
        {
            this.al_all = al_in;
            this.SetAllCombinationFG();
            this.al_reference = this.GetAllReferenceLst();
            if (this.al_reference.Count != 0)
            {
                this.SetConAttrShowPos();
                this.SetFilter();
            }
            return this.al_all;
        }

        private void SetOBoxObject(Guid g_left, ArrayList al_right, ArrayList al_filtervalue, ArrayList al_filterreflex)
        {
            string conAttrShowPos = "";
            string str2 = "";
            string str3 = "";
            for (int i = 0; i < this.al_all.Count; i++)
            {
                DEBoxObject obj2 = (DEBoxObject) this.al_all[i];
                if (obj2.ControlType != "TextEditPLM")
                {
                    DEMetaAttribute metaObject = (DEMetaAttribute) obj2.MetaObject;
                    if (metaObject.Oid == g_left)
                    {
                        for (int j = 0; j < this.al_all.Count; j++)
                        {
                            DEBoxObject obj3 = (DEBoxObject) this.al_all[j];
                            if (obj3.ControlType != "TextBox")
                            {
                                DEMetaAttribute attribute2 = (DEMetaAttribute) obj3.MetaObject;
                                for (int k = 0; k < al_right.Count; k++)
                                {
                                    Guid guid = (Guid) al_right[k];
                                    if (attribute2.Oid == guid)
                                    {
                                        if (conAttrShowPos.Length == 0)
                                        {
                                            ArrayList list = new ArrayList();
                                            list = this.GetSplitString(obj3.ConAttrShowPos);
                                            if (list.Count > 1)
                                            {
                                                conAttrShowPos = obj3.ConAttrShowPos;
                                                string str4 = "";
                                                string str5 = "";
                                                for (int m = 0; m < list.Count; m++)
                                                {
                                                    if (m == 0)
                                                    {
                                                        str4 = al_filtervalue[k].ToString();
                                                        str5 = al_filterreflex[k].ToString();
                                                    }
                                                    else
                                                    {
                                                        str4 = str4 + "," + al_filtervalue[k].ToString();
                                                        str5 = str5 + "," + al_filterreflex[k].ToString();
                                                    }
                                                }
                                                str2 = str4;
                                                str3 = str5;
                                            }
                                            else
                                            {
                                                conAttrShowPos = j.ToString();
                                                str2 = al_filtervalue[k].ToString();
                                                str3 = al_filterreflex[k].ToString();
                                            }
                                        }
                                        else
                                        {
                                            ArrayList list2 = new ArrayList();
                                            list2 = this.GetSplitString(obj3.ConAttrShowPos);
                                            if (list2.Count > 1)
                                            {
                                                conAttrShowPos = conAttrShowPos + "," + obj3.ConAttrShowPos;
                                                string str6 = "";
                                                string str7 = "";
                                                for (int n = 0; n < list2.Count; n++)
                                                {
                                                    if (n == 0)
                                                    {
                                                        str6 = al_filtervalue[k].ToString();
                                                        str7 = al_filterreflex[k].ToString();
                                                    }
                                                    else
                                                    {
                                                        str6 = str6 + al_filtervalue[k].ToString();
                                                        str7 = str7 + "," + al_filterreflex[k].ToString();
                                                    }
                                                }
                                                str2 = str2 + "," + str6;
                                                str3 = str3 + "," + str7;
                                            }
                                            else
                                            {
                                                conAttrShowPos = conAttrShowPos + "," + j.ToString();
                                                str2 = str2 + "," + al_filtervalue[k].ToString();
                                                str3 = str3 + "," + al_filterreflex[k].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        ArrayList splitString = new ArrayList();
                        splitString = this.GetSplitString(obj2.ConAttrShowPos);
                        if (splitString.Count > 1)
                        {
                            this.SetConAttrObject(splitString, conAttrShowPos, str2, str3);
                        }
                        else
                        {
                            obj2.FilterType = 1;
                            obj2.FilterAttribue = str3;
                            obj2.FilterPos = conAttrShowPos;
                            obj2.RelationTableName = str2;
                            this.SetReflexAttr(str3, conAttrShowPos);
                        }
                    }
                }
            }
        }

        private void SetReflexAttr(string str_filterrefex, string str_filterpos)
        {
            ArrayList posValue = new ArrayList();
            ArrayList list2 = new ArrayList();
            posValue = this.GetPosValue(str_filterrefex);
            list2 = this.GetPosValue(str_filterpos);
            for (int i = 0; i < list2.Count; i++)
            {
                int num2 = (int) list2[i];
                int num3 = (int) posValue[i];
                for (int j = 0; j < this.al_all.Count; j++)
                {
                    if ((num2 == j) && (num3 == 1))
                    {
                        DEBoxObject obj2 = (DEBoxObject) this.al_all[i];
                        obj2.IsReflex = true;
                    }
                }
            }
        }
    }
}

