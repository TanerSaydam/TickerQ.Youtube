using TickerQ.Utilities.Base;
using TickerQ.Utilities.Interfaces;

namespace TickerQ.WebAPI;

public sealed class MyBackground : ITickerFunction
{
    public async Task ExecuteAsync(TickerFunctionContext context, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("I am working on ExecuteAsync...");
        await Task.CompletedTask;
    }

    [TickerFunction("MyWork1")]
    public async Task MyWork1()
    {
        Console.WriteLine("I am working on MyWork1...");
        await Task.CompletedTask;
    }
}