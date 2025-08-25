 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos
{
    public class SubCountryResponse
    {
        public int Code { get; set; }
        public string UniqueId { get; set; }
        public string SubCountryLocalizedName { get; set; }
        public string SubCountryEnglishName { get; set; }
        public string CountryName { get; set; }
        public string ClientId { get; set; }
        public bool IsDirty { get; set; }
    }
}
