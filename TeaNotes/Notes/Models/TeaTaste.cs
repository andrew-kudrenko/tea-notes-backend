using System.ComponentModel.DataAnnotations;

namespace TeaNotes.Notes.Models
{
    public class TeaTaste
    {
        public int Id { get; set; }

        [RegularExpression(@"^(sweet|salty|sour|bitter|umami)$")]
        public string Kind { get; set; } = string.Empty;
        public int TeaNoteId { get; set; }
        public TeaNote TeaNote { get; set; } = null!;
    }
}
