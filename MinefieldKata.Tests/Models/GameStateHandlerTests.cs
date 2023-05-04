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

        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        public void WhenHandlingInput_IfTheUserInADirectionAndTheMoveIsValid_ThePlayerIsMovedAndTheMoveCountIsIncremented(ConsoleKey key, Direction direction)
        {
            mockPlayer.Setup(x => x.Move(It.IsAny<Direction>())).Returns(true);

            gameStateHandler.HandleInput(key);

            mockPlayer.Verify(x => x.Move(direction), Times.Once);
            Assert.Equal(1, gameStateHandler.MoveCount);
        }

        [Theory]
        [InlineData(ConsoleKey.UpArrow, Direction.Up)]
        [InlineData(ConsoleKey.DownArrow, Direction.Down)]
        [InlineData(ConsoleKey.LeftArrow, Direction.Left)]
        [InlineData(ConsoleKey.RightArrow, Direction.Right)]
        public void WhenHandlingInput_IfTheUserInADirectionAndTheMoveIsNotValid_TheMoveCountIsNotIncremented(ConsoleKey key, Direction direction)
        {
            mockPlayer.Setup(x => x.Move(It.IsAny<Direction>())).Returns(false);

            gameStateHandler.HandleInput(key);

            mockPlayer.Verify(x => x.Move(direction), Times.Once);
            Assert.Equal(0, gameStateHandler.MoveCount);
        }

        [Theory]
        [InlineData(ConsoleKey.Escape)]
        [InlineData(ConsoleKey.Backspace)]
        [InlineData(ConsoleKey.A)]
        [InlineData(ConsoleKey.NumPad8)]
        public void WhenHandlingInput_AndHandleInputIsCalledWithAnyNonDirectionalKey_MoveIsNotCalledAndTheMovesAreNotIncremented(ConsoleKey key)
        {
            mockPlayer.Setup(x => x.Move(It.IsAny<Direction>())).Returns(false);

            gameStateHandler.HandleInput(key);

            mockPlayer.Verify(x => x.Move(It.IsAny<Direction>()), Times.Never);
            Assert.Equal(0, gameStateHandler.MoveCount);
        }
    }
}
