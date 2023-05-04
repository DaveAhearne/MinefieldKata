using MinefieldKata.Enums;
using System.Security.Principal;

namespace MinefieldKata.Models
{
    public interface IPlayer
    {
        int Lives { get; }
        void Damage();
        bool Move(Direction direction);
        void SetPosition(Position position);
    }

    public class Player : IPlayer
    {
        public Position Position { get; set; } = new Position(0, 0);
        public int Lives { get; private set; } = 3;

        private readonly IMap _map;

        public Player(IMap map)
        {
            _map = map;
        }

        public void Damage()
        {
            Lives--;
        }

        public bool Move(Direction direction)
        {
            var newPosition =  direction switch
            {
                Direction.Up => new Position(Position.X, Position.Y + 1),
                Direction.Down => new Position(Position.X, Position.Y - 1),
                Direction.Left => new Position(Position.X - 1, Position.Y),
                Direction.Right => new Position(Position.X + 1, Position.Y),
                _ => new Position(Position.X, Position.Y),
            };

            if (IsValidMove(newPosition))
            {
                Position = newPosition;
                return true;
            }

            return false;
        }

        private bool IsValidMove(Position position)
        {
            if (position.X < 0 || position.X >= _map.Width)
                return false;

            if (position.Y < 0 || position.Y >= _map.Height)
                return false;

            return true;
        }

        public void SetPosition(Position position)
        {
            Position = position;
        }
    }
}
