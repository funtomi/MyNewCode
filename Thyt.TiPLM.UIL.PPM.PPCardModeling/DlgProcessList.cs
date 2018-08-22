using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
using Thyt.TiPLM.UIL.Common;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgProcessList : Form {

        private ArrayList lstProcess;
        private bool readOnly;

        public DlgProcessList(ArrayList lstProcess, bool readOnly) {
            this.InitializeComponent();
            this.lstProcess = lstProcess;
            this.InitializeListView();
            this.readOnly = readOnly;
            if (readOnly) {
                this.btnProperty.Visible = false;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            DlgProcessProperty property;
            try {
                property = new DlgProcessProperty(this.lstProcess);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (property.ShowDialog() == DialogResult.OK) {
                this.lstProcess.Add(property.State);
                this.lvwProcess.Items.Add(this.FillListViewItem(property.State));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if ((this.lvwProcess.SelectedItems.Count >= 1) && (MessageBox.Show("确定删除" + this.lvwProcess.SelectedItems[0].Text + "吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)) {
                foreach (CLState state in this.lstProcess) {
                    if (state.OprKey == this.lvwProcess.SelectedItems[0].Text) {
                        this.lstProcess.Remove(state);
                        this.lvwProcess.Items.Remove(this.lvwProcess.SelectedItems[0]);
                        break;
                    }
                }
            }
        }

        private void btnProperty_Click(object sender, EventArgs e) {
            if (this.lvwProcess.SelectedItems.Count >= 1) {
                int num = 0;
                while (num < this.lstProcess.Count) {
                    if (((CLState)this.lstProcess[num]).OprKey == this.lvwProcess.SelectedItems[0].Text) {
                        break;
                    }
                    num++;
                }
                CLState state = null;
                if (num < this.lstProcess.Count) {
                    DlgProcessProperty property;
                    state = this.lstProcess[num] as CLState;
                    try {
                        property = new DlgProcessProperty(state);
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (property.ShowDialog() == DialogResult.OK) {
                        this.lstProcess[num] = property.State;
                        this.FillListViewItem(this.lvwProcess.SelectedItems[0], state);
                    }
                }
            }
        }

        private ListViewItem FillListViewItem(CLState state) {
            return new ListViewItem(state.OprKey) {
                SubItems = { 
                state.CellSign,
                state.EleSignature ? "是" : "否",
                state.EleSignature ? state.SgnSizeMode : ""
            }
            };
        }

        private ListViewItem FillListViewItem(ListViewItem item, CLState state) {
            item.SubItems.Clear();
            item.Text = state.OprKey;
            item.SubItems.Add(state.CellSign);
            item.SubItems.Add(state.EleSignature ? "是" : "否");
            item.SubItems.Add(state.EleSignature ? state.SgnSizeMode : "");
            return item;
        }

        private void InitializeListView() {
            this.lvwProcess.Items.Clear();
            if ((this.lstProcess != null) && (this.lstProcess.Count > 0)) {
                foreach (CLState state in this.lstProcess) {
                    this.lvwProcess.Items.Add(this.FillListViewItem(state));
                }
            }
        }

        private void lvwProcess_ItemActivate(object sender, EventArgs e) {
            if (!this.readOnly) {
                this.btnProperty_Click(null, null);
            }
        }

        public ArrayList Processes {
            get { return this.lstProcess; }
        }
    }
}

