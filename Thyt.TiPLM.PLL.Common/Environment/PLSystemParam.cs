namespace Thyt.TiPLM.PLL.Environment
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Common;

    public class PLSystemParam
    {
        public const string PARAMETER_BomEditDocRelaName = "BomEditDocRelaName";
        public const string PARAMETER_BomEditOrderRelaName = "BomEditOrderRelaName";
        public const string PARAMETER_CADInteragtion_ShowAllPartRelations = "CADInteragtion_ShowAllPartRelations";
        public const string PARAMETER_CADInteragtion_ShowPartRelations = "CADInteragtion_ShowPartRelations";
        public const string PARAMETER_CONFIGSTATUS_EXPORT_RELAPRODUCT = "ConfigStatusExportRelaProduct";
        public const string PARAMETER_CONFIGSTATUS_EXPORT_RELAPRODUCTNAME = "ConfigStatusExportRelaProductName";
        public const string PARAMETER_DocTemplate_FileName_Id2Model = "DocTemplate_FileName_Id2Model";
        public const string PARAMETER_DocTemplate_FileName_Name2Model = "DocTemplate_FileName_Name2Model";
        public const string PARAMETER_IMP_AutoRelease = "ImpAutoRelease";
        public const string PARAMETER_IMP_CheckRevsion = "ImpCheckRevsion";
        public const string PARAMETER_Inventor_VirtualPart = "Inventor_VirtualPart";
        public const string PARAMETER_LinkedLastGetWay = "LinkedLastGetWay";
        public const string PARAMETER_Not_Select_ResNode = "Not_Select_ResNode";
        public const string PARAMETER_Relation_Name = "Relation_Name";
        public const string PARAMETER_RES_FilterMapped = "ResourcFilterMapped";
        public const string PARAMETER_RES_NodeText = "ResourcNodeText";
        public const string PARAMETER_Show_Part_Drawing_Tab = "Show_Part_Drawing_Tab";
        public const string PARAMETER_Show_ReadOnly_Attrs = "Show_ReadOnly_Attrs";
        public const string PARAMETER_Thum_Height = "Thum_Height";
        public const string PARAMETER_Thum_Width = "Thum_Width";
        public static string parameterClientPortalToolBar;
        public static string parameterClientPortalView;
        public static string parameterCustomizedInterface;
        public static string SecurityPath = "";
        private static Hashtable systemParamTable = null;

        public static void ClearParametersCache()
        {
            if (systemParamTable != null)
            {
                lock (typeof(Hashtable))
                {
                    systemParamTable.Clear();
                    systemParamTable = null;
                }
            }
        }

        private static string ConvertArrayListToString(ArrayList value)
        {
            string str = "";
            if ((value == null) || (value.Count == 0))
            {
                return str;
            }
            for (int i = 0; i < value.Count; i++)
            {
                object obj2 = value[i];
                if (obj2 != null)
                {
                    if (obj2 is string)
                    {
                        str = str + value[i].ToString() + "<&&>";
                    }
                    else if (obj2 is string[])
                    {
                        string[] strArray = (string[]) obj2;
                        string str2 = "";
                        for (int j = 0; j < (strArray.Length - 1); j++)
                        {
                            str2 = str2 + strArray[j] + "@->";
                        }
                        str2 = str2 + strArray[strArray.Length - 1];
                        str = str + str2 + "<&&>";
                    }
                }
            }
            return str.Remove(str.Length - 4);
        }

        private static ArrayList ConvertStringToArrayList(object value)
        {
            if ((value == null) || value.Equals(""))
            {
                return new ArrayList();
            }
            string str = value.ToString();
            ArrayList list = new ArrayList();
            string[] strArray = str.ToString().Split(new string[] { "<&&>" }, StringSplitOptions.None);
            if (strArray == null)
            {
                return new ArrayList();
            }
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].Contains("@->"))
                {
                    string[] strArray2 = strArray[i].Split(new string[] { "@->" }, StringSplitOptions.None);
                    if ((strArray2 != null) && (strArray2.Length != 0))
                    {
                        list.Add(strArray2);
                    }
                }
                else
                {
                    list.Add(strArray[i]);
                }
            }
            return list;
        }

        public static DECustomExcepion CreateCustomException(DECustomExcepion customException, Guid userOid){
        return Agent.CreateCustomException(customException, userOid);
    }
        
        public static Hashtable CreateExceptionClass(Hashtable ht, Guid userOid){
            return Agent.CreateExceptionClass(ht, userOid);
        }
        public void CreateIdUniqueRule(DEIdUniqueRule rule, Guid userOid)
        {
            Agent.CreateIdUniqueRule(rule, userOid);
        }

        public static void DeleteCustomException(int[] codes, Guid userOid)
        {
            Agent.DeleteCustomException(codes, userOid);
        }

        public static void DeleteExceptionClass(Guid[] classOids, Guid userOid)
        {
            Agent.DeleteExceptionClass(classOids, userOid);
        }

        public void DeleteIdUniqueRule(Guid oid, Guid userOid)
        {
            Agent.DeleteIdUniqueRule(oid, userOid);
        }

        public static DECustomExcepion[] GetAllCustomException(Guid userOid){
          return   Agent.GetAllCustomException(userOid);
        }
        public static Hashtable GetAllExceptionClass(Guid userOid){
         return   Agent.GetAllExceptionClass(userOid);
    }
        public ArrayList GetAllIdUniqueRules(){
         return   
            Agent.GetAllIdUniqueRules();
        }

        public ArrayList GetAllRelationRules() {
         return   
            Agent.GetAllRelationRules();
        }
        public Hashtable GetAllSystemLargeParams() {
         return    
            Agent.GetAllSystemLargeParams();}

        public Hashtable GetAllSystemParams(){
         return    
            Agent.GetAllSystemParams();}

        public string GetAuthorizedEnterprise(){
         return    
            Agent.GetAuthorizedEnterprise();}

        public string GetAuthorizedSoftwareName() {
         return    
            Agent.GetAuthorizedSoftwareName();}

        public static string GetBPMMessageContent(string messageTypeId)
        {
            try
            {
                PLSystemParam param = new PLSystemParam();
                return Convert.ToString(param.GetSystemParameter("BPM_MESSAGE_CONTENT_" + messageTypeId));
            }
            catch
            {
                return "";
            }
        }

        public static string GetBPMMessageTitle(string messageTypeId)
        {
            try
            {
                PLSystemParam param = new PLSystemParam();
                return Convert.ToString(param.GetSystemParameter("BPM_MESSAGE_TITLE_" + messageTypeId));
            }
            catch
            {
                return "";
            }
        }

        public static DECustomExcepion[] GetCustomExcepionByClass(Guid classOid, Guid userOid) {
         return    
            Agent.GetCustomExcepionByClass(classOid, userOid);}

        public static DECustomExcepion GetCustomExcepionByCode(int code, Guid userOid) {
         return   
            Agent.GetCustomExcepionByCode(code, userOid);}

        public static Hashtable GetExceptionClassByOid(Guid classOid, Guid userOid) {
         return   
            Agent.GetExceptionClassByOid(classOid, userOid);
        }
        public bool GetlockAppServer() {
         return   
            Agent.GetlockAppServer();}

        public static int GetMaxExcepionCode(Guid userOid){
         return    
            Agent.GetMaxExcepionCode(userOid);}

        public DateTime GetPLMServerDate() {
         return   
            Agent.GetServerDate();
        }
        public DERelationsRule GetRelationRule(Guid RuleOid) {
         return   
            Agent.GetRelationRule(RuleOid);
        }
        public ArrayList GetRelationRules(string clsName) {
         return   
            Agent.GetRelationRules(clsName);
        }
        public static DateTime GetServerDate()
        {
            PLSystemParam param = new PLSystemParam();
            return param.GetPLMServerDate();
        }

        public object GetSystemLargeParameter(string paramName, string paramUserName){
         return   this.GetAllSystemLargeParams()[paramName + paramUserName];
        }

        public object GetSystemParameter(string paramName)
        {
            lock (typeof(Hashtable))
            {
                return systemParamTable[paramName];
            }
        }

        public object GetSystemParameters(string paramName)
        {
            lock (typeof(Hashtable))
            {
                if (systemParamTable == null)
                {
                    systemParamTable = Agent.GetAllSystemParams();
                }
                Hashtable hashtable = new Hashtable();
                foreach (string str in systemParamTable.Keys)
                {
                    if (str.IndexOf(paramName) == 0)
                    {
                        hashtable.Add(str.Remove(0, paramName.Length), systemParamTable[str]);
                    }
                }
                return hashtable;
            }
        }

        public string GetUpdateDateSpan() {
         return   
            Agent.GetUpdateDateSpan();
        }
        public static bool IsWebGuestUser(string id)
        {
            string str = ParamWebGuestUserId.Trim().ToUpper();
            if (str == "")
            {
                return false;
            }
            ArrayList list = new ArrayList();
            while (str.IndexOf(";") != -1)
            {
                list.Add(str.Substring(0, str.IndexOf(";")));
                if (str.Length > (str.IndexOf(";") + 1))
                {
                    str = str.Substring(str.IndexOf(";") + 1, (str.Length - str.IndexOf(";")) - 1);
                }
                else
                {
                    str = "";
                }
            }
            if (str != "")
            {
                list.Add(str);
            }
            return list.Contains(id.ToUpper());
        }

        public void LockAppServer(Guid logId, string lockDesc)
        {
            Agent.LockAppServer(logId, lockDesc);
        }

        public static DECustomExcepion ModifyCustomException(DECustomExcepion customException, Guid userOid) {
         return   
            Agent.ModifyCustomException(customException, userOid);
        }
        public static Hashtable ModifyExceptionClass(Hashtable ht, Guid userOid) {
         return   
            Agent.ModifyExceptionClass(ht, userOid);
        }
        public void SaveAllRelations(ArrayList rels)
        {
            Agent.SaveAllRelations(rels);
        }

        public static void SetBPMMessageContent(string messageTypeId, string message)
        {
            try
            {
                new PLSystemParam().SetSystemParameter("BPM_MESSAGE_CONTENT_" + messageTypeId, message);
            }
            catch
            {
            }
        }

        public static void SetBPMMessageTitle(string messageTypeId, string message)
        {
            try
            {
                new PLSystemParam().SetSystemParameter("BPM_MESSAGE_TITLE_" + messageTypeId, message);
            }
            catch
            {
            }
        }

        public void SetSystemLargeParameter(string paramName, object paramValue, string paramUserName)
        {
            Agent.SetSystemLargeParameter(paramName, paramValue, paramUserName);
            systemParamTable = null;
        }

        public void SetSystemParameter(string paramName, object paramValue)
        {
            Agent.SetSystemParameter(paramName, paramValue);
            systemParamTable = null;
        }

        public void SetSystemParameters(string paramName, object paramValue)
        {
            Agent.SetSystemParameters(paramName, paramValue);
            systemParamTable = null;
        }

        public void UnlockAppServer(Guid logId)
        {
            Agent.UnlockAppServer(logId);
        }

        private static ISystemParam Agent{
            get{
         return   ((ISystemParam) RemoteProxy.GetObject(typeof(ISystemParam)));
            }
        }
        public static bool AutoCheckInSubCards
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("CHECKIN_SUBCARDS_WITH_FATHER");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("CHECKIN_SUBCARDS_WITH_FATHER", value.ToString());
            }
        }

        public static bool AutoCheckOutSubCards
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("CHECKOUT_SUBCARDS_WITH_FATHER");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("CHECKOUT_SUBCARDS_WITH_FATHER", value.ToString());
            }
        }

        public static bool AutoDeleteSubCards
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("DELETE_SUBCARDS_WITH_FATHER");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("DELETE_SUBCARDS_WITH_FATHER", value.ToString());
            }
        }

        public static bool AutoReleaseSubCards
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("RELEASE_SUBCARDS_WITH_FATHER");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("RELEASE_SUBCARDS_WITH_FATHER", value.ToString());
            }
        }

        public static bool AutoReturnInMidItem
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("AUTO_RETURN_IN_MIDITEM");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("AUTO_RETURN_IN_MIDITEM", value);
                }
                catch
                {
                }
            }
        }

        public static bool AutoReturnWhenLoadItems
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("AUTO_RETURN_WHEN_LOAD_ITEMS");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("AUTO_RETURN_WHEN_LOAD_ITEMS", value.ToString());
            }
        }

        public static int CalculatePrecisionControl
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("CALCULATE_PRECISION_CONTROL");
                    if (systemParameter == null)
                    {
                        return 3;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 3;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("CALCULATE_PRECISION_CONTROL", value);
                }
                catch
                {
                }
            }
        }

        public static bool CardEnableFormat
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("CARD_ENABLE_FORMAT");
                return ((systemParameter != null) && Convert.ToBoolean(systemParameter));
            }
            set
            {
                new PLSystemParam().SetSystemParameter("CARD_ENABLE_FORMAT", value.ToString());
            }
        }

        public static bool CellReturnDetectWord
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("CELL_RETURN_DETECT_WORD");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return true;
                    }
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("CELL_RETURN_DETECT_WORD", value.ToString());
            }
        }

        public static DECreateRule CheckInCardRule
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return new DECreateRule(Convert.ToString(param.GetSystemParameter("PPM_CHECKIN_CARD")));
                }
                catch
                {
                    return new DECreateRule();
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PPM_CHECKIN_CARD", value.ToString());
                }
                catch
                {
                }
            }
        }

        public static bool CheckInWhenItemsEdit
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("AUTO_CHECKIN_IN_ITEMSEDITUSAGE");
                return ((systemParameter != null) && Convert.ToBoolean(systemParameter));
            }
            set
            {
                new PLSystemParam().SetSystemParameter("AUTO_CHECKIN_IN_ITEMSEDITUSAGE", value.ToString());
            }
        }

        public static bool CutHeadAndTailWhileSinglePage
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("CUT_HEAD_TAIL_WHILE_SINGLE_PAGE");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return true;
                    }
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("CUT_HEAD_TAIL_WHILE_SINGLE_PAGE", value.ToString());
            }
        }

        public static bool DelFileInCard
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("DEL_FILE_IN_CARD");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("DEL_FILE_IN_CARD", value);
                }
                catch
                {
                }
            }
        }

        public static bool DelSubCardWithRelation
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("DEL_SUBCARD_WITH_RELATION");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return true;
                    }
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("DEL_SUBCARD_WITH_RELATION", value.ToString());
            }
        }

        public static bool ExportOrginColorPicInCard
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("EXPORT_GRAYSCALE_PIC_IN_CARD");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return true;
                    }
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("EXPORT_GRAYSCALE_PIC_IN_CARD", value.ToString());
            }
        }

        public static bool ExportWithPicInCard
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("EXPORT_WITH_PIC_IN_CARD");
                if (systemParameter != null)
                {
                    return Convert.ToBoolean(systemParameter);
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("EXPORT_WITH_PIC_IN_CARD", value.ToString());
            }
        }

        public static bool ForbidReturnInCard
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("FORBID_RETURN_IN_PPCARD");
                    if (systemParameter == null)
                    {
                        return false;
                    }
                    return Convert.ToBoolean(systemParameter);
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FORBID_RETURN_IN_PPCARD", value);
                }
                catch
                {
                }
            }
        }

        public static bool GrayEleSignWhenPrint
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("GRAY_ELESIGN_WHEN_PRINT");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("GRAY_ELESIGN_WHEN_PRINT", value.ToString());
            }
        }

        public static bool GrayLocPicWhenPrint
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("GRAY_LOCPIC_WHEN_PRINT");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("GRAY_LOCPIC_WHEN_PRINT", value.ToString());
            }
        }

        public static bool GrayPPSignWhenPrint
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("GRAY_PPSIGN_WHEN_PRINT");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return true;
                    }
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("GRAY_PPSIGN_WHEN_PRINT", value.ToString());
            }
        }

        public static bool GrayResPicWhenPrint
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("GRAY_RESPIC_WHEN_PRINT");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("GRAY_RESPIC_WHEN_PRINT", value.ToString());
            }
        }

        public static int HowRespondTempAutoNumber
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_RESPOND_TEMP_AUTONUMBER");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_RESPOND_TEMP_AUTONUMBER", value);
                }
                catch
                {
                }
            }
        }

        public static int IntervalBetweenItems
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("INTERVAL_BETWEEN_ITEMS");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToInt32(systemParameter);
                    }
                    catch
                    {
                        return 0;
                    }
                }
                return 0;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("INTERVAL_BETWEEN_ITEMS", value.ToString());
            }
        }

        public static bool IsAutoLogon
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("IsAutoLogon");
                    return (systemParameter.ToString().Trim() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("IsAutoLogon", value);
                }
                catch
                {
                }
            }
        }

        public static bool IsOutPutMultiPagesInOneSheet
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("OUTPUT_MULTIPAGES_IN_ONESHEET");
                return ((systemParameter != null) && Convert.ToBoolean(systemParameter));
            }
            set
            {
                new PLSystemParam().SetSystemParameter("OUTPUT_MULTIPAGES_IN_ONESHEET", value.ToString());
            }
        }

        public static bool IsShowPhoto
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("IsShowPhoto");
                    return (systemParameter.ToString().Trim() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("IsShowPhoto", value);
                }
                catch
                {
                }
            }
        }

        public static bool ModifyInheritAttrFromPart
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("MODIFY_INHERIT_ATTR_FROM_PART");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("MODIFY_INHERIT_ATTR_FROM_PART", value.ToString());
            }
        }

        public static DEOutputMultiCardPagesInOneSheet OutputMultiCardPagesInOneSheet
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("OUTPUT_MULTI_CARDPAGES_IN_ONESHEET");
                    if (systemParameter == null)
                    {
                        return null;
                    }
                    return new DEOutputMultiCardPagesInOneSheet(systemParameter.ToString());
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("OUTPUT_MULTI_CARDPAGES_IN_ONESHEET", value.ToString());
                }
                catch
                {
                }
            }
        }

        public static bool ParamAvShowAuxiToolBar
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("AV_SHOWAUXITOOLBAR"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("AV_SHOWAUXITOOLBAR", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamAvShowMainToolBar
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("AV_SHOWMAINTOOLBAR"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("AV_SHOWMAINTOOLBAR", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamBOMEditDocRelaName
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("BomEditDocRelaName");
                    if (systemParameter == null)
                    {
                        return null;
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("BomEditDocRelaName", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamBOMEditOrderRelaName
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("BomEditOrderRelaName");
                    if (systemParameter == null)
                    {
                        return null;
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("BomEditOrderRelaName", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamCADInteragtionShowAllPartRelations
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("CADInteragtion_ShowAllPartRelations"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("CADInteragtion_ShowAllPartRelations", value);
                }
                catch
                {
                }
            }
        }

        public static List<string> ParamCADInteragtionShowPartRelations
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("CADInteragtion_ShowPartRelations").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        string[] collection = str.Split(new string[] { ";" }, StringSplitOptions.None);
                        if ((collection == null) || (collection.Length == 0))
                        {
                            return null;
                        }
                        return new List<string>(collection);
                    }
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    if ((value == null) || (value.Count == 0))
                    {
                        param.SetSystemParameter("CADInteragtion_ShowPartRelations", "");
                    }
                    else
                    {
                        string paramValue = "";
                        for (int i = 0; i < value.Count; i++)
                        {
                            paramValue = paramValue + value[i] + ";";
                        }
                        param.SetSystemParameter("CADInteragtion_ShowPartRelations", paramValue);
                    }
                }
                catch
                {
                }
            }
        }

        public static bool ParamCheckWhenCheckin
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PARAM_CHECK_WHEN_CHECKIN"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PARAM_CHECK_WHEN_CHECKIN", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamCodeAuto
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("CODE_AUTO"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("CODE_AUTO", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamDefaultGroup
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("BPM_DEFAULT_OBJGROUP"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("BPM_DEFAULT_OBJGROUP", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamDocTemplateFileNameId2Model
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("DocTemplate_FileName_Id2Model"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("DocTemplate_FileName_Id2Model", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamDocTemplateFileNameName2Model
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("DocTemplate_FileName_Name2Model"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("DocTemplate_FileName_Name2Model", value);
                }
                catch
                {
                }
            }
        }

        public static string Parameter3DDocTypes
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("3D_DocTypes");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("3D_DocTypes", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterActivityActorSelected
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ACTIVITY_ACTOR_SELECTED"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ACTIVITY_ACTOR_SELECTED", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterADAdminName
        {
            get
            {
                try
                {
                    return new PLSystemParam().GetSystemParameter("ADREGION_ADMINNAME").ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ADREGION_ADMINNAME", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterADPassword
        {
            get
            {
                try
                {
                    return new PLSystemParam().GetSystemParameter("ADREGION_ADMINPASSWORD").ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ADREGION_ADMINPASSWORD", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterADRegionName
        {
            get
            {
                try
                {
                    return new PLSystemParam().GetSystemParameter("ADREGION_SERVICENAME").ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ADREGION_SERVICENAME", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterADRegionRoleuser
        {
            get
            {
                try
                {
                    return new PLSystemParam().GetSystemParameter("ADREGION_ROLEUSER").ToString();
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ADREGION_ROLEUSER", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterAliasOfTiAdmin
        {
            get
            {
                object systemParameter = null;
                try
                {
                    systemParameter = new PLSystemParam().GetSystemParameter("ALIAS_OF_TIADMIN");
                    if ((systemParameter != null) && (systemParameter.ToString() != ""))
                    {
                        return (string) systemParameter;
                    }
                }
                catch
                {
                }
                if (ConstCommon.FUNCTION_EDMS)
                {
                    return "TiEDM系统管理配置工具";
                }
                return (ConstCommon.ProductName + "系统管理配置工具");
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ALIAS_OF_TIADMIN", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterAliasOfTiDesk
        {
            get
            {
                object systemParameter = null;
                try
                {
                    systemParameter = new PLSystemParam().GetSystemParameter("ALIAS_OF_TIDESK");
                    if ((systemParameter != null) && (systemParameter.ToString() != ""))
                    {
                        return (string) systemParameter;
                    }
                }
                catch
                {
                }
                if (ConstCommon.FUNCTION_EDMS)
                {
                    return "TiEDM桌面系统";
                }
                return (ConstCommon.ProductName + "桌面系统");
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ALIAS_OF_TIDESK", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterAliasOfTiMessage
        {
            get
            {
                object systemParameter = null;
                try
                {
                    systemParameter = new PLSystemParam().GetSystemParameter("ALIAS_OF_TIMESSAGE");
                    if ((systemParameter != null) && (systemParameter.ToString() != ""))
                    {
                        return (string) systemParameter;
                    }
                }
                catch
                {
                }
                if (ConstCommon.FUNCTION_EDMS)
                {
                    return "TiEDM消息工具";
                }
                return (ConstCommon.ProductName + "消息工具");
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ALIAS_OF_TIMESSAGE", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterAliasOfTiModeler
        {
            get
            {
                object systemParameter = null;
                try
                {
                    systemParameter = new PLSystemParam().GetSystemParameter("ALIAS_OF_TIMODELER");
                    if ((systemParameter != null) && (systemParameter.ToString() != ""))
                    {
                        return (string) systemParameter;
                    }
                }
                catch
                {
                }
                if (ConstCommon.FUNCTION_EDMS)
                {
                    return "TiEDM企业建模工具";
                }
                if (ConstCommon.FUNCTION_IORS)
                {
                    return "信息对象注册管理系统管理工具";
                }
                return (ConstCommon.ProductName + "企业建模工具");
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ALIAS_OF_TIMODELER", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterAliasOfTiResource
        {
            get
            {
                object systemParameter = null;
                try
                {
                    systemParameter = new PLSystemParam().GetSystemParameter("ALIAS_OF_TIRESOURCE");
                    if ((systemParameter != null) && (systemParameter.ToString() != ""))
                    {
                        return (string) systemParameter;
                    }
                }
                catch
                {
                }
                if (ConstCommon.FUNCTION_EDMS)
                {
                    return "TiEDM工程资源管理工具";
                }
                return (ConstCommon.ProductName + "工程资源管理工具");
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ALIAS_OF_TIRESOURCE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterAutoAlterActivityActor
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("AUTO_ALTER_ACTIVITYACTOR"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("AUTO_ALTER_ACTIVITYACTOR", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterCardTimeFormat
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("CARD_TIME_FORMAT").ToString();
                }
                catch
                {
                    return "yyyy-MM-dd";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("CARD_TIME_FORMAT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterClassFormula
        {
            get
            {
                string str = "提示";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("TIPLM_CLASSFORMULA").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIPLM_CLASSFORMULA", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterClientPortalToolBar
        {
            get
            {
                try
                {
                    if (parameterClientPortalToolBar == null)
                    {
                        object systemLargeParameter = new PLSystemParam().GetSystemLargeParameter("CLIENTPORTALOPR_TOOLBAR", string.Empty);
                        if (systemLargeParameter == null)
                        {
                            parameterClientPortalToolBar = string.Empty;
                        }
                        else
                        {
                            parameterClientPortalToolBar = systemLargeParameter.ToString();
                        }
                    }
                }
                catch
                {
                    parameterClientPortalToolBar = string.Empty;
                }
                return parameterClientPortalToolBar;
            }
            set
            {
                try
                {
                    parameterClientPortalToolBar = value;
                    new PLSystemParam().SetSystemLargeParameter("CLIENTPORTALOPR_TOOLBAR", value, string.Empty);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static string ParameterClientPortalView
        {
            get
            {
                try
                {
                    if (parameterClientPortalView == null)
                    {
                        object systemLargeParameter = new PLSystemParam().GetSystemLargeParameter("CLIENTPORTAL_VIEWTYPE", string.Empty);
                        if (systemLargeParameter == null)
                        {
                            parameterClientPortalView = string.Empty;
                        }
                        else
                        {
                            parameterClientPortalView = systemLargeParameter.ToString();
                        }
                    }
                }
                catch
                {
                    parameterClientPortalView = string.Empty;
                }
                return parameterClientPortalView;
            }
            set
            {
                parameterClientPortalView = value;
                try
                {
                    new PLSystemParam().SetSystemLargeParameter("CLIENTPORTAL_VIEWTYPE", value, string.Empty);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static bool ParameterConfigDefaultForbidden
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return (param.GetSystemParameter("CONFIG_DEFAULTFORBIDDEN").ToString() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string paramValue = value ? "True" : "False";
                    param.SetSystemParameter("CONFIG_DEFAULTFORBIDDEN", paramValue);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterConfigStatusExportRelaProduct
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return (param.GetSystemParameter("ConfigStatusExportRelaProduct").ToString() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string paramValue = value ? "True" : "False";
                    param.SetSystemParameter("ConfigStatusExportRelaProduct", paramValue);
                }
                catch
                {
                }
            }
        }

        public static string ParameterConfigStatusExportRelaProductName
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("ConfigStatusExportRelaProductName");
                    if (systemParameter != null)
                    {
                        return systemParameter.ToString();
                    }
                    return "";
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ConfigStatusExportRelaProductName", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterCustomizedInterface
        {
            get
            {
                try
                {
                    if (parameterCustomizedInterface == null)
                    {
                        object systemLargeParameter = new PLSystemParam().GetSystemLargeParameter("CLIENTPORTAL_CUSTOMIZEDINTERFACE", string.Empty);
                        if (systemLargeParameter == null)
                        {
                            parameterCustomizedInterface = string.Empty;
                        }
                        else
                        {
                            parameterCustomizedInterface = systemLargeParameter.ToString();
                        }
                    }
                }
                catch
                {
                    return string.Empty;
                }
                return parameterCustomizedInterface;
            }
            set
            {
                try
                {
                    parameterCustomizedInterface = value;
                    new PLSystemParam().SetSystemLargeParameter("CLIENTPORTAL_CUSTOMIZEDINTERFACE", value, string.Empty);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static bool ParameterDownLoadAutoOutputToDocTemplet
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("DOCTEMPLET_AUTOOUTPUTVALUE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("DOCTEMPLET_AUTOOUTPUTVALUE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterEnableLUServer
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("LUSERVER_ENABLED"));
                }
                catch
                {
                    if (((ParameterLUFtpAddress != "") && (ParameterLUFtpUser != "")) && ((ParameterLUFtpUser != "") && (ParameterLUFtpPassword != null)))
                    {
                        ParameterEnableLUServer = true;
                        return true;
                    }
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUSERVER_ENABLED", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterExteriorSaftWebAddress
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SAFECAD_EXTERIORWEBSVR").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SAFECAD_EXTERIORWEBSVR", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterFileReleaseCopy
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("FILERELEASE_COPY"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FILERELEASE_COPY", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterFileStoreBackground
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("FILESTORE_BACKGROUND"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FILESTORE_BACKGROUND", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterFingerAddin
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("FINGER_PARAMETERFINGERADDIN");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FINGER_PARAMETERFINGERADDIN", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterFirstStrictSignatureMode
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("SIGNATUREFRIST_IS_STRICT"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNATUREFRIST_IS_STRICT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterFolderMappingDriver
        {
            get
            {
                string str = "X:";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("FOLDERMAPPING_DRIVER").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FOLDERMAPPING_DRIVER", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterForcedUseCodeAsID
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("FORCED_USECODE_AS_ID"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FORCED_USECODE_AS_ID", value);
                }
                catch
                {
                }
            }
        }

        public static decimal ParameterGSDJ
        {
            get
            {
                decimal num = 0M;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToDecimal(param.GetSystemParameter("GSDJ"));
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("GSDJ", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterGWDTODWG
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return (param.GetSystemParameter("GWDTODWG").ToString() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string paramValue = value ? "True" : "False";
                    param.SetSystemParameter("GWDTODWG", paramValue);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterIDAutoGenerate
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ID_AUTOGENERATE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ID_AUTOGENERATE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterIDCaseSensitive
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ID_CASE_SENSITIVE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ID_CASE_SENSITIVE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterImpAutoRelease
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ImpAutoRelease"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ImpAutoRelease", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterImpCheckRevsion
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ImpCheckRevsion"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ImpCheckRevsion", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterInventorVirtualPart
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("Inventor_VirtualPart"));
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Inventor_VirtualPart", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterItemIdUnique
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ITEMID_UNIQUE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ITEMID_UNIQUE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterLocalFolerCanDrag
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("LOCALFOLDERDRAG_SUPPORT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LOCALFOLDERDRAG_SUPPORT", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterLoginAllowFaultNum
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToInt32(param.GetSystemParameter("LOGIN_ALLOWFAULTNUM"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LOGIN_ALLOWFAULTNUM", value);
                }
                catch
                {
                }
            }
        }

        public static decimal ParameterLoginForbbinTime
        {
            get
            {
                decimal num = Convert.ToDecimal((double) 0.5);
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    decimal num2 = Convert.ToDecimal(param.GetSystemParameter("LOGIN_FORRBINTIME"));
                    if (num2 <= 0M)
                    {
                        num2 = num;
                    }
                    return num2;
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LOGIN_FORRBINTIME", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterLUFtpAddress
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("LUFTPADDRESS"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUFTPADDRESS", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterLUFtpPassword
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("LUFTPPASSWORD"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUFTPPASSWORD", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterLUFtpPort
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToInt32(param.GetSystemParameter("LUFTPPORT"));
                }
                catch
                {
                    return 0x15;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUFTPPORT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterLUFtpUser
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("LUFTPUSER"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUFTPUSER", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterLUIpRule
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("LUSERVER_IPRULE"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUSERVER_IPRULE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterLURuleApplied4All
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("LUSERVER_APPLIED4ALL"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUSERVER_APPLIED4ALL", value);
                }
                catch
                {
                }
            }
        }

        public static decimal ParameterLUScnFileSize
        {
            get
            {
                decimal num = 0M;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToDecimal(param.GetSystemParameter("LUSCNFILESIZE"));
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUSCNFILESIZE", value);
                }
                catch
                {
                }
            }
        }

        public static DateTime ParameterLUScnFileTime
        {
            get
            {
                DateTime now = DateTime.Now;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToDateTime(param.GetSystemParameter("LUSCNFILETIME"));
                }
                catch
                {
                    return now;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LUSCNFILETIME", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterMultiDownloadSupport
        {
            get {

                return true;
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("MULTIDOWNLOAD_SUPPORT", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterPartDefaultNum
        {
            get
            {
                int num = 0;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToInt32(param.GetSystemParameter("PART_DEFAULT_NUM"));
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PART_DEFAULT_NUM", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterPartMaxLevel
        {
            get
            {
                int num = 30;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    num = Convert.ToInt32(param.GetSystemParameter("PART_MAX_LEVEL"));
                    if (num == 0)
                    {
                        num = 30;
                    }
                    return num;
                }
                catch
                {
                    return 10;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PART_MAX_LEVEL", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordContinuous
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("PASSWORD_CONTINUOUS");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_CONTINUOUS", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordContinuousConstraint
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_CONTINUOUS_CONSTRAINT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_CONTINUOUS_CONSTRAINT", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterPassWordLength
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToInt32(param.GetSystemParameter("PASSWORD_LENGTH"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_LENGTH", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordLengthConstraint
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_LENGTH_CONSTRAINT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_LENGTH_CONSTRAINT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordMixCaptialSmall
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_MIXCAPTIALSMALL"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_MIXCAPTIALSMALL", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordMixCaptialSmallConstraint
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_MIXCAPTIALSMALL_CONSTRAINT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_MIXCAPTIALSMALL_CONSTRAINT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordRepeat
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("PASSWORD_REPEAT");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_REPEAT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordRepeatConstraint
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_REPEAT_CONSTRAINT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_REPEAT_CONSTRAINT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordSimpleLetter
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("PASSWORD_SIMPLELETTER");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_SIMPLELETTER", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPassWordSimpleLetterConstraint
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PASSWORD_SIMPLELETTER_CONSTRAINT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PASSWORD_SIMPLELETTER_CONSTRAINT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterProcessInstClassName
        {
            get
            {
                string str = "";
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("PROCESS_INSTANCE_CODETYPE");
                    if (systemParameter != null)
                    {
                        str = systemParameter.ToString();
                    }
                    return str;
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PROCESS_INSTANCE_CODETYPE", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterProcessInstCodeCommand
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("PROCESS_INSTANCE_CODECOMMAND").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PROCESS_INSTANCE_CODECOMMAND", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterProjectServerDBAddress
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("PROJECTSERVER_DBADDRESS").ToString();
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PROJECTSERVER_DBADDRESS", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterProjectServerIPAddress
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("PROJECTSERVER_IPADDRESS").ToString();
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PROJECTSERVER_IPADDRESS", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterPSDisplayPSOptionLabel
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PS_DISPLAY_PSOPTION_LABEL"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PS_DISPLAY_PSOPTION_LABEL", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterRearrangePSOrders
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("REARRANGE_PSORDERS"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("REARRANGE_PSORDERS", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterRenameIdUnderUnreleased
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("RENAMEID_UNDER_UNRELEASED"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RENAMEID_UNDER_UNRELEASED", value);
                }
                catch
                {
                }
            }
        }

        public static ArrayList ParameterResFilter
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return ConvertStringToArrayList(param.GetSystemParameter("ResourcFilterMapped"));
                }
                catch
                {
                    return new ArrayList();
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ResourcFilterMapped", ConvertArrayListToString(value));
                }
                catch
                {
                }
            }
        }

        public static string ParameterResNodeText
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("ResourcNodeText"));
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ResourcNodeText", value);
                }
                catch
                {
                }
            }
        }

        public static ArrayList ParameterResRels
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return ConvertStringToArrayList(param.GetSystemParameter("RES_TITLE_"));
                }
                catch
                {
                    return new ArrayList();
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RES_TITLE_", ConvertArrayListToString(value));
                }
                catch
                {
                }
            }
        }

        public static string ParameterRkRuleContain
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("RkRuleContain"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RkRuleContain", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterRkRuleNotContain
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("RkRuleNotContain"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RkRuleNotContain", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterSaftCadAddress
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SAFECAD_SUPPORT").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SAFECAD_SUPPORT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterSaftWebAddress
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SAFECAD_WEBSVR").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SAFECAD_WEBSVR", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterSelectAllActivity
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("SELECT_ALL_ACTIVITY"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SELECT_ALL_ACTIVITY", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterSelectNoObjectActivity
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("SELECT_NOOBJECT_ACTIVITY"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SELECT_NOOBJECT_ACTIVITY", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterSessionDefaultAddress
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SESSION_DEFAULTADDRESS").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SESSION_DEFAULTADDRESS", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterSignatureSpecialBizid
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("SIGNATURE_SPECIAL_BIZID"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNATURE_SPECIAL_BIZID", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterSignTimeFormat
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SIGN_TIME_FORMAT").ToString();
                }
                catch
                {
                    return "yyyy-MM-dd";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGN_TIME_FORMAT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterStrictSignatureMode
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("SIGNATURE_IS_STRICT"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNATURE_IS_STRICT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterTableSpaceName
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("TIPLM_TABLESPACE").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIPLM_TABLESPACE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterTideskShowResource
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWRESOURCE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWRESOURCE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterTypeExpandAuto
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("typeExpandAuto"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("typeExpandAuto", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUPloadSign
        {
            get
            {
                bool flag = false;
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return (param.GetSystemParameter("UPLOAD_SIGNEDFILE").ToString() == "Y");
                }
                catch
                {
                    return flag;
                }
            }
            set
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string paramValue = value ? "Y" : "N";
                    param.SetSystemParameter("UPLOAD_SIGNEDFILE", paramValue);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUseCustomizedRevision
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("CUSTOMIZEDREVISION"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("CUSTOMIZEDREVISION", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUseLetterRevision
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("LETTERREVISION"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LETTERREVISION", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUseReBack
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("FOLDER_REBACK"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FOLDER_REBACK", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUserSetSignAllowed
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("USERSETSIGN_ALLOWED"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("USERSETSIGN_ALLOWED", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterUseRuleOption
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("RULE_OPTION"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RULE_OPTION", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterVerifyBrowseItem
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("VERIFY_BROWSEITEM"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("VERIFY_BROWSEITEM", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParameterViewFileByEditor
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("ViewFileByEditor"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("ViewFileByEditor", value);
                }
                catch
                {
                }
            }
        }

        public static string ParameterWebServerPath
        {
            get
            {
                string str = "";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("WEBSERVER_PATH").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("WEBSERVER_PATH", value);
                }
                catch
                {
                }
            }
        }

        public static int ParameterXGZZGMAX
        {
            get
            {
                int num = 0x1869f;
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("XGZZG_CODE_MAX");
                    if (systemParameter != null)
                    {
                        num = Convert.ToInt32(systemParameter);
                    }
                    return num;
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("XGZZG_CODE_MAX", value.ToString());
                }
                catch
                {
                }
            }
        }

        public static int ParameterXGZZGMIN
        {
            get
            {
                int num = 0;
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("XGZZG_CODE_MIN");
                    if (systemParameter != null)
                    {
                        num = Convert.ToInt32(systemParameter);
                    }
                    return num;
                }
                catch
                {
                    return num;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("XGZZG_CODE_MIN", value.ToString());
                }
                catch
                {
                }
            }
        }

        public static bool ParamFileSaveFromClient
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("FILESAVE_FROMCLIENT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("FILESAVE_FROMCLIENT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamGetLinksUseSP
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("GETLINKRELATION_USESP"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("GETLINKRELATION_USESP", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamLinedGetLastWay
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("LinkedLastGetWay");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("LinkedLastGetWay", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamLoginOnMultiMachine
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return ((param.GetSystemParameter("Login_On_MultiMachine") == null) || Convert.ToBoolean(param.GetSystemParameter("Login_On_MultiMachine")));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Login_On_MultiMachine", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamNewAttrEnable2VirtualClass
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PARAM_NEWATTRENABLED2VIRTUALCLASSS"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PARAM_NEWATTRENABLED2VIRTUALCLASSS", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamNotSelectResNode
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("Not_Select_ResNode"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Not_Select_ResNode", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamProcessOnlineAlert
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("PARAM_USE_ONLINEALERT"));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PARAM_USE_ONLINEALERT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamPSOptionLabelFormat
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("PSOPTION_LABEL_FORMAT"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("PSOPTION_LABEL_FORMAT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamRelationName
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("Relation_Name"));
                }
                catch
                {
                    return "PARTTODOC";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Relation_Name", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamShowPartRelatedDrawing
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("Show_Part_Drawing_Tab"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Show_Part_Drawing_Tab", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamShowReadOnlyAttrs
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("Show_ReadOnly_Attrs"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Show_ReadOnly_Attrs", value);
                }
                catch
                {
                }
            }
        }

        public static int ParamSignBmpHightAtOfficeOutPut
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("SIGNBMPSIZE_AT_OFFICEOUTPUT").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        return Convert.ToInt32(str);
                    }
                    return 20;
                }
                catch
                {
                    return 20;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNBMPSIZE_AT_OFFICEOUTPUT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamSignDateFlag
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("SIGNDATEFLAG_AT_OFFICEOUTPUT").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        return str;
                    }
                    return "签字日期";
                }
                catch
                {
                    return "签字日期";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNDATEFLAG_AT_OFFICEOUTPUT", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamSignFlag
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("SIGNFLAG_AT_OFFICEOUTPUT").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        return str;
                    }
                    return "签字";
                }
                catch
                {
                    return "签字";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SIGNFLAG_AT_OFFICEOUTPUT", value);
                }
                catch
                {
                }
            }
        }

        public static int ParamThumHeight
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("Thum_Height").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        return Convert.ToInt32(str);
                    }
                    return 80;
                }
                catch
                {
                    return 80;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Thum_Height", value);
                }
                catch
                {
                }
            }
        }

        public static int ParamThumWdith
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    string str = param.GetSystemParameter("Thum_Width").ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        return Convert.ToInt32(str);
                    }
                    return 100;
                }
                catch
                {
                    return 100;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("Thum_Width", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowFile
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWFILE"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWFILE", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowFolder
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWFOLDER"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWFOLDER", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowMyTask
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWMYTASK"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWMYTASK", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowProduct
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWPRODUCT"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWPRODUCT", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowSearch
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWSEARCH"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWSEARCH", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamTideskShowWork
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("TIDESKSHOWWORK"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("TIDESKSHOWWORK", value);
                }
                catch
                {
                }
            }
        }

        public static bool ParamVarConfigCheckStatusValues
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToBoolean(param.GetSystemParameter("VARCONFIG_CHECKSTATUSVALUES"));
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("VARCONFIG_CHECKSTATUSVALUES", value);
                }
                catch
                {
                }
            }
        }

        public static string ParamWebGuestUserId
        {
            get
            {
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return Convert.ToString(param.GetSystemParameter("WEBGUEST_USERID"));
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("WEBGUEST_USERID", value);
                }
                catch
                {
                }
            }
        }

        public static bool RTXIsUse
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("RTX_ISUSE");
                    return (systemParameter.ToString().Trim() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RTX_ISUSE", value);
                }
                catch
                {
                }
            }
        }

        public static string RTXServerAddr
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("RTX_SERVERADDR");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RTX_SERVERADDR", value);
                }
                catch
                {
                }
            }
        }

        public static string RTXServerPort
        {
            get
            {
                string str = "0";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("RTX_SERVERPORT").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("RTX_SERVERPORT", value);
                }
                catch
                {
                }
            }
        }

        public static bool SameCardHeaderItems
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("SAME_CARD_HEADER_ITEMS");
                if (systemParameter != null)
                {
                    return Convert.ToBoolean(systemParameter);
                }
                return true;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("SAME_CARD_HEADER_ITEMS", value.ToString());
            }
        }

        public static bool SecurityDiskIsUse
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SecurityDiskIsUse");
                    return (systemParameter.ToString().Trim() == "True");
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SecurityDiskIsUse", value);
                }
                catch
                {
                }
            }
        }

        public static string SecurityDiskSize
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SecurityDiskSize");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SecurityDiskSize", value);
                }
                catch
                {
                }
            }
        }

        public static bool SinglePageWhenExportCard
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("SINGLE_PAGE_WHEN_EXPORT_CARD");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("SINGLE_PAGE_WHEN_EXPORT_CARD", value.ToString());
            }
        }

        public static string SMTPFromEmail
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SMTP_FROMEMAIL");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_FROMEMAIL", value);
                }
                catch
                {
                }
            }
        }

        public static string SMTPIP
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SMTP_IP");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_IP", value);
                }
                catch
                {
                }
            }
        }

        public static bool SMTPISUSE
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SMTP_ISUSE");
                    return (systemParameter.ToString().Trim() == "True");
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_ISUSE", value);
                }
                catch
                {
                }
            }
        }

        public static string SMTPPassword
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SMTP_PASSWORD");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_PASSWORD", value);
                }
                catch
                {
                }
            }
        }

        public static string SMTPPort
        {
            get
            {
                string str = "0";
                try
                {
                    PLSystemParam param = new PLSystemParam();
                    return param.GetSystemParameter("SMTP_PORT").ToString();
                }
                catch
                {
                    return str;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_PORT", value);
                }
                catch
                {
                }
            }
        }

        public static string SMTPUserName
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("SMTP_USERNAME");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("SMTP_USERNAME", value);
                }
                catch
                {
                }
            }
        }

        public static bool SubCardsAutoEnterIntoProcess
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("SUBCARDS_AUTO_ENTERINTO_PROCESS");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("SUBCARDS_AUTO_ENTERINTO_PROCESS", value.ToString());
            }
        }

        public static bool UpdataCardAttrsFromPart
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("UPDATA_CARD_ATTRS_FROM_PART");
                    return ((systemParameter == null) || Convert.ToBoolean(systemParameter));
                }
                catch
                {
                    return true;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("UPDATA_CARD_ATTRS_FROM_PART", value);
                }
                catch
                {
                }
            }
        }

        public static bool UpdateSubCardFromParent
        {
            get
            {
                object systemParameter = new PLSystemParam().GetSystemParameter("UPDATE_SUBCARD_FROM_PARENT");
                if (systemParameter != null)
                {
                    try
                    {
                        return Convert.ToBoolean(systemParameter);
                    }
                    catch
                    {
                        return false;
                    }
                }
                return false;
            }
            set
            {
                new PLSystemParam().SetSystemParameter("UPDATE_SUBCARD_FROM_PARENT", value.ToString());
            }
        }

        public static string WebReleasedCardTypes
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("WEB_RELEASED_CARD_TYPE");
                    if (systemParameter == null)
                    {
                        return "";
                    }
                    return Convert.ToString(systemParameter);
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("WEB_RELEASED_CARD_TYPE", value);
                }
                catch
                {
                }
            }
        }

        public static int WhenDataNotEnough
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_DO_WHEN_DATA_NOT_ENOUGH");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_DO_WHEN_DATA_NOT_ENOUGH", value);
                }
                catch
                {
                }
            }
        }

        public static int WhenMultiFormula
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_DO_WHEN_MULTI_FORMULA");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_DO_WHEN_MULTI_FORMULA", value);
                }
                catch
                {
                }
            }
        }

        public static int WhenMultiResource
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_DO_WHEN_MULTI_RESOURCE");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_DO_WHEN_MULTI_RESOURCE", value);
                }
                catch
                {
                }
            }
        }

        public static int WhenNoneFormula
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_DO_WHEN_NONE_FORMULAS");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_DO_WHEN_NONE_FORMULAS", value);
                }
                catch
                {
                }
            }
        }

        public static int WhenNoneResource
        {
            get
            {
                try
                {
                    object systemParameter = new PLSystemParam().GetSystemParameter("HOW_DO_WHEN_NONE_RESOURCE");
                    if (systemParameter == null)
                    {
                        return 1;
                    }
                    return Convert.ToInt32(systemParameter);
                }
                catch
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    new PLSystemParam().SetSystemParameter("HOW_DO_WHEN_NONE_RESOURCE", value);
                }
                catch
                {
                }
            }
        }
    }
}

