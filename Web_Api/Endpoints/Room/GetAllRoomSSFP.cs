using Application.UseCases.Rooms.Queries.GetAllRoomSSFP;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Room
{
    public class GetAllRoomSSFP : EndpointBaseAsync
        .WithRequest<FieldRoom>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRoomSSFP(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/room/getAllRoomSSFP")]
        public async override Task<ActionResult<Result>> HandleAsync(
           [FromQuery] FieldRoom request, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send
                 (new GetAllRoomSSFPQueries(
                 request.SearchTerm,request.SortDepartmentName ,request.Page, request.Limit));
            return Ok(status);
        }
    }

    public class FieldRoom
    {
        public string? SearchTerm { get; set; }
        public string? SortDepartmentName { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}

