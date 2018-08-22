    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.DEL.Foundation.FileService;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.FileService;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
    using Thyt.TiPLM.UIL.Environment;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class FrmMarkupByBrowser : FormPLM
    {
        private bool bCancel = true;
        public IInnerBrowser browser;
        
        public string ClassName;
        private DEMarkup curMarkup;
        public Guid dataOid = Guid.Empty;
        private PLMDelSameViewer DelSameViewer;
        public string FileName;
        public Guid FileOid = Guid.Empty;
        public FrmMarkupByBrowser frmMarkupByBrowser;
        private bool isTextChanged;
        public int ItemIteration = 1;
        public Guid ItemOid = Guid.Empty;
        private string markFileName;
        private string markLocation;
        public int Mode;
        
        public Guid processOid = Guid.Empty;
        private string tempPath;
        public Guid UserOid = Guid.Empty;
        public Guid workItemOid = Guid.Empty;

        public FrmMarkupByBrowser()
        {
            this.InitializeComponent();
            this.DelSameViewer = new PLMDelSameViewer(this.DelViewer);
            PLMEvent.Instance.D_DelSameViewer = (PLMDelSameViewer) Delegate.Combine(PLMEvent.Instance.D_DelSameViewer, this.DelSameViewer);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.bCancel = true;
            base.Close();
        }

        private void btnMarkup_Click(object sender, EventArgs e)
        {
            this.ShowMarkup();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Save();
            if (base.DialogResult == DialogResult.OK)
            {
                base.Close();
            }
        }

        private void DelViewer(string type)
        {
            for (int i = 0; i < this.pnlBrowser.Controls.Count; i++)
            {
                Control control = this.pnlBrowser.Controls[i];
                if (control.GetType().ToString() == type)
                {
                    this.pnlBrowser.Controls.RemoveAt(i);
                    i--;
                    if (MessageBoxPLM.Show("本批注窗口即将关闭，是否保存批注内容？", "批注保存", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        this.Save();
                    }
                    base.DialogResult = DialogResult.OK;
                    base.Close();
                    return;
                }
            }
        }

    

        public static void EditMarkUp(string fileName, Guid fileOid, Guid processOid, Guid workItemOid, Guid dataOid, Guid userOid, string className, IInnerBrowser browser, Guid itemOid, int itemIteration)
        {
            FrmMarkupByBrowser browser2 = new FrmMarkupByBrowser {
                Mode = 2,
                FileOid = fileOid,
                processOid = processOid,
                workItemOid = workItemOid,
                dataOid = dataOid,
                UserOid = userOid,
                ClassName = className,
                FileName = fileName,
                browser = browser,
                ItemOid = itemOid,
                ItemIteration = itemIteration
            };
            if (ClientData.OptRmarkEditStyle == 0)
            {
                browser2.MdiParent = ClientData.mainForm;
            }
            else
            {
                browser2.ShowInTaskbar = true;
                browser2.WindowState = FormWindowState.Maximized;
            }
            browser2.Show();
        }

        private void FrmMarkupByBrowser_Closing(object sender, CancelEventArgs e)
        {
            this.browser.ExitMarkupMode();
            ViewFileHelper.DeleteDirectory(this.markLocation);
            BrowserPool.BrowserManager.RemoveBrowser(this.pnlBrowser);
        }

        private void FrmMarkupByBrowser_Load(object sender, EventArgs e)
        {
            this.browser.AddControl(this.pnlBrowser);
            this.browser.SetSourceFile(this.FileName);
            this.markLocation = ViewFileHelper.Instance.GetTempMarkupPath();
            if (Directory.Exists(this.markLocation))
            {
                if ((Directory.GetFiles(this.markLocation).Length > 0) && (MessageBoxPLM.Show("系统可能正在批注其他的文件，继续操作将使原批注文件丢失，是否继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No))
                {
                    return;
                }
                ViewFileHelper.DeleteDirectory(this.markLocation);
                Thread.Sleep(100);
            }
            while (Directory.Exists(this.markLocation))
            {
            }
            Directory.CreateDirectory(this.markLocation);
            new DirectoryInfo(this.markLocation).Attributes = FileAttributes.Hidden;
            try
            {
                this.curMarkup = PLItem.Agent.GetMarkup(this.FileOid, this.workItemOid, this.dataOid, this.UserOid);
                if ((this.curMarkup != null) && (this.Mode == 0))
                {
                    this.Mode = 2;
                }
                if ((this.Mode == 0) && (this.curMarkup == null))
                {
                    this.curMarkup = new DEMarkup();
                    this.curMarkup.FileOid = this.FileOid;
                    this.curMarkup.ProcessOid = this.processOid;
                    this.curMarkup.WorkItemOid = this.workItemOid;
                    this.curMarkup.DataOid = this.dataOid;
                    this.curMarkup.UserOid = this.UserOid;
                    this.curMarkup.MarkupFileOid = Guid.Empty;
                }
                if (this.curMarkup.MarkupFileOid != Guid.Empty)
                {
                    ArrayList files = PLFileService.Agent.GetFiles(this.curMarkup.MarkupFileOid);
                    if ((files != null) && (files.Count > 0))
                    {
                        this.markFileName = this.markLocation + @"\" + ViewFileHelper.Instance.GetFileName(this.curMarkup.MarkupFileOid);
                        FSClientUtil.DownloadFile("ClaRel_BROWSE", this.curMarkup.MarkupFileOid, this.markFileName);
                    }
                }
                this.ShowMarkup();
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
        }


        public static void MarkUp(string fileName, Guid fileOid, Guid processOid, Guid workItemOid, Guid dataOid, Guid userOid, string className, IInnerBrowser browser, Guid itemOid, int itemIteration)
        {
            FrmMarkupByBrowser browser2 = new FrmMarkupByBrowser {
                Mode = 0,
                FileOid = fileOid,
                processOid = processOid,
                workItemOid = workItemOid,
                dataOid = dataOid,
                UserOid = userOid,
                ClassName = className,
                FileName = fileName,
                browser = browser,
                ItemOid = itemOid,
                ItemIteration = itemIteration
            };
            if (ClientData.OptRmarkEditStyle == 0)
            {
                browser2.MdiParent = ClientData.mainForm;
            }
            else
            {
                browser2.ShowInTaskbar = true;
                browser2.WindowState = FormWindowState.Maximized;
            }
            browser2.Show();
        }

        private bool NeedUploadMarkupFile(Guid srcFileOid, string markupFilePath)
        {
            ArrayList files = PLFileService.Agent.GetFiles(srcFileOid);
            if (files.Count > 0)
            {
                long originalSize = (files[0] as DEFile).OriginalSize;
                if (File.Exists(markupFilePath) && (new FileInfo(markupFilePath).Length != originalSize))
                {
                    return true;
                }
            }
            return false;
        }

        private void Save()
        {
            this.bCancel = false;
            this.browser.ExitMarkupMode();
            Thread.Sleep(100);
            string[] files = Directory.GetFiles(this.markLocation);
            if (((files.Length == 0) && this.browser.CanMarkup()) && (this.Mode != 1))
            {
                MessageBoxPLM.Show("无法获取批注文件，请确认批注文件已保存！");
                base.DialogResult = DialogResult.None;
                bool allowMarkup = true;
                try
                {
                    if (this.markFileName == null)
                    {
                        this.browser.EnterMarkupMode(this.FileName, this.markLocation, Path.GetFileName(this.FileName) + ".plmmkp", allowMarkup);
                    }
                    else
                    {
                        this.browser.EnterMarkupMode(this.FileName, this.markLocation, Path.GetFileName(this.markFileName), allowMarkup);
                    }
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
                return;
            }
            if (this.Mode == 0)
            {
                if (!this.browser.CanMarkup() && (this.txtMark.Text.Trim() == ""))
                {
                    goto Label_0340;
                }
                DEMarkup mark = new DEMarkup {
                    UserOid = this.UserOid,
                    ProcessOid = this.processOid,
                    WorkItemOid = this.workItemOid,
                    DataOid = this.dataOid,
                    Markup = this.txtMark.Text.Trim(),
                    FileOid = this.FileOid,
                    MarkupFileOid = Guid.NewGuid(),
                    ItemOid = this.ItemOid,
                    ItemIteration = this.ItemIteration,
                    ClassName = this.ClassName
                };
                bool flag2 = false;
                if (this.browser.CanMarkup())
                {
                    try
                    {
                        if (this.NeedUploadMarkupFile(this.FileOid, files[0]))
                        {
                            FSClientUtil.UploadFile(mark.MarkupFileOid, files[0], this.ClassName, 'I', Guid.Empty, ClientData.LogonUser.Name);
                            flag2 = true;
                        }
                    }
                    catch (Exception exception2)
                    {
                        PrintException.Print(exception2);
                        return;
                    }
                }
                if (!flag2 && (this.txtMark.Text.Trim() == ""))
                {
                    goto Label_0340;
                }
                try
                {
                    PLItem.Agent.CreateMarkup(mark, ClientData.LogonUser.Oid);
                    goto Label_0340;
                }
                catch (Exception exception3)
                {
                    PrintException.Print(exception3);
                    return;
                }
            }
            if (this.Mode == 2)
            {
                bool flag3 = false;
                if (this.curMarkup.Markup == null)
                {
                    this.curMarkup.Markup = "";
                }
                if (this.curMarkup.Markup != this.txtMark.Text.Trim())
                {
                    this.curMarkup.Markup = this.txtMark.Text.Trim();
                    flag3 = true;
                }
                this.curMarkup.ItemOid = this.ItemOid;
                this.curMarkup.ItemIteration = this.ItemIteration;
                bool flag4 = false;
                if (this.browser.CanMarkup())
                {
                    try
                    {
                        if (this.NeedUploadMarkupFile(this.curMarkup.FileOid, files[0]))
                        {
                            FSClientUtil.UploadFile(this.curMarkup.MarkupFileOid, files[0], this.ClassName, 'I', Guid.Empty, ClientData.LogonUser.Name);
                            flag4 = true;
                        }
                    }
                    catch (Exception exception4)
                    {
                        PrintException.Print(exception4);
                        return;
                    }
                }
                try
                {
                    if (flag3 || flag4)
                    {
                        PLItem.Agent.ModifyMarkup(this.curMarkup, ClientData.LogonUser.Oid);
                    }
                }
                catch (Exception exception5)
                {
                    PrintException.Print(exception5);
                    return;
                }
            }
        Label_0340:
            base.DialogResult = DialogResult.OK;
        }

        private void ShowMarkup()
        {
            if (this.curMarkup == null)
            {
                throw new Exception("错误，无法显示批注信息");
            }
            if (this.curMarkup.Markup != null)
            {
                this.txtMark.Text = this.curMarkup.Markup;
            }
            if (this.FileName != null)
            {
                try
                {
                    bool allowMarkup = true;
                    if (this.Mode == 1)
                    {
                        allowMarkup = false;
                    }
                    if (this.markFileName == null)
                    {
                        this.browser.EnterMarkupMode(this.FileName, this.markLocation, Path.GetFileName(this.FileName) + ".plmmkp", allowMarkup);
                    }
                    else
                    {
                        this.browser.EnterMarkupMode(this.FileName, this.markLocation, Path.GetFileName(this.markFileName), allowMarkup);
                    }
                }
                catch (Exception exception)
                {
                    MessageBoxPLM.Show(exception.Message);
                }
            }
        }

        private void txtMark_TextChanged(object sender, EventArgs e)
        {
            this.isTextChanged = true;
        }

        public static void ViewMarkUp(string fileName, Guid fileOid, Guid processOid, Guid workItemOid, Guid dataOid, Guid userOid, string className, bool isShowDialog, IInnerBrowser browser)
        {
            FrmMarkupByBrowser browser2 = new FrmMarkupByBrowser {
                Mode = 1,
                FileOid = fileOid,
                processOid = processOid,
                workItemOid = workItemOid,
                dataOid = dataOid,
                UserOid = userOid,
                ClassName = className,
                FileName = fileName,
                browser = browser
            };
            if (isShowDialog)
            {
                browser2.ShowDialog();
            }
            else
            {
                browser2.MdiParent = ClientData.mainForm;
                browser2.Show();
            }
        }
    }
}

