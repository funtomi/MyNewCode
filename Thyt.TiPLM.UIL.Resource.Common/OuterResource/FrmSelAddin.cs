    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.Common.Interface.Resource;
    using Thyt.TiPLM.DEL.Addin;
    using Thyt.TiPLM.UIL.Addin;
    using Thyt.TiPLM.UIL.Common;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common.OuterResource {
    public partial class FrmSelAddin : FormPLM
    {
       
        private int OperType;
        private object parent;

        public FrmSelAddin()
        {
            this.InitializeComponent();
        }

        public FrmSelAddin(object de, int OperType)
        {
            this.InitializeComponent();
            this.parent = de;
            this.OperType = OperType;
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.GetOutResAddin();
            base.Close();
        }
 

        private void FrmSelAddin_Load(object sender, EventArgs e)
        {
            this.lV_Addin.SmallImageList = ClientData.MyImageList.imageList;
            this.lV_Addin.Columns.AddRange(UIAddinHelper.Instance.CompactAddinListColumns);
            DEAddinReg[] addinsByModule = AddinFramework.Instance.GetAddinsByModule(6);
            UIAddinHelper.Instance.FillCompactAddinList(this.lV_Addin, addinsByModule, View.Details);
        }

        private void GetOutResAddin()
        {
            new ArrayList();
            Cursor.Current = Cursors.Default;
            DEAddinReg tag = null;
            if (this.lV_Addin.SelectedItems.Count > 0)
            {
                tag = this.lV_Addin.SelectedItems[0].Tag as DEAddinReg;
            }
            if ((tag != null) && ((tag.Status & 2) > 0))
            {
                IOutResAddIn addinEntryObject = AddinFramework.Instance.GetAddinEntryObject(tag.Oid) as IOutResAddIn;
                if (addinEntryObject != null)
                {
                    if (!addinEntryObject.Execute(this.parent, this.OperType))
                    {
                        MessageBoxPLM.Show("外部资源插件调用失败！");
                    }
                }
                else
                {
                    MessageBoxPLM.Show("插件调用失败!");
                }
            }
        }

    }
}

