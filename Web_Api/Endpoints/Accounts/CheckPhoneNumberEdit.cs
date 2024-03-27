using Application.UseCases.Accounts.Queries.CheckPhoneNumber;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class checkPhoneNumberEdit : EndpointBaseAsync
        .WithRequest<FormCheckPhoneNumber>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public checkPhoneNumberEdit(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/phoneNumberEdit")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FormCheckPhoneNumber request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new CheckPhoneNumberQuery(request.AccountId, request.PhoneNumber));
            return Ok(status);
        }
    }

    public class FormCheckPhoneNumber
    {
        public string AccountId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
