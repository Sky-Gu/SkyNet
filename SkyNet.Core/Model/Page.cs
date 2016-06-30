namespace SkyNet.Core.Model
{
    /// <summary>
    ///     页面信息
    /// </summary>
    public class Page
    {
        /// <summary>
        ///     初始化Page
        /// </summary>
        /// <param name="request"></param>
        public Page(Request request)
        {
            Request = request;
            PageResult = new PageReuslt { Request = request };
        }

        /// <summary>
        ///     请求状态码
        /// </summary>
        public virtual int StatusCode { get; set; }

        /// <summary>
        ///     请求地址
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        ///     页面标题
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        ///     页面内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        ///     是否需要被Pieline处理
        /// </summary>
        public virtual bool IsSave
        {
            get { return PageResult.IsSave; }
            set { PageResult.IsSave = value; }
        }

        /// <summary>
        ///     请求信息
        /// </summary>
        public Request Request { get; }

        /// <summary>
        ///     页面处理信息
        /// </summary>
        public PageReuslt PageResult { get; }

        #region AddOrUpdateResultItem

        /// <summary>
        ///     添加或更新一个ResultItem，
        ///     key存在时，更新
        ///     key不存在，添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">value值</param>
        public void AddOrUpdateResultItem(string key, object value)
        {
            PageResult.AddOrUpdateResultItem(key, value);
        }

        #endregion
    }
}