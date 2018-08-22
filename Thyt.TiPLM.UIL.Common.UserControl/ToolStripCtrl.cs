using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
namespace Thyt.TiPLM.UIL.Common.UserControl {
    public partial class ToolStripCtrl : ToolStripControlHost {
        public List<ToolStripItem> Items;

        public ToolStripCtrl(ToolStripItem[] tsp_Items)
            : base(CreateControlInstance(tsp_Items)) {
            this.Items = new List<ToolStripItem>();
            this.InitializeComponent();
            this.Items.AddRange(tsp_Items);
        }

        private static Control CreateControlInstance(ToolStripItem[] tsp_Items) {
            ToolStrip strip = new ToolStrip();
            strip.Items.AddRange(tsp_Items.ToArray<ToolStripItem>());
            return strip;
        }


    }
}

