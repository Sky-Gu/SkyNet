using System.Collections.Generic;

namespace SkyNet.Core.Model
{
    /// <summary>
    ///     站点爬去设置
    /// </summary>
    public class Site
    {
        /// <summary>
        ///     线程数
        /// </summary>
        public int ThreadCount { get; set; } = 5;

        /// <summary>
        ///     最大休眠时间
        /// </summary>
        public int MaxSleepTime { get; set; } = 1000;

        /// <summary>
        ///     最小休眠时间
        /// </summary>
        public int MinSleepTime { get; set; } = 500;

        /// <summary>
        ///     失败重试次数
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        ///     请求头部
        /// </summary>
        public Dictionary<string, string> Handers { get; set; } = new Dictionary<string, string>();
    }
}