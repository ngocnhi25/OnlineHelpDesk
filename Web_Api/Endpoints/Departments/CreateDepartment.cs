using Application.UseCases.Departments.Commands.CreateDepartment;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Departments
{
    public class CreateDepartment : EndpointBaseAsync
        .WithRequest<CreateDepartmentCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public CreateDepartment(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/department/create_department")]
        public override async Task<ActionResult<Result>> HandleAsync
            (CreateDepartmentCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }


    }
}
