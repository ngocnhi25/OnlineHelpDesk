using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Auth;
using Application.Services;
using Domain.Entities.Accounts;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Accounts.Commands.Register
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IMailService _mailService;
        private readonly IBCryptService _bCryptService;
        private readonly IRandomService _randomService;
        private readonly IFileService _fileService;

        public RegisterCommandHandler(IUnitOfWorkRepository repo, IMailService mailService, IBCryptService bCryptService, IRandomService randomService, IFileService fileService)
        {
            _repo = repo;
            _mailService = mailService;
            _bCryptService = bCryptService;
            _randomService = randomService;
            _fileService = fileService;
        }

        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            bool checkAccountId;
            string accountId;
            string newFileName;

            var checkEmail = await _repo.accountRepo.GetByEmail(request.Email);
            if (checkEmail != null)
                return Result.Failure(new Error("Error.Client", "Data duplication"), "Email already exists.");

            var checkPhone = await _repo.accountRepo.GetByPhoneNumber(request.PhoneNumber);
            if (checkPhone != null)
                return Result.Failure(new Error("Error.Client", "Data duplication"), "Phone number already exists.");

            if (request.ImageFile == null)
            {
                return Result.Failure(new Error("Error.Client", "Data empty"), "Profile picture can't blank.");
            } 
            else
            {
                var fileResult = await _fileService.SaveImage(request.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    newFileName = fileResult.Item2;
                } else
                {
                    return Result.Failure(new Error("Error.Client", "Wrong data type"), fileResult.Item2);
                }
            }

            do
            {
                string code = await _randomService.RandomSevenNumberCode();
                switch (request.RoleId)
                {
                    case 1:
                        accountId = "ST" + code;
                        break;
                    case 2:
                        accountId = "TC" + code;
                        break;
                    case 3:
                        accountId = "FH" + code;
                        break;
                    case 4:
                        accountId = "AS" + code;
                        break;
                    case 5:
                        accountId = "AD" + code;
                        break;
                    default:
                        return Result.Failure(new Error("Error.Client", "Data transfer errors"), "No role exists in the database.");
                }

                checkAccountId = await _repo.accountRepo.CheckRegisterAccount(accountId);

            } while (!checkAccountId);

            string password = "@Asd1234";
            string hashPassword = _bCryptService.EncodeString(password);

            var userRegister = new Account
            {
                AccountId = accountId,
                RoleId = request.RoleId,
                FullName = request.FullName!,
                Email = request.Email,
                Password = hashPassword,
                AvatarPhoto = newFileName,
                Address = request.Address!,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender!,
                Birthday = request.Birthday!,
                CreatedAt = DateTime.UtcNow,
                StatusAccount = StaticVariables.StatusAccountUser[0]
            };

            _repo.accountRepo.Add(userRegister);

            var sendMail = new MailRequest
            {
                ToEmail = request.Email,
                Subject = "Verify Confirmation",
                Body = "<br/>" + 
                       $"<h3>Username: {accountId}</h3>" +
                       $"<h4>Password: {password}</h4>" +
                       $"<br/>",
                Attachments = null
            };

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                await _mailService.SendMailAsync(sendMail);

                return Result.Success("Register Successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await _repo.RollbackTransactionAsync(cancellationToken);
                await _fileService.DeleteImage(newFileName);

                Error error = new("Error.RegisterCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Register failed!");
            }
        }
    }
}
