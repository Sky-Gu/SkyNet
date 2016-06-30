using System;
using SkyNet.Core.Model;
using SkyNet.Tools;

namespace SkyNet.Core.Pipeline
{
    public class FilePipeline : IPipeline
    {
        public FilePipeline()
        {

        }

        public FilePipeline(string fileName, string directory)
        {
            FileName = fileName;
            Directory = directory;
        }

        /// <summary>
        ///     文件名称
        /// </summary>
        public string FileName { get; }

        /// <summary>
        ///     文件目录
        /// </summary>
        public string Directory { get; }

        public void Process(PageReuslt pageResult)
        {
            const string extension = @".html";

            var directoryPath = @"D:\DownloaderPage\";
            var fileName = Guid.NewGuid() + extension;
            if (!string.IsNullOrWhiteSpace(FileName))
                fileName = FileName;
            if (!string.IsNullOrWhiteSpace(Directory))
                directoryPath = Directory;

            new FileHelper().Create(directoryPath, fileName, pageResult.ToString());
        }
    }
}