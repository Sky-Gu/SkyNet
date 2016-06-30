using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public virtual int Depth { get; set; }

        /// <summary>
        ///     请求的URI
        /// </summary>
        public virtual Uri Uri { get; set; }

        /// <summary>
        ///     请求的优先级
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        ///     附加信息
        /// </summary>
        public virtual Dictionary<string, object> Addition { get; set; }

        /// <summary>
        ///     请求方式
        /// </summary>
        public virtual string Method { get; set; } = "GET";

        /// <summary>
        ///     post请求时的数据
        /// </summary>
        public virtual string PostBody { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}