using System.Diagnostics;

namespace NamiCustomers.Infrastucture.ExternalServices.Email.Dtos
{
    public class MailSettings
    {
        public string? Mail { get; set; }
        public string? DisplayName { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; } = 587;
        public string? addreas { get; set; }
    }








    public class CompanySetting
    {
        public string? CompanyName { get; set; }
        public string? Accountnumber { get; set; }
        public string? Shebanumber { get; set; }
        public string? BankName { get; set; }
        public string? BankCode { get; set; }

        public string? BanckBranch { get; set; }
    }









}
