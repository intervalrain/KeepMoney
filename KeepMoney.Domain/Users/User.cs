using KeepMoney.Domain.Common;

namespace KeepMoney.Domain.Users;

public class User : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }
    public Role Role { get; private set; }
    public string PasswordHash { get; private set; }

    private User(Guid id, string firstName, string lastName, string email, SubscriptionType subscriptionType, Role role, string passwordHash)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        SubscriptionType = subscriptionType;
        Role = role;
        PasswordHash = passwordHash;
    }

    public static User Create(string firstName, string lastName, string email, SubscriptionType subscriptionType, Role role, string passwordHash)
    {
        return new User(Guid.NewGuid(), firstName, lastName, email, subscriptionType, role, passwordHash);
    }
}

