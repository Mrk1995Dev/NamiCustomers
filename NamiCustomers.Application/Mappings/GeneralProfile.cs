using NamiCustomers.Abstractions.Dtos.Appointments;
using NamiCustomers.Abstractions.Dtos.Dealers;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.SevenSoftServices.Dtos;
using NamiCustomers.Domain.Entities.Dealers;
using NamiCustomers.Domain.Entities.Subscribers;

namespace NamiCustomers.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<VehicleModel, VehicleModelDto>().ReverseMap();
 
            CreateMap<Dealer, DealerDto>().ReverseMap();
            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<ChassisInformationByVinNumberResponse, VehicleModelDto>();
            
        }
    }
}
