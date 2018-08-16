namespace Thyt.TiPLM.PLL.Common
{
    using System;
    using System.Threading;

    public sealed class RequestServiceCounter
    {
        private const int INTERVAL_SECOND = 300;
        public static DateTime LastServiceRequestTime = DateTime.Now;
        private static TimeSpan m_span = new TimeSpan(0, 0, 0);
        private static Timer timer = new Timer(new TimerCallback(RequestServiceCounter.threadTimerCallback), null, 0x493e0, 0x493e0);

        public static void ResetSpan()
        {
            lock (typeof(int))
            {
                m_span = new TimeSpan(0, 0, 0);
            }
        }

        private static void threadTimerCallback(object obj)
        {
            lock (typeof(int))
            {
                m_span += new TimeSpan(0, 300, 0);
            }
        }

        public static TimeSpan RequestServiceSpan {
            get {
                return m_span;
            }
        }
    }
}

