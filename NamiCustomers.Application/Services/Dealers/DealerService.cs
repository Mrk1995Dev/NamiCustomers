using NamiCustomers.Abstractions.Dtos.Dealers;
using NamiCustomers.Domain.Entities.Dealers;

namespace NamiCustomers.Application.Services.Dealers;

public interface IDealerService
{
    Task<ResultDto<DealerDto>> EditAsync(DealerDto dealerDto);
    Task<ResultDto<List<DealerDto>>> GetAllAsync();
    Task<ResultDto<DealerDto>> GetAsync(int id);
    Task<ResultDto<DealerDto>> RegisterAsync(DealerDto DealerDto);
    Task<ResultDto<DealerDto>> RemoveAsync(int id);
}
public class DealerService(IAppDbContext dbContext,IMapper mapper) : IDealerService
{
    public async Task<ResultDto<DealerDto>> RegisterAsync(DealerDto DealerDto)
    {
        var newEntity = mapper.Map<Dealer>(DealerDto);
        await dbContext.Dealers.AddAsync(newEntity);
        var result = await dbContext.SaveChangesAsync();
        if (result < 1)
        {
            var model = mapper.Map<DealerDto>(newEntity);
            return new ResultDto<DealerDto>(Infrastucture.Properties.Resources.errSave, false);
        }
        var newModel = mapper.Map<DealerDto>(newEntity);
        return new ResultDto<DealerDto>(Infrastucture.Properties.Resources.msgSave, true, newModel);
    }

    public async Task<ResultDto<DealerDto>> RemoveAsync(int id)
    {
        var entity = await dbContext.Dealers.FindAsync(id);
        if (entity is null)
            return new ResultDto<DealerDto>(Infrastucture.Properties.Resources.errNotFound, false);
        var model = mapper.Map<DealerDto>(entity);
        dbContext.Dealers.Remove(entity);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto<DealerDto>(Infrastucture.Properties.Resources.errDelete, false);
        return new ResultDto<DealerDto>(Infrastucture.Properties.Resources.msgDeleted, true, model);
    }


    public async Task<ResultDto<List<DealerDto>>> GetAllAsync()
    {
        var data = await dbContext.Dealers.ToListAsync();
        var models = mapper.Map<List<DealerDto>>(data);
        return new ResultDto<List<DealerDto>>(Infrastucture.Properties.Resources.msgFound, true, models);
    }


    public async Task<ResultDto<DealerDto>> EditAsync(DealerDto model)
    {
        var entity = await dbContext.Dealers.FindAsync(model.Id);
        if (entity is null)
            return new ResultDto<DealerDto>(
               Infrastucture.Properties.Resources.errNotFound,false
               );
        mapper.Map(model, entity);
        dbContext.Dealers.Update(entity);
        var editedEntity = mapper.Map<DealerDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return new ResultDto<DealerDto>(
                Infrastucture.Properties.Resources.errEdited, false
                );
        return new ResultDto<DealerDto>(
            Infrastucture.Properties.Resources.msgEdited
           
            , true, editedEntity);
    }


    public async Task<ResultDto<DealerDto>> GetAsync(int id)
    {
        var data = await dbContext.Dealers.FirstOrDefaultAsync(cu => cu.Id == id);
        if (data == null) return new ResultDto<DealerDto>(
           Infrastucture.Properties.Resources.errNotFound, false
          );
        var model = mapper.Map<DealerDto>(data);
        return new ResultDto<DealerDto>(
            Infrastucture.Properties.Resources.msgFound,
           
            true, model);
    }
}
