using TeaNotes.Users.Models;

namespace TeaNotes.Email
{
    public record ConfirmationCode
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Code { get; set; }
        public required DateTime ExpiresAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
