using System.Text.Json.Serialization;

namespace Domain.Entities.Roles
{
    public class RoleType
    {
        public int Id { get; set; }
        public string RoleTypeName { get; set; }
        public List<Role>? Role { get; set; }
    }
}
