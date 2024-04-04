namespace TeaNotes.Auth.Models
{
    public record AuthTokenPair(AuthToken Access, AuthToken Refresh);
}
