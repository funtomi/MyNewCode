namespace Thyt.TiPLM.PLL.Common
{
    using System;
    using System.Collections;
    using System.Reflection;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Admin.NewResponsibility;

    public class ServiceSwitches
    {
        public static bool DistiPrintAndSignDownLoad = false;
        public static bool IntergationSolidWorksDrawing = false;
        public static bool IsBQErpReport = false;
        public static bool IsCompatibleAutoCAD2004 = true;
        public static bool IsIntergCAXA = false;
        public static bool IsTianGong = false;
        public static bool IsTiIntegratorTopMost = false;
        public static bool SignBackWriteItemID = false;
        public static bool UseAutoLinkRule = false;
        public static bool UseClassFormulaDef = false;
        public static bool UseDataView = false;
        public static bool UseFingerVerify = false;
        public static bool UseTiFileSynService = false;
        public static bool UseTiNotifyService = false;

        public static void LoadCustomizedSwitches()
        {
            IDictionary customizedSwitches = ((IUser) RemoteProxy.GetObject(typeof(IUser))).GetCustomizedSwitches();
            if (customizedSwitches != null)
            {
                SetTypeMembers(typeof(ConstCommon), null, customizedSwitches);
                SetTypeMembers(typeof(ServiceSwitches), null, customizedSwitches);
            }
        }

        private static void SetTypeMembers(Type aType, object obj, IDictionary dic)
        {
            MemberInfo[] members = aType.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            for (int i = 0; i < members.Length; i++)
            {
                if (members[i].MemberType == MemberTypes.Field)
                {
                    FieldInfo field = aType.GetField(members[i].Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                    if (!field.IsLiteral && (field.IsStatic || (obj != null)))
                    {
                        object obj2 = field.IsStatic ? null : obj;
                        string str = (string) dic[aType.Name + "." + field.Name];
                        if (str != null)
                        {
                            if (field.FieldType == typeof(string))
                            {
                                field.SetValue(obj2, str);
                            }
                            if (field.FieldType == typeof(bool))
                            {
                                try
                                {
                                    field.SetValue(obj2, Convert.ToBoolean(str));
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

