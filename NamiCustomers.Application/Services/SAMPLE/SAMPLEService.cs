namespace NamiCustomers.Application.Services.SAMPLE
{
    public interface ISAMPLEService
    {
        Task<IEnumerable<SAMPLEDTO>> GetAll(SAMPLEFilterDTO filter);
        Task<SAMPLEDTO> GetAsync(int id);
        Task<int> CreateAsync(SAMPLEDTO SAMPLE);
        Task<SAMPLEDTO> UpdateAsync(SAMPLEDTO SAMPLE);
        Task<int> DeleteAsync(int id);
    }
    public class SAMPLEService(IAppDbContext dbContext, IMapper mapper) : ISAMPLEService
    {
        public async Task<int> CreateAsync(SAMPLEDTO SAMPLEDTO)
        {
            dbContext.SAMPLEs.Add(mapper.Map<SAMPLEEntity>(SAMPLEDTO));
            return await dbContext.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var result = await dbContext.SAMPLEs.FindAsync(id);
            if (result != null)
            {
                dbContext.SAMPLEs.Remove(result);
                return await dbContext.SaveChangesAsync();
            }
            return await dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<SAMPLEDTO>> GetAll(SAMPLEFilterDTO filter)
        {
            //var query = $"SELECT * FROM SAMPLEs ";
            //using (var connection = _SAMPLEDapperContext.GetDbconnection())
            //{
            //    return await connection.QueryAsync<SAMPLEDTO>(query);
            //}
            var query = dbContext.SAMPLEs.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Field))
            {
                query = query.Where(c => c.Field.Contains(filter.Field));
            }

            var data = await query.ToListAsync();
            return mapper.Map<IEnumerable<SAMPLEDTO>>(data);
        }

        private IQueryable<SAMPLEEntity> ExecuteQuery(SAMPLEFilterDTO filter)
        {
            var query = dbContext.SAMPLEs.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Field))
            {
                query = query.Where(c => c.Field.Contains(filter.Field));
            }

            return query;
        }

        public async Task<SAMPLEDTO> GetAsync(int id)
        {
            var result = await dbContext.SAMPLEs.FirstOrDefaultAsync(c => c.Id == id);

            return mapper.Map<SAMPLEDTO>(result);
        }

        public async Task<SAMPLEDTO> UpdateAsync(SAMPLEDTO item)
        {
            var result = await dbContext.SAMPLEs.FindAsync(item.Id);
            if (result != null)
            {
                mapper.Map(item, result);
                dbContext.SAMPLEs.Update(result);
                dbContext.SaveChanges();
            }
            return mapper.Map<SAMPLEDTO>(result);
            //await Task.CompletedTask;
        }





    }
}
