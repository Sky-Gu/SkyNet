using System.Collections.Generic;
using System.Linq;
using SkyNet.Core.Model;

namespace SkyNet.Core.Scheduler
{
    public class MemoryScheduler : IScheduler
    {
        /// <summary>
        ///     内存的url队列
        /// </summary>
        public List<string> WaitUrlList { get; } = new List<string>();

        /// <summary>
        ///     内存的完成队列
        /// </summary>
        public List<string> CompleteUrlList { get; } = new List<string>();

        /// <summary>
        ///     添加一个等待队列
        /// </summary>
        public void AddWaitRequest(Request request)
        {
            var url = request.Uri.ToString();
            if (!CompleteUrlList.Contains(url) && !WaitUrlList.Contains(url))
                WaitUrlList.Add(url);
        }

        /// <summary>
        /// 添加一个完成队列
        /// </summary>
        public void AddFinishRequest(Request request)
        {
            CompleteUrlList.Add(request.Uri.ToString());
        }

        /// <summary>
        ///     url出队
        /// </summary>
        public Request GetRequest()
        {
            var url = WaitUrlList.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(url))
                return null;

            var result = new Request(url);
            WaitUrlList.RemoveAt(0);
            return result;
        }
    }
}