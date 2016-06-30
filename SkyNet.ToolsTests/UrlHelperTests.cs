using System;
using System.Collections.Generic;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Tools;

namespace SkyNet.ToolsTests
{
    [TestClass]
    public class UrlHelperTests
    {
        [TestMethod]
        public void RemoveProtocolTest_Success()
        {
            var actual = UrlHelper.RemoveProtocol("http://www.qq.com");

            Assert.IsTrue(actual.IndexOf("http://", StringComparison.Ordinal) < 0);
        }

        [TestMethod]
        public void GetDomainTest_Success()
        {
            const string domain = "www.qq.com";
            var urlList = new[] {"http://www.qq.com/index", "www.qq.com/index", "http://www.qq.com/", "www.qq.com"};
            urlList.ForEach(item =>
            {
                var actual = UrlHelper.GetDomain(item);
                Assert.AreEqual(domain, actual);
            });
        }

        [TestMethod]
        public void CanonicalizeUrlTest()
        {
            const string url = "/sh/zufang";
            const string domain = "http://www.1buo.com";
            const string fullUrl = "http://www.1buo.com/sh/zufang";

            var actual = UrlHelper.CanonicalizeUrl(url, domain);

            Assert.AreEqual(actual, fullUrl);
        }

        [TestMethod]
        public void IsUrlTest_Susscee()
        {
            var urlCase = new List<string> {"http://www.1buo.com", "www.1buo.cn"};

            urlCase.ForEach(item => Assert.IsTrue(UrlHelper.IsUrl(item)));
        }

        [TestMethod]
        public void IsUrlTest_NotUrl()
        {
            var urlCase = new List<string>
            {
                "javascript:;",
                "javascript:",
                "javascript:alert();",
                "#",
                "javascript:void(0);",
                "/sh/zufang"
            };

            urlCase.ForEach(item => { Assert.IsFalse(UrlHelper.IsUrl(item)); });
        }
    }
}