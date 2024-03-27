using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificationRemarkRepository : GenericRepository<NotificationRemark>, INotificationRemarkRepository
    {

        public NotificationRemarkRepository(OHDDbContext dbContext)
           : base(dbContext) { }

        public async Task<NotificationRemark> GetNotifiRemarkById(Guid id)
        {
            var notificationRemark = await _dbContext.Set<NotificationRemark>().Where(x => x.Id == id).FirstOrDefaultAsync();
            return notificationRemark;
        }

        public async Task<List<NotificationRemark>> GetNotificationRemarkByRequestId(Guid requestId)
        {
            var listNotifiRemarkByRequestId = await _dbContext.Set<NotificationRemark>().
                                        Include(n => n.Account).ThenInclude(n => n.Role).ThenInclude(n => n.RoleTypes).
                                        Where(re => re.RequestId == requestId).ToListAsync();
            return listNotifiRemarkByRequestId;
        }

        public async Task<List<NotificationRemark>> GetNotificationRemarkByAccountId(string accountId)
        {
            var listNotifiRemarkByAccountId = await _dbContext.Set<NotificationRemark>().
                                        Include(n => n.Account).ThenInclude(n => n.Role).ThenInclude(n => n.RoleTypes).
                                        Where(noti => noti.AccountId == accountId).ToListAsync();
            return listNotifiRemarkByAccountId;
        }

    }
}
