namespace TeaNotes.Notes.Models
{
    public record TeaNoteImpression
    {
        public required string Comment { get; set; }
        public required string WellCombinedWith { get; set; }
        public required int? Rate { get; set; }
    }
}
