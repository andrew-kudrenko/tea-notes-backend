namespace TeaNotes.Notes.Models
{
    public record TeaNoteAftertaste
    {
        public required string Comment { get; set; }
        public required int? Duration { get; set; }
        public required int? Intensity { get; set; }
    }
}
