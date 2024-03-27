using Application.Common.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.Verify
{
    public sealed class VerifyCommandHandler : ICommandHandler<VerifyCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public VerifyCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(VerifyCommand request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetByEmail(request.Email);
            if (acc == null)
                return Result.Failure(new Error("Error.Client", "No data exists"), "The email does not exist. Please double-check your email address.");

            if (acc.IsBanned)
                return Result.Failure(new Error("Error.Client", "Account Banned"), "Your account has been banned.");

            if (acc.VerifyCode != request.VerifyCode)
                return Result.Failure(new Error("Error.Client", "Data comparison errors"), "Incorrect verification code.");

            if(acc.VerifyRefreshExpiry < DateTime.UtcNow)
                return Result.Failure(new Error("Error.Client", "Data comparison errors"), "Confirmation time has expired.");

            return Result.Success("Successful verification.");
        }
    }
}
