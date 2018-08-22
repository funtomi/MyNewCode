using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Environment;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.Environment;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
using Thyt.TiPLM.UIL.Environment;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCThumbnail : UserControlPLM {
        public ShowThumFile AfterShowThumFile;
        private DEBusinessItem bizItem;
        private ToolStripButton btnOpenObject;
        private DESecureFile curFile;
        private DEBusinessItem curIbizItem;
        private int curIndex;
        private Dictionary<Guid, DEFileType> dic_FileTypes = new Dictionary<Guid, DEFileType>();
        private DEPSOption psOption;
        private List<DESecureFile> thumbnailFiles;
        private List<DEBusinessItem> thumbnailItems;
        private ThumbnailSetting thumSetting = new ThumbnailSetting();
        
        private List<DESecureFile> visualizationFiles;
        private List<DEBusinessItem> visualizationItems;

        public UCThumbnail(ThumbnailSetting thumSetting) {
            this.InitializeComponent();
            this.thumSetting = thumSetting;
            this.toolStrip1.Visible = false;
        }

        private void btnOpenObject_Click(object sender, EventArgs e) {
            try {
                if ((this.curIbizItem != null) && (BizItemHandlerEvent.Instance.D_OpenItem != null)) {
                    List<IBizItem> items = new List<IBizItem> {
                        this.curIbizItem
                    };
                    PLMOperationArgs args = new PLMOperationArgs(FrmLogon.PLMProduct.ToString(), PLMLocation.BPMList.ToString(), items, this.psOption, null, null, null, null);
                    BizItemHandlerEvent.Instance.D_OpenItem(null, args);
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private bool CompareEqualsFiles(List<DESecureFile> cur_visualizationFiles, List<DESecureFile> cur_thumbnailFiles) {
            if (this.GetListCount(this.visualizationFiles) != this.GetListCount(cur_visualizationFiles)) {
                return false;
            }
            foreach (DESecureFile file in cur_visualizationFiles) {
                bool flag = false;
                if (this.visualizationFiles != null) {
                    foreach (DESecureFile file2 in this.visualizationFiles) {
                        if (file2.FileOid.Equals(file.FileOid)) {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag) {
                    return false;
                }
            }
            if (this.GetListCount(this.thumbnailFiles) != this.GetListCount(cur_thumbnailFiles)) {
                return false;
            }
            foreach (DESecureFile file3 in cur_thumbnailFiles) {
                bool flag2 = false;
                if (this.thumbnailFiles != null) {
                    foreach (DESecureFile file4 in this.thumbnailFiles) {
                        if (file4.FileOid.Equals(file3.FileOid)) {
                            flag2 = true;
                            break;
                        }
                    }
                }
                if (!flag2) {
                    return false;
                }
            }
            return true;
        }

        public bool Display(object obizItem, DEPSOption psOption, ThumbnailSetting thumSetting) {
            if (obizItem is TreeNode) {
                obizItem = ((TreeNode)obizItem).Tag;
            }
            if (obizItem is DataRowView) {
                DataRowView view = (DataRowView)obizItem;
                obizItem = PLItem.Agent.GetBizItemByMaster(new Guid((byte[])view[0]), 0, psOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
            }
            if (!(obizItem is IBizItem)) {
                this.bizItem = null;
                this.InitFileRelaData();
                int generation = GC.GetGeneration(this.pnlViewer);
                BrowserPool.BrowserManager.RemoveBrowser(this.pnlViewer);
                this.pnlViewer.Controls.Clear();
                GC.Collect(generation);
                return (this.toolStrip1.Visible = false);
            }
            bool flag = false;
            if (this.bizItem == null) {
                flag = true;
            } else if (((IBizItem)obizItem).MasterOid != this.bizItem.MasterOid) {
                flag = true;
            }
            this.bizItem = PSConvert.ToBizItem(obizItem as IBizItem, psOption.CurView, ClientData.LogonUser.Oid);
            this.toolStrip1.Visible = true;
            this.psOption = psOption;
            this.thumSetting = thumSetting;
            bool flag2 = this.InitFiles();
            if (!flag) {
                flag = flag2;
            }
            if (!flag) {
                this.toolStrip1.Visible = (this.curFile != null) && this.thumSetting.IsShowToolBar;
                return (this.curFile != null);
            }
            this.SetShowTypeContent(this.thumSetting);
            this.ShowFile();
            this.toolStrip1.Visible = (this.curFile != null) && this.thumSetting.IsShowToolBar;
            return (this.curFile != null);
        }

        private int GetListCount(List<DESecureFile> list) {
            if ((list != null) && (list.Count != 0)) {
                return list.Count;
            }
            return 0;
        }

        private void GetShowFile() {
            this.curFile = null;
            this.curIbizItem = null;
            if (this.tspBtnShowType.Checked) {
                if ((this.thumbnailFiles != null) && (this.thumbnailFiles.Count > this.curIndex)) {
                    this.curFile = this.thumbnailFiles[this.curIndex];
                    this.curIbizItem = this.thumbnailItems[this.curIndex];
                }
            } else if ((this.visualizationFiles != null) && (this.visualizationItems.Count > this.curIndex)) {
                this.curFile = this.visualizationFiles[this.curIndex];
                this.curIbizItem = this.visualizationItems[this.curIndex];
            }
            this.SetPageBtnsVisible();
        }

        private void InitFileRelaData() {
            this.thumbnailFiles = new List<DESecureFile>();
            this.thumbnailItems = new List<DEBusinessItem>();
            this.visualizationFiles = new List<DESecureFile>();
            this.visualizationItems = new List<DEBusinessItem>();
            this.curIndex = 0;
            this.curIbizItem = null;
            this.curFile = null;
        }

        private bool InitFiles() {
            if (this.bizItem != null) {
                List<DESecureFile> list = new List<DESecureFile>();
                List<DESecureFile> list2 = new List<DESecureFile>();
                List<DEBusinessItem> list3 = new List<DEBusinessItem>();
                List<DEBusinessItem> list4 = new List<DEBusinessItem>();
                Dictionary<DESecureFile, DEBusinessItem> dictionary = PLItem.Agent.GetVisualizationFiles(this.bizItem.Iteration.Oid, this.bizItem.Iteration.ClassName, ClientData.LogonUser.Oid, this.psOption);
                if ((dictionary == null) || (dictionary.Count == 0)) {
                    this.InitFileRelaData();
                    return true;
                }
                DESecureFile[] array = new DESecureFile[dictionary.Count];
                dictionary.Keys.CopyTo(array, 0);
                PLFileType type = new PLFileType();
                for (int i = 0; i < array.Length; i++) {
                    DESecureFile item = array[i];
                    if (item.FileType != Guid.Empty) {
                        DEFileType fileType = null;
                        if (this.dic_FileTypes.ContainsKey(item.FileType)) {
                            fileType = this.dic_FileTypes[item.FileType];
                        } else {
                            fileType = type.GetFileType(item.FileType);
                            this.dic_FileTypes[item.FileType] = fileType;
                        }
                        if (fileType != null) {
                            if (fileType.CanThumbnail) {
                                list2.Add(item);
                                list4.Add(PSConvert.ToBizItem(dictionary[item], this.psOption.CurView, ClientData.LogonUser.Oid));
                            }
                            if (fileType.CanVisualization) {
                                list.Add(item);
                                list3.Add(PSConvert.ToBizItem(dictionary[item], this.psOption.CurView, ClientData.LogonUser.Oid));
                            }
                        }
                    }
                }
                if (!this.CompareEqualsFiles(list, list2)) {
                    this.InitFileRelaData();
                    this.thumbnailFiles = list2;
                    this.thumbnailItems = list4;
                    this.visualizationFiles = list;
                    this.visualizationItems = list3;
                    return true;
                }
            }
            return false;
        }

        private void PictureDoubleClick(object sender, EventArgs e) {
            if ((this.curIbizItem != null) && (this.curFile != null)) {
                ViewFileHelper.Instance.ViewFile(this.curIbizItem.Iteration, false, this.curFile, null, null, this.psOption, false, this.curIbizItem, null, null, true);
            }
        }

        private void SetPageBtnsVisible() {
            if (this.tspBtnShowType.Checked) {
                if (this.thumbnailFiles != null) {
                    if (this.thumbnailFiles.Count > 1) {
                        this.SetPageBtnsVisible(true);
                    } else {
                        this.SetPageBtnsVisible(false);
                    }
                }
            } else if (this.visualizationFiles != null) {
                if (this.visualizationFiles.Count > 1) {
                    this.SetPageBtnsVisible(true);
                } else {
                    this.SetPageBtnsVisible(false);
                }
            }
        }

        private void SetPageBtnsVisible(bool isVisible) {
            this.tspBtnFirst.Visible = isVisible;
            this.tspBtnLast.Visible = isVisible;
            this.tspBtnNext.Visible = isVisible;
            this.tspbtnPreview.Visible = isVisible;
            this.tspLabelPage.Visible = isVisible;
            this.toolStripSeparator2.Visible = isVisible;
        }

        private void SetPictureBoxSizeMode() {
            try {
                if ((base.Width >= 0) && (base.Height >= 0)) {
                    foreach (Control control in this.pnlViewer.Controls) {
                        if (control is PictureBox) {
                            ((PictureBox)control).SizeMode = PictureBoxSizeMode.Zoom;
                            ((PictureBox)control).Dock = DockStyle.Fill;
                            ((PictureBox)control).Dock = DockStyle.Fill;
                        }
                        if (control.Controls.Count > 0) {
                            foreach (Control control2 in control.Controls) {
                                if (control2 is PictureBox) {
                                    ((PictureBox)control2).SizeMode = PictureBoxSizeMode.Zoom;
                                    ((PictureBox)control2).Dock = DockStyle.Fill;
                                    ((PictureBox)control2).Dock = DockStyle.Fill;
                                }
                                if (control2.Controls.Count > 0) {
                                    foreach (Control control3 in control2.Controls) {
                                        if (control3 is PictureBox) {
                                            ((PictureBox)control3).SizeMode = PictureBoxSizeMode.Zoom;
                                            ((PictureBox)control3).Dock = DockStyle.Fill;
                                            ((PictureBox)control3).Dock = DockStyle.Fill;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } catch {
            }
        }

        private void SetPictureDoubleClickEvent() {
            foreach (Control control in this.pnlViewer.Controls) {
                control.DoubleClick -= new EventHandler(this.PictureDoubleClick);
                if (control.Controls.Count > 0) {
                    foreach (Control control2 in control.Controls) {
                        control2.DoubleClick -= new EventHandler(this.PictureDoubleClick);
                    }
                }
            }
            foreach (Control control3 in this.pnlViewer.Controls) {
                control3.DoubleClick += new EventHandler(this.PictureDoubleClick);
                if (control3.Controls.Count > 0) {
                    foreach (Control control4 in control3.Controls) {
                        control4.DoubleClick += new EventHandler(this.PictureDoubleClick);
                    }
                }
            }
        }

        public void SetShowTypeContent(ThumbnailSetting thumSetting) {
            this.thumSetting = thumSetting;
            PreviewStyle style = (PreviewStyle)Enum.Parse(typeof(PreviewStyle), ClientData.PreviewStyle);
            switch (style) {
                case PreviewStyle.Thumbnail:
                    this.tspBtnShowType.Checked = true;
                    this.tspBtnShowType.Text = "缩略图";
                    this.tspBtnShowType.Visible = false;
                    this.toolStripSeparator1.Visible = false;
                    break;

                case PreviewStyle.Visualization:
                    this.tspBtnShowType.Checked = false;
                    this.tspBtnShowType.Text = "可视化";
                    this.tspBtnShowType.Visible = false;
                    this.toolStripSeparator1.Visible = false;
                    break;
            }
            if (style == PreviewStyle.Thumbnail2Visualization) {
                if ((this.thumbnailFiles != null) && (this.thumbnailFiles.Count != 0)) {
                    this.tspBtnShowType.Checked = true;
                    this.tspBtnShowType.Text = "缩略图";
                    this.tspBtnShowType.Visible = (this.visualizationFiles != null) && (this.visualizationFiles.Count != 0);
                    this.toolStripSeparator1.Visible = true;
                } else if ((this.visualizationFiles != null) && (this.visualizationFiles.Count != 0)) {
                    this.tspBtnShowType.Checked = false;
                    this.tspBtnShowType.Text = "可视化";
                    this.tspBtnShowType.Visible = false;
                    this.toolStripSeparator1.Visible = false;
                }
            }
            if (((this.visualizationFiles == null) || (this.visualizationFiles.Count == 0)) && ((this.thumbnailFiles == null) || (this.thumbnailFiles.Count == 0))) {
                this.tspBtnShowType.Checked = true;
                this.tspBtnShowType.Text = "缩略图";
                this.tspBtnShowType.Visible = false;
                this.toolStripSeparator1.Visible = false;
            }
            this.SetPageBtnsVisible();
        }

        private void ShowFile() {
            this.GetShowFile();
            if (this.curFile == null) {
                int generation = GC.GetGeneration(this.pnlViewer);
                BrowserPool.BrowserManager.RemoveBrowser(this.pnlViewer);
                this.pnlViewer.Controls.Clear();
                GC.Collect(generation);
            } else {
                BrowserDisplayRule visibleDisplayRule = ViewFileHelper.Instance.GetVisibleDisplayRule();
                ViewFileHelper.Instance.ViewFile(this.curIbizItem.Iteration, false, this.curFile, null, this.pnlViewer, this.psOption, false, this.curIbizItem, null, visibleDisplayRule, false);
                this.SetPictureDoubleClickEvent();
                if (this.AfterShowThumFile != null) {
                    this.AfterShowThumFile(this, new ThumShowEventArgs(this.curIbizItem, this.curFile));
                }
                this.btnOpenObject.Text = this.curIbizItem.Id + "[" + ModelContext.MetaModel.GetClassLabel(this.curIbizItem.ClassName) + "]" + PSConvert.ToString(this.curIbizItem.ExactState);
                this.tspLabelPage.Text = this.tspBtnShowType.Checked ? string.Concat(new string[5]) : string.Concat(new string[5]);
            }
        }

        private void tspBtnFirst_Click(object sender, EventArgs e) {
            try {
                if (this.curIndex != 0) {
                    this.curIndex = 0;
                    this.ShowFile();
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void tspBtnLast_Click(object sender, EventArgs e) {
            try {
                if (this.tspBtnShowType.Checked) {
                    if (this.curIndex == (((this.thumbnailFiles != null) && (this.thumbnailFiles.Count > 0)) ? (this.thumbnailFiles.Count - 1) : 0)) {
                        return;
                    }
                    this.curIndex = ((this.thumbnailFiles != null) && (this.thumbnailFiles.Count > 0)) ? (this.thumbnailFiles.Count - 1) : 0;
                } else {
                    if (this.curIndex == (((this.visualizationFiles != null) && (this.visualizationFiles.Count > 0)) ? (this.visualizationFiles.Count - 1) : 0)) {
                        return;
                    }
                    this.curIndex = ((this.visualizationFiles != null) && (this.visualizationFiles.Count > 0)) ? (this.visualizationFiles.Count - 1) : 0;
                }
                this.ShowFile();
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void tspBtnNext_Click(object sender, EventArgs e) {
            try {
                if (this.tspBtnShowType.Checked) {
                    if ((this.thumbnailFiles == null) || (this.curIndex == (this.thumbnailFiles.Count - 1))) {
                        return;
                    }
                } else if ((this.visualizationFiles == null) || (this.curIndex == (this.visualizationFiles.Count - 1))) {
                    return;
                }
                this.curIndex++;
                this.ShowFile();
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void tspbtnPreview_Click(object sender, EventArgs e) {
            try {
                if (this.curIndex != 0) {
                    this.curIndex--;
                    this.ShowFile();
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void tspBtnShowType_Click(object sender, EventArgs e) {
            try {
                this.tspBtnShowType.Checked = !this.tspBtnShowType.Checked;
                this.tspBtnShowType.Text = this.tspBtnShowType.Checked ? "缩略图" : "可视化";
                this.curIndex = 0;
                this.curIbizItem = null;
                this.curFile = null;
                this.ShowFile();
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void UCThumbnail_Resize(object sender, EventArgs e) {
        }

        public delegate void ShowThumFile(object sender, ThumShowEventArgs args);
    }
}

