namespace RichillCapital.Core.Features.Bots;

public sealed record BotDto(
    string Id,
    string Name,
    string Description,
    string Platform);