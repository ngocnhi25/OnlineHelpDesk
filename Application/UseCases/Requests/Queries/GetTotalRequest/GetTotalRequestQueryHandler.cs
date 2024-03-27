using System.Threading;
using System.Threading.Tasks;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Accounts;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using SharedKernel;

namespace Application.UseCases.Assigness.Queries.GetTotalRequest
{
    public sealed class GetTotalRequestQueryHandler : IQueryHandler
        <GetTotalRequestQuery, CountRequestPage>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetTotalRequestQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<CountRequestPage>> Handle(GetTotalRequestQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.requestRepo.GetCountRequest();
            if (result == null)
            {
                return Result.Failure<CountRequestPage>(new Error("Error.Empty", "data null"), "Count Request is Fail");
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
            return Result.Success<CountRequestPage>(resultList, "Count Request successfully!");
        }
    }
}
