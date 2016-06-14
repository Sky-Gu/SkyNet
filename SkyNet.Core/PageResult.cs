using System.Collections.Generic;

namespace SkyNet.Core
{
    /// <summary>
    ///     PageProcessor 的处理结果
    /// </summary>
    public class PageResult
    {
        public PageResult()
        {
            IsSave = true;
            ResultItem = new Dictionary<string, object>();
        }

        /// <summary>
        ///     请求信息
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        ///     是否需要被保存流程处理
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        ///     结果值
        /// </summary>
        public Dictionary<string, object> ResultItem { get; set; }
    }
}