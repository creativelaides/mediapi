namespace MedApi.Domain.Primitives;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected AggregateRoot()
    {
        Id = Guid.NewGuid();
        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    protected void SetUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}