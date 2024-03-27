using Application.DTOs.Auth;
using System.Security.Claims;

namespace Application.Services
{
    public interface ITokenService
    {
        TokenDTO GetToken(IEnumerable<Claim> claims);
        string GetRefreshToken();
    }
}
