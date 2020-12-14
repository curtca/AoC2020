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
            Bus b = new Bus(input);
            long id = b.Earliest();
            Assert.Equal(expected, id);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 295 },
                new object[] { input, 3215 },
            };

        static string sample1 =
@"939
7,13,x,x,59,x,31,19";

        static string input =
@"1015292
19,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,743,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,643,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23";
    }
}