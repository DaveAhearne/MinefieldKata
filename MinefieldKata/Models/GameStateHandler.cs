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
        private readonly IPlayer _player;
        public GameState State { get; private set; } = GameState.Playing;

        public GameStateHandler(IPlayer player)
        {
            _player = player;
        }

        public void UpdateGameState()
        {
            if (_player.Lives <= 0)
            {
                State = GameState.Lost;
                return;
            }
        }
    }
}
