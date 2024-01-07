using RichillCapital.Core.Domain.Enumerations;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class Instrument : Entity<Symbol>
{
    private Instrument(
        Symbol id,
        Description description,
        InstrumentType type,
        Exchange exchange)
        : base(id)
    {
        Description = description;
        Type = type;
        Exchange = exchange;
    }

    public Symbol Symbol => Id;

    public Description Description { get; private set; }

    public InstrumentType Type { get; private set; }

    public Exchange Exchange { get; private set; }

    public static Instrument Create(
        Symbol symbol,
        Description description,
        InstrumentType type,
        Exchange exchange)
    {
        var instrument = new Instrument(
            symbol,
            description,
            type,
            exchange);

        return instrument;
    }
}