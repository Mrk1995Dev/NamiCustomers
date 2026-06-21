using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Application.Services.Accounts;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Vehicles;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using static Dapper.SqlMapper;



namespace NamiCustomers.Application.Services.Vehicles;

public interface IVehicleService
{
    Task<ResultDto<VehicleModelDto>> RemoveAsync(int id);
    Task<ResultDto<VehicleModelDto>> GetAsync(int id);
    Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId);
    Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleRegisterDto);
    Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model);
    Task<ResultDto<VehicleModelDto>> SetDefaultAsync(int id);
}
public class VehicleService(IAppDbContext dbContext,
    IMapper mapper, ISevenSoftService sevenSoftService,ITokenService tokenService
    ,UserManager<ApplicationUser> userManager) : IVehicleService
{
    public async Task<ResultDto<VehicleModelDto>> RegisterAsync(VehicleModelDto vehicleModelDto)
    {
      //  var subscriber = subscriberService.GetAsync(vehicleModelDto.SubscriberId.Value).Result.Data;

        if (dbContext.VehicleModels.Any(c => c.SubscriberId == vehicleModelDto.SubscriberId && c.VinNumber == vehicleModelDto.VinNumber))
        {
            return   ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errDuplicateSubscriberVin);
        }
        if (dbContext.VehicleModels.Any(c => c.SubscriberId != vehicleModelDto.SubscriberId && c.VinNumber == vehicleModelDto.VinNumber))
        {
            return ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errExistedSubscriber);
        }


        var response = await sevenSoftService.GetRelationCustomerInfoByVinNumber(vehicleModelDto.VinNumber, vehicleModelDto.NationalCode, vehicleModelDto.Mobile);
        if (response != "OK")
        {
            return   ResultDto.Failure<VehicleModelDto>(response);
        }


        var sVehicle = await sevenSoftService.GetChassisInformationByVinNumber(vehicleModelDto.VinNumber);
        if (sVehicle == null)
        {
            return ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errSave);
        }
        else
        {
            vehicleModelDto.VehicleModelIdSevenSoft = new Guid(sVehicle.VehicleModelId);
            vehicleModelDto.ChassisUsageTypeName = sVehicle.ChassisUsageTypeName;
            //vehicleModelDto.BrandIdSevenSoft = sVehicle.BrandId;
            vehicleModelDto.BodyColor = sVehicle.BodyColor;
            vehicleModelDto.FullSystem = sVehicle.FullSystem;
            vehicleModelDto.ProductYear = sVehicle.ProductYear;
            vehicleModelDto.SelectedVehicleCommonName = sVehicle.SelectedVehicleCommonName;
            vehicleModelDto.SelectedVehicleDescription = sVehicle.SelectedVehicleDescription;
            vehicleModelDto.VehicleModelLocalizedName = sVehicle.VehicleModelLocalizedName;
            vehicleModelDto.VehicleModelName = sVehicle.VehicleModelName;
            vehicleModelDto.VehicleAttachment = new VehicleAttachmentDto { };
        }

        var newEntity = mapper.Map<VehicleModel>(vehicleModelDto);
        if (!dbContext.VehicleAttachments.Where(c => c.VehicleModelIdSevenSoft == vehicleModelDto.VehicleModelIdSevenSoft).Any())
        {
            newEntity.VehicleAttachment = new VehicleAttachment() { VehicleModelIdSevenSoft = newEntity.VehicleModelIdSevenSoft };
        }

        var hisVehicles = await dbContext.VehicleModels.Where(c => c.SubscriberId == newEntity.SubscriberId).ToListAsync();
        hisVehicles.ForEach(c => { c.IsDefault = false; });
        newEntity.IsDefault = true;

        dbContext.VehicleModels.UpdateRange(hisVehicles);

        await dbContext.VehicleModels.AddAsync(newEntity);
        var result = await dbContext.SaveChangesAsync();
        if (result < 1)
        {
            var model = mapper.Map<VehicleModelDto>(newEntity);
            return ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errSave);
        }
        var newModel = mapper.Map<VehicleModelDto>(newEntity);
        return   ResultDto.Success<VehicleModelDto>(newModel);
    }

    public async Task<ResultDto<VehicleModelDto>> RemoveAsync(int id)
    {
        var entity = await dbContext.VehicleModels.FindAsync(id);
        if (entity is null)
            return ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound);
        var model = mapper.Map<VehicleModelDto>(entity);
        dbContext.VehicleModels.Remove(entity);
        if (await dbContext.SaveChangesAsync() < 1) return ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errDelete);
        return ResultDto.Success<VehicleModelDto>(model);
    }


    public async Task<ResultDto<List<VehicleModelDto>>> GetAllAsync(int subscriberId)
    {
        var data = await dbContext.VehicleModels.Where(c => c.SubscriberId == subscriberId).ToListAsync();

        foreach (var item in data)
        {
            var relatedAttach = dbContext.VehicleAttachments.FirstOrDefault(c => c.VehicleModelIdSevenSoft == item.VehicleModelIdSevenSoft);
            if (relatedAttach != null)
            {
                item.VehicleAttachment = relatedAttach;
            }
        }
        var models = mapper.Map<List<VehicleModelDto>>(data);
        return ResultDto.Success<List<VehicleModelDto>>(models);
    }


    public async Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model)
    {
        var entity = await dbContext.VehicleModels.FindAsync(model.Id);
        if (entity is null)
            return   ResultDto.Failure<VehicleModelDto>(
               Infrastucture.Properties.Resources.errNotFound
               );
        mapper.Map(model, entity);
        dbContext.VehicleModels.Update(entity);
        var editedEntity = mapper.Map<VehicleModelDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return   ResultDto.Failure<VehicleModelDto>(
                Infrastucture.Properties.Resources.errEdited
               );
        return   ResultDto.Success<VehicleModelDto>( editedEntity);
    }

    public async Task<ResultDto<VehicleModelDto>> SetDefaultAsync(int id)
    {
        var entity = await dbContext.VehicleModels.FindAsync(id);
        if (entity is null)
            return   ResultDto.Failure<VehicleModelDto>(
               Infrastucture.Properties.Resources.errNotFound
               );

        var subscriber=await dbContext.Subscribers.FindAsync(entity.SubscriberId);
        var user = await userManager.Users.FirstAsync(c => c.NationalCode == subscriber.NationalCode);

        var hisVehicles = await dbContext.VehicleModels.Where(c => c.SubscriberId == entity.SubscriberId).ToListAsync();
        hisVehicles.ForEach(c => { c.IsDefault = false; });
        entity.IsDefault = true;

        dbContext.VehicleModels.UpdateRange(hisVehicles);

        var editedEntity = mapper.Map<VehicleModelDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return   ResultDto.Failure<VehicleModelDto>(
                Infrastucture.Properties.Resources.errEdited
               );
        await tokenService.RevokeTokensAsync(user);
        await tokenService.GenerateAndStoreTokensAsync(user);
        return  ResultDto.Success<VehicleModelDto>(editedEntity);
    }


    public async Task<ResultDto<VehicleModelDto>> GetAsync(int id)
    {
        var data = await dbContext.VehicleModels.Include(c => c.VehicleAttachment).FirstOrDefaultAsync(cu => cu.Id == id);

        if (data == null) return  ResultDto.Failure<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound);

        var relatedAttach = dbContext.VehicleAttachments.FirstOrDefault(c => c.VehicleModelIdSevenSoft == data.VehicleModelIdSevenSoft);

        if (relatedAttach != null)
            data.VehicleAttachment = relatedAttach;

        var model = mapper.Map<VehicleModelDto>(data);
        return   ResultDto.Success<VehicleModelDto>(model);
    }

}
