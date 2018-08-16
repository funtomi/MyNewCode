namespace Thyt.TiPLM.CLT.TiModeler {
    using ClientUIFramework;
    using System;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.Admin.BPM;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.PLL.Project2;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility.User;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Common.Operation;
    using Thyt.TiPLM.UIL.Product;
    using Thyt.TiPLM.UIL.Product.Common;
    using Thyt.TiPLM.UIL.Project2;
    using Thyt.TiPLM.UIL.TiMessage;

    public class AppMain {
        private static void LogOff() {
            try {
                PLUser.Logoff(ClientData.LogonUser.Oid, PLMProductName.TiModeler.ToString(), ClientData.Session);
                UIMessage.Instance.UninitilizeMessage();
                BizOperationHelper.ClearLocalTempFiles();
            } catch {
            }
        }

        [STAThread]
        private static void Main(string[] args) {
            ConstEnvironment.CallInModel = true;
            Application.ThreadException += new ThreadExceptionEventHandler(ClientData.Application_ThreadException);
            if (!ClientData.OpenExistApp()) {
                Application.EnableVisualStyles();
                if (args.Length > 0) {
                    string param = args[0];
                    ClientData.UseStartParameter(param);
                }
                RemoteProxy.SetCallExcuteName();
                FrmVerify.GetFingerCltDefalut = (GetFingerCltDefalutEventHandler)Delegate.Combine(FrmVerify.GetFingerCltDefalut, new GetFingerCltDefalutEventHandler(UIFinger.GetFingerCltDefalut));
                PLPassWordChecker.ResetPassWord = (ResetPassWord)Delegate.Combine(PLPassWordChecker.ResetPassWord, new ResetPassWord(FrmSetPsnPwd.ResetUserPassWord));
                ClientData.AutoCopyLiveUpdateFiles();
                if (FrmLogon.Logon(PLMProductName.TiModeler.ToString())) {
                    try {
                        SplashHelper.Instance.ShowSplashForm(ConstCommon.FUNCTION_EDMS ? "TiModeler.edm" : "TiModeler.plm");
                        PSInit.InitPS(ClientData.LogonUser, true);
                        Thyt.TiPLM.CLT.TiModeler.FrmMain frmMain = new Thyt.TiPLM.CLT.TiModeler.FrmMain();
                        ClientData.mainForm = frmMain;
                        BPMEventInit.InitBPMEvent();
                        UIMessage.Instance.InitilizeMessage(frmMain);
                        MenuBuilder.Instance.Init();
                        PLProject.Instance.ProjDelegateInstance = (ProjDelegate)Delegate.Combine(PLProject.Instance.ProjDelegateInstance, new ProjDelegate(UIProject.ProjDelegateEvent));
                        SplashHelper.Instance.CloseSplashForm();
                        Application.Run(frmMain);
                        frmMain.Dispose();
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "TiModeler", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    } finally {
                        LogOff();
                    }
                }
            }
        }
    }
}

