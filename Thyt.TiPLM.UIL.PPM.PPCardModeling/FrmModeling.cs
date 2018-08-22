using Crownwood.DotNetMagic.Docking;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Admin.NewResponsibility;
using Thyt.TiPLM.DEL.Product;
using Thyt.TiPLM.PLL.Admin.DataModel;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class FrmModeling : Form {
        private static Content dckProperty;
        private CLCardTemplate m_tp;
        private PPTmpControl pageControl;
        public PropertyPanel PropertyEditor;
        private const string TEMPLATEPROPERTY = "模板属性";

        public FrmModeling(CLCardTemplate tp) {
            this.InitializeComponent();
            this.m_tp = tp;
            this.Text = this.m_tp.Item.Master.Id;
            this.InitializeMyUI();
            this.InitializePageControl();
        }

        public FrmModeling(DEBusinessItem item, DEUser user, bool readOnly) {
            this.InitializeComponent();
            if (item.Iteration == null) {
                throw new Exception("取得的迭代为空。");
            }
            this.m_tp = new CLCardTemplate(user.Oid, false, readOnly, item);
            this.Text = item.Master.Id;
            if (readOnly) {
                this.Text = this.Text + "(只读)";
            }
            try {
                this.m_tp.CheckProperties();
            } catch (Exception exception) {
                MessageBox.Show("警告：模板范围和表中区域没有正确设置，请用编辑界面中的快捷菜单命令更正。\n详细信息是：" + exception.Message, "PPM警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.InitializePageControl();
            if (!readOnly) {
                this.InitializeMyUI();
            }
        }

        private void FrmModeling_Closed(object sender, EventArgs e) {
            DlgOption2.HasKey = false;
            DlgOption2.ccFavorite = null;
            this.RemoveMyUI();
        }

        private void FrmPPCModeling_Closing(object sender, CancelEventArgs e) {
            if (!this.m_tp.IsSaved && !this.m_tp.ReadOnly) {
                bool flag = ModelContext.MetaModel.IsForm(this.m_tp.HeadClass);
                string text = "是否保存" + (flag ? "表单" : "卡片") + "模板？";
                string caption = (flag ? "表单" : "卡片") + "定制";
                MessageBoxButtons yesNoCancel = MessageBoxButtons.YesNoCancel;
                switch (MessageBox.Show(text, caption, yesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)) {
                    case DialogResult.Yes:
                        if (!this.pageControl.SaveTemplate()) {
                            e.Cancel = true;
                            return;
                        }
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void FrmPPCModeling_Enter(object sender, EventArgs e) {
            dckProperty.Control = this.PropertyEditor;
        }

        public void InitializeMyUI() {
            this.PropertyEditor = new PropertyPanel(this.m_tp.Item, ModelContext.MetaModel.GetClassEx("PPCRDTEMPLATE"), this.m_tp.UserOid, true);
            bool flag = false;
            foreach (Content content in TiModelerUIContainer.dockingManager.Contents) {
                if (content.Title == "模板属性") {
                    flag = true;
                    break;
                }
            }
            if (!flag) {
                dckProperty = new Content(TiModelerUIContainer.dockingManager, this.PropertyEditor, "模板属性", PLMImageList.GetIcon("ICO_PPM_PPCARDTMPPROPERTY"));
                dckProperty.AutoHideSize = new Size(300, 0);
                dckProperty.CloseButton = false;
                TiModelerUIContainer.dockingManager.Contents.Add(dckProperty);
                base.SuspendLayout();
                TiModelerUIContainer.dockingManager.AddContentWithState(dckProperty, Crownwood.DotNetMagic.Docking.State.DockRight);
                TiModelerUIContainer.dockingManager.ToggleContentAutoHide(dckProperty);
                base.ResumeLayout();
            }
        }

        private void InitializePageControl() {
            this.pageControl = new PPTmpControl(this.m_tp, this);
            this.pageControl.Dock = DockStyle.Fill;
            base.Controls.Add(this.pageControl);
        }

        public void RemoveMyUI() {
            if (base.MdiParent != null) {
                int num = 0;
                foreach (Form form in base.MdiParent.MdiChildren) {
                    if (form is FrmModeling) {
                        num++;
                    }
                }
                if (num <= 1) {
                    try {
                        TiModelerUIContainer.dockingManager.HideContent(TiModelerUIContainer.dockingManager.Contents["模板属性"]);
                        TiModelerUIContainer.dockingManager.Contents.Remove(TiModelerUIContainer.dockingManager.Contents["模板属性"]);
                    } catch (Exception exception) {
                        MessageBox.Show("移除导航器出错。相关信息是：" + exception.Message);
                    }
                }
            }
        }
    }
}

