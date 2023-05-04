using MinefieldKata.Enums;
using System.Numerics;

namespace MinefieldKata.Models
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }

        bool IsMine(Position p);
    }

    public class Map : IMap
    {
        public SquareType[,] grid;
        public int Width { get; }
        public int Height { get; }

        public Map(IMineLayer mineLayer, int width, int height, int numberOfMines)
        {
            Width = width;
            Height = height;

            grid = new SquareType[width, height];

            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    grid[x, y] = SquareType.Safe;
        }

        public bool IsMine(Position p)
        {
            return grid[p.X, p.Y] == SquareType.Mine;
        }
    }
}
