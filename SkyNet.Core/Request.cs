using System.Collections.Generic;

namespace SkyNet.Core
{
    /// <summary>
    ///     请求地址封装
    /// </summary>
    public class Request
    {
        public Request()
        {
            Addition = new Dictionary<string, object>();
        }

        /// <summary>
        ///     请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     附加信息
        /// </summary>
        public Dictionary<string, object> Addition { get; set; }
    }
}