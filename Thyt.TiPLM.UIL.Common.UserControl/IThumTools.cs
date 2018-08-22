namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using System.Windows.Forms;
    using Thyt.TiPLM.DEL.Product;

    public interface IThumTools
    {
        event DisplayStyleChangedHandler DisplayStyleChanged;

        event PreviewChangedHandler PreviewChanged;

        void ReReadSetting();
        void ShowThum(object objItem, DEPSOption option);

        string Location { get; set; }

        ThumbnailSetting ThumSetting { get; set; }

        ToolStrip ToolBar { get; }

        ToolStripCtrl ToolsItem { get; }
    }
}

