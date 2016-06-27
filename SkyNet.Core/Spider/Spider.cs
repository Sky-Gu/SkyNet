using System.Collections.Generic;
using SkyNet.Core.Downloader;
using SkyNet.Core.Model;
using SkyNet.Core.PageProcessor;
using SkyNet.Core.Pipeline;
using SkyNet.Core.Scheduler;

namespace SkyNet.Core.Spider
{
    public class Spider : ISpider
    {
        public Spider(Site site)
        {
            Site = site;
        }

        /// <summary>
        ///     下载器
        /// </summary>
        public IDownLoader DownLoader { get; }

        /// <summary>
        ///     页面处理流程
        /// </summary>
        public IPageProcessor PageProcessor { get; }

        /// <summary>
        ///     页面处理器集合
        /// </summary>
        public List<IPipeline> Pipelines { get; set; }

        /// <summary>
        ///     队列管理器
        /// </summary>
        public IScheduler Scheduler { get; }

        public Site Site { get; }

        /// <summary>
        ///     页面下载
        /// </summary>
        public Page Run(Request request)
        {
            var requestUrl = Scheduler.GetRequest();
            return new Page(new Request());
        }

        /// <summary>
        ///     页面分析
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public PageReuslt PageProcesser(Page page)
        {
            return new PageReuslt();
        }
    }
}