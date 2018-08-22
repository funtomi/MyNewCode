using System;
using System.ComponentModel;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Product.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class FrmProperty : Form {
        
        public DEBusinessItem Item;
        private PropertyPanel pro;
        private bool readOnly;
        private Guid uOid;
        private bool updateDirectely;

        public FrmProperty(Guid uOid, bool readOnly, bool updateDirectely, Guid masterOid) {
            this.InitializeComponent();
            this.InitializeData(uOid, readOnly, updateDirectely, PLItem.Agent.GetBizItemByMaster(masterOid, 0, PLOption.GetUserGlobalOption(ClientData.LogonUser.Oid).CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem);
        }

        public FrmProperty(Guid uOid, bool readOnly, bool updateDirectely, DEBusinessItem item) {
            this.InitializeComponent();
            this.InitializeData(uOid, readOnly, updateDirectely, item);
        }

        private void btnOK_Click(object sender, EventArgs e) {
            if (this.readOnly) {
                base.DialogResult = DialogResult.Cancel;
                base.Close();
            } else if (this.pro.UpdateData(false)) {
                this.Item = this.pro.Item;
                if (this.pro.Item.Master.ClassName == "PPCRDTEMPLATE") {
                    CLCardTemplate template = new CLCardTemplate(this.uOid, false, false, this.Item) {
                        HasCover = this.chkCover.Checked,
                        HasMainPage = this.chkMainPage.Checked,
                        HasNextPage = this.chkNextPage.Checked
                    };
                    try {
                        template.CheckProperties();
                    } catch (Exception exception) {
                        if (MessageBox.Show(exception.Message + "\n现在改正这些错误吗？选择“否”，将在下次提醒您更正该模板中的错误。", "PPM提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                            return;
                        }
                    }
                    Cursor current = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    try {
                        if (this.updateDirectely) {
                            template.Item.Iteration = PLItem.UpdateItemIterationDirectly(template.Item, this.uOid, false);
                        } else {
                            template.Item.Iteration = PLItem.UpdateItemIteration(template.Item.Iteration, this.uOid, false);
                        }
                    } catch (Exception exception2) {
                        MessageBox.Show("更新模板属性失败！\n" + exception2.Message, "PPC - FrmBrowse", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    } finally {
                        Cursor.Current = current;
                    }
                    BizItemHandlerEvent.Instance.D_AfterIterationUpdated(BizOperationHelper.ConvertPLMBizItemDelegateParam(template.Item));
                }
                base.DialogResult = DialogResult.OK;
                base.Close();
            }
        }

        private void InitializeData(Guid uOid, bool readOnly, bool updateDirectely, DEBusinessItem item) {
            this.uOid = uOid;
            this.readOnly = readOnly;
            this.updateDirectely = updateDirectely;
            this.Item = item;
            this.Text = this.Item.Master.Id + "的属性";
            if (this.Item.Master.ClassName == "PPCRDTEMPLATE") {
                if (readOnly) {
                    this.tbcProperty.TabPages.RemoveAt(1);
                }
                CLCardTemplate template = new CLCardTemplate(this.uOid, false, readOnly, this.Item);
                this.chkCover.Checked = template.HasCover;
                this.chkMainPage.Checked = template.HasMainPage;
                this.chkNextPage.Checked = template.HasNextPage;
                try {
                    template.CheckProperties();
                } catch (Exception exception) {
                    MessageBox.Show("警告：模板范围和表中区域没有正确设置，请用编辑界面中的快捷菜单命令更正。\n详细信息是：" + exception.Message, "PPM警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            } else {
                this.tbcProperty.TabPages.RemoveAt(1);
            }
            this.pro = new PropertyPanel(this.Item, ModelContext.MetaModel.GetClassEx(this.Item.Master.ClassName), uOid, readOnly);
            this.pro.Dock = DockStyle.Fill;
            this.tbcProperty.TabPages[0].Controls.Add(this.pro);
            if (readOnly) {
                this.btnOK.Visible = false;
                this.btnOK.Enabled = false;
                this.btnCancle.Text = "确定";
            }
        }

        private void tbcProperty_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.tbcProperty.SelectedIndex == 0) {
                CLCardTemplate template = new CLCardTemplate(this.uOid, false, this.readOnly, this.pro.Item) {
                    HasCover = this.chkCover.Checked,
                    HasMainPage = this.chkMainPage.Checked,
                    HasNextPage = this.chkNextPage.Checked
                };
                this.pro.UpdateUI(template.Item, ModelContext.MetaModel.GetClassEx("PPCRDTEMPLATE"), this.uOid, this.readOnly);
            }
        }
    }
}

