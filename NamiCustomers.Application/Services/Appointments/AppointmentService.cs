
using NamiCustomers.Abstractions.Dtos.Appointments;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.Properties;

namespace NamiCustomers.Application.Services.Appointments;

public interface IAppointmentService
{
    Task<ResultDto<List<AppointmentDto>>> DefineAppointments(DefineAppointmentDto defineAppointmentDto);
    Task<ResultDto<AppointmentDto>> EditAsync(AppointmentDto model);
    Task<ResultDto<List<AppointmentDto>>> GetAllAsync();
    Task<ResultDto<AppointmentDto>> GetAsync(int id);
    Task<ResultDto<AppointmentDto>> RemoveAsync(int id);
    Task<ResultDto<AppointmentDto>> Reserve(int appointmentId, int subscriberId);
}

public class AppointmentService(IAppDbContext dbContext,IMapper mapper) : IAppointmentService
{
    public async Task<ResultDto<AppointmentDto>> RemoveAsync(int id)
    {
        var entity = await dbContext.Appointments.FindAsync(id);
        if (entity is null)
            return new ResultDto<AppointmentDto>(Infrastucture.Properties.Resources.errNotFound, false);
        var model = mapper.Map<AppointmentDto>(entity);
        dbContext.Appointments.Remove(entity);
        if (await dbContext.SaveChangesAsync() < 1) return new ResultDto<AppointmentDto>(Infrastucture.Properties.Resources.errDelete, false);
        return new ResultDto<AppointmentDto>(Infrastucture.Properties.Resources.msgDeleted,true ,model);
    }

    public async Task<ResultDto<AppointmentDto>> EditAsync(AppointmentDto model)
    {
        var entity = await dbContext.Appointments.FindAsync(model.Id);
        if (entity is null)
            return new ResultDto<AppointmentDto>(
               Infrastucture.Properties.Resources.errNotFound,false
               );
        mapper.Map(model, entity);
        dbContext.Appointments.Update(entity);
        var editedEntity = mapper.Map<AppointmentDto>(entity);
        if (await dbContext.SaveChangesAsync() < 1)
            return new ResultDto<AppointmentDto>(
                Infrastucture.Properties.Resources.errEdited,false
               );
        return new ResultDto<AppointmentDto>(
            Infrastucture.Properties.Resources.msgEdited
           , true
            , editedEntity);
    }

    public async Task<ResultDto<List<AppointmentDto>>> GetAllAsync()
    {
        var data = await dbContext.Appointments.ToListAsync();
        var models = mapper.Map<List<AppointmentDto>>(data);
        return new ResultDto<List<AppointmentDto>>(Infrastucture.Properties.Resources.msgFound, true, models);
    }
    public async Task<ResultDto<AppointmentDto>> GetAsync(int id)
    {
        var data = await dbContext.Appointments.Include(c => c.Subscriber).Include(c => c.Dealer).FirstOrDefaultAsync(cu => cu.Id == id);
        if (data == null) return new ResultDto<AppointmentDto>(
           Infrastucture.Properties.Resources.errNotFound,false
           );
        var model = mapper.Map<AppointmentDto>(data);
        return new ResultDto<AppointmentDto>(
            Infrastucture.Properties.Resources.msgFound,
            true, model);
    }
     
    public async Task<ResultDto<AppointmentDto>> Reserve(int appointmentId, int subscriberId)
    {
        var appointment = dbContext.Appointments.Include(c => c.Dealer).FirstOrDefault(c => c.Id == appointmentId);
        if (appointment != null)
        {
            appointment.SubscriberId = subscriberId;
            dbContext.SaveChanges();
        }

       return await GetAsync(appointmentId);
    }

    public async Task<ResultDto<List<AppointmentDto>>> DefineAppointments(DefineAppointmentDto defineAppointmentDto)
    {
        if (defineAppointmentDto.EndTime <= defineAppointmentDto.StartTime)
        {
            throw new Exception(Resources.errEndTimeCannotEqualOrLessThanStatrtTime);
        }

        if (defineAppointmentDto.NumberOfAppointments <= 0)
        {
            throw new Exception(Resources.errNumberOfAppointmentsCanNotEqualOrLessThanZero);
        }
        if (defineAppointmentDto.ReserveDate.Value.DayOfWeek.HasFlag(DayOfWeek.Friday) ||
            defineAppointmentDto.ReserveDate.Value.DayOfWeek.HasFlag(DayOfWeek.Thursday)
            )
        {
            throw new Exception(Resources.errVacationDateNotSelectable);
        }


        List<Appointment> appointments = new();
        TimeSpan duration = defineAppointmentDto.EndTime - defineAppointmentDto.StartTime;
        int anyAppointmentTime = (int)(duration.TotalMinutes / defineAppointmentDto.NumberOfAppointments);
        for (int i = 0; i < defineAppointmentDto.NumberOfAppointments; i++)
        {
            DateTime reserveTime = defineAppointmentDto.StartTime.AddMinutes(i * anyAppointmentTime);
            Appointment item = new Appointment
            {
                DealerId = defineAppointmentDto.DealerId,
                ReservedNumber = i + 1,
                ReservedDate = reserveTime,
            };
            appointments.Add(item);
        }
        dbContext.Appointments.AddRange(appointments);
        dbContext.SaveChanges();
        return await  GetAllAsync();
    }
}
