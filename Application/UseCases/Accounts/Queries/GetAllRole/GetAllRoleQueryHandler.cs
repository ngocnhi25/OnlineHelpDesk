using Application.Common.Messaging;
using Application.DTOs.Accounts;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Queries.GetAllRole
{
    public sealed class GetAllRoleQueryHandler : IQueryHandler<GetAllRoleQuery, List<RoleDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRoleQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Result<List<RoleDTO>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _repo.roleRepo.GetAllAsync();
            if (roles == null)
            {
                return Result.Failure<List<RoleDTO>>(new Error("Error.Empty", "data null"), "Empty account role data");
            }
            var resultList = _mapper.Map<List<RoleDTO>>(roles);
            return Result.Success<List<RoleDTO>>(resultList, "Get successful account role data!");
        }
    }
}
