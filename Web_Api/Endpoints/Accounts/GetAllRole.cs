using Application.UseCases.Accounts.Queries.GetAllRole;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class GetAllRole : EndpointBaseAsync
        .WithRequest<GetAllRoleQuery>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRole(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/roles")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] GetAllRoleQuery request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new GetAllRoleQuery());
            return Ok(status);
        }
    }
}
