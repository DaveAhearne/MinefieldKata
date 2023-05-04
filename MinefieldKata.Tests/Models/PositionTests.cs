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


        [Fact]
        public void WhenComparingAPositionWithNull_ItIsNotEqual()
        {
            var position = new Position();
            Position p = null;

            Assert.False(position.Equals(p));
        }

        [Fact]
        public void WhenComparingAPositionWithADifferentType_ItIsNotEqual()
        {
            var position = new Position();
            DateTime d = DateTime.Now;

            Assert.False(position.Equals(d));
        }

        [Fact]
        public void WhenComparingTwoDifferentPositions_ItIsNotEqual()
        {
            var positionA = new Position(1, 1);
            var positionB = new Position(2, 3);

            Assert.False(positionA.Equals(positionB));
        }

        [Fact]
        public void WhenComparingTwoIdenticalPositions_ItIsEqual()
        {
            var positionA = new Position(2, 3);
            var positionB = new Position(2, 3);

            Assert.True(positionA.Equals(positionB));
        }

        [Fact]
        public void WhenComparingTwoIdenticalPositions_TheyHaveTheSameHashCode()
        {
            var positionA = new Position(2, 3);
            var positionB = new Position(2, 3);

            Assert.Equal(positionA.GetHashCode(), positionB.GetHashCode());
        }

        [Fact]
        public void WhenComparingTwoDifferentPositions_TheyHaveTheDifferntHashCodes()
        {
            var positionA = new Position(2, 3);
            var positionB = new Position(3, 4);

            Assert.NotEqual(positionA.GetHashCode(), positionB.GetHashCode());
        }
    }
}
