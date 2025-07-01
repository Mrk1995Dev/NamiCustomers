
using System.Text.Json;


namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices
{
    public record KaraMessage(string srcNum, string recipient, string body);

    public class KaraShobSmsService : ISmsService
    {
        private readonly List<SmsSetting> _smsSetting;
        private readonly SmsSetting _smsProvider;
        private readonly CompanySetting _companySetting;
        private readonly ILogger<ISmsService> _logger;

        public KaraShobSmsService(IOptions<List<SmsSetting>> SmsSettings, IOptions<CompanySetting> companySetting, ILogger<ISmsService> logger)
        {
            _smsSetting = SmsSettings.Value;
            _companySetting = companySetting.Value;
            _smsProvider = _smsSetting.Single(c => c.ProviderName == "KaraShob");
            _logger = logger;
        }


        public async Task<HttpResponseMessage> SendSms(string reciverMobile, string messageBody)
        {
            try
            {
                reciverMobile = string.Concat("98", reciverMobile.AsSpan(reciverMobile.Length - 10));
                KaraMessage message = new(_smsProvider.source, reciverMobile, messageBody);

                var client = new HttpClient();
                var jsonContent = JsonSerializer.Serialize(message);
                var req = new HttpRequestMessage(HttpMethod.Post, _smsProvider.SmsQueueServiceUrl);
                req.Headers.Add("x-api-key", _smsProvider.ApiKey);
                jsonContent = $"[{jsonContent}]";
                req.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(req);
                var result = response.EnsureSuccessStatusCode();
                var smsResponse = await response.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation($"SendSms of KaraShobSmsService to {reciverMobile} successful!");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendSms of KaraShobSmsService to {reciverMobile} Faild! ==>\n because ==>\n {ex.Message} ==>\n {ex.StackTrace}");
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
        }


    }
}
