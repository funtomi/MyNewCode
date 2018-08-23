using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.Common;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.PLL.Product2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Common.UserControl;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgNewGuid : Form {
        private Guid groupOid = Guid.Empty;

        private CLCardTemplate m_tp;

        private string revLabel;
        private int securityLevel;
        private string tmpType;
        private UCSelectClass tvHeadItem;
        private UCSelectClass tvMainItem;


        public DlgNewGuid(string templateType, DEUser user) {
            this.InitializeComponent();
            this.tvHeadItem = new UCSelectClass(false, SelectClassConstraint.BusinessItemClass);
            this.pnlHeadItem.Controls.Add(this.tvHeadItem);
            this.tvHeadItem.Dock = DockStyle.Fill;
            this.tvMainItem = new UCSelectClass(false, SelectClassConstraint.BusinessItemClass);
            this.pnlMainItem.Controls.Add(this.tvMainItem);
            this.tvMainItem.Dock = DockStyle.Fill;
            this.tmpType = templateType;
            try {
                this.m_tp = new CLCardTemplate(user.Oid, true, false, null);
                this.m_tp.UserName = user.Name;
            } catch (Exception exception) {
                throw new Exception("新建CLCardTemplate失败。", exception);
            }
            PLUser user2 = new PLUser();
            int userSecurityByOid = user2.GetUserSecurityByOid(ClientData.LogonUser.Oid);
            if (userSecurityByOid == 0) {
                userSecurityByOid = 1;
            }
            this.cobGroup.Items.Clear();
            for (int i = 1; i <= userSecurityByOid; i++) {
                this.cobGroup.Items.Add(i);
            }
            this.cobGroup.Enabled = true;
            ArrayList belongingProjGroupByUserOid = user2.GetBelongingProjGroupByUserOid(ClientData.LogonUser.Oid);
            if ((belongingProjGroupByUserOid != null) && (belongingProjGroupByUserOid.Count > 0)) {
                this.cobGroup.DataSource = belongingProjGroupByUserOid;
                this.cobGroup.DisplayMember = "Name";
                this.cobGroup.ValueMember = "Oid";
            } else {
                this.cobGroup.Enabled = false;
            }
            string clsname = "PPCRDTEMPLATE";
            bool isGenIdInServer = false;
            this.txtId.Text = PLItem.GetIDAtNew(clsname, out isGenIdInServer);
            this.txtId.Tag = isGenIdInServer;
            if (isGenIdInServer) {
                this.txtId.Text = "自动生成";
            }
            if (!string.IsNullOrEmpty(this.txtId.Text)) {
                this.txtId.ReadOnly = true;
                this.btnCode.Enabled = false;
            } else {
                this.txtId.ReadOnly = false;
                this.btnCode.Enabled = true;
            }
            this.SetTreeViewState(templateType);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            base.Close();
        }

        private void btnCode_Click(object sender, EventArgs e) {
            FrmGetCode code = new FrmGetCode {
                ItemType = BusinessItemType.BusinessItem,
                ClassName = "PPCRDTEMPLATE"
            };
            if (code.ShowDialog() == DialogResult.OK) {
                this.txtId.Text = code.Id;
                this.txtId.Tag = code.CodeId;
            }
        }

        private void btnOK_Click(object sender, EventArgs e) {
            string str = this.txtId.Text.Trim();
            if (str == "") {
                MessageBox.Show("模板代号不允许为空。", ConstCommon.ProductName);
                this.txtId.Focus();
                this.txtId.SelectAll();
            } else {
                this.securityLevel = Convert.ToInt32(this.cobSecurityLevel.SelectedValue);
                if (this.cobGroup.Enabled && (this.cobGroup.SelectedIndex > -1)) {
                    this.groupOid = (Guid)this.cobGroup.SelectedValue;
                }
                this.revLabel = this.txtRevLabel.Text.Trim();
                string tmpType = "";
                string str3 = "";
                string releaseDesc = "";
                if (ModelContext.MetaModel.IsCard(this.tmpType) || ModelContext.MetaModel.IsForm(this.tmpType)) {
                    tmpType = this.tmpType;
                    releaseDesc = tmpType + ":" + str3;
                } else {
                    if (this.tvHeadItem.SelectedClass != null) {
                        tmpType = this.tvHeadItem.SelectedClass.Name;
                    }
                    releaseDesc = tmpType + ":" + str3;
                }
                try {
                    this.m_tp.CreatCLItem(this.txtId.Tag.Equals(true) ? null : str, this.revLabel, this.securityLevel, this.groupOid, this.txtId.Tag as string, tmpType);
                    if (BizItemHandlerEvent.Instance.D_AfterItemCreated != null) {
                        List<IBizItem> items = new List<IBizItem> {
                            this.m_tp.Item
                        };
                        PLMOperationArgs args = new PLMOperationArgs(FrmLogon.PLMProduct.ToString(), PLMLocation.PPCardTemplateList.ToString(), items, ClientData.UserGlobalOption);
                        BizItemHandlerEvent.Instance.D_AfterItemCreated(null, args);
                    }
                } catch (Exception exception) {
                    PrintException.Print(new PLMException("创建模板失败！\n", exception));
                    this.txtId.Focus();
                    this.txtId.SelectAll();
                    return;
                }
                try {
                    PLCardTemplate.RemAgent.UpdateReleaseDesc(this.m_tp.Item.RevOid, releaseDesc, ClientData.LogonUser.Oid);
                } catch (Exception exception2) {
                    PrintException.Print(new PLMException("更新模板的“头对象:主对象”失败！\n", exception2));
                    return;
                }
                if (this.m_tp.Item == null) {
                    base.DialogResult = DialogResult.Cancel;
                } else {
                    this.m_tp.HasCover = this.chkCover.Checked;
                    this.m_tp.HasMainPage = this.chkMainPage.Checked;
                    this.m_tp.HasNextPage = this.chkNextPage.Checked;
                    this.m_tp.Item.Revision.ReleaseDesc = releaseDesc;
                    try {
                        this.m_tp.Item.Iteration = PLItem.UpdateItemIteration(this.m_tp.Item.Iteration, this.m_tp.UserOid, false);
                        base.DialogResult = DialogResult.OK;
                    } catch (Exception exception3) {
                        MessageBox.Show("新建模板失败。" + exception3.Message, "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        base.DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }

        private void SetTreeViewState(string templateType) {
            this.tvHeadItem.ReadOnly = true;
            this.tvMainItem.ReadOnly = true;
            this.tvMainItem.Enabled = false;
            if (ModelContext.MetaModel.IsForm(templateType)) {
                this.tvHeadItem.SetValue(templateType);
                this.tvHeadItem.Enabled = false;
            } else if (ModelContext.MetaModel.IsCard(templateType)) {
                this.tvHeadItem.SetValue(templateType);
                this.tvHeadItem.Enabled = false;
            } else {
                this.tvHeadItem.Enabled = true;
            }
        }

        private void SetTypeTree(TreeView tv) {
            DlgChangeHeadMainItem.CreateTypeTree(tv);
        }

        public CLCardTemplate TP {
            get {
                return this.m_tp;
            }
            set {
                this.m_tp = value;
            }
        }
    }
}

