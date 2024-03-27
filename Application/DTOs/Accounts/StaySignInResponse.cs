namespace Application.DTOs.Accounts
{
    public class StaySignInResponse
    {
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
