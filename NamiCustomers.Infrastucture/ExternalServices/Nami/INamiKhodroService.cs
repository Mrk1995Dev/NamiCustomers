using NamiCustomers.Infrastucture.ExternalServices.Nami.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;
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
        return await GetData<NamiNewsResponse[]>(Infrastucture.Properties.NamiKhodroResource.NamiNews, null);
    }
    private async Task<T> GetData<T>(string apiAddress, dynamic queryString)
    {
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        string content = await response.Content.ReadAsStringAsync();

        return await Task.FromResult(JsonSerializer.Deserialize<T>(content));
    }
}


