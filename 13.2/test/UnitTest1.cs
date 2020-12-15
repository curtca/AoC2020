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
            long sync = b.Sync();
            Assert.Equal(expected, sync);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 1068781 },
                new object[] { sample2, 3417 },
                new object[] { sample3, 754018 },
                new object[] { sample4, 779210 },
                new object[] { sample5, 1261476 },
                new object[] { sample6, 1202161486 },
                new object[] { input, 1001569619313439 },
            };

        static string sample1 =
@"939
7,13,x,x,59,x,31,19";

        static string sample2 =
@"x
17,x,13,19";

        static string sample3 = 
@"x
67,7,59,61";

        static string sample4 =
@"x
67,x,7,59,61";

        static string sample5 =
@"x
67,7,x,59,61";

        static string sample6 =
@"x
1789,37,47,1889";

        static string input =
@"1015292
19,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,743,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,643,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,23";
    }
}