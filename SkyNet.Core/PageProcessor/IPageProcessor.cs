using SkyNet.Core.Model;

namespace SkyNet.Core.PageProcessor
{
    /// <summary>
    ///     页面解析器
    /// </summary>
    public interface IPageProcessor
    {
        /// <summary>
        ///     请求的设置信息
        /// </summary>
        Site Site { get; set; }

        /// <summary>
        ///     页面解析
        /// </summary>
        void Process(Page page);
    }
}