using MinefieldKata.Enums;

namespace MinefieldKata.Models
{
    public interface IPlayer
    {
        int Lives { get; }
        void Damage();
        void Move(Direction direction);
    }

    public class Player : IPlayer
    {
        public Position Position { get; set; } = new Position(0, 0);
        public int Lives { get; private set; } = 3;

        public void Damage()
        {
            Lives--;
        }

        public void Move(Direction direction)
        {
            Position = direction switch
            {
                Direction.Up => new Position(Position.X, Position.Y + 1),
                Direction.Down => new Position(Position.X, Position.Y - 1),
                Direction.Left => new Position(Position.X - 1, Position.Y),
                Direction.Right => new Position(Position.X + 1, Position.Y),
                _ => new Position(Position.X, Position.Y),
            };
        }
    }
}
