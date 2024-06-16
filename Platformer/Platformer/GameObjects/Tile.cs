namespace Platformer
{
    internal class Tile
    {
        public readonly TileType Type;
        public readonly Rectangle Rectangle;

        public Tile(TileType type, Rectangle rectangle)
        {
            Type = type;
            Rectangle = rectangle;
        }
    }
}
