namespace Thyt.TiPLM.PLL.Common
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using Thyt.TiPLM.Common;

    public class PLSplashImage
    {
        public static Image GetSplashImage(string resName)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + resName + ".splash");
            }
            catch (Exception exception)
            {
                throw new PLMException("错误的图片名或图片已被删除", exception);
            }
            return image;
        }
    }
}

