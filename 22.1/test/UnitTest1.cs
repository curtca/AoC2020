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
            War w = new War(input);
            long score = w.WinningScore();
            Assert.Equal(expected, score);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample1, 306 },
                new object[] { input, 33403 },
            };

        static string sample1 =
@"Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10";

        static string input =
@"Player 1:
7
1
9
10
12
4
38
22
18
3
27
31
43
33
47
42
21
24
50
39
8
6
16
46
11

Player 2:
49
41
40
35
44
29
30
19
14
2
34
17
25
5
15
32
20
48
45
26
37
28
36
23
13";
    }
}