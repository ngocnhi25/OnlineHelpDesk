using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using Application.UseCases.Requests.Queries.GetAllRequest;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetAllClientRequest
{
    public sealed class GetAllClientRequestHandler
        : IQueryHandler<GetAllClienRequestQueries, PagedList<RequestDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllClientRequestHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<RequestDTO>>> Handle(GetAllClienRequestQueries request, CancellationToken cancellationToken)
        {

            var list = await _repo.requestRepo.GetAllClientEnableRequestSSFP(request.AccountId,
                request.FCondition, request.SCondition, request.TCondition,
                request.SearchTerm, request.SortColumn, request.SortOrder, 
                request.Page, request.Limit, cancellationToken);
            if (list == null)
            {
                return Result.Failure<PagedList<RequestDTO>>(new Error("Error.Empty", "data null"), "List Request is Empty");
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
