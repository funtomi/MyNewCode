using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Thyt.TiPLM.DEL.Project2;
using Thyt.TiPLM.PLL.Admin.NewResponsibility;
using Thyt.TiPLM.PLL.Project2;
using Thyt.TiPLM.UIL.Common;
using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class UCSearchInProject : UserControlPLM, IUCAgent {
        private bool b_isItemActive = true;
        private string curClsName = "";

        public UCSearchInProject() {
            this.InitializeComponent();
        }

        public string GetValue() {
            string str = "";
            string str2 = "false";
            string str3 = "SearchInProject(";
            if (this.chkInPrj.Checked) {
                str2 = "true";
            }
            if (this.lv_projects.CheckedItems.Count == 0) {
                str = " ";
            } else {
                DEProject tag = this.lv_projects.CheckedItems[0].Tag as DEProject;
                str = tag.Oid.ToString();
            }
            if (string.IsNullOrEmpty(str)) {
                return "";
            }
            string str4 = str3;
            return (str4 + str2 + "," + str + ")");
        }

        private void InitProjectHeader() {
            this.lv_projects.Columns.Clear();
            ColumnHeader header = new ColumnHeader();
            ColumnHeader header2 = new ColumnHeader();
            header.Text = "项目名称";
            header.Width = 140;
            header2.Text = "项目管理者";
            header2.Width = 80;
            this.lv_projects.Columns.AddRange(new ColumnHeader[] { header, header2 });
        }

        private void LoadProjectData() {
            ArrayList allProjects = new PLProject().GetAllProjects(ClientData.LogonUser.Oid);
            this.lv_projects.Items.Clear();
            foreach (DEProject project2 in allProjects) {
                ListViewItem item = new ListViewItem {
                    Text = project2.Name,
                    SubItems = { PrincipalRepository.GetPrincipalName(project2.Project_manager) },
                    Tag = project2
                };
                this.lv_projects.Items.Add(item);
            }
        }

        private void LocationPrjData(string strPrjOid) {
            if (this.lv_projects.Items.Count != 0) {
                foreach (ListViewItem item in this.lv_projects.Items) {
                    DEProject tag = item.Tag as DEProject;
                    if (tag.Oid.ToString() == strPrjOid) {
                        item.Checked = true;
                    } else {
                        item.Checked = false;
                    }
                }
            }
        }

        private void lv_projects_ItemActivate(object sender, EventArgs e) {
            this.b_isItemActive = true;
            if (this.lv_projects.SelectedItems.Count != 0) {
                int index = this.lv_projects.SelectedItems[0].Index;
                foreach (ListViewItem item in this.lv_projects.CheckedItems) {
                    item.Checked = false;
                }
                this.lv_projects.Items[index].Checked = true;
                this.b_isItemActive = false;
            }
        }

        private void lv_projects_ItemChecked(object sender, ItemCheckedEventArgs e) {
            if (!this.b_isItemActive) {
                e.Item.Checked = false;
            }
        }

        public string ParseValue(string str_funcvalue) {
            return "wyl";
        }
        public void SetInput(object o_in) {
            if (((o_in != null) && (o_in is ArrayList)) && ((o_in as ArrayList).Count >= 2)) {
                string str = (o_in as ArrayList)[0].ToString();
                if (str.Trim().Length > 0) {
                    this.curClsName = str;
                    this.InitProjectHeader();
                    this.LoadProjectData();
                }
                string str2 = (o_in as ArrayList)[1].ToString();
                if (str2.StartsWith("SearchInProject(") && str2.EndsWith(")")) {
                    this.SetUCParameter((o_in as ArrayList)[1].ToString());
                }
            }
        }

        private void SetUCParameter(string str_in) {
            string str = str_in.Substring("SearchInProject".Length + 1, (str_in.Length - "SearchInProject".Length) - 2);
            if ((str != null) && (str.Trim().Length > 0)) {
                char[] separator = ",".ToCharArray();
                string[] strArray = str.Split(separator);
                for (int i = 0; i < strArray.Length; i++) {
                    string strPrjOid = strArray[i].ToString();
                    if ((i == 0) && (strPrjOid == "true")) {
                        this.chkInPrj.Checked = true;
                    }
                    if ((i == 1) && (strPrjOid.Trim().Length > 0)) {
                        this.LocationPrjData(strPrjOid);
                    }
                }
            }
        }
    }
}

