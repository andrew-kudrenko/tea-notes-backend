using MimeKit;
using TeaNotes.Users.Models;

namespace TeaNotes.Email
{
    public class RegisterEmailBuilder
    {
        private readonly IConfiguration _configuration;
        private readonly BodyBuilder _bodyBuilder = new();

        public RegisterEmailBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MimeMessage Create(User user, ConfirmationCode code)
        {
            _bodyBuilder.HtmlBody = CreateMessageBody(code.Code);

            var message = new MimeMessage()
            {
                Subject = $"[TeaNotes] Подтвердите e-mail адрес.",
                Body = _bodyBuilder.ToMessageBody(),
            };
            message.From.Add(MailboxAddress.Parse(_configuration["Email:Address"]!));
            message.To.Add(MailboxAddress.Parse(user.Email));

            return message;
        }

        private string CreateConfirmationLink(string code) => 
            $"{_configuration["Client:Url"]}/auth/confirm-email/${code}";

        private string CreateMessageBody(string code) => $@"
            <h5>Добро пожаловать на TeaNotes!</h5>
            <br>
            <br>
            <p>
                Для завершения процесса регистрации, пожалуйста, 
                перейдите по ссылке {CreateConfirmationLink(code)}.
            </p>
            <br>
            <br>
            <p>Хорошего обучения!</p>

            <p>
                Если вы не зарегистрировали аккаунт на TeaNotes, 
                пожалуйста, проигнорируйте это сообщение.
            </p>
            
            <br>
            <br>
            
            <p>
                С любовью, <br>
                Команда TeaNotes.
            </p>
        ";
    }
}
