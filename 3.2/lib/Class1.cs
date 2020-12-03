using System;

namespace lib
{
    public class Toboggan
    {
        public long CountTrees5WaysProduct(string input) // 3 right, 1 down
        {
            string[] lines = input.Split("\r\n");
            int width = lines[0].Length;
            int height = lines.Length;
            int cTrees11 = 0;
            int cTrees31 = 0;
            int cTrees51 = 0;
            int cTrees71 = 0;
            int cTrees12 = 0;
            int x11 = 0;
            int x31 = 0;
            int x51 = 0;
            int x71 = 0;
            int x12 = 0;
            for (int y = 1; y < height; y++)
            {
                x11 = (x11 + 1) % width;
                x31 = (x31 + 3) % width;
                x51 = (x51 + 5) % width;
                x71 = (x71 + 7) % width;

                if (lines[y][x11] == '#')
                    cTrees11++;
                if (lines[y][x31] == '#')
                    cTrees31++;
                if (lines[y][x51] == '#')
                    cTrees51++;
                if (lines[y][x71] == '#')
                    cTrees71++;
                if (y % 2 == 0) {
                    x12 = (x12 + 1) % width;
                    if (lines[y][x12] == '#')
                        cTrees12++;
                }
            }
            long prod = (long) cTrees11 * cTrees31 * cTrees51 * cTrees71 * cTrees12;
            return prod;
        }
    }
}
