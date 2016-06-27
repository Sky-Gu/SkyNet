using System;
using System.Collections.Generic;

namespace SkyNet.Core.Model
{
    /// <summary>
    ///     请求地址封装
    /// </summary>
    public class Request
    {
        public Request()
        {
        }

        public Request(Uri uri)
        {
            Uri = uri;
        }

        public Request(string url) : this(new Uri(url))
        {
        }

        public Request(string url, int depth) : this(url)
        {
            Depth = depth;
        }


        /// <summary>
        ///     爬取深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        ///     请求的URI
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        ///     请求的优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        ///     附加信息
        /// </summary>
        public Dictionary<string, object> Addition { get; set; }

        /// <summary>
        ///     请求方式
        /// </summary>
        public string Method { get; set; } = "GET";

        /// <summary>
        ///     post请求时的数据
        /// </summary>
        public string PostBody { get; set; }
    }
}