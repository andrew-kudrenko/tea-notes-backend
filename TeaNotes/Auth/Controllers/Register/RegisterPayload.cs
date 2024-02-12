﻿namespace TeaNotes.Auth.Controllers.Register
{
    public record RegisterPayload
    {
        public required string NickName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordRepeat { get; set; }
    }
}
