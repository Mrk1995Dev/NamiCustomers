using NamiCustomers.Abstractions.Dtos.Appointments;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Domain.Entities.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;

namespace NamiCustomers.Application.Mappings;

public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        CreateMap<VehicleModel, VehicleModelDto>().ReverseMap();
        CreateMap<Subscriber,SubscriberDto>().ReverseMap();
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<ChassisInformationByVinNumberResponse, VehicleModelDto>();
        CreateMap<ChassisInformationByVinNumberResponse, VehicleModel>().ReverseMap();
        CreateMap<VehicleAttachment, VehicleAttachmentDto>().ReverseMap();
    }
}
