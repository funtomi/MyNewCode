    using DevExpress.XtraEditors.Controls;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Thyt.TiPLM.CLT.UIL.DeskLib.WinControls;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Product2;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public partial class UCCusSPL : UserControlPLM
    {
        private ArrayList attrList = new ArrayList();
        private string clsName = "";
        private DEResFolder curFolder;
        
        private ListView lvwMid = new ListView();
        private string myClassName;
        private DataView myView = new DataView();
        private int pageNum;
        
        private const int showNum = 0x19;
        
        private DataSet theDataSet = new DataSet();
        private Guid userOid;

        public UCCusSPL(string name)
        {
            this.InitializeComponent();
            this.myClassName = name;
            this.dropDownTree.TreeNodeSelect = new AfterNodeSelected(this.NodeSelected);
            this.AddIcon();
            this.userOid = ClientData.LogonUser.Oid;
        }

        private void AddFilter(ArrayList attrList)
        {
            for (int i = 0; i < attrList.Count; i++)
            {
                DEMetaAttribute attribute = (DEMetaAttribute) attrList[i];
                if (attribute.Name == "PLM_ID")
                {
                    this.txtValue.Tag = attribute;
                    return;
                }
            }
        }

        private void AddIcon()
        {
            string[] resNames = new string[] { "ICO_RES_PRE", "ICO_RES_NXT" };
            ClientData.MyImageList.AddIcons(resNames);
            this.tb.ImageList = ClientData.MyImageList.imageList;
            this.tbnPre.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PRE");
            this.tbnNext.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NXT");
        }

        private void comb_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FilterData();
        }

        private StringBuilder CreateCondition(ComboBoxEditPLM cob, TextEditPLM txtBox)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("PLM_ID");
            string text = cob.Text;
            if (text != null)
            {
                if (text == "前几字符是")
                {
                    builder.Append(" LIKE ");
                    if ((txtBox.Text == null) || (txtBox.Text == ""))
                    {
                        builder.Append(" '");
                        builder.Append("*' ");
                        return builder;
                    }
                    builder.Append(" '");
                    builder.Append(txtBox.Text);
                    builder.Append("*' ");
                    return builder;
                }
                if (text == "后几字符是")
                {
                    builder.Append(" LIKE ");
                    if ((txtBox.Text == null) || (txtBox.Text == ""))
                    {
                        builder.Append("'*");
                        builder.Append("' ");
                        return builder;
                    }
                    builder.Append("'*");
                    builder.Append(txtBox.Text);
                    builder.Append("' ");
                    return builder;
                }
                if (text == "包含")
                {
                    builder.Append(" LIKE ");
                    if ((txtBox.Text == null) || (txtBox.Text == ""))
                    {
                        builder.Append("'*");
                        builder.Append("' ");
                        return builder;
                    }
                    builder.Append("'*");
                    builder.Append(txtBox.Text);
                    builder.Append("*' ");
                    return builder;
                }
                if (text == "等于")
                {
                    if (txtBox.Text == null)
                    {
                        builder.Append(" LIKE ");
                        builder.Append("'*");
                        builder.Append("' ");
                        return builder;
                    }
                    builder.Append(" = ");
                    builder.Append("'");
                    builder.Append(txtBox.Text);
                    builder.Append("' ");
                    return builder;
                }
                if (text != "不等于")
                {
                    return builder;
                }
                if (txtBox.Text == null)
                {
                    builder.Append(" LIKE ");
                    builder.Append("'*'");
                    return builder;
                }
                builder.Append(" <> ");
                builder.Append("'");
                builder.Append(txtBox.Text);
                builder.Append("' ");
            }
            return builder;
        }

        private void CreateLvwStyle(ListView lvw, ArrayList attrList)
        {
            lvw.Clear();
            ColumnHeader header = new ColumnHeader {
                Text = "代号",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            lvw.Columns.Add(header);
            header = new ColumnHeader {
                Text = "类名",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            lvw.Columns.Add(header);
            header = new ColumnHeader {
                Text = "安全级别",
                TextAlign = HorizontalAlignment.Center,
                Width = 100
            };
            lvw.Columns.Add(header);
            for (int i = 0; i < attrList.Count; i++)
            {
                DEMetaAttribute attribute = (DEMetaAttribute) attrList[i];
                if (attribute.IsViewable)
                {
                    header = new ColumnHeader {
                        Text = attribute.Label,
                        TextAlign = HorizontalAlignment.Center,
                        Width = 100
                    };
                    lvw.Columns.Add(header);
                }
            }
        }

        private void DisplayData(DataView dv)
        {
            try
            {
                if (dv.Count == 0)
                {
                    this.lvwMid.Items.Clear();
                    this.lvw.Items.Clear();
                    this.tbnNext.Enabled = false;
                    this.tbnPre.Enabled = false;
                }
                else
                {
                    int num;
                    int num2;
                    ListViewItem item;
                    this.lvwMid.Items.Clear();
                    this.lvw.Items.Clear();
                    foreach (DataRowView view in dv)
                    {
                        item = new ListViewItem {
                            Text = view[1].ToString(),
                            SubItems = { 
                                view[2].ToString(),
                                view[3].ToString()
                            }
                        };
                        num = 0;
                        while (num < (dv.Table.Columns.Count - 5))
                        {
                            DEMetaAttribute attribute = (DEMetaAttribute) this.attrList[num];
                            if (attribute.IsViewable)
                            {
                                item.SubItems.Add(view[num + 5].ToString());
                            }
                            num++;
                        }
                        if (item.SubItems.Count > 0)
                        {
                            item.Tag = view;
                            this.lvwMid.Items.Add(item);
                        }
                    }
                    if (this.lvwMid.Items.Count <= 0x19)
                    {
                        for (num = 0; num < this.lvwMid.Items.Count; num++)
                        {
                            item = new ListViewItem {
                                Text = this.lvwMid.Items[num].Text,
                                Tag = this.lvwMid.Items[num].Tag
                            };
                            num2 = 1;
                            while (num2 < this.lvwMid.Items[num].SubItems.Count)
                            {
                                item.SubItems.Add(this.lvwMid.Items[num].SubItems[num2].Text);
                                num2++;
                            }
                            this.lvw.Items.Add(item);
                        }
                        this.pageNum = 0;
                        this.tbnNext.Enabled = false;
                        this.tbnPre.Enabled = false;
                    }
                    else
                    {
                        for (num = 0; num < 0x19; num++)
                        {
                            item = new ListViewItem {
                                Text = this.lvwMid.Items[num].Text,
                                Tag = this.lvwMid.Items[num].Tag
                            };
                            for (num2 = 1; num2 < this.lvwMid.Items[num].SubItems.Count; num2++)
                            {
                                item.SubItems.Add(this.lvwMid.Items[num].SubItems[num2].Text);
                            }
                            this.lvw.Items.Add(item);
                        }
                        this.pageNum = 0;
                        this.tbnNext.Enabled = true;
                        this.tbnPre.Enabled = false;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBoxPLM.Show("显示数据出错：" + exception.Message, "工程资源");
            }
        }
 
        private void FilterData()
        {
            if (this.myView != null)
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    if ((this.txtValue.Text == null) || (this.txtValue.Text == ""))
                    {
                        this.myView.RowFilter = "";
                    }
                    else
                    {
                        this.myView.RowFilter = this.CreateCondition(this.cob, this.txtValue).ToString();
                    }
                    this.DisplayData(this.myView);
                }
                catch
                {
                    MessageBoxPLM.Show("过滤数据发生错误:请检查输入的数据类型是否正确", "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private ArrayList GetAttributes(string classname) {
            return ModelContext.MetaModel.GetAttributes(classname);
        }

        private void lvw_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (this.lvw.SelectedItems.Count >= 1)
            {
                CLCopyData data = new CLCopyData();
                foreach (ListViewItem item in this.lvw.SelectedItems)
                {
                    DataRowView tag = (DataRowView) item.Tag;
                    try
                    {
                        DEBusinessItem item2 = PLItem.Agent.GetBizItemByMaster(new Guid((byte[]) tag[0]), 0, ClientData.UserGlobalOption.CurView, ClientData.LogonUser.Oid, BizItemMode.BizItem) as DEBusinessItem;
                        if (item2 != null)
                        {
                            data.Add(item2);
                        }
                    }
                    catch (PLMException exception)
                    {
                        PrintException.Print(exception);
                        return;
                    }
                    catch (Exception exception2)
                    {
                        MessageBoxPLM.Show("拖动资源数据出错：" + exception2.Message, "工程资源");
                        return;
                    }
                }
                this.lvw.DoDragDrop(data, DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        private void NodeSelected(TreeNode node)
        {
            if (this.dropDownTree.SelectedNode == null)
            {
                this.gbFilter.Visible = false;
                this.lvw.Clear();
            }
            else if (this.dropDownTree.SelectedNode.Text != this.clsName)
            {
                this.curFolder = (DEResFolder) this.dropDownTree.SelectedNode.Tag;
                if (typeof(DEMetaClass).IsInstanceOfType(this.dropDownTree.SelectedNode.Tag))
                {
                    this.gbFilter.Visible = true;
                    this.clsName = this.dropDownTree.SelectedNode.Text;
                    DEMetaClass tag = (DEMetaClass) this.dropDownTree.SelectedNode.Tag;
                    this.attrList = this.GetAttributes(tag.Name);
                    this.AddFilter(this.attrList);
                    this.myView = null;
                    if (this.txtValue.Text != "")
                    {
                        this.txtValue.Text = "";
                    }
                    if (this.cob.Text != "等于")
                    {
                        this.cob.Text = "等于";
                    }
                    PLSPL plspl = new PLSPL();
                    try
                    {
                        this.theDataSet = plspl.GetSPLDataSet(tag.Name, this.attrList, this.userOid, this.curFolder.FilterString, this.curFolder.FilterValue);
                    }
                    catch (PLMException exception)
                    {
                        PrintException.Print(exception);
                    }
                    catch (Exception exception2)
                    {
                        MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    if (this.theDataSet.Tables.Count != 0)
                    {
                        this.CreateLvwStyle(this.lvw, this.attrList);
                        if (this.theDataSet.Tables[0].Rows.Count == 0)
                        {
                            this.tbnNext.Enabled = false;
                            this.tbnPre.Enabled = false;
                        }
                        else
                        {
                            this.myView = this.theDataSet.Tables[0].DefaultView;
                            this.DisplayData(this.myView);
                        }
                    }
                }
                else
                {
                    this.gbFilter.Visible = false;
                    this.lvw.Clear();
                }
            }
        }

        private void tb_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            int num;
            int num2;
            ListViewItem item;
            if (e.Button == this.tbnPre)
            {
                this.pageNum--;
                if (this.pageNum < 0)
                {
                    MessageBoxPLM.Show("PageNum < 0 ", "工程资源", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    this.lvw.Items.Clear();
                    for (num = 0x19 * this.pageNum; num < (0x19 + (0x19 * this.pageNum)); num++)
                    {
                        item = new ListViewItem {
                            Text = this.lvwMid.Items[num].Text,
                            Tag = this.lvwMid.Items[num].Tag
                        };
                        num2 = 1;
                        while (num2 < this.lvwMid.Items[num].SubItems.Count)
                        {
                            item.SubItems.Add(this.lvwMid.Items[num].SubItems[num2].Text);
                            num2++;
                        }
                        this.lvw.Items.Add(item);
                    }
                    if (this.pageNum == 0)
                    {
                        this.tbnPre.Enabled = false;
                    }
                    this.tbnNext.Enabled = true;
                }
            }
            else if (e.Button == this.tbnNext)
            {
                this.pageNum++;
                if (this.lvwMid.Items.Count <= (0x19 * (this.pageNum + 1)))
                {
                    this.lvw.Items.Clear();
                    for (num = 0x19 * this.pageNum; num < this.lvwMid.Items.Count; num++)
                    {
                        item = new ListViewItem {
                            Text = this.lvwMid.Items[num].Text,
                            Tag = this.lvwMid.Items[num].Tag
                        };
                        num2 = 1;
                        while (num2 < this.lvwMid.Items[num].SubItems.Count)
                        {
                            item.SubItems.Add(this.lvwMid.Items[num].SubItems[num2].Text);
                            num2++;
                        }
                        this.lvw.Items.Add(item);
                    }
                    this.tbnPre.Enabled = true;
                    this.tbnNext.Enabled = false;
                }
                else
                {
                    this.lvw.Items.Clear();
                    for (num = 0x19 * this.pageNum; num < (0x19 + (0x19 * this.pageNum)); num++)
                    {
                        item = new ListViewItem {
                            Text = this.lvwMid.Items[num].Text,
                            Tag = this.lvwMid.Items[num].Tag
                        };
                        for (num2 = 1; num2 < this.lvwMid.Items[num].SubItems.Count; num2++)
                        {
                            item.SubItems.Add(this.lvwMid.Items[num].SubItems[num2].Text);
                        }
                        this.lvw.Items.Add(item);
                    }
                    this.tbnPre.Enabled = true;
                    this.tbnNext.Enabled = true;
                }
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.FilterData();
        }

        private void UCCusSPL_Load(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode("引用资源");
            this.dropDownTree.treeView.Nodes.Add(node);
            TreeView tvwClasses = new TreeView();
            UIDataModel.FillClassesTree(tvwClasses, this.myClassName, -1, -1);
            if (tvwClasses.Nodes.Count > 0)
            {
                foreach (TreeNode node3 in tvwClasses.Nodes)
                {
                    TreeNode node2 = node3;
                    tvwClasses.Nodes.Remove(node3);
                    this.dropDownTree.treeView.Nodes[0].Nodes.Add(node2);
                }
            }
            this.gbFilter.Visible = false;
            this.tbnPre.Enabled = false;
            this.tbnNext.Enabled = false;
            this.dropDownTree.SelectedNode = null;
        }
    }
}

