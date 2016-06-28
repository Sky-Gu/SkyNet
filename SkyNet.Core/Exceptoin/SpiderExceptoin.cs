using System;

namespace SkyNet.Core.Exceptoin
{
    /// <summary>
    ///     爬虫异常
    /// </summary>
    public class SpiderExceptoin : Exception
    {
        public SpiderExceptoin(string msg) : base(msg)
        {
        }
    }
}