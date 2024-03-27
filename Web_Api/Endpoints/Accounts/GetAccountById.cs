using Application.UseCases.Accounts.Queries.GetAccountById;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class GetAccountById : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAccountById(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/accounts/{accountId}")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "accountId")]string accoutnId, 
            CancellationToken cancellationToken = default)
        {
            var result = await Sender.Send(new GetAccountByIdQuery(accoutnId));
            return Ok(result);
        }
    }
}
