using SkyNet.Core.Model;
using SkyNet.Core.Scheduler;

namespace SkyNet.Core.Spider
{
    /// <summary>
    ///     爬虫约束
    /// </summary>
    public interface ISpider
    {
        /// <summary>
        ///     队列管理器
        /// </summary>
        IScheduler Scheduler { get; }

        /// <summary>
        ///     爬虫设置
        /// </summary>
        Site Site { get; }


        /// <summary>
        ///     运行爬虫
        /// </summary>
        void Run();

        /// <summary>
        ///     停止爬虫
        /// </summary>
        void Stop();
    }
}