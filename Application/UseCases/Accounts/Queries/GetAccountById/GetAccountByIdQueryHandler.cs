using Application.Common.Messaging;
using Application.DTOs.Accounts;
using AutoMapper;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Queries.GetAccountById
{
    public sealed class GetAccountByIdQueryHandler : IQueryHandler<GetAccountByIdQuery, AccountResponse>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMapper _mapper;

        public GetAccountByIdQueryHandler(IUnitOfWorkRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetByAccountId(request.AccountId);
            if(acc == null)
                return Result.Failure<AccountResponse>(new Error("Error.Client", "No data exists"), "Account doesn't exist.");

            var result = _mapper.Map<AccountResponse>(acc);
            return Result.Success<AccountResponse>(result, "Get account data successfully!");
        }
    }
}
