namespace Thyt.TiPLM.UIL.PPM.PPCardModeling
{
    using System;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Admin.NewResponsibility;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.UIL.Common;

    public class OpenTemplateUtil
    {
        private bool readOnly;
        private DEUser user;

        public OpenTemplateUtil(DEUser user, bool readOnly)
        {
            this.user = user;
            this.readOnly = readOnly;
        }

        public void OpenTemplateEx(DEBusinessItem item)
        {
            if (item.Iteration == null)
            {
                item = PLItem.Agent.GetBizItemByMaster(item.Master.Oid, item.Revision.Revision, PLOption.GetUserGlobalOption(ClientData.LogonUser.Oid).CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
            }
            if (this.readOnly)
            {
                foreach (Form form in ClientData.mainForm.MdiChildren)
                {
                    if ((form.Text.IndexOf(item.Master.Id) >= 0) && (form is FrmModeling))
                    {
                        form.Activate();
                        return;
                    }
                }
            }
            else
            {
                foreach (Form form2 in ClientData.mainForm.MdiChildren)
                {
                    if ((form2.Text == item.Master.Id) && (form2 is FrmModeling))
                    {
                        form2.Activate();
                        return;
                    }
                }
            }
            if (this.readOnly || !(item.Master.Holder != this.user.Oid))
            {
                Cursor current = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                FrmModeling modeling = null;
                try
                {
                    modeling = new FrmModeling(item, this.user, this.readOnly);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("新建FrmModeling失败。\n" + exception.Message, "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                finally
                {
                    Cursor.Current = current;
                }
                if (!this.readOnly)
                {
                    modeling.MdiParent = ClientData.mainForm;
                    modeling.WindowState = FormWindowState.Maximized;
                    modeling.Show();
                }
                else
                {
                    modeling.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("请先检出该对象。", "卡片模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void TemplateCompare(DEBusinessItem item1, DEBusinessItem item2)
        {
            new FrmTmpCompare(item1, item2, this.user) { MdiParent = ClientData.mainForm }.Show();
        }
    }
}

