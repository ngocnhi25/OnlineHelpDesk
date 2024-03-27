using Application.Common.Messaging;
using Application.DTOs.Accounts;
using Application.Services;
using Domain.Repositories;
using SharedKernel;
using System.Security.Claims;

namespace Application.UseCases.Accounts.Commands.StaySignIn
{
    public sealed class StaySignInCommandHandler : ICommandHandler<StaySignInCommand, StaySignInResponse>
    {
        private readonly IUnitOfWorkRepository _repo;
        private readonly ITokenService _tokenService;

        public StaySignInCommandHandler(IUnitOfWorkRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<Result<StaySignInResponse>> Handle(StaySignInCommand request, CancellationToken cancellationToken)
        {
            var acc = await _repo.accountRepo.GetStaySignIn(request.AccountId, request.RefreshToken);
            var errorLogin = new Error("Error.Login", "Account no longer active");
            if (acc == null)
                return Result.Failure<StaySignInResponse>(new Error("Error.Client", "No data exists"), "The account code or refresh token does not exist.");

            if (acc.IsBanned)
                return Result.Failure<StaySignInResponse>(new Error("Error.Client", "Account Banned"), "Your account has been banned.");

            if (acc.RefreshTokenExpiry < DateTime.UtcNow)
                return Result.Failure<StaySignInResponse>(new Error("Error.Client", "No data exists"), "RefreshToken has expired.");

            if (acc.StatusAccount == StaticVariables.StatusAccountUser[0] ||
                acc.StatusAccount == StaticVariables.StatusAccountUser[1])
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, acc.AccountId),
                    new Claim(ClaimTypes.Name, acc.FullName),
                    new Claim(ClaimTypes.Email, acc.Email),
                    new Claim(ClaimTypes.Role, acc.Role!.RoleTypes!.RoleTypeName),
                    new Claim("RoleName", acc.Role!.RoleName),
                };

                var token = _tokenService.GetToken(claims);

                var loginResponse = new StaySignInResponse
                {
                     Access_token = token.TokenString!,
                     Refresh_token = acc.RefreshToken!,
                     Expiration = token.ValidTo
                };

                return Result.Success<StaySignInResponse>(loginResponse, "Login Successfully!");
            }

            if (acc.StatusAccount == StaticVariables.StatusAccountUser[2])
                return Result.Failure<StaySignInResponse>(errorLogin, "Your account is temporarily banned from logging in.");

            if (acc.StatusAccount == StaticVariables.StatusAccountUser[3])
                return Result.Failure<StaySignInResponse>(errorLogin, "Your account has been banned from logging in.");

            return Result.Failure<StaySignInResponse>(errorLogin, "Login faild! Incorrect Account code or Password.");
        }
    }
}
