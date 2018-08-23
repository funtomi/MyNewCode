    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Admin.NewResponsibility;
    using Thyt.TiPLM.PLL.PPM.Card;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Common.Operation;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling
{
    public partial class FrmBrowse : FormPLM
    {
        private string headClass;
        private bool inDialog;
        private DEBusinessItem selectedItem;
        public CLCardTemplate SelectedTemplate;
        private DEUser user;
        private string virtualClsName;

        public FrmBrowse(bool inDialog, string tmpType, DEUser user)
        {
            this.virtualClsName = "";
            this.InitializeComponent();
            this.inDialog = inDialog;
            if (!inDialog)
            {
                this.pnlMain.Controls.Remove(this.pnlBottom);
            }
            this.headClass = tmpType;
            if (tmpType == "OUTPUTTEMPLATE")
            {
                this.headClass = null;
            }
            this.user = user;
            if (ModelContext.MetaModel.GetClass(this.headClass) != null)
            {
                this.Text = ModelContext.MetaModel.GetClass(this.headClass).Label + "模板列表";
            }
            else
            {
                this.Text = "业务对象输出模板列表";
            }
            this.lvwTemplates.SmallImageList = ClientData.ItemImages.ImgList;
            this.FillTemplateListView(this.headClass, null, this.lvwTemplates, user.Oid, inDialog);
            if ((FrmLogon.PLMProduct == PLMProductName.TiModeler.ToString()) && !inDialog)
            {
                BizItemHandlerEvent.Instance.D_AfterItemCreated = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterItemCreated, new PLMDelegate2(this.AfterItemCreated));
                BizItemHandlerEvent.Instance.D_AfterDeleted = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterDeleted, new PLMDelegate2(this.AfterDeleted));
                BizItemHandlerEvent.Instance.D_AfterItemCloned = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterItemCloned, new PLMDelegate2(this.AfterCloned));
                BizItemHandlerEvent.Instance.D_AfterRenamed = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterRenamed, new PLMDelegate2(this.AfterRenamed));
                BizItemHandlerEvent.Instance.D_AfterCheckIn = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterCheckIn, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterCheckOut = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterReleased = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterReleased, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterRevisionCreated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterRevisionCreated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoCheckOut = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterUndoCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoNewRevision = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterUndoNewRevision, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterIterationUpdated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterIterationUpdated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterMasterUpdated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterMasterUpdated, new PLMBizItemDelegate(this.AfterReleaseDescUpdated));
                BizItemHandlerEvent.Instance.D_AfterAbandon = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterAbandon, new PLMBizItemDelegate(this.AfterReleaseDescUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoAbandon = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterUndoAbandon, new PLMBizItemDelegate(this.AfterItemUpdated));
            }
        }

        public FrmBrowse(bool inDialog, string tmpType, DEUser user, string virtualClsName)
        {
            this.virtualClsName = "";
            this.InitializeComponent();
            this.inDialog = inDialog;
            if (!inDialog)
            {
                this.pnlMain.Controls.Remove(this.pnlBottom);
            }
            this.headClass = tmpType;
            if (tmpType == "OUTPUTTEMPLATE")
            {
                this.headClass = null;
            }
            this.user = user;
            this.virtualClsName = virtualClsName;
            if (ModelContext.MetaModel.GetClass(this.headClass) != null)
            {
                this.Text = ModelContext.MetaModel.GetClass(this.headClass).Label + "模板列表";
            }
            else
            {
                this.Text = "业务对象输出模板列表";
            }
            this.lvwTemplates.SmallImageList = ClientData.ItemImages.ImgList;
            this.FillTemplateListView(this.headClass, null, this.lvwTemplates, user.Oid, inDialog);
            if ((FrmLogon.PLMProduct == PLMProductName.TiModeler.ToString()) && !inDialog)
            {
                BizItemHandlerEvent.Instance.D_AfterItemCreated = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterItemCreated, new PLMDelegate2(this.AfterItemCreated));
                BizItemHandlerEvent.Instance.D_AfterDeleted = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterDeleted, new PLMDelegate2(this.AfterDeleted));
                BizItemHandlerEvent.Instance.D_AfterItemCloned = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterItemCloned, new PLMDelegate2(this.AfterCloned));
                BizItemHandlerEvent.Instance.D_AfterRenamed = (PLMDelegate2) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterRenamed, new PLMDelegate2(this.AfterRenamed));
                BizItemHandlerEvent.Instance.D_AfterCheckIn = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterCheckIn, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterCheckOut = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterReleased = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterReleased, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterRevisionCreated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterRevisionCreated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoCheckOut = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterUndoCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoNewRevision = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterUndoNewRevision, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterIterationUpdated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterIterationUpdated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterMasterUpdated = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterMasterUpdated, new PLMBizItemDelegate(this.AfterReleaseDescUpdated));
                BizItemHandlerEvent.Instance.D_AfterAbandon = (PLMBizItemDelegate) Delegate.Combine(BizItemHandlerEvent.Instance.D_AfterAbandon, new PLMBizItemDelegate(this.AfterReleaseDescUpdated));
            }
        }

        public FrmBrowse(bool inDialog, string strHeadClass, string strMainClass, DEUser user, bool needMainClassAnstor)
        {
            this.virtualClsName = "";
            this.InitializeComponent();
            this.inDialog = inDialog;
            if (!inDialog)
            {
                this.pnlMain.Controls.Remove(this.pnlBottom);
            }
            this.headClass = strHeadClass;
            this.user = user;
            this.Text = (this.headClass == null) ? "" : (ModelContext.MetaModel.GetClass(this.headClass).Label + "模板列表");
            this.lvwTemplates.SmallImageList = ClientData.ItemImages.ImgList;
            this.FillTemplateListView(strHeadClass, strMainClass, this.lvwTemplates, user.Oid, inDialog);
        }

        public FrmBrowse(bool inDialog, string strHeadClass, string strMainClass, DEUser user, bool needMainClassAnstor, string virtualClsName)
        {
            this.virtualClsName = "";
            this.InitializeComponent();
            this.inDialog = inDialog;
            if (!inDialog)
            {
                this.pnlMain.Controls.Remove(this.pnlBottom);
            }
            this.headClass = strHeadClass;
            this.user = user;
            this.virtualClsName = virtualClsName;
            this.Text = (this.headClass == null) ? "" : (ModelContext.MetaModel.GetClass(this.headClass).Label + "模板列表");
            this.lvwTemplates.SmallImageList = ClientData.ItemImages.ImgList;
            this.FillTemplateListView(strHeadClass, strMainClass, this.lvwTemplates, user.Oid, inDialog);
        }

        private void AfterCloned(object sender, PLMOperationArgs e)
        {
            if (((e.BizItems != null) && (e.BizItems.Length != 0)) && (e.BizItems[0].ClassName == "PPCRDTEMPLATE"))
            {
                DEBusinessItem item = e.BizItems[0] as DEBusinessItem;
                string attrValue = (string) item.Iteration.GetAttrValue("TEMPLATETYPE");
                if ((this.headClass == attrValue) || (this.headClass == null))
                {
                    ListViewItem lvi = new ListViewItem(item.Master.Id, ClientData.ItemImages.GetObjectImage(item.Master.ClassName, PLDataModel.GetStateByMasterInfo(item.Master.State, true)));
                    this.FillListViewItem(lvi, item.Master, item.Revision);
                    lvi.Tag = e.Items[0];
                    this.lvwTemplates.Items.Add(lvi);
                    lvi.Selected = true;
                }
            }
        }

        private void AfterDeleted(object sender, PLMOperationArgs e)
        {
            if (((e.BizItems != null) && (e.BizItems.Length != 0)) && (e.BizItems[0].ClassName == "PPCRDTEMPLATE"))
            {
                foreach (ListViewItem item in this.lvwTemplates.Items)
                {
                    if (((DEBusinessItem) item.Tag).Master.Oid == e.BizItems[0].MasterOid)
                    {
                        this.lvwTemplates.Items.Remove(item);
                        break;
                    }
                }
            }
        }

        private void AfterItemCreated(object sender, PLMOperationArgs e)
        {
            if ((e.Items != null) && (((DEBusinessItem) e.Items[0]).ClassName == "PPCRDTEMPLATE"))
            {
                string className = "";
                DEBusinessItem item = e.Items[0] as DEBusinessItem;
                if (item.Iteration == null)
                {
                    className = PLCardTemplate.GetTemplateType(item.Revision, this.user.Oid);
                }
                else
                {
                    className = (string) item.Iteration.GetAttrValue("TEMPLATETYPE");
                }
                if ((this.headClass == className) || (((this.headClass == null) && !ModelContext.MetaModel.IsForm(className)) && !ModelContext.MetaModel.IsCard(className)))
                {
                    int objectImage = ClientData.ItemImages.GetObjectImage(item.Master.ClassName, PLDataModel.GetStateByMasterInfo(item.Master.State, true));
                    ListViewItem lvi = new ListViewItem(item.Master.Id, objectImage);
                    this.FillListViewItem(lvi, item.Master, item.Revision);
                    lvi.Tag = item;
                    this.lvwTemplates.Items.Add(lvi);
                    lvi.Selected = true;
                }
            }
        }

        private void AfterItemUpdated(IBizItem[] bizItems)
        {
            if ((bizItems != null) && (bizItems.Length != 0))
            {
                DEBusinessItem item = PSConvert.ToBizItem(bizItems[0], ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid);
                string attrValue = (string) item.Iteration.GetAttrValue("TEMPLATETYPE");
                if ((this.headClass == attrValue) || (this.headClass == null))
                {
                    foreach (ListViewItem item2 in this.lvwTemplates.Items)
                    {
                        if (((DEBusinessItem) item2.Tag).Master.Oid == item.Master.Oid)
                        {
                            item2.Tag = item;
                            if (!this.inDialog)
                            {
                                this.FillListViewItem(item2, item.Master, item.Revision);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void AfterReleaseDescUpdated(IBizItem[] bizItems)
        {
            if ((bizItems != null) && (bizItems.Length != 0))
            {
                DEBusinessItem item = PSConvert.ToBizItem(bizItems[0], ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid);
                string attrValue = (string) item.Iteration.GetAttrValue("TEMPLATETYPE");
                if (ClientData.mainForm != null)
                {
                    foreach (Form form in ClientData.mainForm.MdiChildren)
                    {
                        if (form is FrmBrowse)
                        {
                            FrmBrowse browse = form as FrmBrowse;
                            if ((browse.headClass == attrValue) || (browse.headClass == null))
                            {
                                foreach (ListViewItem item2 in browse.lvwTemplates.Items)
                                {
                                    if (((DEBusinessItem) item2.Tag).Master.Oid == item.Master.Oid)
                                    {
                                        item2.Tag = item;
                                        if (!browse.inDialog)
                                        {
                                            browse.FillListViewItem(item2, item.Master, item.Revision);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AfterRenamed(object sender, PLMOperationArgs e)
        {
            if (((e.BizItems != null) && (e.BizItems.Length != 0)) && (e.BizItems[0].ClassName == "PPCRDTEMPLATE"))
            {
                string templateType = "";
                DEBusinessItem item = PSConvert.ToBizItem(e.BizItems[0], ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid);
                if (item.Iteration == null)
                {
                    templateType = PLCardTemplate.GetTemplateType(item.Revision, this.user.Oid);
                }
                else
                {
                    templateType = (string) item.Iteration.GetAttrValue("TEMPLATETYPE");
                }
                if ((this.headClass == templateType) || (this.headClass == null))
                {
                    foreach (ListViewItem item2 in this.lvwTemplates.Items)
                    {
                        if (((DEBusinessItem) item2.Tag).Master.Oid == item.Master.Oid)
                        {
                            item2.Tag = e.BizItems[0];
                            if (!this.inDialog)
                            {
                                this.FillListViewItem(item2, item.Master, item.Revision);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SelectTemplate();
        }

        private ContextMenuStrip BuildContextMenu(DEBusinessItem item)
        {
            ContextMenuStrip strip = new ContextMenuStrip();
            ToolStripMenuItem item2 = null;
            ToolStripSeparator separator = null;
            item2 = new ToolStripMenuItem("刷新列表") {
                Name = "&Fresh",
                Image = PLMImageList.GetIcon("ICO_REFRESH").ToBitmap()
            };
            item2.Click += new EventHandler(this.mniFreshList_Click);
            strip.Items.Add(item2);
            item2 = new ToolStripMenuItem("浏览") {
                Name = "&Browse",
                Image = PLMImageList.GetIcon("ICO_PPM_BROWSE").ToBitmap()
            };
            item2.Click += new EventHandler(this.mniBrowse_Click);
            strip.Items.Add(item2);
            item2 = new ToolStripMenuItem("属性") {
                Name = "&Property",
                Image = PLMImageList.GetIcon("ICO_PPM_PPCARDTMPPROPERTY").ToBitmap()
            };
            item2.Click += new EventHandler(this.mniProperty_Click);
            strip.Items.Add(item2);
            if (!this.inDialog)
            {
                if (item.Master.Holder == this.user.Oid)
                {
                    separator = new ToolStripSeparator();
                    strip.Items.Add(separator);
                    item2 = new ToolStripMenuItem("编辑模板") {
                        Name = "E&dit file",
                        Image = PLMImageList.GetIcon("ICO_PPM_PPCARDTEMPLATE").ToBitmap()
                    };
                    item2.Click += new EventHandler(this.mniEditFile_Click);
                    strip.Items.Add(item2);
                    item2 = new ToolStripMenuItem("修改属性") {
                        Name = "Edit property",
                        Image = PLMImageList.GetIcon("ICO_PPM_PPCARDTMPPROPERTY").ToBitmap()
                    };
                    item2.Click += new EventHandler(this.mniEditProperty_Click);
                    strip.Items.Add(item2);
                }
                else if ((item.State == ItemState.CheckIn) || (item.State == ItemState.Release))
                {
                    separator = new ToolStripSeparator();
                    strip.Items.Add(separator);
                    item2 = new ToolStripMenuItem("更新属性") {
                        Name = "Update property",
                        Image = PLMImageList.GetIcon("ICO_PPM_PPCARDTMPPROPERTY").ToBitmap()
                    };
                    item2.Click += new EventHandler(this.mniUpdateProperty_Click);
                    strip.Items.Add(item2);
                }
                List<object> items = new List<object> {
                    item
                };
                PLMOperationArgs args = new PLMOperationArgs(FrmLogon.PLMProduct, PLMLocation.PPCardTemplateList.ToString(), items, ClientData.UserGlobalOption.CloneAsGlobal());
                ToolStripItem[] contextMenuItems = MenuBuilder.Instance.GetContextMenuItems(args);
                foreach (ToolStripItem item3 in contextMenuItems)
                {
                    if (((item.LastRevision == 1) && (item3.Name == "PLM30_RENAME")) && (item.Master.Holder == this.user.Oid))
                    {
                        strip.Items.Add(item3);
                    }
                }
                separator = new ToolStripSeparator();
                strip.Items.Add(separator);
                foreach (ToolStripItem item4 in contextMenuItems)
                {
                    if (item4.Name != "PLM30_RENAME")
                    {
                        if (item4.Name == "PLM30_DELETE")
                        {
                            if (item.CanEdit(ClientData.LogonUser.Oid) || ((item.Master.State != ItemState.CheckOut) && (item.Master.LastRevision == item.Revision.Revision)))
                            {
                                strip.Items.Add(item4);
                            }
                        }
                        else
                        {
                            strip.Items.Add(item4);
                        }
                    }
                }
            }
            return strip;
        }

        protected override void Dispose(bool disposing)
        {
            if ((FrmLogon.PLMProduct == PLMProductName.TiModeler.ToString()) && !this.inDialog)
            {
                BizItemHandlerEvent.Instance.D_AfterItemCreated = (PLMDelegate2) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterItemCreated, new PLMDelegate2(this.AfterItemCreated));
                BizItemHandlerEvent.Instance.D_AfterDeleted = (PLMDelegate2) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterDeleted, new PLMDelegate2(this.AfterDeleted));
                BizItemHandlerEvent.Instance.D_AfterItemCloned = (PLMDelegate2) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterItemCloned, new PLMDelegate2(this.AfterCloned));
                BizItemHandlerEvent.Instance.D_AfterRenamed = (PLMDelegate2) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterRenamed, new PLMDelegate2(this.AfterRenamed));
                BizItemHandlerEvent.Instance.D_AfterCheckIn = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterCheckIn, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterCheckOut = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterReleased = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterReleased, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterRevisionCreated = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterRevisionCreated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoCheckOut = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterUndoCheckOut, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterUndoNewRevision = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterUndoNewRevision, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterIterationUpdated = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterIterationUpdated, new PLMBizItemDelegate(this.AfterItemUpdated));
                BizItemHandlerEvent.Instance.D_AfterMasterUpdated = (PLMBizItemDelegate) Delegate.Remove(BizItemHandlerEvent.Instance.D_AfterMasterUpdated, new PLMBizItemDelegate(this.AfterReleaseDescUpdated));
            }
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void EditFile(bool readOnly)
        {
            if (this.lvwTemplates.SelectedItems.Count > 0)
            {
                DEBusinessItem tag = (DEBusinessItem) this.lvwTemplates.SelectedItems[0].Tag;
                if (!readOnly && (tag.Master.Holder != this.user.Oid))
                {
                    MessageBoxPLM.Show("请先检出该对象。", "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    new OpenTemplateUtil(this.user, readOnly).OpenTemplateEx(tag);
                }
            }
        }

        private void EditProperties(bool readOnly)
        {
            if (this.lvwTemplates.SelectedItems.Count != 1)
            {
                MessageBoxPLM.Show("请选中一个模板。", "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                DEBusinessItem tag = (DEBusinessItem) this.lvwTemplates.SelectedItems[0].Tag;
                if (!readOnly && (tag.Master.Holder != this.user.Oid))
                {
                    MessageBoxPLM.Show("请先检出该对象。", "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cursor current = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    FrmProperty property = null;
                    try
                    {
                        property = new FrmProperty(this.user.Oid, readOnly, false, tag.Master.Oid);
                    }
                    catch (Exception exception)
                    {
                        MessageBoxPLM.Show("载入属性失败。\n" + exception.Message, "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    finally
                    {
                        Cursor.Current = current;
                    }
                    property.ShowDialog();
                }
            }
        }

        private void FillListViewItem(ListViewItem lvi, DEItemMaster2 mas, DEItemRevision2 rev)
        {
            lvi.SubItems.Clear();
            lvi.Text = mas.Id;
            lvi.SubItems.Add(rev.Revision.ToString());
            lvi.SubItems.Add(rev.LastIteration.ToString());
            lvi.SubItems.Add(this.GetRealeaseDescLabel(rev));
            lvi.SubItems.Add(mas.StateLabel);
            lvi.SubItems.Add(PrincipalRepository.GetPrincipalName(mas.Holder));
            lvi.SubItems.Add(PrincipalRepository.GetPrincipalName(rev.Creator));
            lvi.SubItems.Add(rev.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            if (mas.State == ItemState.Release)
            {
                lvi.SubItems.Add(PrincipalRepository.GetPrincipalName(rev.Releaser));
                lvi.SubItems.Add(rev.ReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                lvi.SubItems.Add("");
                lvi.SubItems.Add("");
            }
        }

        private void FillListViewItemSimple(ListViewItem item, DEItemMaster2 mas, DEItemRevision2 rev)
        {
            item.SubItems.Clear();
            item.Text = mas.Id;
            this.lvwTemplates.SetColumnSortFormat(2, SortedListViewFormatType.String);
            if (this.lvwTemplates.Columns.Contains(this.colIteration))
            {
                this.lvwTemplates.Columns.Remove(this.colIteration);
            }
            if (this.lvwTemplates.Columns.Contains(this.colState))
            {
                this.lvwTemplates.Columns.Remove(this.colState);
            }
            if (this.lvwTemplates.Columns.Contains(this.colHolder))
            {
                this.lvwTemplates.Columns.Remove(this.colHolder);
            }
            item.SubItems.Add(rev.Revision.ToString());
            item.SubItems.Add(this.GetRealeaseDescLabel(rev));
            item.SubItems.Add(PrincipalRepository.GetPrincipalName(rev.Creator));
            item.SubItems.Add(rev.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            item.SubItems.Add(PrincipalRepository.GetPrincipalName(rev.Releaser));
            item.SubItems.Add(rev.ReleaseTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void FillTemplateListView(bool isDialog, ArrayList masters, ArrayList revs, SortableListView lvwTemplates)
        {
            if (masters != null)
            {
                for (int i = 0; i < masters.Count; i++)
                {
                    DEItemMaster2 mas = masters[i] as DEItemMaster2;
                    DEItemRevision2 rev = revs[i] as DEItemRevision2;
                    if (isDialog)
                    {
                        int objectImage = ClientData.ItemImages.GetObjectImage(mas.ClassName, "release");
                        ListViewItem item = new ListViewItem(mas.Id, objectImage);
                        this.FillListViewItemSimple(item, mas, rev);
                        item.Tag = new DEBusinessItem(mas, rev, null);
                        lvwTemplates.Items.Add(item);
                    }
                    else
                    {
                        int imageIndex = ClientData.ItemImages.GetObjectImage(mas.ClassName, PLDataModel.GetStateByMasterInfo(mas.State, true));
                        ListViewItem lvi = new ListViewItem(mas.Id, imageIndex);
                        this.FillListViewItem(lvi, mas, rev);
                        lvi.Tag = new DEBusinessItem(mas, rev, null);
                        lvwTemplates.Items.Add(lvi);
                    }
                }
                this.lvwTemplates.SetColumnSortFormat(1, SortedListViewFormatType.Numeric);
                if (!isDialog)
                {
                    this.lvwTemplates.SetColumnSortFormat(2, SortedListViewFormatType.Numeric);
                }
            }
        }

        public void FillTemplateListView(string headClassName, string mainClassName, SortableListView lvwTemplates, Guid uOid, bool isDialog)
        {
            lvwTemplates.Items.Clear();
            DEItemMaster2[] masters = null;
            DEItemRevision2[] revs = null;
            ArrayList mastersList = new ArrayList();
            ArrayList revsList = new ArrayList();
            string[] fieldNames = null;
            string[] operators = null;
            object[] values = null;
            PLMDataType[] attrTypes = null;
            if (mainClassName == null)
            {
                if ((headClassName == null) || (!ModelContext.MetaModel.IsCard(headClassName) && !ModelContext.MetaModel.IsForm(headClassName)))
                {
                    if (headClassName == null)
                    {
                        this.GetItemMasRev(out masters, out revs, null, null, null, null);
                    }
                    else
                    {
                        if (this.virtualClsName != "")
                        {
                            fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE", "I.PLM_VIRTUALCLASSNAME" };
                            operators = new string[] { "=", "=", "=" };
                            values = new object[] { headClassName, 'R', this.virtualClsName };
                            attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char, PLMDataType.String };
                            this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                        }
                        if ((this.virtualClsName == "") || (masters.Length == 0))
                        {
                            fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE" };
                            operators = new string[] { "=", "=" };
                            values = new object[] { headClassName, 'R' };
                            attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char };
                            this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                        }
                    }
                    if (masters != null)
                    {
                        for (int i = 0; i < masters.Length; i++)
                        {
                            if (this.IsOutPutTemplate(revs[i], this.user.Oid))
                            {
                                int objectImage = ClientData.ItemImages.GetObjectImage(masters[i].ClassName, PLDataModel.GetStateByMasterInfo(masters[i].State, true));
                                ListViewItem lvi = new ListViewItem(masters[i].Id, objectImage);
                                this.FillListViewItem(lvi, masters[i], revs[i]);
                                lvi.Tag = new DEBusinessItem(masters[i], revs[i], null);
                                lvwTemplates.Items.Add(lvi);
                            }
                        }
                        this.lvwTemplates.SetColumnSortFormat(1, SortedListViewFormatType.Numeric);
                        this.lvwTemplates.SetColumnSortFormat(2, SortedListViewFormatType.Numeric);
                    }
                    return;
                }
                if (!isDialog)
                {
                    fieldNames = new string[] { "I.PLM_TEMPLATETYPE" };
                    operators = new string[] { "=" };
                    values = new object[] { headClassName };
                    attrTypes = new PLMDataType[] { PLMDataType.String };
                    this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                }
                else
                {
                    if (this.virtualClsName != "")
                    {
                        fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE", "I.PLM_VIRTUALCLASSNAME" };
                        operators = new string[] { "=", "=", "=" };
                        values = new object[] { headClassName, 'R', this.virtualClsName };
                        attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char, PLMDataType.String };
                        this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                    }
                    if ((this.virtualClsName == "") || (masters.Length == 0))
                    {
                        fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE" };
                        operators = new string[] { "=", "=" };
                        values = new object[] { headClassName, 'R' };
                        attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char };
                        this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                    }
                }
                mastersList.AddRange(masters);
                revsList.AddRange(revs);
            }
            else if (headClassName == null)
            {
                this.GetMasRevList(mainClassName, mastersList, revsList);
            }
            else
            {
                this.GetMasRevList(mainClassName, mastersList, revsList);
                if (this.virtualClsName != "")
                {
                    fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE", "I.PLM_VIRTUALCLASSNAME" };
                    operators = new string[] { "=", "=", "=" };
                    values = new object[] { headClassName, 'R', this.virtualClsName };
                    attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char, PLMDataType.String };
                    this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                }
                if ((this.virtualClsName == "") || (masters.Length == 0))
                {
                    fieldNames = new string[] { "I.PLM_TEMPLATETYPE", "M.PLM_M_STATE" };
                    operators = new string[] { "=", "=" };
                    values = new object[] { headClassName, 'R' };
                    attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char };
                    this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                }
                for (int j = 0; j < revs.Length; j++)
                {
                    if (!this.HasSameTemplate(revs[j], revsList))
                    {
                        mastersList.Add(masters[j]);
                        revsList.Add(revs[j]);
                    }
                }
            }
            this.FillTemplateListView(isDialog, mastersList, revsList, lvwTemplates);
        }

        private void FrmBrowse_Load(object sender, EventArgs e)
        {
            if (this.inDialog && !this.grpHistory.Visible)
            {
                if (this.lvwTemplates.Items.Count == 1)
                {
                    base.Visible = false;
                    this.lvwTemplates.Items[0].Selected = true;
                    this.btnOK_Click(null, null);
                }
                else if (this.lvwTemplates.Items.Count == 0)
                {
                    base.Visible = false;
                    MessageBoxPLM.Show("没有可供选择的模板。");
                    base.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void GetItemMasRev(out DEItemMaster2[] masters, out DEItemRevision2[] revs, string[] fieldNames, string[] operators, object[] values, PLMDataType[] attrTypes)
        {
            masters = null;
            revs = null;
            try
            {
                PLItem.Agent.GetItemMasterRevisions(this.user.Oid, "PPCRDTEMPLATE", fieldNames, operators, values, attrTypes, out masters, out revs);
            }
            catch (Exception exception)
            {
                PrintException.Print(new PLMException("获取对象的MasterRevisions失败！\n", exception));
            }
        }

        private void GetMasRevList(string mainClassName, ArrayList mastersList, ArrayList revsList)
        {
            DEItemMaster2[] masters = null;
            DEItemRevision2[] revs = null;
            string[] fieldNames = null;
            string[] operators = null;
            object[] values = null;
            PLMDataType[] attrTypes = null;
            ArrayList allAncestorName = CLCard.GetAllAncestorName(mainClassName);
            for (int i = allAncestorName.Count - 1; i >= 0; i--)
            {
                fieldNames = new string[] { "M.PLM_R_RELEASEDESC", "M.PLM_M_STATE" };
                operators = new string[] { " LIKE ", "=" };
                values = new object[] { "%:" + allAncestorName[i].ToString(), 'R' };
                attrTypes = new PLMDataType[] { PLMDataType.String, PLMDataType.Char };
                this.GetItemMasRev(out masters, out revs, fieldNames, operators, values, attrTypes);
                mastersList.AddRange(masters);
                revsList.AddRange(revs);
            }
        }

        private string GetRealeaseDescLabel(DEItemRevision2 rev)
        {
            if ((rev.ReleaseDesc == null) || (rev.ReleaseDesc == ""))
            {
                return "";
            }
            if (rev.ReleaseDesc.IndexOf(":") == -1)
            {
                return rev.ReleaseDesc;
            }
            string headOrMainFromRevDesc = PLCardTemplate.GetHeadOrMainFromRevDesc(rev, 0);
            string name = PLCardTemplate.GetHeadOrMainFromRevDesc(rev, 1);
            if (ModelContext.MetaModel.GetClass(headOrMainFromRevDesc) != null)
            {
                headOrMainFromRevDesc = ModelContext.MetaModel.GetClassLabel(headOrMainFromRevDesc);
            }
            if (ModelContext.MetaModel.GetClass(name) != null)
            {
                name = ModelContext.MetaModel.GetClassLabel(name);
            }
            return (headOrMainFromRevDesc + ":" + name);
        }

        private bool HasSameTemplate(DEItemRevision2 rev, ArrayList revsList)
        {
            for (int i = 0; i < revsList.Count; i++)
            {
                if (rev.Oid == ((DEItemRevision2) revsList[i]).Oid)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOutPutTemplate(DEItemRevision2 rev, Guid uOid)
        {
            if (rev.ReleaseDesc == null)
            {
                return false;
            }
            if (rev.ReleaseDesc.ToString().IndexOf(":") == -1)
            {
                return false;
            }
            string headOrMainFromRevDesc = PLCardTemplate.GetHeadOrMainFromRevDesc(rev, 0);
            return ((headOrMainFromRevDesc == "") || (!ModelContext.MetaModel.IsCard(headOrMainFromRevDesc) && !ModelContext.MetaModel.IsForm(headOrMainFromRevDesc)));
        }

        private void lvwTemplates_ItemActivate(object sender, EventArgs e)
        {
            if (this.inDialog)
            {
                this.SelectTemplate();
            }
            else
            {
                bool readOnly = true;
                DEBusinessItem tag = this.lvwTemplates.SelectedItems[0].Tag as DEBusinessItem;
                if ((tag.Master.State == ItemState.CheckOut) && (tag.Holder == this.user.Oid))
                {
                    readOnly = false;
                }
                this.EditFile(readOnly);
            }
        }

        private void lvwTemplates_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (this.lvwTemplates.SelectedItems.Count == 1))
            {
                DEBusinessItem tag = (DEBusinessItem) this.lvwTemplates.SelectedItems[0].Tag;
                if (tag.Iteration == null)
                {
                    tag = PLItem.Agent.GetBizItemByMaster(tag.Master.Oid, tag.Revision.Revision, PLOption.GetUserGlobalOption(this.user.Oid).CurView, this.user.Oid, BizItemMode.BizItem) as DEBusinessItem;
                }
                this.BuildContextMenu(tag).Show(this.lvwTemplates, new Point(e.X, e.Y));
            }
        }

        private void lvwTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.inDialog && (this.lvwTemplates.SelectedItems.Count > 0))
            {
                this.selectedItem = (DEBusinessItem) this.lvwTemplates.SelectedItems[0].Tag;
                if (this.SelectOldRevision)
                {
                    this.numRevisions.Maximum = Convert.ToDecimal(this.selectedItem.Master.LastRevision);
                    this.numRevisions.Value = this.numRevisions.Maximum;
                }
            }
        }

        private void mniBrowse_Click(object sender, EventArgs e)
        {
            this.EditFile(true);
        }

        private void mniEditFile_Click(object sender, EventArgs e)
        {
            this.EditFile(false);
        }

        private void mniEditProperty_Click(object sender, EventArgs e)
        {
            this.EditProperties(false);
        }

        private void mniFreshList_Click(object sender, EventArgs e)
        {
            this.FillTemplateListView(this.headClass, null, this.lvwTemplates, this.user.Oid, this.inDialog);
        }

        private void mniProperty_Click(object sender, EventArgs e)
        {
            this.EditProperties(true);
        }

        private void mniUpdateProperty_Click(object sender, EventArgs e)
        {
            this.UpdatePropertiesDirectely();
        }

        private void SelectTemplate()
        {
            if (this.lvwTemplates.SelectedItems.Count == 0)
            {
                MessageBoxPLM.Show("尚未选中模板。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DEBusinessItem item = null;
                if (this.SelectOldRevision)
                {
                    if (this.selectedItem == null)
                    {
                        MessageBoxPLM.Show("尚未选中模板。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    int revNum = Convert.ToInt32(this.numRevisions.Value);
                    item = PLItem.Agent.GetBizItemByMaster(this.selectedItem.Master.Oid, revNum, PLOption.GetUserGlobalOption(this.user.Oid).CurView, this.user.Oid, BizItemMode.BizItem) as DEBusinessItem;
                    this.SelectedTemplate = new CLCardTemplate(this.user.Oid, false, true, item);
                }
                else
                {
                    item = PLItem.Agent.GetBizItemByMaster(this.selectedItem.Master.Oid, 0, PLOption.GetUserGlobalOption(ClientData.LogonUser.Oid).CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                    this.SelectedTemplate = new CLCardTemplate(this.user.Oid, false, true, item);
                }
                try
                {
                    this.SelectedTemplate.CheckProperties();
                }
                catch (Exception exception)
                {
                    MessageBoxPLM.Show("严重警告：该模板有关属性填写有错，不能选择该模板，请联系定制人员更正。\n详细信息是：" + exception.Message, "PPM警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        private bool TemplateTypeMatch(string className, string masterId, DEItemRevision2 rev, Guid uOid)
        {
            if (className == "PPCARD")
            {
                return true;
            }
            try
            {
                if (className == PLCardTemplate.GetTemplateType(rev, uOid))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        private void UpdatePropertiesDirectely()
        {
            if (this.lvwTemplates.SelectedItems.Count != 1)
            {
                MessageBoxPLM.Show("请选中一个模板。", "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                DEBusinessItem tag = (DEBusinessItem) this.lvwTemplates.SelectedItems[0].Tag;
                Cursor current = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                FrmProperty property = null;
                try
                {
                    property = new FrmProperty(this.user.Oid, false, true, tag.Master.Oid);
                }
                catch (Exception exception)
                {
                    MessageBoxPLM.Show("载入属性失败。\n" + exception.Message, "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                finally
                {
                    Cursor.Current = current;
                }
                property.ShowDialog();
            }
        }

        public bool SelectOldRevision
        {
            get {
                return this.grpHistory.Visible;
            }
            set
            {
                this.grpHistory.Visible = value;
            }
        }
    }
}

