namespace TeaNotes.User.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
