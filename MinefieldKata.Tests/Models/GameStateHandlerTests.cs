using MinefieldKata.Enums;
using MinefieldKata.Models;
using Moq;

namespace MinefieldKata.Tests.Models
{
    public class GameStateHandlerTests
	{
        private Mock<IPlayer> mockPlayer;
        private Mock<IMap> mockMap;

        private GameStateHandler gameStateHandler;

        public GameStateHandlerTests()
        {
            mockPlayer = new Mock<IPlayer>();
            mockMap = new Mock<IMap>();

            gameStateHandler = new GameStateHandler(mockMap.Object, mockPlayer.Object);
        }

        [Fact]
        public void WhenThePlayerHasMoreThanZeroLives_TheGameStateIsLeftAsPlaying()
        {
            mockPlayer.SetupGet(x => x.Lives).Returns(2);
            gameStateHandler.UpdateGameState();

            Assert.Equal(GameState.Playing, gameStateHandler.State);
        }

        [Fact]
        public void WhenThePlayerHasZeroLives_TheGameStateIsSetToLost()
        {
            mockPlayer.SetupGet(x => x.Lives).Returns(0);
            gameStateHandler.UpdateGameState();

            Assert.Equal(GameState.Lost, gameStateHandler.State);
        }

        [Fact]
        public void WhenUpdatingTheGameState_IfThePlayerHasMadeItThroughTheMinefield_ThenTheStateIsSetToWon()
        {
            mockPlayer.SetupGet(x => x.Lives).Returns(3);
            mockPlayer.Setup(x => x.IsThroughTheMinefield()).Returns(true);

            gameStateHandler.UpdateGameState();

            Assert.Equal(GameState.Won, gameStateHandler.State);
        }

        [Fact]
        public void WhenUpdatingTheGameState_IfThePlayerIsStoodOnAMine_DamagePlayerIsCalled()
        {
            mockMap.Setup(x => x.IsStandingOnMine(mockPlayer.Object)).Returns(true);

            gameStateHandler.UpdateGameState();

            mockPlayer.Verify(x => x.Damage(), Times.Once);
        }
    }
}
