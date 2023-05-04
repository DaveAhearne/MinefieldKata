namespace MinefieldKata.Utilities
{
    public interface IUserInput
    {
        Action<ConsoleKey> OnPressed { get; set; }

        Task Listen(CancellationToken cancellationToken);
        void RemoveInputListeners();
    }

    public class UserInput : IUserInput
    {
        public Action<ConsoleKey> OnPressed { get; set; } = (key) => { };

        public void RemoveInputListeners()
        {
            OnPressed = (key) => { };
        }

        public async Task Listen(CancellationToken cancellationToken)
        {
            await Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    while (!Console.KeyAvailable)
                    {
                        await Task.Delay(50, cancellationToken);
                    }

                    OnPressed(Console.ReadKey(true).Key);
                }
            }, cancellationToken);
        }
    }
}
