namespace Thyt.TiPLM.PLL.Common
{
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Remoting;
    using System.Runtime.Remoting.Channels;
    using System.Runtime.Remoting.Channels.Tcp;
    using System.Runtime.Remoting.Messaging;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.PLL.Environment;

    public sealed class RemoteProxy
    {
        private const string CONFIG_FILENAME = "RemoteConfig.xml";
        private const string CONFIG_INTERFACE = "Interface";
        private const string CONFIG_NAME = "Name";
        private const string CONFIG_POSTFIX = "Postfix";
        private const string CONFIG_PREFIX = "Prefix";
        private const string CONFIG_REMOTING_OBJECT = "RemotingObject";
        public static string ConfigFilePath = null;
        private static bool isfirst = true;
        public static int Port = 0x22c4;
        public static string Protocal = "tcp";
        public static string Server = "localhost";
        private static TcpChannel tc = null;
        private static Hashtable UrlTable = null;

        private static ClientIP GetExcuteNameCode()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string fileName = "";
            if (entryAssembly != null)
            {
                fileName = Path.GetFileName(entryAssembly.Location);
            }
            else
            {
                Process currentProcess = Process.GetCurrentProcess();
                if ((currentProcess != null) && (currentProcess.MainModule != null))
                {
                    fileName = Path.GetFileName(currentProcess.MainModule.FileName);
                }
            }
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(fileName.ToUpper()));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.AppendFormat("{0:x2}", buffer[i]);
            }
            return new ClientIP(builder.ToString());
        }

        public static object GetObject(Type objType)
        {
            string uRL = GetURL(objType.FullName);
            if (uRL == null)
            {
                throw new PLMException("获得远程对象" + objType + "路径失败！请检查远程对象配置文件！");
            }
            object obj2 = null;
            try
            {
                obj2 = Activator.GetObject(objType, uRL);
            }
            catch (Exception exception)
            {
                PLMEventLog.WriteExceptionLog(exception);
                throw new PLMException("应用服务器没有响应！", exception);
            }
            return obj2;
        }

        public static object GetObject(Type objType, string id)
        {
            string url = string.Format(Protocal + "://{0}:{1}/{2}", Server, Port, id);
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

        public static object GetObject(Type objType, string server, int port, string url)
        {
            string str = string.Format(Protocal + "://{0}:{1}/{2}", server, port, url);
            object obj2 = null;
            try
            {
                obj2 = Activator.GetObject(objType, str);
            }
            catch (Exception exception)
            {
                PLMEventLog.WriteExceptionLog(exception);
                throw new PLMException("应用服务器没有响应！", exception);
            }
            return obj2;
        }

        private static string GetURL(string objType)
        {
            lock (typeof(Hashtable))
            {
                if (UrlTable == null)
                {
                    RetriveConfigInfo();
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
                            ProcessXml(reader);
                        }
                    }
                    input.Close();
                    string path = Path.Combine(Path.GetDirectoryName(ConstConfig.GetConfigFilePath("RemoteConfig.xml")), "RemoteConfig.config");
                    try
                    {
                        if (isfirst && File.Exists(path))
                        {
                            RemotingConfiguration.Configure(path, false);
                            isfirst = false;
                        }
                    }
                    catch (Exception exception)
                    {
                        EventLog.WriteEntry("TiPLM", exception.Message);
                    }
                }
                if (UrlTable[objType] != null)
                {
                    return UrlTable[objType].ToString();
                }
                return null;
            }
        }

        private static void ProcessXml(XmlTextReader reader)
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
                str3 = Protocal + "://" + Server + ":" + Port.ToString() + "/";
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
            if (UrlTable != null)
            {
                UrlTable.Clear();
                UrlTable = null;
            }
            RetriveConfigInfo();
            PLSystemParam.ClearParametersCache();
        }

        public static void RetriveConfigInfo()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common", true);
                if (key != null)
                {
                    object obj2 = key.GetValue("Server");
                    if (obj2 != null)
                    {
                        Server = Convert.ToString(obj2);
                    }
                    obj2 = key.GetValue("Port");
                    if (obj2 != null)
                    {
                        Port = Convert.ToInt32((string) obj2);
                    }
                    obj2 = key.GetValue("Protocal");
                    if (obj2 != null)
                    {
                        Protocal = Convert.ToString(obj2);
                    }
                    key.Close();
                }
                else
                {
                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common");
                }
            }
            catch (Exception exception)
            {
                Protocal = "tcp";
                Port = 0x22c4;
                PLMEventLog.WriteExceptionLog(exception);
            }
        }

        public static void RetriveConfigInfo4Service()
        {
            try
            {
                RegistryKey key = Registry.Users.OpenSubKey(@".DEFAULT\SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common", true);
                if (key != null)
                {
                    object obj2 = key.GetValue("Server");
                    if (obj2 != null)
                    {
                        Server = Convert.ToString(obj2);
                    }
                    obj2 = key.GetValue("Port");
                    if (obj2 != null)
                    {
                        Port = Convert.ToInt32((string) obj2);
                    }
                    obj2 = key.GetValue("Protocal");
                    if (obj2 != null)
                    {
                        Protocal = Convert.ToString(obj2);
                    }
                    key.Close();
                }
                else
                {
                    Registry.Users.CreateSubKey(@".DEFAULT\SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common");
                }
            }
            catch (Exception exception)
            {
                Protocal = "tcp";
                Port = 0x22c4;
                PLMEventLog.WriteExceptionLog(exception);
            }
        }

        public static void SetCallExcuteName()
        {
            CallContext.SetData("clientname", GetExcuteNameCode());
        }

        public static void SetConfigInfo(string cfgName, string cfgValue)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common", true);
                if (key == null)
                {
                    key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\北京清软英泰信息技术有限公司\TiPLM\Common");
                }
                key.SetValue(cfgName, cfgValue);
                key.Close();
            }
            catch (Exception exception)
            {
                PLMEventLog.WriteExceptionLog(exception);
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

