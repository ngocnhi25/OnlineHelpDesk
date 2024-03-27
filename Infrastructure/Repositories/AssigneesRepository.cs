using System.Linq.Expressions;
using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Repositories
{
    public sealed class AssigneesRepository : GenericRepository<ProcessByAssignees>, IAssigneesRepository
    {
        public AssigneesRepository(OHDDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Account?>> GetAllAssignee()
        {
            var list = await _dbContext.Set<Account>()
                 .Where(a => a.RoleId == 4 && a.Enable == true && a.IsBanned == false)
                 .ToListAsync()
                 ;
            return list;
        }

        public async Task<DataResponse<Request?>> GetAllPendingRequestOfAssigneeSSFP(
            string AccountId,
            string? searchTerm, string? sortColumn, string? sortOrder,
            string? sortStatus, int page, int pageSize, CancellationToken cancellationToken)
        {
            var fiveDaysAgo = DateTime.Now.AddHours(-120);

            IQueryable<Request> requestQuery = _dbContext.Set<Request>()
             .Include(u => u.RequestStatus)
             .Include(i => i.Account)
             .Include(cu => cu.ProcessByAssignees!)
             .ThenInclude(i => i.Account)
             .Include(r => r.Room).ThenInclude(de => de!.Departments)
             .Where(r => r.CreatedAt <= fiveDaysAgo
                && !new[] { 6, 7 }.Contains(r.RequestStatusId)
                && r.ProcessByAssignees!.Any(pe => pe.AccountId == AccountId));

            if (!string.IsNullOrEmpty(searchTerm))
            {
                requestQuery = requestQuery.Where(a =>
                    a.Room!.RoomNumber.Contains(searchTerm) ||
                    a.Room!.Departments!.DepartmentName.Contains(searchTerm) ||
                    a.ProcessByAssignees!.Any(p => p.AccountId.Contains(searchTerm))
                );
            }

            if (!string.IsNullOrEmpty(sortStatus))
            {
                requestQuery = requestQuery
                    .Where(a => a.RequestStatus!.StatusName == sortStatus);
            };

            Expression<Func<Request, object>> keySelector = sortColumn?.ToLower() switch
            {
                _ => request => request.CreatedAt
            };

            // Adjust the ordering based on sortOrder
            if (sortOrder?.ToLower() == "asc")
            {
                requestQuery = requestQuery.OrderBy(keySelector);
            }
            else
            {
                requestQuery = requestQuery.OrderByDescending(keySelector);
            }

            var totalCount = await requestQuery.CountAsync();

            var requests = await requestQuery
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return new DataResponse<Request?>
            {
                Items = requests, 
                TotalCount = totalCount,
            };
        }

        public async Task<Account?> GetAssigneesByAccountId(string accountId)
        {
            var user = await _dbContext.Set<Account>()
                 .SingleOrDefaultAsync(u => u.AccountId == accountId);
            return user;
        }

        public async Task<ProcessByAssignees?> GetByAssigneeHandleRequest(string assigneesId, Guid requestId)
        {
            var processRequest = await _dbContext.Set<ProcessByAssignees>().SingleOrDefaultAsync
                (u => u.AccountId == assigneesId && u.RequestId == requestId);
            return processRequest;    
        }

        public async Task<List<ProcessByAssignees>> GetListByAssigneeHandleRequest(Guid requestId)
        {
            var listProcessRequest = await _dbContext.Set<ProcessByAssignees>().Where(ai => ai.RequestId == requestId).ToListAsync();
            return listProcessRequest;
        }
        public async Task<List<ProcessByAssignees>> GetListHandleRequestOfOneAssigneeByAccountId(string accountId)
        {
            var listProcessRequestByAccountId = await _dbContext.Set<ProcessByAssignees>().Where(ai => ai.AccountId == accountId).ToListAsync();
            return listProcessRequestByAccountId;
        }


        public async Task<DataResponse<ProcessByAssignees?>> GetListRequestOfSingleAssigneeSSFP
            (string AccountId, string SearchTerm, string SortColumn, string SortOrder,string SortStatus,
             int Page, int Limit)
        {
            IQueryable<ProcessByAssignees> requestQuery = _dbContext.Set<ProcessByAssignees>()
                .Include(re =>re.Request)
                    .ThenInclude( cu => cu!.Room).ThenInclude( h => h!.Departments)
                    .Include(res =>  res.Request!.RequestStatus)
                    .Where(a => a.AccountId == AccountId)
                ;


            if (!string.IsNullOrEmpty(SearchTerm))
            {
                requestQuery = requestQuery.Where(a =>
                    a.Request!.Room!.RoomNumber.Contains(SearchTerm) ||
                    a.Request.Room.Departments!.DepartmentName.Contains(SearchTerm)
                );
            }

            if (!string.IsNullOrEmpty(SortStatus))
            {
                requestQuery = requestQuery
                    .Where(a => a.Request!.RequestStatus!.StatusName == SortStatus);
            };

            Expression<Func<ProcessByAssignees, object>> keySelector = SortColumn?.ToLower() switch
            {
                "createdat" => ProcessByAssignees => ProcessByAssignees.Request!.CreatedAt,
                "requeststatusid" => ProcessByAssignees => ProcessByAssignees.Request!.RequestStatusId,
                _ => ProcessByAssignees => ProcessByAssignees.Request!.CreatedAt
            } ;


            if (SortOrder?.ToLower() == "asc")
            {
                requestQuery = requestQuery.OrderBy(keySelector);
            }
            else
            {
                requestQuery = requestQuery.OrderByDescending(keySelector);
            };

            var totalCount = await requestQuery
                .CountAsync();

            var requests = await requestQuery
                .Skip((Page - 1) * Limit)
                .Take(Limit)
                .ToListAsync();


            return new DataResponse<ProcessByAssignees?>
            {
                Items = requests,
                TotalCount = totalCount,
            };
        }

        public async Task<ProcessByAssignees?> GetRequestById(Guid requestId)
        {
            var result = await _dbContext.Set<ProcessByAssignees>().SingleOrDefaultAsync
            (u =>  u.RequestId == requestId);
            return result;
        }
    }
}

