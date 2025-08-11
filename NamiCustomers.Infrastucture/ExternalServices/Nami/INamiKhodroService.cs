using NamiCustomers.Infrastucture.ExternalServices.Nami.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
using NamiCustomers.Infrastucture.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.ExternalServices.Nami;

public interface INamiKhodroService
{
    Task<NamiNewsResponse[]> GetNamiNews();
}
public class NamiKhodroService : INamiKhodroService
{
    public async Task<NamiNewsResponse[]> GetNamiNews()
    {
        return await RestUtility. GetData<NamiNewsResponse[]>(string.Empty,Infrastucture.Properties.NamiKhodroResource.NamiNews, null);
    }
    
}


