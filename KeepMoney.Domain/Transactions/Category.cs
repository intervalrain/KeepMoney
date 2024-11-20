using KeepMoney.Domain.Common;
using KeepMoney.Domain.Users;

namespace KeepMoney.Domain.Transactions;

public class Category : Entity
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Category(Guid id, string name, Guid userId, User user) : base(id)
    {
        Name = name;
        UserId = userId;
        User = user;
    }

    public static Category Create(string name, Guid userId, User user)
    {
        return new Category(Guid.NewGuid(), name, userId, user);
    }

    protected Category() : base(Guid.NewGuid()) { }
}

