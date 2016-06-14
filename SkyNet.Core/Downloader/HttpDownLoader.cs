using SkyNet.Tools;

namespace SkyNet.Core.Downloader
{
    /// <summary>
    ///     http 页面请求
    /// </summary>
    public class HttpDownLoader : IDownLoader
    {
        /// <summary>
        ///     下载页面
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns>页面信息</returns>
        public Page DownLoader(Request request)
        {
            var result = HttpHelper.Get(request.Url);
            return new Page
            {
                Request = request,
                PageResult = new PageResult
                {
                    Request = request
                }
            };
        }
    }
}