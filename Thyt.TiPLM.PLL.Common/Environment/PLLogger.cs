namespace Thyt.TiPLM.PLL.Environment
{
    using System;
    using System.Collections;
    using System.Net;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Common;

    public class PLLogger
    {
        private ILogger log = ((ILogger) RemoteProxy.GetObject(typeof(ILogger)));

        public ArrayList ClearLog(string logKind, int day, bool isSave) {
         return   
            this.log.ClearLog(logKind, day, isSave);}

        public void CreateBoAccessObj(DEProductConfigLog productObj)
        {
            this.log.CreateBoAccessObj(productObj);
        }

        public void CreateSecurityObj(DESecurityLog security0bj)
        {
            this.log.CreateSecurityObj(security0bj);
        }

        public void CreateServerObj(DEServerLog serverObj)
        {
            this.log.CreateServerObj(serverObj);
        }

        public void CreateSystemObj(DESystemLog systemObj)
        {
            this.log.CreateSystemObj(systemObj);
        }

        public object GetCondition(string logType, string sqlornot) {
         return               this.log.GetCondition(logType, sqlornot);
        }

        public ArrayList GetLogObjs(ArrayList objCon, string logKind)
        {
            ArrayList list = new ArrayList();
            return this.log.GetLogObjs(objCon, logKind);
        }

        public ArrayList GetLogObjs(string logKind, int num)
        {
            ArrayList list = new ArrayList();
            return this.log.GetLogObjs(logKind, num);
        }

        public DELogProperty GetLogPropertyObjs(string logkind) {
            return
               this.log.GetLogPropertyObjs(logkind);
        }

        public ArrayList GetProductDataLogByItem(DEBusinessItem item, string classlabel, int num)
        {
            ArrayList list = new ArrayList();
            return this.log.GetProductDataLogByItem(item, classlabel, num);
        }

        public void InsertLogPropertyObj(DELogProperty logProperty)
        {
            this.log.InsertLogPropertyObj(logProperty);
        }

        public void ModifyLogPropertyObj(DELogProperty logProperty)
        {
            this.log.ModifyLogPropertyObj(logProperty);
        }

        public void SaveCondition(string logKind, ArrayList objCon, string xmlCon)
        {
            this.log.SaveCondition(logKind, objCon, xmlCon);
        }

        public static void WriteFileServerErrorLog(string ip, string target, string description, ConstEnvironment.ServerOperType operType)
        {
            DEServerLog serverObj = new DEServerLog {
                LogType = ConstEnvironment.GetServerLogTypeName(ConstEnvironment.ServerLogType.Error),
                Operation = ConstEnvironment.GetServerOperTypeName(operType),
                Oid = Guid.NewGuid(),
                ServerIp = ip,
                ServerName = Dns.GetHostName(),
                SourceObject = ConstEnvironment.GetServerObjTypeName(ConstEnvironment.ServerObjType.FileServer),
                TargetObject = target,
                Description = description
            };
            new PLLogger().CreateServerObj(serverObj);
        }

        public static void WriteFileServerInfoLog(string ip, string target, string description, ConstEnvironment.ServerOperType operType, string servername)
        {
            DEServerLog serverObj = new DEServerLog {
                LogType = ConstEnvironment.GetServerLogTypeName(ConstEnvironment.ServerLogType.Information),
                Operation = ConstEnvironment.GetServerOperTypeName(operType),
                Oid = Guid.NewGuid(),
                ServerIp = ip,
                ServerName = servername,
                SourceObject = ConstEnvironment.GetServerObjTypeName(ConstEnvironment.ServerObjType.FileServer),
                Description = description,
                TargetObject = target
            };
            new PLLogger().CreateServerObj(serverObj);
        }

        public static void WriteFileServerMonitorErrorLog(string ip, string target, string description, ConstEnvironment.ServerOperType operType)
        {
            DEServerLog serverObj = new DEServerLog {
                LogType = ConstEnvironment.GetServerLogTypeName(ConstEnvironment.ServerLogType.Error),
                Operation = ConstEnvironment.GetServerOperTypeName(operType),
                Oid = Guid.NewGuid(),
                ServerIp = ip,
                ServerName = Dns.GetHostName(),
                SourceObject = ConstEnvironment.GetServerObjTypeName(ConstEnvironment.ServerObjType.FileListener),
                TargetObject = target,
                Description = description
            };
            new PLLogger().CreateServerObj(serverObj);
        }

        public static void WriteFileServerMonitorInfoLog(string ip, string target, string description, ConstEnvironment.ServerOperType operType)
        {
            DEServerLog serverObj = new DEServerLog {
                LogType = ConstEnvironment.GetServerLogTypeName(ConstEnvironment.ServerLogType.Information),
                Operation = ConstEnvironment.GetServerOperTypeName(operType),
                Oid = Guid.NewGuid(),
                ServerIp = ip,
                ServerName = Dns.GetHostName(),
                SourceObject = ConstEnvironment.GetServerObjTypeName(ConstEnvironment.ServerObjType.FileListener),
                Description = description,
                TargetObject = target
            };
            new PLLogger().CreateServerObj(serverObj);
        }
    }
}

