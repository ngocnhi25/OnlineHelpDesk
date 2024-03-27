using System;
using Application.Common.Mapppings;
using Application.DTOs.Accounts;
using Domain.Entities.Accounts;
using Domain.Entities.Roles;

namespace Application.DTOs.Requests
{
    public class RoleDTO : IMapForm<Role>
    {
        public int RoleTypeId { get; set; }
        public string RoleName { get; set; }
        public AccountDTO Account { get; set; }
    }
}

