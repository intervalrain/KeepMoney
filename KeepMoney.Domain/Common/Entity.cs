namespace KeepMoney.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }

    protected readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid id)
    {
        Id = id;
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var events = _domainEvents.ToList();
        _domainEvents.Clear();
        return events;
    }
}

