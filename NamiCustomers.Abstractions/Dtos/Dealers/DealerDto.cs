using NamiCustomers.Abstractions.Dtos.Subscribers;

namespace NamiCustomers.Abstractions.Dtos.Dealers;

public class DealerDto
{
    public int Id { get; set; }
    public string? DealerNo { get; set; }
    public string? DealerName { get; set; }
    public string? DealerAddress { get; set; }
    public string? DealerPhone { get; set; }
    public string? DealerprePhone { get; set; }
    public string? DealerType { get; set; }
    public virtual CityDto? City { get; set; }
    public int? CityId { get; set; }
    public string? Email { get; set; }
    public string? CityName { get; set; }
    public string? DealerMobile { get; set; }
    public int? Sort { get; set; }
}
