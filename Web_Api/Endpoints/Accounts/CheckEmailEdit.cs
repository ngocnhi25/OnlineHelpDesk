using Application.UseCases.Accounts.Queries.CheckEmail;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class CheckEmailEdit : EndpointBaseAsync
        .WithRequest<FormCheckEmail>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public CheckEmailEdit(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/checkEmailEdit")]
        [Authorize(Roles = "Administrator")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FormCheckEmail request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new CheckEmailQuery(request.AccountId, request.Email));
            return Ok(status);
        }
    }

    public class FormCheckEmail
    {
        public string AccountId { get; set; }
        public string Email { get; set; }
    }
}
