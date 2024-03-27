using Application.Common.Mapppings;
using Domain.Entities.Accounts;

namespace Application.DTOs.Accounts
{
    public class AccountResponse : IMapForm<Account>
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string AvatarPhoto { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Enable { get; set; }
        public bool IsBanned { get; set; }
        public int RoleId { get; set; }
        public string StatusAccount { get; set; }

        public RoleDTO Role { get; set; }
    }
}
