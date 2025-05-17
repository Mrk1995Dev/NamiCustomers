namespace NamiCustomers.Domain.Entities
{
    [Auditable]
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public int CoWorkers { get; set; }
    }
}
