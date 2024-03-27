using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Assigness.Queries.GetAssigneeById
{
    public sealed record GetAssigneeByIdQueries : IQuery<AccountResponse>
	{
        public string? AccountId { get; set; }
    }

}

