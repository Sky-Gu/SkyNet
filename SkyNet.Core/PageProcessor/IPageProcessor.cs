namespace SkyNet.Core.PageProcessor
{
    /// <summary>
    ///     页面解析器
    /// </summary>
    public interface IPageProcessor
    {
        /// <summary>
        ///     页面解析
        /// </summary>
        void Process(Page page);
    }
}