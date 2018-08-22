namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.Common.Interface.Addin;
    using Thyt.TiPLM.Common.Interface.Environment;
    using Thyt.TiPLM.Common.Interface.Product;
    using Thyt.TiPLM.DEL.Admin.BPM;
    using Thyt.TiPLM.DEL.Environment;
    using Thyt.TiPLM.DEL.FileConvertor;
    using Thyt.TiPLM.DEL.Foundation.FileService;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.Common;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.PLL.FileService;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Addin;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
    using Thyt.TiPLM.UIL.Environment;

    public sealed class ViewFileHelper
    {
        private static FolderBrowserDialog DownloadFolderDlg = null;
        public static ViewFileHelper Instance = new ViewFileHelper();

        private ViewFileHelper()
        {
        }

        public void AfterDownLoad(ArrayList lst)
        {
            Hashtable hashtable = new Hashtable();
            new Hashtable();
            Thyt.TiPLM.PLL.Environment.PLFileType type = new Thyt.TiPLM.PLL.Environment.PLFileType();
            Hashtable hashtable2 = new Hashtable();
            Hashtable hashtable3 = new Hashtable();
            try
            {
                foreach (DEDownloadSignaturFile file in lst)
                {
                    if (file.SecureFileList.Count != 0)
                    {
                        for (int i = 0; i < file.SecureFileList.Count; i++)
                        {
                            DESecureFile file2 = file.SecureFileList[i] as DESecureFile;
                            if (file2 != null)
                            {
                                string path = file.SecureFileList[i + 1].ToString();
                                if (path.IndexOf(@"\\") > 0)
                                {
                                    path = path.Replace(@"\\", @"\");
                                }
                                if (((FrmErrInfo.HsErrInfo != null) && (FrmErrInfo.HsErrInfo.Count > 0)) && FrmErrInfo.HsErrInfo.ContainsKey(file.Item.Master.Id))
                                {
                                    ArrayList list2 = FrmErrInfo.HsErrInfo[file.Item.Master.Id] as ArrayList;
                                    if (list2.Contains(Path.GetFileName(path)))
                                    {
                                        continue;
                                    }
                                }
                                if (file2.FileType == Guid.Empty)
                                {
                                    ArrayList fileTypeByFilePath = type.GetFileTypeByFilePath(path);
                                    for (int j = 0; j < fileTypeByFilePath.Count; j++)
                                    {
                                        DEFileType type3 = fileTypeByFilePath[j] as DEFileType;
                                        if ((type3 != null) && !type3.DefaultAfterDownLoadToolOid.Equals(Guid.Empty))
                                        {
                                            file2.FileType = type3.Oid;
                                            break;
                                        }
                                    }
                                }
                                if (file2.FileType != Guid.Empty)
                                {
                                    DEFileType fileType;
                                    if (hashtable2.Contains(file2.FileType))
                                    {
                                        fileType = hashtable2[file2.FileType] as DEFileType;
                                    }
                                    else
                                    {
                                        fileType = type.GetFileType(file2.FileType);
                                        hashtable2[file2.FileType] = fileType;
                                    }
                                    if (fileType != null)
                                    {
                                        if (Path.GetExtension(path).ToUpper() == ".GXT")
                                        {
                                            string str2 = Path.GetFileNameWithoutExtension(path) + ".emf";
                                            string emfPicPath = Path.Combine(Path.GetDirectoryName(path), str2);
                                            DEBusinessItem item = null;
                                            if ((file.Item != null) && (file.Item != null))
                                            {
                                                item = file.Item;
                                            }
                                            if (BizItemHandlerEvent.Instance.D_CreateEmf != null)
                                            {
                                                BizItemHandlerEvent.Instance.D_CreateEmf(item, path, emfPicPath);
                                            }
                                        }
                                        if (fileType.DefaultAfterDownLoadToolOid != Guid.Empty)
                                        {
                                            Hashtable hashtable4;
                                            ArrayList list;
                                            ArrayList list4;
                                            if (hashtable.Contains(fileType.DefaultAfterDownLoadToolOid))
                                            {
                                                hashtable4 = (Hashtable) hashtable[fileType.DefaultAfterDownLoadToolOid];
                                            }
                                            else
                                            {
                                                hashtable4 = new Hashtable();
                                                hashtable[fileType.DefaultAfterDownLoadToolOid] = hashtable4;
                                            }
                                            if (hashtable4.ContainsKey(file.Item))
                                            {
                                                list = hashtable4[file.Item] as ArrayList;
                                            }
                                            else
                                            {
                                                list = new ArrayList();
                                                hashtable4[file.Item] = list;
                                            }
                                            list.Add(file2);
                                            if (hashtable3.ContainsKey(fileType.DefaultAfterDownLoadToolOid))
                                            {
                                                list4 = hashtable3[fileType.DefaultAfterDownLoadToolOid] as ArrayList;
                                            }
                                            else
                                            {
                                                list4 = new ArrayList();
                                                hashtable3[fileType.DefaultAfterDownLoadToolOid] = list4;
                                            }
                                            if (!list4.Contains(path))
                                            {
                                                list4.Add(path);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (hashtable.Count != 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
                    enumerator.Reset();
                    while (enumerator.MoveNext())
                    {
                        Guid key = (Guid) enumerator.Key;
                        Hashtable hsItems = (Hashtable) enumerator.Value;
                        if (hashtable3.ContainsKey(key))
                        {
                            ArrayList list5 = hashtable3[key] as ArrayList;
                            string[] files = (string[]) list5.ToArray(typeof(string));
                            try
                            {
                                IAfterDownLoad addinEntryObject = AddinFramework.Instance.GetAddinEntryObject(key) as IAfterDownLoad;
                                if (addinEntryObject != null)
                                {
                                    string str4;
                                    addinEntryObject.AfterDownloadFile(hsItems, files, out str4);
                                    if (!string.IsNullOrEmpty(str4))
                                    {
                                        MessageBoxPLM.Show(str4);
                                    }
                                }
                                continue;
                            }
                            catch (Exception exception)
                            {
                                MessageBoxPLM.Show(exception.Message);
                                PLMEventLog.WriteExceptionLog("下载后程序处理", exception);
                                continue;
                            }
                        }
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public static bool AuthorizeDownloadFile(IBizItem item)
        {
            switch (PLGrantPerm.Agent.CanDoObjectOperation(ClientData.LogonUser.Oid, item.MasterOid, item.ClassName, "ClaRel_DOWNLOAD", item.SecurityLevel, item.Phase, item.RevNum))
            {
                case 0:
                    MessageBoxPLM.Show("您没有对指定数据的文件下载权限。", "下载源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;

                case 2:
                    MessageBoxPLM.Show("指定数据在某个流程中，您没有在该流程中对数据的文件下载权限。", "下载源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
            }
            return true;
        }

        private static IBizItem[] ConvertPLMBizItemDelegateParam(object item)
        {
            if (item is IBizItem)
            {
                return new IBizItem[] { ((IBizItem) item) };
            }
            if (item is IBizItem[])
            {
                return (IBizItem[]) item;
            }
            return null;
        }

        public static bool DeleteDirectory(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                string[] directories = Directory.GetDirectories(dirPath);
                foreach (string str in Directory.GetFiles(dirPath))
                {
                    try
                    {
                        System.IO.File.Delete(str);
                    }
                    catch
                    {
                        return false;
                    }
                }
                foreach (string str2 in directories)
                {
                    try
                    {
                        if (!DeleteDirectory(str2))
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                try
                {
                    Directory.Delete(dirPath);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void DoConvertTask(DESecureFile file, DEFileOperRule rule, DEObjectAttachFile curItem)
        {
            IProgressCallback progressWindow = ClientData.GetProgressWindow();
            ArrayList state = new ArrayList(4) {
                progressWindow,
                file,
                rule,
                curItem
            };
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.DoConvertTaskInThread), state);
            progressWindow.ShowWindow();
        }

        private void DoConvertTaskInThread(object objs)
        {
            ArrayList list = (ArrayList) objs;
            object obj2 = list[0];
            DESecureFile file = list[1] as DESecureFile;
            DEFileOperRule rule = (DEFileOperRule) list[2];
            DEObjectAttachFile file2 = list[3] as DEObjectAttachFile;
            IProgressCallback callback = obj2 as IProgressCallback;
            try
            {
                callback.Begin(0, 100);
                if ((file != null) && (rule != null))
                {
                    FileConvertTaskManager.ConvertResult sucess = FileConvertTaskManager.ConvertResult.Sucess;
                    DEFileConvert[] fileConverts = null;
                    if (rule.IsDepend)
                    {
                        sucess = FileConvertTaskManager.instance.DoneFileConvertDependTask(file.FileOid, file.FileName, file2.Oid, file2.ClassName, file.FileType, RevisionEffectivityWay.LastRev, null, null, null, null, null, 1, FileConvertOperTye.Browser, ClientData.LogonUser.LogId, rule.Oid, out fileConverts);
                    }
                    else
                    {
                        sucess = FileConvertTaskManager.instance.DoneFileConvertTask(file2.Oid, file.FileOid, file.FileName, file2.ClassName, file.FileType, 1, FileConvertOperTye.Browser, ClientData.LogonUser.LogId, rule.Oid, out fileConverts);
                    }
                    switch (sucess)
                    {
                        case FileConvertTaskManager.ConvertResult.Sucess:
                            MessageBoxPLM.Show("文件转换成功！", ConstCommon.ProductName);
                            return;

                        case FileConvertTaskManager.ConvertResult.ConfigError:
                            MessageBoxPLM.Show("不能正常发起转换任务，请检查文件转换服务器相关配置！", ConstCommon.ProductName);
                            return;

                        case FileConvertTaskManager.ConvertResult.ConvertError:
                            MessageBoxPLM.Show("文件转换失败，请检查Windows日志！", ConstCommon.ProductName);
                            return;
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        public void DoLoadFileList(object alist)
        {
            ArrayList list = (ArrayList) alist;
            IProgressCallback callback = list[0] as IProgressCallback;
            FileIndexLoader loader = list[1] as FileIndexLoader;
            try
            {
                callback.Begin(0, 100);
                callback.SetText("正在获取文件列表，请稍候...");
                loader.Run();
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
            finally
            {
                if (callback != null)
                {
                    callback.End();
                }
            }
        }

        public static bool DownloadAllFiles(DEObjectAttachFile item, string pathName, DEPSOption option, Guid fileOid, bool isView, string downType)
        {
            DERelatedItemFiles itemFiles = PLItem.Agent.GetAllRelatedFiles(item.Oid, item.ClassName, fileOid, ClientData.LogonUser.Oid, option);
            foreach (Guid guid in itemFiles.AllFileOids)
            {
                if (guid != fileOid)
                {
                    DEFile file = null;
                    ArrayList files = PLFileService.Agent.GetFiles(guid);
                    if ((files != null) && (files.Count > 0))
                    {
                        file = (DEFile) files[0];
                        string path = pathName + @"\" + file.FileName;
                        if (System.IO.File.Exists(path))
                        {
                            FileInfo info = new FileInfo(path);
                            if (((info.Length == file.OriginalSize) && (info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") == file.FileCreateTime.ToString("yyyy-MM-dd HH:mm:ss"))) && (info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") == file.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss")))
                            {
                                itemFiles.SetFilePath(guid, path);
                                continue;
                            }
                        }
                        string gxtPath = FSClientUtil.DownloadFile(downType, file.FileId, path);
                        if (((path != null) && (Path.GetExtension(path) != null)) && (Path.GetExtension(path).ToUpper() == ".GXT"))
                        {
                            string str3 = Path.GetFileNameWithoutExtension(path) + ".emf";
                            string emfPicPath = Path.Combine(Path.GetDirectoryName(path), str3);
                            DEBusinessItem item2 = null;
                            if (item is DEItemIteration2)
                            {
                                item2 = (DEBusinessItem) PLItem.Agent.GetBizItemByIteration(item.Oid, item.ClassName, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                            }
                            if (BizItemHandlerEvent.Instance.D_CreateEmf != null)
                            {
                                BizItemHandlerEvent.Instance.D_CreateEmf(item2, gxtPath, emfPicPath);
                            }
                        }
                        FileInfo info2 = new FileInfo(path) {
                            CreationTime = file.FileCreateTime,
                            LastWriteTime = file.FileModifyTime
                        };
                        itemFiles.SetFilePath(guid, path);
                    }
                }
            }
            DESecureFile fileByFileOid = item.FileList.GetFileByFileOid(fileOid);
            if (fileByFileOid == null)
            {
                return false;
            }
            if (fileByFileOid.FileType == Guid.Empty)
            {
                fileByFileOid.FileType = UIFileType.GetFileType(fileByFileOid.FileName);
            }
            DEFileType fileType = new Thyt.TiPLM.PLL.Environment.PLFileType().GetFileType(fileByFileOid.FileType);
            if (((fileType != null) && (fileType.BeforeEditAddinOid != Guid.Empty)) && (AddinFramework.Instance.GetAddinByOid(fileType.BeforeEditAddinOid) != null))
            {
                IBeforeFileEdit addinEntryObject = AddinFramework.Instance.GetAddinEntryObject(fileType.BeforeEditAddinOid) as IBeforeFileEdit;
                if (addinEntryObject != null)
                {
                    addinEntryObject.ProcessFiles(itemFiles, pathName, ClientData.LogonUser.Oid);
                }
                else
                {
                    MessageBoxPLM.Show("当前文件类型定义中指定的前处理工具不可用，请联系系统管理员进行检查。", "文件浏览/编辑", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return true;
        }

        public static string DownloadFile(DEObjectAttachFile item, DESecureFile info, string destineDir, string desFileName, DEPSOption option, bool promptOverride, bool IsView, string downType)
        {
            if (item == null)
            {
                return "";
            }
            bool flag = false;
            foreach (DESecureFile file in item.FileList)
            {
                if (file.FileOid == info.FileOid)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                return "";
            }
            string directoryName = null;
            string tempPath = null;
            if (destineDir != null)
            {
                directoryName = destineDir;
                tempPath = Path.Combine(directoryName, info.FileName);
            }
            else if (desFileName != null)
            {
                directoryName = Path.GetDirectoryName(desFileName);
                tempPath = desFileName;
            }
            else
            {
                directoryName = FSClientUtil.GetTempFilePath(info.FileOid);
                tempPath = Path.Combine(directoryName, info.FileName);
                if ((PLSystemParam.SecurityPath != "") && !tempPath.StartsWith(PLSystemParam.SecurityPath))
                {
                    directoryName = PLSecurityDisk.Agent.ConvertPathToSecurity(directoryName);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    tempPath = PLSecurityDisk.Agent.ConvertPathToSecurity(tempPath);
                }
            }
            DEFile file2 = null;
            ArrayList files = PLFileService.Agent.GetFiles(info.FileOid);
            if ((files != null) && (files.Count > 0))
            {
                file2 = (DEFile) files[0];
            }
            if (file2 != null)
            {
                bool flag2 = false;
                if (System.IO.File.Exists(tempPath))
                {
                    FileInfo info2 = new FileInfo(tempPath);
                    if ((info2.Length == file2.OriginalSize) && (info2.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") == file2.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    {
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    if (promptOverride && System.IO.File.Exists(tempPath))
                    {
                        if (MessageBoxPLM.Show("文件" + tempPath + "已存在，是否覆盖？", "文件下载", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            tempPath = FSClientUtil.DownloadFile(downType, info.FileOid, tempPath);
                        }
                    }
                    else
                    {
                        tempPath = FSClientUtil.DownloadFile(downType, info.FileOid, tempPath);
                    }
                }
                if (item.ClassName == null)
                {
                    return tempPath;
                }
                bool flag3 = true;
                if (IsView)
                {
                    DEFileType fileType = null;
                    Thyt.TiPLM.PLL.Environment.PLFileType type2 = new Thyt.TiPLM.PLL.Environment.PLFileType();
                    if (info.FileType != Guid.Empty)
                    {
                        fileType = type2.GetFileType(info.FileType);
                    }
                    else
                    {
                        ArrayList fileTypeByFilePath = type2.GetFileTypeByFilePath(tempPath);
                        if ((fileTypeByFilePath != null) && (fileTypeByFilePath.Count > 0))
                        {
                            fileType = fileTypeByFilePath[0] as DEFileType;
                            info.FileType = fileType.Oid;
                        }
                    }
                    if (fileType != null)
                    {
                        flag3 = (fileType.Option & ConstEnvironment.FILETYPE_VIEWNOBYRELATION) != ConstEnvironment.FILETYPE_VIEWNOBYRELATION;
                    }
                }
                if (info.FileType != Guid.Empty)
                {
                    if (flag3)
                    {
                        DownloadAllFiles(item, directoryName, option, info.FileOid, IsView, downType);
                        return tempPath;
                    }
                    DownloadRelationFiles(item, directoryName, option, info.FileOid, info.FileType, IsView, downType);
                }
            }
            return tempPath;
        }

        public static string DownLoadFile(DEObjectAttachFile filelist, DESecureFile fileinfo, string path, DEPSOption psoption, bool editable, string downType, IBizItem bizItem)
        {
            string str = DownloadFileByDir(filelist, fileinfo, path, psoption, true, !editable, downType);
            if ((str == "") || !System.IO.File.Exists(str))
            {
                return null;
            }
            if (fileinfo.InLocalHost)
            {
            }
            try
            {
                PLLogger logger = new PLLogger();
                DEProductConfigLog productObj = new DEProductConfigLog();
                productObj.Description = productObj.Description + "下载源文件到" + Dns.GetHostName();
                if (bizItem == null)
                {
                    bizItem = PLItem.Agent.GetBizItemByIteration(filelist.Oid, filelist.ClassName, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                }
                productObj.ObjectOid = bizItem.MasterOid;
                productObj.ObjectType = ModelContext.MetaModel.GetClassLabel(bizItem.ClassName);
                productObj.OperObjName = bizItem.Id;
                productObj.Operator = ClientData.LogonUser.LogId;
                productObj.Operation = ConstEnvironment.GetProductDateOperTypeName(ConstEnvironment.ProductDateOperType.DownLoad);
                logger.CreateBoAccessObj(productObj);
            }
            catch
            {
            }
            return str;
        }

        public static string DownloadFileByDir(DEObjectAttachFile item, DESecureFile info, string destineDir, DEPSOption option, bool promptOverride, string downType){
           return DownloadFile(item, info, destineDir, null, option, promptOverride, false, downType);
        }
        public static string DownloadFileByDir(DEObjectAttachFile item, DESecureFile info, string destineDir, DEPSOption option, bool promptOverride, bool IsView, string downType){
           return DownloadFile(item, info, destineDir, null, option, promptOverride, IsView, downType);
    }
        public static string DownloadFileByFileName(DEObjectAttachFile item, DESecureFile info, string desFileName, DEPSOption option, bool promptOverride){
           return DownloadFile(item, info, null, desFileName, option, promptOverride, false, "ClaRel_DOWNLOAD");
        }
        public static bool DownloadRelationFiles(DEObjectAttachFile item, string pathName, DEPSOption option, Guid fileOid, Guid fileTypeOid, bool isView, string downType)
        {
            ArrayList relatedFileTypes = new Thyt.TiPLM.PLL.Environment.PLFileType().GetRelatedFileTypes(fileTypeOid);
            List<Guid> list2 = new List<Guid>();
            foreach (DESecureFile file in item.FileList)
            {
                foreach (Guid guid in relatedFileTypes)
                {
                    if (file.FileType == guid)
                    {
                        list2.Add(file.FileOid);
                        break;
                    }
                }
            }
            foreach (Guid guid2 in list2.ToArray())
            {
                if (guid2 != fileOid)
                {
                    DEFile file2 = null;
                    ArrayList files = PLFileService.Agent.GetFiles(guid2);
                    if ((files != null) && (files.Count > 0))
                    {
                        file2 = (DEFile) files[0];
                        string path = pathName + @"\" + file2.FileName;
                        if (System.IO.File.Exists(path))
                        {
                            FileInfo info = new FileInfo(path);
                            if (((info.Length == file2.OriginalSize) && (info.CreationTime.ToString("yyyy-MM-dd HH:mm:ss") == file2.FileCreateTime.ToString("yyyy-MM-dd HH:mm:ss"))) && (info.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") == file2.FileModifyTime.ToString("yyyy-MM-dd HH:mm:ss")))
                            {
                                continue;
                            }
                        }
                        FSClientUtil.DownloadFile(downType, file2.FileId, path);
                        FileInfo info2 = new FileInfo(path) {
                            CreationTime = file2.FileCreateTime,
                            LastWriteTime = file2.FileModifyTime
                        };
                    }
                }
            }
            return true;
        }

        public static DESecureFile EditFile(DESecureFile info, DEBusinessItem item)
        {
            string path = "";
            string desFileName = "";
            DESecureFile file = null;
            if (!info.InLocalHost)
            {
                string str3 = SelectSaveAsPath(info.FileName);
                if (str3 == null)
                {
                    return null;
                }
                if (!ClientData.OptShowSaveDialog)
                {
                    str3 = Path.Combine(str3, item.Id);
                    if (!Directory.Exists(str3))
                    {
                        Directory.CreateDirectory(str3);
                    }
                }
                try
                {
                    path = DownloadFileByFileName(item.Iteration, info, str3, ClientData.UserGlobalOption, true);
                    if (Path.GetExtension(path).ToUpper() == ".GXT")
                    {
                        string fileName = Path.GetFileNameWithoutExtension(info.FileName) + ".emf";
                        file = item.FileList.FindFile(fileName);
                        if (file != null)
                        {
                            desFileName = Path.Combine(Path.GetDirectoryName(str3), fileName);
                            desFileName = DownloadFileByFileName(item.Iteration, file, desFileName, ClientData.UserGlobalOption, true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "业务对象管理");
                    return null;
                }
                if ((path == "") || !System.IO.File.Exists(path))
                {
                    MessageBoxPLM.Show("该源文件不在当前计算机上，无法进行编辑！", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
                try
                {
                    PLLogger logger = new PLLogger();
                    DEProductConfigLog productObj = new DEProductConfigLog();
                    productObj.Description = productObj.Description + "下载（编辑）源文件到" + Dns.GetHostName();
                    productObj.ObjectOid = item.MasterOid;
                    productObj.ObjectType = ModelContext.MetaModel.GetClassLabel(item.ClassName);
                    productObj.Operation = ConstEnvironment.GetProductDateOperTypeName(ConstEnvironment.ProductDateOperType.DownLoad);
                    productObj.OperObjName = item.Id;
                    productObj.Operator = ClientData.LogonUser.LogId;
                    logger.CreateBoAccessObj(productObj);
                }
                catch
                {
                }
                try
                {
                    info.HostName = Dns.GetHostName().ToUpper();
                    info.Location = Path.GetDirectoryName(path);
                    info.FileName = Path.GetFileName(path);
                    info.FileType = UIFileType.GetFileType(path);
                    info.ItemMasterOid = item.MasterOid;
                    info.ItemRevNum = item.RevNum;
                    info.ItemClassName = item.ClassName;
                    PLItem.Agent.UpdateSecureFile(item.IterOid, item.ClassName, info, ClientData.LogonUser.Oid);
                    info.AcceptChange();
                    if (((Path.GetExtension(path).ToUpper() == ".GXT") && (file != null)) && System.IO.File.Exists(desFileName))
                    {
                        file.HostName = Dns.GetHostName().ToUpper();
                        file.Location = Path.GetDirectoryName(desFileName);
                        file.FileName = Path.GetFileName(desFileName);
                        file.FileType = UIFileType.GetFileType(desFileName);
                        file.ItemMasterOid = item.MasterOid;
                        file.ItemRevNum = item.RevNum;
                        file.ItemClassName = item.ClassName;
                        PLItem.Agent.UpdateSecureFile(item.IterOid, item.ClassName, file, ClientData.LogonUser.Oid);
                        file.AcceptChange();
                    }
                }
                catch (Exception exception2)
                {
                    PrintException.Print(exception2, MessageBoxIcon.Hand);
                    return null;
                }
                if (Path.GetExtension(path).ToUpper() == ".GXT")
                {
                    FrmViewFile.ViewFile(info, path, item.ClassName, null, false, item);
                    return info;
                }
                UIBrowser.OpenFileWithEditor(path, info.FileType);
                return info;
            }
            if (info.InCurrentHost)
            {
                string location = info.Location;
                if (location.EndsWith(@"\"))
                {
                    path = info.Location + info.FileName;
                }
                else
                {
                    path = info.Location + @"\" + info.FileName;
                }
                if (!System.IO.File.Exists(path))
                {
                    if (DialogResult.Yes == MessageBoxPLM.Show("在指定路径下没有找到该文件，无法编辑！可能是文件路径发生变化，或已被删除。是否尝试从服务器获取文件？", "编辑源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        try
                        {
                            DownLoadFile(item.Iteration, info, location, ClientData.UserGlobalOption, true, "ClaRel_EDIT", item);
                            goto Label_048A;
                        }
                        catch (Exception exception3)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception3.Message, "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;
                        }
                    }
                    return null;
                }
            }
            else
            {
                if (DialogResult.Yes == MessageBoxPLM.Show("文件存在于他人机器上。是否尝试从服务器获取文件？", "浏览源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string selectedPath = info.Location;
                    if (!Directory.Exists(selectedPath))
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog {
                            Description = "请选择文件存放路径。",
                            ShowNewFolderButton = true
                        };
                        if (dialog.ShowDialog() != DialogResult.OK)
                        {
                            return null;
                        }
                        selectedPath = dialog.SelectedPath;
                    }
                    try
                    {
                        DownLoadFile(item.Iteration, info, selectedPath, ClientData.UserGlobalOption, true, "ClaRel_EDIT", item);
                        if (!selectedPath.EndsWith(@"\"))
                        {
                            path = selectedPath + @"\" + info.FileName;
                        }
                        else
                        {
                            path = selectedPath + info.FileName;
                        }
                        goto Label_048A;
                    }
                    catch (Exception exception4)
                    {
                        MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception4.Message, "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                return null;
            }
        Label_048A:
            if (!System.IO.File.Exists(path))
            {
                MessageBoxPLM.Show("该源文件不在当前计算机上，无法进行编辑！", "编辑源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            if (Path.GetExtension(path).ToUpper() == ".GXT")
            {
                FrmViewFile.ViewFile(info, path, item.ClassName, null, false, item);
                return info;
            }
            UIBrowser.OpenFileWithEditor(path, info.FileType);
            return info;
        }

        [DllImport("lfsofm43.dll")]
        public static extern int FileCode(string pathOfFile);
        public string GetFileName(Guid fileOid)
        {
            ArrayList files = PLFileService.Agent.GetFiles(fileOid);
            if (files.Count > 0)
            {
                return (files[0] as DEFile).FileName;
            }
            return null;
        }

        public static int GetHighestPriorityFileType(DESecureFileList files)
        {
            Guid[] fileTypesPriority = ClientData.GetFileTypesPriority();
            int num = files.Count - 1;
            if (ClientData.OptDefaultOpenFileSort == OpenFileSort.Last)
            {
                num = files.Count - 1;
                foreach (Guid guid in fileTypesPriority)
                {
                    for (int i = files.Count - 1; i >= 0; i--)
                    {
                        DESecureFile file = files[i] as DESecureFile;
                        if ((file != null) && (file.FileType == guid))
                        {
                            return i;
                        }
                    }
                }
                return num;
            }
            if (ClientData.OptDefaultOpenFileSort == OpenFileSort.First)
            {
                num = 0;
                foreach (Guid guid2 in fileTypesPriority)
                {
                    for (int j = 0; j < files.Count; j++)
                    {
                        DESecureFile file2 = files[j] as DESecureFile;
                        if ((file2 != null) && (file2.FileType == guid2))
                        {
                            return j;
                        }
                    }
                }
            }
            return num;
        }

        public static DESecureFile GetRelationFileForBrowse(DEObjectAttachFile item, DESecureFile info)
        {
            if ((info == null) || (info.FileType == Guid.Empty))
            {
                return null;
            }
            DEFileOperRule[] fileOperRulesBySrcFileTypeOid = PLFileOperRule.RemotingAgent.GetFileOperRulesBySrcFileTypeOid(info.FileType);
            if (fileOperRulesBySrcFileTypeOid == null)
            {
                return null;
            }
            Guid empty = Guid.Empty;
            DEFileOperRule rule = null;
            foreach (DEFileOperRule rule2 in fileOperRulesBySrcFileTypeOid)
            {
                if (rule2.Oper == FileConvertOperTye.Browser)
                {
                    empty = rule2.TagFileTypeOid;
                    rule = rule2;
                    break;
                }
            }
            if (rule == null)
            {
                return null;
            }
            Thyt.TiPLM.PLL.Environment.PLFileType type = new Thyt.TiPLM.PLL.Environment.PLFileType();
            DEFileType fileType = type.GetFileType(info.FileType);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(info.FileName);
            if (fileType.WithVersion)
            {
                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);
            }
            DESecureFile file = null;
            foreach (DESecureFile file2 in item.FileList)
            {
                if ((file2.State != FileState.Deleted) && (file2.FileType == empty))
                {
                    DEFileType type3 = type.GetFileType(file2.FileType);
                    string path = Path.GetFileNameWithoutExtension(file2.FileName);
                    if (type3.WithVersion)
                    {
                        path = Path.GetFileNameWithoutExtension(path);
                    }
                    if (path == fileNameWithoutExtension)
                    {
                        file = file2;
                        break;
                    }
                }
            }
            if (((rule == null) || rule.IsDepend) || ((file != null) && (((file == null) || file.IsConvertVaild) || info.InLocalHost)))
            {
                return file;
            }
            DEFileConvert[] fileConverts = null;
            if (FileConvertTaskManager.instance.DoneFileConvertTask(item.Oid, info.FileOid, info.FileName, item.ClassName, info.FileType, 1, FileConvertOperTye.Browser, ClientData.LogonUser.LogId, rule.Oid, out fileConverts) != FileConvertTaskManager.ConvertResult.Sucess)
            {
                return null;
            }
            if ((fileConverts == null) || (fileConverts.Length == 0))
            {
                return null;
            }
            DESecureFile[] fileArray = PLItem.Agent.GetSecureFilesByFileOid(fileConverts[0].ConvertFileOid, ClientData.LogonUser.Oid, false);
            if ((fileArray == null) || (fileArray.Length == 0))
            {
                return null;
            }
            DEBusinessItem item2 = PLItem.Agent.GetBizItemByIteration(item.Oid, item.ClassName, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
            if ((BizItemHandlerEvent.Instance.D_RefreshBizItemFileList != null) && (item2 != null))
            {
                BizItemHandlerEvent.Instance.D_RefreshBizItemFileList(ConvertPLMBizItemDelegateParam(item2));
            }
            return fileArray[0];
        }

        public string GetTempMarkupPath()
        {
            string tempPath = Path.Combine(ConstConfig.GetTempfilePath() + "markup", Guid.NewGuid().ToString());
            tempPath = PLSecurityDisk.Agent.ConvertPathToSecurity(tempPath);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            new DirectoryInfo(tempPath).Attributes = FileAttributes.Hidden;
            return tempPath;
        }

        public BrowserDisplayRule GetVisibleDisplayRule(){
           return new BrowserDisplayRule { 
                ShowToolBars = false,
                ShowScrollBars = false,
                ShowStatusBar = false,
                EnablePopupMenu = false,
                OnlyBrowser = true,
                IsEdit = false
            };
        }
        public static void MarkupFile(DEObjectAttachFile item, DESecureFile file, DELProcessInfoForCLT processArg, string className, DEPSOption option)
        {
            DEBusinessItem item2 = null;
            try
            {
                FileBrowseWay way;
                string fileName = null;
                if (file.InCurrentHost)
                {
                    fileName = file.Location + @"\" + file.FileName;
                }
                else
                {
                    fileName = DownloadFileByDir(item, file, null, option, false, true, "ClaRel_BROWSE");
                    PLLogger logger = new PLLogger();
                    DEProductConfigLog productObj = new DEProductConfigLog {
                        Description = "下载批注源文件到" + Dns.GetHostName()
                    };
                    item2 = (DEBusinessItem) PLItem.Agent.GetBizItemByIteration(item.Oid, item.ClassName, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                    productObj.ObjectOid = item2.Master.Oid;
                    productObj.ObjectType = ModelContext.MetaModel.GetClassLabel(item2.Master.ClassName);
                    productObj.OperObjName = item2.Master.Id;
                    productObj.Operator = ClientData.LogonUser.LogId;
                    productObj.Operation = ConstEnvironment.GetProductDateOperTypeName(ConstEnvironment.ProductDateOperType.DownLoad);
                    logger.CreateBoAccessObj(productObj);
                }
                DEBrowser browser = null;
                try
                {
                    way = UIBrowser.GetMarkupTool(file.FileOid, file.FileName, file.FileType, out browser);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception, "业务对象管理");
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
                            FrmMarkupByBrowser.MarkUp(fileName, file.FileOid, processArg.ProcessInstanceOid, processArg.WorkItemOid, processArg.GroupDataOid, ClientData.LogonUser.Oid, className, browser2, item2.IterOid, item2.LastIteration);
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
            catch (PLMException exception3)
            {
                PrintException.Print(exception3, "业务对象管理");
            }
            catch (Exception exception4)
            {
                PrintException.Print(0x7b5, exception4);
            }
        }

        public static string SelectDownloadDirectory()
        {
            if (DownloadFolderDlg == null)
            {
                DownloadFolderDlg = new FolderBrowserDialog();
            }
            DownloadFolderDlg.ShowNewFolderButton = true;
            if (Directory.Exists(ClientData.OptEditWorkingPath))
            {
                DownloadFolderDlg.SelectedPath = ClientData.OptEditWorkingPath;
            }
            if (DownloadFolderDlg.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            return DownloadFolderDlg.SelectedPath;
        }

        public static string SelectSaveAsPath(string fileName)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (Directory.Exists(ClientData.OptEditWorkingPath))
            {
                if (!ClientData.OptShowSaveDialog)
                {
                    return ClientData.OptEditWorkingPath;
                }
                dialog.InitialDirectory = ClientData.OptEditWorkingPath;
            }
            dialog.FileName = fileName;
            dialog.Title = "选择保存文件的路径";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }
            string directoryName = Path.GetDirectoryName(dialog.FileName);
            ClientData.OptEditWorkingPath = directoryName;
            return directoryName;
        }

        public static void UpdateFileBeforeSave(DEObjectAttachFile item, Guid userOid)
        {
            if (item.HasFile)
            {
                DESecureFileList fileList = item.FileList;
                string path = "";
                foreach (DESecureFile file in fileList)
                {
                    if ((file.State != FileState.Deleted) && file.InLocalHost)
                    {
                        if (!file.InCurrentHost)
                        {
                            throw new PLMException(0x7ac);
                        }
                        path = file.Location + @"\" + file.FileName;
                        if (!System.IO.File.Exists(path))
                        {
                            throw new PLMException(0x7ac);
                        }
                        if (PLFileService.FileChanged(file.FileOid, path))
                        {
                            file.FileOid = Guid.NewGuid();
                            FSClientUtil.UploadSystemFile(file.FileOid, path, item.ClassName, ClientData.LogonUser.Name);
                        }
                        try
                        {
                            file.HostName = (string) (file.Location = null);
                            PLItem.Agent.UpdateSecureFile(item.Oid, item.ClassName, file, ClientData.LogonUser.Oid);
                            file.AcceptChange();
                        }
                        catch (Exception exception)
                        {
                            file.FileOid = Guid.NewGuid();
                            throw exception;
                        }
                    }
                }
            }
        }

        public void ViewDefaultFile(IBizItem tmp, DELProcessInfoForCLT processArgs, DEPSOption option)
        {
            if (tmp != null)
            {
                DEBusinessItem bizItem = tmp as DEBusinessItem;
                if (bizItem == null)
                {
                    try
                    {
                        bizItem = PLItem.Agent.GetBizItem(tmp.MasterOid, tmp.RevNum, tmp.IterNum, option.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                    }
                    catch (Exception exception)
                    {
                        PrintException.Print(exception);
                        return;
                    }
                }
                if (bizItem.Iteration.HasFile)
                {
                    int highestPriorityFileType = GetHighestPriorityFileType(bizItem.Iteration.FileList);
                    DESecureFile info = bizItem.Iteration.FileList[highestPriorityFileType] as DESecureFile;
                    this.ViewDefaultFile(bizItem.Iteration, bizItem.CanEdit(ClientData.LogonUser.Oid), info, processArgs, null, option, false, bizItem);
                }
            }
        }

        public void ViewDefaultFile(DEObjectAttachFile item, bool canEdit, DESecureFile info, DELProcessInfoForCLT processArgs, Control container, DEPSOption option, bool isCompare, IBizItem bizItem)
        {
            this.ViewFile(item, canEdit, info, processArgs, container, option, isCompare, bizItem, null, null, true);
        }

        public static void ViewFile(Guid fileOid, string fileName)
        {
            string str = null;
            try
            {
                str = FSClientUtil.DownloadFile(fileOid, "ClaRel_BROWSE");
            }
            catch (Exception exception)
            {
                MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FrmViewFile.ViewFile(fileOid, str, Guid.Empty, true);
        }

        public string ViewFile(DEObjectAttachFile item, bool canEdit, DESecureFile info, DELProcessInfoForCLT processArg, Control container, DEPSOption option, bool isCompare, IBizItem bizItem, DEBrowser browser, BrowserDisplayRule rule, bool isShowFileList)
        {
            string str;
            string str3;
            if (item != null)
            {
                if (item.FileList.GetFileByFileOid(info.FileOid) == null)
                {
                    return null;
                }
                DESecureFile relationFileForBrowse = GetRelationFileForBrowse(item, info);
                if (relationFileForBrowse != null)
                {
                    info = relationFileForBrowse;
                }
                ArrayList files = PLFileService.Agent.GetFiles(info.FileOid);
                if ((files != null) && (files.Count > 0))
                {
                    DEFile file2 = files[0] as DEFile;
                    if (file2 != null)
                    {
                        int num = Convert.ToInt32((long) (file2.OriginalSize / 0x100000L));
                        if ((num > 50) && (MessageBoxPLM.Show("您当前要浏览的源文件为" + num.ToString() + "M，浏览会花费较长时间，您确认要继续吗？", "浏览源文件", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Cancel))
                        {
                            return null;
                        }
                    }
                }
                if (bizItem != null)
                {
                    switch (PLGrantPerm.Agent.CanDoObjectOperation(ClientData.LogonUser.Oid, bizItem.MasterOid, bizItem.ClassName, PLGrantPerm.ToPermString(PLMBOOperation.BOView), bizItem.SecurityLevel, bizItem.Phase, bizItem.RevNum))
                    {
                        case 0:
                            MessageBoxPLM.Show("您没有浏览该对象源文件的权限。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;

                        case 2:
                            MessageBoxPLM.Show("当前对象在流程中，您没有在流程中浏览该对象源文件的权限。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;
                    }
                }
                str = string.Empty;
                string path = string.Empty;
                if (FileEvent.fileOperEvent != null)
                {
                    FileEvent.fileOperEvent(OperEventType.Before, "ClaRel_BROWSE", info.FileOid);
                }
                if (!info.InLocalHost)
                {
                    goto Label_0402;
                }
                if (info.InCurrentHost)
                {
                    path = info.Location;
                    str = info.Location + @"\" + info.FileName;
                    if (!System.IO.File.Exists(str))
                    {
                        if (DialogResult.Yes == MessageBoxPLM.Show("在指定路径下没有找到该文件，无法浏览！可能是文件路径发生变化，或已被删除。是否尝试从服务器获取文件？", "浏览源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            try
                            {
                                DownLoadFile(item, info, path, option, canEdit, "ClaRel_BROWSE", bizItem);
                                goto Label_0334;
                            }
                            catch (Exception exception)
                            {
                                if (canEdit)
                                {
                                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                {
                                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                return null;
                            }
                        }
                        return null;
                    }
                    try
                    {
                        if (ClientData.LogonUser.Oid != info.UserOid)
                        {
                            str = DownLoadFile(item, info, null, option, true, "ClaRel_BROWSE", bizItem);
                        }
                        if (!System.IO.File.Exists(str))
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：源文件未上载", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;
                        }
                        goto Label_0334;
                    }
                    catch (Exception exception2)
                    {
                        if (canEdit)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception2.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception2.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return null;
                    }
                }
                if (DialogResult.Yes == MessageBoxPLM.Show("文件存在于他人机器上。是否尝试从服务器获取文件？", "浏览源文件", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        str = DownLoadFile(item, info, null, option, canEdit, "ClaRel_BROWSE", bizItem);
                        if (str == null)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：文件未上载", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return null;
                        }
                        goto Label_0334;
                    }
                    catch (Exception exception3)
                    {
                        if (canEdit)
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception3.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception3.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        return null;
                    }
                }
            }
            return null;
        Label_0334:
            Cursor.Current = Cursors.WaitCursor;
            ProgressBarCallback.SetText("浏览文件" + Path.GetFileName(str));
            ProgressBarCallback.BeginAutoMove(2);
            try
            {
                Guid fileType = info.FileType;
                if (container != null)
                {
                    UIBrowser.OpenFileWithBrowser(info.FileOid, str, info.FileType, container, null, isCompare, bizItem, browser, rule);
                }
                else
                {
                    bool flag = false;
                    if (Path.GetExtension(str).ToUpper() == ".GXT")
                    {
                        FrmViewFile.IsEditGxt = false;
                        FrmViewFile.IsOnlyBrowser = true;
                        flag = true;
                    }
                    FrmViewFile.ViewFile(info, str, item.ClassName, flag ? null : processArg, false, bizItem, browser, isShowFileList);
                }
                if (FileEvent.fileOperEvent != null)
                {
                    FileEvent.fileOperEvent(OperEventType.After, "ClaRel_BROWSE", info.FileOid);
                }
                return null;
            }
            finally
            {
                ProgressBarCallback.End();
                Cursor.Current = Cursors.Default;
            }
        Label_0402:
            str3 = null;
            try
            {
                if (item.ClassName == null)
                {
                    string tempFilePath = FSClientUtil.GetTempFilePath(info.FileOid);
                    string tempPath = Path.Combine(tempFilePath, info.FileName);
                    if ((PLSystemParam.SecurityPath != "") && !tempPath.StartsWith(PLSystemParam.SecurityPath))
                    {
                        tempFilePath = PLSecurityDisk.Agent.ConvertPathToSecurity(tempFilePath);
                        if (!Directory.Exists(tempFilePath))
                        {
                            Directory.CreateDirectory(tempFilePath);
                        }
                        tempPath = PLSecurityDisk.Agent.ConvertPathToSecurity(tempPath);
                    }
                    str3 = FSClientUtil.DownloadFile("ClaRel_BROWSE", info.FileOid, tempFilePath);
                }
                else
                {
                    DEBusinessItem item2 = PLItem.Agent.GetBizItemByIteration(item.Oid, item.ClassName, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                    str3 = DownloadFileByDir(item2.Iteration, info, null, option, false, true, "ClaRel_BROWSE");
                }
            }
            catch (Exception exception4)
            {
                if (canEdit)
                {
                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception4.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBoxPLM.Show("无法从文件服务器获取源文件，原因是：" + exception4.Message, "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return null;
            }
            switch (str3)
            {
                case null:
                case "":
                    return null;
            }
            if (!System.IO.File.Exists(str3))
            {
                MessageBoxPLM.Show("指定的文件索引不存在，可能是文件没有正确上载。", "浏览源文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            Cursor.Current = Cursors.WaitCursor;
            ProgressBarCallback.SetText("浏览文件" + Path.GetFileName(str3));
            ProgressBarCallback.BeginAutoMove(2);
            try
            {
                if (container != null)
                {
                    UIBrowser.OpenFileWithBrowser(info.FileOid, str3, info.FileType, container, Path.GetDirectoryName(str3), false, bizItem, browser, rule);
                }
                else
                {
                    bool flag2 = false;
                    if (Path.GetExtension(str3).ToUpper() == ".GXT")
                    {
                        FrmViewFile.IsEditGxt = false;
                        FrmViewFile.IsOnlyBrowser = true;
                        flag2 = true;
                    }
                    FrmViewFile.ViewFile(info, str3, item.ClassName, flag2 ? null : processArg, true, bizItem, browser, isShowFileList);
                }
                if (FileEvent.fileOperEvent != null)
                {
                    FileEvent.fileOperEvent(OperEventType.After, "ClaRel_BROWSE", info.FileOid);
                }
            }
            catch (Exception exception5)
            {
                PrintException.Print(exception5);
            }
            finally
            {
                ProgressBarCallback.End();
                Cursor.Current = Cursors.Default;
            }
            return str3;
        }

        public static void ViewFileMarkup(DEObjectAttachFile item, DESecureFile file, string className, DEPSOption option, bool isMulti)
        {
            try
            {
                string fileName = null;
                if ((file.Location != null) && (file.Location != ""))
                {
                    fileName = file.Location + @"\" + file.FileName;
                }
                else
                {
                    fileName = DownloadFileByDir(item, file, null, option, false, true, "ClaRel_BROWSE");
                }
                if (!isMulti)
                {
                    FrmViewAllMarkups.ViewAllMarkups(fileName, file.FileOid, className, false, file, null);
                }
                else
                {
                    FrmViewAllMarkups.ViewAllMarkups(fileName, file.FileOid, className, false, file, item);
                }
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception, "业务对象管理");
            }
            catch (Exception exception2)
            {
                PrintException.Print(0x7b6, exception2);
            }
        }

        private class ConvertToTree
        {
            private bool expand;

            public ConvertToTree(bool expand)
            {
                this.expand = expand;
            }

            public WTreeNode ConvertToTreeStructure(object root)
            {
                WTreeNode parent = new WTreeNode {
                    Tag = root
                };
                this.CreateRecusiveTreeNode(parent, root);
                return parent;
            }

            private void CreateRecusiveTreeNode(WTreeNode parent, object element)
            {
                object[] children = this.GetChildren(element);
                for (int i = 0; i < children.Length; i++)
                {
                    this.CreateTreeNode(parent, children[i], -1);
                }
                for (int j = 0; j < parent.Nodes.Count; j++)
                {
                    this.CreateRecusiveTreeNode(parent.Nodes[j], parent.Nodes[j].Tag);
                }
            }

            private void CreateTreeNode(WTreeNode parent, object element, int index)
            {
                WTreeNode node = this.NewTreeNode(parent, 0, index);
                this.UpdateItem(node, element);
            }

            private object[] GetChildren(object parentElement)
            {
                try
                {
                    return this.GetRawChildren(parentElement);
                }
                catch
                {
                    return new object[0];
                }
            }

            private int GetNodeLevel(WTreeNode node)
            {
                int num = 0;
                for (WTreeNode node2 = node.Parent; node2 != null; node2 = node2.Parent)
                {
                    num++;
                }
                return num;
            }

            protected virtual object[] GetRawChildren(object parentElement)
            {
                if (parentElement is DEFolder2)
                {
                    DEFolder2 folder = (DEFolder2) parentElement;
                    ArrayList list = new ArrayList();
                    if (this.expand)
                    {
                        ArrayList subFolders = PLFolder.RemotingAgent.GetSubFolders(ClientData.LogonUser.Oid, folder.Oid);
                        list.AddRange(subFolders);
                    }
                    ArrayList c = PLItem.Agent.GetShortCuts(folder.Oid, ClientData.LogonUser.Oid, false, BizItemMode.SmartBizItem);
                    list.AddRange(c);
                    return list.ToArray();
                }
                if (parentElement is string)
                {
                    switch (((string) parentElement))
                    {
                        case "个人文件夹":
                            return PLFolder.RemotingAgent.GetTopFolders(ClientData.LogonUser.Oid, false).ToArray();

                        case "公共文件夹":
                            return PLFolder.RemotingAgent.GetTopFolders(ClientData.LogonUser.Oid, true).ToArray();
                    }
                }
                return new object[0];
            }

            protected WTreeNode NewTreeNode(WTreeNode parent, int style, int ix)
            {
                WTreeNode node;
                if (ix >= 0)
                {
                    node = new WTreeNode();
                    parent.Nodes.Insert(ix, node);
                    return node;
                }
                node = new WTreeNode();
                parent.Nodes.Add(node);
                return node;
            }

            protected void UpdateItem(WTreeNode node, object element)
            {
                node.Tag = element;
            }
        }

        public class FileIndexLoader
        {
            private bool expand;
            private object input;
            private WTreeNode root;

            public FileIndexLoader(object input, bool expand)
            {
                this.input = input;
                this.expand = expand;
            }

            public WTreeNode GetRoot() {
                return this.root;
            }
            public void Run()
            {
                this.root = new ViewFileHelper.ConvertToTree(this.expand).ConvertToTreeStructure(this.input);
            }
        }
    }
}

