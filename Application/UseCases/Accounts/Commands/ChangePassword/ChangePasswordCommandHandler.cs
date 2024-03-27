using Application.Common.Messaging;
using Application.Services;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IBCryptService _bCryptService;

        public ChangePasswordCommandHandler(IUnitOfWorkRepository repo, IBCryptService bCryptService)
        {
            _repo = repo;
            _bCryptService = bCryptService;
        }

        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var newPassword = request.NewPassword;
            var acc = await _repo.accountRepo.GetByEmail(request.Email);
            if (acc == null)
                return Result.Failure(new Error("Error.Client", "No data exists"), "The email does not exist. Please double-check your email address.");

            if (acc.IsBanned)
                return Result.Failure(new Error("Error.Client", "Account Banned"), "Your account has been banned.");

            if (!acc.Enable)
            {
                acc.Enable = true;
                acc.StatusAccount = StaticVariables.StatusAccountUser[1];
            }


            if (string.IsNullOrWhiteSpace(newPassword) &&
                newPassword.Length < 8 || newPassword.Length > 16 &&
                !newPassword.Any(char.IsDigit) &&
                !newPassword.Any(char.IsUpper) &&
                !newPassword.Any(char.IsLower) &&
                !newPassword.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                    return Result.Failure(new Error("Error.Client", "No data exists"), 
                        "Non-empty password. " +
                        "No whitespace in the password. " +
                        "At least one digit. " +
                        "At least one uppercase letter. " +
                        "At least one lowercase letter. " +
                        "At least one special character (letter or digit). " +
                        "Length between 8 and 16 characters");
            }

            var hasPassword = _bCryptService.EncodeString(newPassword);

            acc.Password = hasPassword;
            acc.UpdatedAt = DateTime.UtcNow;
            _repo.accountRepo.Update(acc);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                return Result.Success("Change password successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.ChangePassword", "There is an error saving data!");
                return Result.Failure(error, "ChangePassword errors");
            }
        }
    }
}
