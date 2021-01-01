using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections;

public class Tiles
{
    List<Tile> tiles = new List<Tile>();
    // map from edge signature (not tile id!) to list of tuples with that sig: Tile, dir (clockwise=0, counter=1), rotation
    Dictionary<int, List<Tuple<Tile, int, int>>> tilesbyedge = new Dictionary<int, List<Tuple<Tile, int, int>>>();
    Tile[,] positionedTiles = null;
    char[,] patched = null;

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

        /*  Hypothesis: At this point, there will be exactly 4 tiles (corners) with exactly 2 edges containing
            signatures that are not contained on any other tiles, and those two edges will be adjacent. There will 
            similarly be 40 tiles each with a single edge with a unique signature. These are the tiles on the 
            perimeter. The other 100 tiles will will not have ANY unique signatures. Rather, ALL have edges 
            will share a signature with one other tile. So once we choose (arbitrarily) one of the correct corner 
            tiles, everything falls into place. (In the full input of 12x12 tiles.)

            Remember: Each edge has two signatures (keys) in tilesbyedge. One for each face. */
    }

    public long RoughWaters()
    {
        char[,] patched = Patch();
        string monster =
@"                  # 
#    ##    ##    ###
 #  #  #  #  #  #   ";
        OverlayWith0s(patched, monster);
        Debug.Print("After monsters found...");
        DumpPatch();
        long c = CountHashes(patched);
        return c;
    }

    private long CountHashes(char[,] patched)
    {
        long hashes = 0;
        int size = patched.GetLength(0);
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
                hashes += patched[x,y] == '#' ? 1 : 0;
        }
        return hashes;
    }

    private void OverlayWith0s(char[,] image, string monster)
    {
        // monster is multiline string with just # or space
        var mgrid = monster.Split("\r\n").Select(line => line.ToCharArray()).ToArray();
        int imagesize = image.GetLength(0);

        for (int face = 0; face < 2; face++)
        {
            if (face == 1) // flip along diagonal
            {
                var mgridnew = new char[mgrid[0].Length][];
                for (int y = 0; y < mgridnew.Length; y++) // y in target
                {
                    mgridnew[y] = new char[mgrid.Length];
                    for (int x = 0; x < mgrid.Length; x++) // x in target
                        mgridnew[y][x] = mgrid[x][y];
                }
                mgrid = mgridnew;
            }

            for (int rot = 0; rot < 4; rot++)
            {
                if (rot > 0) // rotate
                {
                    var mgridnew = new char[mgrid[0].Length][];
                    for (int y = 0; y < mgridnew.Length; y++) // y in target
                    {
                        mgridnew[y] = new char[mgrid.Length];
                        for (int x = 0; x < mgrid.Length; x++) // x in target
                            mgridnew[y][x] = mgrid[mgrid.Length - x - 1][y];
                    }
                    mgrid = mgridnew;
                }

                int maxxoff = imagesize - mgrid[0].Length;
                int maxyoff = imagesize - mgrid.Length;
                for (int yoff = 0; yoff <= maxyoff; yoff++)
                {
                    for (int xoff = 0; xoff <= maxxoff; xoff++)
                    {
                        StampIfMatch(image, mgrid, xoff, yoff);
                    }
                }
            }
        }
    }

    private void StampIfMatch(char[,] image, char[][] mgrid, int xoff, int yoff)
    {
        int imagesize = image.GetLength(0);
        for (int y = 0; y < mgrid.Length; y++)
            for (int x = 0; x < mgrid[0].Length; x++)
                if (mgrid[y][x] == '#' && image[xoff + x, yoff + y] == '.')
                    return;

        for (int y = 0; y < mgrid.Length; y++)
            for (int x = 0; x < mgrid[0].Length; x++)
                if (mgrid[y][x] == '#')
                    image[xoff + x, yoff + y] = 'O';
    }

    public char[,] Patch()
    {
        int dimTiles = (int)Math.Sqrt(tiles.Count);
        int dimSpaces = dimTiles * (Tile.size - 2);
        positionedTiles = new Tile[dimTiles, dimTiles];
        patched = new char[dimSpaces, dimSpaces];

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
        // startingCorner.face = 1 - startingCorner.face; // HACK: We're not actually trying the flip when matching monster
        while (!(singles.Contains(startingCorner.PositionedSig(0))
              && singles.Contains(startingCorner.PositionedSig(3))))
            startingCorner.rotation++;
        Paste(startingCorner, 0, 0);

        Debug.Print("Tile arrangement:");
        Debug.Write($"{startingCorner.id}/f{startingCorner.face}/r{startingCorner.rotation} ");


        for (int row = 0; row < dimTiles; row++)
        {
            if (row > 0)
            {
                Tile t = FindAndOrient(positionedTiles[0, row - 1], 2); // get the tile that fits on edge 1, and orient it accordingly
                Paste(t, 0, row);
                Debug.Write($"{t.id}/f{t.face}/r{t.rotation} ");
            }

            for (int col = 1; col < dimTiles; col++)
            {
                Tile t = FindAndOrient(positionedTiles[col - 1, row], 1); // get the tile that fits on edge 1, and orient it accordingly
                Paste(t, col, row);
                Debug.Write($"{t.id}/f{t.face}/r{t.rotation} ");
            }
            Debug.Print("");
        }

        return patched;
    }

    public void DumpPatch()
    {
        int dimTiles = (int)Math.Sqrt(tiles.Count);
        int dimSpaces = dimTiles * (Tile.size - 2);
        Debug.Print("Full patch:");
        for (int y = 0; y < dimSpaces; y++)
        {
            for (int x = 0; x < dimSpaces; x++)
                Debug.Write(patched[x, y]);
            Debug.Print("");
        }
    }

    private void Paste(Tile t, int col, int row)
    {
        int size = Tile.size - 2;
        positionedTiles[col, row] = t;
        for (int y = 1; y <= size; y++)
        {
            for (int x = 1; x <= size; x++)
                patched[col * size + x - 1, row * size + y - 1] = t.At(x, y);
        }
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
                sb.Append(At(col, row));
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public char At(int x, int y)
    {
        if (face == 1)
            (y, x) = (x, y);

        for (int r = 0; r < rotation; r++)
            if (face == 0)
                (x, y) = (y, size - x - 1);
            else
                (x, y) = (size - y - 1, x);

        return lines[y][x];
    }

}
