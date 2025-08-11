using NamiCustomers.Infrastucture.ExternalServices.Nami.Dtos;
using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.Infrastucture.ExternalServices.Nami;

public interface INamiKhodroService
{
    Task<NamiNewsResponse[]> GetNamiNews();
}
public class NamiKhodroService : INamiKhodroService
{
    public async Task<NamiNewsResponse[]> GetNamiNews()
    {
        return await RestUtility.GetData<NamiNewsResponse[]>(string.Empty, Infrastucture.Properties.NamiKhodroResource.NamiNews, null);
    }

}


