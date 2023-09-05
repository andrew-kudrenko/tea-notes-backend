using TeaNotes.Auth.Models;

namespace TeaNotes.Auth.Controllers.Refresh
{
    public record RefreshResponse
    {
        public required AuthTokenPair Tokens { get; set; }
    }
}
