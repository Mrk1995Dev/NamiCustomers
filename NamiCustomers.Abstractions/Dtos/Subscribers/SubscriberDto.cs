using NamiCustomers.Abstractions.Dtos.Vehicles;

namespace NamiCustomers.Abstractions.Dtos.Subscribers
{
    public class SubscriberDto
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string PhoneNumber { get; set; }
        public string? NationalCode { get; set; }
        public string? Family { get; set; }
        public string? Mobile { get; set; }
        public string? Sex { get; set; }
        public ICollection<VehicleModelDto>? VehicleModels { get; set; }
    }
}
