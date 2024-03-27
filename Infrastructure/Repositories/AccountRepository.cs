using Domain.Entities.Accounts;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public sealed class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
       public AccountRepository(OHDDbContext dbContext) 
            : base(dbContext) { }

        public async Task<bool> CheckRegisterAccount(string accountId)
        {
            var acc = await _dbContext.Set<Account>()
                .SingleOrDefaultAsync(u => u.AccountId == accountId);
            if (acc != null)
                return false;
            return true;
        }

        public async Task<List<Account>> GetAllAccount()
        {
            var listAccount = await _dbContext.Set<Account>().Include(rt => rt.Role).ThenInclude(rt => rt.RoleTypes).ToListAsync();
            return listAccount;
        }
        
        public async Task<List<Account>> GetAllFacilityHeads()
        {
            var listFacilityHeads = await _dbContext.Set<Account>().Where(a => a.Role!.RoleTypes!.Id == 2).ToListAsync();
            return listFacilityHeads;
        }

        public async Task<DataResponse<Account>> GetAllAccountSSFP(
            string? searchTerm, 
            string? sortColumn, 
            string? sortOrder,
            string? roleName,
            string? accountStatus,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<Account> accountQuery = _dbContext.Set<Account>()
                .Include(a => a.Role);

            if(!string.IsNullOrEmpty(searchTerm) )
            {
                accountQuery = accountQuery.Where(a => 
                a.AccountId.Contains(searchTerm) || 
                a.Email.Contains(searchTerm) || 
                a.FullName.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(roleName))
            {
                accountQuery = accountQuery.Where(a =>
                a.Role!.RoleName == roleName);
            }
            
            if (!string.IsNullOrEmpty(accountStatus))
            {
                accountQuery = accountQuery.Where(a =>
                a.StatusAccount == accountStatus);
            }

            Expression<Func<Account, object>> keySelector = sortColumn?.ToLower() switch
            {
                "accountid" => account => account.AccountId,
                "fullname" => account => account.FullName,
                "birthday" => account => account.Birthday,
                "statusaccount" => account => account.StatusAccount,
                "rolename" => account => account.Role!.RoleName,
                _ => account => account.CreatedAt,

            };

            if(sortOrder?.ToLower() == "asc")
            {
                accountQuery = accountQuery.OrderBy(keySelector);
            } else
            {
                accountQuery = accountQuery.OrderByDescending(keySelector);
            }

            var totalCount = await accountQuery.CountAsync();

            var accounts = await accountQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new DataResponse<Account>
            {
                Items = accounts,
                TotalCount = totalCount,
            };
        }

        public async Task<Account?> GetByAccountId(string accountId)
        {
            var user = await _dbContext.Set<Account>()
                .Include(a => a.Role)
                .ThenInclude(c => c.RoleTypes)
                .Include(r => r.Requests)
                .Include(rem => rem.Remarks)
                .SingleOrDefaultAsync(u => u.AccountId == accountId);
            return user;
        }

        public async Task<Account?> GetByEmail(string email)
        {
            var user = await _dbContext.Set<Account>().SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<Account?> GetByEmailEdit(string accountId, string email)
        {
            var user = await _dbContext.Set<Account>().SingleOrDefaultAsync(u => u.Email == email && u.AccountId != accountId);
            return user;
        }

        public async Task<Account?> GetByPhoneNumber(string phone)
        {
            var user = await _dbContext.Set<Account>().FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user;
        }

        public async Task<Account?> GetByPhoneNumberEdit(string accountId, string phoneNumber)
        {
            var user = await _dbContext.Set<Account>().FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.AccountId != accountId);
            return user;
        }

        public async Task<DataResponse<Account?>> GetListAssigneesSSFP(
            string? searchTerm,
            int page,
            int pageSize,
            CancellationToken cancellationToken)
        {
            IQueryable<Account> accountQuery = _dbContext.Set<Account>()
                .Where(u => u.RoleId == 4);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accountQuery = accountQuery.Where(a =>
                    a.AccountId.Contains(searchTerm) ||
                    a.Email.Contains(searchTerm) ||
                    a.FullName.Contains(searchTerm));
            }

            var totalCount = await accountQuery.CountAsync(cancellationToken);

            var accounts = await accountQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new DataResponse<Account?>
            {
                Items = accounts,
                TotalCount = totalCount
            };
        }


        public async Task<Account?> GetStaySignIn(string accountId, string refreshToken)
        {
            var user = await _dbContext.Set<Account>()
                .Include(a => a.Role)
                .ThenInclude(c => c.RoleTypes)
                .SingleOrDefaultAsync(u =>
                    u.AccountId == accountId &&
                    u.RefreshToken == refreshToken
                );
            return user;
        }
    }
}
