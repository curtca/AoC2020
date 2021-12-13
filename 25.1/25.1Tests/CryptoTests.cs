using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestClass()]
    public class CryptoTests
    {
        [TestMethod()]
        [DynamicData("TestData")]
        public void CryptoTest(string input, long output)
        {
            var c = new Crypto(input);
            long e = c.GetEncryptionKey();
            Assert.AreEqual(output, e);
        }

        public static IEnumerable<object[]> TestData =>
            new List<object[]>
            {
                new object[] { sample, 14897079 },
                new object[] { input, 711945 },
            };

        static string sample =
@"5764801
17807724";

        static string input =
@"11239946
10464955";
    }
}