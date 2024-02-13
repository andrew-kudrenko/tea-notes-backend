using TeaNotes.Users.Models;

namespace TeaNotes.Email
{
    public class ConfirmationCodeGenerator
    {
        private readonly int _lifetimeInMinutes = 15;

        public ConfirmationCode Generate(User user) => new()
        {
            Email = user.Email,
            ExpiresAt =  DateTime.Now.AddMinutes(_lifetimeInMinutes),
            Code = Guid.NewGuid().ToString(),
            UserId = user.Id,
            User = user,
        };
    }
}
