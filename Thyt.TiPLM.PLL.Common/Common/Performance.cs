namespace Thyt.TiPLM.PLL.Common
{
    using System;

    public class Performance
    {
        public PLMCacheObject CacheObjectEvent;
        public static Performance Instance = new Performance();

        private Performance()
        {
        }
    }
}

