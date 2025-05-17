

namespace NamiCustomers.Domain.Entities
{
    public class IBaseEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
