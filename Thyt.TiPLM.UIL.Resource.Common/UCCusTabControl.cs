    using System;
    using System.Collections;
    using System.Windows.Forms;
    using Thyt.TiPLM.UIL.Common.UserControl;
    using Thyt.TiPLM.UIL.Controls;
namespace Thyt.TiPLM.UIL.Resource.Common {
    public class UCCusTabControl : PanelPLM
    {
        private ArrayList theClsList = new ArrayList();

        public UCCusTabControl()
        {
            this.Initialize();
        }

        private void AddIcon()
        {
        }

        private void Initialize()
        {
            this.AddIcon();
            UCResTree tree = new UCResTree {
                Dock = DockStyle.Fill
            };
            base.Controls.Add(tree);
        }

        private void tbcRes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

