using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;


public class CityResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string CityLocalizedName { get; set; }
    public string CityEnglishName { get; set; }
    public string SubCountryName { get; set; }
    public string CountryName { get; set; }
    public string SubCountryId { get; set; }
    public bool DepivedArea { get; set; }
    public object CityCategoryId { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}