using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using NamiCustomers.Infrastucture.ExternalServices.Email.Dtos;

namespace NamiCustomers.Infrastucture.ExternalServices.Email
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendForgetPasswordEmailAsync(FrogetPasswordDto request);

    }
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public Task SendEmailAsync(MailRequest mailRequest)
        {
            throw new NotImplementedException();
        }

        public async Task SendForgetPasswordEmailAsync(FrogetPasswordDto request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\TemplatesEmail\\FogetPassWord.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[login]", _mailSettings.addreas).Replace("[Name]", request.FullName).Replace("[Newpass]", request.NewPass.Trim()).Replace("[IP]", request.IpRequest.ToString()).Replace("[browser_name]", request.browser.ToString()).Replace("[date]", request.Date.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"درخواست فراموشی رمز - {request.FullName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            // smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);not support on namikhodro server
            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            try
            {


                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {

                throw;
            }
            smtp.Disconnect(true);
        }


    }
}
