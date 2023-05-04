namespace MinefieldKata.Models
{
    public interface IPlayer
    {
        int Lives { get; }
        void Damage();
    }

    public class Player : IPlayer
    {
        public int Lives { get; private set; } = 3;

        public void Damage()
        {
            Lives--;
        }
    }
}
