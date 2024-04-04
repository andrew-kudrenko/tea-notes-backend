using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TeaNotes.Common;

namespace TeaNotes.Users.Models
{
    [Index(nameof(Nickname), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = ModelErrorMessages.StringLength)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public string Nickname { get; set; } = string.Empty;
        
        [EmailAddress(ErrorMessage = ModelErrorMessages.Email)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsEmailVerified { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        
        [JsonIgnore]
        public DateTime RegisteredAt { get; set; }
    }
}
