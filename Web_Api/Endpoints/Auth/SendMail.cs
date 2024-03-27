using Application.UseCases.Accounts.Commands.SendMailVerifyCode;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class SendMail : EndpointBaseAsync
    .WithRequest<string>
    .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public SendMail(IMediator sender)
        {
            Sender = sender;
        }


        [HttpGet("api/auth/send-mail/{email}")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "email")]string email,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new SendMailVerifyCodeCommand(email));
            return Ok(status);
        }
    }
}
