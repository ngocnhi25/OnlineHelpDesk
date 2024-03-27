using Application.Common.Messaging;
using Application.DTOs.Auth;
using Application.Services;
using Domain.Repositories;
using SharedKernel;
using System.Security.Claims;

namespace Application.UseCases.Accounts.Commands.Login
{
    public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, LoginDTO>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly IBCryptService _encryptService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(
            IUnitOfWorkRepository repo,
            IBCryptService encryptService,
            ITokenService tokenService)
        {
            _repo = repo;
            _encryptService = encryptService;
            _tokenService = tokenService;
        }

        public async Task<Result<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.accountRepo.GetByAccountId(request.AccountId);
            var errorLogin = new Error("Error.Login", "Client errors");
            if (user == null)
                return Result.Failure<LoginDTO>(errorLogin, "Login faild! Incorrect Account code or Password.");

            if (user.IsBanned)
                return Result.Failure<LoginDTO>(new Error("Error.Login", "Account Banned"), "Your account has been banned.");

            var checkPassword = _encryptService.DecryptString(request.Password, user.Password);
            if (!checkPassword)
                return Result.Failure<LoginDTO>(errorLogin, "Login faild! Incorrect Account code or Password.");

            if(user.StatusAccount == StaticVariables.StatusAccountUser[0] ||
                user.StatusAccount == StaticVariables.StatusAccountUser[1])
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Sid, user.AccountId),
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role!.RoleTypes!.RoleTypeName),
                        new Claim("RoleName", user.Role!.RoleName),
                    };

                var token = _tokenService.GetToken(claims);
                var refreshToken = _tokenService.GetRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                _repo.accountRepo.Update(user);

                try
                {
                    await _repo.SaveChangesAsync(cancellationToken);

                    var loginResponse = new LoginDTO
                    {
                        AccountId = user.AccountId,
                        RoleName = user.Role.RoleName,
                        Email = user.Email,
                        RoleTypeName = user.Role.RoleTypes.RoleTypeName,
                        Enable = user.Enable,
                        Access_token = token.TokenString!,
                        Refresh_token = refreshToken,
                        Expiration = token.ValidTo
                    };

                    return Result.Success<LoginDTO>(loginResponse, "Login Successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Error error = new("Error.LoginCommandHandler", "There is an error saving data!");
                    return Result.Failure<LoginDTO>(error, "Login failed!");
                }
            }

            if(user.StatusAccount == StaticVariables.StatusAccountUser[2])
                return Result.Failure<LoginDTO>(errorLogin, "Your account is temporarily banned from logging in.");
            
            if(user.StatusAccount == StaticVariables.StatusAccountUser[3])
                return Result.Failure<LoginDTO>(errorLogin, "Your account has been banned from logging in.");

            return Result.Failure<LoginDTO>(errorLogin, "Login faild! Incorrect Account code or Password.");
        }
    }
}
