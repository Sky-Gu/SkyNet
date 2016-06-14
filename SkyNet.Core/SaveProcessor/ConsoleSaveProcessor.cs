using System;

namespace SkyNet.Core.SaveProcessor
{
    /// <summary>
    ///     控制台输出
    /// </summary>
    public class ConsolePipeline : ISaveProcessor
    {
        /// <summary>
        ///     控制台输出处理结果
        /// </summary>
        /// <param name="pageResult"></param>
        public void Process(PageResult pageResult)
        {
            Console.WriteLine(pageResult.Request.Url);
        }
    }
}