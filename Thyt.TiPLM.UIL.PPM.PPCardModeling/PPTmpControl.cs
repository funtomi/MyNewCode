using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Thyt.TiPLM.CLT.UIL.DeskLib.Menus;
using Thyt.TiPLM.DEL.Admin.DataModel;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Product.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class PPTmpControl : TabControl {
        public PPTmpControl CompareControl;
        private FrmModeling frmParent;
        private CLCardTemplate m_tp;
        public PPTmpPage pageCover;
        public PPTmpPage pageMainPage;
        public PPTmpPage pageNextPage;

        public PPTmpControl(IContainer container) {
            container.Add(this);
            this.InitializeComponent();
        }

        public PPTmpControl(CLCardTemplate tp, FrmModeling parent) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.frmParent = parent;
            this.InitializePages();
        }

        public PPTmpControl(DEBusinessItem item, DEUser user, bool readOnly) {
            this.InitializeComponent();
            this.m_tp = new CLCardTemplate(user.Oid, false, readOnly, item);
            try {
                this.m_tp.CheckProperties();
            } catch (Exception exception) {
                MessageBox.Show("警告：模板范围和表中区域没有正确设置，请用编辑界面中的快捷菜单命令更正。\n详细信息是：" + exception.Message, "PPM警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.InitializePages();
        }

        private ContextMenu BuildContextMenu() {
            ContextMenu menu = new ContextMenu();
            MenuItemEx item = null;
            if (!this.m_tp.ReadOnly) {
                item = new MenuItemEx("Save", "保存", null, null) {
                    Icon = PLMImageList.GetIcon("ICO_ENV_SAVE").ToBitmap()
                };
                item.Click += new EventHandler(this.OnSave);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("-", "-", null, null);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("Import…", "当前页导入…", null, null) {
                    Icon = PLMImageList.GetIcon("ICO_PPM_PPCARDIMPORT").ToBitmap()
                };
                item.Click += new EventHandler(this.OnImport);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("ResetViewRange", "重新设置模板区域", null, null);
                item.Click += new EventHandler(this.OnResetViewRange);
                menu.MenuItems.Add(item);
            }
            item = new MenuItemEx("Export…", "导出当前页…", null, null) {
                Icon = PLMImageList.GetIcon("ICO_PPM_PPCARDEXPORT").ToBitmap()
            };
            item.Click += new EventHandler(this.OnExport);
            menu.MenuItems.Add(item);
            if (!this.m_tp.ReadOnly) {
                item = new MenuItemEx("Page setup…", "页面设置…", null, null);
                item.Click += new EventHandler(this.OnPageSetup);
                menu.MenuItems.Add(item);
            }
            item = new MenuItemEx("Print preview", "当前页打印预览", null, null) {
                Icon = PLMImageList.GetIcon("ICO_PPM_PRINTPREVIEW").ToBitmap()
            };
            item.Click += new EventHandler(this.OnPrintPreview);
            menu.MenuItems.Add(item);
            if (!this.m_tp.ReadOnly) {
                item = new MenuItemEx("-", "-", null, null);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("Set as page range", "设为" + base.SelectedTab.Text + "有效区域", null, null);
                item.Click += new EventHandler(this.OnSetPageRange);
                menu.MenuItems.Add(item);
                item = new MenuItemEx("Set as mid-table range", "设为" + base.SelectedTab.Text + "表中区域", null, null);
                item.Click += new EventHandler(this.OnSetMidTable);
                menu.MenuItems.Add(item);
            }
            item = new MenuItemEx("-", "-", null, null);
            menu.MenuItems.Add(item);
            if (ModelContext.MetaModel.IsCard(this.m_tp.TemplateType) && this.m_tp.CheckPageMidRegion()) {
                bool flag = false;
                if (((this.m_tp.Item.State == ItemState.CheckOut) && (this.m_tp.Item.Holder != this.m_tp.UserOid)) || (this.m_tp.Item.State == ItemState.Abandoned)) {
                    flag = true;
                }
                if (!flag || (this.m_tp.AutoNumber != null)) {
                    item = new MenuItemEx("AutoNumber…", "自动编号规则设置…", null, null) {
                        Icon = PLMImageList.GetIcon("ICO_PPM_AUTONUMBER").ToBitmap()
                    };
                    item.Click += new EventHandler(this.OnAutoNumber);
                    menu.MenuItems.Add(item);
                }
                if ((this.m_tp.AutoNumber != null) && !flag) {
                    item = new MenuItemEx("Clear AutoNumber", "清除自动编号规则", null, null);
                    item.Click += new EventHandler(this.OnClearAutoNumber);
                    menu.MenuItems.Add(item);
                }
                if (!flag || (this.m_tp.AutoNumber != null)) {
                    item = new MenuItemEx("-", "-", null, null);
                    menu.MenuItems.Add(item);
                }
                if (!this.m_tp.ReadOnly || (this.m_tp.ReadOnly && (this.m_tp.DataHiddenCol != null))) {
                    item = new MenuItemEx("DataHiddenCol…", "数据隐藏列设置…", null, null);
                    item.Click += new EventHandler(this.OnSetDataHiddenCol);
                    menu.MenuItems.Add(item);
                }
                if (!this.m_tp.ReadOnly) {
                    item = new MenuItemEx("Clear DataHiddenCol", "清除数据隐藏列设置", null, null);
                    item.Click += new EventHandler(this.OnClearDataHiddenCol);
                    menu.MenuItems.Add(item);
                }
                if (!this.m_tp.ReadOnly || (this.m_tp.ReadOnly && (this.m_tp.DataHiddenCol != null))) {
                    item = new MenuItemEx("-", "-", null, null);
                    menu.MenuItems.Add(item);
                }
            }
            if (base.Alignment == TabAlignment.Top) {
                item = new MenuItemEx("Change label docking", "标签置底", null, null);
                item.Click += new EventHandler(this.OnChangeLabelDocking);
                menu.MenuItems.Add(item);
            }
            if (base.Alignment == TabAlignment.Bottom) {
                item = new MenuItemEx("Change label docking", "标签置顶", null, null);
                item.Click += new EventHandler(this.OnChangeLabelDocking);
                menu.MenuItems.Add(item);
            }
            if (this.frmParent != null) {
                item = new MenuItemEx("Close", "关闭", null, null);
                item.Click += new EventHandler(this.OnClose);
                menu.MenuItems.Add(item);
            }
            return menu;
        }

        private void Export(string exportFileName) {
            this.GetTmpPage(base.SelectedTab.Text).Export(exportFileName, false);
        }

        private string GetLatestHeadMainInfo() {
            string mainItem = this.pageMainPage.GetMainItem();
            string headOrMainFromRevDesc = PLCardTemplate.GetHeadOrMainFromRevDesc(this.m_tp.Item.Revision, 0);
            if (ModelContext.MetaModel.IsCard(this.m_tp.TemplateType) || ModelContext.MetaModel.IsForm(this.m_tp.TemplateType)) {
                headOrMainFromRevDesc = this.m_tp.TemplateType;
            }
            return (headOrMainFromRevDesc + ":" + mainItem);
        }

        private PPTmpPage GetTmpPage(string tabText) {
            PPTmpPage page = null;
            string str = tabText;
            if (str == null) {
                return page;
            }
            if (str != "封面") {
                if (str != "首页") {
                    if (str != "续页") {
                        return page;
                    }
                    return this.pageNextPage;
                }
            } else {
                return this.pageCover;
            }
            return this.pageMainPage;
        }

        private void InitializePages() {
            if (this.m_tp.HasCover) {
                TabPage page = new TabPage("封面");
                if (File.Exists(this.m_tp.CoverPath)) {
                    this.pageCover = new PPTmpPage(this.m_tp, this.m_tp.CoverPath, this.m_tp.RowCountOfCover, this.m_tp.ColCountOfCover, this.m_tp.MidBeginOfCover, this.m_tp.MidEndOfCover, this.m_tp.ReadOnly);
                } else {
                    this.pageCover = new PPTmpPage(this.m_tp, this.m_tp.RowCountOfCover, this.m_tp.ColCountOfCover, this.m_tp.MidBeginOfCover, this.m_tp.MidEndOfCover, this.m_tp.ReadOnly, this.m_tp.CoverText);
                }
                page.Controls.Add(this.pageCover);
                base.TabPages.Add(page);
            }
            if (this.m_tp.HasMainPage) {
                TabPage page2 = new TabPage("首页");
                if (File.Exists(this.m_tp.MainPagePath)) {
                    this.pageMainPage = new PPTmpPage(this.m_tp, this.m_tp.MainPagePath, this.m_tp.RowCountOfMain, this.m_tp.ColCountOfMain, this.m_tp.MidBeginOfMain, this.m_tp.MidEndOfMain, this.m_tp.ReadOnly);
                } else {
                    this.pageMainPage = new PPTmpPage(this.m_tp, this.m_tp.RowCountOfMain, this.m_tp.ColCountOfMain, this.m_tp.MidBeginOfMain, this.m_tp.MidEndOfMain, this.m_tp.ReadOnly, this.m_tp.MainPageText);
                }
                page2.Controls.Add(this.pageMainPage);
                base.TabPages.Add(page2);
            }
            if (this.m_tp.HasNextPage) {
                TabPage page3 = new TabPage("续页");
                if (File.Exists(this.m_tp.NextPagePath)) {
                    this.pageNextPage = new PPTmpPage(this.m_tp, this.m_tp.NextPagePath, this.m_tp.RowCountOfNext, this.m_tp.ColCountOfNext, this.m_tp.MidBeginOfNext, this.m_tp.MidEndOfNext, this.m_tp.ReadOnly);
                } else {
                    this.pageNextPage = new PPTmpPage(this.m_tp, this.m_tp.RowCountOfNext, this.m_tp.ColCountOfNext, this.m_tp.MidBeginOfNext, this.m_tp.MidEndOfNext, this.m_tp.ReadOnly, this.m_tp.NextPageText);
                }
                page3.Controls.Add(this.pageNextPage);
                base.TabPages.Add(page3);
            }
        }

        private void OnAutoNumber(object sender, EventArgs e) {
            try {
                DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute("PPCRDTEMPLATE", "AUTONUMBER");
                if (attribute == null) {
                    MessageBox.Show("请先为“卡片模板”类定制“自动编号规则（AUTONUMBER）”属性，长度应不小于100。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else {
                    DlgAutoNumber number = new DlgAutoNumber(this.m_tp, true);
                    if (number.ShowDialog() == DialogResult.OK) {
                        string str = PPCardCompiler.CreateXML(number.NumberFormat);
                        if (str.Length > attribute.Size) {
                            MessageBox.Show(string.Concat(new object[] { "设置自动编号规则形成的脚本长度 ", str.Length.ToString(), " 超过了“卡片模板”类属性“AUTONUMBER”的长度 ", attribute.Size, " 。\n请修改该属性的长度，否则模板将不会保存该规则。" }), "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } else {
                            this.m_tp.AutoNumber = str;
                            this.m_tp.IsSaved = false;
                        }
                    }
                }
            } catch (Exception exception) {
                MessageBox.Show("自动编号规则设置出错，请重试。具体原因为：\n" + exception.Message, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnChangeHeadMainItem(object sender, EventArgs e) {
            if (!ModelContext.MetaModel.IsForm(this.m_tp.TemplateType) && !ModelContext.MetaModel.IsCard(this.m_tp.TemplateType)) {
                this.m_tp.Item.Revision.ReleaseDesc = this.GetLatestHeadMainInfo();
                DlgChangeHeadMainItem item = new DlgChangeHeadMainItem(this.m_tp);
                if (item.ShowDialog() == DialogResult.OK) {
                    this.m_tp.IsSaved = false;
                }
            }
        }

        private void OnChangeLabelDocking(object sender, EventArgs e) {
            if (base.Alignment == TabAlignment.Bottom) {
                base.Alignment = TabAlignment.Top;
            } else if (base.Alignment == TabAlignment.Top) {
                base.Alignment = TabAlignment.Bottom;
            }
        }

        private void OnClearAutoNumber(object sender, EventArgs e) {
            if ((this.m_tp.Item.State == ItemState.CheckIn) || (this.m_tp.Item.State == ItemState.Release)) {
                if (MessageBox.Show("卡片处于" + this.m_tp.Item.StateLabel + "状态，选择“是”，将清除模板的自动编号规则，是否继续清除？", "卡片模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    this.m_tp.AutoNumber = null;
                    PLItem.UpdateItemIterationDirectly(this.m_tp.Item, this.m_tp.UserOid, false);
                }
            } else {
                this.m_tp.AutoNumber = null;
                this.m_tp.IsSaved = false;
            }
        }

        private void OnClearDataHiddenCol(object sender, EventArgs e) {
            this.m_tp.DataHiddenCol = null;
            this.m_tp.IsSaved = false;
        }

        private void OnClose(object sender, EventArgs e) {
            this.frmParent.Close();
        }

        private void OnExport(object sender, EventArgs e) {
            int num = Convert.ToInt32(PPMCommon.GetRegistryKey());
            SaveFileDialog dialog = new SaveFileDialog {
                Title = "当前页导出为Excel文件",
                FileName = @"C:\" + this.m_tp.TemplateID
            };
            string str = "所有文件 (*.*)|*.*";
            string str2 = ".xls";
            if (num < 12) {
                str = "Excel工作簿 (*.xls)|*.xls|" + str;
            } else {
                str = "Excel工作簿 (*.xlsx)|*.xlsx|Excel 97-2003 工作簿 (*.xls)|*.xls|" + str;
                str2 = ".xlsx";
            }
            dialog.Filter = str;
            dialog.DefaultExt = str2;
            if (dialog.ShowDialog() == DialogResult.OK) {
                Cursor current = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                try {
                    this.Export(dialog.FileName);
                    this.m_tp.IsSaved = false;
                } catch (Exception exception) {
                    MessageBox.Show("导出到Excel文档失败，请重试。\n" + exception.Message, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } finally {
                    Cursor.Current = current;
                }
            }
        }

        private void OnImport(object sender, EventArgs e) {
            int num = Convert.ToInt32(PPMCommon.GetRegistryKey());
            string str = "HTML文件 (*.htm;*.html)|*.htm;*.html|所有文件 (*.*)|*.*";
            if (num < 12) {
                str = "Excel工作簿 (*.xls)|*.xls|" + str;
            } else {
                str = "Excel工作簿 (*.xlsx)|*.xlsx|Excel 97-2003 工作簿 (*.xls)|*.xls|" + str;
            }
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "请您选择文件",
                Filter = str
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                string str2 = Path.GetExtension(dialog.FileName).ToLower();
                if (((num >= 12) && ((str2 == ".xls") || (str2 == ".xlsx"))) || ((num < 12) && (str2 == ".xls"))) {
                    ArrayList list;
                    Cursor current = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    try {
                        if (OfficeWrap.Instance == null) {
                            OfficeWrap.Instance = new OfficeWrap();
                        }
                        list = OfficeWrap.Instance.Import(dialog.FileName, BizOperationHelper.Instance.GetTempFilePath());
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "PPM - FrmPPCard", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    } finally {
                        Cursor.Current = current;
                    }
                    PPTmpPage tmpPage = this.GetTmpPage(base.SelectedTab.Text);
                    if (list.Count > 0) {
                        tmpPage.TemplateUrl = (string)list[0];
                        this.m_tp.IsSaved = false;
                    }
                } else {
                    this.GetTmpPage(base.SelectedTab.Text).TemplateUrl = dialog.FileName;
                    this.m_tp.IsSaved = false;
                }
            }
        }

        private void OnPageSetup(object sender, EventArgs e) {
            DlgPageSetup setup = new DlgPageSetup(this.m_tp.PageSetup);
            if (setup.ShowDialog() == DialogResult.OK) {
                this.m_tp.PageSetup = setup.CltPageSetup.GetXml();
            }
        }

        private void OnPrintPreview(object sender, EventArgs e) {
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try {
                this.PrintPreview();
            } catch (Exception exception) {
                MessageBox.Show("打印预览失败，可能的原因有：\n可能没有安装打印机；或者您的Excel没有激活；或者曾经异常操作Excel。" + exception.Message, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } finally {
                Cursor.Current = current;
            }
        }

        private void OnResetViewRange(object sender, EventArgs e) {
            this.GetTmpPage(base.SelectedTab.Text).ResetViewRange();
        }

        public void OnSave(object sender, EventArgs e) {
            this.SaveTemplate();
        }

        private void OnSetDataHiddenCol(object sender, EventArgs e) {
            try {
                DEMetaAttribute attribute = ModelContext.MetaModel.GetAttribute("PPCRDTEMPLATE", "DATAHIDDENCOL");
                if (attribute == null) {
                    MessageBox.Show("请先为“卡片模板”类定制“数据隐藏列（DATAHIDDENCOL）”属性。长度应不小于50。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else {
                    ArrayList midColNameList = this.pageMainPage.GetMidColNameList();
                    ArrayList nextColList = null;
                    if (this.pageNextPage != null) {
                        nextColList = this.pageNextPage.GetMidColNameList();
                    }
                    DlgDataHiddenCol col = new DlgDataHiddenCol(this.m_tp.DataHiddenCol, this.m_tp.HasNextPage, this.m_tp.ReadOnly, midColNameList, nextColList);
                    if (col.ShowDialog() == DialogResult.OK) {
                        if (col.dataHiddenCol.Length > attribute.Size) {
                            MessageBox.Show(string.Concat(new object[] { "设置数据隐藏列形成的脚本长度 ", col.dataHiddenCol.Length.ToString(), " 超过了“卡片模板”类属性“DATAHIDDENCOL”的长度 ", attribute.Size, " 。\n请修改该属性的长度，否则模板将不会保存该规则。" }), "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        } else {
                            this.m_tp.DataHiddenCol = col.dataHiddenCol;
                            this.m_tp.IsSaved = false;
                        }
                    }
                }
            } catch (Exception exception) {
                MessageBox.Show("数据隐藏列设置出错，请重试。具体原因为：\n" + exception.Message, "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OnSetMidTable(object sender, EventArgs e) {
            if (this.GetTmpPage(base.SelectedTab.Text).SetMidTable(base.SelectedTab.Text)) {
                this.frmParent.PropertyEditor.UpdateUI(this.m_tp.Item, null, this.m_tp.UserOid, true);
            }
        }

        private void OnSetPageRange(object sender, EventArgs e) {
            if (this.GetTmpPage(base.SelectedTab.Text).SetPageRange(base.SelectedTab.Text)) {
                this.frmParent.PropertyEditor.UpdateUI(this.m_tp.Item, null, this.m_tp.UserOid, true);
            }
        }

        private void PPTmpControl_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if ((base.Alignment == TabAlignment.Top) && (e.Y <= 20)) {
                    this.BuildContextMenu().Show(this, new Point(e.X, e.Y));
                }
                if ((base.Alignment == TabAlignment.Bottom) && (e.Y >= (base.Bottom - 20))) {
                    this.BuildContextMenu().Show(this, new Point(e.X, e.Y));
                }
            }
        }

        private void PPTmpControl_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.CompareControl != null) {
                int num = -1;
                string text = base.TabPages[base.SelectedIndex].Text;
                for (int i = 0; i < this.CompareControl.TabPages.Count; i++) {
                    if (this.CompareControl.TabPages[i].Text == text) {
                        num = i;
                        break;
                    }
                }
                if (num != -1) {
                    this.CompareControl.SelectedIndex = num;
                }
            }
        }

        private void PrepareTemplateText() {
            if (this.m_tp.HasCover) {
                this.m_tp.CoverText = this.pageCover.TemplateText;
            }
            if (this.m_tp.HasMainPage) {
                this.m_tp.MainPageText = this.pageMainPage.TemplateText;
            }
            if (this.m_tp.HasNextPage) {
                this.m_tp.NextPageText = this.pageNextPage.TemplateText;
            }
        }

        private void PrintPreview() {
            PPTmpPage tmpPage = this.GetTmpPage(base.SelectedTab.Text);
            Convert.ToInt32(PPMCommon.GetRegistryKey());
            string exportFileName = BizOperationHelper.Instance.GetTempFilePath() + base.SelectedTab.Text + ".xls";
            tmpPage.Export(exportFileName, false);
            if (OfficeWrap.Instance == null) {
                OfficeWrap.Instance = new OfficeWrap();
            }
            OfficeWrap.Instance.SetupPages(this.m_tp.PageSetup);
            OfficeWrap.Instance.PrintPreview(exportFileName);
        }

        public bool SaveTemplate() {
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            bool flag = true;
            if (!this.m_tp.IsSaved && !this.m_tp.ReadOnly) {
                try {
                    try {
                        this.m_tp.CheckProperties();
                        flag = this.ValidateMidBindCells();
                    } catch (Exception exception) {
                        if (MessageBox.Show(exception.Message + "\n现在改正这些错误吗？选择“否”，将在下次提醒您更正该模板中的错误。", "PPM提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                            flag = false;
                        }
                    }
                    if (flag) {
                        this.m_tp.SetNullBlob();
                        this.m_tp.Item.Iteration = PLItem.UpdateItemIteration(this.m_tp.Item.Iteration, this.m_tp.UserOid, false);
                        this.PrepareTemplateText();
                        this.m_tp.UpdateBlobs();
                        this.UpdateHeadMainItem();
                        this.m_tp.IsSaved = true;
                        this.m_tp.IsNew = false;
                        foreach (Form form in ClientData.mainForm.MdiChildren) {
                            if (form is FrmBrowse) {
                                BizItemHandlerEvent.Instance.D_AfterIterationUpdated(BizOperationHelper.ConvertPLMBizItemDelegateParam(this.m_tp.Item));
                                goto Label_015D;
                            }
                        }
                    }
                } catch (Exception exception2) {
                    flag = false;
                    this.m_tp.IsSaved = false;
                    MessageBox.Show("保存失败!\n" + exception2.Message, "TiModeler - FrmModeling", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        Label_015D:
            Cursor.Current = current;
            return flag;
        }

        public void TemplateCompare() {
            if ((this.pageCover != null) && (this.CompareControl.pageCover != null)) {
                this.pageCover.PageCompare(this.CompareControl.pageCover);
            }
            if ((this.pageMainPage != null) && (this.CompareControl.pageMainPage != null)) {
                this.pageMainPage.PageCompare(this.CompareControl.pageMainPage);
            }
            if ((this.pageNextPage != null) && (this.CompareControl.pageNextPage != null)) {
                this.pageNextPage.PageCompare(this.CompareControl.pageNextPage);
            }
        }

        private void UpdateHeadMainItem() {
            try {
                if (this.m_tp.HasMainPage) {
                    string latestHeadMainInfo = this.GetLatestHeadMainInfo();
                    if (latestHeadMainInfo != this.m_tp.Item.Revision.ReleaseDesc) {
                        this.m_tp.Item.Revision.ReleaseDesc = latestHeadMainInfo;
                        PLCardTemplate.RemAgent.UpdateReleaseDesc(this.m_tp.Item.RevOid, this.m_tp.Item.Revision.ReleaseDesc, ClientData.LogonUser.Oid);
                        BizItemHandlerEvent.Instance.D_AfterMasterUpdated(BizOperationHelper.ConvertPLMBizItemDelegateParam(this.m_tp.Item));
                    }
                }
            } catch (Exception exception) {
                throw new Exception("更新模板的“头对象:主对象”失败。" + exception.Message);
            }
        }

        private bool ValidateKeyCell(ArrayList midBindCells, string pageLabel) {
            if ((midBindCells != null) && (midBindCells.Count != 0)) {
                PPCardCell cell = null;
                PPCardCell cell2 = null;
                foreach (PPCardCell cell3 in midBindCells) {
                    if (cell3.IsKey) {
                        cell = cell3;
                    } else if (cell3.IsSubKey) {
                        cell2 = cell3;
                    }
                    if ((cell != null) && (cell2 != null)) {
                        break;
                    }
                }
                if ((cell == null) && (cell2 != null)) {
                    MessageBox.Show(pageLabel + cell2.Address + "设置了子关键列，但是没有设置关键列。必须设置关键列，或者取消子关键列的设置。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
                if (((cell != null) && (cell2 != null)) && (cell.ClassName != cell2.LeftClassName)) {
                    MessageBox.Show(pageLabel + "子关键列“" + FormulaCommon2.GetClassLabel(cell2.ClassName) + "”不是关键列“" + FormulaCommon2.GetClassLabel(cell.ClassName) + "”的子对象，请参看子关键列说明。", "PPM", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private bool ValidateMidBindCells() {
            ArrayList midBindCells = this.pageMainPage.GetMidBindCells();
            if (!this.ValidateKeyCell(midBindCells, "首页")) {
                return false;
            }
            if (this.pageNextPage != null) {
                ArrayList list = this.pageNextPage.GetMidBindCells();
                if (!this.ValidateKeyCell(list, "续页")) {
                    return false;
                }
            }
            return true;
        }
    }
}

