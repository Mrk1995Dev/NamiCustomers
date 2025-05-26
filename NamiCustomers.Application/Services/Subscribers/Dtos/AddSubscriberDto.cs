namespace NamiCustomers.Application.Services.Subscribers.Dtos
{
    public class AddSubscriberDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public int CoWorkers { get; set; }
    }
}
