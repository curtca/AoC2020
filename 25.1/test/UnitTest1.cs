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
            Floor f= new Floor(input);
            long b = f.Black();
            Assert.Equal(expected, b);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1,  },
                new object[] { input,  },
            };

        static string sample1 =
@"5764801
17807724";

        static string input =
@"11239946
10464955";
    }
}