namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices.Dtos
{
    public class JiringBodyDto
    {
        public string access_token { get; set; }
        public DateTime expires_at { get; set; }
        public string scope { get; set; }
    }
}
