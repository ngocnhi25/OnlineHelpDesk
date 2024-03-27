namespace Application.DTOs.Auth
{
    public class TokenDTO
    {
        public string? TokenString { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
