using System;

namespace SkyNet.Core.Exceptoin
{
    /// <summary>
    ///     下载异常
    /// </summary>
    public class DownloadException : Exception
    {
        public DownloadException(string message) : base(message)
        {
        }
    }
}