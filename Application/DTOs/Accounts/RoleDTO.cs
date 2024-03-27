using Application.Common.Mapppings;
using Domain.Entities.Roles;

namespace Application.DTOs.Accounts
{
    public class RoleDTO : IMapForm<Role>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public RoleTypeDTO RoleTypes { get; set; }
    }
}
