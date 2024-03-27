using System;
using Application.UseCases.Assigness.Queries.GetAllAssignees;
using Application.UseCases.Requests.Queries.GetRequestStatus;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
	public class GetRequestStatus : EndpointBaseAsync
        .WithRequest<GetRequestStatusQuery>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetRequestStatus(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request/requestStatus")]
        public async override Task<ActionResult<Result>> HandleAsync(
           [FromQuery] GetRequestStatusQuery request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return Ok(status);
        }
    }
}

