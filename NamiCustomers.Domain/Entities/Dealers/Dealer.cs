using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Domain.Entities.Dealers;

[Auditable]
public class Dealer : IBaseEntity<int>
{
    public int Id { get; set; }

    public string? DealerNo { get; set; }

    public string DealerName { get; set; } = null!;

    public string? ManagerName { get; set; }

    public string? DealerAddress { get; set; }

    public string? DealerPrePhone { get; set; }

    public string? DealerPhone { get; set; }

    public string? DealerMobile { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? PostalCode { get; set; }

    public DealerType DealerType { get; set; } = DealerType.SalesAndService;

    public bool IsActive { get; set; } = true;

    public int CityId { get; set; }
    public City? City { get; set; }

    public string? NationalId { get; set; }

    public string? EconomicCode { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public int Sort { get; set; }

    public string? Description { get; set; }
}
