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
    }
}
