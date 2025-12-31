namespace Store.Data.Entities;

public class BaseEntity<T>
{
    public required T Id { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}