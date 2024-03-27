using Application.Services;

namespace Infrastructure.Services
{
    public class BCryptService : IBCryptService
    {
        public bool DecryptString(string value, string valueHash)
        {
            var result = BCrypt.Net.BCrypt.Verify(value, valueHash);
            return result;
        }

        public string EncodeString(string value)
        {
            var result = BCrypt.Net.BCrypt.HashPassword(value);
            return result;
        }
    }
}
