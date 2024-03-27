using Application.UseCases.Requests.Queries.GetAllRequestOfAssigneeProcessing;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests.Assignees
{
    public class GetAllRequestOfAssigneesAccountId : EndpointBaseAsync
        .WithRequest<FieldSSFPProcessByAssignees>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRequestOfAssigneesAccountId(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/requests/processByAssignees/{accountId}")]
        [Authorize(Roles = "Facility-Heads, Assignees")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FieldSSFPProcessByAssignees request,
            CancellationToken cancellationToken = default)
        {
            var assigneeAccountId = User.FindFirstValue(ClaimTypes.Sid);
            var status = await Sender.Send(new GetAllRequestOfAssigneeProcessingQuery(
                assigneeAccountId,
                request.SearchTerm,
                request.SortColumn,
                request.SortOrder,
                request.Department,
                request.Room,
                request.SeveralLevel,
                request.RequestStatus,
                request.Page,
                request.Limit
            ));
            return Ok(status);
        }

    }

    public class FieldSSFPProcessByAssignees
    {
        [FromRoute(Name = "accountId")]
        public string AccountIdAssignees { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? Department { get; set; }
        public string? Room { get; set; }
        public string? SeveralLevel { get; set; }
        public string? RequestStatus { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
