namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;

    [Serializable]
    public class ThumbnailSetting
    {
        public Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle DisplayStyle = Thyt.TiPLM.UIL.Common.UserControl.DisplayStyle.Detail;
        public bool IsPreview;
        public bool IsShowToolBar = true;
        public int PreviwWidth = 100;
    }
}

