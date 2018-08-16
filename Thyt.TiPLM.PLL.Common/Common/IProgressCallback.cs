namespace Thyt.TiPLM.PLL.Common
{
    using System;

    public interface IProgressCallback
    {
        void AutoMove();
        void Begin(int minimum, int maximum);
        void End();
        void Increment(int val);
        void SetText(string text);
        void ShowWindow();

        bool IsAborting { get; }
    }
}

