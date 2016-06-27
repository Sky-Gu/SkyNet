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
        private readonly List<string> _urlList = new List<string>();

        public void AddRequest(Request request)
        {
            _urlList.Add(request.Uri.ToString());
        }

        /// <summary>
        ///     url出队
        /// </summary>
        public Request GetRequest()
        {
            return new Request(_urlList.FirstOrDefault());
        }
    }
}