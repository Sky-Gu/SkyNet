using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Core.Crawler;
using SkyNet.Core.Enum;
using SkyNet.Core.Model;
using SkyNet.Core.PageProcessor;

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
    }
}