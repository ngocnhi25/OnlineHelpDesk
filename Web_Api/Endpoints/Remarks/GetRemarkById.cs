using Application.UseCases.Remarks.Queries.GetRemarkById;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class GetRemarkById : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetRemarkById(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/remark/uni/{id}")]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromRoute] string id,
             CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var newRemarkQueries = new GetRemarkByIdQueries { RemarkId = id };
            var status = await Sender.Send(newRemarkQueries);
            return Ok(status);
        }
    }
}
