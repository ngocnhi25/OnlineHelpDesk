using System;
using Application.Common.Messaging;
using Application.DTOs.Requests;
using Application.UseCases.Assigness.Queries.GetTotalRequest;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetTotalRequestByAssignees
{
    public sealed class GetTotalRequestByAssigneesQueryHandler
         : IQueryHandler
        <GetTotalRequestByAssigneesQuery, CountRequestPage>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetTotalRequestByAssigneesQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<CountRequestPage>> Handle(
            GetTotalRequestByAssigneesQuery request,
            CancellationToken cancellationToken)
        {
            string id = request.AccountId!;
            var IdOk = await _repo.assigneesRepo.GetAssigneesByAccountId(id);
            if( IdOk == null)
            {
                return Result.Failure<CountRequestPage>(new Error("Error.Empty", "data null"), "Id Account can not found !");
            }
            var result = await _repo.requestRepo.GetCountRequestByAssignees(id);
            if (result == null)
            {
                return Result.Failure<CountRequestPage>(new Error("Error.Empty", "data null"), "Count Request By Assignees is Fail");
            }
            var resultList = new CountRequestPage
            {
                All = result.All,
                Open = result.Open,
                Assigned = result.Assigned,
                WorkInProgress = result.WorkInProgress,
                NeedMoreInfo = result.NeedMoreInfo,
                Rejected = result.Rejected,
                Complete = result.Complete,
                Pending = result.Pending
            };
            return Result.Success<CountRequestPage>(resultList, "Count Request By Assignees successfully!");
        }
    }
}

