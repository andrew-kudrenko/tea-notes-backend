namespace TeaNotes.Notes.Models
{
    public record TeaNoteGeneralInfo
    {
        public required string Title { get; set; }
        public required string? Kind { get; set; }
        public required string Region { get; set; }
        public required string Manufacturer { get; set; }
        public required int? ManufacturingYear { get; set; }
        public required int? PricePerGram { get; set; }
        public required DateOnly TastingDate { get; set; }
    }
}
