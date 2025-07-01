namespace NamiCustomers.Domain.Entities;

[Auditable]
public class City
{
    public int Id { get; set; }
    public string Title { get; set; }
}
