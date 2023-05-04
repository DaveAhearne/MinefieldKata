namespace MinefieldKata.Models
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        private char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position()
        {
            X = 0;
            Y = 0;
        }

        public override bool Equals(object? obj)
        {
            Position? position = obj as Position;

            if (position == null)
                return false;

            return position.Y == Y && position.X == X;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"{alpha[X]}{Y + 1}";
        }
    }
}
