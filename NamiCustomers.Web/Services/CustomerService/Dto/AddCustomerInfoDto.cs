namespace NamiCustomers.Web.Services.CustomerService.Dto
{
    public class AddCustomerInfoDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public int CoWorkers { get; set; }
    }

}