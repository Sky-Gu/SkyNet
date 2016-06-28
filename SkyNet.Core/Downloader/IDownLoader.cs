using SkyNet.Core.Crawler;
using SkyNet.Core.Model;

namespace SkyNet.Core.Downloader
{
    /// <summary>
    ///     页面下载模块
    /// </summary>
    public interface IDownLoader
    {　
　
        /// <summary>
        ///     下载页面
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="spider">对应的爬虫</param>
        /// <returns>页面Page</returns>
        Page DownLoader(Request request, ISpider spider);

    }
}