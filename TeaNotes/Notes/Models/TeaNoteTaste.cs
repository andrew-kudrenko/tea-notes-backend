using System.ComponentModel.DataAnnotations;

namespace TeaNotes.Notes.Models
{
    public class TeaNoteTaste
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Kind { get; set; } = string.Empty;
        [Required]
        public int TeaNoteId { get; set; }
        public TeaNote TeaNote { get; set; } = null!;
    }
}
