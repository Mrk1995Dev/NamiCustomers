




using NamiCustomers.Abstractions.Dtos.Settings;

namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices
{


    public class JiringSmsService : ISmsService
    {
        private readonly List<SmsSetting> _smsSetting;
        private readonly SmsSetting _smsProvider;
        private readonly CompanySetting _companySetting;
        private readonly ILogger<ISmsService> _logger;

        public JiringSmsService(IOptions<List<SmsSetting>> smsSettings, IOptions<CompanySetting> companySetting, ILogger<ISmsService> logger)
        {
            _smsSetting = smsSettings.Value;
            _companySetting = companySetting.Value;
            _smsProvider = _smsSetting.Single(c => c.ProviderName == "Jiring");
            _logger = logger;
        }

        public async Task<HttpResponseMessage> SendSms(string reciverMobile, string message)
        {
            try
            {
                reciverMobile = string.Concat("98", reciverMobile.AsSpan(reciverMobile.Length - 10));

                string accessToken = GetToken();

                JiringSourceDto source = new();
                source.SourceAddress = _smsProvider.source;
                source.DestinationAddress = reciverMobile;
                source.MessageText = message;
                var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(source);
                jsonContent = $"[{jsonContent}]";
                var smsClient = new RestClient(_smsProvider.SmsQueueServiceUrl);
                var smsRequest = new RestRequest("send", Method.Post);


                smsRequest.AddHeader("Authorization", "Bearer " + accessToken);
                smsRequest.AddHeader("Content-Type", "application/json");

                smsRequest.AddBody(jsonContent);
                RestResponse smsResponse = smsClient.Execute(smsRequest);

                var s = smsResponse.Content;
                if (smsResponse.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation($"SendSms of JiringSmsService to {reciverMobile} successful!");
                }
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendSms of JiringSmsService to {reciverMobile} Faild! ==>\n because ==>\n {ex.Message} ==>\n {ex.StackTrace}");
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.InternalServerError };
        }

        /// <summary>
        /// دریافت توکن sms
        /// </summary>
        /// <returns></returns>
        static string GetToken()
        {
            var accessToken = "";

            var client = new RestClient("https://sms.jiring.ir:9095/connect/");
            var req = new RestRequest("token", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("scope", "ApiAccess", ParameterType.GetOrPost);
            req.AddParameter("username", "namikhodro", ParameterType.GetOrPost);
            req.AddParameter("password", "H6uK?$3@EnBX", ParameterType.GetOrPost);

            RestResponse response = client.Execute(req);
            Console.WriteLine(response);
            if (response.IsSuccessful)
            {

                accessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<JiringBodyDto>(response.Content).access_token;
            }

            return accessToken;
        }


    }






}
