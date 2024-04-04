using TeaNotes.Users.Models;

namespace TeaNotes.Email
{
    public class ConfirmationCodeGenerator
    {
        private readonly int _lifetime;

        public ConfirmationCodeGenerator(IConfiguration configuration) => _lifetime = int.Parse(configuration["Email:ConfirmationCodeLifetime"]!);

        public ConfirmationCode Generate(User user) => new()
        {
            Email = user.Email,
            ExpiresAt =  DateTime.Now.AddSeconds(_lifetime),
            Code = Guid.NewGuid().ToString(),
            UserId = user.Id,
            User = user,
        };
    }
}
