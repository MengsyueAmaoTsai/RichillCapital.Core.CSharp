using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.Enumerations;

public sealed class InstrumentType : Enumeration<InstrumentType>
{
    private static readonly InstrumentType Index = new(nameof(Index), 1);

    private InstrumentType(string name, int value)
        : base(name, value)
    {
    }
}