using Application.Common.Messaging;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Problems.Queries.GetAllProblem
{
    public sealed class GetAllProblemQueryHandler : IQueryHandler<GetAllProblemQuery, List<ProblemDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllProblemQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<ProblemDTO>>> Handle(GetAllProblemQuery request, CancellationToken cancellationToken)
        {
            var listProblems = await _repo.problemRepo.GetAllProblem();
            var resultList = _mapper.Map<List<ProblemDTO>>(listProblems);
            return resultList;
        }
    }
}
