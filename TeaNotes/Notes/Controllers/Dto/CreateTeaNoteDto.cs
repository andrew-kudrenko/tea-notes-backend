namespace TeaNotes.Notes.Controllers.Dto
{
    public record CreateTeaNoteDto
    {
        public required string Title { get; set; }
        public required string Taste { get; set; }
        public required string Aroma { get; set; }
        public required string Temperature { get; set; }
        public required string Feeling { get; set; }
        public required List<string> Tastes { get; set; }
    }
}
