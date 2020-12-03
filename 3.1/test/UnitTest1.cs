using System;
using System.Collections.Generic;
using Xunit;

namespace test
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void Reports_AddTo2020_Product(string input, int expected)
        {
            lib.Toboggan t = new lib.Toboggan();
            var trees = t.CountTrees3R1D(input);
            Assert.Equal(expected, trees);
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { sample, 7 },
                new object[] { input, 164 },
            };

        static string sample =
@"..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#";

        static string input =
@"....##..#........##...#.#..#.##
.#.#..#....##....#...#..##.....
##.#..##..#...#..........##.#..
.#.##.####..#......###.........
#.#.#...........#.....#...#....
#.......#....#.#.##..###..##..#
.#...#...##....#.........#.....
..........##.#.#.....#....#.#..
.......##..##...#.#.#...#......
.#.#.#...#...##...#.##.##..#...
........##.#.#.###.........##..
#.#..#.#.#.....#...#...#......#
.#.#.#...##......#...#.........
.#..##.##.#...#...##....#.#....
.##...#..#..#......##.###....##
.....#...#.###.....#.#.........
#.##..#....#.#.#.#.............
........#...#......#...#..#....
##..##...##.##...#...#.###...##
#.#....##.#...###......#..#.#..
..#.....#.##......#..........#.
#.......#..#......#.....#....#.
.....###...........#....#.##...
#.#........##.......#.#...#.##.
.#.#.#........#........#.#.....
#..#..##.....#.###..#.#.#.##..#
..#.#...#..##.#.#.#.......###..
........#........#..#..#...#...
##............#...#..##.##...#.
#....#.#.....##...#............
............#...#..#.#.#....#..
#.#.#...##.##.#....#....#......
................###.....#.....#
##.#####.#..#...###..#...###...
...#.....#...#.#....#...#..#...
.......#....##.##.#.##.........
..#..#..##.....#...#.#.....#...
...#...#.#.##.#..###.......#...
...#...........#.#####..##..#..
#.#...#........####..#......#.#
#..#.##...........#.#......#.##
#.#..#....##..#..##.#..........
.....#..#.....#........##..#...
...###.......#.##.......#......
...##..#..#...#....#.###...#...
....####....#........#.##.#.#.#
#....#.....#.###.##...##..##.##
.##.#...#.##.#......#..##.#....
...#.............#.............
..##..##.#.....#........##....#
#.....#....#.......####...#..#.
..#...#..#...#...##.#....##....
.#...##....#....#..#....#......
##..#.#...##......#..#.......##
..#...#.##..#.....#.#...#..#.#.
#..##....#..........#..........
.#........#..#......#......#.#.
...##.#.........#.#....#.#...#.
#.....#.#..#...#...#..#...#...#
#.........#.#.........##.......
.#.......#......#.........###..
.#..#..##...........#.....#..#.
.#....................#.....#..
.##.....#....#....#.###.....#..
...##.#.....#.#....#.........#.
.........##.....#.....#.##..#..
......#......#.#..#..###...#..#
..##...#.#..#...#.#....#.......
..#..##.###.#..#..#..#......#..
.....##...##.........#...##...#
###.#..##....##...##.#..###....
#...#.#..##......##...#.#..#...
..........#....###....#........
#.#.#.#.#.....#..##.##.....#...
.##.....#...#.....#......#.....
.#..........#.#.............#..
.#..##..#.#..##...#....#.##...#
..#.#..........#...#..........#
.#.......#.......#...#..#.....#
##.#...##...#......#.#..#......
#####..#....#..#...#...#.#.....
....#.......#.#..#.............
#..#..#.#.####...#....#....##..
#..#.##.#......#...#......#....
#...##.##...#....#..........##.
..#..#.......##.#....#...#.#...
.....#.##..............##.....#
..##.##...##.....#.........#.#.
.#....##...##...##..#....##..#.
.#...#....#..#......#.#........
#....#.#.#..............#....##
..##..#..#....#####.#....#.#.##
#....#...#.##..#.##.........###
#..#..#....#...............#.#.
#....##...##........##.##.#.##.
......#......##....##.....#.###
#...##..#..#.....#.#........##.
..#.#.##...#...#....#..###...#.
#..##..#.###..##.#.#....#......
..###..#.##........###.....#...
#.............#.............#..
.#.##....#..##.#...#....#.#####
###.....###.#......##..#..##...
.#.#......##.#....#....#.#..#..
###..#..#....#......###.##.....
......#.......#......#..##..##.
..#..#...#..#....#.##....#.#..#
.......##..#........#.#.##.....
.#...#..#........#..#.#..#..#.#
.#..#.##.......#......#...#.#..
.##..##......##.#...#......####
.....#....#......#.....#......#
..........#.#.#...##.#..#.#....
...#.......##......#..#.#.##...
.###..#.#.#....................
##...#...#.##............#.....
....#....#.##...#..#.#...##....
..#.#....#.###...#...#.#.####.#
..#..#.#...#.#......##.........
#..#..####.##.#.#..###....#...#
....#..........#.##.#..#.#.#.#.
#.#.##.........#.....##...#..##
#......#...#.##.#......#..#.#..
#...#........#..#..#...##...#..
.....#.####..##..#.#.##..#...#.
#..#........#........#...#....#
...........#..#.....#.#.#.#....
....#......#....#...#....##....
.#.#..#...#.#....#..#.#....##.#
....#...#...#.##..#...#..##...#
#######...............##.....##
.#.#..............#....#..#.###
#......#.#......###....###.....
##..#...#.##..##..##.#....#....
#....##..#..#...#.#.#...#......
..........#..#.##..##.##.#..##.
....#.#.#.....##........#..#...
..###...#.....##.##.....##..##.
....#.#..#.#.......#.......#...
..##.#..#.....##...###...#...#.
..#.........#...##...#...#..#..
..#..#..#..#..##.#...##..#.#...
...##..#..##..#..####...#.....#
............#............###...
.#.#.###.#.....#.#.#..#.###..#.
...#.........#.....####........
....##.#..##.#.............#...
....#.##.....#..#.....#......##
..........#.............#...##.
#..#.....#.......##..###.......
..##.#...........#.......#..#..
...#...#.#.##.###....#.#..#....
...#..........##..#..#..#...###
.........#.....#..##.....#..#..
#........#...#...#..#.#....##..
.#.#.....####..#.##.#..........
###.......##..#.##...#.....#...
..###.##.#..#..#..#.....##...#.
.........#.....##.#..#..##.....
#..#..##...###..............#..
#....#.#....#..#.....#..####...
####..#.....##...#..#.#.#.#...#
...#....#.....#.##.#.#.#....##.
..........#...#.....###....#.##
#....#.#.#....#..#..#.....#....
.....#.#...#......#....#......#
.####....##...#...#......##..#.
.#...#..#....#..#..............
##.#...##...#.##..#......#.....
..####.##..#....#.#......#.#.##
........#.....##...#.#..##....#
....#.#.#.#.###...#.#...##...##
#.....#...####.#....#.#........
..#.....#...##.........###.....
....#....#....#..#......#####.#
###.....#..#.#.#......#.##.#...
....#.##......#..#.#...........
.#....#....#.#..#.......#...##.
...................#.#.#..#....
##...#.....#........#....#...#.
........##......#...##.#..#.#.#
#.#..###...#....#.#...#.......#
#..........##......#..#..#.....
.............#...##.#...#......
#..#....##..#.........#..#.###.
.....#..........#....##.#...##.
###....................#.#.##..
#........##...##......#....###.
#....#.............#....#...#..
##.......##.#.......#....#..#..
##...#............#..#.#....##.
..#.#..#...#####..........#....
..#.........##.....#.#...####..
....#..............#...........
..#...#.#.#..#.......##.#.#.#..
....#.##.....##..#.....#..####.
#...#...#...#.......#.........#
......#..#.####...###.#.#.....#
.......#..#..#.....#.#..##.#..#
.#......##..#............#.....
.#........#.#....#....#........
.....#.#..#.##.#..##....#..#...
#.#...........#....##.....#....
..#..#..##.###..##..#..###.#.##
##.#..#...##.#.........#...#.#.
......#..#..##...#....#...#.##.
#.##.......................#...
.......#..#..#.##..##......#...
..#.#...#.....#..###....#...#..
##..#.....#..#..#.##.....#...##
#...##...###...#.#..###....#...
...#.#.#..####.....#...##....#.
.#.#..##.....#..#.....##..##..#
#...#..........#.##.#.#........
..##....#.##....#..##......#...
....#..........###.....####..##
...........###....##.#.#.#.#...
...#......................####.
#.#.#...#.#.#.#.#......#.....##
..###...#.####...#..##..#....#.
....#....#.......#...#.........
.#.###.............##..#...#...
....#.#....##...#.....#.##.....
#.......##.....#.#.....#....##.
....##.....###..#.#..#....#...#
......#..##...#......#.....#.##
.#.....#.##.###....#.....#..###
...#..#.###.#....#..#..#...##.#
...##..#...#..#.#.#..#...#.....
##.####...##..#.#.#....#.......
..##..#.#.......##.#......##.#.
....##....#....#..#....#..##.#.
..##..........##....#...#.#..#.
#.#...#.#.###.#.#..##.#...#....
.....#..#.............#...#...#
....#.#..#...##...#....#.##....
#..#...#.###.....#...#.....#.#.
#####....#....#....#.......#.##
#...##....##.#.#...#.....##.#..
#.......#...#..#..#...#....#...
....#......#.#..........#....##
#.###...#.#..##..#.##........#.
#..#.....##.......#..#..#.#....
...#...#.##...#....#.#.#.#...#.
...##..#.#....#......###......#
#.#....#...#..#..#....#........
..#..#.#...##..........#.###...
#..........#...#..#....#....###
..#..#.#....#..............#...
...#........#...#.#....###.#..#
....#.#.#................#..#.#
..#........##.#....#.#..#......
...##..#..#.......#..#......#.#
..#..#....#.........#....#.##..
#.....#....###.#..#..#...#...#.
..#..##.###.#..##....#.###.....
...#...####..#........###.#....
.........#.#...#..#..#.#.......
.##.........##.#..............#
..#.#.#.....###........#.#.#..#
....##..#....#....#.#..#.......
#.#.....#...#........##........
.##.#.#..#..#.#.#.........#....
#.....#..#.##...#...#..........
##..#....#....##.#..#.........#
................#.##.#......#.#
..#..#.#........##...###..#...#
##........#.......#...##.##..#.
##....#.....#..##....#.......#.
#.#....#.#........#..#.........
......##......#...#.....#.##...
###....#..........##.#.#......#
......#...###.........###..#...
.####....#...##..#.#.....#...#.
.##...#...###....#...#.#..###..
#..#......##...#.###..###...#..
#....#.#.#..#....##...#.##..#..
..#.....#...#..........#.##.###
#.....#....###.......##..##.#..
#..##...#..#....#.###......#...
#..#........##..#.....#.#.#....
#.##.#.#..#....#.#.............
.#...............#....##.......
.#.##......##........#...#..#.#
.#...#....#....#...#..#...##...
.....#..###...##........#.#....
...#.......#....##..#..#....#..
...###....#........#..#.###.#..
......##..##..............###.#
.......#.####..##....#.#....#..
#...#......#...#..#.....##....#
.#..#..###....#..##.##.#.......
#......##......#..##....#..##..
.....#..#.#......##.##..##.....
...#..#.......#......#.........
....#..####......#..#....#...#.
..#.#..#...#....#....#.......#.
####..#........#.###...##.#.#.#
.......##........#.#.#...##....
...#.........#..#.#..##....#...
.....#..#...#.#....#...#.#.##.#
#..##.....#.....##.......#...#.
.......##.#.#.....#....#......#
...#...#.##...#......#....#....
..#..#.#...#..#.....#...###.#..
.........#...#..#.......##.....
..##...................#..#.###
.##.##..#.#...#.#....#.....##..
#.#...##...#...#...##..#......#
....#..#...#.....##.#.....#..##
##.#..........###..#...#..#....
...##....#.##....#......#......
.....#.........#....#.#.......#
.......#............#.#.....#..
..#..#...#..#####..#....##.....
...##......##...#.#........##..
.....#..###...##.#.#.##.#...#..
..#.#.#..##..#.##...##.#.#.....
......##...#..##......#.#......
......................#........
#...#..#....#..#.#.##.#.....#.#
.#......#.#....#.#.#..#....#...
.#..#.#.#..#....#..............";

    }
}