using NamiCustomers.Application.Services.Appointments.Dtos;
using NamiCustomers.Application.Services.Dealers.Dtos;
using NamiCustomers.Application.Services.Vehicles.Dtos;
using NamiCustomers.Domain.Entities.Dealers;
using NamiCustomers.Domain.Entities.Subscribers;

namespace NamiCustomers.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SAMPLEDTO, SAMPLEEntity>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelDto>().ReverseMap();
            CreateMap<VehicleModel, VehicleRegisterDto>().ReverseMap();
            CreateMap<Dealer, DealerDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
        }
    }
}
