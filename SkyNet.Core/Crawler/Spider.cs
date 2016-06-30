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

namespace SkyNet.Core.Crawler
{
    public sealed class Spider : ISpider
    {
        private readonly Random _random = new Random();

        public Spider(Site site, IPageProcessor pageProcessor)
        {
            Site = site;
            Status = SpiderStatusEnum.Init;
            PageProcessor = pageProcessor;
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
        public List<IPipeline> Pipelines { get; set; } = new List<IPipeline>();

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
                        {
                            Status = SpiderStatusEnum.Finished;
                            break;
                        }

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

        #region ProcessRequest

        /// <summary>
        ///     页面下载
        /// </summary>
        private void ProcessRequest(Request request, IDownLoader downLoader)
        {
            var page = downLoader.DownLoader(request, this);
            PageProcessor.Process(page);

            SpiderListening.ForEach(item => item.AfterSuccess(request));

            if (page.IsSave)
                Pipelines.ForEach(item => item.Process(page.PageResult));

            GetPageUrl(page).ForEach(item => Scheduler.AddRequest(new Request(item)));
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

            return resultList.Distinct().ToList();
        }

        #endregion
    }
}
