using System;
using Application.Common.Messaging;
using Application.DTOs.Accounts;
using Application.DTOs.Departments;
using Application.UseCases.Departments.Queries.GetAllDepartment;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Assigness.Queries.GetAllAssignee
{
    public sealed class GetAllAssigneeHandler
         : IQueryHandler<GetAllAssigneeQueries, IEnumerable<AccountResponse>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;
        public GetAllAssigneeHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AccountResponse>>> Handle(GetAllAssigneeQueries request, CancellationToken cancellationToken)
        {
            var list = await _repo.assigneesRepo.GetAllAssignee();
            if (list == null)
            {
                return Result.Failure<IEnumerable<AccountResponse>>(new Error("Error.Empty", "data null"), "List Assignee is Null");
            }
            var resultList = _mapper.Map<IEnumerable<AccountResponse>>(list);
            return Result.Success<IEnumerable<AccountResponse>>(resultList, "Get List Assignee successfully !");
        }
    }
}

