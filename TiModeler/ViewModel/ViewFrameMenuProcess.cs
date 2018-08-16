namespace Thyt.TiPLM.CLT.TiModeler.ViewModel
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Admin.View;
    using Thyt.TiPLM.PLL.Admin.View;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.Common;

    public class ViewFrameMenuProcess
    {
        private DEViewModel deVM;
        private DEUser theOper;
        private VMViewPanal thePanal;
        private VMShape1 theShape;

        public ViewFrameMenuProcess()
        {
        }

        public ViewFrameMenuProcess(VMViewPanal panal, DEViewModel deVM)
        {
            this.thePanal = panal;
            this.deVM = deVM;
        }

        public ViewFrameMenuProcess(VMViewPanal panal, VMShape1 selectedShape, DEUser oper)
        {
            this.thePanal = panal;
            this.theShape = selectedShape;
            this.theOper = oper;
        }

        public void AddOldView(object sender, EventArgs e)
        {
            FrmViewModelProperty property = new FrmViewModelProperty(true);
            if (((property.ShowDialog() == DialogResult.OK) && (property.selectedView != null)) && (property.selectedView.Count > 0))
            {
                this.thePanal.AddSelectedOldView(property.selectedView);
            }
            property.Dispose();
        }

        public MenuItemEx[] BuildEditMenuItems()
        {
            MenuItemEx[] exArray = new MenuItemEx[5];
            exArray[0] = new MenuItemEx("ViewFrame &Property", "视图模型属性(&P)", null, null);
            exArray[0].Click += new EventHandler(this.ViewFrameProperty);
            exArray[0].DefaultItem = true;
            exArray[0].ImageList = ClientData.MyImageList.imageList;
            exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray[1] = new MenuItemEx("-", "-", null, null);
            exArray[2] = new MenuItemEx("ViewFrame &Refresh", "刷新(&R)", null, null);
            exArray[2].ImageList = ClientData.MyImageList.imageList;
            exArray[2].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
            exArray[2].Click += new EventHandler(this.ViewFrameRefresh);
            exArray[3] = new MenuItemEx("-", "-", null, null);
            exArray[4] = new MenuItemEx("ViewFrame &Close", "关闭(&C)", null, null);
            exArray[4].ImageList = ClientData.MyImageList.imageList;
            exArray[4].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_EXIT");
            exArray[4].Click += new EventHandler(this.ViewFrameClose);
            return exArray;
        }

        public MenuItemEx[] BuildSelfEditMenuItems()
        {
            MenuItemEx[] exArray = new MenuItemEx[12];
            exArray[0] = new MenuItemEx("ViewFrame &Property", "视图模型属性(&P)", null, null);
            exArray[0].Click += new EventHandler(this.ViewFrameProperty);
            exArray[0].DefaultItem = true;
            exArray[0].ImageList = ClientData.MyImageList.imageList;
            exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray[1] = new MenuItemEx("-", "-", null, null);
            exArray[2] = new MenuItemEx("&Add Old View", "添加原有视图(&A)", null, null);
            exArray[2].Click += new EventHandler(this.AddOldView);
            exArray[3] = new MenuItemEx("&New View", "新建视图(&N)", null, null);
            exArray[3].Click += new EventHandler(this.NewView);
            exArray[4] = new MenuItemEx("-", "-", null, null);
            exArray[5] = new MenuItemEx("Cancel E&Dit", "取消编辑(&D)", null, null);
            exArray[5].Click += new EventHandler(this.ViewFrameCancelEdit);
            exArray[6] = new MenuItemEx("ViewFrame &Finish Edit", "结束编辑(&F)", null, null);
            exArray[6].Click += new EventHandler(this.ViewFrameFinishEdit);
            exArray[7] = new MenuItemEx("-", "-", null, null);
            exArray[8] = new MenuItemEx("ViewFrame &Refresh", "刷新(&R)", null, null);
            exArray[8].ImageList = ClientData.MyImageList.imageList;
            exArray[8].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
            exArray[8].Click += new EventHandler(this.ViewFrameRefresh);
            exArray[9] = new MenuItemEx("-", "-", null, null);
            exArray[10] = new MenuItemEx("&Save", "保存(&S)", null, null);
            exArray[10].ImageList = ClientData.MyImageList.imageList;
            exArray[10].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_SAVE");
            exArray[10].Click += new EventHandler(this.ViewFrameSave);
            exArray[11] = new MenuItemEx("ViewFrame &Close", "关闭(&C)", null, null);
            exArray[11].ImageList = ClientData.MyImageList.imageList;
            exArray[11].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_EXIT");
            exArray[11].Click += new EventHandler(this.ViewFrameClose);
            return exArray;
        }

        public MenuItemEx[] BuildViewMenuItems()
        {
            if (!(((DEViewModel) this.thePanal.mainWindow.Tag).Locker == ClientData.LogonUser.Oid))
            {
                MenuItemEx[] exArray = new MenuItemEx[] { new MenuItemEx("View &Property", "视图属性(&P)", null, null) };
                exArray[0].Click += new EventHandler(this.ViewProperty);
                exArray[0].DefaultItem = true;
                exArray[0].ImageList = ClientData.MyImageList.imageList;
                exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                return exArray;
            }
            MenuItemEx[] exArray2 = new MenuItemEx[3];
            exArray2[0] = new MenuItemEx("View &Property", "视图属性(&P)", null, null);
            exArray2[0].Click += new EventHandler(this.ViewProperty);
            exArray2[0].DefaultItem = true;
            exArray2[0].ImageList = ClientData.MyImageList.imageList;
            exArray2[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray2[1] = new MenuItemEx("-", "-", null, null);
            exArray2[2] = new MenuItemEx("View &Remove", "移除视图(&R)", null, null);
            exArray2[2].Click += new EventHandler(this.ViewRemove);
            return exArray2;
        }

        public MenuItemEx[] BuildViewRelationMenuItems()
        {
            if (!(((DEViewModel) this.thePanal.mainWindow.Tag).Locker == ClientData.LogonUser.Oid))
            {
                MenuItemEx[] exArray = new MenuItemEx[] { new MenuItemEx("ViewRelation &Property", "视图关系属性(&P)", null, null) };
                exArray[0].Click += new EventHandler(this.ViewRelationProperty);
                exArray[0].DefaultItem = true;
                exArray[0].ImageList = ClientData.MyImageList.imageList;
                exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
                return exArray;
            }
            MenuItemEx[] exArray2 = new MenuItemEx[3];
            exArray2[0] = new MenuItemEx("ViewRelation &Property", "视图关系属性(&P)", null, null);
            exArray2[0].Click += new EventHandler(this.ViewRelationProperty);
            exArray2[0].DefaultItem = true;
            exArray2[0].ImageList = ClientData.MyImageList.imageList;
            exArray2[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray2[1] = new MenuItemEx("-", "-", null, null);
            exArray2[2] = new MenuItemEx("ViewRelation &Delete", "删除关系(&D)", null, null);
            exArray2[2].Click += new EventHandler(this.ViewRelationDelete);
            return exArray2;
        }

        public MenuItemEx[] BuiltInitMenuItemOfActivedVM()
        {
            MenuItemEx[] exArray = new MenuItemEx[7];
            exArray[0] = new MenuItemEx("ViewFrame &Property", "视图模型属性(&P)", null, null);
            exArray[0].Click += new EventHandler(this.ViewFrameProperty);
            exArray[0].DefaultItem = true;
            exArray[0].ImageList = ClientData.MyImageList.imageList;
            exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray[1] = new MenuItemEx("-", "-", null, null);
            exArray[2] = new MenuItemEx("ViewFrame &UnActive", "取消激活(&U)", null, null);
            exArray[2].Click += new EventHandler(this.ViewFrameUnActive);
            exArray[3] = new MenuItemEx("-", "-", null, null);
            exArray[4] = new MenuItemEx("ViewFrame &Refresh", "刷新视图模型(&R)", null, null);
            exArray[4].ImageList = ClientData.MyImageList.imageList;
            exArray[4].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
            exArray[4].Click += new EventHandler(this.ViewFrameRefresh);
            exArray[5] = new MenuItemEx("-", "-", null, null);
            exArray[6] = new MenuItemEx("ViewFrame &Close", "关闭视图模型(&C)", null, null);
            exArray[6].ImageList = ClientData.MyImageList.imageList;
            exArray[6].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_EXIT");
            exArray[6].Click += new EventHandler(this.ViewFrameClose);
            return exArray;
        }

        public MenuItemEx[] BuiltInitMenuItemOfUnActivedVM()
        {
            MenuItemEx[] exArray = new MenuItemEx[8];
            exArray[0] = new MenuItemEx("ViewFrame &Property", "视图模型属性(&P)", null, null);
            exArray[0].Click += new EventHandler(this.ViewFrameProperty);
            exArray[0].DefaultItem = true;
            exArray[0].ImageList = ClientData.MyImageList.imageList;
            exArray[0].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_PROPERTY");
            exArray[1] = new MenuItemEx("-", "-", null, null);
            exArray[2] = new MenuItemEx("ViewFrame &Edit", "编辑视图模型(&S)", null, null);
            exArray[2].Click += new EventHandler(this.ViewFrameEdit);
            exArray[3] = new MenuItemEx("ViewFrame &Active", "激活视图模型(&A)", null, null);
            exArray[3].Click += new EventHandler(this.ViewFrameActive);
            exArray[4] = new MenuItemEx("-", "-", null, null);
            exArray[5] = new MenuItemEx("ViewFrame &Refresh", "刷新视图模型(&R)", null, null);
            exArray[5].ImageList = ClientData.MyImageList.imageList;
            exArray[5].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_REFRESH");
            exArray[5].Click += new EventHandler(this.ViewFrameRefresh);
            exArray[6] = new MenuItemEx("-", "-", null, null);
            exArray[7] = new MenuItemEx("ViewFrame &Close", "关闭视图模型(&C)", null, null);
            exArray[7].ImageList = ClientData.MyImageList.imageList;
            exArray[7].ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_VIW_EXIT");
            exArray[7].Click += new EventHandler(this.ViewFrameClose);
            return exArray;
        }

        public void NewView(object sender, EventArgs e)
        {
            int x = new Random().Next(0, 600);
            int y = new Random().Next(0, 600);
            Point startPoint = new Point(x, y);
            VMNode1 node = new VMNode1(Guid.NewGuid(), startPoint, "Node");
            this.thePanal.addNode(node);
        }

        public void ViewFrameActive(object sender, EventArgs e)
        {
            if (MessageBox.Show("激活该视图模型后，该视图模型将在英泰全生命周期系统使用。\n是否确定要激活该视图？", "激活视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
            {
                PLViewModel model = new PLViewModel();
                try
                {
                    DEViewModel tag = (DEViewModel) this.thePanal.mainWindow.Tag;
                    model.ActiveViewModelOrNot(tag.Oid, true);
                    tag.IsActive = 'A';
                    this.thePanal.mainWindow.Tag = tag;
                    ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Tag = tag;
                }
                catch
                {
                    MessageBox.Show("激活视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        public void ViewFrameCancelEdit(object sender, EventArgs e)
        {
            if (MessageBox.Show("取消编辑会丢失视图模型的所有修改！\n您确定要取消编辑？", "视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
            {
                PLViewModel model = new PLViewModel();
                try
                {
                    DEViewModel tag = (DEViewModel) this.thePanal.mainWindow.Tag;
                    model.ChangeVMLocker(tag.Oid, Guid.Empty);
                }
                catch
                {
                    MessageBox.Show("编辑视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                this.thePanal.mainWindow.RefreshViewModel();
            }
        }

        public void ViewFrameClose(object sender, EventArgs e)
        {
            this.thePanal.mainWindow.Close();
        }

        public void ViewFrameEdit(object sender, EventArgs e)
        {
            PLViewModel model = new PLViewModel();
            try
            {
                DEViewModel tag = (DEViewModel) this.thePanal.mainWindow.Tag;
                if (tag == null)
                {
                    MessageBox.Show("编辑视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    model.ChangeVMLocker(tag.Oid, ClientData.LogonUser.Oid);
                    tag.Locker = ClientData.LogonUser.Oid;
                    this.thePanal.mainWindow.Tag = tag;
                    ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Tag = tag;
                }
            }
            catch
            {
                MessageBox.Show("编辑视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ViewFrameFinishEdit(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确定要结束对视图模型的编辑？", "结束编辑视图模型", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.Cancel)
            {
                switch (MessageBox.Show("是否保存视图模型？", "保存视图模型", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        try
                        {
                            this.thePanal.saveFile();
                            break;
                        }
                        catch (ViewException exception)
                        {
                            MessageBox.Show(exception.Message, "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show("保存视图模型“" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "”失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                        break;

                    case DialogResult.No:
                        this.thePanal.mainWindow.RefreshViewModel();
                        break;

                    case DialogResult.Cancel:
                        return;
                }
                try
                {
                    PLViewModel model = new PLViewModel();
                    DEViewModel tag = (DEViewModel) this.thePanal.mainWindow.Tag;
                    model.ChangeVMLocker(tag.Oid, Guid.Empty);
                    tag.Locker = Guid.Empty;
                    this.thePanal.mainWindow.Tag = tag;
                    ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Tag = tag;
                }
                catch
                {
                    MessageBox.Show("结束编辑视图模型“" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "”操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
        }

        public void ViewFrameProperty(object sender, EventArgs e)
        {
            FrmViewModelProperty property = new FrmViewModelProperty(true, (DEViewModel) this.thePanal.mainWindow.Tag);
            if (property.ShowDialog() == DialogResult.OK)
            {
                this.thePanal.mainWindow.Tag = property.theVM;
                this.thePanal.mainWindow.Text = property.theVM.Name;
                ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Tag = property.theVM;
                ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Text = property.theVM.Name;
            }
        }

        public void ViewFrameRefresh(object sender, EventArgs e)
        {
            this.thePanal.mainWindow.RefreshViewModel();
        }

        public void ViewFrameSave(object sender, EventArgs e)
        {
            try
            {
                this.thePanal.saveFile();
                MessageBox.Show("保存视图模型“" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "”成功！", "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (ViewException exception)
            {
                MessageBox.Show(exception.Message, "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                if (exception.InnerException != null)
                {
                    MessageBox.Show(exception.InnerException.Message, "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch
            {
                MessageBox.Show("保存视图模型“" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "”失败！", "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public void ViewFrameUnActive(object sender, EventArgs e)
        {
            PLViewModel model = new PLViewModel();
            bool flag = false;
            try
            {
                flag = model.HasUsedViewModel(((DEViewModel) this.thePanal.mainWindow.Tag).Oid);
            }
            catch (ViewException exception)
            {
                MessageBox.Show(exception.Message, "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("判断视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "是否已经与业务对象绑定失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (!flag)
            {
                if (MessageBox.Show("取消激活后，该视图模型将暂时无法绑定。您是否要继续？", "取消激活", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    return;
                }
            }
            else if (MessageBox.Show("已存在零部件与视图模型“" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "”绑定，且取消激活后，该视图模型将暂时无法绑定。您是否要继续？", "视图模型", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                return;
            }
            try
            {
                DEViewModel tag = (DEViewModel) this.thePanal.mainWindow.Tag;
                model.ActiveViewModelOrNot(tag.Oid, false);
                tag.IsActive = 'U';
                this.thePanal.mainWindow.Tag = tag;
                ((FrmMain) this.thePanal.mainWindow.MdiParent).tvwNavigator.SelectedNode.Tag = tag;
            }
            catch
            {
                MessageBox.Show("取消激活视图模型" + ((DEViewModel) this.thePanal.mainWindow.Tag).Name + "操作失败！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }

        public void ViewProperty(object sender, EventArgs e)
        {
            bool isInEdit = false;
            if (((DEViewModel) this.thePanal.mainWindow.Tag).Locker.Equals(ClientData.LogonUser.Oid))
            {
                isInEdit = true;
            }
            bool isNew = false;
            if (this.thePanal.isNewAddedNode(((VMNode1) this.thePanal.selectedShape).OID))
            {
                isNew = true;
            }
            new FrmNodeProperty((VMNode1) this.thePanal.selectedShape, this.thePanal.dataDoc, isInEdit, isNew).ShowDialog();
        }

        public void ViewRelationDelete(object sender, EventArgs e)
        {
            this.thePanal.deleteObject();
        }

        public void ViewRelationProperty(object sender, EventArgs e)
        {
            bool isInEdit = false;
            if (((DEViewModel) this.thePanal.mainWindow.Tag).Locker.Equals(ClientData.LogonUser.Oid))
            {
                isInEdit = true;
            }
            new FrmView2ViewProperty((VMLine1) this.thePanal.selectedShape, this.thePanal.dataDoc, isInEdit).ShowDialog();
        }

        public void ViewRemove(object sender, EventArgs e)
        {
            this.thePanal.deleteObject();
        }
    }
}

