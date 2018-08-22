
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using Thyt.TiPLM.PLL.PPM.Card;
namespace Thyt.TiPLM.UIL.PPM.PPCardModeling {
    public partial class DlgHistoryList : Form {
        private ArrayList lstHistory;
        private bool readOnly;

        public DlgHistoryList(ArrayList lstHistory, bool readOnly) {
            this.InitializeComponent();
            this.lstHistory = lstHistory;
            this.InitializeListView();
            this.readOnly = readOnly;
            if (readOnly) {
                this.btnProperty.Visible = false;
                this.btnDelete.Visible = false;
                this.btnAdd.Visible = false;
                this.btnDown.Visible = false;
                this.btnUp.Visible = false;
                base.Width -= this.btnUp.Width;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            DlgHistoryProperty property;
            try {
                property = new DlgHistoryProperty(this.lvwHistory.Items.Count + 1);
            } catch (Exception exception) {
                MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (property.ShowDialog() == DialogResult.OK) {
                this.lstHistory.Add(property.History);
                this.lvwHistory.Items.Add(this.FillListViewItem(property.History));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if ((this.lvwHistory.SelectedItems.Count >= 1) && (MessageBox.Show("确定删除第" + this.lvwHistory.SelectedItems[0].Text + "个记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)) {
                int index = this.lvwHistory.SelectedIndices[0];
                this.lstHistory.RemoveAt(index);
                this.lvwHistory.Items.RemoveAt(index);
                for (int i = index; i < this.lstHistory.Count; i++) {
                    CLHistory history1 = (CLHistory)this.lstHistory[i];
                    history1.Order--;
                }
                for (int j = index; j < this.lvwHistory.Items.Count; j++) {
                    this.lvwHistory.Items[j].Text = (j + 1).ToString();
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e) {
            if ((this.lvwHistory.Items.Count != 0) && (this.lvwHistory.SelectedIndices.Count != 0)) {
                int index = this.lvwHistory.SelectedIndices[0];
                if (index != (this.lvwHistory.Items.Count - 1)) {
                    int num2 = index + 1;
                    ListViewItem item = this.lvwHistory.Items[num2];
                    this.lvwHistory.Items.Remove(item);
                    int num3 = index + 1;
                    item.Text = num3.ToString();
                    this.lvwHistory.Items.Insert(index, item);
                    this.lvwHistory.Items[num2].Text = (num2 + 1).ToString();
                    this.lvwHistory.Items[num2].Selected = true;
                    CLHistory history = this.lstHistory[index] as CLHistory;
                    history.Order = num2 + 1;
                    this.lstHistory[index] = this.lstHistory[num2];
                    ((CLHistory)this.lstHistory[index]).Order = index + 1;
                    this.lstHistory[num2] = history;
                }
            }
        }

        private void btnProperty_Click(object sender, EventArgs e) {
            if (this.lvwHistory.SelectedItems.Count >= 1) {
                int num = 0;
                while (num < this.lstHistory.Count) {
                    if (((CLHistory)this.lstHistory[num]).Order.ToString() == this.lvwHistory.SelectedItems[0].Text) {
                        break;
                    }
                    num++;
                }
                CLHistory history = null;
                if (num < this.lstHistory.Count) {
                    DlgHistoryProperty property;
                    history = this.lstHistory[num] as CLHistory;
                    try {
                        property = new DlgHistoryProperty(history);
                    } catch (Exception exception) {
                        MessageBox.Show(exception.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    if (property.ShowDialog() == DialogResult.OK) {
                        this.lstHistory[num] = property.History;
                        this.FillListViewItem(this.lvwHistory.SelectedItems[0], history);
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e) {
            if ((this.lvwHistory.Items.Count != 0) && (this.lvwHistory.SelectedIndices.Count != 0)) {
                int num = this.lvwHistory.SelectedIndices[0];
                if (num != 0) {
                    int index = num - 1;
                    ListViewItem item = this.lvwHistory.SelectedItems[0];
                    this.lvwHistory.Items.Remove(item);
                    int num3 = index + 1;
                    item.Text = num3.ToString();
                    this.lvwHistory.Items.Insert(index, item);
                    this.lvwHistory.Items[num].Text = (num + 1).ToString();
                    item.Selected = true;
                    CLHistory history = this.lstHistory[num] as CLHistory;
                    history.Order = index + 1;
                    this.lstHistory[num] = this.lstHistory[index];
                    ((CLHistory)this.lstHistory[num]).Order = num + 1;
                    this.lstHistory[index] = history;
                }
            }
        }

        private ListViewItem FillListViewItem(CLHistory history) {
            ListViewItem item = new ListViewItem(history.Order.ToString());
            string text = string.Empty;
            if (history.CellsRemark.Count > 0) {
                foreach (string str2 in history.CellsRemark) {
                    text = text + str2 + ";";
                }
                text = text.Substring(0, text.Length - 1);
            }
            item.SubItems.Add(text);
            item.SubItems.Add(history.CellSign);
            item.SubItems.Add(history.CellDate);
            return item;
        }

        private ListViewItem FillListViewItem(ListViewItem item, CLHistory history) {
            item.SubItems.Clear();
            item.Text = history.Order.ToString();
            string text = string.Empty;
            if (history.CellsRemark.Count > 0) {
                foreach (string str2 in history.CellsRemark) {
                    text = text + str2 + ";";
                }
                text = text.Substring(0, text.Length - 1);
            }
            item.SubItems.Add(text);
            item.SubItems.Add(history.CellSign);
            item.SubItems.Add(history.CellDate);
            return item;
        }

        private void InitializeListView() {
            this.lvwHistory.Items.Clear();
            if ((this.lstHistory != null) && (this.lstHistory.Count > 0)) {
                foreach (CLHistory history in this.lstHistory) {
                    this.lvwHistory.Items.Add(this.FillListViewItem(history));
                }
            }
        }

        private void lvwModifyRecord_ItemActivate(object sender, EventArgs e) {
            if (!this.readOnly) {
                this.btnProperty_Click(null, null);
            }
        }

        public ArrayList Histories {
            get {
                foreach (CLHistory history in this.lstHistory) {
                    history.Total = this.lstHistory.Count;
                }
                return this.lstHistory;
            }
        }
    }
}

