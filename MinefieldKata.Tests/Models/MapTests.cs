using MinefieldKata.Enums;
using MinefieldKata.Models;

namespace MinefieldKata.Tests.Models
{
    public class MapTests
    {
        [Theory]
        [InlineData(2,2)]
        [InlineData(5,5)]
        [InlineData(11,11)]
        public void WhenInitializingAMap_AllSquaresAreSetToSafe(int width, int height)
        {
            var map = new Map(width, height);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Assert.Equal(SquareType.Safe, map.grid[x, y]);
        }
    }
}
