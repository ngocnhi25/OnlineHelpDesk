using Application.UseCases.Remarks.Queries.GetListRemarskByAccountId;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class GetListRemarskByAccountId : EndpointBaseAsync
        .WithRequest<GetListRemarskByAccountIdQueries>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetListRemarskByAccountId(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/remark")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromQuery] GetListRemarskByAccountIdQueries request,
             CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var newRemarkQueries = new GetListRemarskByAccountIdQueries {  AccountId = accountId };
            var status = await Sender.Send(newRemarkQueries);
            return Ok(status);
        }
    }
}
