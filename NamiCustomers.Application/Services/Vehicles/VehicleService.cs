using NamiCustomers.Abstractions.Dtos.Vehicles;
using NamiCustomers.Domain.Entities.Subscribers;



namespace NamiCustomers.Application.Services.Vehicles;

public interface IVehicleService
{
    Task<ResultDto<VehicleModelDto>> RemoveAsync(int id);
    Task<ResultDto<VehicleModelDto>> GetAsync(int id);
    Task<ResultDto<List<VehicleModelDto>>> GetAllAsync();
    Task<ResultDto<VehicleModelDto>> RegisterByVinNumberAsync(VehicleRegisterDto vehicleRegisterDto);
    Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model);
}
public class VehicleService(IAppDbContext dbContext, IMapper mapper) : IVehicleService
{
    public async Task<ResultDto<VehicleModelDto>> RegisterByVinNumberAsync(VehicleRegisterDto vehicleRegisterDto)
    {
        var newEntity = mapper.Map<VehicleModel>(vehicleRegisterDto);
        await dbContext.VehicleModels.AddAsync(newEntity);
        var result = await dbContext.SaveChangesAsync();
        if (result < 1)
        {
            var model = mapper.Map<VehicleModelDto>(newEntity);
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errSave, false, model);
        }
        var newModel = mapper.Map<VehicleModelDto>(newEntity);
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgSave, true, newModel);
    }

    public async Task<ResultDto<VehicleModelDto>> RemoveAsync(int id)
    {
        var entity = await dbContext.VehicleModels.FindAsync(id);
        if (entity is null)
            return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errNotFound, false, new VehicleModelDto { Id = id });
        var model = mapper.Map<VehicleModelDto>(entity);
        dbContext.VehicleModels.Remove(entity);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.errDelete, false, model);
        return new ResultDto<VehicleModelDto>(Infrastucture.Properties.Resources.msgDeleted, true, model);
    }


    public async Task<ResultDto<List<VehicleModelDto>>> GetAllAsync()
    {
        var data = await dbContext.VehicleModels.ToListAsync();
        var models = mapper.Map<List<VehicleModelDto>>(data);
        return new ResultDto<List<VehicleModelDto>>(Infrastucture.Properties.Resources.msgFound, true, models);
    }


    public async Task<ResultDto<VehicleModelDto>> EditAsync(VehicleModelDto model)
    {
        var entity = await dbContext.VehicleModels.FindAsync(model.Id);
        if (entity is null)
            return new ResultDto<VehicleModelDto>(
               Infrastucture.Properties.Resources.errNotFound
                , false
                , model);
        mapper.Map(model, entity);
        dbContext.VehicleModels.Update(entity);
        var editedEntity = mapper.Map<VehicleModelDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return new ResultDto<VehicleModelDto>(
                Infrastucture.Properties.Resources.errEdited
                , false
                , editedEntity);
        return new ResultDto<VehicleModelDto>(
            Infrastucture.Properties.Resources.msgEdited
            , true
            , editedEntity);
    }


    public async Task<ResultDto<VehicleModelDto>> GetAsync(int id)
    {
        var data = await dbContext.VehicleModels.FirstOrDefaultAsync(cu => cu.Id == id);
        if (data == null) return new ResultDto<VehicleModelDto>(
           Infrastucture.Properties.Resources.errNotFound,
            false,
            null);
        var model = mapper.Map<VehicleModelDto>(data);
        return new ResultDto<VehicleModelDto>(
            Infrastucture.Properties.Resources.msgFound,
            true,
            model);
    }

}
