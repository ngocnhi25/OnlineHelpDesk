using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Requests.Queries.GetListRequestOfSingleAssignee
{
    public sealed class GetListRequestOfSingleAssigneeHandler
        : IQueryHandler<GetListRequestOfSingleAssigneeQueries, PagedList<AssigneeResponse>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetListRequestOfSingleAssigneeHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<Result<PagedList<AssigneeResponse>>> Handle(GetListRequestOfSingleAssigneeQueries request, CancellationToken cancellationToken)
        {
            var requestQueryObj = await _repo.assigneesRepo.GetListRequestOfSingleAssigneeSSFP
                (request.AccountId , request.SearchTerm,request.SortColumn,request.SortOrder,request.SortStatus,request.Page,request.Limit);
            if (requestQueryObj.Items == null)
            {
                return Result.Failure<PagedList<AssigneeResponse>> (new Error("Error.Empty", "data null"), "List Request Of Assignee is Null");
            }
            var resultList = _mapper.Map<List<AssigneeResponse>>(requestQueryObj.Items);
            var resultPageList = new PagedList<AssigneeResponse>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = requestQueryObj.TotalCount
            };
            return Result.Success<PagedList<AssigneeResponse>> (resultPageList, "Get List Request Of Assignee successfully !");
        }

    }
}
