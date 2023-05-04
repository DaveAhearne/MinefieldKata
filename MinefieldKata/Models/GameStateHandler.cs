using MinefieldKata.Enums;
using MinefieldKata.Utilities;

namespace MinefieldKata.Models
{
    public interface IGameStateHandler
    {
        GameState State { get; }
        void UpdateGameState();
        void HandleInput(ConsoleKey key);
        void DisplayStatus();
    }

    public class GameStateHandler : IGameStateHandler
    {
        private readonly IMap _map;
        private readonly IPlayer _player;
        private readonly IConsoleDisplay _consoleDisplay;
        private readonly IUserInput _userInput;

        public int MoveCount { get; private set; } = 0;
        public GameState State { get; private set; } = GameState.Playing;

        public GameStateHandler(IMap map, IPlayer player, IConsoleDisplay consoleDisplay, IUserInput userInput)
        {
            _map = map;
            _player = player;
            _consoleDisplay = consoleDisplay;
            _userInput = userInput;
        }

        public void UpdateGameState()
        {
            if (_map.IsStandingOnMine(_player))
                _player.Damage();

            if (_player.Lives <= 0)
            {
                State = GameState.Lost;
                return;
            }

            if (_player.IsThroughTheMinefield())
            {
                State = GameState.Won;
                return;
            }
        }

        public void HandleInput(ConsoleKey key)
        {
            var direction = key switch
            {
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                _ => Direction.Unknown
            };

            if (direction == Direction.Unknown)
                return;

            if (_player.Move(direction))
                MoveCount++;
        }

        public void DisplayStatus()
        {
            switch (State)
            {
                case GameState.Playing:
                    _consoleDisplay.UpdateDisplayLine($"Number of Moves: {MoveCount} \t Player Position: {_player.Position} \t Number of Lives: {_player.Lives}");
                    break;
                case GameState.Won:
                    _userInput.RemoveInputListeners();
                    _consoleDisplay.SetDisplayLine($"You win! :) - Your score was {MoveCount}");
                    break;
                case GameState.Lost:
                    _userInput.RemoveInputListeners();
                    _consoleDisplay.SetDisplayLine("You lose! :(");
                    break;
            }
        }
    }
}
