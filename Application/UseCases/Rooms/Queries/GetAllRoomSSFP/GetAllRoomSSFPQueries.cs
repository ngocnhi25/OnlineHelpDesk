using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Departments;

namespace Application.UseCases.Rooms.Queries.GetAllRoomSSFP
{
    public sealed record GetAllRoomSSFPQueries
		(string? SearchTerm,string? SortDepartmentName, int Page, int Limit)
        : IQuery<PagedList<RoomDTO>>;
}

