using System.ComponentModel.DataAnnotations;
using TeaNotes.Common;

namespace TeaNotes.Auth.Controllers.Register
{
    public record RegisterPayload
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = ModelErrorMessages.StringLength)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public required string Nickname { get; set; }
        
        [EmailAddress(ErrorMessage = ModelErrorMessages.Email)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public required string Email { get; set; }

        [Compare(nameof(PasswordRepeat), ErrorMessage = ModelErrorMessages.PasswordsAreNotEqual)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = ModelErrorMessages.StringLength)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = ModelErrorMessages.PasswordsAreNotEqual)]
        [Required(ErrorMessage = ModelErrorMessages.Required)]
        public required string PasswordRepeat { get; set; }
    }
}
