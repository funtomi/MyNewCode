namespace Thyt.TiPLM.PLL.Common
{
    using System;

    public class ProgressBarCallback
    {
        private static IProgressCallback callback;

        private ProgressBarCallback()
        {
        }

        public static void AutoMove()
        {
            if (callback != null)
            {
                callback.AutoMove();
            }
        }

        public static void Begin(int minimum, int maximum)
        {
            if (callback != null)
            {
                callback.Begin(minimum, maximum);
            }
        }

        public static void BeginAutoMove(int possibleSecond)
        {
            Begin(0, possibleSecond);
            AutoMove();
        }

        public static void BeginAutoMoveFromTo(int minimum, int maximum)
        {
            Begin(minimum, maximum);
            AutoMove();
        }

        public static void End()
        {
            if (callback != null)
            {
                callback.End();
            }
        }

        public static void Increment(int val)
        {
            if (callback != null)
            {
                callback.Increment(val);
            }
        }

        public static void SetCallback(IProgressCallback cb)
        {
            callback = cb;
        }

        public static void SetText(string text)
        {
            if (callback != null)
            {
                callback.SetText(text);
            }
        }
    }
}

