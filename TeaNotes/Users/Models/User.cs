using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TeaNotes.Users.Models
{
    [Index(nameof(NickName), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string NickName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
