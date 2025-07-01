namespace NamiCustomers.Abstractions.Dtos.Subscribers
{
    public class SubscriberDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
