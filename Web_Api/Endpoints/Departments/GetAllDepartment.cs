using Application.UseCases.Departments.Queries.GetAllDepartment;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_Api.Endpoints.Departments
{
    public class GetAllDepartment: EndpointBaseAsync
        .WithRequest<GetAllDepartmentQueries>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllDepartment(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/department/getAll")]
        public async override Task<ActionResult<Result>> HandleAsync([FromQuery]GetAllDepartmentQueries request, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return Ok(status);
        }
    }
}
