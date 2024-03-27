using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Accounts.Queries.GetAccountById
{
    public sealed record GetAccountByIdQuery(string AccountId) : IQuery<AccountResponse>;
}
