namespace SkyNet.Core.SaveProcessor
{
    /// <summary>
    ///     处理结果保存
    /// </summary>
    public interface ISaveProcessor
    {
        /// <summary>
        ///     保存处理结果
        /// </summary>
        /// <param name="pageResult">页面的处理结果</param>
        void Process(PageResult pageResult);
    }
}
