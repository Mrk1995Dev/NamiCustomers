namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices.Dtos
{
    public class SmsServiceOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
        public string SendSmsUri { get; set; } = string.Empty;
    }
}
