using MinefieldKata.Enums;
using MinefieldKata.Models;
using MinefieldKata.Utilities;
using Moq;

namespace MinefieldKata.Tests.Models
{
    public class GameStateHandlerTests
	{
        private Mock<IPlayer> mockPlayer;
        private Mock<IMap> mockMap;
        private Mock<IConsoleDisplay> mockConsoleDisplay;
        private Mock<IUserInput> mockUserInput;

        private GameStateHandler gameStateHandler;

        public GameStateHandlerTests()
        {
            mockPlayer = new Mock<IPlayer>();
            mockMap = new Mock<IMap>();
            mockConsoleDisplay = new Mock<IConsoleDisplay>();
            mockUserInput = new Mock<IUserInput>();

            gameStateHandler = new GameStateHandler(mockMap.Object, mockPlayer.Object, mockConsoleDisplay.Object, mockUserInput.Object);
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


        [Fact]
        public void WhenCallingDisplayStatus_AndTheStateIsPlaying_TheCurrentDisplayLineIsUpdatedWithTheScoreAndPosition()
        {
            mockPlayer.Setup(x => x.IsThroughTheMinefield()).Returns(false);
            mockPlayer.SetupGet(x => x.Lives).Returns(3);
            mockPlayer.SetupGet(x => x.Position).Returns(new Position(0, 0));

            gameStateHandler.DisplayStatus();
            mockConsoleDisplay.Verify(x => x.UpdateDisplayLine($"Number of Moves: 0 \t Player Position: A1 \t Number of Lives: 3"), Times.Once);
        }

        [Fact]
        public void WhenCallingDisplayStatus_AndTheStateIsWon_TheCurrentDisplayLineIsSetToTheWinMessageAndScoreAndInputListenersAreRemoved()
        {
            mockPlayer.Setup(x => x.IsThroughTheMinefield()).Returns(true);
            mockPlayer.SetupGet(x => x.Lives).Returns(3);

            gameStateHandler.UpdateGameState();
            gameStateHandler.DisplayStatus();

            mockConsoleDisplay.Verify(x => x.SetDisplayLine($"You win! :) - Your score was 0"), Times.Once);
            mockUserInput.Verify(x => x.RemoveInputListeners(), Times.Once);
        }

        [Fact]
        public void WhenCallingDisplayStatus_AndTheStateIsLost_TheCurrentDisplayLineIsSetToTheLoseMessageAndInputListenersAreRemoved()
        {
            mockPlayer.SetupGet(x => x.Lives).Returns(0);

            gameStateHandler.UpdateGameState();
            gameStateHandler.DisplayStatus();

            mockConsoleDisplay.Verify(x => x.SetDisplayLine($"You lose! :("), Times.Once);
            mockUserInput.Verify(x => x.RemoveInputListeners(), Times.Once);
        }
    }
}
