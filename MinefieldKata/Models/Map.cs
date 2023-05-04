using MinefieldKata.Enums;

namespace MinefieldKata.Models
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }
    }

    public class Map : IMap
    {
        public SquareType[,] grid;
        public int Width { get; }
        public int Height { get; }

        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            grid = new SquareType[width, height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    grid[x, y] = SquareType.Safe;
        }
    }
}
