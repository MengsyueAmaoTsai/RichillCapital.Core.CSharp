using RichillCapital.Core.SharedKernel;
using RichillCapital.Extensions.Primitives;

namespace RichillCapital.Core.Features.Bots.Delete;

public sealed record DeleteBotCommand(string BotId) : ICommand<Result>;