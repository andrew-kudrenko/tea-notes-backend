namespace TeaNotes.Auth.Models
{
    public record AuthToken(string Token, DateTime ExpiresAt);
}
