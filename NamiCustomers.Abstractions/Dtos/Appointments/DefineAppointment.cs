using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Appointments
{
    public class DefineAppointmentDto
    {
        public int DealerId { get; set; }
        [UIHint("PersianDate")]
        public DateTime? ReserveDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfAppointments { get; set; }
    }
}
