using MinefieldKata.Enums;
using MinefieldKata.Models;
using Moq;

namespace MinefieldKata.Tests.Models
{
    public class MapTests
    {
        [Theory]
        [InlineData(2,2)]
        [InlineData(5,5)]
        [InlineData(11,11)]
        public void WhenInitializingAMap_WithNoMines_AllSquaresAreSetToSafe(int width, int height)
        {
            var mockMineLayer = new Mock<IMineLayer>();

            var map = new Map(mockMineLayer.Object, width, height, 0);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    Assert.Equal(SquareType.Safe, map.grid[x, y]);
        }

        [Fact]
        public void WhenASquareIsMined_IsMineReturnsTrue()
        {
            var mockMineLayer = new Mock<IMineLayer>();

            var map = new Map(mockMineLayer.Object, 5, 5, 0);

            map.grid[1, 1] = SquareType.Mine;

            Assert.True(map.IsMine(new Position(1, 1)));
        }

        [Fact]
        public void WhenASquareIsNotMined_IsMineReturnsFalse()
        {
            var mockMineLayer = new Mock<IMineLayer>();

            var map = new Map(mockMineLayer.Object, 5, 5, 0);

            map.grid[1, 1] = SquareType.Safe;

            Assert.False(map.IsMine(new Position(1, 1)));
        }
    }
}
