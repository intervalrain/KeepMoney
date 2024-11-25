using KeepMoney.Domain.Users;

namespace KeepMoney.Api.Models;

public record UserResponse(string UserId, string FirstName, string LastName, string SubscriptionType)
{
    public static UserResponse ToDto(User user) => new(user.Id.ToString(), user.FirstName, user.LastName, user.Subscription.SubscriptionType.ToString());
}