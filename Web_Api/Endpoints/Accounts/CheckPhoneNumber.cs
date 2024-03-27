using Application.UseCases.Accounts.Queries.CheckPhoneNumber;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class CheckPhoneNumber : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public CheckPhoneNumber(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/phoneNumber/{phoneNumber}")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "phoneNumber")] string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new CheckPhoneNumberQuery(null, phoneNumber));
            return Ok(status);
        }
    }
}
