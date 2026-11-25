using System.Text.Json;

namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices
{
    /// <summary>
    /// Nami Winno SMS gateway client (SmsService: ApiKey, BaseUrl, SendSmsUri).
    /// </summary>
    public class SmsService : ISmsService
    {
        private readonly SmsServiceOptions _options;
        private readonly ILogger<ISmsService> _logger;

        public SmsService(IOptions<SmsServiceOptions> options, ILogger<ISmsService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendSms(string reciverMobile, string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_options.BaseUrl) ||
                    string.IsNullOrWhiteSpace(_options.SendSmsUri) ||
                    string.IsNullOrWhiteSpace(_options.ApiKey))
                {
                    _logger.LogError("SmsService is not configured (BaseUrl / SendSmsUri / ApiKey).");
                    return new HttpResponseMessage(HttpStatusCode.InternalServerError);
                }

                var mobile = NormalizeMobile(reciverMobile);
                var requestUrl = BuildSendUrl();
                var jsonContent = JsonSerializer.Serialize(new { Mobile = mobile, Message = message });

                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("SendSms of SmsService to {Mobile} successful!", mobile);
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync();
                    _logger.LogError(
                        "SendSms of SmsService to {Mobile} failed with {StatusCode}: {Body}",
                        mobile,
                        (int)response.StatusCode,
                        body);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendSms of SmsService to {Mobile} failed!", reciverMobile);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        private string BuildSendUrl()
        {
            var baseUrl = _options.BaseUrl.TrimEnd('/');
            var uri = _options.SendSmsUri;
            // SendSmsUri already ends with "apiKey="; append encoded key.
            return $"{baseUrl}/{uri}{Uri.EscapeDataString(_options.ApiKey)}";
        }

        private static string NormalizeMobile(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
                return mobile;

            mobile = mobile.Trim();
            if (mobile.Length >= 10)
                return string.Concat("98", mobile.AsSpan(mobile.Length - 10));

            return mobile;
        }
    }
}
