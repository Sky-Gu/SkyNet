using System;
using SkyNet.Core.Model;

namespace SkyNet.Core.Crawler
{
    /// <summary>
    ///     爬虫监听事件
    /// </summary>
    public interface ISpiderListening
    {
        /// <summary>
        ///     初始化完成后
        /// </summary>
        void AfterInit(ISpider spider);

        /// <summary>
        ///     爬取成功之后
        /// </summary>
        void AfterSuccess(Request request);

        /// <summary>
        ///     错误处理
        /// </summary>
        void ErrorHandler(Request request, Exception e);
    }
}