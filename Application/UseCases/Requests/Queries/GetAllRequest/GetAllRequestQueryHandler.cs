using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetAllRequest
{
    public sealed class GetAllRequestQueryHandler
        : IQueryHandler<GetAllRequestQuery, PagedList<RequestDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRequestQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<RequestDTO>>> Handle(GetAllRequestQuery request, CancellationToken cancellationToken)
        {
            var list = await _repo.requestRepo.GetAllRequestSSFP(request.SearchTerm, request.SortColumn, request.SortOrder,request.SortStatus ,request.Page, request.Limit, cancellationToken);
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

