using Application.Common.Messaging;
using Application.Services;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.DeleteAccount
{
    internal sealed class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IFileService _fileService;

        public DeleteAccountCommandHandler(IUnitOfWorkRepository repo, IFileService fileService)
        {
            _repo = repo;
            _fileService = fileService;
        }

        public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetByAccountId(request.AccountId);
            if (acc == null)
                return Result.Failure(new Error("Error.Client", "No data exists"), "The account code does not exist. Please double-check your account code.");

            if(acc.Enable == true)
                return Result.Failure(new Error("Error.Client", "delete account error"), "The account has been verified, it cannot be deleted. ");

            var oldPhoto = acc.AvatarPhoto;
            _repo.accountRepo.Delete(acc);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                if(oldPhoto != null)
                {
                    await _fileService.DeleteImage(oldPhoto);
                }

                return Result.Success("Delete account " + request.AccountId + " successfully");
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
