﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using SkyNet.Core.Downloader;
using SkyNet.Core.Enum;
using SkyNet.Core.Exceptoin;
using SkyNet.Core.Model;
using SkyNet.Core.PageProcessor;
using SkyNet.Core.Pipeline;
using SkyNet.Core.Scheduler;

namespace SkyNet.Core.Spider
{
    public class Spider : ISpider
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
        public SpiderStatusEnum Status { get; set; }

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
        protected void ProcessRequest(Request request, IDownLoader downLoader)
        {
            var page = downLoader.DownLoader(request, this);
            PageProcessor.Process(page);

            SpiderListening.ForEach(item => item.SuccessAfter(request));

            if (page.IsSave)
                Pipelines.ForEach(item => item.Process(page.PageResult));

            //TODO 采集新的url 入队列
        }

        #endregion

        #region InitSpider

        /// <summary>
        ///     初始化爬虫
        /// </summary>
        /// <exception cref="ArgumentNullException">Scheduler is not null</exception>
        protected void InitSpider()
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


            SpiderListening.ForEach(item => item.InitAfter(this));
        }

        #endregion

        #region CheckRunning

        /// <summary>
        ///     验证是否正在运行
        /// </summary>
        protected void CheckRunning()
        {
            if (Status == SpiderStatusEnum.Running)
            {
                throw new SpiderExceptoin("Spider is already running!");
            }
        }

        #endregion
    }
}