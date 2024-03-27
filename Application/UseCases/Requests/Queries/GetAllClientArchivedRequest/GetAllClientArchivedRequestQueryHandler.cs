using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetAllClientArchivedRequest
{
    public sealed class GetAllClientArchivedRequestQueryHandler
        : IQueryHandler<GetAllClientArchivedRequestQuery, PagedList<RequestDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllClientArchivedRequestQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<RequestDTO>>> Handle(GetAllClientArchivedRequestQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.accountRepo.GetByAccountId(request.AccountId!);
            if (user == null)
            {
                return (Result<PagedList<RequestDTO>>)Result.Success("You don't have any requests yet");
            }

            var list = await _repo.requestRepo.GetAllClientUnenableRequestSSFP(request.AccountId,
                request.FCondition, request.SCondition, request.TCondition,
                request.SearchTerm, request.SortColumn, request.SortOrder,
                request.Page, request.Limit, cancellationToken);
            if (list == null)
            {
                return Result.Failure<PagedList<RequestDTO>>(new Error("Error.Empty", "data null"), "List Request is Null");
            }
            var resultList = _mapper.Map<List<RequestDTO>>(list.Items);
            var resultPageList = new PagedList<RequestDTO>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = list.TotalCount
            };

            return Result.Success<PagedList<RequestDTO>>(resultPageList, "Get List Request successfully !");
        }
    }
}
