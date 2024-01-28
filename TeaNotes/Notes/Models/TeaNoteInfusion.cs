namespace TeaNotes.Notes.Models
{
    public record TeaNoteInfusion
    {
        public required string Appearance { get; set; }
        public required string Aroma { get; set; }
        public required string Taste { get; set; }
        public required int? Balance { get; set; }
        public required int? Bouquet { get; set; }
        public required int? Extractivity { get; set; }
        public required int? Tartness { get; set; }
        public required int? Viscosity { get; set; }
        public required int? Density { get; set; }
        public required List<string> Tastes { get; set; }
    }
}
