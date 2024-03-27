using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Accounts.Queries.GetAllRoleType
{
    public sealed record GetAllRoleTypeQuery() : IQuery<List<RoleTypeDTO>>;
}
