

namespace NamiCustomers.Domain.Entities.Subscribers;

[Auditable]
public class Appointment : IBaseEntity<int>
{
    public int Id { get; set; }
    public int? SubscriberId { get; set; }
    public DateTime ReservedDate { get; set; }
    public int DealerId { get; set; }
    public int ReservedNumber { get; set; }
    public string DealerName { get; set; }
    public Subscriber? Subscriber { get; set; }
}
