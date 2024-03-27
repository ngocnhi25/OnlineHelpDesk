using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RemarkRepository : GenericRepository<Remark>, IRemarkRepository
    {
        public RemarkRepository(OHDDbContext dbContext)
            : base(dbContext) { }

        public async Task<List<Remark>> GetRemarksRequestId(Guid requestId)
        {
            var listRemarks = await _dbContext.Set<Remark>()
                .Where( r => r.RequestId == requestId)
                .Include(a => a.Account)
                .Include(r => r.Request).ThenInclude(ro => ro.Room).ThenInclude(de => de.Departments)
                .OrderBy(r => r.CreateAt)
                .ToListAsync();

            return listRemarks;
        }

        public async Task<List<Remark>> GetRemarksByAccountId(string accountId)
        {
            var distinctRemarkList= await _dbContext.Set<Remark>()
                .Where(r => r.AccountId == accountId)
                .Include(r => r.Account)
                .GroupBy(r => r.RequestId) // Nhóm theo RequestId
                .Select(group => group.OrderByDescending(r => r.CreateAt).First()) // Chọn record đầu tiên với ngày chat mới nhất từ mỗi nhóm
                .ToListAsync();

            return distinctRemarkList;
        }

        public async Task<Remark> GetLatestRemark(string remarkId)
        {
            Guid guidRemarkId = Guid.Parse(remarkId);
            var latestRemark = await _dbContext.Set<Remark>()
                .Where(r => r.Id == guidRemarkId)
                .Include(a => a.Account)
                .Include(re => re.Request)
                .FirstOrDefaultAsync();
            return latestRemark;
        }
    }
}
