using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Tiles
{
    List<Tile> tiles = new List<Tile>();
    // map from edge signature (not tile id!) to list of tuples with that sig: Tile, face, rotation
    Dictionary<int, List<Tuple<Tile, int, int>>> tilesbyedge = new Dictionary<int, List<Tuple<Tile, int, int>>>();

    public Tiles(string input)
    {
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

    public long RoughWaters()
    {
        char[,] patched = Patch();
        return -1;
    }

    public char[,] Patch()
    {
        int dimTiles = (int)Math.Sqrt(tiles.Count);
        int dimSpaces = dimTiles * (Tile.size - 2) + 2;
        Tile[,] positionedTiles = new Tile[dimTiles, dimTiles];
        char[,] patched = new char[dimSpaces, dimSpaces];

        // Count number of tiles which share each edge.
        var uniqueEdges = tilesbyedge.Select(pair => new { edgesig = pair.Key, Count = pair.Value.Count })
            .OrderBy(x => x.Count);

        // Signatures which are unique (on the perimiter). Contains sigs in both directions, 
            // so 2x as many as actual perimiter edges.
        var singles = uniqueEdges.Where(x => x.Count == 1).Select(pair => pair.edgesig);

        // Corners are the tiles which have 2 unique edges (on the 0 face, arbitrarily)
        var corners = tiles.Where(t =>
                t.edges[0].Where(sig => singles.Contains(sig)).Count() == 2
            );

        Tile startingCorner = corners.First(); // upper left of new patch
        // Need to orient it so that the two unique edges are top and left (0 and 3)
        startingCorner.face = 1 - startingCorner.face; // flip it just for fun
        while (!(singles.Contains(startingCorner.PositionedSig(0))
              && singles.Contains(startingCorner.PositionedSig(3))))
            startingCorner.rotation++;
        positionedTiles[0, 0] = startingCorner;

        Debug.Print("Tile arrangement:");
        Debug.Write($"{startingCorner.id}/f{startingCorner.face}/r{startingCorner.rotation} ");


        for (int row = 0; row < dimTiles; row++)
        {
            if (row > 0)
            {
                Tile t = FindAndOrient(positionedTiles[0, row - 1], 2); // get the tile that fits on edge 1, and orient it accordingly
                positionedTiles[0, row] = t;
                Debug.Write($"{t.id}/f{t.face}/r{t.rotation} ");
            }

            for (int col = 1; col < dimTiles; col++)
            {
                Tile t = FindAndOrient(positionedTiles[col - 1, row], 1); // get the tile that fits on edge 1, and orient it accordingly
                positionedTiles[col, row] = t;
                Debug.Write($"{t.id}/f{t.face}/r{t.rotation} ");
            }
            Debug.Print("");
        }

        return patched;
    }

    private Tile FindAndOrient(Tile sourceTile, int sourceEdge)
    {
        int sig = sourceTile.PositionedSig(sourceEdge); // this is what we need to match;
        var matches = tilesbyedge[sig]; // this better be count=2
        var match = matches.Where(tuple => tuple.Item1 != sourceTile).First();
        Tile targetTile = match.Item1;
        int targetDir = match.Item2;
        int targetEdge = match.Item3;
        targetTile.face = 1 - targetDir;
        targetTile.rotation = (6 + sourceEdge + targetTile.face * (-1 + 2 * ((targetEdge + 1) % 2)) - targetEdge) % 4;
        return targetTile;
    }
}

public class Tile
{
    public static int size = 10;
    public string[] lines;
    public int id;
    public int[][] edges = new int[][] { new int[4], new int[4] };
    // bit encoding of each edge by [0=clockwise, 1=countercw][rotation]
    // When two tiles are adjacent, opposite edges must have opposite orientations (clockwise / counterclockwise)
    //      or opposite faces (
    public int rotation = 0; // clockwise, 0-3
    public int face = 0;     // or 1

    public Tile(string input)
    {
        lines = input.Split("\r\n"); // First line is like "Tile ####:"
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

    public int PositionedSig(int edge)
    {
        int sig = 0;
        if (face == 0)
            sig = edges[0][(4 - rotation + edge) % 4];
        else
            sig = edges[1][(3 + rotation - edge) % 4];
        return sig;
    }

    // Returns the signature of the edge both in forward and reverse directions 
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

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"{id}/f{face}/r{rotation}:");
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                int x = col, y = row;
                if (face == 1)
                    (y, x) = (x, y);
                for (int r = 0; r < rotation; r++)
                    if (face == 0)
                        (x, y) = (y, size - x - 1);
                    else
                        (x, y) = (size - y - 1, x);
                sb.Append(lines[y][x]);
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}
