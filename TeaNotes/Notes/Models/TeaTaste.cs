using System.ComponentModel.DataAnnotations;
using TeaNotes.Common;

namespace TeaNotes.Notes.Models
{
    public class TeaTaste
    {
        public int Id { get; set; }

        [RegularExpression(@"^(sweet|salty|sour|bitter|umami)$", ErrorMessage = ModelErrorMessages.RegEx)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public required string Kind { get; set; }
        public int TeaNoteId { get; set; }
        public TeaNote TeaNote { get; set; } = null!;
    }
}
