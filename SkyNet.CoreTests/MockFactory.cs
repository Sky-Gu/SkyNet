using System;
using System.Collections.Generic;
using Moq;
using SkyNet.Core.Crawler;
using SkyNet.Core.Downloader;
using SkyNet.Core.Model;

namespace SkyNet.CoreTests
{
    public static class MockFactory
    {
        #region GetSiteMock

        /// <summary>
        ///     获取Site 对象的Mock
        /// </summary>
        public static Mock<Site> GetSiteMock()
        {
            var siteMock = new Mock<Site>();
            siteMock.SetupProperty(m => m.RetryCount, 1);
            siteMock.SetupProperty(m => m.ThreadCount, 1);
            siteMock.SetupProperty(m => m.MaxSleepTime, 500);
            siteMock.SetupProperty(m => m.MinSleepTime, 500);
            siteMock.SetupProperty(m => m.Handers, new Dictionary<string, string>());

            return siteMock;
        }

        #endregion

        #region GetISpiderMock

        /// <summary>
        ///     获取 ISpider  的Mock
        /// </summary>
        public static Mock<ISpider> GetISpiderMock()
        {
            var mock = new Mock<ISpider>();
            mock.Setup(m => m.Site).Returns(new Site());

            return mock;
        }

        #endregion

        #region GetPageMock

        /// <summary>
        ///     获取 Page 的Mock
        /// </summary>
        public static Mock<Page> GetPageMock(Mock<Request> requestMock)
        {
            var mock = new Mock<Page>(requestMock.Object);
            mock.SetupAllProperties();
            return mock;
        }

        #endregion

        #region GetIDownLoaderMock

        /// <summary>
        ///     获取 IDownLoader 的Mock
        /// </summary>
        public static Mock<IDownLoader> GetIDownLoaderMock(Mock<Request> requestMock, Mock<ISpider> spiderMock,
            Mock<Page> pageMock)
        {
            var mock = new Mock<IDownLoader>();
            mock
                .Setup(m => m.DownLoader(requestMock.Object, spiderMock.Object))
                .Returns(pageMock.Object);

            return mock;
        }

        #endregion

        #region GetPageResultMock

        /// <summary>
        ///     获取 PageResult 的Mock
        /// </summary>
        public static Mock<PageReuslt> GetPageResultMock()
        {
            var mock = new Mock<PageReuslt>();
            mock.SetupProperty(m => m.IsSave, true);
            mock.SetupProperty(m => m.Request, GetRequstMock().Object);


            return mock;
        }

        #endregion

        #region GetRequstMock

        /// <summary>
        ///     获取 Request 的Mock
        /// </summary>
        public static Mock<Request> GetRequstMock()
        {
            var mock = new Mock<Request>();
            mock.SetupProperty(m => m.Depth, 1);
            mock.SetupProperty(m => m.PostBody, "");
            mock.SetupProperty(m => m.Priority, 1);
            mock.SetupProperty(m => m.Method, "GET");
            mock.SetupProperty(m => m.Uri, new Uri("htttp://www.qq.com"));
            mock.SetupProperty(m => m.Addition, new Dictionary<string, object>());

            return mock;
        }


        /// <summary>
        ///     获取 Request 的Mock
        /// </summary>
        /// <param name="url">url地址</param>
        public static Mock<Request> GetRequstMock(string url)
        {
            var mock = new Mock<Request>();
            mock.SetupProperty(m => m.Depth, 1);
            mock.SetupProperty(m => m.PostBody, "");
            mock.SetupProperty(m => m.Priority, 1);
            mock.SetupProperty(m => m.Method, "GET");
            mock.SetupProperty(m => m.Uri, new Uri(url));
            mock.SetupProperty(m => m.Addition, new Dictionary<string, object>());

            return mock;
        }

        #endregion
    }
}