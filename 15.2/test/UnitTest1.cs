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
            Memory m = new Memory(input);
            long n = m.Nth(2020);
            Assert.Equal(expected, n);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 175594 },
                new object[] { sample2, 2578 },
                new object[] { sample3, 3544142 },
                new object[] { sample4, 261214 },
                new object[] { sample5, 6895259 },
                new object[] { sample6, 18 },
                new object[] { sample7,362 },
                new object[] { input, 47205 },
            };

        static string sample1 = @"0,3,6";
        static string sample2 = @"1,3,2";
        static string sample3 = @"2,1,3";
        static string sample4 = @"1,2,3";
        static string sample5 = @"2,3,1";
        static string sample6 = @"3,2,1";
        static string sample7 = @"3,1,2";
        static string input = @"1,12,0,20,8,16";
    }
}