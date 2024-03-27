using Application.UseCases.Accounts.Queries.CheckEmail;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class CheckEmail : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public CheckEmail(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/checkEmail/{email}")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "email")] string email,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new CheckEmailQuery(null, email));
            return Ok(status);
        }
    }
}
