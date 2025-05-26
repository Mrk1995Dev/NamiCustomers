using NamiCustomers.Application.Services.Dealers.Dtos;
using NamiCustomers.Application.Services.Subscribers.Dtos;

namespace NamiCustomers.Application.Services.Appointments.Dtos
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
