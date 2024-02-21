using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TeaNotes.Users.Models
{
    [Index(nameof(Nickname), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина не менее 3 и не более 30 символов")]
        public string Nickname { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Обязательно к заполнению")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsEmailVerified { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        
        [JsonIgnore]
        public DateTime RegisteredAt { get; set; }
    }
}
