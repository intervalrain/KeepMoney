using KeepMoney.Domain.Common;

namespace KeepMoney.Domain.Users;

public class Subscription : Entity
{
    public SubscriptionType SubscriptionType { get; }
    public DateTime Due { get; } 

    private Subscription(Guid id, SubscriptionType subscriptionType, DateTime due)
        : base(id)
    {
        SubscriptionType = subscriptionType;
        Due = due;
    }

    public static Subscription Create(SubscriptionType subscriptionType, DateTime due)
    {
        return new Subscription(Guid.NewGuid(), subscriptionType, due);
    }

    public static readonly Subscription Canceled = Create(SubscriptionType.Canceled, DateTime.UtcNow);

    protected Subscription() : base(Guid.NewGuid()) { }
}

