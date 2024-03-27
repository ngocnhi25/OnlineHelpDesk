using Application.Common.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.ActiveAccount
{
    public sealed class ActiveAccountCommandHandler : ICommandHandler<ActiveAccountCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public ActiveAccountCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(ActiveAccountCommand request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetByAccountId(request.AccountId);
            if (acc == null)
                return Result.Failure(new Error("Error.Client", "No data exists"), "The account code does not exist. Please double-check your account code.");

            if (acc.Enable == false)
                return Result.Failure(new Error("Error.Client", "banned account error"), "Unverified accounts, accounts cannot be active.");

            acc.IsBanned = false;
            acc.StatusAccount = StaticVariables.StatusAccountUser[1];
            _repo.accountRepo.Update(acc);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);

                return Result.Success("Account " + request.AccountId + " is opened successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.DeleteAccount", "There is an error saving data!");
                return Result.Failure(error, "Account code verification errors");
            }
        }
    }
}
