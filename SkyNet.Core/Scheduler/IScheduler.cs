using SkyNet.Core.Model;

namespace SkyNet.Core.Scheduler
{
    /// <summary>
    ///     队列管理器
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        ///     添加一个url进入队列
        /// </summary>
        void AddWaitRequest(Request request);

        /// <summary>
        ///     添加已完成的url进入完成队列
        /// </summary>
        /// <param name="request"></param>
        void AddFinishRequest(Request request);

        /// <summary>
        ///     抽取一个Url给爬虫调用
        /// </summary>
        Request GetRequest();
    }
}