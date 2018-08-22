    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Addin;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.DEL.Foundation.FileService;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.FileService;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
    using Thyt.TiPLM.UIL.Environment;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class FrmViewFile : ManagedForm
    {
        private DEBusinessItem _bizItem;
        private string _className;
        private DELProcessInfoForCLT _processArgs;
        private IInnerBrowser browser;
        private SimpleButton btnAddSDRemark;
        
        private DESecureFile CurFile;
        private DEMarkup curMarkup;
        private static string CurViewFilePath = "";
        private PLMDelSameViewer DelSameViewer;
        
        public static bool IsEditGxt = false;
        public bool IsMarkUpMode;
        public static bool IsOnlyBrowser = false;
        private int LastSelectedIndex = -1;
        private string markFileName;
        private string markLocation;
        private int Mode;
        
        private ToolTip tip = new ToolTip();

        public FrmViewFile()
        {
            this.InitializeComponent();
            this.DelSameViewer = new PLMDelSameViewer(this.DelViewer);
            PLMEvent.Instance.D_DelSameViewer = (PLMDelSameViewer) Delegate.Combine(PLMEvent.Instance.D_DelSameViewer, this.DelSameViewer);
        }

        private void AddRemark()
        {
            if (this.lstSelfDefRemarks.SelectedIndices.Count == 1)
            {
                string str = this.lstSelfDefRemarks.SelectedItem.ToString();
                int length = 0x3e8 - this.txtRemark.Text.Length;
                int num2 = str.Length;
                int num3 = this.txtRemark.Text.Length;
                if (length >= num2)
                {
                    if (num3 == 0)
                    {
                        this.txtRemark.Text = this.txtRemark.Text + str;
                    }
                    else
                    {
                        this.txtRemark.Text = this.txtRemark.Text + "\r\n" + str;
                    }
                }
                else if (num3 == 0)
                {
                    this.txtRemark.Text = this.txtRemark.Text + str.Substring(0, length);
                }
                else
                {
                    this.txtRemark.Text = this.txtRemark.Text + "\r\n" + str.Substring(0, length);
                }
            }
        }

        private void btnAddSDRemark_Click(object sender, EventArgs e)
        {
            this.AddRemark();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.btnMarkup.Visible = true;
            this.btnMarkup.Enabled = true;
            this.panelButtons.Visible = false;
            this.splitContainerSmall.PanelVisibility = SplitPanelVisibility.Panel1;
            this.browser.ExitMarkupMode();
            this.IsMarkUpMode = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnMarkup_Click(object sender, EventArgs e)
        {
            try
            {
                DESecureFile selectedItem = this.combFiles.SelectedItem as DESecureFile;
                for (int i = 0; i < this.panelBrowser.Controls.Count; i++)
                {
                    Control control = this.panelBrowser.Controls[i];
                    try
                    {
                        this.browser = (IInnerBrowser) control;
                    }
                    catch
                    {
                        this.browser = null;
                    }
                    if (this.browser != null)
                    {
                        this.DoShowMarkUp(selectedItem);
                        this.IsMarkUpMode = true;
                        this.btnMarkup.Enabled = false;
                        goto Label_0085;
                    }
                }
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
        Label_0085:
            try
            {
                if (this.combFiles.SelectedIndex != -1)
                {
                    FileBrowseWay way;
                    DEBrowser browser = null;
                    DESecureFile info = this.combFiles.SelectedItem as DESecureFile;
                    try
                    {
                        way = UIBrowser.GetMarkupTool(info.FileOid, info.FileName, info.FileType, out browser);
                    }
                    catch (Exception exception2)
                    {
                        PrintException.Print(exception2, "业务对象管理");
                        return;
                    }
                    if (way == FileBrowseWay.InnerBrowser)
                    {
                        if (browser != null)
                        {
                            IInnerBrowser browser2 = null;
                            try
                            {
                                browser2 = BrowserPool.BrowserManager.GetBrowser(browser, null);
                            }
                            catch (Exception exception3)
                            {
                                MessageBoxPLM.Show(exception3.Message);
                                return;
                            }
                            if (browser2 != null)
                            {
                                this.DoShowMarkUp(info);
                                this.IsMarkUpMode = true;
                                this.btnMarkup.Enabled = false;
                            }
                        }
                        else
                        {
                            MessageBoxPLM.Show("没有获取到有效的浏览器，无法批注。", "文件批注", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBoxPLM.Show("批注文件必须通过内部浏览器完成。", "文件批注", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception exception4)
            {
                PrintException.Print(exception4);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveMarkUp();
        }

        private void btnTopMost_Click(object sender, EventArgs e)
        {
            if (!base.TopMost)
            {
                base.TopMost = true;
                this.btnTopMost.Text = "取消置顶";
            }
            else
            {
                base.TopMost = false;
                this.btnTopMost.Text = "置顶";
            }
        }

        private void combFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            DESecureFile selectedItem;
            string str;
            bool flag;
            FileBrowseWay way;
            if (this.combFiles.SelectedItem != null)
            {
                if (this.IsMarkUpMode)
                {
                    MessageBox.Show("当前文件处于批注状态，请先保存批注或者退出批注状态再操作！", "提示");
                    this.combFiles.SelectedIndexChanged -= new EventHandler(this.combFiles_SelectedIndexChanged);
                    this.combFiles.SelectedIndex = this.LastSelectedIndex;
                    this.combFiles.SelectedIndexChanged += new EventHandler(this.combFiles_SelectedIndexChanged);
                    return;
                }
                if (this._bizItem == null)
                {
                    return;
                }
                selectedItem = this.combFiles.SelectedItem as DESecureFile;
                this.LastSelectedIndex = this.combFiles.SelectedIndex;
                if (this._bizItem.FileList.GetFileByFileOid(selectedItem.FileOid) == null)
                {
                    return;
                }
                if (Path.GetExtension(selectedItem.FileName).ToUpper() == ".GXT")
                {
                    switch (PLGrantPerm.Agent.CanDoObjectOperation(ClientData.LogonUser.Oid, this._bizItem.MasterOid, this._bizItem.ClassName, PLGrantPerm.ToPermString(PLMBOOperation.BODownload), this._bizItem.SecurityLevel, this._bizItem.Phase, this._bizItem.RevNum))
                    {
                        case 0:
                            MessageBoxPLM.Show("您没有下载该对象源文件的权限。", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;

                        case 2:
                            MessageBoxPLM.Show("当前对象在流程中，您没有下载该对象源文件的权限。", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                    }
                }
                DESecureFile relationFileForBrowse = ViewFileHelper.GetRelationFileForBrowse(this._bizItem.Iteration, selectedItem);
                if (relationFileForBrowse != null)
                {
                    selectedItem = relationFileForBrowse;
                }
                ArrayList files = PLFileService.Agent.GetFiles(selectedItem.FileOid);
                if ((files != null) && (files.Count > 0))
                {
                    DEFile file3 = files[0] as DEFile;
                    if (file3 != null)
                    {
                        int num2 = Convert.ToInt32((long) (file3.OriginalSize / 0x100000L));
                        if ((num2 > 50) && (MessageBoxPLM.Show("您当前要浏览的源文件为" + num2.ToString() + "M，浏览会花费较长时间，您确认要继续吗？", "浏览源文件", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Cancel))
                        {
                            return;
                        }
                    }
                }
                str = string.Empty;
                string path = string.Empty;
                flag = false;
                if (FileEvent.fileOperEvent != null)
                {
                    FileEvent.fileOperEvent(OperEventType.Before, "ClaRel_BROWSE", selectedItem.FileOid);
                }
                bool editable = this._bizItem.CanEdit(ClientData.LogonUser.Oid);
                if (!selectedItem.InLocalHost)
                {
                    try
                    {
                        str = ViewFileHelper.DownloadFileByDir(this._bizItem.Iteration, selectedItem, null, ClientData.UserGlobalOption, false, true, "ClaRel_BROWSE");
                    }
                    catch (Exception exception4)
                    {
                        if (editable)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception4.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception4.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }
                    switch (str)
                    {
                        case null:
                        case "":
                            return;
                    }
                    if (!File.Exists(str))
                    {
                        MessageBoxPLM.Show("指定的文件索引不存在，可能是文件没有正确上载。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    flag = true;
                    goto Label_04DE;
                }
                if (selectedItem.InCurrentHost)
                {
                    path = selectedItem.Location;
                    str = selectedItem.Location + @"\" + selectedItem.FileName;
                    if (!File.Exists(str))
                    {
                        if (DialogResult.Yes == MessageBoxPLM.Show("在指定路径下没有找到该文件，无法浏览！可能是文件路径发生变化，或已被删除。是否尝试从服务器获取文件？", "浏览源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            try
                            {
                                ViewFileHelper.DownLoadFile(this._bizItem.Iteration, selectedItem, path, ClientData.UserGlobalOption, editable, "ClaRel_BROWSE", this._bizItem);
                                goto Label_04DE;
                            }
                            catch (Exception exception)
                            {
                                if (editable)
                                {
                                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }
                        return;
                    }
                    try
                    {
                        if (ClientData.LogonUser.Oid != selectedItem.UserOid)
                        {
                            str = ViewFileHelper.DownLoadFile(this._bizItem.Iteration, selectedItem, null, ClientData.UserGlobalOption, true, "ClaRel_BROWSE", this._bizItem);
                        }
                        if (!File.Exists(str))
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：源文件未上载", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        goto Label_04DE;
                    }
                    catch (Exception exception2)
                    {
                        if (editable)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception2.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception2.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }
                }
                if (DialogResult.Yes == MessageBoxPLM.Show("文件存在于他人机器上。是否尝试从服务器获取文件？", "浏览源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        str = ViewFileHelper.DownLoadFile(this._bizItem.Iteration, selectedItem, null, ClientData.UserGlobalOption, editable, "ClaRel_BROWSE", this._bizItem);
                        if (str == null)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：文件未上载", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        goto Label_04DE;
                    }
                    catch (Exception exception3)
                    {
                        if (editable)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception3.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception3.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return;
                    }
                }
            }
            return;
        Label_04DE:
            way = FileBrowseWay.InnerBrowser;
            string title = "文件浏览";
            BrowserDisplayRule rule = null;
            if (Path.GetExtension(str).ToUpper() == ".GXT")
            {
                rule = new BrowserDisplayRule {
                    IsEdit = IsEditGxt,
                    OnlyBrowser = IsOnlyBrowser
                };
                if (IsEditGxt)
                {
                    title = "文件编辑";
                }
                rule.Item = this._bizItem;
                IsEditGxt = false;
                IsOnlyBrowser = false;
            }
            DEBrowser browser = null;
            try
            {
                way = UIBrowser.GetBrowser(selectedItem.FileOid, str, selectedItem.FileType, out browser);
            }
            catch (Exception exception5)
            {
                PrintException.Print(exception5, title);
                return;
            }
            if (way == FileBrowseWay.InnerBrowser)
            {
                string deleteFilePath = null;
                if (flag)
                {
                    deleteFilePath = Path.GetDirectoryName(str);
                }
                UIBrowser.OpenFileWithBrowser(selectedItem.FileOid, str, this.panelBrowser, way, browser, deleteFilePath, rule, this._bizItem);
            }
            else
            {
                UIBrowser.OpenFileWithBrowser(selectedItem.FileOid, str, null, way, browser, Path.GetDirectoryName(str), this._bizItem);
            }
        }

        private void DelViewer(string type)
        {
            for (int i = 0; i < this.panelBrowser.Controls.Count; i++)
            {
                Control control = this.panelBrowser.Controls[i];
                if (control.GetType().ToString() == type)
                {
                    this.panelBrowser.Controls.RemoveAt(i);
                    i--;
                    base.Close();
                    return;
                }
            }
        }

        private void DoShowMarkUp(DESecureFile info)
        {
            if (this.browser == null)
            {
                MessageBox.Show("没有合适的批注编辑器，无法批注！", "提示");
            }
            else
            {
                this.Mode = 0;
                this.CurFile = info;
                this.lstSelfDefRemarks.Items.Clear();
                Hashtable userOption = ClientData.GetUserOption();
                for (int i = 1; userOption["Option_Remark_" + i.ToString()] != null; i++)
                {
                    string item = (string) userOption["Option_Remark_" + i.ToString()];
                    this.lstSelfDefRemarks.Items.Add(item);
                }
                this.txtRemark.Text = "";
                this.markLocation = ViewFileHelper.Instance.GetTempMarkupPath();
                this.panelButtons.Visible = true;
                this.splitContainerSmall.PanelVisibility = SplitPanelVisibility.Both;
                this.markFileName = null;
                if (Directory.Exists(this.markLocation))
                {
                    if ((Directory.GetFiles(this.markLocation).Length > 0) && (MessageBox.Show("系统可能正在批注其他的文件，继续操作将使原批注文件丢失，是否继续操作？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No))
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
                try
                {
                    this.curMarkup = PLItem.Agent.GetMarkup(info.FileOid, this._processArgs.WorkItemOid, this._processArgs.GroupDataOid, ClientData.LogonUser.Oid);
                    if ((this.curMarkup != null) && (this.Mode == 0))
                    {
                        this.Mode = 2;
                    }
                    if ((this.Mode == 0) && (this.curMarkup == null))
                    {
                        this.curMarkup = new DEMarkup();
                        this.curMarkup.FileOid = info.FileOid;
                        this.curMarkup.ProcessOid = this._processArgs.ProcessInstanceOid;
                        this.curMarkup.WorkItemOid = this._processArgs.WorkItemOid;
                        this.curMarkup.DataOid = this._processArgs.GroupDataOid;
                        this.curMarkup.UserOid = ClientData.LogonUser.Oid;
                        this.curMarkup.MarkupFileOid = Guid.Empty;
                        this.curMarkup.ClassName = this._className;
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
                catch
                {
                }
            }
        }

        private void FrmViewFile_Closing(object sender, CancelEventArgs e)
        {
            BrowserPool.BrowserManager.RemoveBrowser(this.panelBrowser);
            if (BizItemHandlerEvent.Instance.D_BeforeCloseForm != null)
            {
                BizItemHandlerEvent.Instance.D_BeforeCloseForm(this);
            }
        }

        private static DESecureFile GetSecureFile(IBizItem bizItem, Guid fileOid) {
            return PSConvert.ToBizItem(bizItem, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid).Iteration.FileList.FindFile(fileOid);
        }

        private void lstSelfDefRemarks_DoubleClick(object sender, EventArgs e)
        {
            this.AddRemark();
        }

        private void lstSelfDefRemarks_MouseMove(object sender, MouseEventArgs e)
        {
            int num = this.lstSelfDefRemarks.IndexFromPoint(e.Location);
            if (((num != -1) && (num < this.lstSelfDefRemarks.Items.Count)) && (this.tip.GetToolTip(this.lstSelfDefRemarks) != this.lstSelfDefRemarks.Items[num].ToString()))
            {
                this.tip.SetToolTip(this.lstSelfDefRemarks, this.lstSelfDefRemarks.Items[num].ToString());
            }
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
            this.btnMarkup.Enabled = true;
            this.browser.ExitMarkupMode();
            Thread.Sleep(100);
            string[] files = Directory.GetFiles(this.markLocation);
            if ((files.Length == 0) && this.browser.CanMarkup())
            {
                MessageBox.Show("无法获取批注文件，请确认批注文件已保存！");
                bool allowMarkup = true;
                try
                {
                    if (this.markFileName == null)
                    {
                        this.browser.EnterMarkupMode(CurViewFilePath, this.markLocation, Path.GetFileName(CurViewFilePath) + ".plmmkp", allowMarkup);
                    }
                    else
                    {
                        this.browser.EnterMarkupMode(CurViewFilePath, this.markLocation, Path.GetFileName(this.markFileName), allowMarkup);
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
                if (!this.browser.CanMarkup() && (this.txtRemark.Text.Trim() == ""))
                {
                    goto Label_036C;
                }
                DEMarkup mark = new DEMarkup {
                    UserOid = ClientData.LogonUser.Oid,
                    ProcessOid = this._processArgs.ProcessInstanceOid,
                    WorkItemOid = this._processArgs.WorkItemOid,
                    DataOid = this._processArgs.GroupDataOid,
                    Markup = this.txtRemark.Text.Trim(),
                    FileOid = this.CurFile.FileOid,
                    MarkupFileOid = Guid.NewGuid(),
                    ItemOid = this._bizItem.IterOid,
                    ItemIteration = this._bizItem.LastIteration,
                    ClassName = this._bizItem.ClassName
                };
                bool flag2 = false;
                if (this.browser.CanMarkup())
                {
                    try
                    {
                        if (this.NeedUploadMarkupFile(this.CurFile.FileOid, files[0]))
                        {
                            FSClientUtil.UploadFile(mark.MarkupFileOid, files[0], this._bizItem.ClassName, 'I', Guid.Empty, ClientData.LogonUser.Name);
                            flag2 = true;
                        }
                    }
                    catch (Exception exception2)
                    {
                        PrintException.Print(exception2);
                        return;
                    }
                }
                if (!flag2 && (this.txtRemark.Text.Trim() == ""))
                {
                    goto Label_036C;
                }
                try
                {
                    PLItem.Agent.CreateMarkup(mark, ClientData.LogonUser.Oid);
                    goto Label_036C;
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
                if (this.curMarkup.Markup != this.txtRemark.Text.Trim())
                {
                    this.curMarkup.Markup = this.txtRemark.Text.Trim();
                    flag3 = true;
                }
                this.curMarkup.ItemOid = this._bizItem.IterOid;
                this.curMarkup.ItemIteration = this._bizItem.LastIteration;
                bool flag4 = false;
                if (this.browser.CanMarkup())
                {
                    try
                    {
                        if (this.NeedUploadMarkupFile(this.curMarkup.FileOid, files[0]))
                        {
                            FSClientUtil.UploadFile(this.curMarkup.MarkupFileOid, files[0], this._bizItem.ClassName, 'I', Guid.Empty, ClientData.LogonUser.Name);
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
        Label_036C:
            ViewFileHelper.DeleteDirectory(this.markLocation);
            this.IsMarkUpMode = false;
        }

        public void SaveMarkUp()
        {
            this.Save();
            this.panelButtons.Visible = false;
            this.splitContainerSmall.PanelVisibility = SplitPanelVisibility.Panel1;
            this.IsMarkUpMode = false;
        }

        private void SetInput(IBizItem bizItem, DESecureFile file, bool isShowFileList, DELProcessInfoForCLT processArgs, string className)
        {
            if (bizItem != null)
            {
                this._bizItem = PSConvert.ToBizItem(bizItem, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid);
            }
            else
            {
                this.pnlFileList.Visible = false;
                isShowFileList = false;
            }
            this._processArgs = processArgs;
            this._className = className;
            bool flag = isShowFileList ? (this._bizItem.FileCount > 1) : false;
            this.pnlFileList.Visible = flag || (this._processArgs != null);
            this.btnMarkup.Visible = this._processArgs != null;
            if (!this.IsMarkUpMode)
            {
                this.splitContainerSmall.PanelVisibility = SplitPanelVisibility.Panel1;
                this.panelButtons.Visible = false;
                this.btnMarkup.Enabled = true;
            }
            else
            {
                this.splitContainerSmall.PanelVisibility = SplitPanelVisibility.Both;
                this.panelButtons.Visible = true;
                this.btnMarkup.Enabled = false;
            }
            ArrayList fileList = null;
            if (isShowFileList)
            {
                fileList = this._bizItem.FileList;
            }
            else
            {
                fileList = new ArrayList {
                    file
                };
            }
            this.combFiles.SelectedIndexChanged -= new EventHandler(this.combFiles_SelectedIndexChanged);
            this.combFiles.DataSource = fileList;
            try
            {
                for (int i = 0; i < this.combFiles.Properties.Items.Count; i++)
                {
                    DESecureFile file2 = this.combFiles.Properties.Items[i] as DESecureFile;
                    if ((file2 != null) && (file2.FileOid == file.FileOid))
                    {
                        this.combFiles.SelectedIndex = i;
                        goto Label_01A1;
                    }
                }
            }
            finally
            {
                this.combFiles.SelectedIndexChanged += new EventHandler(this.combFiles_SelectedIndexChanged);
            }
        Label_01A1:
            this.LastSelectedIndex = this.combFiles.SelectedIndex;
        }

        public void SetStatusText(DESecureFile file)
        {
            if (file == null)
            {
                this.statusStrip1.Visible = false;
            }
            else
            {
                this.statusStrip1.Visible = file.ConvertRuleOid == Guid.Empty;
                this.tspStateVaild.Text = file.IsConvertVaild ? "转换文件有效" : "转换文件无效";
            }
        }

        private void ShowMarkup()
        {
            if (this.curMarkup == null)
            {
                throw new Exception("错误，无法显示批注信息");
            }
            if (this.curMarkup.Markup != null)
            {
                this.txtRemark.Text = this.curMarkup.Markup;
            }
            if (CurViewFilePath != null)
            {
                try
                {
                    bool allowMarkup = true;
                    if (this.markFileName == null)
                    {
                        this.browser.EnterMarkupMode(CurViewFilePath, this.markLocation, Path.GetFileName(CurViewFilePath) + ".plmmkp", allowMarkup);
                    }
                    else
                    {
                        this.browser.EnterMarkupMode(CurViewFilePath, this.markLocation, Path.GetFileName(this.markFileName), allowMarkup);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (this.txtRemark.Text.Length == 0)
            {
                this.groupBox2.Text = "填写批注(1000字以内)";
            }
            else
            {
                int num = 0x3e8 - this.txtRemark.Text.Length;
                if (num == 0)
                {
                    this.groupBox2.Text = "填写批注(已经达到1000字上限)";
                }
                else
                {
                    this.groupBox2.Text = "填写批注(还可以写 " + num.ToString() + "个字)";
                }
            }
        }

        public static void ViewFile(Guid fileOid, string fileName, Guid fileTypeOid, bool deleteAllowed)
        {
            DESecureFile file = new DESecureFile {
                FileOid = fileOid,
                FileName = Path.GetFileName(fileName),
                Location = Path.GetDirectoryName(fileName),
                FileType = fileTypeOid
            };
            ViewFile(file, fileName, null, null, deleteAllowed, null);
        }

        public static void ViewFile(Guid fileOid, string fileName, Guid fileTypeOid, bool deleteAllowed, IBizItem bizItem)
        {
            DESecureFile file = new DESecureFile {
                FileOid = fileOid,
                FileName = Path.GetFileName(fileName),
                Location = Path.GetDirectoryName(fileName),
                FileType = fileTypeOid
            };
            ViewFile(file, fileName, (bizItem == null) ? string.Empty : bizItem.ClassName, null, deleteAllowed, bizItem);
        }

        public static void ViewFile(DESecureFile file, string fileName, string className, DELProcessInfoForCLT processArgs, bool deleteAllowed, IBizItem bizItem)
        {
            ViewFile(file, fileName, className, processArgs, deleteAllowed, bizItem, null, false);
        }

        public static void ViewFile(DESecureFile file, string fileName, string className, DELProcessInfoForCLT processArgs, bool deleteAllowed, IBizItem bizItem, DEBrowser browser, bool isShowFileList)
        {
            CurViewFilePath = fileName;
            if (bizItem != null)
            {
                switch (PLGrantPerm.Agent.CanDoObjectOperation(ClientData.LogonUser.Oid, bizItem.MasterOid, bizItem.ClassName, PLGrantPerm.ToPermString(PLMBOOperation.BOView), bizItem.SecurityLevel, bizItem.Phase, bizItem.RevNum))
                {
                    case 0:
                        MessageBoxPLM.Show("您没有浏览该对象源文件的权限。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;

                    case 2:
                        MessageBoxPLM.Show("当前对象在流程中，您没有浏览该对象源文件的权限。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                }
                if (Path.GetExtension(fileName).ToUpper() == ".GXT")
                {
                    switch (PLGrantPerm.Agent.CanDoObjectOperation(ClientData.LogonUser.Oid, bizItem.MasterOid, bizItem.ClassName, PLGrantPerm.ToPermString(PLMBOOperation.BODownload), bizItem.SecurityLevel, bizItem.Phase, bizItem.RevNum))
                    {
                        case 0:
                            MessageBoxPLM.Show("您没有下载该对象源文件的权限。", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;

                        case 2:
                            MessageBoxPLM.Show("当前对象在流程中，您没有下载该对象源文件的权限。", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                    }
                }
            }
            FileBrowseWay innerBrowser = FileBrowseWay.InnerBrowser;
            string title = "文件浏览";
            BrowserDisplayRule rule = null;
            if (Path.GetExtension(fileName).ToUpper() == ".GXT")
            {
                rule = new BrowserDisplayRule {
                    IsEdit = IsEditGxt,
                    OnlyBrowser = IsOnlyBrowser
                };
                if (IsEditGxt)
                {
                    title = "文件编辑";
                }
                rule.Item = bizItem as DEBusinessItem;
                IsEditGxt = false;
                IsOnlyBrowser = false;
            }
            try
            {
                if (browser == null)
                {
                    innerBrowser = UIBrowser.GetBrowser(file.FileOid, fileName, file.FileType, out browser);
                }
                else
                {
                    innerBrowser = browser.IsInnerBrowser ? FileBrowseWay.InnerBrowser : FileBrowseWay.OpenProcess;
                }
            }
            catch (Exception exception)
            {
                PrintException.Print(exception, title);
                return;
            }
            if (innerBrowser == FileBrowseWay.InnerBrowser)
            {
                FrmViewFile file2 = null;
                Form form = FormManager.GetForm(PLMFormType.ViewFile, file.FileOid);
                if ((form != null) && (form is FrmViewFile))
                {
                    file2 = (FrmViewFile) form;
                    if (bizItem != null)
                    {
                        file2.Text = bizItem.Id + "[" + ModelContext.MetaModel.GetClassLabel(bizItem.ClassName) + "] - " + title;
                        file2.SetStatusText(GetSecureFile(bizItem, file.FileOid));
                    }
                    else
                    {
                        file2.Text = Path.GetFileName(fileName) + " - " + title;
                    }
                }
                else
                {
                    try
                    {
                        file2 = new FrmViewFile();
                        if (bizItem != null)
                        {
                            file2.Text = bizItem.Id + "[" + ModelContext.MetaModel.GetClassLabel(bizItem.ClassName) + "] - " + title;
                            file2.SetStatusText(GetSecureFile(bizItem, file.FileOid));
                        }
                        else
                        {
                            file2.Text = Path.GetFileName(fileName) + " - " + title;
                        }
                    }
                    catch (Exception exception2)
                    {
                        PrintException.Print(exception2, MessageBoxIcon.Exclamation);
                        return;
                    }
                    file2.SetManaged(PLMFormType.ViewFile, file.FileOid);
                }
                file2.SetInput(bizItem, file, isShowFileList, processArgs, className);
                file2.Show();
                file2.Activate();
                string deleteFilePath = null;
                if (deleteAllowed)
                {
                    deleteFilePath = Path.GetDirectoryName(fileName);
                }
                UIBrowser.OpenFileWithBrowser(file.FileOid, fileName, file2.panelBrowser, innerBrowser, browser, deleteFilePath, rule, bizItem);
            }
            else
            {
                UIBrowser.OpenFileWithBrowser(file.FileOid, fileName, null, innerBrowser, browser, Path.GetDirectoryName(fileName), bizItem);
            }
        }
    }
}

