namespace NamiCustomers.Application.Services.Customers.Dtos
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public int CoWorkers { get; set; }
    }
}
