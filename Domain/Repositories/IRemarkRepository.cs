using Domain.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IRemarkRepository : IGenericRepository<Remark>
    {
        Task<List<Remark>> GetRemarksRequestId(Guid requestId);
        Task<List<Remark>> GetRemarksByAccountId(string accountId);
        Task<Remark> GetLatestRemark(string remarkId);

    }
}
