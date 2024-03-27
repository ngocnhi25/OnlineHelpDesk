using System;
using Application.Common.Messaging;
using Application.DTOs.Accounts;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Assigness.Queries.GetAssigneeById
{
    public class GetAssigneeByIdHandler : IQueryHandler<GetAssigneeByIdQueries, AccountResponse>
    {

        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAssigneeByIdHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<AccountResponse>> Handle(
            GetAssigneeByIdQueries request,
            CancellationToken cancellationToken)
        {
            string id = request.AccountId;
            var requestQueryObj = await _repo.assigneesRepo.GetAssigneesByAccountId(id);
            if (requestQueryObj == null)
            {
                return Result.Failure<AccountResponse>(new Error("Error.Empty", "data null"), "Account is Null");
            }

            var resultObject = _mapper.Map<AccountResponse>(requestQueryObj);
            return Result.Success<AccountResponse>(resultObject, "Get infor assignee successfully !");
        }
    }
}

