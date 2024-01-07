using FluentValidation;

using RichillCapital.Core.Domain.ValueObjects;

namespace RichillCapital.Core.Features.Bots.Create;

internal sealed class CreateBotCommandValidator :
    AbstractValidator<CreateBotCommand>
{
    public CreateBotCommandValidator()
    {
        RuleFor(command => command.BotId)
            .NotNull()
            .NotEmpty()
            .MaximumLength(BotId.MaxLength);

        RuleFor(command => command.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(command => command.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Description.MaxLength);
    }
}