using System;
using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Assigness.Queries.GetAllAssignee
{
	public sealed record GetAllAssigneeQueries : IQuery<IEnumerable<AccountResponse>> { };
}

