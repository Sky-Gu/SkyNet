using System;
using System.IO;

namespace SkyNet.Tools
{
    /// <summary>
    ///     文件帮助类
    /// </summary>
    public class FileHelper
    {
        #region Create

        /// <summary>
        ///     创建文件
        /// </summary>
        /// <param name="directory">文件路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileContent">文件内容</param>
        public void Create(string directory, string fileName, string fileContent)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentException("directory");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName");

            CreateDirectory(directory);

            using (var fileStream = new FileStream(directory + fileName, FileMode.CreateNew))
            {
                using (var sw = new StreamWriter(fileStream))
                {
                    sw.WriteLine(fileContent);
                }
            }
        }

        #endregion

        #region IsExistDirectory  

        /// <summary>
        ///     检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public bool IsExistDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("directoryPath");

            return Directory.Exists(directoryPath);
        }

        #endregion

        #region CreateDirectory  

        /// <summary>
        ///     创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public void CreateDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("directoryPath");

            if (!IsExistDirectory(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        #endregion
    }
}