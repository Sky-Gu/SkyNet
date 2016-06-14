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
        void Enqueue(Request request);

        /// <summary>
        ///     抽取一个Url给爬虫调用
        /// </summary>
        Request Dequeue();
    }
}