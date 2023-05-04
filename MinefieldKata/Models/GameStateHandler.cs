using MinefieldKata.Enums;

namespace MinefieldKata.Models
{
    public interface IGameStateHandler
    {
        GameState State { get; }
        void UpdateGameState();
        void HandleInput(ConsoleKey key);
    }

    public class GameStateHandler : IGameStateHandler
    {
        private readonly IMap _map;
        private readonly IPlayer _player;
        public GameState State { get; private set; } = GameState.Playing;

        public GameStateHandler(IMap map, IPlayer player)
        {
            _map = map;
            _player = player;
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

            _player.Move(direction);
        }
    }
}
