namespace Application.Services
{
    public interface IBCryptService
    {
        bool DecryptString(string value, string valueHash);
        string EncodeString(string value);
    }
}
