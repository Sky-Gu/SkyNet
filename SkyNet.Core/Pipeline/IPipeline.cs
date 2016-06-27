using SkyNet.Core.Model;

namespace SkyNet.Core.Pipeline
{
    /// <summary>
    ///     处理结果保存
    /// </summary>
    public interface IPipeline
    {
        /// <summary>
        ///     保存处理结果
        /// </summary>
        /// <param name="pageResult">页面的处理结果</param>
        void Process(PageReuslt pageResult);
    }
}
