namespace NamiCustomers.Application.Services.Subscribers.Dtos
{
    public class SubscriberListDto
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
