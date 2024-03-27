using Application.Common.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Queries.CheckEmail
{
    public sealed class CheckEmailQueryHandler : IQueryHandler<CheckEmailQuery>
    {
        private readonly IUnitOfWorkRepository _repo;

        public CheckEmailQueryHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(CheckEmailQuery request, CancellationToken cancellationToken)
        {
            if(request.AccountId == null)
            {
                var acc = await _repo.accountRepo.GetByEmail(request.Email);
                if (acc != null)
                    return Result.Failure(new Error("Error.Client", "data duplication"), "Email already exists.");

                return Result.Success("Valid email.");
            } else
            {
                var acc = await _repo.accountRepo.GetByEmailEdit(request.AccountId, request.Email);
                if (acc != null)
                    return Result.Failure(new Error("Error.Client", "data duplication"), "Email already exists.");

                return Result.Success("Valid email.");
            }
            
        }
    }
}
