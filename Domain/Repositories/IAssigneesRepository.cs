using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using SharedKernel;

namespace Domain.Repositories
{
    public interface IAssigneesRepository : IGenericRepository<ProcessByAssignees>
    {
        Task<Account?> GetAssigneesByAccountId(string accountId);
        Task<ProcessByAssignees?> GetByAssigneeHandleRequest(string assigneesId , Guid requestId);

        Task<DataResponse<ProcessByAssignees?>> GetListRequestOfSingleAssigneeSSFP
        (string SearchTerm, string SortColumn, string SortOrder, string SortStatus,
        string AccountId, int Page, int Limit);

        Task<DataResponse<Request?>> GetAllPendingRequestOfAssigneeSSFP
        (string AccountId,string? searchTerm, string? sortColumn, string? sortOrder, string? sortStatus,
        int page, int pageSize, CancellationToken cancellationToken);

        Task<ProcessByAssignees?> GetRequestById(Guid requestId);
        Task<List<ProcessByAssignees>> GetListByAssigneeHandleRequest(Guid requestId);
        Task<List<ProcessByAssignees>> GetListHandleRequestOfOneAssigneeByAccountId(string accountId);

        Task<IEnumerable<Account?>> GetAllAssignee();
    }
}

