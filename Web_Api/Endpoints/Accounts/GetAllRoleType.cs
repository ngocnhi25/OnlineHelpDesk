using Application.UseCases.Accounts.Queries.GetAllRoleType;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class GetAllRoleType : EndpointBaseAsync
        .WithRequest<GetAllRoleTypeQuery>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRoleType(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/role-types")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] GetAllRoleTypeQuery request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new GetAllRoleTypeQuery());
            return Ok(status);
        }
    }
}
