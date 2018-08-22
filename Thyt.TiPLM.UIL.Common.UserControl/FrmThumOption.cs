    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class FrmThumOption : FormPLM
    {
       
        public PreviewStyle Enum_PreviewStyle;
       

        public FrmThumOption()
        {
            this.InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            if (this.radVisualization.Checked)
            {
                this.Enum_PreviewStyle = PreviewStyle.Visualization;
            }
            if (this.radThumbnail2Visualization.Checked)
            {
                this.Enum_PreviewStyle = PreviewStyle.Thumbnail2Visualization;
            }
            if (this.radThumbnail.Checked)
            {
                this.Enum_PreviewStyle = PreviewStyle.Thumbnail;
            }
            base.Close();
        }
 
    }
}

