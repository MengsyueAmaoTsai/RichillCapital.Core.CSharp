using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Domain.Errors;

public static class BotErrors
{
    public static Error Duplicated(BotId id) =>
        Error.Conflict($"Duplicated bot id. {id.Value}");

    public static Error Duplicated(Name name) =>
        Error.Conflict($"Duplicated bot name. {name.Value}");
}