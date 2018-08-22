    using AxtiEcmsControll;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Management;
    using System.Net;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class FrmGetCode : FormPLM
    {
        public string ClassName;
        public string cmd;
        public string CodeId;
        private AxecmsControll ecms;
        public string Id;
        private bool isNextSeg;
        private bool isReBack;
        public BusinessItemType ItemType;
        public string ReturnProps;

        public FrmGetCode()
        {
            this.Id = "";
            this.ClassName = "";
            this.ReturnProps = "";
            this.CodeId = "";
            this.cmd = "";
            this.isNextSeg = true;
            this.InitializeComponent();
        }

        public FrmGetCode(string id, string ClassName, BusinessItemType ItemType, bool isNextSeg)
        {
            this.Id = "";
            this.ClassName = "";
            this.ReturnProps = "";
            this.CodeId = "";
            this.cmd = "";
            this.isNextSeg = true;
            this.InitializeComponent();
            this.Id = id;
            this.ClassName = ClassName;
            this.ItemType = ItemType;
            this.isNextSeg = isNextSeg;
        }

        public FrmGetCode(string id, string ClassName, BusinessItemType ItemType, object isReBack)
        {
            this.Id = "";
            this.ClassName = "";
            this.ReturnProps = "";
            this.CodeId = "";
            this.cmd = "";
            this.isNextSeg = true;
            this.InitializeComponent();
            this.Id = id;
            this.ClassName = ClassName;
            this.ItemType = ItemType;
            this.isNextSeg = false;
            this.isReBack = Convert.ToBoolean(isReBack);
        }

        public FrmGetCode(string id, string ClassName, string cmd, BusinessItemType ItemType, bool isNextSeg)
        {
            this.Id = "";
            this.ClassName = "";
            this.ReturnProps = "";
            this.CodeId = "";
            this.cmd = "";
            this.isNextSeg = true;
            this.InitializeComponent();
            this.Id = id;
            this.cmd = cmd;
            this.ClassName = ClassName;
            this.ItemType = ItemType;
            this.isNextSeg = isNextSeg;
        }

        public FrmGetCode(string id, string ClassName, string cmd, BusinessItemType ItemType, object isReBack)
        {
            this.Id = "";
            this.ClassName = "";
            this.ReturnProps = "";
            this.CodeId = "";
            this.cmd = "";
            this.isNextSeg = true;
            this.InitializeComponent();
            this.Id = id;
            this.cmd = cmd;
            this.ClassName = ClassName;
            this.ItemType = ItemType;
            this.isNextSeg = false;
            this.isReBack = Convert.ToBoolean(isReBack);
        }

        private void FrmGetCode_Load(object sender, EventArgs e)
        {
            if (this.IsWin32EXE())
            {
                this.Win32FormLoad(ref this.Id);
            }
            else
            {
                this.Win64FormLoad(ref this.Id);
            }
            if (this.Id == "")
            {
                base.DialogResult = DialogResult.No;
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
            base.Close();
        }

        public static void GenerateCode(string oldId, string ClassName, BusinessItemType ItemType, bool isNextSeg, out string Id, out string codeId)
        {
            FrmGetCode code = new FrmGetCode(oldId, ClassName, ItemType, isNextSeg);
            Id = "";
            codeId = "";
            if (code.ShowDialog() == DialogResult.OK)
            {
                Id = code.Id;
                codeId = code.CodeId;
            }
        }

        public static void GenerateCode(string oldId, string ClassName, string cmd, BusinessItemType ItemType, bool isNextSeg, out string Id, out string codeId)
        {
            FrmGetCode code = new FrmGetCode(oldId, ClassName, cmd, ItemType, isNextSeg);
            Id = "";
            codeId = "";
            if (code.ShowDialog() == DialogResult.OK)
            {
                Id = code.Id;
                codeId = code.CodeId;
            }
        }

        public string GetUserHost()
        {
            try
            {
                string str = "";
                IPHostEntry hostByName = Dns.GetHostByName(Dns.GetHostName());
                IPAddress[] addressList = hostByName.AddressList;
                if (((hostByName != null) && (addressList != null)) && (addressList.Length > 0))
                {
                    str = addressList[0].ToString();
                }
                return str;
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
            return "";
        }

        private void InitEcmsCtrl()
        {
            ResourceManager manager = new ResourceManager(typeof(FrmGetCode));
            this.ecms = new AxecmsControll();
            this.ecms.BeginInit();
            this.ecms.Enabled = true;
            this.ecms.Location = new Point(0x20, 0x18);
            this.ecms.Name = "ecms";
            this.ecms.OcxState = (AxHost.State) manager.GetObject("ecms.OcxState");
            this.ecms.Size = new Size(320, 240);
            this.ecms.TabIndex = 0;
            base.Controls.Add(this.ecms);
            this.ecms.EndInit();
        }


        public static bool Is64System()
        {
            try
            {
                string str = string.Empty;
                ConnectionOptions options = new ConnectionOptions();
                ManagementScope scope = new ManagementScope(@"\\localhost", options);
                ObjectQuery query = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    str = obj2["AddressWidth"].ToString();
                }
                return (str == "64");
            }
            catch (Exception exception)
            {
                try
                {
                    EventLog.WriteEntry("TiPLM", exception.ToString());
                }
                catch
                {
                }
                return false;
            }
        }
        private bool IsWin32EXE() {
            bool flag;
            if (!Is64System() || (((System.Environment.OSVersion.Version.Major != 5) || (System.Environment.OSVersion.Version.Minor < 1)) && (System.Environment.OSVersion.Version.Major <= 5))) {
                return true;
            }
            if (!IsWow64Process(Process.GetCurrentProcess().Handle, out flag)) {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            return flag;
        }


        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool IsWow64Process([In] IntPtr processHandle, [MarshalAs(UnmanagedType.Bool)] out bool wow64Process);
        public static bool RecycleCode(string oldId, string ClassName, BusinessItemType ItemType)
        {
            FrmGetCode code = new FrmGetCode(oldId, ClassName, ItemType, true);
            return (code.ShowDialog() == DialogResult.OK);
        }

        public static bool RecycleCode(string oldId, string ClassName, string cmd, BusinessItemType ItemType)
        {
            FrmGetCode code = new FrmGetCode(oldId, ClassName, cmd, ItemType, true);
            return (code.ShowDialog() == DialogResult.OK);
        }

        public static bool SearchesCode()
        {
            FrmGetCode code = new FrmGetCode("", "", BusinessItemType.Unknown, false) {
                cmd = "CX"
            };
            return (code.ShowDialog() == DialogResult.OK);
        }

        private void Win32FormLoad(ref string Id)
        {
            try
            {
                this.InitEcmsCtrl();
                string strMain = "TIPRODUCT";
                string logId = ClientData.LogonUser.LogId;
                string server = RemoteProxy.Server;
                this.ecms.LoginClear(ref strMain);
                if (!ConstCommon.FUNCTION_ECMS_ISPDM)
                {
                    this.ecms.Ecms_SetConnUser(ref strMain, ref logId, ref server);
                }
                else
                {
                    this.ecms.Ecms_SetConn(ref strMain);
                }
                if (!this.ecms.Ecms_isConnUsedable())
                {
                    this.ecms.Ecms_SetConn(ref strMain);
                }
                if (!this.ecms.Ecms_isConnUsedable())
                {
                    MessageBoxPLM.Show("无法连接编码服务器！", "生成编码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (this.cmd == "")
                    {
                        if (ModelContext.MetaModel.IsPart(this.ClassName))
                        {
                            this.cmd = "BM";
                        }
                        else if (ModelContext.MetaModel.IsProduct(this.ClassName))
                        {
                            this.cmd = "CP";
                        }
                        else
                        {
                            this.cmd = "WJ";
                        }
                    }
                    if (this.isReBack)
                    {
                        string strRuleName = this.ecms.RuleName();
                        if ((Id != "") && (strRuleName.Length > 0))
                        {
                            this.ecms.Ecms_RecycleCode(ref Id, ref strRuleName, ref this.cmd);
                        }
                    }
                    else
                    {
                        string strInitCode = "@@@@";
                        if ((Id != "") && (Id.IndexOf("@@") == -1))
                        {
                            strInitCode = "@@" + Id + "@@";
                            this.ReturnProps = this.ecms.Ecms_RuleGetNextCode(ref this.cmd, ref strInitCode, ref this.isNextSeg);
                            if (((this.ReturnProps != null) && (this.ReturnProps != "")) && (this.ReturnProps.IndexOf("@@") == -1))
                            {
                                MessageBoxPLM.Show(this.ReturnProps);
                                Id = "";
                                this.ReturnProps = "";
                            }
                            else
                            {
                                Id = this.ecms.RetCode;
                                if (!ConstCommon.FUNCTION_ECMS_ISPDM)
                                {
                                    this.CodeId = this.ecms.RetCodeId;
                                }
                            }
                        }
                        else
                        {
                            if (Id != "")
                            {
                                strInitCode = Id;
                            }
                            this.ReturnProps = this.ecms.Ecms_GetCode(ref this.cmd, ref strInitCode);
                            if (((this.ReturnProps != null) && (this.ReturnProps != "")) && (this.ReturnProps.IndexOf("@@") == -1))
                            {
                                MessageBoxPLM.Show(this.ReturnProps);
                                Id = "";
                                this.ReturnProps = "";
                            }
                            else
                            {
                                Id = this.ecms.RetCode;
                                if (!ConstCommon.FUNCTION_ECMS_ISPDM)
                                {
                                    this.CodeId = this.ecms.RetCodeId;
                                }
                            }
                        }
                    }
                    this.ecms.Ecms_CloseCnn();
                    this.ecms.Ecms_LoginClear(ref strMain);
                }
            }
            catch (Exception exception)
            {
                MessageBoxPLM.Show(exception.Message);
            }
            finally
            {
                this.ecms.Dispose();
            }
        }

        private void Win64FormLoad(ref string Id)
        {
            try
            {
                string str = "TIPRODUCT";
                string logId = ClientData.LogonUser.LogId;
                string server = RemoteProxy.Server;
                System.Type typeFromProgID = System.Type.GetTypeFromProgID("CatchComServer.TiEcmsControllCom");
                object target = Activator.CreateInstance(typeFromProgID);
                typeFromProgID.InvokeMember("Ecms_LoginClear", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, new object[] { str, true });
                bool flag = false;
                object[] args = new object[] { str, logId, server, flag };
                ParameterModifier[] modifiers = new ParameterModifier[] { new ParameterModifier(4) };
                modifiers[0][1] = false;
                modifiers[0][2] = false;
                modifiers[0][3] = true;
                typeFromProgID.InvokeMember("Ecms_SetConnUser", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, args, modifiers, null, null);
                if (args[3].Equals(false))
                {
                    typeFromProgID.InvokeMember("Ecms_SetConn", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, new object[] { str, true });
                    args = new object[] { flag };
                    modifiers = new ParameterModifier[] { new ParameterModifier(1) };
                    modifiers[0][0] = true;
                    typeFromProgID.InvokeMember("Ecms_isConnUsedable", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, args, modifiers, null, null);
                    if (args[0].Equals(false))
                    {
                        this.ReturnProps = "";
                        Id = "";
                        MessageBoxPLM.Show("无法连接编码服务器！", "生成编码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (this.cmd == "")
                {
                    if (ModelContext.MetaModel.IsPart(this.ClassName))
                    {
                        this.cmd = "BM";
                    }
                    else if (ModelContext.MetaModel.IsProduct(this.ClassName))
                    {
                        this.cmd = "CP";
                    }
                    else
                    {
                        this.cmd = "WJ";
                    }
                }
                bool flag1 = Id != "";
                args = new object[] { "BM", "@@@@", "" };
                modifiers = new ParameterModifier[] { new ParameterModifier(3) };
                modifiers[0][1] = false;
                modifiers[0][2] = true;
                typeFromProgID.InvokeMember("Ecms_GetCode", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, args, modifiers, null, null);
                this.ReturnProps = (args[2] == null) ? "" : args[2].ToString();
                Id = this.ReturnProps;
                typeFromProgID.InvokeMember("Ecms_CloseCnn", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, new object[] { true });
                typeFromProgID.InvokeMember("Ecms_LoginClear", BindingFlags.InvokeMethod | BindingFlags.Public, null, target, new object[] { str, true });
            }
            catch (Exception exception)
            {
                MessageBoxPLM.Show(exception.Message);
            }
        }
    }
}

