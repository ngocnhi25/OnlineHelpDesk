using Application.UseCases.Departments.Queries.GetListDepartmentSSFP;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Departments
{
    public class GetListDepartmentSSFP
        : EndpointBaseAsync
        .WithRequest<Field>
        .WithActionResult<Result>

    {
        private readonly IMediator Sender;

        public GetListDepartmentSSFP(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/department/getListDepartmentSSFP")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery]Field request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send
                 (new GetListDepartmentSSFPQueries(
                 request.SearchTerm,request.Page, request.Limit));
            return Ok(status);
        }
    }

    public class Field
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}

