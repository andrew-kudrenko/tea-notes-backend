namespace TeaNotes.Notes.Models
{
    public record TeaNoteInfusion
    {
        public required string Appearance { get; set; }
        public required string Aroma { get; set; }
        public required string Taste { get; set; }
        public required double? Balance { get; set; }
        public required double? Bouquet { get; set; }
        public required double? Extractivity { get; set; }
        public required double? Tartness { get; set; }
        public required double? Viscosity { get; set; }
        public required double? Density { get; set; }
        public required List<string> Tastes { get; set; }
    }
}
