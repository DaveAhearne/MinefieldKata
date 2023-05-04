namespace MinefieldKata.Models
{
    public interface IPlayer
    {
        int Lives { get; }
        void Damage();

    }

    public class Player : IPlayer
    {
        public Position Position { get; set; } = new Position(0, 0);
        public int Lives { get; private set; } = 3;

        public void Damage()
        {
            Lives--;
        }
    }
}
