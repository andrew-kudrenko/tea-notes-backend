using System.ComponentModel.DataAnnotations;

namespace TeaNotes.Auth.Controllers.Register
{
    public record RegisterPayload
    {
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина не менее 3 и не более 30 символов")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Nickname { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public required string Email { get; set; }

        [Compare("PasswordRepeat", ErrorMessage = "Пароли не совпадают")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Длина не менее 8 и не более 30 символов")]
        [Required]
        public required string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required]
        public required string PasswordRepeat { get; set; }
    }
}
