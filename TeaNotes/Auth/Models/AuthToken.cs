namespace TeaNotes.Auth.Models
{
    public class AuthToken
    {
        public required string Token { get; set; }
        public required DateTime ExpiresAt { get; set; }
    }
}
