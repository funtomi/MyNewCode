    using Crownwood.DotNetMagic.Docking;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.FileService;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public partial class FrmRESPicture : FormPLM
    {
        private ContextMenu cmuOnPic;
        private ContextMenu cmuPicture;
        private DockingManager dockingManager;
        
        private ImageList ImageListLarge;
        private ImageList ImageListSmall;
       
        private ArrayList myList = new ArrayList();
        private PictureBoxPLM pb;
        private Guid picOid = Guid.Empty;
       
        private DEPicture selectedPicture = new DEPicture();
       
        private TreeView tvwPicture = new TreeView();

        public FrmRESPicture()
        {
            this.InitializeComponent();
            this.AddIcon();
            this.dockingManager = new DockingManager(this, ClientData.OptApplicationStyle);
            this.CreateDockingControl();
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void AddIcon()
        {
            string[] resNames = new string[] { "ICO_RES_PICTURE", "ICO_RES_PICTURE_SELECTED" };
            ClientData.MyImageList.AddIcons(resNames);
            this.tvwPicture.ImageList = ClientData.MyImageList.imageList;
            this.tvwPicture.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PICTURE");
            this.tvwPicture.SelectedImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PICTURE_SELECTED");
        }

        private void ClearChecked(int flag)
        {
            if (flag == 0)
            {
                this.miLargeIcon.Checked = false;
                this.miSmallIcon.Checked = false;
                this.miList.Checked = false;
                this.miDetails.Checked = false;
            }
            else if (flag == 1)
            {
                this.miNormal.Checked = false;
                this.miStretchImage.Checked = false;
                this.miAutoSize.Checked = false;
                this.miCenterImage.Checked = false;
            }
        }

        private void CreateDockingControl()
        {
            this.tvwPicture.Dock = DockStyle.Left;
            this.tvwPicture.Location = new Point(0, 0);
            this.tvwPicture.Name = "tvwPicture";
            this.tvwPicture.Size = new Size(0x79, 0x127);
            this.tvwPicture.TabIndex = 0;
            this.tvwPicture.AfterSelect += new TreeViewEventHandler(this.tvwPicture_AfterSelect);
            Content c = this.dockingManager.Contents.Add(this.tvwPicture, "图片资源", ClientData.MyImageList.imageList, ClientData.MyImageList.GetIconIndex("ICO_RES_PICTURE"));
            this.dockingManager.AddContentWithState(c, Crownwood.DotNetMagic.Docking.State.DockLeft);
            this.dockingManager.ToggleContentAutoHide(c);
        }
         
        private void FrmRESPicture_Load(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode("图片资源");
            this.tvwPicture.Nodes.Add(node);
            this.tvwPicture.Nodes[0].Nodes.Add("图片");
            this.InitializeLvwImage();
        }


        private void InitializeLvwImage()
        {
            string str = "";
            this.ImageListLarge = new ImageList();
            this.ImageListSmall = new ImageList();
            this.lvwImage.LargeImageList = this.ImageListLarge;
            this.lvwImage.SmallImageList = this.ImageListSmall;
            this.lvwImage.View = View.Details;
            this.lvwImage.Items.Clear();
            try
            {
                this.myList = new PLPicture().GetAllPictures();
                if (this.myList.Count > 0)
                {
                    int num;
                    DEPicture picture2 = null;
                    string filename = "";
                    for (num = 0; num < this.myList.Count; num++)
                    {
                        picture2 = (DEPicture) this.myList[num];
                        filename = FSClientUtil.DownloadFile(picture2.Oid, "ClaRel_BROWSE");
                        picture2.Name = filename;
                        this.ImageListLarge.Images.Add(Image.FromFile(filename));
                        this.ImageListSmall.Images.Add(Image.FromFile(filename));
                    }
                    ListViewItem item = null;
                    for (num = 0; num < this.myList.Count; num++)
                    {
                        picture2 = (DEPicture) this.myList[num];
                        item = new ListViewItem(picture2.Alias, num);
                        long size = picture2.Size;
                        if (picture2.Size >= 0x400L)
                        {
                            size = picture2.Size / 0x400L;
                            str = size.ToString() + " KB";
                        }
                        else if (picture2.Size < 0x400L)
                        {
                            str = size.ToString() + " 字节";
                        }
                        item.SubItems.AddRange(new string[] { str, picture2.Creator, picture2.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), picture2.Modifier, picture2.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss"), picture2.Description });
                        item.Tag = picture2;
                        this.lvwImage.Items.Add(item);
                    }
                }
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
            }
            catch (Exception exception2)
            {
                MessageBoxPLM.Show("初始化图片列表出错：" + exception2.Message, "图片资源");
            }
        }

        private void lvwImage_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvwImage.SelectedItems.Count > 0)
            {
                DEPicture tag = (DEPicture) this.lvwImage.SelectedItems[0].Tag;
                this.picOid = tag.Oid;
                this.selectedPicture = tag;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void lvwImage_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (this.lvwImage.SelectedItems.Count >= 1)
            {
                DEPicture tag = (DEPicture) this.lvwImage.SelectedItems[0].Tag;
                if (tag != null)
                {
                    this.lvwImage.DoDragDrop(tag, DragDropEffects.Move | DragDropEffects.Copy);
                }
            }
        }

        private void lvwImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.lvwImage.SelectedItems.Count <= 0)
                {
                    this.miProperty.Visible = false;
                    this.miSelected.Visible = false;
                }
                else
                {
                    this.miProperty.Visible = true;
                    this.miSelected.Visible = true;
                }
            }
        }

        private void lvwImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvwImage.SelectedItems.Count <= 0)
            {
                this.pnlNatural.Visible = false;
                this.pnlMiniature.Dock = DockStyle.Fill;
                this.pb.Image = null;
            }
            else
            {
                this.pnlMiniature.Dock = DockStyle.Top;
                this.pnlNatural.Visible = true;
                DEPicture tag = (DEPicture) this.lvwImage.SelectedItems[0].Tag;
                if (tag != null)
                {
                    this.lblAlias.Text = "名称：" + tag.Alias;
                    if (tag.Size < 0x400L)
                    {
                        this.lblSize.Text = "大小：" + tag.Size.ToString() + " 字节";
                    }
                    else if (tag.Size >= 0x400L)
                    {
                        this.lblSize.Text = "大小：" + ((tag.Size / 0x400L)).ToString() + " KB";
                    }
                    this.lblCreator.Text = "创建者：" + tag.Creator;
                    this.lblCreateTime.Text = "创建时间：" + tag.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    this.lblModifier.Text = "修改者：" + tag.Modifier;
                    this.lblModifyTime.Text = "修改时间：" + tag.ModifyTime.ToString("yyyy-MM-dd HH:mm:ss");
                    this.lblDes.Text = "描述：" + tag.Description;
                    this.pb.Image = Image.FromFile(tag.Name);
                    this.miStretchImage_Click(sender, e);
                }
                else
                {
                    this.lblAlias.Text = "名称：";
                    this.lblSize.Text = "大小：";
                    this.lblCreator.Text = "创建者：";
                    this.lblCreateTime.Text = "创建时间：";
                    this.lblModifier.Text = "修改者：";
                    this.lblModifyTime.Text = "修改时间：";
                    this.lblDes.Text = "描述：";
                    this.pb.Image = null;
                }
            }
        }

        private void miAutoSize_Click(object sender, EventArgs e)
        {
            this.pb.Location = new Point(10, 10);
            this.pb.SizeMode = PictureBoxSizeMode.AutoSize;
            this.ClearChecked(1);
            this.miAutoSize.Checked = true;
        }

        private void miCenterImage_Click(object sender, EventArgs e)
        {
            this.pb.SizeMode = PictureBoxSizeMode.CenterImage;
            this.ClearChecked(1);
            this.miCenterImage.Checked = true;
            this.pb.Location = new Point(10, 10);
            int width = this.pnlLargeImage.Width - 20;
            int height = this.pnlLargeImage.Height - 20;
            this.pb.Size = new Size(width, height);
        }

        private void miDetails_Click(object sender, EventArgs e)
        {
            this.lvwImage.View = View.Details;
            this.ClearChecked(0);
            this.miDetails.Checked = true;
        }

        private void miLargeIcon_Click(object sender, EventArgs e)
        {
            this.lvwImage.View = View.LargeIcon;
            this.ClearChecked(0);
            this.miLargeIcon.Checked = true;
        }

        private void miList_Click(object sender, EventArgs e)
        {
            this.lvwImage.View = View.List;
            this.ClearChecked(0);
            this.miList.Checked = true;
        }

        private void miNormal_Click(object sender, EventArgs e)
        {
            this.pb.SizeMode = PictureBoxSizeMode.Normal;
            this.ClearChecked(1);
            this.miNormal.Checked = true;
            this.pb.Location = new Point(10, 10);
            int width = this.pnlLargeImage.Width - 20;
            int height = this.pnlLargeImage.Height - 20;
            this.pb.Size = new Size(width, height);
        }

        private void miProperty_Click(object sender, EventArgs e)
        {
            if (this.lvwImage.SelectedItems.Count > 0)
            {
                new FrmPicProperty((DEPicture) this.lvwImage.SelectedItems[0].Tag, 1).ShowDialog();
            }
        }

        private void miRefresh_Click(object sender, EventArgs e)
        {
            this.InitializeLvwImage();
            this.pnlNatural.Visible = false;
            this.pnlMiniature.Dock = DockStyle.Fill;
            this.pb.Image = null;
        }

        private void miSelected_Click(object sender, EventArgs e)
        {
            if ((this.lvwImage.SelectedItems.Count > 0) && (MessageBoxPLM.Show("选择该图片?", "选择图片", MessageBoxButtons.OKCancel) == DialogResult.OK))
            {
                DEPicture tag = (DEPicture) this.lvwImage.SelectedItems[0].Tag;
                this.picOid = tag.Oid;
                this.selectedPicture = tag;
                base.DialogResult = DialogResult.OK;
            }
        }

        private void miShowDock_Click(object sender, EventArgs e)
        {
            this.dockingManager.ShowContent(this.dockingManager.Contents["图片资源"]);
            this.dockingManager.BringAutoHideIntoView(this.dockingManager.Contents["图片资源"]);
        }

        private void miSmallIcon_Click(object sender, EventArgs e)
        {
            this.lvwImage.View = View.SmallIcon;
            this.ClearChecked(0);
            this.miSmallIcon.Checked = true;
        }

        private void miStretchImage_Click(object sender, EventArgs e)
        {
            this.pb.SizeMode = PictureBoxSizeMode.StretchImage;
            this.ClearChecked(1);
            this.miStretchImage.Checked = true;
            this.pb.Location = new Point(10, 10);
            int width = this.pnlLargeImage.Width - 20;
            int height = this.pnlLargeImage.Height - 20;
            this.pb.Size = new Size(width, height);
        }

        private void pnlLargeImage_Resize(object sender, EventArgs e)
        {
            if (this.pb.SizeMode == PictureBoxSizeMode.StretchImage)
            {
                this.pb.Location = new Point(10, 10);
                int width = this.pnlLargeImage.Width - 20;
                int height = this.pnlLargeImage.Height - 20;
                this.pb.Size = new Size(width, height);
            }
            else
            {
                this.pb.Location = new Point(10, 10);
            }
        }

        private void tvwPicture_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.lvwImage.Items.Clear();
            this.pnlNatural.Visible = false;
            this.pnlMiniature.Dock = DockStyle.Fill;
            this.pb.Image = null;
            if ((this.tvwPicture.SelectedNode != null) && (!this.tvwPicture.Nodes[0].IsSelected && (this.tvwPicture.SelectedNode.Text == "图片")))
            {
                this.InitializeLvwImage();
            }
        }

        public Guid PictureOid
        {
            get{
               return this.picOid;
            }set
            {
                this.picOid = value;
            }
        }

        public DEPicture SelectedPicture
        {
            get {
                return this.selectedPicture;
            }
            set
            {
                this.selectedPicture = value;
            }
        }
    }
}

