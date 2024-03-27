using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Accounts;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Queries.GetAllAccount
{
    public sealed class GetAllAccountQueryHandler :
        IQueryHandler<GetAllAccountQuery,
            PagedList<AccountResponse>>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAccountQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<PagedList<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
        {
            var listAccount = await _repo.accountRepo.GetAllAccountSSFP(request.SearchTerm, request.SortColumn, request.SortOrder, request.RoleName, request.AccountStatus, request.Page, request.Limit, cancellationToken);

            if (listAccount.Items == null)
            {
                return Result.Failure<PagedList<AccountResponse>>(new Error("Error.Empty", "data null"), "List Account is Null");
            }
            var resultList = _mapper.Map<List<AccountResponse>>(listAccount.Items);
            var resultPageList = new PagedList<AccountResponse>
            {
                Items = resultList,
                Page = request.Page,
                Limit = request.Limit,
                TotalCount = listAccount.TotalCount
            };
            return Result.Success<PagedList<AccountResponse>>(resultPageList, "Get list account successfully!");
        }
    }
}
