using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HostHelper.Services;

namespace Tests
{
    [TestClass]
    public class VirtualHostTests
    {
        [TestMethod]
        public void TestVirtualHostSerialisation()
        {
            var txtBlocks = VirtualHostService.GetVirtualHostTextBlocks();
            foreach (var block in txtBlocks)
            {
                var vHost = VirtualHostService.ConvertToVirtualHost(block);
                var vHostText = vHost.GenerateText();
                Assert.AreEqual(block, vHostText);
            }
        }
    }
}
