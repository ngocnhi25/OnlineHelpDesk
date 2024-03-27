using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Accounts.Queries.GetAllRole
{
    public sealed record GetAllRoleQuery() : IQuery<List<RoleDTO>>;
}
