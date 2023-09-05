namespace TeaNotes.Auth.Controllers.Login
{
    public record LoginPayload
    {
        public required string NickName { get; set; }
        public required string Password { get; set; }
    }
}
