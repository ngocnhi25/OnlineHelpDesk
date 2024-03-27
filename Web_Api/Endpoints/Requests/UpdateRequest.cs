using Application.UseCases.Requests.Commands.UpdateRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Endpoints.Requests
{
    public class UpdateRequest : EndpointBaseAsync
        .WithRequest<UpdateRequestCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public UpdateRequest(IMediator sender)
        {
            Sender = sender;
        }

        //update status name of ticket 
        [HttpPost("api/request/update_request")]
        [Authorize(Roles = "End-Users")]
        public async override Task<ActionResult<Result>> HandleAsync(
            UpdateRequestCommand request
            ,CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return status;
        }

    }
}
