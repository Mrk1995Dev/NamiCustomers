using NamiCustomers.Abstractions.Dtos.Dealers;
using NamiCustomers.Abstractions.Dtos.Subscribers;

namespace NamiCustomers.Abstractions.Dtos.Appointments
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int? SubscriberId { get; set; }
        public DateTime ReservedDate { get; set; }
        public int DealerId { get; set; }
        public int ReservedNumber { get; set; }
        public DealerDto Dealer { get; set; }
        public SubscriberDto? Subscriber { get; set; }
    }
}
