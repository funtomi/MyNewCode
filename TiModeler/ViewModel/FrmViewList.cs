
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.TiModeler;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.UIL.Admin.ViewNetwork;
    using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.CLT.TiModeler.ViewModel {
    public partial class FrmViewList : Form
    {
        private ArrayList allViews;
        

        public FrmViewList()
        {
            this.allViews = new ArrayList();
            this.InitializeComponent();
        }

        public FrmViewList(FrmMain main)
        {
            this.allViews = new ArrayList();
            this.InitializeComponent();
            this.frmMain = main;
        }

        private void BuildMenu(string type)
        {
            this.cmuView.MenuItems.Clear();
            string str = type;
            if (str != null)
            {
                if (str != "Thyt.TiPLM.DEL.Product.DEView")
                {
                    if (str != "Empty")
                    {
                        return;
                    }
                }
                else
                {
                    ViewListMenuProcess process = new ViewListMenuProcess(this.lvwView, (DEView) this.lvwView.SelectedItems[0].Tag, ClientData.LogonUser);
                    this.cmuView.MenuItems.AddRange(process.BuildViewMenuItems());
                    return;
                }
                ViewListMenuProcess process2 = new ViewListMenuProcess(this.lvwView, null, ClientData.LogonUser);
                this.cmuView.MenuItems.AddRange(process2.BuildEmptyMenuItems());
            }
        }

        private void FrmViewList_Activated(object sender, EventArgs e)
        {
            try
            {
                ViewModelUL.FillAllViews(this.lvwView, false);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void FrmViewList_Closing(object sender, CancelEventArgs e)
        {
            this.frmMain.RemoveWnd(this);
        }

        private void FrmViewList_Load(object sender, EventArgs e)
        {
            try
            {
                ViewModelUL.FillAllViews(this.lvwView, false);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "视图管理", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }


        private void lvwView_DoubleClick(object sender, EventArgs e)
        {
            if ((this.lvwView.SelectedItems.Count <= 1) && (this.lvwView.SelectedItems.Count > 0))
            {
                FrmNodeProperty property = null;
                try
                {
                    property = new FrmNodeProperty((DEView) this.lvwView.SelectedItems[0].Tag);
                }
                catch (ViewException exception)
                {
                    MessageBox.Show(exception.Message, "视图属性", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                if ((property.ShowDialog() == DialogResult.OK) || property.hasApply)
                {
                    DEView deView = property.deView;
                    ListViewItem item = this.lvwView.SelectedItems[0];
                    item.Text = deView.Label;
                    item.Tag = deView;
                    item.SubItems.Clear();
                    item.Text = deView.Label;
                    item.SubItems.Add(deView.Creator);
                    item.SubItems.Add(deView.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    string text = "";
                    if ((deView.Option & 1) == 1)
                    {
                        text = "允许";
                    }
                    else
                    {
                        text = "不允许";
                    }
                    item.SubItems.Add(text);
                    string str2 = "";
                    if ((deView.Option & 2) == 2)
                    {
                        str2 = "允许";
                    }
                    else
                    {
                        str2 = "不允许";
                    }
                    item.SubItems.Add(str2);
                    item.SubItems.Add(deView.GetDesc());
                }
                property.Dispose();
            }
        }

        private void lvwView_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.lvwView.SelectedItems.Count <= 1)
            {
                string type = null;
                Point pos = new Point(e.X + 10, e.Y);
                ListViewItem itemAt = this.lvwView.GetItemAt(e.X, e.Y);
                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        if (this.lvwView.SelectedItems[0].Bounds.Contains(e.X, e.Y))
                        {
                            type = itemAt.Tag.GetType().ToString();
                        }
                    }
                    catch
                    {
                        type = "Empty";
                    }
                    this.BuildMenu(type);
                    if (this.cmuView.MenuItems.Count > 0)
                    {
                        this.cmuView.Show(this.lvwView, pos);
                    }
                }
            }
        }
    }
}

