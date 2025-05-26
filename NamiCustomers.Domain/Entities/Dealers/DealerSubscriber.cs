using NamiCustomers.Domain.Entities.Subscribers;
using System.ComponentModel.DataAnnotations.Schema;

namespace NamiCustomers.Domain.Entities.Dealers;

public class DealerSubscriber : IBaseEntity<int>
{
    public int Id { get; set; }
    public virtual Dealer? Dealer { get; set; }
    public int? DealerId { get; set; }
    public virtual Subscriber? Subscriber { get; set; }
    public int? SubscriberId { get; set; }
    public int Code { get; set; }
    public bool IsAccept { get; set; }
}
