using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CsQuery;
using SkyNet.Core.Downloader;
using SkyNet.Core.Enum;
using SkyNet.Core.Exceptoin;
using SkyNet.Core.Model;
using SkyNet.Core.PageProcessor;
using SkyNet.Core.Pipeline;
using SkyNet.Core.Scheduler;
using SkyNet.Tools;

namespace SkyNet.Core.Crawler
{
    public sealed class Spider : ISpider
    {
        private readonly Random _random = new Random();

        public Spider(Site site, IPageProcessor pageProcessor)
        {
            Status = SpiderStatusEnum.Init;
            PageProcessor = pageProcessor;
            PageProcessor.Site = Site = site;
            Scheduler = new MemoryScheduler();
            DownLoader = new HttpDownLoader();
        }

        public Spider(Site site, IPageProcessor pageProcessor, IScheduler scheduler) : this(site, pageProcessor)
        {
            Scheduler = scheduler;
        }

        public Spider(Site site, IPageProcessor pageProcessor, IScheduler scheduler, IDownLoader downLoader)
            : this(site, pageProcessor, scheduler)
        {
            DownLoader = downLoader;
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
        public List<IPipeline> Pipelines { get; } = new List<IPipeline>();

        /// <summary>
        ///     爬虫状态
        /// </summary>
        public SpiderStatusEnum Status { get; private set; }

        /// <summary>
        ///     爬虫线程数，通过Site进行设置
        /// </summary>
        public int ThreadCount => Site.ThreadCount;

        /// <summary>
        ///     爬虫监听
        /// </summary>
        public List<ISpiderListening> SpiderListening { get; set; } = new List<ISpiderListening>();


        /// <summary>
        ///     队列管理器
        /// </summary>
        public IScheduler Scheduler { get; }

        /// <summary>
        ///     爬虫设置
        /// </summary>
        public Site Site { get; }

        #region Run

        /// <summary>
        ///     运行爬虫
        /// </summary>
        public void Run()
        {
            InitSpider();
            Status = SpiderStatusEnum.Running;

            var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = ThreadCount };

            Parallel.For(0, ThreadCount, parallelOptions, i =>
            {
                while (Status == SpiderStatusEnum.Running)
                {
                    Request requset = null;
                    try
                    {
                        requset = Scheduler.GetRequest();
                        if (requset == null)
                            break;

                        ProcessRequest(requset, DownLoader);

                        Thread.Sleep(_random.Next(Site.MinSleepTime, Site.MaxSleepTime));
                    }
                    catch (Exception e)
                    {
                        SpiderListening.ForEach(item => item.ErrorHandler(requset, e));
                    }
                }
            });
        }

        #endregion

        #region Stop

        /// <summary>
        ///     停止爬虫
        /// </summary>
        public void Stop()
        {
            Status = SpiderStatusEnum.Stopped;
        }

        #endregion

        #region AddSeedUrl

        /// <summary>
        ///     增加种子url
        /// </summary>
        /// <param name="url">种子URL</param>
        public Spider AddSeedUrl(string url)
        {
            Scheduler.AddWaitRequest(new Request(url));
            return this;
        }

        #endregion

        #region ProcessRequest

        /// <summary>
        ///     页面下载
        /// </summary>
        private void ProcessRequest(Request request, IDownLoader downLoader)
        {
            var page = downLoader.DownLoader(request, this);

            PageProcessor.Process(page);
            Scheduler.AddFinishRequest(request);
            SpiderListening.ForEach(item => item.AfterSuccess(request));

            if (page.IsSave)
                Pipelines.ForEach(item => item.Process(page.PageResult));

            GetPageUrl(page).ForEach(item => Scheduler.AddWaitRequest(new Request(item)));
        }

        #endregion

        #region InitSpider

        /// <summary>
        ///     初始化爬虫
        /// </summary>
        /// <exception cref="ArgumentNullException">Scheduler is not null</exception>
        private void InitSpider()
        {
            CheckRunning();

            if (Scheduler == null)
                throw new ArgumentNullException($"Scheduler is not null");

            if (DownLoader == null)
                throw new ArgumentNullException($"DownLoader is not null");

            if (PageProcessor == null)
                throw new ArgumentNullException($"PageProcessormo is not null");

            if (Site.MinSleepTime < 500)
                throw new SpiderExceptoin("Sleep time should be large than 500");

            if (ThreadCount <= 0)
                throw new ArgumentNullException($"ThreadCount should be more than one!");

            //http并发请求限制
            ServicePointManager.DefaultConnectionLimit = ThreadCount > 1024 ? ThreadCount : 1024;


            SpiderListening.ForEach(item => item.AfterInit(this));
        }

        #endregion

        #region CheckRunning

        /// <summary>
        ///     验证是否正在运行
        /// </summary>
        private void CheckRunning()
        {
            if (Status == SpiderStatusEnum.Running)
            {
                throw new SpiderExceptoin("Spider is already running!");
            }
        }

        #endregion

        #region GetPageUrl

        /// <summary>
        ///     获取页面中的Url
        /// </summary>
        /// <param name="page">爬取到的页面信息</param>
        /// <returns>link 连接列表</returns>
        public List<string> GetPageUrl(Page page)
        {
            CQ dom = page.Content;
            var resultList = new List<string>();
            dom["a"].Each(item => resultList.Add(item.GetAttribute("href")));

            var domain = $"{page.Uri.Scheme}://{page.Uri.Host}";

            var urlList = resultList
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Distinct()
                .Select(item =>
                {
                    if (UrlHelper.IsUrl(item))
                        return item;

                    var canonicalizeUrl = UrlHelper.CanonicalizeUrl(item, domain);

                    return UrlHelper.IsUrl(canonicalizeUrl) ? canonicalizeUrl : "";
                }).Where(item => !string.IsNullOrWhiteSpace(item))
                .Distinct()
                .ToList();

            return urlList;
        }

        #endregion

        #region AddListening

        /// <summary>
        ///     添加监听事件
        /// </summary>
        /// <param name="listening">监听事件</param>
        public Spider AddListening(ISpiderListening listening)
        {
            SpiderListening.Add(listening);
            return this;
        }

        #endregion

        #region AddPiepline

        /// <summary>
        ///     添加 Piepline
        /// </summary>
        /// <param name="piepline">结果处理对象</param>
        public Spider AddPiepline(IPipeline piepline)
        {
            return AddPiepline(new List<IPipeline> { piepline });
        }


        /// <summary>
        ///     添加 Piepline
        /// </summary>
        /// <param name="pieplines">结果处理对象集合</param>
        public Spider AddPiepline(List<IPipeline> pieplines)
        {
            Pipelines.AddRange(pieplines);
            return this;
        }

        #endregion
    }
}