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
            Conway c = new Conway(input);
            long cycles = c.RunCycles(6);
            Assert.Equal(expected, cycles);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 112 },
                new object[] { input, 359 },
            };

        static string sample1 =
@".#.
..#
###";

        static string input =
@".##..#.#
#...##.#
##...#.#
.##.##..
...#.#.#
.##.#..#
...#..##
###..##.";
    }
}