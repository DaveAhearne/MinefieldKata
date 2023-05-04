using MinefieldKata.Enums;
using MinefieldKata.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldKata.Tests.Models
{
	public class GameStateHandlerTests
	{
        private Mock<IPlayer> mockPlayer;

        private GameStateHandler gameStateHandler;

        public GameStateHandlerTests()
        {
            mockPlayer = new Mock<IPlayer>();

            gameStateHandler = new GameStateHandler(mockPlayer.Object);
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
    }
}
