namespace SkyNet.Core
{
    /// <summary>
    ///     页面信息
    /// </summary>
    public class Page
    {
        public Page()
        {
            PageResult = new PageResult();
        }

        /// <summary>
        ///     请求信息
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        ///     页面处理信息
        /// </summary>
        public PageResult PageResult { get; set; }
    }
}