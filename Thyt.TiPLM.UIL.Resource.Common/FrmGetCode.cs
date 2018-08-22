    using AxtiEcmsControll;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Net;
    using System.Resources;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common {
    public partial class FrmGetCode : FormPLM
    {
        public string ClassName;
        public string cmd;
        public string CodeId;
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
            try
            {
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
                        if ((this.Id != "") && (strRuleName.Length > 0))
                        {
                            this.ecms.Ecms_RecycleCode(ref this.Id, ref strRuleName, ref this.cmd);
                        }
                    }
                    else
                    {
                        string strInitCode = "@@@@";
                        if ((this.Id != "") && (this.Id.IndexOf("@@") == -1))
                        {
                            strInitCode = "@@" + this.Id + "@@";
                            this.ReturnProps = this.ecms.Ecms_RuleGetNextCode(ref this.cmd, ref strInitCode, ref this.isNextSeg);
                            if (((this.ReturnProps != null) && (this.ReturnProps != "")) && (this.ReturnProps.IndexOf("@@") == -1))
                            {
                                MessageBoxPLM.Show(this.ReturnProps);
                                this.Id = "";
                                this.ReturnProps = "";
                            }
                            else
                            {
                                this.Id = this.ecms.RetCode;
                                if (!ConstCommon.FUNCTION_ECMS_ISPDM)
                                {
                                    this.CodeId = this.ecms.RetCodeId;
                                }
                            }
                        }
                        else
                        {
                            if (this.Id != "")
                            {
                                strInitCode = this.Id;
                            }
                            this.ReturnProps = this.ecms.Ecms_GetCode(ref this.cmd, ref strInitCode);
                            if (((this.ReturnProps != null) && (this.ReturnProps != "")) && (this.ReturnProps.IndexOf("@@") == -1))
                            {
                                MessageBoxPLM.Show(this.ReturnProps);
                                this.Id = "";
                                this.ReturnProps = "";
                            }
                            else
                            {
                                this.Id = this.ecms.RetCode;
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
    }
}

