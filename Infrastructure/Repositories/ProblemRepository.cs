using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class ProblemRepository : GenericRepository<Problem>, IProblemRepository
    {
        public ProblemRepository(OHDDbContext dbContext)
                   : base(dbContext) { }

        public Task<List<Problem>> GetAllProblem()
        {
            var list = _dbContext.Set<Problem>().ToListAsync();

            return list;
        }

        public Task<Problem?> GetProblemById(int id)
        {
            var problem = _dbContext.Set<Problem>().SingleOrDefaultAsync(p => p.Id == id);

            return problem;
        }
    }
}
