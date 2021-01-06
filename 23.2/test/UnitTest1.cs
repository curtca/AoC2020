using System;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Test(string input, long expected)
        {
            Cups c = new Cups(input, 1_000_000, 10_000_000);
            long s = c.OneHundred();
            Assert.Equal(expected, s);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 149245887792 },
                new object[] { input, 474747880250 },
            };

        static string sample1 =
@"389125467";

        static string input =
@"193467258";
    }
}