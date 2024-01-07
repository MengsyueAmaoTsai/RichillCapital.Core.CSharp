namespace RichillCapital.Core.SharedKernel;

public interface IAuditableEntity
{
    DateTimeOffset CreatedTime { get; }

    DateTimeOffset UpdatedTime { get; }
}