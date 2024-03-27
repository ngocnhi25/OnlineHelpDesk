using Domain.Entities.Roles;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(OHDDbContext dbContext) : base(dbContext)
        {
        }
    }
}
