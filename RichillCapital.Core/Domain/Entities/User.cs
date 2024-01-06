using RichillCapital.Core.Domain.Events;
using RichillCapital.Core.Domain.ValueObjects;
using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Domain.Entities;

public sealed class User : Entity<UserId>
{
    private User(
        UserId id,
        Email email,
        Name name)
        : base(id)
    {
        Email = email;
        Name = name;
    }

    public Email Email { get; private set; }

    public Name Name { get; private set; }

    public static User Create(UserId id, Email email, Name name)
    {
        var user = new User(id, email, name);

        user.RegisterDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}