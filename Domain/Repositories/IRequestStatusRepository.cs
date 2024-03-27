using System;
using Domain.Entities.Departments;
using Domain.Entities.Requests;

namespace Domain.Repositories
{
	public interface IRequestStatusRepository : IGenericRepository<RequestStatus>
    {
		Task<IEnumerable<RequestStatus?>> GetAll(); 
		Task<RequestStatus?> GetRequestStatusById(int id);
	}
}

