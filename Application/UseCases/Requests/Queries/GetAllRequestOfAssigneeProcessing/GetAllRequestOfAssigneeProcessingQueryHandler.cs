using Application.Common.Messaging;
using Application.DTOs.Accounts;
using Application.DTOs;
using Application.DTOs.Requests;
using Domain.Repositories;
using SharedKernel;
using AutoMapper;

namespace Application.UseCases.Requests.Queries.GetAllRequestOfAssigneeProcessing
{
    internal sealed class GetAllRequestOfAssigneeProcessingQueryHandler : IQueryHandler<GetAllRequestOfAssigneeProcessingQuery, PagedList<RequestDTO>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllRequestOfAssigneeProcessingQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<RequestDTO>>> Handle(GetAllRequestOfAssigneeProcessingQuery request, CancellationToken cancellationToken)
        {
            var listAccount = await _repo.requestRepo.GetAllRequestOfAssigneeProcessingSSFP(
                request.AccountIdAssignees,
                request.SearchTerm, 
                request.SortColumn, 
                request.SortOrder, 
                request.Department, 
                request.Room, 
                request.SeveralLevel, 
                request.Status, 
                request.Page, 
                request.Limit, 
                cancellationToken);

            if (listAccount.Items == null)
            {
                return Result.Failure<PagedList<RequestDTO>>(new Error("Error.Empty", "data null"), "List Account is Null");
            }
            var resultList = _mapper.Map<List<RequestDTO>>(listAccount.Items);
            var resultPageList = new PagedList<RequestDTO>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = listAccount.TotalCount
            };
            return Result.Success<PagedList<RequestDTO>>(resultPageList, "Get list account successfully!");
        }
    }
}
