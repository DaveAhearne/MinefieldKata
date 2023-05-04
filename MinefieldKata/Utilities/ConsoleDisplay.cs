namespace MinefieldKata.Utilities
{
    public interface IConsoleDisplay
    {
        void SetDisplayLine(string text);
        void UpdateDisplayLine(string text);
    }

    public class ConsoleDisplay : IConsoleDisplay
    {
        public void SetDisplayLine(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
        }

        public void UpdateDisplayLine(string text)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(text);
        }
    }
}
