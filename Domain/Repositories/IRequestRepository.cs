using Domain.Entities.Requests;
using SharedKernel;

namespace Domain.Repositories
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<DataResponse<Request>> GetAllRequestSSFP
            (string? searchTerm, string? sortColumn, string? sortOrder,string? sortStatus,
            int page, int pageSize, CancellationToken cancellationToken);
        Task<DataResponse<Request>> GetAllPendingRequestSSFP
            (string? searchTerm, string? sortColumn, string? sortOrder, string? sortStatus,
            int page, int pageSize, CancellationToken cancellationToken);

        Task<DataResponse<Request>> GetAllClientEnableRequestSSFP
           (string? AccountId , 
            string? FCondition, string? SCondition, string? TCondition,
            string? searchTerm, string? sortColumn, string? sortOrder,
           int page, int pageSize, CancellationToken cancellationToken);
        Task<DataResponse<Request>> GetAllClientUnenableRequestSSFP
           (string? AccountId,
            string? FCondition, string? SCondition, string? TCondition,
            string? searchTerm, string? sortColumn, string? sortOrder,
           int page, int pageSize, CancellationToken cancellationToken);

        Task<Request?> GetRequestById(Guid id);
        Task<Request?> GetRequestByRoomId(Guid id);
        Task<List<Request>> GetAllRequestWithoutSSFP(string accountId);
        Task<RequestCountRespone?> GetCountRequest();
        Task<RequestCountRespone?> GetCountRequestByAssignees(string id);
        Task<DataResponse<Request>> GetAllRequestOfAssigneeProcessingSSFP(
            string accountIdAssignees,
            string? searchTerm,
            string? sortColumn,
            string? sortOrder,
            string? department,
            string? room,
            string? severalLevel,
            string? status,
            int page,
            int limit,
            CancellationToken cancellationToken
        );
    }
}

