namespace NamiCustomers.Domain.Entities;

[Auditable]
public class City
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int ProvinceId { get; set; }
    public Province? Province { get; set; }
}
