namespace Thyt.TiPLM.UIL.Resource.Common.OuterResource
{
    using System;

    public class OuterResourceCreatedEventArgs : EventArgs
    {
        public object newResource;

        public OuterResourceCreatedEventArgs(object newResource)
        {
            this.newResource = newResource;
        }
    }
}

