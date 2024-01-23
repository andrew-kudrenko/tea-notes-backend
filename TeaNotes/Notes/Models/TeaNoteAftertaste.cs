namespace TeaNotes.Notes.Models
{
    public record TeaNoteAftertaste
    {
        public required string Comment { get; set; }
        public required double? Duration { get; set; }
        public required double? Intensity { get; set; }
    }
}
