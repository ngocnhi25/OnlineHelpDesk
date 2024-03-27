using System;
using Application.Common.Messaging;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetRequestStatus
{
    public sealed record GetRequestStatusQuery : IQuery<IEnumerable<RequestStatusDTO>>;
}

