namespace Thyt.TiPLM.UIL.Common.UserControl {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Xml.Serialization;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;

    public class UIThumbnailHelper {
        public static UIThumbnailHelper Instance = new UIThumbnailHelper();

        public string DownLoadThumFile(IBizItem bizItem, DEPSOption psOption) {
            List<Guid> list = new List<Guid>();
            List<string> list2 = new List<string>();
            list.Add(bizItem.IterOid);
            list2.Add(bizItem.ClassName);
            string fullPath = "";
            DEItemIteration2[] thumBizitemIters = null;
            DESecureFile[] files = null;
            PLItem.Agent.GetThumbnailFiles(list.ToArray(), list2.ToArray(), ClientData.LogonUser.Oid, (psOption == null) ? ClientData.UserGlobalOption.CloneAsLocal() : psOption, out thumBizitemIters, out files);
            new Dictionary<Guid, string>();
            for (int i = 0; i < list.Count; i++) {
                if (files[i] != null) {
                    bizItem.ThumBizitemIterOid = thumBizitemIters[i].Oid;
                    fullPath = ViewFileHelper.DownloadFileByDir(thumBizitemIters[i], files[i], null, psOption, true, false, "ClaRel_BROWSE");
                    if (string.IsNullOrEmpty(fullPath)) {
                        return fullPath;
                    }
                    string path = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                    if (!Directory.Exists(path)) {
                        Directory.CreateDirectory(path);
                    }
                    string destFileName = Path.Combine(path, files[i].FileOid.ToString() + ".jpg");
                    if (File.Exists(fullPath)) {
                        File.Copy(fullPath, destFileName, true);
                    } else if (!string.IsNullOrEmpty(files[i].FullPath) && File.Exists(files[i].FullPath)) {
                        fullPath = files[i].FullPath;
                    }
                }
            }
            return fullPath;
        }

        public Dictionary<Guid, string> DownLoadThumFiles(List<IBizItem> bizItems, DEPSOption psOption) {
            List<Guid> list = new List<Guid>();
            List<string> list2 = new List<string>();
            for (int i = 0; i < bizItems.Count; i++) {
                list.Add(bizItems[i].IterOid);
                list2.Add(bizItems[i].ClassName);
            }
            DEItemIteration2[] thumBizitemIters = null;
            DESecureFile[] files = null;
            PLItem.Agent.GetThumbnailFiles(list.ToArray(), list2.ToArray(), ClientData.LogonUser.Oid, (psOption == null) ? ClientData.UserGlobalOption.CloneAsLocal() : psOption, out thumBizitemIters, out files);
            Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
            for (int j = 0; j < list.Count; j++) {
                if (files[j] != null) {
                    bizItems[j].ThumBizitemIterOid = thumBizitemIters[j].Oid;
                    string str = ViewFileHelper.DownloadFileByDir(thumBizitemIters[j], files[j], null, psOption, true, true, "ClaRel_BROWSE");
                    if (!string.IsNullOrEmpty(str)) {
                        string path = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                        if (!Directory.Exists(path)) {
                            Directory.CreateDirectory(path);
                        }
                        string destFileName = Path.Combine(path, files[j].FileOid.ToString() + ".jpg");
                        if (File.Exists(str)) {
                            File.Copy(str, destFileName, true);
                            dictionary.Add(list[j], destFileName);
                        } else if (!string.IsNullOrEmpty(files[j].FullPath) && File.Exists(files[j].FullPath)) {
                            File.Copy(files[j].FullPath, destFileName, true);
                            dictionary.Add(list[j], destFileName);
                        }
                    }
                }
            }
            return dictionary;
        }

        private string GetConfigFileName(string location, ModelContext.ConfigFileAccess accessWay) {
            return
                ModelContext.GetConfigFilePath(ClientData.LogonUser.LogId, "Thum@" + location + ".xml", accessWay);
        }
        public Image GetReducedImage(string filePath) {
            try {
                Image image = Image.FromFile(filePath);
                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
                Image image2 = image.GetThumbnailImage(PLSystemParam.ParamThumWdith, PLSystemParam.ParamThumHeight, callback, IntPtr.Zero);
                GC.GetGeneration(GC.GetGeneration(image));
                image.Dispose();
                return image2;
            } catch (Exception) {
                return null;
            }
        }

        public ThumbnailSetting ReadSetting(string location) {
            string configFileName = this.GetConfigFileName(location, ModelContext.ConfigFileAccess.Read);
            ThumbnailSetting setting = new ThumbnailSetting();
            if (File.Exists(configFileName)) {
                FileStream stream = null;
                try {
                    XmlSerializer serializer = new XmlSerializer(typeof(ThumbnailSetting));
                    stream = new FileStream(configFileName, FileMode.Open, FileAccess.Read);
                    setting = (ThumbnailSetting)serializer.Deserialize(stream);
                } catch (Exception exception) {
                    PrintException.Print(exception);
                } finally {
                    if (stream != null) {
                        stream.Close();
                    }
                }
            }
            return setting;
        }

        public void SaveSetting(string location, ThumbnailSetting setting) {
            FileStream stream = null;
            try {
                string configFileName = this.GetConfigFileName(location, ModelContext.ConfigFileAccess.Write);
                if (File.Exists(configFileName)) {
                    File.Delete(configFileName);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(ThumbnailSetting));
                stream = new FileStream(configFileName, FileMode.Create, FileAccess.Write);
                serializer.Serialize((Stream)stream, setting);
                stream.Close();
            } catch (Exception exception) {
                PrintException.Print(exception);
            } finally {
                if (stream != null) {
                    stream.Close();
                }
            }
        }

        public bool ThumbnailCallback() {
            return false;
        }
    }
}

