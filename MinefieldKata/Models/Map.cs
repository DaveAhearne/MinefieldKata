using MinefieldKata.Enums;

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

        public Map(IMinePositionGenerator minePositionGenerator, int width, int height, int numberOfMines)
        {
            Width = width;
            Height = height;
            grid = new SquareType[width, height];

            Initialize(minePositionGenerator, numberOfMines);
        }

        private void Initialize(IMinePositionGenerator minePositionGenerator, int numberOfMines)
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    grid[x, y] = SquareType.Safe;

            var mines = minePositionGenerator.Generate(this, numberOfMines);

            foreach (var mine in mines)
                Set(mine, SquareType.Mine);
        }

        public void Set(Position p, SquareType square)
        {
            grid[p.X, p.Y] = square;
        }

        public bool IsMine(Position p)
        {
            return grid[p.X, p.Y] == SquareType.Mine;
        }
    }
}
