using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface INotificationRemarkRepository : IGenericRepository<NotificationRemark>
    {
        Task<NotificationRemark> GetNotifiRemarkById(Guid id);
        Task<List<NotificationRemark>> GetNotificationRemarkByRequestId(Guid requestId);
        Task<List<NotificationRemark>> GetNotificationRemarkByAccountId(string accountId);
    }
}
