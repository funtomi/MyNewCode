namespace Thyt.TiPLM.UIL.Common.UserControl
{
    using System;
    using Thyt.TiPLM.DEL.Product;

    public class ThumShowEventArgs : EventArgs
    {
        public IBizItem bizItem;
        public DESecureFile file;

        public ThumShowEventArgs()
        {
        }

        public ThumShowEventArgs(IBizItem bizItem, DESecureFile file)
        {
            this.bizItem = bizItem;
            this.file = file;
        }
    }
}

