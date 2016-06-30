using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Core.Pipeline;

namespace SkyNet.CoreTests.Pipeline
{
    [TestClass]
    public class FilePipelineTests
    {
        [TestMethod]
        public void ProcessTest_Success()
        {
            var pageRequestMock = MockFactory.GetPageResultMock();
            var pipeLine = new FilePipeline();
            pipeLine.Process(pageRequestMock.Object);
        }

        [TestMethod]
        public void ProcessTest_SuccessFilePath()
        {
            var pageRequestMock = MockFactory.GetPageResultMock();
            var pipeLine = new FilePipeline
            {
                FileName = "fileUnitTest.txt",
                Directory = @"D:\"
            };

            pipeLine.Process(pageRequestMock.Object);
        }
    }
}