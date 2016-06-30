using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SkyNet.Tools;

namespace SkyNet.ToolsTests
{
    [TestClass]
    public class FileHelperTests
    {
        [TestMethod]
        public void CreateTest_success()
        {
            var fileHelper = new FileHelper();
            fileHelper.Create(@"D:\", @"test.txt", "暗杀了空间发生了空间发了手机放辣椒等法律框架");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTest_ArgumentException()
        {
            var fileHelper = new FileHelper();
            fileHelper.Create(null, @"test.txt", "暗杀了空间发生了空间发了手机放辣椒等法律框架");
            fileHelper.Create("1", null, "暗杀了空间发生了空间发了手机放辣椒等法律框架");
            fileHelper.Create("1", null, "暗杀了空间发生了空间发了手机放辣椒等法律框架");
        }

        [TestMethod]
        public void IsExistDirectoryTest_Success()
        {
            var fileHelper = new FileHelper();
            var actual = fileHelper.IsExistDirectory(@"C:\");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsExistDirectoryTest_ArgumentException()
        {
            var fileHelper = new FileHelper();
            fileHelper.IsExistDirectory("");
        }

        [TestMethod]
        public void CreateDirectoryTest_Success()
        {
            var fileHelper = new FileHelper();
            fileHelper.CreateDirectory(@"D:\CreateDirectoryTest\");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateDirectoryTest_ArgumentException()
        {
            var fileHelper = new FileHelper();
            fileHelper.CreateDirectory("");
        }
    }
}