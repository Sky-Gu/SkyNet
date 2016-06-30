using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Core.Downloader;

namespace SkyNet.CoreTests.Downloader
{
    [TestClass]
    public class HttpDownLoaderTests
    {
        [TestMethod]
        public void DownLoaderTest_Success()
        {
            var spiderMock = MockFactory.GetISpiderMock();
            var requstMock = MockFactory.GetRequstMock("http://www.baidu.com");
            var downLoader = new HttpDownLoader();
            var actual = downLoader.DownLoader(requstMock.Object, spiderMock.Object);

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual.StatusCode, 200);
            Assert.AreEqual(actual.Url, requstMock.Object.Uri.ToString());
            Assert.IsTrue(!string.IsNullOrWhiteSpace(actual.Title));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(actual.Content));
        }


        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void DownLoaderTest_AggregateException()
        {
            var spiderMock = MockFactory.GetISpiderMock();
            var requstMock = MockFactory.GetRequstMock("http://www.google.com");
            var downLoader = new HttpDownLoader();
            downLoader.DownLoader(requstMock.Object, spiderMock.Object);
        }
    }
}