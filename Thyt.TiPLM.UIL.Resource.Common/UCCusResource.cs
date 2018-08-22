    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.PLL.Resource;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Common.UserControl;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public partial class UCCusResource : UserControlPLM, IResControl
    {
        private ArrayList attrList = new ArrayList();
        private string ClsName = "";
        private ArrayList cobList = new ArrayList();
       
        private ArrayList lblList = new ArrayList();
        private ListView lvwMid = new ListView();
        private DataView myView = new DataView();
        private int pageNum;
        
        private const int showNum = 0x19;
        private static ulong Stamp;
       
        private static string tempPath = ConstConfig.GetCachePath();
        private DataSet theDataSet;
        private ArrayList txtBoxList = new ArrayList();
        private ArrayList txtList = new ArrayList();

        public UCCusResource(string className)
        {
            this.InitializeComponent();
            this.ClsName = className;
            this.attrList = GetAttributes(this.ClsName);
            if (this.attrList.Count <= 0)
            {
                this.pnlFilter.Visible = false;
            }
            else
            {
                this.AddFilter(this.attrList);
                this.CreateLvwStyle(this.lvw, this.attrList);
                string[] resNames = new string[] { "ICO_RES_PRE", "ICO_RES_NXT", "ICO_RES_CLEAR", "ICO_RES_REFRESH" };
                ClientData.MyImageList.AddIcons(resNames);
                this.tb.ImageList = ClientData.MyImageList.imageList;
                this.tbnPre.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_PRE");
                this.tbnNext.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_NXT");
                this.tbnClear.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_CLEAR");
                this.tbnRefresh.ImageIndex = ClientData.MyImageList.GetIconIndex("ICO_RES_REFRESH");
            }
        }

        private void AddFilter(ArrayList attrList)
        {
            DEMetaAttribute attribute;
            ArrayList list = new ArrayList();
            for (int i = 0; i < attrList.Count; i++)
            {
                attribute = (DEMetaAttribute) attrList[i];
                if (attribute.IsViewable && attribute.IsFilterable)
                {
                    list.Add(attribute);
                }
            }
            if (list.Count <= 0)
            {
                this.pnlFilter.Visible = false;
            }
            else
            {
                this.pnlFilter.Visible = true;
                this.pnlFilter.Size = new Size(base.Width, 0x12 + (list.Count * 0x19));
                int width = 0;
                int num = 0;
                while (num < list.Count)
                {
                    attribute = (DEMetaAttribute) list[num];
                    LabelPLM lplm = new LabelPLM {
                        Top = 0x10 + (num * 0x19),
                        Left = 5,
                        Name = "lbl_" + attribute.Name,
                        Text = attribute.Label,
                        AutoSize = true,
                        Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top
                    };
                    if (width < lplm.Width)
                    {
                        width = lplm.Width;
                    }
                    ComboBoxEditPLM tplm2 = new ComboBoxEditPLM {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Left = lplm.Right + 5,
                        BackColor = SystemColors.Control,
                        Top = 0x10 + (num * 0x19),
                        Height = 20,
                        Width = 80
                    };
                    if (((attribute.DataType == 0) || (attribute.DataType == 1)) || ((attribute.DataType == 2) || (attribute.DataType == 6)))
                    {
                        tplm2.Properties.Items.AddRange(new object[] { "等于", "大于", "小于", "大于等于", "小于等于", "不等于", "在..之间", "不在..之间" });
                    }
                    else if (((attribute.DataType == 4) || (attribute.DataType == 7)) || ((attribute.DataType == 3) || (attribute.DataType == 5)))
                    {
                        tplm2.Properties.Items.AddRange(new object[] { "等于", "不等于", "前几字符是", "后几字符是", "包含" });
                        tplm2.Text = "等于";
                    }
                    else
                    {
                        tplm2.Properties.Items.AddRange(new object[] { "等于", "大于", "小于", "大于等于", "小于等于", "不等于", "在..之间", "不在..之间", "前几字符是", "后几字符是", "包含" });
                    }
                    tplm2.Name = "comb_" + attribute.Name;
                    if (tplm2.Text == "")
                    {
                        tplm2.Text = "等于";
                    }
                    tplm2.Tag = 0;
                    tplm2.SelectedIndexChanged += new EventHandler(this.comb_SelectedIndexChanged);
                    TextEditPLM tplm = new TextEditPLM {
                        Left = tplm2.Right + 5,
                        BackColor = SystemColors.Control,
                        Top = 0x10 + (num * 0x19),
                        Name = "txtValue_" + attribute.Name,
                        Size = new Size(80, 0x15),
                        Text = "",
                        Tag = attribute,
                        Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top
                    };
                    tplm.TextChanged += new EventHandler(this.txtValue_TextChanged);
                    this.gbFilter.Controls.Add(lplm);
                    this.gbFilter.Controls.Add(tplm2);
                    this.gbFilter.Controls.Add(tplm);
                    this.txtBoxList.Add(tplm);
                    this.cobList.Add(tplm2);
                    num++;
                }
                for (num = 0; num < this.cobList.Count; num++)
                {
                    ComboBoxEditPLM tplm3 = (ComboBoxEditPLM) this.cobList[num];
                    tplm3.Left = (5 + width) + 5;
                    tplm3.Width = 0x58;
                    TextEditPLM tplm4 = (TextEditPLM) this.txtBoxList[num];
                    tplm4.Left = tplm3.Right + 5;
                    tplm4.Width = (base.Width - tplm4.Left) - 5;
                }
            }
        }

        public static void CacheObject(object tagObject, string clsName)
        {
        }

        private void ClearConditon()
        {
            for (int i = 0; i < this.txtBoxList.Count; i++)
            {
                TextEditPLM tplm = (TextEditPLM) this.txtBoxList[i];
                tplm.Text = "";
                ComboBoxEditPLM tplm2 = (ComboBoxEditPLM) this.cobList[i];
                if ((tplm2.Text == "在..之间") || (tplm2.Text == "不在..之间"))
                {
                    tplm.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                    tplm.Width = (base.Width - tplm.Left) - 5;
                    tplm2.Tag = 0;
                    for (int j = 0; j < this.lblList.Count; j++)
                    {
                        LabelPLM lplm = (LabelPLM) this.lblList[j];
                        TextEditPLM tplm3 = (TextEditPLM) this.txtList[j];
                        if (lplm.Tag == tplm.Tag)
                        {
                            this.gbFilter.Controls.Remove(lplm);
                            this.lblList.Remove(lplm);
                        }
                        if (tplm3.Tag == tplm.Tag)
                        {
                            this.gbFilter.Controls.Remove(tplm3);
                            this.txtList.Remove(tplm3);
                        }
                    }
                }
                tplm2.Text = "等于";
            }
            try
            {
                this.myView.RowFilter = "";
                this.DisplayData(this.myView);
            }
            catch (PLMException exception)
            {
                PrintException.Print(exception);
            }
            catch (Exception exception2)
            {
                MessageBoxPLM.Show("过滤数据发生错误" + exception2.ToString(), "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void comb_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextEditPLM tplm2;
            int num = -1;
            this.FilterData();
            for (int i = 0; i < this.cobList.Count; i++)
            {
                ComboBoxEditPLM tplm = (ComboBoxEditPLM) this.cobList[i];
                if (tplm.ContainsFocus)
                {
                    int tag = (int) tplm.Tag;
                    if ((tplm.Text == "在..之间") || (tplm.Text == "不在..之间"))
                    {
                        if (tag == 1)
                        {
                            return;
                        }
                        num = i;
                        tplm.Tag = 1;
                    }
                    else
                    {
                        switch (tag)
                        {
                            case 0:
                                return;

                            case 1:
                                for (int j = 0; j < this.lblList.Count; j++)
                                {
                                    tplm2 = (TextEditPLM) this.txtBoxList[i];
                                    tplm2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
                                    tplm2.Width = (base.Width - tplm2.Left) - 5;
                                    tplm.Tag = 0;
                                    LabelPLM lplm = (LabelPLM) this.lblList[j];
                                    TextEditPLM tplm3 = (TextEditPLM) this.txtList[j];
                                    if (lplm.Tag == tplm2.Tag)
                                    {
                                        this.gbFilter.Controls.Remove(lplm);
                                        this.lblList.Remove(lplm);
                                    }
                                    if (tplm3.Tag == tplm2.Tag)
                                    {
                                        this.gbFilter.Controls.Remove(tplm3);
                                        this.txtList.Remove(tplm3);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            if (num >= 0)
            {
                tplm2 = (TextEditPLM) this.txtBoxList[num];
                tplm2.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                tplm2.Width = ((base.Width - tplm2.Left) - 50) / 2;
                LabelPLM lplm2 = new LabelPLM {
                    Top = tplm2.Top,
                    Left = tplm2.Right + 5,
                    Text = " AND ",
                    Width = 0x23,
                    Tag = tplm2.Tag
                };
                this.lblList.Add(lplm2);
                TextEditPLM tplm4 = new TextEditPLM {
                    Top = tplm2.Top,
                    Left = lplm2.Right + 5,
                    Text = ""
                };
                tplm4.Width = (base.Width - 5) - tplm4.Left;
                tplm4.Tag = tplm2.Tag;
                tplm4.BackColor = SystemColors.Control;
                tplm4.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                tplm4.TextChanged += new EventHandler(this.txtValue_TextChanged);
                this.txtList.Add(tplm4);
                this.gbFilter.Controls.Add(lplm2);
                this.gbFilter.Controls.Add(tplm4);
            }
        }

        private StringBuilder CreateCondition(ComboBoxEditPLM cob, TextEditPLM txtBox)
        {
            DEMetaAttribute attribute2;
            TextEditPLM tplm;
            DEMetaAttribute tag = (DEMetaAttribute) txtBox.Tag;
            StringBuilder builder = new StringBuilder();
            if (cob.Text == "不在..之间")
            {
                builder.Append("(");
            }
            builder.Append("PLM_");
            builder.Append(tag.Name);
            switch (cob.Text)
            {
                case "前几字符是":
                    builder.Append(" LIKE ");
                    builder.Append(" '");
                    builder.Append(txtBox.Text);
                    builder.Append("*' ");
                    return builder;

                case "后几字符是":
                    builder.Append(" LIKE ");
                    builder.Append("'*");
                    builder.Append(txtBox.Text);
                    builder.Append("' ");
                    return builder;

                case "包含":
                    builder.Append(" LIKE ");
                    builder.Append("'*");
                    builder.Append(txtBox.Text);
                    builder.Append("*' ");
                    return builder;

                case "大于":
                    builder.Append(" > ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "小于":
                    builder.Append(" < ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "等于":
                    if (((tag.DataType != 0) && (tag.DataType != 1)) && ((tag.DataType != 2) && (tag.DataType != 6)))
                    {
                        builder.Append(" = ");
                        builder.Append(" '");
                        builder.Append(txtBox.Text);
                        builder.Append("' ");
                        return builder;
                    }
                    builder.Append(" = ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "小于等于":
                    builder.Append(" <= ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "大于等于":
                    builder.Append(" >= ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "不等于":
                    if (((tag.DataType != 0) && (tag.DataType != 1)) && ((tag.DataType != 2) && (tag.DataType != 6)))
                    {
                        builder.Append(" <> ");
                        builder.Append(" '");
                        builder.Append(txtBox.Text);
                        builder.Append("' ");
                        return builder;
                    }
                    builder.Append(" <> ");
                    builder.Append(txtBox.Text);
                    return builder;

                case "在..之间":
                    builder.Append(" >= ");
                    builder.Append(txtBox.Text);
                    builder.Append(" AND ");
                    builder.Append("PLM_");
                    builder.Append(tag.Name);
                    builder.Append(" <= ");
                    for (int i = 0; i < this.txtList.Count; i++)
                    {
                        tplm = (TextEditPLM) this.txtList[i];
                        attribute2 = (DEMetaAttribute) tplm.Tag;
                        if (attribute2.Oid == tag.Oid)
                        {
                            builder.Append(tplm.Text);
                            return builder;
                        }
                    }
                    return builder;

                case "不在..之间":
                    builder.Append(" < ");
                    builder.Append(txtBox.Text);
                    builder.Append(" OR ");
                    builder.Append("PLM_");
                    builder.Append(tag.Name);
                    builder.Append(" > ");
                    for (int j = 0; j < this.txtList.Count; j++)
                    {
                        tplm = (TextEditPLM) this.txtList[j];
                        attribute2 = (DEMetaAttribute) tplm.Tag;
                        if (attribute2.Oid == tag.Oid)
                        {
                            builder.Append(tplm.Text);
                            builder.Append(")");
                            return builder;
                        }
                    }
                    return builder;
            }
            return builder;
        }

        private void CreateLvwStyle(ListView lvw, ArrayList attrList)
        {
            lvw.Clear();
            for (int i = 0; i < attrList.Count; i++)
            {
                DEMetaAttribute attribute = (DEMetaAttribute) attrList[i];
                if (attribute.IsViewable)
                {
                    ColumnHeader header = new ColumnHeader {
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
                    item = new ListViewItem();
                    int num3 = 0;
                    string str = "";
                    num = 0;
                    while (num < this.lvw.Columns.Count)
                    {
                        foreach (DEMetaAttribute attribute in this.attrList)
                        {
                            if (attribute.IsViewable && (this.lvw.Columns[num].Text == attribute.Label))
                            {
                                str = "PLM_" + attribute.Name;
                                if (num3 == 0)
                                {
                                    item.Text = view[str].ToString();
                                }
                                else
                                {
                                    item.SubItems.Add(view[str].ToString());
                                }
                                num3++;
                                break;
                            }
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

        public void DisplayResourceData()
        {
            string str = "PLM_CUS_" + this.ClsName;
            GetData(out Stamp, out this.theDataSet, this.ClsName, this.attrList);
            if ((this.theDataSet != null) && (this.theDataSet.Tables.Count != 0))
            {
                if (this.theDataSet.Tables[0].Rows.Count == 0)
                {
                    this.tbnNext.Enabled = false;
                    this.tbnPre.Enabled = false;
                    this.tbnRefresh.Enabled = false;
                }
                else
                {
                    this.myView = this.theDataSet.Tables[str].DefaultView;
                    this.DisplayData(this.myView);
                }
            }
        }

 
        private void FilterData()
        {
            int num3 = 0;
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            if (this.myView != null)
            {
                for (int i = 0; i < this.txtBoxList.Count; i++)
                {
                    TextEditPLM txtBox = (TextEditPLM) this.txtBoxList[i];
                    if (txtBox.Text != "")
                    {
                        ComboBoxEditPLM cob = (ComboBoxEditPLM) this.cobList[i];
                        if ((cob.Text == "在..之间") || (cob.Text == "不在..之间"))
                        {
                            DEMetaAttribute tag = (DEMetaAttribute) txtBox.Tag;
                            for (int j = 0; j < this.txtList.Count; j++)
                            {
                                TextEditPLM tplm2 = (TextEditPLM) this.txtList[j];
                                DEMetaAttribute attribute2 = (DEMetaAttribute) tplm2.Tag;
                                if ((tag.Oid == attribute2.Oid) && (tplm2.Text != ""))
                                {
                                    num3++;
                                    if (num3 > 1)
                                    {
                                        builder.Append(" AND ");
                                    }
                                    builder2 = this.CreateCondition(cob, txtBox);
                                    builder.Append(builder2.ToString());
                                }
                            }
                        }
                        else
                        {
                            num3++;
                            if (num3 > 1)
                            {
                                builder.Append(" AND ");
                            }
                            builder.Append(this.CreateCondition(cob, txtBox).ToString());
                        }
                    }
                }
                try
                {
                    this.myView.RowFilter = builder.ToString();
                    this.DisplayData(this.myView);
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch
                {
                    MessageBoxPLM.Show("过滤数据发生错误:请检查输入的数据类型是否正确", "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private static ArrayList GetAttributes(string classname) {
          return  ModelContext.MetaModel.GetAttributes(classname);
        }

        public static void GetData(out ulong stamp, out DataSet ds, string clsName, ArrayList attrList)
        {
            PLCustomizeResource resource = new PLCustomizeResource();
            ds = new DataSet();
            stamp = 0L;
            try
            {
                ulong num = resource.GetStamp(clsName);
                if (num == 0L)
                {
                    resource.AddStamp(clsName);
                    num = 1L;
                }
                ds = (DataSet) GetObject(clsName);
                if (ds == null)
                {
                    try
                    {
                        ds = resource.GetDataSetIncludeStamp(clsName);
                        stamp = num;
                        CacheObject(ds, clsName);
                    }
                    catch (PLMException exception)
                    {
                        PrintException.Print(exception);
                    }
                    catch (Exception exception2)
                    {
                        MessageBoxPLM.Show("读取数据集发生错误" + exception2.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {
                    string str = "PLM_SYS_PARAMETER";
                    new PLCustomizeResource();
                    string str2 = "PLM_CUS_" + clsName;
                    ArrayList attributes = GetAttributes(clsName);
                    if (ds.Tables[str] != null)
                    {
                        stamp = Convert.ToUInt64(ds.Tables[str].Rows[0][0]);
                    }
                    if (attributes.Count == ds.Tables[str2].Columns.Count)
                    {
                        for (int i = 0; i < attributes.Count; i++)
                        {
                            DEMetaAttribute attribute = (DEMetaAttribute) attributes[i];
                            DEMetaAttribute attribute2 = (DEMetaAttribute) attrList[i];
                            if (attribute.Oid != attribute2.Oid)
                            {
                                return;
                            }
                        }
                        if (stamp != num)
                        {
                            try
                            {
                                ds = resource.GetDataSetIncludeStamp(clsName);
                                stamp = num;
                                CacheObject(ds, clsName);
                            }
                            catch (PLMException exception3)
                            {
                                PrintException.Print(exception3);
                            }
                            catch (Exception exception4)
                            {
                                MessageBoxPLM.Show("读取数据集发生错误" + exception4.ToString(), "读取数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                    }
                }
            }
            catch (PLMException exception5)
            {
                PrintException.Print(exception5);
            }
            catch (Exception exception6)
            {
                MessageBoxPLM.Show("读取数据集发生错误" + exception6.ToString(), "工程资源", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        public static object GetObject(string ClsName)
        {
            object obj3;
            string str = "Res" + ClsName;
            if (!File.Exists(tempPath + str))
            {
                return null;
            }
            FileStream serializationStream = new FileStream(tempPath + str, FileMode.Open);
            try
            {
                object obj2 = new BinaryFormatter().Deserialize(serializationStream);
                serializationStream.Close();
                obj3 = obj2;
            }
            catch (Exception)
            {
                obj3 = null;
            }
            finally
            {
                serializationStream.Close();
            }
            return obj3;
        }


        private void lvw_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (this.lvw.SelectedItems.Count >= 1)
            {
                DECopyData data = new DECopyData();
                CLCopyData data2 = new CLCopyData();
                data.ClassName = this.ClsName;
                foreach (ListViewItem item in this.lvw.SelectedItems)
                {
                    data.ItemList.Add((DataRowView) item.Tag);
                }
                data2.Add(data);
                this.lvw.DoDragDrop(data2, DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy);
            }
        }

        public void ReleaseDataSet()
        {
            if (this.theDataSet != null)
            {
                this.theDataSet.Dispose();
                this.theDataSet = null;
                GC.Collect();
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
            else if (e.Button == this.tbnClear)
            {
                this.ClearConditon();
            }
            else if (e.Button == this.tbnRefresh)
            {
                string str = "PLM_CUS_" + this.ClsName;
                GetData(out Stamp, out this.theDataSet, this.ClsName, this.attrList);
                if ((this.theDataSet.Tables.Count != 0) && (this.theDataSet.Tables[0].Rows.Count != 0))
                {
                    this.myView = this.theDataSet.Tables[str].DefaultView;
                    this.ClearConditon();
                }
            }
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            this.FilterData();
        }

        private void UCCusResource_Load(object sender, EventArgs e)
        {
        }
    }
}

