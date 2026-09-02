using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Domain.Entities.Dealers;

public enum DealerType
{
    [Display(Name = "فروش")]
    Sales = 1,

    [Display(Name = "خدمات پس از فروش")]
    AfterSales = 2,

    [Display(Name = "فروش و خدمات")]
    SalesAndService = 3
}
