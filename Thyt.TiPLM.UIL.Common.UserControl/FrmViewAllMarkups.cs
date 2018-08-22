    using DevExpress.Utils;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.BPM;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.FileService;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
    using Thyt.TiPLM.UIL.Environment;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class FrmViewAllMarkups : FormPLM
    {
        private Hashtable AllFileNames = new Hashtable();
        private Hashtable AllMarkups = new Hashtable();
        
        private string className;
        
        private DESecureFile file;
        private string fileName;
        private Guid fileOid;
        private Hashtable htUsers = new Hashtable();
        private bool isShowDialog;
        private DEObjectAttachFile item;
        
        private ArrayList MarkupList = new ArrayList();
        private ArrayList ProcessList = new ArrayList();
        private Hashtable processToWorkItems = new Hashtable();

        public FrmViewAllMarkups(string fileName, Guid fileOid, string className, ArrayList MarkupList, bool isShowDialog, DESecureFile file, DEObjectAttachFile item)
        {
            this.InitializeComponent();
            this.fileOid = fileOid;
            this.className = className;
            this.fileName = fileName;
            this.MarkupList = MarkupList;
            this.isShowDialog = isShowDialog;
            this.file = file;
            this.item = item;
            this.AllMarkups.Add(fileOid, MarkupList);
            this.AllFileNames.Add(fileOid, fileName);
            this.Fill();
        }

        private void AddProcessToList(DELProcessInsProperty pI)
        {
            foreach (ListViewItem item in this.lvwProcess.Items)
            {
                if (item.Tag == pI)
                {
                    return;
                }
            }
            ListViewItem item2 = new ListViewItem(pI.Name) {
                SubItems = { this.Translate(pI.State) },
                Tag = pI
            };
            this.lvwProcess.Items.Add(item2);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((this.lvwProcess.SelectedIndices.Count != 1) || (this.lvwWorkItem.SelectedIndices.Count != 1))
            {
                if (this.lvwProcess.SelectedIndices.Count == 0)
                {
                    MessageBoxPLM.Show("请选择一个流程！");
                }
                else if (this.lvwWorkItem.SelectedIndices.Count == 0)
                {
                    MessageBoxPLM.Show("请选择一个步骤！");
                }
                base.DialogResult = DialogResult.None;
            }
            else
            {
                FileBrowseWay way;
                base.DialogResult = DialogResult.OK;
                DELProcessInsProperty tag = (DELProcessInsProperty) this.lvwProcess.SelectedItems[0].Tag;
                DELWorkItem item = (DELWorkItem) ((object[]) this.lvwWorkItem.SelectedItems[0].Tag)[0];
                DEMarkup markup = (DEMarkup) ((object[]) this.lvwWorkItem.SelectedItems[0].Tag)[1];
                base.Close();
                if (markup.FileOid != this.file.FileOid)
                {
                    DESecureFile fileByFileOid = PLItem.Agent.GetBizItemByIteration(markup.ItemOid, this.className, Guid.Empty, ClientData.LogonUser.Oid, BizItemMode.SmartBizItem).FileList.GetFileByFileOid(markup.FileOid);
                    if (fileByFileOid != null)
                    {
                        this.file = fileByFileOid;
                    }
                    else
                    {
                        this.file.FileOid = markup.FileOid;
                    }
                }
                if (this.AllFileNames.Contains(this.file.FileOid))
                {
                    this.fileName = this.AllFileNames[this.file.FileOid] as string;
                }
                else
                {
                    this.fileName = FSClientUtil.DownloadFile(this.file.FileOid, "ClaRel_DOWNLOAD");
                    this.file.FileType = UIFileType.GetFileType(this.fileName);
                    this.file.FileName = this.fileName;
                    this.AllFileNames.Add(this.file.FileOid, this.fileName);
                }
                DEBrowser browser = null;
                try
                {
                    way = UIBrowser.GetMarkupTool(this.file.FileOid, this.file.FileName, this.file.FileType, out browser);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "浏览文件批注");
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
                        catch (Exception exception2)
                        {
                            MessageBoxPLM.Show(exception2.Message);
                            return;
                        }
                        if (browser2 != null)
                        {
                            if (this.AllFileNames.Contains(this.file.FileOid))
                            {
                                this.fileName = this.AllFileNames[this.file.FileOid] as string;
                            }
                            else
                            {
                                this.fileName = FSClientUtil.DownloadFile(this.file.FileOid, "ClaRel_DOWNLOAD");
                                this.AllFileNames.Add(this.file.FileOid, this.fileName);
                            }
                            FrmMarkupByBrowser.ViewMarkUp(this.fileName, this.file.FileOid, tag.ID, item.ID, Guid.Empty, item.ActorID, this.className, this.isShowDialog, browser2);
                        }
                    }
                }
                else
                {
                    MessageBoxPLM.Show("必须定义打开文件的内部浏览器，否则不能批注");
                }
            }
        }

        private void cmbFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.item != null)
            {
                this.file = this.item.FileList[this.cmbFileList.SelectedIndex] as DESecureFile;
            }
            if (this.AllMarkups.Contains(this.file.FileOid))
            {
                this.MarkupList = this.AllMarkups[this.file.FileOid] as ArrayList;
            }
            else
            {
                try
                {
                    this.MarkupList = PLItem.Agent.GetMarkups(this.file.ItemMasterOid, this.file.FileOid, ClientData.LogonUser.Oid);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "获取批注信息");
                    this.FillProcess();
                    return;
                }
                if ((this.MarkupList != null) && (this.MarkupList.Count != 0))
                {
                    this.AllMarkups.Add(this.file.FileOid, this.MarkupList);
                }
                else
                {
                    MessageBoxPLM.Show("文件未被批注过！");
                    this.FillProcess();
                    return;
                }
            }
            this.FillProcess();
        }
 
        private void Fill()
        {
            this.cmbFileList.Properties.Items.Clear();
            this.cmbFileList.Text = "";
            if (this.item == null)
            {
                this.cmbFileList.Properties.Items.Add(this.file.FileName);
                this.cmbFileList.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < this.item.FileCount; i++)
                {
                    this.cmbFileList.Properties.Items.Add(((DESecureFile) this.item.FileList[i]).FileName);
                    if (this.item.FileList[i] == this.file)
                    {
                        this.cmbFileList.SelectedIndex = i;
                    }
                }
            }
        }

        private void FillProcess()
        {
            this.lvwProcess.Items.Clear();
            this.lvwWorkItem.Items.Clear();
            foreach (DEMarkup markup in this.MarkupList)
            {
                bool flag = false;
                DELProcessInsProperty theDELProcessInsProperty = null;
                foreach (DELProcessInsProperty property2 in this.ProcessList)
                {
                    if (property2.ID == markup.ProcessOid)
                    {
                        theDELProcessInsProperty = property2;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    BPMProcessor processor = new BPMProcessor();
                    try
                    {
                        processor.GetProcessInsProperty(ClientData.LogonUser.Oid, markup.ProcessOid, out theDELProcessInsProperty);
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception);
                        continue;
                    }
                    if (theDELProcessInsProperty != null)
                    {
                        this.ProcessList.Add(theDELProcessInsProperty);
                    }
                }
                if (theDELProcessInsProperty != null)
                {
                    this.AddProcessToList(theDELProcessInsProperty);
                }
            }
            if (this.lvwProcess.Items.Count > 0)
            {
                this.lvwProcess.Items[0].Selected = true;
            }
        }

        private ArrayList FindWorkItems(Guid processOid)
        {
            BPMProcessor processor = new BPMProcessor();
            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            foreach (DEMarkup markup in this.MarkupList)
            {
                if ((processOid == markup.ProcessOid) && !list2.Contains(markup.WorkItemOid))
                {
                    DELWorkItem theWorkItem = new DELWorkItem();
                    try
                    {
                        processor.GetWorkItemByOid(markup.WorkItemOid, out theWorkItem);
                        list.Add(theWorkItem);
                        list2.Add(markup.WorkItemOid);
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception);
                    }
                }
            }
            return list;
        }

        private DEUser GetUserById(Guid userOid)
        {
            PLUser user = new PLUser();
            if (this.htUsers.Contains(userOid))
            {
                return (DEUser) this.htUsers[userOid];
            }
            DEUser userByOid = user.GetUserByOid(userOid);
            if (userByOid != null)
            {
                this.htUsers[userOid] = userByOid;
                return userByOid;
            }
            return null;
        }


        private void lvwProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwProcess.SelectedIndices.Count == 1)
            {
                this.lvwWorkItem.Items.Clear();
                DELProcessInsProperty tag = (DELProcessInsProperty) this.lvwProcess.SelectedItems[0].Tag;
                ArrayList list = new ArrayList();
                string key = tag.ID.ToString() + this.cmbFileList.SelectedIndex;
                if (this.processToWorkItems[key] == null)
                {
                    list = this.FindWorkItems(tag.ID);
                    this.processToWorkItems.Add(key, list);
                }
                else
                {
                    list = (ArrayList) this.processToWorkItems[key];
                }
                foreach (DELWorkItem item in list)
                {
                    ListViewItem item2 = new ListViewItem(item.Name);
                    if ((item.CompletedDate == DateTime.MinValue) || (item.CompletedDate == DateTime.MaxValue))
                    {
                        item2.SubItems.Add("未完成");
                    }
                    else
                    {
                        item2.SubItems.Add(item.CompletedDate.ToString());
                    }
                    object[] objArray = new object[2];
                    objArray[0] = item;
                    item2.Tag = objArray;
                    bool flag = false;
                    foreach (DEMarkup markup in this.MarkupList)
                    {
                        if ((markup.ProcessOid == item.ProcessInstanceID) && (markup.WorkItemOid == item.ID))
                        {
                            DEUser userById = null;
                            try
                            {
                                userById = this.GetUserById(markup.UserOid);
                            }
                            catch (Exception)
                            {
                            }
                            if (userById != null)
                            {
                                item2.SubItems.Add(userById.Name);
                            }
                            else
                            {
                                item2.SubItems.Add(item.Reserve1);
                            }
                            item2.SubItems.Add(markup.Markup);
                            objArray[1] = markup;
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        this.lvwWorkItem.Items.Add(item2);
                    }
                }
            }
        }

        private void lvwWorkItem_DoubleClick(object sender, EventArgs e)
        {
            this.btnOK_Click(sender, e);
        }

        private string Translate(string eString)
        {
            string str = eString;
            string str2 = eString;
            if (str2 == null)
            {
                return str;
            }
            if (str2 != "Initial")
            {
                if (str2 != "Running")
                {
                    if (str2 == "Completed")
                    {
                        return "已完成";
                    }
                    if (str2 != "Aborted")
                    {
                        return str;
                    }
                    return "已取消";
                }
            }
            else
            {
                return "初始化";
            }
            return "运行中";
        }

        public static void ViewAllMarkups(string fileName, Guid fileOid, string className, bool isShowDialog, DESecureFile file, DEObjectAttachFile item)
        {
            ArrayList markupList = new ArrayList();
            try
            {
                markupList = PLItem.Agent.GetMarkups(file.ItemMasterOid, fileOid, ClientData.LogonUser.Oid);
            }
            catch (Exception exception)
            {
                PrintException.Print(exception, "获取批注信息");
                return;
            }
            if ((markupList != null) && (markupList.Count != 0))
            {
                new FrmViewAllMarkups(fileName, fileOid, className, markupList, isShowDialog, file, item).ShowDialog();
            }
            else
            {
                MessageBoxPLM.Show("文件未被批注过！");
            }
        }
    }
}

