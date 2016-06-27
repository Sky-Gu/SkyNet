using System;
using SkyNet.Core.Model;
using SkyNet.Core.Scheduler;

namespace SkyNet.Core.Spider
{
    /// <summary>
    ///     爬虫约束
    /// </summary>
    public interface ISpider
    {
        /// <summary>
        ///     队列管理器
        /// </summary>
        IScheduler Scheduler { get; }

        /// <summary>
        ///     爬虫设置
        /// </summary>
        Site Site { get; }

        /// <summary>
        ///     下载页面
        /// </summary>
        /// <param name="request">页面请求</param>
        /// <returns>页面信息</returns>
        Page Run(Request request);

        /// <summary>
        ///     页面处理
        /// </summary>
        /// <param name="page">页面信息</param>
        /// <returns>页面抽取结果</returns>
        PageReuslt PageProcesser(Page page);
    }
}