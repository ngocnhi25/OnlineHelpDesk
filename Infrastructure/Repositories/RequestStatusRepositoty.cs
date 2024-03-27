using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class RequestStatusRepositoty : GenericRepository<RequestStatus>, IRequestStatusRepository
    {
        public RequestStatusRepositoty(OHDDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<RequestStatus?>> GetAll()
        {
            var list = await _dbContext.Set<RequestStatus>()
              .ToListAsync();
            return list;
        }

        public async Task<RequestStatus?> GetRequestStatusById(int id)
        {
            var list = await _dbContext.Set<RequestStatus>().SingleOrDefaultAsync(r => r.Id == id);
            return list;
        }
    }
}

