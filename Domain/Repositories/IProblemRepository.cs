using Domain.Entities.Requests;

namespace Domain.Repositories
{
    public interface IProblemRepository : IGenericRepository<Problem>
    {
        
        Task<List<Problem>> GetAllProblem();
        Task<Problem?> GetProblemById(int id);
    }
}
