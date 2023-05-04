using MinefieldKata.Utilities;

namespace MinefieldKata.Models
{
    public interface IMinePositionGenerator
    {
        List<Position> Generate(IMap map, int numberOfMines);
    }

    public class MinePositionGenerator : IMinePositionGenerator
    {
        private readonly IRandomProvider _rngProvider;

        public MinePositionGenerator(IRandomProvider rngProvider)
        {
            _rngProvider = rngProvider;
        }

        public List<Position> Generate(IMap map, int numberOfMines)
        {
            int minesRemaining = numberOfMines;
            List<Position> mineLocations = new List<Position>();

            while (minesRemaining > 0)
            {
                var mineX = _rngProvider.Next(0, map.Width);
                var mineY = _rngProvider.Next(0, map.Height);

                if (map.IsMine(new Position(mineX, mineY)))
                    continue;

                mineLocations.Add(new Position(mineX, mineY));
                minesRemaining--;
            }

            return mineLocations;
        }
    }
}
