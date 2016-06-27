using SkyNet.Core.Model;

namespace SkyNet.Core.PageProcessor
{
    /// <summary>
    ///     默认的页面处理
    /// </summary>
    public class DefaultPageProcessor : IPageProcessor
    {
        public Site Site { get; set; } = new Site();

        public void Process(Page page)
        {
            page.AddOrUpdateResultItem("title", page.Title);
            page.AddOrUpdateResultItem("url", page.Url);
        }
    }
}