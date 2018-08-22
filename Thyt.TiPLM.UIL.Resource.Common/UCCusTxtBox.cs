    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Product;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common
{
    public partial class UCCusTxtBox : UserControlPLM
    {
        private ArrayList attrs;
        private string attrValue;
        private string clsName;
        private const string comColumn = "PLM_Res_ComColumn";
        private int flag;
        public ListBoxPLM lbFilter;
        private DEMetaAttribute metaAttr;
        private int MinHeight;
        private int MinWidth;
        private DataSet myds;
        private DataView myView;
        private ArrayList oidList;
        private Guid resOid;
        protected SplitterPLM spl;
        private static ulong Stamp;
        public object Tag2;
        public TextEditPLM txtInput;

        public UCCusTxtBox()
        {
            this.resOid = Guid.Empty;
            this.MinHeight = 150;
            this.attrValue = "";
            this.attrs = new ArrayList();
            this.InitializeComponent();
        }

        public UCCusTxtBox(Guid classOid)
        {
            this.resOid = Guid.Empty;
            this.MinHeight = 150;
            this.attrValue = "";
            this.attrs = new ArrayList();
            this.InitializeComponent();
            this.clsName = this.GetClassName(classOid);
            this.attrs = this.GetAttributes(this.clsName);
            this.InitControls();
            this.SetSizeAndLocation();
            base.Controls.AddRange(new Control[] { this.txtInput, this.spl, this.lbFilter });
            base.Resize += new EventHandler(this.ControlResize);
            base.LocationChanged += new EventHandler(this.ControlResize);
            this.flag = 1;
        }

        public UCCusTxtBox(string clsName)
        {
            this.resOid = Guid.Empty;
            this.MinHeight = 150;
            this.attrValue = "";
            this.attrs = new ArrayList();
            this.InitializeComponent();
            this.clsName = clsName;
            this.attrs = this.GetAttributes(this.clsName);
            this.InitControls();
            this.SetSizeAndLocation();
            base.Controls.AddRange(new Control[] { this.txtInput, this.spl, this.lbFilter });
            base.Resize += new EventHandler(this.ControlResize);
            base.LocationChanged += new EventHandler(this.ControlResize);
            this.flag = 1;
        }

        private void ControlResize(object sender, EventArgs e)
        {
            this.SetSizeAndLocation();
        }

        private void DisplayData(DataView myView)
        {
            if (myView != null)
            {
                if (myView.Count <= 0)
                {
                    this.spl.Visible = false;
                    this.lbFilter.Visible = false;
                }
                else
                {
                    this.lbFilter.Items.Clear();
                    this.oidList = new ArrayList();
                    foreach (DataRowView view in myView)
                    {
                        if (!this.IsInListBox(view["PLM_Res_ComColumn"].ToString(), this.lbFilter) && (view["PLM_Res_ComColumn"].ToString() != ""))
                        {
                            this.lbFilter.Items.Add(view["PLM_Res_ComColumn"].ToString());
                            this.oidList.Add(view["PLM_OID"]);
                        }
                    }
                    this.lbFilter.SelectedIndex = 0;
                    base.Parent.Controls.Add(this.lbFilter);
                    this.spl.Visible = true;
                    this.lbFilter.Visible = true;
                    this.lbFilter.BringToFront();
                    this.SetListBoxLocation();
                    if ((this.lbFilter.Width < base.Width) || (this.lbFilter.Height < base.Height))
                    {
                        this.lbFilter.Width = this.MinWidth;
                        this.lbFilter.Height = 150;
                    }
                }
            }
        } 

        private void FilterData(string str)
        {
            if (this.myView != null)
            {
                try
                {
                    StringBuilder builder = new StringBuilder();
                    if (str != "")
                    {
                        builder.Append("PLM_Res_ComColumn");
                        builder.Append(" LIKE ");
                        builder.Append(" '");
                        builder.Append(str);
                        builder.Append("*' ");
                        this.myView.RowFilter = builder.ToString();
                    }
                    else
                    {
                        this.myView.RowFilter = "";
                    }
                }
                catch
                {
                    MessageBoxPLM.Show("过滤数据发生错误:请检查输入的数据类型是否正确！", "筛选数据集", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private ArrayList GetAttributes(string classname){
           return ModelContext.MetaModel.GetAttributes(classname);
        }
        private string GetClassName(Guid clsId){
           return ModelContext.MetaModel.GetClass(clsId).Name;
        }
        private DataRow GetDataRowByGuid(Guid resOid, DataSet ds)
        {
            string str = "PLM_CUS_" + this.clsName;
            if (ds != null)
            {
                foreach (DataRow row in ds.Tables[str].Rows)
                {
                    Guid guid = new Guid((byte[]) row["PLM_OID"]);
                    if (guid == resOid)
                    {
                        return row;
                    }
                }
            }
            return null;
        }

        public string GetResourceID(Guid resOID, string clsName)
        {
            string str = "PLM_CUS_" + this.clsName;
            string str2 = "";
            ulong stamp = 0L;
            if (resOID != Guid.Empty)
            {
                DataSet set;
                UCCusResource.GetData(out stamp, out set, clsName, this.attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return str2;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return str2;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        Guid guid = new Guid((byte[]) row["PLM_OID"]);
                        if (resOID == guid)
                        {
                            return row["PLM_ID"].ToString();
                        }
                    }
                    return str2;
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    MessageBoxPLM.Show("error!" + exception2.Message, "工程资源");
                }
            }
            return str2;
        }

        public Guid GetResourceOID(string id, string clsName)
        {
            string str = "PLM_CUS_" + this.clsName;
            Guid empty = Guid.Empty;
            ulong stamp = 0L;
            if (id != "")
            {
                DataSet set;
                UCCusResource.GetData(out stamp, out set, clsName, this.attrs);
                if ((set == null) || (set.Tables.Count <= 0))
                {
                    return empty;
                }
                if (set.Tables[str].Rows.Count <= 0)
                {
                    return empty;
                }
                try
                {
                    foreach (DataRow row in set.Tables[str].Rows)
                    {
                        string str2 = row["PLM_ID"].ToString();
                        if (id == str2)
                        {
                            return new Guid((byte[]) row["PLM_OID"]);
                        }
                    }
                    return empty;
                }
                catch (PLMException exception)
                {
                    PrintException.Print(exception);
                }
                catch (Exception exception2)
                {
                    MessageBoxPLM.Show("error!" + exception2.Message, "工程资源");
                }
            }
            return empty;
        }

        private void InitControls()
        {
            this.txtInput = new TextEditPLM();
            this.txtInput.Size = new Size(0x110, 0x15);
            this.txtInput.AllowDrop = true;
            this.txtInput.KeyDown += new KeyEventHandler(this.txtInput_KeyDown);
            this.txtInput.DragDrop += new DragEventHandler(this.txtInput_DragDrop);
            this.txtInput.TextChanged += new EventHandler(this.txtInput_TextChanged);
            this.txtInput.DragEnter += new DragEventHandler(this.txtInput_DragEnter);
            this.txtInput.Leave += new EventHandler(this.txtInput_Leave);
            this.txtInput.DoubleClick += new EventHandler(this.txtInput_DoubleClick);
            this.spl = new SplitterPLM();
            this.spl.Width = this.txtInput.Width;
            this.spl.Top = (this.txtInput.Top + this.txtInput.Height) + 2;
            this.spl.Left = this.txtInput.Left;
            this.spl.Height = 3;
            this.spl.Visible = false;
            this.lbFilter = new ListBoxPLM();
            this.lbFilter.ItemHeight = 0x10;
            this.lbFilter.Top = (this.spl.Top + this.spl.Height) + 2;
            this.lbFilter.Left = this.txtInput.Left;
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new Size(this.MinWidth, this.MinHeight);
            this.lbFilter.Visible = false;
            this.lbFilter.KeyPress += new KeyPressEventHandler(this.lbFilter_KeyPress);
            this.lbFilter.DoubleClick += new EventHandler(this.lbFilter_DoubleClick);
            this.lbFilter.Leave += new EventHandler(this.lbFilter_Leave);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Initialize()
        {
            string str4 = "PLM_CUS_" + this.clsName;
            UCCusResource.GetData(out Stamp, out this.myds, this.clsName, this.attrs);
            if (this.myds != null)
            {
                this.metaAttr = (DEMetaAttribute) base.Tag;
                if (((this.metaAttr != null) && (this.metaAttr.LinkType == 1)) && (this.metaAttr.Combination != ""))
                {
                    DataColumn column = new DataColumn("PLM_Res_ComColumn") {
                        DataType = typeof(string)
                    };
                    this.myds.Tables[str4].Columns.Add(column);
                    foreach (DataRow row in this.myds.Tables[str4].Rows)
                    {
                        string combination = this.metaAttr.Combination;
                        string str3 = combination;
                        foreach (DEMetaAttribute attribute in this.attrs)
                        {
                            if (this.metaAttr.Combination.IndexOf("[" + attribute.Name + "]") > -1)
                            {
                                string str2 = "PLM_" + attribute.Name;
                                combination = combination.Replace("[" + attribute.Name + "]", Convert.ToString(row[str2]));
                                str3 = str3.Replace("[" + attribute.Name + "]", "");
                            }
                        }
                        if (combination == str3)
                        {
                            combination = "";
                        }
                        row["PLM_Res_ComColumn"] = combination;
                    }
                }
                else if ((this.metaAttr.LinkType == 0) && (this.metaAttr.DataType == 8))
                {
                    DataColumn column2 = new DataColumn("PLM_Res_ComColumn") {
                        DataType = typeof(string)
                    };
                    this.myds.Tables[str4].Columns.Add(column2);
                    foreach (DataRow row2 in this.myds.Tables[str4].Rows)
                    {
                        row2["PLM_Res_ComColumn"] = row2["PLM_ID"].ToString();
                    }
                    this.resOid = this.GetResourceOID(this.txtInput.Text, this.clsName);
                }
                this.myView = this.myds.Tables[str4].DefaultView;
                if (this.txtInput != null)
                {
                    this.flag = 0;
                    this.attrValue = this.txtInput.Text.Trim();
                }
            }
        }


        public bool IsInDataSet(string str)
        {
            string str2 = "PLM_CUS_" + this.clsName;
            bool flag = false;
            if (this.myds == null)
            {
                this.Initialize();
            }
            DataSet myds = this.myds;
            if ((myds == null) || (myds.Tables.Count <= 0))
            {
                return false;
            }
            if (myds.Tables[str2].Rows.Count <= 0)
            {
                return false;
            }
            foreach (DataColumn column in myds.Tables[str2].Columns)
            {
                if (column.ColumnName == "PLM_Res_ComColumn")
                {
                    foreach (DataRow row in myds.Tables[str2].Rows)
                    {
                        if (str == row["PLM_Res_ComColumn"].ToString())
                        {
                            return true;
                        }
                    }
                    return flag;
                }
            }
            return flag;
        }

        private bool IsInListBox(string str, ListBoxPLM lb)
        {
            foreach (object obj2 in lb.Items)
            {
                if (str == ((string) obj2))
                {
                    return true;
                }
            }
            return false;
        }

        private void lbFilter_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbFilter.SelectedIndex >= 0)
            {
                this.flag = 1;
                this.txtInput.Text = this.lbFilter.SelectedItem.ToString();
                this.txtInput.Focus();
                this.lbFilter.Visible = false;
                this.spl.Visible = false;
                base.Parent.Controls.Remove(this.lbFilter);
            }
        }

        private void lbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.lbFilter.SelectedIndex >= 0)
            {
                if (e.KeyChar == '\r')
                {
                    this.flag = 1;
                    this.txtInput.Text = this.lbFilter.SelectedItem.ToString();
                    this.txtInput.Focus();
                    this.lbFilter.Visible = false;
                    this.spl.Visible = false;
                    base.Parent.Controls.Remove(this.lbFilter);
                }
                if (e.KeyChar == '\x001b')
                {
                    this.txtInput.Focus();
                    this.lbFilter.Visible = false;
                    this.spl.Visible = false;
                    base.Parent.Controls.Remove(this.lbFilter);
                }
            }
        }

        private void lbFilter_Leave(object sender, EventArgs e)
        {
            if ((!this.txtInput.Focused && !this.spl.Focused) && (this.metaAttr != null))
            {
                if (this.txtInput.Text.Trim() == "")
                {
                    this.attrValue = "";
                }
                else
                {
                    if (!this.IsInDataSet(this.txtInput.Text.Trim()))
                    {
                        this.flag = 1;
                        this.txtInput.Text = this.attrValue;
                        MessageBoxPLM.Show("该值在资源数据中不存在，请重新输入！", "工程资源", MessageBoxButtons.OK);
                        this.txtInput.Focus();
                    }
                    else
                    {
                        this.attrValue = this.txtInput.Text.Trim();
                    }
                    if ((this.metaAttr.LinkType == 0) && (this.metaAttr.DataType == 8))
                    {
                        this.resOid = this.GetResourceOID(this.txtInput.Text.Trim(), this.clsName);
                    }
                    if ((this.lbFilter != null) && this.lbFilter.Visible)
                    {
                        this.lbFilter.Visible = false;
                        this.spl.Visible = false;
                        base.Parent.Controls.Remove(this.lbFilter);
                    }
                }
            }
        }

        private void SetListBoxLocation()
        {
            if (this.lbFilter.Visible)
            {
                if (((base.Parent != null) && ((base.Top - base.Parent.Top) >= 0x97)) && ((base.Parent.Height - base.Bottom) < 0x9d))
                {
                    this.lbFilter.Location = new Point(base.Left, base.Top - 0x97);
                }
                else
                {
                    this.lbFilter.Location = new Point(base.Left, (base.Top + base.Height) + 1);
                }
            }
        }

        private void SetSizeAndLocation()
        {
            this.MinWidth = base.Width;
            if (this.txtInput != null)
            {
                this.txtInput.Width = base.Width;
                this.txtInput.Location = new Point(0, 0);
                this.SetListBoxLocation();
                base.Height = this.txtInput.Height;
            }
        }

        private void txtInput_DoubleClick(object sender, EventArgs e)
        {
            if ((this.txtInput.Text.Trim() == "") && (this.myView != null))
            {
                this.myView.RowFilter = "";
                this.DisplayData(this.myView);
            }
        }

        private void txtInput_DragDrop(object sender, DragEventArgs e)
        {
            if (!this.txtInput.ReadOnly)
            {
                this.metaAttr = (DEMetaAttribute) base.Tag;
                CLCopyData data = new CLCopyData();
                data = (CLCopyData) e.Data.GetData(typeof(CLCopyData));
                if (data != null)
                {
                    DECopyData data2 = (DECopyData) data[0];
                    if (data2 != null)
                    {
                        if (data2.ClassName != this.clsName)
                        {
                            MessageBoxPLM.Show("资源类不匹配", "工程资源", MessageBoxButtons.OK);
                            this.txtInput.Focus();
                        }
                        else
                        {
                            DataRowView view = (DataRowView) data2.ItemList[0];
                            if (view != null)
                            {
                                if ((this.metaAttr != null) && (this.metaAttr.LinkType == 1))
                                {
                                    this.flag = 1;
                                    string combination = this.metaAttr.Combination;
                                    foreach (DEMetaAttribute attribute in this.attrs)
                                    {
                                        if (this.metaAttr.Combination.IndexOf("[" + attribute.Name + "]") > -1)
                                        {
                                            string str2 = "PLM_" + attribute.Name;
                                            combination = combination.Replace("[" + attribute.Name + "]", Convert.ToString(view[str2]));
                                        }
                                    }
                                    this.txtInput.Text = combination.Trim();
                                }
                                else
                                {
                                    this.flag = 1;
                                    this.txtInput.Text = view["PLM_ID"].ToString();
                                }
                                this.resOid = new Guid((byte[]) view["PLM_OID"]);
                                this.txtInput.Focus();
                            }
                        }
                    }
                }
            }
        }

        private void txtInput_DragEnter(object sender, DragEventArgs e)
        {
            if (!this.txtInput.ReadOnly)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            string str = "";
            str = this.txtInput.Text.Trim();
            if (e.KeyValue == 40)
            {
                this.FilterData(str);
                if (this.myView.Count > 0)
                {
                    this.DisplayData(this.myView);
                    this.lbFilter.Focus();
                }
                else
                {
                    this.spl.Visible = false;
                    this.lbFilter.Visible = false;
                }
            }
            else if (e.KeyValue == 0x26)
            {
                this.FilterData(str);
                if (this.myView.Count > 0)
                {
                    this.DisplayData(this.myView);
                    this.lbFilter.Focus();
                }
                else
                {
                    this.spl.Visible = false;
                    this.lbFilter.Visible = false;
                }
            }
            else if (e.KeyValue == 13)
            {
                if (this.lbFilter.Items.Count > 0)
                {
                    this.flag = 1;
                    this.txtInput.Text = this.lbFilter.Items[0].ToString();
                    this.txtInput.Focus();
                    this.lbFilter.Visible = false;
                    this.spl.Visible = false;
                    base.Parent.Controls.Remove(this.lbFilter);
                }
            }
            else if (e.KeyValue == 0x1b)
            {
                this.txtInput.Focus();
                this.lbFilter.Visible = false;
                this.spl.Visible = false;
                base.Parent.Controls.Remove(this.lbFilter);
            }
        }

        private void txtInput_Leave(object sender, EventArgs e)
        {
            if ((!this.lbFilter.Focused && !this.spl.Focused) && (this.metaAttr != null))
            {
                if (this.txtInput.Text.Trim() == "")
                {
                    this.attrValue = "";
                }
                else if (!this.IsInDataSet(this.txtInput.Text.Trim()))
                {
                    this.flag = 1;
                    this.txtInput.Text = this.attrValue;
                    MessageBoxPLM.Show("该值在资源数据中不存在，请重新输入！", "工程资源", MessageBoxButtons.OK);
                    this.txtInput.Focus();
                }
                else
                {
                    this.attrValue = this.txtInput.Text.Trim();
                }
                if ((this.metaAttr.LinkType == 0) && (this.metaAttr.DataType == 8))
                {
                    this.resOid = this.GetResourceOID(this.txtInput.Text.Trim(), this.clsName);
                }
                if ((this.lbFilter != null) && this.lbFilter.Visible)
                {
                    this.lbFilter.Visible = false;
                    this.spl.Visible = false;
                    base.Parent.Controls.Remove(this.lbFilter);
                }
            }
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if ((this.flag == 0) || (this.flag == 1))
            {
                if (this.flag == 1)
                {
                    this.flag = 0;
                }
                else
                {
                    this.FilterData(this.txtInput.Text.Trim());
                    this.DisplayData(this.myView);
                }
            }
        }

        private void UCCusTxtBox_Load(object sender, EventArgs e)
        {
            this.Initialize();
        }

        public bool ReadOnly
        {
            get{
               return this.txtInput.ReadOnly;
            }set
            {
                this.txtInput.ReadOnly = value;
            }
        }

        public Guid ResourceOid
        {
            get{ 
               return this.resOid;
            }set
            {
                this.resOid = value;
            }
        }

        public string ResValue
        {
            get {
                return this.txtInput.Text.Trim();
            }
            set
            {
                this.txtInput.Text = value;
            }
        }
    }
}

