using Application.UseCases.Requests.Queries.GetAllClientRequest;
using Application.UseCases.Requests.Queries.GetAllRequestWithoutSSFP;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class GetAllRequestWithoutSSFP: EndpointBaseAsync
        .WithRequest<GetAllRequestWithoutSSFPQuery>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRequestWithoutSSFP(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request_withoutssfp")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromRoute] GetAllRequestWithoutSSFPQuery request,
            CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);

            var status = await Sender.Send(new GetAllRequestWithoutSSFPQuery(accountId));
            return Ok(status);
        }
    }
}
