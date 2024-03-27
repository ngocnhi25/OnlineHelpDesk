using Application.Common.Mapppings;
using Domain.Entities.Accounts;
using Domain.Entities.Roles;
using System.Text.Json.Serialization;

namespace Application.DTOs.Auth
{
    public class LoginDTO : IMapForm<Account>
    {
        public string AccountId { get; set; }
        public string RoleName { get; set; }
        public string RoleTypeName { get; set; }
        public string Email { get; set; }
        public bool Enable { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
