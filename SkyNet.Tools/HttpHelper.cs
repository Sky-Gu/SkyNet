using System.Net.Http;

namespace SkyNet.Tools
{
    /// <summary>
    ///     Http请求帮助类
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        ///     发送HttpGet请求
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <returns>请求的响应信息</returns>
        public static string Get(string url)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}