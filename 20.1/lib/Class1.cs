using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Tiles
{
    List<Tile> tiles = new List<Tile>();
    Dictionary<int, List<Tuple<Tile, int, int>>> tilesbyedge = new Dictionary<int, List<Tuple<Tile, int, int>>>();
    public Tiles(string input)
    {
        // map from edge signature (not tile id!) to list of tuples with that sig: Tile, face, rotation
        string[] strtiles = input.Split("\r\n\r\n");
        foreach (var strTile in strtiles)
        {
            Tile t = new Tile(strTile);
            tiles.Add(t);
            for (int face = 0; face < 2; face++)
            {
                for (int edge = 0; edge < 4; edge++)
                {
                    List<Tuple<Tile, int, int>> listOfOrientedTiles;
                    if (!tilesbyedge.TryGetValue(t.edges[face][edge], out listOfOrientedTiles))
                    {
                        listOfOrientedTiles = new List<Tuple<Tile, int, int>>();
                        tilesbyedge[t.edges[face][edge]] = listOfOrientedTiles;
                    }

                    listOfOrientedTiles.Add(new Tuple<Tile, int, int>(t, face, edge));
                }
            }
        }

        /*    
            Hypothesis: At this point, there will be exactly 4 tiles (corners) with exactly 2 edges containing
            signatures that are not contained on any other tiles, and those two edges will be adjacent. There will 
            similarly be 40 tiles each with a single edge with a unique signature. These are the tiles on the 
            perimeter. The other 100 tiles will will not have ANY unique signatures. Rather, ALL have edges 
            will share a signature with one other tile. So once we choose (arbitrarily) one of the correct corner 
            tiles, everything falls into place. (In the full input of 12x12 tiles.)

            Remember: Each edge has two signatures (keys) in tilesbyedge. One for each face.
        */
    }

    public long CornersProduct()
    {
        // Count number of tiles which share each edge.
        var uniqueEdges = tilesbyedge.Select(pair => new { edgesig = pair.Key, Count = pair.Value.Count })
            .OrderBy(x => x.Count);

        var singles = uniqueEdges.Where(x => x.Count == 1).Select(pair => pair.edgesig);

        // Corners are the tiles which have 2 unique edges
        var corners = tiles.Where(t =>
            {
                int uniqueedges = t.edges[0].Where(sig => singles.Contains(sig)).Count();
                return uniqueedges == 2;
            });

        return corners.Aggregate((long) 1, (product, tile) => product *= tile.id);
    }
}

public class Tile
{
    static int size = 10;
    public int id;
    public int[][] edges = new int[][] { new int[4], new int[4] };
    // [0=clockwise, 1=countercw][rotation] bit encoding of each edge, by direction (0-1) and edge (0-3)
    // When two tiles are adjacent, opposite edges must have opposite orientations (clockwise / counterclockwise)
    //      or opposite faces (
    // flip is defined as flipping the face, keeping the upper-left corner in place
    public Tile(string input)
    {
        string[] lines = input.Split("\r\n"); // First line is like "Tile ####:"
        id = int.Parse(lines[0].Substring(5, 4));
        lines = lines.Skip(1).ToArray();
        size = lines.Length;
        for (int edge = 0; edge < 4; edge++)
        {
            int x0 = 0, y0 = 0, dx = 0, dy = 0;
            switch (edge)
            {
                case 0:
                    x0 = 0; y0 = 0; dx = 1; dy = 0;
                    break;
                case 1:
                    x0 = size - 1; y0 = 0; dx = 0; dy = 1;
                    break;
                case 2:
                    x0 = size - 1; y0 = size - 1; dx = -1; dy = 0;
                    break;
                case 3:
                    x0 = 0; y0 = size - 1; dx = 0; dy = -1;
                    break;
            }

            (edges[0][edge], edges[1][edge]) = Sig(lines, x0, y0, dx, dy);
        }
    }

    // Returns the signature of the edge both in clockwise and counter-clockwise rotations 
    private (int, int) Sig(string[] lines, int x0, int y0, int dx, int dy)
    {
        int sig0 = 0, sig1 = 0;
        int x = x0, y = y0;
        for (int i = 0; i < size; i++)
        {
            if (lines[y][x] == '#')
            {
                sig0 |= 1 << i;
                sig1 |= 1 << (size - i - 1);
            }
            x += dx; y += dy;
        }
        return (sig0, sig1);
    }
}
