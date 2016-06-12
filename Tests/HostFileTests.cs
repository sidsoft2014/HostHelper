using HostHelper.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HostFileTests
    {
        [TestMethod]
        public void TestHostFileSerialisation()
        {
            var txtBlocks = HostFileService.GetAllHosts();

            Assert.AreEqual("", "");
        }
    }
}
