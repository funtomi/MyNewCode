namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using System.Windows.Forms;

    public class PLMBaseTree : TreeView, IPLMTree
    {
        public virtual string GetLocationPath() {
            return
                UITreeHelper.Instance.GetLocationPath(this);
        }
        public virtual void SetLocationPath(string LocationPath)
        {
            UITreeHelper.Instance.SetLocationPath(this, LocationPath);
        }
    }
}

