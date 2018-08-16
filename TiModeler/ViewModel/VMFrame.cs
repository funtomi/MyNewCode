
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Resources;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.View;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    public partial class VMFrame : Form
    {
       
        public bool isDeleteToClose;
        public VMViewPanal viewPanel;

        public VMFrame(DEViewModel theVM)
        {
            base.Tag = theVM;
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_VIW_VIEWMODEL");
            this.Text = "视图模型--" + theVM.Name;
            this.init();
        }

        public VMFrame(DEViewModel theVM, ArrayList newViewList)
        {
            base.Tag = theVM;
            this.InitializeComponent();
            base.Icon = PLMImageList.GetIcon("ICO_VIW_VIEWMODEL");
            this.Text = "视图模型--" + theVM.Name;
            this.initInNewVM(newViewList);
        }

        public void controlTBButton(string flag)
        {
            if (flag.Equals(TagForViewWork.TOOLBAR_CURSOR))
            {
                if (this.viewPanel.CursorDown)
                {
                    this.viewPanel.CursorDown = false;
                    this.viewPanel.RecDown = false;
                    this.viewPanel.ReserveDown = false;
                    this.tbrbtnArrow.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Default;
                }
                else
                {
                    this.viewPanel.CursorDown = true;
                    this.viewPanel.RecDown = false;
                    this.viewPanel.ReserveDown = false;
                    this.tbrbtnArrow.Pushed = true;
                    this.tbrbtnRec.Pushed = false;
                    this.tbrbtnReserve.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Default;
                }
            }
            else if (flag.Equals(TagForViewWork.TOOLBAR_REC))
            {
                if (this.viewPanel.RecDown)
                {
                    this.viewPanel.CursorDown = false;
                    this.viewPanel.RecDown = false;
                    this.viewPanel.ReserveDown = false;
                    this.tbrbtnRec.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Default;
                }
                else
                {
                    this.viewPanel.CursorDown = false;
                    this.viewPanel.RecDown = true;
                    this.viewPanel.ReserveDown = false;
                    this.tbrbtnArrow.Pushed = false;
                    this.tbrbtnRec.Pushed = true;
                    this.tbrbtnReserve.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
            else if (flag.Equals(TagForViewWork.TOOLBAR_RESERVE))
            {
                if (this.viewPanel.ReserveDown)
                {
                    this.viewPanel.CursorDown = false;
                    this.viewPanel.RecDown = false;
                    this.viewPanel.ReserveDown = false;
                    this.tbrbtnReserve.Pushed = false;
                    this.viewPanel.Cursor = Cursors.Default;
                }
                else
                {
                    this.viewPanel.CursorDown = false;
                    this.viewPanel.RecDown = false;
                    this.viewPanel.ReserveDown = true;
                    this.tbrbtnArrow.Pushed = false;
                    this.tbrbtnRec.Pushed = false;
                    this.tbrbtnReserve.Pushed = true;
                    this.viewPanel.Cursor = Cursors.Cross;
                }
            }
        }
         

        private void init()
        {
            this.viewPanel = new VMViewPanal(this);
            this.panel1.Controls.Add(this.viewPanel);
        }


        private void initInNewVM(ArrayList selectedViewList)
        {
            this.viewPanel = new VMViewPanal(this, selectedViewList);
            this.panel1.Controls.Add(this.viewPanel);
        }

        public void RefreshViewModel()
        {
            base.Activate();
            this.viewPanel.Dispose();
            this.init();
            DataTable table = this.viewPanel.dataDoc.TheDataSet.Tables["PLM_ADM_VIEWMODEL"];
            DEViewModel model = new DEViewModel {
                Oid = new Guid((byte[]) table.Rows[0]["PLM_OID"]),
                Name = (string) table.Rows[0]["PLM_NAME"],
                Creator = (string) table.Rows[0]["PLM_CREATOR"],
                CreateTime = (DateTime) table.Rows[0]["PLM_CREATETIME"],
                Locker = new Guid((byte[]) table.Rows[0]["PLM_LOCKER"]),
                IsActive = Convert.ToChar(table.Rows[0]["PLM_ISACTIVE"])
            };
            if (!(table.Rows[0]["PLM_DESCRIPTION"] is DBNull))
            {
                model.Description = (string) table.Rows[0]["PLM_DESCRIPTION"];
            }
            base.Tag = model;
            this.Text = "视图模型--" + model.Name;
            ((FrmMain) base.MdiParent).tvwNavigator.SelectedNode.Tag = model;
        }

        private void setToolBarTag()
        {
            this.tbrbtnArrow.Tag = TagForViewWork.TOOLBAR_CURSOR;
            this.tbrbtnRec.Tag = TagForViewWork.TOOLBAR_REC;
        }

        private void tbrDraw_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (((DEViewModel) base.Tag).Locker != ClientData.LogonUser.Oid)
            {
                MessageBox.Show("您还没有开始编辑该视图模型，不能对其进行修改！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                string flag = e.Button.Tag.ToString();
                if (flag.Equals(TagForViewWork.TOOLBAR_CURSOR))
                {
                    this.viewPanel.CursorFlag = true;
                    this.viewPanel.RecFlag = false;
                    this.viewPanel.ReserveFlag = false;
                    this.controlTBButton(flag);
                }
                else if (flag.Equals(TagForViewWork.TOOLBAR_REC))
                {
                    this.viewPanel.CursorFlag = false;
                    this.viewPanel.RecFlag = true;
                    this.viewPanel.ReserveFlag = false;
                    this.controlTBButton(flag);
                }
                else
                {
                    this.viewPanel.CursorFlag = false;
                    this.viewPanel.RecFlag = false;
                    this.viewPanel.ReserveFlag = true;
                    this.controlTBButton(flag);
                }
            }
        }

        private void VMFrame_Activated(object sender, EventArgs e)
        {
            DEViewModel keyByValue = (DEViewModel) ((FrmMain) base.MdiParent).HashMDiWindows.GetKeyByValue(base.Tag);
            for (int i = 0; i < TagForTiModeler.TreeNode_ViewNetwork.Nodes.Count; i++)
            {
                TreeNode node = TagForTiModeler.TreeNode_ViewNetwork.Nodes[i];
                if ((node.Tag is DEViewModel) && ((DEViewModel) node.Tag).Oid.Equals(((DEViewModel) base.Tag).Oid))
                {
                    ((FrmMain) base.MdiParent).tvwNavigator.SelectedNode = node;
                    return;
                }
            }
        }

        public void VMFrame_Closing(object sender, CancelEventArgs e)
        {
            if (!this.isDeleteToClose && (((DEViewModel) base.Tag).Locker == ClientData.LogonUser.Oid))
            {
                switch (MessageBox.Show(this, "是否保存到数据库？", "保存视图模型", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        try
                        {
                            this.viewPanel.saveFile();
                            break;
                        }
                        catch (ViewException exception)
                        {
                            MessageBox.Show(exception.Message, "保存视图模型", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            e.Cancel = true;
                            return;
                        }
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }
            FrmMain mdiParent = (FrmMain) base.MdiParent;
            IDictionaryEnumerator enumerator = mdiParent.HashMDiWindows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key is DEViewModel)
                {
                    DEViewModel key = (DEViewModel) enumerator.Key;
                    DEViewModel tag = (DEViewModel) base.Tag;
                    if (key.Oid.Equals(tag.Oid))
                    {
                        mdiParent.HashMDiWindows.Remove(enumerator.Key);
                        return;
                    }
                }
            }
        }

        private void VMFrame_KeyDown(object sender, KeyEventArgs e)
        {
            if (((DEViewModel) base.Tag).Locker != ClientData.LogonUser.Oid)
            {
                MessageBox.Show("您还没有开始编辑该视图模型，不能对其进行修改！", "视图模型", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else if ((e.KeyCode == Keys.Delete) && !this.viewPanel.deleteObject())
            {
                e.Handled = false;
            }
        }

        private void VMFrame_Load(object sender, EventArgs e)
        {
            this.setToolBarTag();
        }
    }
}

