using TeaNotes.Auth.Models;
using TeaNotes.Users.Models;

namespace TeaNotes.Auth.Controllers.Login
{
    public record LoginResponse
    {
        public required User User { get; set; }
        public required AuthTokenPair Tokens { get; set; }
    }
}
