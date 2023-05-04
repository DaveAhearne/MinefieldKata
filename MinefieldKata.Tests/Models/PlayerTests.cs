using MinefieldKata.Enums;
using MinefieldKata.Models;
using Moq;

namespace MinefieldKata.Tests.Models
{
    public class PlayerTests
    {
        private Mock<IMap> mockMap;
        private Player player;

        public PlayerTests()
        {
            mockMap = new Mock<IMap>();
            player = new Player(mockMap.Object);
        }

        [Fact]
        public void WhenCreatingANewPlayer_TheyIntiallyHave3Lives()
        {
            Assert.Equal(3, player.Lives);
        }

        [Fact]
        public void WhenThePlayerIsDamaged_TheyLoseALife()
        {
            player.Damage();
            Assert.Equal(2, player.Lives);
        }

        [Fact]
        public void PlayerHasAnInitialPositionofZeroZero()
        {
            Assert.Equal(new Position(0, 0), player.Position);
        }

        [Theory]
        [InlineData(Direction.Up, 1,2)]
        [InlineData(Direction.Down, 1,0)]
        [InlineData(Direction.Left, 0,1)]
        [InlineData(Direction.Right, 2,1)]
        public void WhenThePlayerMovesInADirection_TheirPositionIsUpdated(Direction direction, int x, int y)
        {
            mockMap.SetupGet(x => x.Height).Returns(5);
            mockMap.SetupGet(x => x.Width).Returns(5);

            player.SetPosition(new Position(1, 1));

            player.Move(direction);
            Assert.Equal(new Position(x,y),player.Position);
        }

        [Fact]
        public void WhenThePlayerMovesUp_AndTheMoveIsNotOnTheBoard_TheCoordinatesOfThePlayerAreUnchanged()
        {
            mockMap.SetupGet(x => x.Height).Returns(3);
            mockMap.SetupGet(x => x.Width).Returns(3);

            player.SetPosition(new Position(1, 2));

            player.Move(Direction.Up);
            Assert.Equal(2, player.Position.Y);
        }

        [Fact]
        public void WhenThePlayerMovesDown_AndTheMoveIsNegative_TheCoordinatesOfThePlayerAreUnchanged()
        {
            mockMap.SetupGet(x => x.Height).Returns(3);
            mockMap.SetupGet(x => x.Width).Returns(3);

            player = new Player(mockMap.Object);
            player.SetPosition(new Position(0, 0));

            player.Move(Direction.Down);
            Assert.Equal(0, player.Position.Y);
        }

        [Fact]
        public void WhenThePlayerMovesLeft_AndTheMoveIsNegative_TheCoordinatesOfThePlayerAreUnchanged()
        {
            mockMap.SetupGet(x => x.Height).Returns(3);
            mockMap.SetupGet(x => x.Width).Returns(3);

            player = new Player(mockMap.Object);
            player.SetPosition(new Position(0, 0));

            player.Move(Direction.Left);

            Assert.Equal(0, player.Position.X);
        }

        [Fact]
        public void WhenThePlayerMovesRight_AndTheMoveIsNotOnTheBoard_TheCoordinatesOfThePlayerAreUnchanged()
        {
            mockMap.SetupGet(x => x.Height).Returns(3);
            mockMap.SetupGet(x => x.Width).Returns(3);

            player = new Player(mockMap.Object);
            player.SetPosition(new Position(2, 0));

            player.Move(Enums.Direction.Right);

            Assert.Equal(2, player.Position.X);
        }
    }
}
