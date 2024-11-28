using ErrorOr;

using KeepMoney.Domain.Common;
using KeepMoney.Domain.Transactions;
using KeepMoney.Domain.Users.Events;

namespace KeepMoney.Domain.Users;

public class User : Entity
{
    private DateOnly _clock = DateOnly.MinValue;
    private int _dailyTokenUsed = 0;

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public Subscription Subscription { get; private set; }
    public List<string> Roles { get; private set; } = new();
    public string PasswordHash { get; private set; }

    private readonly List<Guid> _transactionIds = new();

    private User(Guid id, string firstName, string lastName, string email, Subscription subscription, List<string> roles, string passwordHash)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Subscription = subscription;
        Roles = roles;
        PasswordHash = passwordHash;
    }

    public static User Create(string firstName, string lastName, string email, Subscription subscription, List<string> roles, string passwordHash)
    {
        return new User(Guid.NewGuid(), firstName, lastName, email, subscription, roles, passwordHash);
    }

    public static User Empty => Create(string.Empty, string.Empty, string.Empty, Subscription.Canceled, new List<string>(), string.Empty);

    public ErrorOr<Success> AddTransaction(Transaction transaction)
    {
        if (Subscription?.SubscriptionType == SubscriptionType.Canceled)
        {
            return Error.NotFound("Subscription.NotFound", "Subscription not found");
        }

        var now = DateOnly.FromDateTime(DateTime.UtcNow);
        if (now != _clock)
        {
            _clock = now;
            _dailyTokenUsed = 0;
        }

        if (_dailyTokenUsed == 5 && Subscription?.SubscriptionType == SubscriptionType.Basic)
        {
            return Error.Unauthorized("AddTransaction.Unauthorized", "You have exceed daily limitation of adding transactions");
        }

        _dailyTokenUsed++;

        _transactionIds.Add(transaction.Id);
        _domainEvents.Add(new TransactionSetEvent(transaction));

        return Result.Success;
    }

    public ErrorOr<Success> Subscribe(int days)
    {
        return (Subscription == null) ? Error.NotFound("Subscription.NotFound", "Subscription not found") : Subscription.Extend(days);
    }

    protected User()
        : base(Guid.NewGuid())
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        Subscription = Subscription.Canceled;
        Roles = new List<string>();
        PasswordHash = string.Empty;
    }
}

