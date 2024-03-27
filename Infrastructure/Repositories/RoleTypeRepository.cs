using Domain.Entities.Roles;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RoleTypeRepository : GenericRepository<RoleType>, IRoleTypeRepository
    {
        public RoleTypeRepository(OHDDbContext dbContext) : base(dbContext)
        {
        }
    }
}
