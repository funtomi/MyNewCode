    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Environment;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class ThumbnailListView : SortableListView {
        private ThumbnailFiller imageFiller;
        private bool isShowThumToolBar = true;
        public ImageList ThumImageList;
        private DEPSOption thumPSOption;
        private IThumTools uc_thumTool;

        public event ListViewScroll OnScroll;

        public ThumbnailListView() {
            this.IsFillDetailImage = false;
            base.KeyUp += new KeyEventHandler(this.OnKeyUp);
            base.SelectedIndexChanged += new EventHandler(this.OnItemClick);
        }

        public void AddThumCtrl(Control container, string location, bool isShowToolBar, bool usedBaseScall) {
            if (container != null) {
                container.Controls.Clear();
                this.uc_thumTool = new ThumToolsBaseCtrl(container, this, location, isShowToolBar);
                this.uc_thumTool.PreviewChanged += new PreviewChangedHandler(this.OnPreviewChanged);
                this.uc_thumTool.DisplayStyleChanged += new DisplayStyleChangedHandler(this.OnDisplayStyleChanged);
                if (this.ThumImageList == null) {
                    this.ThumImageList = new ImageList();
                    this.ThumImageList.ImageSize = new Size(PLSystemParam.ParamThumWdith, PLSystemParam.ParamThumHeight);
                }
                this.OnDisplayStyleChanged(this, new ThumToolsEventArgs(this.ThumSetting.IsPreview, this.ThumSetting.DisplayStyle));
                if (usedBaseScall) {
                    this.OnScroll += new ListViewScroll(this.OnScrollChanged);
                    base.Resize += new EventHandler(this.OnResize);
                }
                this.imageFiller = new ThumbnailFiller(this);
            }
        }

        public void DisplyListItemThum(ListViewItem lvItem) {
            if (((((lvItem != null) && (this.ThumSetting != null)) && (this.ThumSetting.DisplayStyle != DisplayStyle.Detail)) && ((base.Bounds.Bottom > lvItem.Bounds.Bottom) && (lvItem.Index >= base.TopItem.Index))) && (lvItem.Tag is IBizItem)) {
                IBizItem tag = lvItem.Tag as IBizItem;
                string path = UIThumbnailHelper.Instance.DownLoadThumFile(tag, this.ThumPSOption);
                string str2 = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                if (!Directory.Exists(str2)) {
                    Directory.CreateDirectory(str2);
                }
                string key = "";
                Image reducedImage = null;
                if (File.Exists(path)) {
                    key = Path.GetFileName(path);
                    reducedImage = UIThumbnailHelper.Instance.GetReducedImage(path);
                    if (reducedImage != null) {
                        if (this.ThumImageList.Images.ContainsKey(key)) {
                            int num = this.ThumImageList.Images.IndexOfKey(key);
                            this.ThumImageList.Images[num] = reducedImage;
                        } else {
                            this.ThumImageList.Images.Add(key, reducedImage);
                        }
                        reducedImage.Dispose();
                    }
                } else {
                    int objectImage = ClientData.ItemImages.GetObjectImage(tag.ClassName, PLDataModel.GetStateByMasterInfo(tag.ExactState, tag.HasFile));
                    if (!this.ThumImageList.Images.ContainsKey(objectImage.ToString())) {
                        string filename = Path.Combine(str2, objectImage.ToString() + ".jpg");
                        reducedImage = ClientData.ItemImages.GetObjectImage1(tag.ClassName, PLDataModel.GetStateByMasterInfo(tag.ExactState, tag.HasFile));
                        reducedImage.Save(filename);
                        this.ThumImageList.Images.Add(objectImage.ToString() + ".jpg", reducedImage);
                        reducedImage.Dispose();
                    }
                }
                lvItem.ImageIndex = this.ThumImageList.Images.IndexOfKey(key);
            }
        }

        protected override void Dispose(bool disposing) {
            base.SmallImageList = null;
            if (this.ThumImageList != null) {
                int generation = GC.GetGeneration(this.ThumImageList);
                this.ThumImageList.Images.Clear();
                GC.Collect(generation);
                this.ThumImageList.Dispose();
            }
            if ((this.uc_thumTool != null) && (this.uc_thumTool is ThumToolsBaseCtrl)) {
                ((ThumToolsBaseCtrl)this.uc_thumTool).Dispose();
            }
            if (this.imageFiller != null) {
                this.imageFiller.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void FillDetailImage() {
            try {
                Cursor.Current = Cursors.WaitCursor;
                if (base.SmallImageList == null) {
                    base.SmallImageList = ClientData.MyImageList.ImgList;
                }
                List<IBizItem> bizItems = new List<IBizItem>();
                ArrayList masterOids = new ArrayList();
                ArrayList revNums = new ArrayList();
                for (int i = base.TopItem.Index; i < base.Items.Count; i++) {
                    if ((base.Items[i] != null) && (base.Bounds.Bottom > base.Items[i].Bounds.Top)) {
                        ListViewItem item = base.Items[i];
                        IBizItem tag = null;
                        if (item.Tag is IBizItem) {
                            tag = item.Tag as IBizItem;
                            item.ImageIndex = this.GetColumnImageIndex(tag);
                        } else if (item.Tag is DataRowView) {
                            masterOids.Add(new Guid((byte[])((DataRowView)item.Tag)[0]));
                            revNums.Add(0);
                        }
                    }
                }
                if (masterOids.Count > 0) {
                    ArrayList list4 = PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, this.ThumPSOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                    if (list4.Count > 0) {
                        bizItems.AddRange((IBizItem[])list4.ToArray(typeof(IBizItem)));
                    }
                }
                if (bizItems.Count != 0) {
                    for (int j = base.TopItem.Index; j < base.Items.Count; j++) {
                        if ((base.Items[j] != null) && (base.Bounds.Bottom > base.Items[j].Bounds.Top)) {
                            ListViewItem item3 = base.Items[j];
                            IBizItem item4 = null;
                            if (item3.Tag is DataRowView) {
                                Guid masterOid = new Guid((byte[])((DataRowView)item3.Tag)[0]);
                                item4 = FindBizItems(bizItems, masterOid);
                                item3.ImageIndex = this.GetColumnImageIndex(item4);
                            }
                        }
                    }
                }
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        protected virtual void FillItemImage() {
            try {
                try {
                    if ((!base.Visible || (base.TopItem == null)) || (base.TopItem.Index == -1)) {
                        return;
                    }
                } catch {
                    return;
                }
                if ((this.uc_thumTool != null) && this.isShowThumToolBar) {
                    if (this.uc_thumTool.ThumSetting.DisplayStyle == DisplayStyle.Detail) {
                        if (this.IsFillDetailImage) {
                            this.FillDetailImage();
                        }
                    } else {
                        Cursor.Current = Cursors.WaitCursor;
                        if ((base.SmallImageList == null) || (base.SmallImageList != this.ThumImageList)) {
                            base.SmallImageList = this.ThumImageList;
                        }
                        int generation = GC.GetGeneration(this.ThumImageList);
                        List<IBizItem> bizItems = new List<IBizItem>();
                        ArrayList masterOids = new ArrayList();
                        ArrayList revNums = new ArrayList();
                        for (int i = base.TopItem.Index; i < base.Items.Count; i++) {
                            if ((base.Items[i] != null) && (base.Bounds.Bottom > base.Items[i].Bounds.Top)) {
                                ListViewItem item = base.Items[i];
                                IBizItem tag = null;
                                if (item.Tag is IBizItem) {
                                    tag = item.Tag as IBizItem;
                                } else if (item.Tag is DataRowView) {
                                    masterOids.Add(new Guid((byte[])((DataRowView)item.Tag)[0]));
                                    revNums.Add(0);
                                }
                                if (tag != null) {
                                    bizItems.Add(tag);
                                }
                            }
                        }
                        if (masterOids.Count > 0) {
                            ArrayList list4 = PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, this.ThumPSOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                            if (list4.Count > 0) {
                                bizItems.AddRange((IBizItem[])list4.ToArray(typeof(IBizItem)));
                            }
                        }
                        Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
                        if (bizItems.Count > 0) {
                            try {
                                dictionary = UIThumbnailHelper.Instance.DownLoadThumFiles(bizItems, this.ThumPSOption);
                            } catch (Exception exception) {
                                PLMEventLog.WriteExceptionLog(exception);
                            }
                        }
                        for (int j = base.TopItem.Index; j < base.Items.Count; j++) {
                            if ((base.Items[j] != null) && (base.Bounds.Bottom > base.Items[j].Bounds.Top)) {
                                ListViewItem item3 = base.Items[j];
                                string key = 0.ToString() + ".jpg";
                                if (!this.ThumImageList.Images.ContainsKey(key)) {
                                    string path = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                                    if (!Directory.Exists(path)) {
                                        Directory.CreateDirectory(path);
                                    }
                                    string filename = Path.Combine(path, key);
                                    Image image = ClientData.ItemImages.imageList.Images[0];
                                    image.Save(filename);
                                    this.ThumImageList.Images.Add(key, image);
                                    image.Dispose();
                                }
                                IBizItem item4 = null;
                                if (item3.Tag is IBizItem) {
                                    item4 = item3.Tag as IBizItem;
                                } else if (item3.Tag is DataRowView) {
                                    Guid masterOid = new Guid((byte[])((DataRowView)item3.Tag)[0]);
                                    item4 = FindBizItems(bizItems, masterOid);
                                }
                                if (item4 != null) {
                                    string str4 = "";
                                    string fileName = "";
                                    Image reducedImage = null;
                                    if (dictionary.ContainsKey(item4.IterOid)) {
                                        str4 = dictionary[item4.IterOid];
                                        if (File.Exists(str4)) {
                                            fileName = Path.GetFileName(str4);
                                            reducedImage = UIThumbnailHelper.Instance.GetReducedImage(str4);
                                            if (reducedImage != null) {
                                                if (this.ThumImageList.Images.ContainsKey(fileName)) {
                                                    int num5 = this.ThumImageList.Images.IndexOfKey(fileName);
                                                    this.ThumImageList.Images[num5] = reducedImage;
                                                } else {
                                                    this.ThumImageList.Images.Add(fileName, reducedImage);
                                                }
                                                reducedImage.Dispose();
                                            }
                                        }
                                    } else {
                                        fileName = ClientData.ItemImages.GetObjectImage(item4.ClassName, PLDataModel.GetStateByMasterInfo(item4.ExactState, item4.HasFile)).ToString() + ".jpg";
                                        if (!this.ThumImageList.Images.ContainsKey(fileName)) {
                                            string str6 = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                                            if (!Directory.Exists(str6)) {
                                                Directory.CreateDirectory(str6);
                                            }
                                            string str7 = Path.Combine(str6, fileName);
                                            reducedImage = ClientData.ItemImages.GetObjectImage1(item4.ClassName, PLDataModel.GetStateByMasterInfo(item4.ExactState, item4.HasFile));
                                            reducedImage.Save(str7);
                                            this.ThumImageList.Images.Add(fileName, reducedImage);
                                            reducedImage.Dispose();
                                        }
                                    }
                                    item3.ImageIndex = this.ThumImageList.Images.IndexOfKey(fileName);
                                }
                            }
                        }
                        this.Refresh();
                        GC.GetGeneration(generation);
                    }
                }
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        public static void FillItemImage(ThumbnailListView lvw) {
            try {
                try {
                    if ((!lvw.Visible || (lvw.TopItem == null)) || (lvw.TopItem.Index == -1)) {
                        return;
                    }
                } catch {
                    return;
                }
                Cursor.Current = Cursors.WaitCursor;
                if ((lvw.SmallImageList == null) || (lvw.SmallImageList != lvw.ThumImageList)) {
                    lvw.SmallImageList = lvw.ThumImageList;
                }
                int generation = GC.GetGeneration(lvw.ThumImageList);
                List<IBizItem> bizItems = new List<IBizItem>();
                ArrayList masterOids = new ArrayList();
                ArrayList revNums = new ArrayList();
                for (int i = lvw.TopItem.Index; i < lvw.Items.Count; i++) {
                    if ((lvw.Items[i] != null) && (lvw.Bounds.Bottom > lvw.Items[i].Bounds.Top)) {
                        ListViewItem item = lvw.Items[i];
                        IBizItem tag = null;
                        if (item.Tag is IBizItem) {
                            tag = item.Tag as IBizItem;
                        } else if (item.Tag is DataRowView) {
                            try {
                                masterOids.Add(new Guid((byte[])((DataRowView)item.Tag)[0]));
                                revNums.Add(0);
                            } catch {
                            }
                        }
                        if (tag != null) {
                            bizItems.Add(tag);
                        }
                    }
                }
                if (masterOids.Count > 0) {
                    ArrayList list4 = PLItem.Agent.GetBizItemsByMasters(masterOids, revNums, lvw.ThumPSOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem);
                    if (list4.Count > 0) {
                        bizItems.AddRange((IBizItem[])list4.ToArray(typeof(IBizItem)));
                    }
                }
                Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
                if (bizItems.Count > 0) {
                    try {
                        dictionary = UIThumbnailHelper.Instance.DownLoadThumFiles(bizItems, lvw.ThumPSOption);
                    } catch (Exception exception) {
                        PLMEventLog.WriteExceptionLog(exception);
                    }
                }
                string key = 0.ToString() + ".jpg";
                if (!lvw.ThumImageList.Images.ContainsKey(key)) {
                    string path = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                    if (!Directory.Exists(path)) {
                        Directory.CreateDirectory(path);
                    }
                    string filename = Path.Combine(path, key);
                    Image image = ClientData.ItemImages.imageList.Images[0];
                    image.Save(filename);
                    lock (typeof(ImageList)) {
                        lvw.ThumImageList.Images.Add(key, image);
                    }
                    image.Dispose();
                }
                for (int j = lvw.TopItem.Index; j < lvw.Items.Count; j++) {
                    if ((lvw.Items[j] != null) && (lvw.Bounds.Bottom > lvw.Items[j].Bounds.Top)) {
                        ListViewItem item3 = lvw.Items[j];
                        IBizItem item4 = null;
                        if (item3.Tag is IBizItem) {
                            item4 = item3.Tag as IBizItem;
                        } else if (item3.Tag is DataRowView) {
                            try {
                                Guid masterOid = new Guid((byte[])((DataRowView)item3.Tag)[0]);
                                item4 = FindBizItems(bizItems, masterOid);
                            } catch {
                            }
                        }
                        if (item4 != null) {
                            string str4 = "";
                            string fileName = "";
                            Image reducedImage = null;
                            if (dictionary.ContainsKey(item4.IterOid)) {
                                str4 = dictionary[item4.IterOid];
                                if (File.Exists(str4)) {
                                    fileName = Path.GetFileName(str4);
                                    reducedImage = UIThumbnailHelper.Instance.GetReducedImage(str4);
                                    if (reducedImage != null) {
                                        if (lvw.ThumImageList.Images.ContainsKey(fileName)) {
                                            int num5 = lvw.ThumImageList.Images.IndexOfKey(fileName);
                                            lvw.ThumImageList.Images[num5] = reducedImage;
                                        } else {
                                            lock (typeof(ImageList)) {
                                                lvw.ThumImageList.Images.Add(fileName, reducedImage);
                                            }
                                        }
                                        reducedImage.Dispose();
                                    }
                                }
                            } else {
                                fileName = ClientData.ItemImages.GetObjectImage(item4.ClassName, PLDataModel.GetStateByMasterInfo(item4.ExactState, item4.HasFile)).ToString() + ".jpg";
                                if (!lvw.ThumImageList.Images.ContainsKey(fileName)) {
                                    string str6 = Path.Combine(ConstConfig.GetTempfilePath(), "Thum");
                                    if (!Directory.Exists(str6)) {
                                        Directory.CreateDirectory(str6);
                                    }
                                    string str7 = Path.Combine(str6, fileName);
                                    reducedImage = ClientData.ItemImages.GetObjectImage1(item4.ClassName, PLDataModel.GetStateByMasterInfo(item4.ExactState, item4.HasFile));
                                    reducedImage.Save(str7);
                                    lock (typeof(ImageList)) {
                                        lvw.ThumImageList.Images.Add(fileName, reducedImage);
                                    }
                                    reducedImage.Dispose();
                                }
                            }
                            item3.ImageIndex = lvw.ThumImageList.Images.IndexOfKey(fileName);
                        }
                    }
                }
                lvw.Refresh();
                GC.GetGeneration(generation);
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        public virtual void FillListViewImageList() {
            try {
                if ((!base.Disposing && !base.IsDisposed) && ((this.imageFiller != null) && !this.imageFiller.IsBusy)) {
                    this.imageFiller.RunFillImage();
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        protected static IBizItem FindBizItems(List<IBizItem> bizItems, Guid masterOid) {
            if ((bizItems != null) && (bizItems.Count != 0)) {
                foreach (IBizItem item in bizItems) {
                    if (item.MasterOid == masterOid) {
                        return item;
                    }
                }
            }
            return null;
        }

        private int GetColumnImageIndex(IBizItem item) {
            if (item == null) {
                return -1;
            }
            ItemState state = (item.LastRevision == item.RevNum) ? item.State : ItemState.Release;
            if (item.LastRevision == item.RevNum) {
                state = (item.LastIteration == item.IterNum) ? item.State : ItemState.CheckIn;
            }
            return ClientData.ItemImages.GetObjectImage(item.ClassName, PLDataModel.GetStateByMasterInfo(state, item.HasFile));
        }

        protected virtual void OnDisplayStyleChanged(object sender, ThumToolsEventArgs e) {
        }

        private void OnItemClick(object sender, EventArgs e) {
            this.ViewThumFile();
        }

        private void OnKeyUp(object sender, KeyEventArgs e) {
            try {
                if (this.OnScroll != null) {
                    this.OnScroll(this, true);
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        private void OnPreviewChanged(object sender, ThumToolsEventArgs e) {
            this.ViewThumFile();
        }

        protected void OnResize(object sender, EventArgs e) {
            try {
                this.FillListViewImageList();
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        protected virtual void OnScrollChanged(object sender, bool vscroll) {
            try {
                if (vscroll) {
                    this.FillListViewImageList();
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        public void PreviewClear() {
            if (this.uc_thumTool != null) {
                this.uc_thumTool.ShowThum(null, null);
            }
        }

        public void PreviewFile() {
            this.ViewThumFile();
        }

        public void ReReadSetting() {
            if (this.uc_thumTool != null) {
                this.uc_thumTool.ReReadSetting();
            }
        }

        protected void ReSetColumns() {
            if (base.Columns.Count > 0) {
                for (int i = 0; i < base.Columns.Count; i++) {
                    ColumnHeader header = base.Columns[i];
                    if ((header.Tag != null) && header.Tag.Equals("Thumbnail")) {
                        header.Width -= PLSystemParam.ParamThumWdith;
                        header.Tag = null;
                    }
                }
                if (this.uc_thumTool.ThumSetting.DisplayStyle == DisplayStyle.Thumbnail) {
                    base.Columns[0].Width += PLSystemParam.ParamThumWdith;
                    base.Columns[0].Tag = "Thumbnail";
                }
            }
        }

        public void SetToolBarVisible(bool isVisible) {
            this.isShowThumToolBar = this.uc_thumTool.ToolBar.Visible = isVisible;
        }

        protected virtual void ViewThumFile() {
            try {
                if (((this.uc_thumTool != null) && this.uc_thumTool.ThumSetting.IsPreview) && ((base.SelectedItems != null) && (base.SelectedItems.Count == 1))) {
                    this.uc_thumTool.ShowThum(base.SelectedItems[0].Tag, this.ThumPSOption);
                }
            } catch (Exception exception) {
                PrintException.Print(exception);
            }
        }

        protected override void WndProc(ref Message m) {
            if ((m.Msg == 0x114) || (m.Msg == 0x115)) {
                if (this.OnScroll != null) {
                    this.OnScroll(this, m.Msg == 0x115);
                }
            } else if ((m.Msg == 0x20a) && (this.OnScroll != null)) {
                this.OnScroll(this, true);
            }
            base.WndProc(ref m);
        }

        public bool IsFillDetailImage { get; set; }

        public DisplayStyleChangedHandler OnDisplayStyleChangedHander {
            set {
                this.uc_thumTool.DisplayStyleChanged += value;
            }
        }

        public DEPSOption ThumPSOption {
            get {
                if (this.thumPSOption == null) {
                    this.thumPSOption = ClientData.UserGlobalOption;
                }
                return this.thumPSOption;
            }
            set {
                this.thumPSOption = value;
            }
        }

        public ThumbnailSetting ThumSetting {
            get {
                if (this.uc_thumTool != null) {
                    return this.uc_thumTool.ThumSetting;
                }
                return null;
            }
        }

        public ToolStrip ThumToolBar {
            get {
                return
                    this.uc_thumTool.ToolBar;
            }
        }

        public ToolStripCtrl ThumToolBarItem {
            get {
                return
                    this.uc_thumTool.ToolsItem;
            }
        }

        public delegate void ListViewScroll(object sender, bool vscroll);

        internal class ThumbnailFiller {
            private BackgroundWorker bkgWork;
            private Exception exception;
            private ThumbnailListView lvw;
            private DateTime request;
            private System.Windows.Forms.Timer time_MouseWheelListener;

            public ThumbnailFiller(ThumbnailListView lvw) {
                this.lvw = lvw;
                this.bkgWork = new BackgroundWorker();
                this.bkgWork.DoWork += new DoWorkEventHandler(this.bkgWork_DoWork);
                this.bkgWork.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bkgWork_RunWorkerCompleted);
                this.time_MouseWheelListener = new System.Windows.Forms.Timer();
                this.time_MouseWheelListener.Interval = 200;
                this.time_MouseWheelListener.Enabled = false;
                this.time_MouseWheelListener.Tick += new EventHandler(this.OnTick);
            }

            private void bkgWork_DoWork(object sender, DoWorkEventArgs e) {
                try {
                    this.exception = null;
                    this.lvw.FillItemImage();
                } catch (Exception exception) {
                    this.exception = exception;
                    PLMEventLog.WriteExceptionLog(exception);
                }
            }

            private void bkgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            }

            public void Dispose() {
                if (this.time_MouseWheelListener != null) {
                    this.time_MouseWheelListener.Enabled = false;
                    this.time_MouseWheelListener.Stop();
                    this.time_MouseWheelListener.Dispose();
                }
                if (this.bkgWork != null) {
                    this.bkgWork.Dispose();
                }
            }

            private void OnTick(object sender, EventArgs e) {
                try {
                    if (DateTime.Now.Subtract(this.request).Milliseconds > 300) {
                        this.bkgWork.RunWorkerAsync();
                        this.time_MouseWheelListener.Enabled = false;
                        if (this.exception != null) {
                            throw this.exception;
                        }
                    }
                } catch (Exception exception) {
                    PLMEventLog.WriteExceptionLog(exception);
                }
            }

            public void RunFillImage() {
                this.request = DateTime.Now;
                this.time_MouseWheelListener.Enabled = true;
            }

            public bool IsBusy {
                get {
                    return
                        this.bkgWork.IsBusy;
                }
            }
        }
    }
}

