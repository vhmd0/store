namespace Store.Data.Entities;

public class BaseEntity<T>
{
    public T Id { get; set; } = default!;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}