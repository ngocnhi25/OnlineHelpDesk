using Domain.Entities.Departments;
using SharedKernel;

namespace Domain.Repositories
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<Room?> GetRoomById(Guid id);
        Task<Room?> GetRoomByRoomNumber(string roomNumber);

        Task<DataResponse<Room>> GetListRoomSSFP
            (string? searchTerm,string? sortDepartmentName
            ,int page, int pageSize, CancellationToken cancellationToken);

    }
}
