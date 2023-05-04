using MinefieldKata.Models;

namespace MinefieldKata.Tests.Models
{
    public class PositionTests
    {
        [Fact]
        public void WhenInitializingAPosition_TheDefaultCoordinatesForXare0AndYare0()
        {
            var position = new Position();
            Assert.Equal(0, position.X);
            Assert.Equal(0, position.Y);
        }

        [Fact]
        public void WhenInitializingAPositionWithValues_TheCoordinatesAreSetToThoseValues()
        {
            var position = new Position(3, 5);
            Assert.Equal(3, position.X);
            Assert.Equal(5, position.Y);
        }
    }
}
