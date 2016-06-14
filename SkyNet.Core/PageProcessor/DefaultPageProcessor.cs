namespace SkyNet.Core.PageProcessor
{
    /// <summary>
    ///     默认的页面处理
    /// </summary>
    public class DefaultPageProcessor : IPageProcessor
    {
        public void Process(Page page)
        {
            page.PageResult.ResultItem.Add("url", page.Request.Url);
        }
    }
}