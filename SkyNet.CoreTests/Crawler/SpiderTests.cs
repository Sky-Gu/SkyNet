using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Core.Crawler;
using SkyNet.Core.Enum;
using SkyNet.Core.Model;
using SkyNet.Core.PageProcessor;
using SkyNet.Core.Pipeline;

namespace SkyNet.CoreTests.Crawler
{
    [TestClass]
    public class SpiderTests
    {
        [TestMethod]
        public void GetPageUrlTest_Success()
        {
            var site = new Site();
            var spider = new Spider(site, new DefaultPageProcessor());
            var page = new Page(new Request())
            {
                Url = "http://www.qq.com",
                Content =
                    "<div><a href='www.qq.com'/><span><a href='www.qq.com'/></span><span><a href='www.1buo.com'/></span></div>"
            };
            var urlList = spider.GetPageUrl(page);

            Assert.IsNotNull(urlList);
            Assert.IsTrue(urlList.Count == 2);
        }

        [TestMethod]
        public void SpiderTest_Success()
        {
            var site = new Site();
            var processor = new DefaultPageProcessor();
            var spider = new Spider(site, processor);

            Assert.IsNotNull(spider);
            Assert.IsNotNull(spider.Scheduler);
            Assert.IsNotNull(spider.DownLoader);
            Assert.IsNotNull(spider.Site);
            Assert.IsNotNull(spider.PageProcessor);
            Assert.IsTrue(spider.Status == SpiderStatusEnum.Init);
            Assert.IsTrue(spider.Pipelines != null);
            Assert.IsTrue(spider.SpiderListening != null);
        }

        [TestMethod]
        public void RunTest_Success()
        {
            var site = new Site { ThreadCount = 30 };
            var pageProcessor = new DefaultPageProcessor();
            var htmlPiepline = new HtmlFilePiepline();
            var filePiepline = new FilePipeline();
            var listening = new ConsoleSpiderListening();
            var spider = new Spider(site, pageProcessor);
            spider.AddSeedUrl("http://sh.lianjia.com/")
                .AddPiepline(htmlPiepline)
                .AddPiepline(filePiepline)
                .AddListening(listening)
                .Run();
        }
    }
}