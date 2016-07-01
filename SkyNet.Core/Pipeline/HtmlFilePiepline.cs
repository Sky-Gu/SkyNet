using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkyNet.Core.Model;
using SkyNet.Tools;

namespace SkyNet.Core.Pipeline
{
    public class HtmlFilePiepline : IPipeline
    {
        /// <summary>
        ///     文件名称
        /// </summary>
        public string FileName { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     文件目录
        /// </summary>
        public string Directory { get; set; } = @"D:\DownloaderHtml\";

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
            FileName = pageResult.ResultItem["title"].ToString() + (DateTime.Now - startTime).TotalMilliseconds + Extension;
            new FileHelper().Create(Directory, FileName, pageResult.ResultItem["content"].ToString());
        }
    }
}
