using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.Enumerations;

public sealed class Exchange : Enumeration<Exchange>
{
    private static readonly Exchange TWSE = new(nameof(TWSE), 1);

    private Exchange(string name, int value)
        : base(name, value)
    {
    }
}