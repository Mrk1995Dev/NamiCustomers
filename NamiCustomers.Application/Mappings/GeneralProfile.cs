namespace NamiCustomers.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<SAMPLEDTO, SAMPLEEntity>().ReverseMap();
        }
    }
}
