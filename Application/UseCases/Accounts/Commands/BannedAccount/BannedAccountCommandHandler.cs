using Application.Common.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.BannedAccount
{
    public sealed class BannedAccountCommandHandler : ICommandHandler<BannedAccountCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public BannedAccountCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(BannedAccountCommand request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetByAccountId(request.AccountId);
            if (acc == null)
                return Result.Failure(new Error("Error.Client", "No data exists"), "The account code does not exist. Please double-check your account code.");

            if (acc.Enable == false)
                return Result.Failure(new Error("Error.Client", "banned account error"), "Unverified accounts, accounts cannot be banned.");

            if(acc.Role!.RoleTypes!.RoleTypeName == "Administrator")
                return Result.Failure(new Error("Error.Client", "banned account error"), "You do not have the right to ban another admin account");

            acc.IsBanned = true;
            acc.StatusAccount = StaticVariables.StatusAccountUser[2];
            _repo.accountRepo.Update(acc);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);

                return Result.Success("Banned account " + request.AccountId + " successfully");
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
