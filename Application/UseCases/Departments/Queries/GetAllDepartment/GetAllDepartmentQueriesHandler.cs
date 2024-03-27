using Application.Common.Messaging;
using Application.DTOs.Departments;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;


namespace Application.UseCases.Departments.Queries.GetAllDepartment
{
    public sealed class GetAllDepartmentQueriesHandler
        : IQueryHandler<GetAllDepartmentQueries, IEnumerable<DepartmentDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        public GetAllDepartmentQueriesHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<DepartmentDTO>>> Handle(GetAllDepartmentQueries request, CancellationToken cancellationToken)
        {
            var list = await _repo.departmentRepo.GetAllDepartment();
            if (list == null)
            {
                return Result.Failure<IEnumerable<DepartmentDTO>>(new Error("Error.Empty", "data null"), "List Department is Null");
            }
            var resultList = _mapper.Map<IEnumerable<DepartmentDTO>>(list);
            return Result.Success<IEnumerable<DepartmentDTO>>(resultList, "Get List Department successfully !");
        }
    }
}
