namespace Thyt.TiPLM.CLT.TiModeler
{
    using System;
    using Thyt.TiPLM.UIL.Admin.NewResponsibility;

    public class ResponsibilityEventInit
    {
        public static void InitEvent()
        {
            FrmMain main = new FrmMain();
            ResponsibilityEvent.instance.removeWindows = (RemoveWindows) Delegate.Combine(ResponsibilityEvent.instance.removeWindows, new RemoveWindows(main.RemoveWindows));
        }
    }
}

