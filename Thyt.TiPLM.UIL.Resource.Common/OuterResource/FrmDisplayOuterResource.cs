    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common;
    using Thyt.TiPLM.DEL.Admin.DataModel;
    using Thyt.TiPLM.DEL.Resource;
    using Thyt.TiPLM.PLL.Admin.DataModel;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.OuterResource
{
    public partial class FrmDisplayOuterResource : FormPLM
    {
        private string dataFilter;
        private DataSet ds;
        private bool isOnlineOutRes;
        private DEOuterTable myDEOuterTB;

        public FrmDisplayOuterResource(DEOuterTable de, DataSet ds)
        {
            this.dataFilter = "";
            this.InitializeComponent();
            this.myDEOuterTB = de;
            this.ds = ds;
            this.gbData.Text = "外部资源数据: " + de.OuterName;
        }

        public FrmDisplayOuterResource(DEOuterTable de, DataSet ds, string dataFilter, bool isOnlineOutRes)
        {
            this.dataFilter = "";
            this.InitializeComponent();
            this.myDEOuterTB = de;
            this.ds = ds;
            this.gbData.Text = "外部资源数据: " + de.OuterName;
            this.dataFilter = dataFilter;
            this.isOnlineOutRes = isOnlineOutRes;
        }

        private void ConvertOuterDSHeader(DataSet ds_ret, Guid g_clsoid)
        {
            ArrayList attrList = new ArrayList();
            ArrayList outList = new ArrayList();
            DEResFolder defolder = new DEResFolder {
                ClassOid = g_clsoid
            };
            DEMetaClass class2 = ModelContext.MetaModel.GetClass(g_clsoid);
            attrList = this.GetAttributes(class2.Name);
            outList = ResFunc.GetOuterAttr(defolder);
            this.ReplaceAttrName(ds_ret, outList, attrList);
        }

        private void CreateStyles(DataGrid dg, DataSet mySet)
        {
            DataGridTableStyle table = new DataGridTableStyle {
                MappingName = mySet.Tables[0].ToString(),
                AlternatingBackColor = Color.LightGray
            };
            foreach (DataColumn column in mySet.Tables[0].Columns)
            {
                DataGridColumnStyle style2 = new DataGridTextBoxColumn {
                    HeaderText = column.ColumnName,
                    MappingName = column.ToString(),
                    Width = 200,
                    NullText = ""
                };
                table.GridColumnStyles.Add(style2);
            }
            dg.TableStyles.Clear();
            dg.TableStyles.Add(table);
        }
 

        private void FrmDisplayOuterResource_Load(object sender, EventArgs e)
        {
            if (this.ds.Tables.Count <= 0)
            {
                this.dgdData.DataSource = null;
            }
            else
            {
                DataView defaultView = this.ds.Tables[0].DefaultView;
                this.dgdData.DataSource = defaultView;
                if ((this.isOnlineOutRes && (this.dataFilter != null)) && (this.dataFilter.Trim() != ""))
                {
                    string str = this.dataFilter.Trim();
                    if (((str != null) && (str.Length > 0)) && str.EndsWith("]"))
                    {
                        int index = str.IndexOf("[");
                        str = str.Substring(0, index - 1).Replace("%", "*");
                    }
                    defaultView.RowFilter = str;
                }
                this.ConvertOuterDSHeader(this.ds, this.myDEOuterTB.TableOid);
                this.CreateStyles(this.dgdData, this.ds);
            }
        }

        private ArrayList GetAttributes(string classname) {
            return ModelContext.MetaModel.GetAttributes(classname);
        }
       
        private void ReplaceAttrLabel(DataSet ds, ArrayList attrList)
        {
            try
            {
                new ArrayList();
                new ArrayList();
                if (ds.Tables.Count >= 0)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        string columnName = ds.Tables[0].Columns[i].ColumnName;
                        foreach (DEMetaAttribute attribute in attrList)
                        {
                            if (columnName == attribute.Name)
                            {
                                ds.Tables[0].Columns[i].ColumnName = attribute.Label;
                                break;
                            }
                        }
                    }
                }
            }
            catch (PLMException exception)
            {
                throw exception;
            }
            catch (Exception exception2)
            {
                throw new PLMException("无法将ds中的列名转换为TiPLM中对应的属性名。", exception2);
            }
        }

        private void ReplaceAttrName(DataSet ds, ArrayList outList, ArrayList attrList)
        {
            try
            {
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                string str = "PLM_";
                if (ds.Tables.Count >= 0)
                {
                    foreach (DEOuterAttribute attribute in outList)
                    {
                        foreach (DEMetaAttribute attribute2 in attrList)
                        {
                            if (attribute.FieldOid == attribute2.Oid)
                            {
                                list.Add(str + attribute2.Name);
                                list2.Add(attribute2.Label);
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        string columnName = ds.Tables[0].Columns[i].ColumnName;
                        for (int j = 0; j < list2.Count; j++)
                        {
                            if (columnName == list[j].ToString())
                            {
                                ds.Tables[0].Columns[i].ColumnName = list2[j].ToString();
                                break;
                            }
                        }
                    }
                }
            }
            catch (PLMException exception)
            {
                throw exception;
            }
            catch (Exception exception2)
            {
                throw new PLMException("无法将ds中的列名转换为TiPLM中对应的属性名。", exception2);
            }
        }
    }
}

