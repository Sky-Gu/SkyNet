using System;
using System.Net;
using System.Net.Http;
using SkyNet.Core.Crawler;
using SkyNet.Core.Model;

namespace SkyNet.Core.Downloader
{
    /// <summary>
    ///     http 页面请求
    /// </summary>
    public class HttpDownLoader : IDownLoader
    {
        private readonly HttpClient _httpclient;

        public HttpDownLoader()
        {
            var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            _httpclient = new HttpClient(handler);
        }

        /// <summary>
        ///     下载页面
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="spider">爬虫实例</param>
        /// <returns>页面信息</returns>
        public Page DownLoader(Request request, ISpider spider)
        {
            if (spider.Site == null)
                throw new NullReferenceException("Site is not null");

            var requestMessage = GetRequestMessage(request, spider.Site);

            var response = _httpclient.SendAsync(requestMessage).Result;

            var result = GetResultPage(request, response);

            return result;
        }

        #region GetResultPage

        /// <summary>
        ///     获取Page
        /// </summary>
        /// <param name="request">页面请求时的Request</param>
        /// <param name="response">站点响应的Response</param>
        /// <returns></returns>
        private Page GetResultPage(Request request, HttpResponseMessage response)
        {
            var result = new Page(request)
            {
                StatusCode = (int)response.StatusCode,
                Content = response.Content.ReadAsStringAsync().Result,
                Url = request.Uri.ToString()
            };
            foreach (var header in response.Headers)
            {
                result.Request.Addition.Add(header.Key, header.Value);
            }
            return result;
        }

        #endregion

        #region GetRequestMessage

        /// <summary>
        ///     获取请求用的HttpRequestMessage
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <param name="site">请求的Site</param>
        /// <returns></returns>
        private HttpRequestMessage GetRequestMessage(Request request, Site site)
        {
            //TODO 需要适应不同的请求方式
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri.ToString());

            foreach (var item in site.Handers)
                requestMessage.Headers.Add(item.Key, item.Value);

            return requestMessage;
        }

        #endregion
    }
}