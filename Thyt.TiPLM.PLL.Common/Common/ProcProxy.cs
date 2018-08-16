namespace Thyt.TiPLM.PLL.Common
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Tcp;
    using System.Xml;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.Common.Interface.ProcProxy;
    using Thyt.TiPLM.DEL.Environment;

    public sealed class ProcProxy
    {
        public const int BIZPORT = 0;
        private const string CONFIG_FILENAME = "RemoteConfig.xml";
        private const string CONFIG_INTERFACE = "Interface";
        private const string CONFIG_NAME = "Name";
        private const string CONFIG_POSTFIX = "Postfix";
        private const string CONFIG_PREFIX = "Prefix";
        private const string CONFIG_REMOTING_OBJECT = "RemotingObject";
        public static string ConfigFilePath = null;
        public static string LocalProxyIP = null;
        public static int[] LocalProxyPorts = new int[] { 0x2326, 0x2328 };
        public const int MONPORT = 1;
        public static int Port = 0x2326;
        public static string Protocal = "tcp";
        public static object pv = new Guid();
        public static string Server = "localhost";
        private static TcpChannel tc = null;
        private static Hashtable UrlTable = new Hashtable();

        public static object GetObject(Type objType)
        {
            if (LocalProxyIP == null)
            {
                RetriveConfigInfo();
            }
            return GetObject(objType, LocalProxyIP, LocalProxyPorts[0]);
        }

        public static object GetObject(Type objType, int PPort)
        {
            if (LocalProxyIP == null)
            {
                RetriveConfigInfo();
            }
            return GetObject(objType, LocalProxyIP, PPort);
        }

        public static object GetObject(Type objType, string id)
        {
            SetLocalProxy();
            string url = string.Format(Protocal + "://{0}:{1}/{2}", Server, Port, id);
            object obj2 = null;
            try
            {
                obj2 = Activator.GetObject(objType, url);
            }
            catch (Exception exception)
            {
                PLMEventLog.WriteExceptionLog(exception);
                throw new PLMException("代理服务器没有响应！", exception);
            }
            return obj2;
        }

        public static object GetObject(Type objType, string SServer, int PPort)
        {
            lock (pv)
            {
                SetProxy(SServer, PPort);
                string url = GetURL(objType.FullName, SServer, PPort);
                if (url == null)
                {
                    throw new PLMException("获得远程对象" + objType + "路径失败！请检查远程对象配置文件！");
                }
                object obj2 = null;
                try
                {
                    obj2 = Activator.GetObject(objType, url);
                }
                catch (Exception exception)
                {
                    PLMEventLog.WriteExceptionLog(exception);
                    throw new PLMException("应用服务器没有响应！", exception);
                }
                return obj2;
            }
        }

        private static string GetURL(string objType)
        {
            if (LocalProxyIP == null)
            {
                RetriveConfigInfo();
            }
            return GetURL(objType, LocalProxyIP, LocalProxyPorts[0]);
        }

        private static string GetURL(string objType, int PPort)
        {
            if (LocalProxyIP == null)
            {
                RetriveConfigInfo();
            }
            return GetURL(objType, LocalProxyIP, PPort);
        }

        private static string GetURL(string objType, string SServer, int PPort)
        {
            lock (pv)
            {
                SetProxy(SServer, PPort);
                if (UrlTable[objType] != null)
                {
                    return (Protocal + "://" + SServer + ":" + PPort.ToString() + "/" + UrlTable[objType].ToString());
                }
                return null;
            }
        }

        private static void LoadUrlsFromXML()
        {
            UrlTable = new Hashtable();
            if (ConfigFilePath == null)
            {
                ConfigFilePath = ConstConfig.GetConfigFilePath("RemoteConfig.xml");
            }
            if (!File.Exists(ConfigFilePath))
            {
                throw new PLMException("客户端配置文件" + ConfigFilePath + "没有找到！无法与应用服务器通信！");
            }
            StreamReader input = new StreamReader(ConfigFilePath);
            XmlTextReader reader = new XmlTextReader(input) {
                WhitespaceHandling = WhitespaceHandling.None
            };
            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "RemotingObject"))
                {
                    ProcessXml(reader, Server, Port);
                }
            }
            input.Close();
        }

        public static PPResponse ProcessRequest(ref PPRequest req)
        {
            IBizService service = (IBizService) GetObject(typeof(IBizService));
            return service.Process(ref req);
        }

        private static void ProcessXml(XmlTextReader reader)
        {
            ProcessXml(reader, Server, Port);
        }

        private static void ProcessXml(XmlTextReader reader, string SServer, int PPort)
        {
            string str = "";
            string key = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            while (((reader.NodeType != XmlNodeType.EndElement) || (reader.Name != "RemotingObject")) && reader.Read())
            {
                if (reader.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                string name = reader.Name;
                if (name != null)
                {
                    if (name != "Interface")
                    {
                        if (name == "Name")
                        {
                            goto Label_0079;
                        }
                        if (name == "Postfix")
                        {
                            goto Label_008E;
                        }
                    }
                    else
                    {
                        reader.Read();
                        key = reader.Value.Trim();
                    }
                }
                goto Label_00A2;
            Label_0079:
                reader.Read();
                str = reader.Value.Trim();
                goto Label_00A2;
            Label_008E:
                reader.Read();
                str5 = reader.Value.Trim();
            Label_00A2:
                reader.Read();
            }
            if (UrlTable[str] == null)
            {
                if ((str4 != null) && (str4 != ""))
                {
                    str3 = str3 + str4 + "/";
                }
                str3 = str3 + str.Replace(".", "/");
                if ((str5 != null) && (str5 != ""))
                {
                    str3 = str3 + "." + str5;
                }
                UrlTable.Add(key, str3);
            }
        }

        public static void RegisterChannel()
        {
            if (tc == null)
            {
                tc = new TcpChannel();
                ChannelServices.RegisterChannel(tc);
            }
        }

        public static void ResetRemoteConfigTable()
        {
            lock (pv)
            {
                if (UrlTable != null)
                {
                    UrlTable.Clear();
                }
                RetriveConfigInfo();
                Server = LocalProxyIP;
                Port = LocalProxyPorts[0];
                LoadUrlsFromXML();
            }
        }

        public static void RetriveConfigInfo()
        {
            RetriveConfigInfo(@"SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common\ProcProxy");
        }

        public static void RetriveConfigInfo(string regpath)
        {
            try
            {
                DEProxyServer internalProxyServer = ((IProxyServer) RemoteProxy.GetObject(typeof(IProxyServer))).GetInternalProxyServer();
                LocalProxyIP = internalProxyServer.HostAddress;
                LocalProxyPorts = new int[] { internalProxyServer.BusinessPort, internalProxyServer.MonitorPort };
            }
            catch (Exception exception)
            {
                PLMEventLog.WriteExceptionLog(exception);
                LocalProxyIP = "localhost";
                LocalProxyPorts = new int[] { 0x2326, 0x2328 };
            }
        }

        private static void SetLocalProxy()
        {
            if (LocalProxyIP == null)
            {
                RetriveConfigInfo();
            }
            SetProxy(LocalProxyIP, LocalProxyPorts[0]);
        }

        private static void SetProxy(string sserver, int pport)
        {
            if ((!sserver.Equals(Server) || (Port != pport)) || ((UrlTable == null) || (UrlTable.Count < 1)))
            {
                if (LocalProxyIP == null)
                {
                    RetriveConfigInfo();
                }
                Server = sserver;
                Port = pport;
                if (UrlTable.Count < 1)
                {
                    LoadUrlsFromXML();
                }
            }
        }

        public static void UngegisterChannel()
        {
            if (tc != null)
            {
                ChannelServices.UnregisterChannel(tc);
            }
        }
    }
}

