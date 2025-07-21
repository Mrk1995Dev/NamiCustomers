using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;



namespace NamiCustomers.Application.Services.Vehicles;

public interface IVehicleService
{
    Task<ResultDto<VehicleModelDto>> RemoveAsync(int id);
    Task<ResultDto<VehicleModelDto>> GetAsync(int id);
    Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId);
    Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleRegisterDto);
    Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model);
}
public class VehicleService(IAppDbContext dbContext, IMapper mapper, ISevenSoftService sevenSoftService, ISubscriberService subscriberService) : IVehicleService
{
    public async Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleModelDto)
    {
        var subscriber = subscriberService.GetAsync(vehicleModelDto.SubscriberId.Value).Result.Data;

        if (dbContext.VehicleModels.Any(c => c.SubscriberId == subscriber.Id && c.VinNumber == vehicleModelDto.VinNumber))
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errDuplicateSubscriberVin, false);
        }
        if (dbContext.VehicleModels.Any(c => c.SubscriberId != subscriber.Id && c.VinNumber == vehicleModelDto.VinNumber))
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errExistedSubscriber, false);
        }


        var response = await sevenSoftService.GetRelationCustomerInfoByVinNumber(vehicleModelDto.VinNumber, subscriber.NationalCode, subscriber.Mobile);
        if (response != "OK")
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errSave, false);
        }


        var sVehicle= await sevenSoftService.GetChassisInformationByVinNumber(vehicleModelDto.VinNumber);
        if (sVehicle==null)
        {
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errSave, false);
        }
        else
        {
            vehicleModelDto.VehicleModelIdSevenSoft =new Guid( sVehicle.VehicleModelId);
            vehicleModelDto.ChassisUsageTypeName = sVehicle.ChassisUsageTypeName;
            //vehicleModelDto.BrandIdSevenSoft = sVehicle.BrandId;
            vehicleModelDto.BodyColor = sVehicle.BodyColor;
            vehicleModelDto.FullSystem = sVehicle.FullSystem;
            vehicleModelDto.ProductYear = sVehicle.ProductYear;
            vehicleModelDto.SelectedVehicleCommonName = sVehicle.SelectedVehicleCommonName;
            vehicleModelDto.SelectedVehicleDescription = sVehicle.SelectedVehicleDescription;
            vehicleModelDto.VehicleModelLocalizedName = sVehicle.VehicleModelLocalizedName;
            vehicleModelDto.VehicleModelName = sVehicle.VehicleModelName;
        }


      

            var newEntity = mapper.Map<VehicleModel>(vehicleModelDto);

        

        await dbContext.VehicleModels.AddAsync(newEntity);
        var result = await dbContext.SaveChangesAsync();
        if (result < 1)
        {
            var model = mapper.Map<VehicleModelDto>(newEntity);
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errSave, false);
        }
        var newModel = mapper.Map<VehicleModelDto>(newEntity);
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgSave, true,newModel);
    }

    public async Task<ResultDto<VehicleModelDto>> RemoveAsync(int id)
    {
        var entity = await dbContext.VehicleModels.FindAsync(id);
        if (entity is null)
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound, false);
        var model = mapper.Map<VehicleModelDto>(entity);
        dbContext.VehicleModels.Remove(entity);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errDelete, false);
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgDeleted, true, model);
    }


    public async Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId)
    {
        var data = await dbContext.VehicleModels.Where(c => c.SubscriberId == subscriberId).ToListAsync();
        var models = mapper.Map<List<VehicleModelDto>>(data);
        return new ResultDto<List<VehicleModelDto>>(Infrastucture.Properties.Resources.msgFound, true, models);
    }


    public async Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model)
    {
        var entity = await dbContext.VehicleModels.FindAsync(model.Id);
        if (entity is null)
            return new ResultDto<VehicleModelDto>(
               Infrastucture.Properties.Resources.errNotFound, false
               );
        mapper.Map(model, entity);
        dbContext.VehicleModels.Update(entity);
        var editedEntity = mapper.Map<VehicleModelDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return new ResultDto<VehicleModelDto>(
                Infrastucture.Properties.Resources.errEdited, false
               );
        return new ResultDto<VehicleModelDto>(
            Infrastucture.Properties.Resources.msgEdited
            
            , true, editedEntity);
    }

    


    public async Task<ResultDto<VehicleModelDto>> GetAsync(int id)
    {
        var data = await dbContext.VehicleModels.Include(c => c.VehicleAttachment).FirstOrDefaultAsync(cu => cu.Id == id);
        if (data == null) return new ResultDto<VehicleModelDto>(
           Infrastucture.Properties.Resources.errNotFound, false
           );
        var model = mapper.Map<VehicleModelDto>(data);
        return new ResultDto<VehicleModelDto>(
            Infrastucture.Properties.Resources.msgFound,
            
            true, model);
    }

}
