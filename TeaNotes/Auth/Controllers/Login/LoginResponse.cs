using TeaNotes.Auth.Models;

namespace TeaNotes.Auth.Controllers.Login
{
    public record LoginResponse
    {
        public required User.Models.User User { get; set; }
        public required AuthTokenPair Tokens { get; set; }
    }
}
