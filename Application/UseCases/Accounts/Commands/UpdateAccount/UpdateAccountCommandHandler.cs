using Application.Common.Messaging;
using Application.Services;
using Domain.Entities.Accounts;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.UpdateAccount
{
    public class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IBCryptService _bCryptService;
        private readonly IFileService _fileService;

        public UpdateAccountCommandHandler(IUnitOfWorkRepository repo, IBCryptService bCryptService, IFileService fileService)
        {
            _repo = repo;
            _bCryptService = bCryptService;
            _fileService = fileService;
        }

        public async Task<Result> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var acccount = await _repo.accountRepo.GetByAccountId(request.AccountId!);
            if(acccount == null) 
                return Result.Failure(new Error("Error.Client", "Data does not exist"), "Account does not exist.");

            string oldFileName = acccount.AvatarPhoto;
            string editFileName = null;

            var checkPhone = await _repo.accountRepo.GetByPhoneNumberEdit(request.AccountId!, request.PhoneNumber!);
            if (checkPhone != null)
                return Result.Failure(new Error("Error.Client", "Data duplication"), "Phone number already exists.");

            if (request.ImageFile != null)
            {
                var fileResult = await _fileService.SaveImage(request.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    editFileName = fileResult.Item2;
                    acccount.AvatarPhoto = editFileName;
                }
                else
                {
                    return Result.Failure(new Error("Error.Client", "Wrong data type"), fileResult.Item2);
                }
            }

            acccount.AccountId = request.AccountId!;
            acccount.RoleId = request.RoleId;
            acccount.FullName = request.FullName!;
            acccount.Address = request.Address!;
            acccount.PhoneNumber = request.PhoneNumber!;
            acccount.Gender = request.Gender!;
            acccount.Birthday = request.Birthday!;
            acccount.UpdatedAt = DateTime.UtcNow;

            _repo.accountRepo.Update(acccount);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                if (request.ImageFile != null)
                {
                    await _fileService.DeleteImage(oldFileName);
                }

                return Result.Success("Edit " + request.AccountId + " Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await _repo.RollbackTransactionAsync(cancellationToken);
                await _fileService.DeleteImage(editFileName);

                Error error = new("Error.RegisterCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Register failed!");
            }
        }
    }
}
