using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TeaNotes.Users.Models
{
    [Index(nameof(NickName), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string NickName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsEmailVerified { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
