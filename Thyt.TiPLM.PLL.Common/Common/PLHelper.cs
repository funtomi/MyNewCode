namespace Thyt.TiPLM.PLL.Common {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using Thyt.TiPLM.Common.Helper;
    using Thyt.TiPLM.Common.Interface.Product;
    using Thyt.TiPLM.Common.Interface.Project2;
    using Thyt.TiPLM.Common.Interface.Report;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Project2;
    using Thyt.TiPLM.DEL.Report;
    using Thyt.TiPLM.PLL.Environment;

    public class PLHelper {
        private PLHelper() {
        }

        public static ArrayList DataTableToBizItems(Guid logoid, DataTable dt, string roidname = "PLM_R_OID") {
            if (dt == null) {
                return new ArrayList();
            }
            ArrayList revOids = new ArrayList();
            foreach (DataRow row in dt.Rows) {
                revOids.Add(new Guid((byte[])row[roidname.ToUpper()]));
            }
            return ItemAgent.GetBizItemsByRevisions(revOids, Guid.Empty, logoid, BizItemMode.BizItem);
        }

        private static object DbHelper(string type, Guid session, string sqlstr, Assembly callassm2, List<DbParameter2> parameters = null, CommandType commandType = CommandType.Text, string ConnectionString = null, DbProviderType providerType = DbProviderType.Oracle, DataTable dt = null, string idcol = "id", string statecol = "__status", string CurrentUserID = "", IList<string> listSql = null, IList<IList<DbParameter2>> listParameters = null, IList<CommandType> listCommandType = null, object tag = null) {
            Dictionary<string, object> args = new Dictionary<string, object> {
                { 
                    "type",
                    type
                },
                { 
                    "session",
                    session
                },
                { 
                    "sqlstr",
                    sqlstr
                },
                { 
                    "parameters",
                    parameters
                },
                { 
                    "commandType",
                    commandType
                },
                { 
                    "ConnectionString",
                    ConnectionString
                },
                { 
                    "providerType",
                    providerType
                }
            };
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null) {
                entryAssembly = callassm2;
            }
            args.Add("callassm", entryAssembly.GetName().Name);
            args.Add("dt", dt);
            args.Add("idcol", idcol);
            args.Add("statecol", statecol);
            args.Add("CurrentUserID", CurrentUserID);
            args.Add("listSql", listSql);
            args.Add("listParameters", listParameters);
            args.Add("listCommandType", listCommandType);
            args.Add("tag", tag);
            return ProjAgent.ProjExec(ExecType.PLMHelper, args);
        }

        private static object DeserializeObject(byte[] pBytes) {
            object obj2 = null;
            if ((pBytes != null) && (pBytes.Length != 0)) {
                MemoryStream serializationStream = new MemoryStream(pBytes) {
                    Position = 0L
                };
                obj2 = new BinaryFormatter().Deserialize(serializationStream);
                serializationStream.Close();
            }
            return obj2;
        }

        public static List<object> ExecProc(Guid session, string sqlstr, List<DbParameter2> parameters = null, CommandType commandType = CommandType.Text, DbProviderType providerType = DbProviderType.Oracle, string ConnectionString = null) {
            return (DbHelper("ExecProc", session, sqlstr, Assembly.GetCallingAssembly(), parameters, commandType, ConnectionString, providerType, null, "id", "__status", "", null, null, null, null) as List<object>);
        }
        public static int ExecSql(Guid session, string sqlstr, List<DbParameter2> parameters = null, object tag = null, CommandType commandType = CommandType.Text, DbProviderType providerType = DbProviderType.Oracle, string ConnectionString = null) {
            return ((int)DbHelper("ExecSql", session, sqlstr, Assembly.GetCallingAssembly(), parameters, commandType, ConnectionString, providerType, null, "id", null, "", null, null, null, tag));
        }
        public static void ExecSqls(Guid session, IList<string> listSql, IList<IList<DbParameter2>> listParameters = null, object tag = null, IList<CommandType> listCommandType = null, DbProviderType providerType = DbProviderType.Oracle, string ConnectionString = null) {
            DbHelper("ExecSqls", session, "", Assembly.GetCallingAssembly(), null, CommandType.Text, ConnectionString, providerType, null, "id", "__status", "", listSql, listParameters, listCommandType, tag);
        }

        public static DataTable GetDataTableBYProcedure(Guid logoid, string procedurename, List<object> paramslist) {
            Dictionary<string, DEProcArg2> dictionary = DeserializeObject(ChartAgent.GetParamsByTemp(logoid, procedurename)) as Dictionary<string, DEProcArg2>;
            int num = 0;
            foreach (DEProcArg2 arg in dictionary.Values) {
                if ((arg.Direction == ParameterDirection.Output) || (arg.Direction == ParameterDirection.ReturnValue)) {
                    break;
                }
                arg.Value=paramslist[num];
                num++;
            }
            return ChartAgent.GetTableExecProc(logoid, procedurename, dictionary).Tables[0];
        }

        public static T GetObjectFormDb<T>(string pname, bool ByteFormat = false, string logid = "") {
            T local = default(T);
            Dictionary<string, object> args = new Dictionary<string, object> {
                { 
                    "type",
                    "GetObjectFormDb"
                },
                { 
                    "pname",
                    pname
                },
                { 
                    "logid",
                    logid
                }
            };
            object obj2 = ProjAgent.ProjExec(ExecType.PLMHelper, args);
            if (obj2 == null) {
                return local;
            }
            if (ByteFormat) {
                return PLMSerializer.FromBytes<T>(PLMSerializer.FromXML<byte[]>(obj2.ToString()));
            }
            return PLMSerializer.FromXML<T>(obj2.ToString());
        }

        public static DataTable QuerySql(Guid session, string sqlstr, List<DbParameter2> parameters = null, CommandType commandType = CommandType.Text, DbProviderType providerType = DbProviderType.Oracle, string ConnectionString = null) {
            return (DbHelper("QuerySql", session, sqlstr, Assembly.GetCallingAssembly(), parameters, commandType, ConnectionString, providerType, null, "id", "__status", "", null, null, null, null) as DataTable);
        }

        public static void SaveDataTable(Guid session, DataTable dt, string idcol = "id", object tag = null, string statecol = "__status", string CurrentUserID = "", DbProviderType providerType = DbProviderType.Oracle, string ConnectionString = null) {
            DbHelper("SaveDataTable", session, "", Assembly.GetCallingAssembly(), null, CommandType.Text, ConnectionString, providerType, dt, idcol, statecol, CurrentUserID, null, null, null, tag);
        }

        public static void SetObjectToDb<T>(string pname, T pvalue, bool ByteFormat = false, string logid = "") {
            try {
                PLSystemParam param = new PLSystemParam();
                string paramValue = "";
                if (ByteFormat) {
                    paramValue = PLMSerializer.ToXML<byte[]>(PLMSerializer.ToBytes<T>(pvalue));
                } else {
                    paramValue = PLMSerializer.ToXML<T>(pvalue);
                }
                param.SetSystemLargeParameter(pname, paramValue, logid);
            } catch (Exception exception) {
                throw exception;
            }
        }

        private static IChart ChartAgent {
            get {
                return ((IChart)RemoteProxy.GetObject(typeof(IChart)));
            }
        }

        private static IItem ItemAgent {
            get {
                return ((IItem)RemoteProxy.GetObject(typeof(IItem)));
            }
        }
        private static IProject ProjAgent {
            get {
                return ((IProject)RemoteProxy.GetObject(typeof(IProject)));
            }
        }
    }
}

