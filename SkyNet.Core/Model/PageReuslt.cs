using System.Collections.Generic;
using Newtonsoft.Json;

namespace SkyNet.Core.Model
{
    /// <summary>
    ///     PageProcessor 的处理结果
    /// </summary>
    public class PageReuslt
    {
        /// <summary>
        ///     初始化PageResult
        /// </summary>
        public PageReuslt()
        {
            IsSave = true;
            ResultItem = new Dictionary<string, object>();
        }

        /// <summary>
        ///     初始化PageResult
        /// </summary>
        /// <param name="isSave">是否需要被Pieline处理</param>
        public PageReuslt(bool isSave) : this()
        {
            IsSave = isSave;
        }

        /// <summary>
        ///     Pieline 抽取的字段
        /// </summary>
        public virtual Dictionary<string, object> ResultItem { get; }

        /// <summary>
        ///     请求信息
        /// </summary>
        public virtual Request Request { get; set; }

        /// <summary>
        ///     是否需要被Pieline处理
        /// </summary>
        public virtual bool IsSave { get; set; }

        #region AddOrUpdateResultIteme

        /// <summary>
        ///     添加或更新一个ResultItem，
        ///     key存在时，更新
        ///     key不存在，添加
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">value值</param>
        public void AddOrUpdateResultItem(string key, object value)
        {
            lock (this)
            {
                if (ResultItem.ContainsKey(key))
                    ResultItem[key] = value;
                else
                    ResultItem.Add(key, value);
            }
        }

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}