using Application.UseCases.Departments.Commands.UpdateStatusDepartment;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Departments
{
    public class UpdateStatusDepartment : EndpointBaseAsync
        .WithRequest<UpdateStatusDepartmentCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public UpdateStatusDepartment(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/department/update_statusDepartment")]
        public async override Task<ActionResult<Result>> HandleAsync(
            UpdateStatusDepartmentCommand request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return status;
        }
    }
}

