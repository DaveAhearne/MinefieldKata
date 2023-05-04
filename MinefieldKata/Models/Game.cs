using MinefieldKata.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldKata.Models
{
    public interface IGame
    {
        Task Run(CancellationToken cancellationToken);
    }

    public class Game : IGame
    {
        private readonly IMap _map;
        private readonly IGameStateHandler _gameStateHandler;
        private readonly IUserInput _userInput;
        private readonly IConsoleDisplay _consoleDisplay;
        private readonly Player _player;

        public Game(IUserInput userInput, IConsoleDisplay consoleDisplay, IMap gameMap)
        {
            _map = gameMap;
            _userInput = userInput;
            _consoleDisplay = consoleDisplay;

            _player = new Player(_map);
            _gameStateHandler = new GameStateHandler(_map, _player, _consoleDisplay, _userInput);
        }

        public async Task Run(CancellationToken cancellationToken)
        {
            _consoleDisplay.SetDisplayLine("Minewalker: - Use the arrow keys to traverse the minefield & get to the rightmost side");
            _gameStateHandler.DisplayStatus();

            _userInput.OnPressed += (key) =>
            {
                _gameStateHandler.HandleInput(key);
                _gameStateHandler.UpdateGameState();
                _gameStateHandler.DisplayStatus();
            };

            await _userInput.Listen(cancellationToken);
        }
    }
}
