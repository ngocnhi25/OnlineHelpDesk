using System;
using Application.UseCases.Assigness.Queries.GetAllAssignee;
using Application.UseCases.Assigness.Queries.GetAllAssignees;
using Application.UseCases.Departments.Queries.GetAllDepartment;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
	public class GetAllAssignee : EndpointBaseAsync
        .WithRequest<GetAllAssigneeQueries>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllAssignee(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/assignee/getAll")]
        public async override Task<ActionResult<Result>> HandleAsync
            ([FromQuery]GetAllAssigneeQueries request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return Ok(status);
        }
    }
}

