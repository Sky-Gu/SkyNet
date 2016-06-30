using System;
using SkyNet.Core.Model;
using SkyNet.Tools;

namespace SkyNet.Core.Pipeline
{
    public class FilePipeline : IPipeline
    {
        /// <summary>
        ///     文件名称
        /// </summary>
        public string FileName { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     文件目录
        /// </summary>
        public string Directory { get; set; } = @"D:\DownloaderPage\";

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; set; } = ".html";

        /// <summary>
        ///     获取完整的文件路径
        /// </summary>
        public string FullFilePath => Directory + FileName;

        public void Process(PageReuslt pageResult)
        {
            var startTime = new DateTime(1970, 1, 1);
            FileName = (DateTime.Now - startTime).TotalMilliseconds + Guid.NewGuid().ToString() + Extension;
            new FileHelper().Create(Directory, FileName, pageResult.ToString());
        }
    }
}