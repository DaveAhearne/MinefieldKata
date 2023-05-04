using MinefieldKata.Enums;

namespace MinefieldKata.Models
{
    public interface IGameStateHandler
    {
        GameState State { get; }
        void UpdateGameState();
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
    }
}
