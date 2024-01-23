namespace TeaNotes.Notes.Models
{
    public record TeaNoteBrewing
    {
        public required string? Method { get; set; }
        public required string? Dishware { get; set; }
        public required int? Volume { get; set; }
        public required int? Temperature { get; set; }
        public required int? Quantity { get; set; }
    }
}
