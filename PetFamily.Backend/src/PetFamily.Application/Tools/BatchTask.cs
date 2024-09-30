namespace PetFamily.Application.Tools
{
    public class BatchTask<T>
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly List<Func<Task<T>>> _funcs;
        private readonly List<Action<T>> _onExecutedAction;

        public BatchTask(List<Func<Task<T>>> funcs, int initialCount)
        {
            _funcs = funcs;
            _semaphore = new SemaphoreSlim(initialCount);
            _onExecutedAction = new();
        }

        public BatchTask<T> OnExecuted(Action<T> action)
        {
            _onExecutedAction.Add(action);
            return this;
        }

        public async Task Run(CancellationToken cancellationToken = default)
        {
            List<Task> tasks = new();

            foreach (var func in _funcs)
            {
                var task = Task.Run(async () =>
                {
                    await _semaphore.WaitAsync();
                    if (cancellationToken.IsCancellationRequested)
                        return;

                    try
                    {
                        var result = await func();
                        foreach (var action in _onExecutedAction)
                        {
                            action(result);
                        }
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                });

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);
        }
    }
}
