using System;

namespace lib
{
    public class Toboggan
    {
        public int CountTrees3R1D(string input) // 3 right, 1 down
        {
            string[] lines = input.Split("\r\n");
            int width = lines[0].Length;
            int height = lines.Length;
            int cTrees = 0;
            int x = 0;
            for (int y = 1; y < height; y++)
            {
                x = (x + 3) % width;

                if (lines[y][x] == '#')
                    cTrees++;
            }
            return cTrees;
        }
    }
}
