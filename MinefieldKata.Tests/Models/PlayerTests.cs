using MinefieldKata.Enums;
using MinefieldKata.Models;

namespace MinefieldKata.Tests.Models
{
    public class PlayerTests
    {
        private Player player;

        public PlayerTests()
        {
            player = new Player();
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
        [InlineData(Direction.Up, 0,1)]
        [InlineData(Direction.Down, 0,-1)]
        [InlineData(Direction.Left, -1,0)]
        [InlineData(Direction.Right, 1,0)]
        public void WhenThePlayerMovesInADirection_TheirPositionIsUpdated(Direction direction, int x, int y)
        {
            player.Move(direction);
            Assert.Equal(new Position(x,y),player.Position);
        }
    }
}
