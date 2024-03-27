using Domain.Entities.Departments;
using SharedKernel;

namespace Domain.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department?> GetDepartmentById(Guid id);
        Task<IEnumerable<Department?>> GetAllDepartment();

        Task<Department?> GetDepartmentByName(string departmentName);

        Task<DataResponse<Department>> GetListDepartmentSSFP
    (string? searchTerm, int page, int pageSize, CancellationToken cancellationToken);

        Task<int> CountRoomInActive(Guid id);
    }
}
