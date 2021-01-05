using System;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Test(string input, string expected)
        {
            Cups c = new Cups(input);
            string s = c.OneHundred();
            Assert.Equal(expected, s);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, "67384529" },
                new object[] { input, "25468379" },
            };

        static string sample1 =
@"389125467";

        static string input =
@"193467258";
    }
}