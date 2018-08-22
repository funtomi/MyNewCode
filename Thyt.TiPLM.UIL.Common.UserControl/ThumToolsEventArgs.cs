namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;

    public class ThumToolsEventArgs : EventArgs
    {
        public Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle DisplayStyle;
        public bool IsPreview;

        public ThumToolsEventArgs()
        {
            this.DisplayStyle = Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle.Detail;
        }

        public ThumToolsEventArgs(bool IsPreview, Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle DisplayStyle)
        {
            this.DisplayStyle = Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle.Detail;
            this.IsPreview = IsPreview;
            this.DisplayStyle = DisplayStyle;
        }
    }
}

