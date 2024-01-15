using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaNotes.Notes.Models
{
    [Index(nameof(Title), IsUnique = true)]
    public class TeaNote
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [StringLength(2_000)]
        public string Taste { get; set; } = string.Empty;
        [StringLength(2_000)]
        public string Aroma { get; set; } = string.Empty;
        [StringLength(50)]
        public string Temperature { get; set; } = string.Empty;
        [StringLength(2_000)]
        public string Feeling { get; set; } = string.Empty;
        public ICollection<TeaNoteTaste> Tastes { get; set; } = null!;
        public User.Models.User User { get; set; } = null!;
    }
}
