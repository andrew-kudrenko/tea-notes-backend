using TeaNotes.Auth.Models;
using TeaNotes.Users.Models;

namespace TeaNotes.Auth.Controllers.Login
{
    public record LoginResponse(User User, AuthToken AccessToken);
}
