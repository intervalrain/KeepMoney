using KeepMoney.Domain.Common;
using KeepMoney.Domain.Users;

namespace KeepMoney.Domain.Transactions;

public class Transaction : Entity
{
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Transaction(Guid id, DateTime date, string category, decimal amount, string note, Guid userId, User user) : base(id)
    {
        Date = date;
        Category = category;
        Amount = amount;
        Note = note;
        UserId = userId;
        User = user;
    }

    public static Transaction Create(DateTime date, string category, decimal amount, string note, Guid userId, User user)
    {
        return new Transaction(Guid.NewGuid(), date, category, amount, note, userId, user);
    }

    protected Transaction() : base(Guid.NewGuid()) { }
}

