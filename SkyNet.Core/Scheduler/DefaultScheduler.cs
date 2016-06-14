using System.Collections.Generic;
using System.Linq;

namespace SkyNet.Core.Scheduler
{
    public class DefaultScheduler : IScheduler
    {
        /// <summary>
        ///     内存的url队列
        /// </summary>
        private readonly List<string> _urlList = new List<string>();

        /// <summary>
        ///     url入队
        /// </summary>
        /// <param name="request">url请求信息</param>
        public void Enqueue(Request request)
        {
            _urlList.Add(request.Url);
        }

        /// <summary>
        ///     url出队
        /// </summary>
        /// <returns></returns>
        public Request Dequeue()
        {
            return new Request
            {
                Url = _urlList.FirstOrDefault()
            };
        }
    }
}