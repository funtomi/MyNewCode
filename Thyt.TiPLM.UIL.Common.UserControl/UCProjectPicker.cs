
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Project2;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Project2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCProjectPicker : UserControlPLM {
        private bool b_start;
        private DEProject curInput;

        public event ProjectDropListHandler ProjectSelected;

        public UCProjectPicker() {
            this.b_start = true;
            this.InitializeComponent();
        }

        public UCProjectPicker(DEProject prjObj)
            : this() {
            this.curInput = prjObj;
            this.InitProjectHeader();
            this.LoadProjectData();
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            if (this.ProjectSelected != null) {
                DEProject projectObject = this.GetProjectObject();
                if (projectObject == null) {
                    MessageBoxPLM.Show("请选择项目", "项目选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                this.ProjectSelected(projectObject);
            }
            this.CloseParent();
        }

        private void CloseParent() {
            if (base.Parent != null) {
                if (base.Parent is PopupContainerControl) {
                    PopupContainerControl parent = base.Parent as PopupContainerControl;
                    if ((parent != null) && (parent.OwnerEdit != null)) {
                        parent.OwnerEdit.ClosePopup();
                    }
                } else {
                    base.Parent.Hide();
                }
            }
        }

        private DEProject GetProjectObject() {
            if (this.lv_projects.SelectedItems.Count > 0) {
                return (this.lv_projects.SelectedItems[0].Tag as DEProject);
            }
            return null;
        }


        private void InitProjectHeader() {
            this.lv_projects.Columns.Clear();
            ColumnHeader header = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            ColumnHeader header3 = new ColumnHeader();
            header.Text = "项目代号";
            header.Width = 100;
            header2.Text = "项目名称";
            header2.Width = 100;
            header3.Text = "项目管理者";
            header3.Width = 80;
            this.lv_projects.Columns.AddRange(new ColumnHeader[] { header, header2, header3 });
        }

        private void LoadProjectData() {
            ArrayList list = new ArrayList();
            ArrayList allProjects = new PLProject().GetAllProjects(ClientData.LogonUser.Oid);
            this.lv_projects.Items.Clear();
            foreach (DEProject project2 in allProjects) {
                if (!list.Contains(project2.Oid)) {
                    list.Add(project2.Oid);
                    ListViewItem item = new ListViewItem {
                        Text = project2.ID,
                        SubItems = { 
                            project2.Name,
                            PrincipalRepository.GetPrincipalName(project2.Project_manager)
                        },
                        Tag = project2
                    };
                    this.lv_projects.Items.Add(item);
                }
            }
        }

        private void LocationPrjData(DEProject PrjObj) {
            if (this.lv_projects.Items.Count != 0) {
                if (base.Width > 280) {
                    this.lv_projects.Columns[2].Width = base.Width - 200;
                }
                foreach (ListViewItem item in this.lv_projects.Items) {
                    DEProject tag = item.Tag as DEProject;
                    if (tag.ProductOid == PrjObj.ProductOid) {
                        item.Selected = true;
                    } else {
                        item.Selected = false;
                    }
                }
            }
        }

        private void lv_projects_DoubleClick(object sender, EventArgs e) {
            if (this.ProjectSelected != null) {
                DEProject projectObject = this.GetProjectObject();
                if (projectObject == null) {
                    MessageBoxPLM.Show("请选择项目", "项目选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                this.ProjectSelected(projectObject);
            }
            this.CloseParent();
        }

        public void ReLoad(DEProject prjObj) {
            if (prjObj != null) {
                this.curInput = prjObj;
                this.LocationPrjData(prjObj);
            }
        }

        private void UCIDPicker_Enter(object sender, EventArgs e) {
            this.b_start = false;
        }
    }
}

