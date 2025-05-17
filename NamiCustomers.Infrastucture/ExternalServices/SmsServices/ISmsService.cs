
namespace NamiCustomers.Infrastucture.ExternalServices.SmsServices
{
    public interface ISmsService
    {
        Task<HttpResponseMessage> SendSms(string reciverMobile, string message);
    }
}
