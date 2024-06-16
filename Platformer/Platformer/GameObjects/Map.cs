using System.Numerics;

namespace Platformer
{
    internal static class Map
    {
        public static readonly Size TileSize = new Size(64, 64);
        public static int MapWidth => MapAsArray.GetLength(0);
        public static int MapHeight => MapAsArray.GetLength(1);

        public static int[,] MapAsArray { get; private set; }

        public static Tile[,] MapAsArrayOfTiles { get; } = GetMapOfTiles();

        private static Tile[,] GetMapOfTiles()
        {
            var stringMap = Assets1.Map.Split('\n').Select(x => x.Split(' ').Select(x => int.Parse(x)).ToArray()).ToArray();
            MapAsArray = new int[stringMap.Length, stringMap[0].Length];
            for (int i = 0; i < stringMap.Length; i++)
            {
                for (int j = 0; j < stringMap[0].Length; j++)
                {
                    MapAsArray[i, j] = stringMap[i][j];
                }
            }

            var map = new Tile[MapHeight, MapWidth];
            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    map[i,j] = new Tile((TileType)MapAsArray[j,i], 
                        new Rectangle(i * TileSize.Width, j * TileSize.Height, TileSize.Width, TileSize.Height));
                }
            }
            return map;
        }

        public static Tile IsPlayerInValidPosition(float x, float y)
        {
            if (x < -TileSize.Width - View.GetInstance().Player.Size.Width / 2 
                || x > (MapHeight - 1) * TileSize.Width - 1
                || y < -TileSize.Height
                || y > (MapWidth - 1) * TileSize.Height - 1)
            {
                Model.GetInstance().GameOver();
                return new Tile(TileType.DirtInner, new Rectangle(0,0,0,0));
            }

            return MapAsArrayOfTiles[(int)x / TileSize.Width + 1, (int)y / TileSize.Height + 1];
        }

        public static Tile GetTileByCoordinates(float x, float y)
        {
            var x1 = (int)x;
            var y1 = (int)y;
            return MapAsArrayOfTiles[x1 / TileSize.Width, y1 / TileSize.Height];
        }

        public static IEnumerable<Tile> GetNeighborsByCoordinates(float x, float y)
        {
            var x1 = (int)x / TileSize.Width;
            var y1 = (int)y / TileSize.Height;

            return GetNeighborsByIndexes(x1, y1);
        }

        public static IEnumerable<Tile> GetNeighborsByIndexes(int x, int y)
        {
            if (IsInBounds(x + 1, y)) yield return MapAsArrayOfTiles[x + 1, y];
            if (IsInBounds(x - 1, y)) yield return MapAsArrayOfTiles[x - 1, y];
            if (IsInBounds(x, y + 1)) yield return MapAsArrayOfTiles[x, y + 1];
            if (IsInBounds(x, y-1)) yield return MapAsArrayOfTiles[x, y - 1];
            if (IsInBounds(x + 1, y + 1)) yield return MapAsArrayOfTiles[x + 1, y + 1];
            if (IsInBounds(x - 1, y - 1)) yield return MapAsArrayOfTiles[x - 1, y - 1];
            if (IsInBounds(x + 1, y + 1)) yield return MapAsArrayOfTiles[x + 1, y + 1];
            if (IsInBounds(x - 1, y - 1)) yield return MapAsArrayOfTiles[x - 1, y - 1];
        }

        public static bool IsInBounds(float x, float y)
        {
            return x < MapHeight && x > 0 && y < MapWidth && y > 0;
        }

        public static void DestroyTile(int x, int y)
        {
            MapAsArrayOfTiles[x, y] = new Tile(TileType.Nothing, new Rectangle(x, y, TileSize.Width, TileSize.Height));
            View.GetInstance().UpdateBitmap(x,y);
        }
    }
}
