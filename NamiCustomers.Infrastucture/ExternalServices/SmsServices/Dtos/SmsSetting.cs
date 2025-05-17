namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices.Dtos
{
    public class SmsSetting
    {
        public string? SmsQueueServiceUrl { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? source { get; set; }
        public string? ApiKey { get; set; }
        public bool IsDefault { get; set; }
        public string ProviderName { get; set; }

    }
}



