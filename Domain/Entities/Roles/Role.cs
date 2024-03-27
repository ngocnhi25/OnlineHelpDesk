using Domain.Entities.Accounts;
using SharedKernel;
using System.Text.Json.Serialization;

namespace Domain.Entities.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }
        [JsonIgnore]
        public RoleType? RoleTypes { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
