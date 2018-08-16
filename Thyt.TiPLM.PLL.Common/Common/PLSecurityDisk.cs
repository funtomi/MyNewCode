namespace Thyt.TiPLM.PLL.Common
{
    using CheckLib;
    using SecurityLib;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.PLL.Environment;

    public class PLSecurityDisk {
        private CheckComClass checkCls;
        private SecuritiesClass securityCls;

        public PLSecurityDisk() {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                this.checkCls = new CheckComClass();
                this.securityCls = new SecuritiesClass();
            }
        }

        private void AddCurProcess(int curpid) {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                File.AppendAllText(Environment.SystemDirectory + @"\TiCFPLMSetting.ini", curpid.ToString() + "\r\n");
            }
        }

        public void AddLicensePro(int curpid) {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                File.AppendAllText(Environment.SystemDirectory + @"\TiCFLicence.ini", curpid.ToString() + "\r\n");
                this.SetProcessId();
            }
        }

        public void CloseSecurityDisk() {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                string systemDirectory = Environment.SystemDirectory;
                this.RemoveCurProcess(Process.GetCurrentProcess().Id);
                if (File.ReadAllLines(systemDirectory + @"\TiCFPLMSetting.ini").Length == 0) {
                    this.securityCls.CloseDisk();
                    this.checkCls.ActiveSetIni(0);
                }
            }
        }

        public string ConvertPathToSecurity(string tempPath) {
            if ((PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) && (PLSystemParam.SecurityPath.Trim() != "")) {
                tempPath = PLSystemParam.SecurityPath + tempPath.Substring(1, tempPath.Length - 1);
            }
            return tempPath;
        }

        public string CopyToTemp(string securityPath) {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                string tempfilePath = ConstConfig.GetTempfilePath();
                if (!File.Exists(securityPath)) {
                    return securityPath;
                }
                securityPath = tempfilePath.Substring(0, 1) + securityPath.Substring(1, securityPath.Length - 1);
                if (!Directory.Exists(Path.GetDirectoryName(securityPath))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(securityPath));
                }
                try {
                    File.Copy(securityPath, tempfilePath.Substring(0, 1) + securityPath.Substring(1, securityPath.Length - 1), true);
                } catch {
                }
            }
            return securityPath;
        }

        public void LoadSecurityDisk() {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                if (!File.Exists(Environment.SystemDirectory + @"\TiCFSetting.ini")) {
                    this.checkCls.CreateIniFile("", "");
                    this.securityCls.InitCondition("", "T");
                    new FolderBrowserDialog();
                    if (this.securityCls.CreateSafeDiskFile("T", Convert.ToUInt32(PLSystemParam.SecurityDiskSize), @"D:\") == 1) {
                        throw new PLMException("创建安全磁盘失败，请联系系统管理员！");
                    }
                } else {
                    this.securityCls.CloseDisk();
                    this.checkCls.ActiveSetIni(0);
                }
                this.securityCls.InitCondition("", "T");
                this.securityCls.LoadDisk();
                string disk = "";
                this.securityCls.GetTDiskInfo(out disk);
                string str3 = disk.Split(new char[] { '|' }, 10)[0];
                PLSystemParam.SecurityPath = str3;
                if ((str3.Trim() == "") || (str3 == "待解密字符长度必须为16的倍数")) {
                    throw new PLMException("加载安全磁盘失败，请联系系统管理员！");
                }
                this.AddCurProcess(Process.GetCurrentProcess().Id);
                this.AddLicensePro(Process.GetCurrentProcess().Id);
            }
        }

        private void RemoveCurProcess(int curpid) {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                string systemDirectory = Environment.SystemDirectory;
                if (File.Exists(systemDirectory + @"\TiCFPLMSetting.ini")) {
                    string[] strArray = File.ReadAllLines(systemDirectory + @"\TiCFPLMSetting.ini");
                    ArrayList list = new ArrayList();
                    foreach (string str2 in strArray) {
                        bool flag = false;
                        foreach (Process process in Process.GetProcesses()) {
                            if (str2 == process.Id.ToString()) {
                                flag = true;
                            }
                        }
                        if ((str2 != curpid.ToString()) && flag) {
                            list.Add(str2);
                        }
                    }
                    File.Delete(systemDirectory + @"\TiCFPLMSetting.ini");
                    File.WriteAllLines(systemDirectory + @"\TiCFPLMSetting.ini", (string[])list.ToArray(typeof(string)));
                }
            }
        }

        private void RemoveLicenesePro() {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                string systemDirectory = Environment.SystemDirectory;
                if (File.Exists(systemDirectory + @"\TiCFLicence.ini")) {
                    string[] strArray = File.ReadAllLines(systemDirectory + @"\TiCFLicence.ini");
                    ArrayList list = new ArrayList();
                    foreach (string str2 in strArray) {
                        bool flag = false;
                        foreach (Process process in Process.GetProcesses()) {
                            if (str2 == process.Id.ToString()) {
                                flag = true;
                            }
                        }
                        if (flag) {
                            list.Add(str2);
                        }
                    }
                    File.Delete(systemDirectory + @"\TiCFLicence.ini");
                    File.WriteAllLines(systemDirectory + @"\TiCFLicence.ini", (string[])list.ToArray(typeof(string)));
                }
            }
        }

        private void SetProcessId() {
            if (PLSystemParam.SecurityDiskIsUse && ConstCommon.FUNCTION_SECURITYDISK) {
                ArrayList list = new ArrayList();
                string systemDirectory = Environment.SystemDirectory;
                if (File.Exists(systemDirectory + @"\TiCFLicence.ini")) {
                    this.RemoveLicenesePro();
                    foreach (string str2 in File.ReadAllLines(systemDirectory + @"\TiCFLicence.ini")) {
                        list.Add((uint)Convert.ToInt32(str2));
                    }
                }
                this.checkCls.SetProcessID((uint[])list.ToArray(typeof(uint)), (uint)list.Count);
                this.checkCls.ActiveSetIni(1);
            }
        }

        public static PLSecurityDisk Agent {
            get {
                return new PLSecurityDisk();
            }
        }
    }
}

