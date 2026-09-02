namespace NamiCustomers.Domain.Entities;

[Auditable]
public class Province : IBaseEntity<int>
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Code { get; set; }
    public ICollection<City>? Cities { get; set; }
}
