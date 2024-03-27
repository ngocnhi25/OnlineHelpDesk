using Application.Common.Messaging;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetRequestById
{
    public sealed record GetRequestByIdQuery(Guid Id) : IQuery<RequestDTO>;
}
