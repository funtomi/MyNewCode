    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl
{
    public partial class ThumToolsBaseCtrl : UserControlPLM, IThumTools
    {
        private Control container;
        private Control itemViewCtrl;
        private string location;
        
        private ThumbnailSetting thumSetting = new ThumbnailSetting();
        
        private UCThumbnail ucThumbnail;

        public event DisplayStyleChangedHandler DisplayStyleChanged;

        public event PreviewChangedHandler PreviewChanged;

        public ThumToolsBaseCtrl(Control container, Control itemViewCtrl, string location, bool isShowToolBar)
        {
            this.InitializeComponent();
            this.location = location;
            this.container = container;
            this.itemViewCtrl = itemViewCtrl;
            this.thumSetting = UIThumbnailHelper.Instance.ReadSetting(location);
            this.panRight.Width = this.thumSetting.PreviwWidth;
            if (this.ucThumbnail == null)
            {
                this.ucThumbnail = new UCThumbnail(this.thumSetting);
                this.ucThumbnail.Dock = DockStyle.Fill;
            }
            base.Controls.Remove(this.panMain);
            if (!isShowToolBar)
            {
                this.panMain.Controls.Remove(this.toolBar);
            }
            this.panMain.Dock = DockStyle.Fill;
            this.itemViewCtrl.Dock = DockStyle.Fill;
            this.panLeft.Controls.Add(this.itemViewCtrl);
            this.panRight.Controls.Add(this.ucThumbnail);
            this.container.Controls.Add(this.panMain);
            this.tspBtnDisplay.Checked = this.thumSetting.DisplayStyle == DisplayStyle.Thumbnail;
            this.tspBtnDisplay.Image = (this.thumSetting.DisplayStyle == DisplayStyle.Thumbnail) ? this.imageList1.Images[1] : this.imageList1.Images[0];
            this.tspBtnPreview.Checked = this.thumSetting.IsPreview;
            this.panRight.Visible = this.tspBtnPreview.Checked;
            this.panRight.Width = this.thumSetting.PreviwWidth;
            if (this.DisplayStyleChanged != null)
            {
                this.DisplayStyleChanged(this, new ThumToolsEventArgs(this.thumSetting.IsPreview, this.thumSetting.DisplayStyle));
            }
        }


        private void InitPanel()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ThumToolsBaseCtrl));
            this.toolBar = new ToolStrip();
            this.tspBtnSetting = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.tspBtnPreview = new ToolStripButton();
            this.tspBtnDisplay = new ToolStripButton();
            this.imageList1 = new ImageList(this.components);
            this.panMain = new PanelPLM();
            this.panLeft = new PanelPLM();
            this.splitter1 = new SplitterPLM();
            this.panRight = new PanelPLM();
            this.toolBar.Items.AddRange(new ToolStripItem[] { this.tspBtnSetting, this.toolStripSeparator1, this.tspBtnPreview, this.tspBtnDisplay });
            this.toolBar.Location = new Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new Size(200, 0x19);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            this.tspBtnSetting.Alignment = ToolStripItemAlignment.Right;
            this.tspBtnSetting.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tspBtnSetting.Image = (Image) manager.GetObject("tspBtnSetting.Image");
            this.tspBtnSetting.ImageTransparentColor = Color.Magenta;
            this.tspBtnSetting.Name = "tspBtnSetting";
            this.tspBtnSetting.Size = new Size(0x17, 0x16);
            this.tspBtnSetting.Text = "设置";
            this.tspBtnSetting.Click += new EventHandler(this.tspBtnSetting_Click);
            this.toolStripSeparator1.Alignment = ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.tspBtnPreview.Alignment = ToolStripItemAlignment.Right;
            this.tspBtnPreview.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tspBtnPreview.Image = (Image) manager.GetObject("tspBtnPreview.Image");
            this.tspBtnPreview.ImageTransparentColor = Color.Magenta;
            this.tspBtnPreview.Name = "tspBtnPreview";
            this.tspBtnPreview.Size = new Size(0x17, 0x16);
            this.tspBtnPreview.Text = "预览";
            this.tspBtnPreview.Click += new EventHandler(this.tspBtnPreview_Click);
            this.tspBtnDisplay.Alignment = ToolStripItemAlignment.Right;
            this.tspBtnDisplay.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.tspBtnDisplay.Image = (Image) manager.GetObject("tspBtnDisplay.Image");
            this.tspBtnDisplay.ImageTransparentColor = Color.Magenta;
            this.tspBtnDisplay.Name = "tspBtnDisplay";
            this.tspBtnDisplay.Size = new Size(0x17, 0x16);
            this.tspBtnDisplay.Text = "显示方式";
            this.tspBtnDisplay.Click += new EventHandler(this.tspBtnDisplay_Click);
            this.imageList1.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "detail.jpg");
            this.imageList1.Images.SetKeyName(1, "icon.jpg");
            this.panMain.Controls.Add(this.panLeft);
            this.panMain.Controls.Add(this.splitter1);
            this.panMain.Controls.Add(this.panRight);
            this.panMain.Controls.Add(this.toolBar);
            this.panMain.Location = new Point(3, 3);
            this.panMain.Name = "panMain";
            this.panMain.Size = new Size(200, 100);
            this.panMain.TabIndex = 1;
            this.panLeft.Dock = DockStyle.Fill;
            this.panLeft.Location = new Point(0, 0x19);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new Size(0x77, 0x4b);
            this.panLeft.TabIndex = 0;
            this.splitter1.Dock = DockStyle.Right;
            this.splitter1.Location = new Point(0x77, 0x19);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new Size(3, 0x4b);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.panRight.Dock = DockStyle.Right;
            this.panRight.Location = new Point(0x7a, 0x19);
            this.panRight.Name = "panRight";
            this.panRight.Size = new Size(0x4e, 0x4b);
            this.panRight.TabIndex = 2;
            this.panRight.Resize += new EventHandler(this.panRight_Resize);
        }

        private void panRight_Resize(object sender, EventArgs e)
        {
            try
            {
                this.thumSetting.PreviwWidth = this.panRight.Width;
                UIThumbnailHelper.Instance.SaveSetting(this.Location, this.ThumSetting);
            }
            catch (Exception)
            {
            }
        }

        public void ReReadSetting()
        {
            this.ThumSetting = UIThumbnailHelper.Instance.ReadSetting(this.location);
        }

        public void ShowThum(object objItem, DEPSOption option)
        {
            if (this.ucThumbnail != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.ucThumbnail.Display(objItem, option, this.thumSetting);
                }
                catch (Exception exception)
                {
                    PrintException.Print(exception);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void tspBtnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                this.tspBtnDisplay.Checked = !this.tspBtnDisplay.Checked;
                this.tspBtnDisplay.ToolTipText = this.tspBtnDisplay.Checked ? "缩略图" : "详细信息";
                this.tspBtnDisplay.Image = this.tspBtnDisplay.Checked ? this.imageList1.Images[1] : this.imageList1.Images[0];
                DisplayStyle displayStyle = this.tspBtnDisplay.Checked ? DisplayStyle.Thumbnail : DisplayStyle.Detail;
                this.thumSetting.DisplayStyle = displayStyle;
                UIThumbnailHelper.Instance.SaveSetting(this.Location, this.ThumSetting);
                if (this.DisplayStyleChanged != null)
                {
                    this.DisplayStyleChanged(this, new ThumToolsEventArgs(this.thumSetting.IsPreview, displayStyle));
                }
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
        }

        private void tspBtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.tspBtnPreview.Checked = !this.tspBtnPreview.Checked;
                this.thumSetting.IsPreview = this.tspBtnPreview.Checked;
                if (this.tspBtnPreview.Checked && (this.PreviewChanged != null))
                {
                    this.PreviewChanged(this, new ThumToolsEventArgs(this.thumSetting.IsPreview, this.thumSetting.DisplayStyle));
                }
                UIThumbnailHelper.Instance.SaveSetting(this.Location, this.ThumSetting);
                this.panRight.Visible = this.tspBtnPreview.Checked;
                this.panRight.Width = this.thumSetting.PreviwWidth;
            }
            catch (Exception exception)
            {
                PrintException.Print(exception);
            }
        }

        private void tspBtnSetting_Click(object sender, EventArgs e)
        {
        }

        public bool DisplayBtnVisible
        {
            get {return
                this.tspBtnDisplay.Visible;
            }set
            {
                this.tspBtnDisplay.Visible = value;
            }
        }

        public string Location
        {
            get{
               return this.location;
            }set
            {
                this.location = value;
            }
        }

        public ThumbnailSetting ThumSetting
        {
            get {
                return this.thumSetting;
            }
            set
            {
                this.thumSetting = value;
            }
        }

        public ToolStrip ToolBar{get{return
            this.toolBar;}}

        public ToolStripCtrl ToolsItem
        {
            get
            {
                ToolStripItem[] itemArray = new ToolStripItem[2];
                itemArray[1] = this.tspBtnDisplay;
                itemArray[0] = this.tspBtnPreview;
                this.toolsItem = new ToolStripCtrl(itemArray);
                this.toolsItem.Alignment = ToolStripItemAlignment.Right;
                return this.toolsItem;
            }
        }
    }
}

