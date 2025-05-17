using NamiCustomers.Infrastucture.ExternalServices.IranFava.Dtos;
using NamiCustomers.Infrastucture.Properties;



namespace NamiCustomers.Infrastucture.ExternalServices.IranFava
{
    public interface IFavaServices
    {
        Task<CreateSignResult> FavaSignOperation(string pdfFileName, string bs64String);
        Task<CustomersAndCarsResonse> GetCustomersAndCars(int saleId, int pageNo);
        Task<GetTokenResult> GetToken();
    }
    public class FavaServices : IFavaServices
    {
        public async Task<CreateSignResult> FavaSignOperation(string pdfFileName, string bs64String)
        {
            FavaRequest favaRequest = new FavaRequest
            {
                title = "قرارداد جانبازان",
                description = "توضیحات",
                documentData = bs64String,
                documentName = pdfFileName,
                recipientUsername = "0082425639",
                documentParameter = Newtonsoft.Json.JsonConvert.SerializeObject(new documentParameter
                {
                    dataFields = new List<DataField>
                {
                    new DataField{
                    dataFieldType = "SIGNATURE",
                    tag = "2702031574200",
                    pageNumber = 4,
                    topRel =0.0,
                    leftRel = 0.0,
                    heightRel = 0.0,
                    widthRel = 0.0,
                    productId = 0,
                    },
                },
                    signatureImageTextParameter = new SignatureImageTextParameter
                    {
                        customText = "امضا",
                        name = true,
                        signDate = true,
                    }

                }),

            };
            string serializedRequest = Newtonsoft.Json.JsonConvert.SerializeObject(favaRequest);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, ResourceIranFava.CreateSign);
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(serializedRequest, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string CreateSignResult = await response.Content.ReadAsStringAsync();
            CreateSignResult signServiceResult = Newtonsoft.Json.JsonConvert.DeserializeObject<CreateSignResult>(CreateSignResult);
            return signServiceResult;
        }

        public async Task<InquirySignResult> FavaInquirySignOperation(Guid workflowTicket)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ResourceIranFava.InquirySign}{workflowTicket}");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            InquirySignResult inquirySignResult = Newtonsoft.Json.JsonConvert.DeserializeObject<InquirySignResult>(await response.Content.ReadAsStringAsync());
            return inquirySignResult;
        }

        public async Task<CustomersAndCarsResonse> GetCustomersAndCars(int saleId, int pageNo)
        {
            var token = (await GetToken()).result;
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Properties.ResourceIranFava.GetCustomersAndCarsUrl);
            request.Headers.Add("accept", "text/plain");
            request.Headers.Add("Authorization", $"Bearer {token.accessToken}");
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new { saleId, pageNo }), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            string strContent = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<CustomersAndCarsResonse>(strContent);
            return result;
        }
        public async Task<GetTokenResult> GetToken()
        {
            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Properties.ResourceIranFava.tokenUrl);
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new { Properties.ResourceIranFava.userID, Properties.ResourceIranFava.userPWD }), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            string strContent = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<GetTokenResult>(strContent);
            return result;
        }
    }
    public record GetTokenResult
    {
        public TokenResult result { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
    }

    public record TokenResult
    {
        public string accessToken { get; set; }
        public string encryptedAccessToken { get; set; }
        public int expireInSeconds { get; set; }
    }
}
