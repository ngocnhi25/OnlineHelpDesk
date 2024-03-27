using Application.UseCases.Remarks.Queries;
using Application.UseCases.Requests.Queries.GetRequestById;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class GetListRemarskByReqId : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetListRemarskByReqId(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/remark/{id}")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromRoute] string id,
             CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var newRemarkQueries = new GetListRemarskByReqIdQueries { RequestId = id};
            var status = await Sender.Send(newRemarkQueries);
            return Ok(status);
        }
    }
}
