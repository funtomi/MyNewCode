using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
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
