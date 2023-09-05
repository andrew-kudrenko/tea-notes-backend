namespace TeaNotes.Auth.Models
{
    public record AuthTokenPair
    {
        public required AuthToken Access { get; set; } 
        public required AuthToken Refresh { get; set; }
    }
}
