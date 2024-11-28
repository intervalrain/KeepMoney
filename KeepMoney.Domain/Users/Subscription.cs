using ErrorOr;

namespace KeepMoney.Domain.Users;

public class Subscription
{
    public SubscriptionType SubscriptionType { get; private set; }
    public DateTimeOffset ExpiryDate { get; private set; }
    public bool IsActive => DateTimeOffset.UtcNow <= ExpiryDate;
    public int RemainingDays => IsActive ? (int)(ExpiryDate - DateTimeOffset.UtcNow).TotalDays : 0;

    private Subscription(SubscriptionType subscriptionType, DateTimeOffset expiryDate)
    {
        SubscriptionType = subscriptionType;
        ExpiryDate = expiryDate;
    }

    public static Subscription Create(SubscriptionType subscriptionType, DateTimeOffset? expiryDate = null)
    {
        return new Subscription(subscriptionType, expiryDate ?? DateTimeOffset.UtcNow);
    }

    public static readonly Subscription Canceled = Create(SubscriptionType.Canceled);

    public ErrorOr<Success> Extend(int days)
    {
        if (days <= 0)
        {
            return Error.Validation("Subscription.Extend", "Days must be positive");
        }

        try
        {
            var baseDate = IsActive ? ExpiryDate : DateTimeOffset.UtcNow;
            ExpiryDate = baseDate.AddDays(days);
            SubscriptionType = SubscriptionType.Pro;
            return Result.Success;
        }
        catch (Exception ex)
        {
            return Error.Failure("Subscription.Extend", ex.Message);
        }
    }

    protected Subscription()
    {
    }
}