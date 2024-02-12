using MimeKit;
using MailKit.Net.Smtp;

namespace TeaNotes.Email
{
    public class EmailClient
    {
        public MailboxAddress Address { get; private set; }

        private readonly SmtpClient _client;
        private readonly IConfiguration _configuration;

        private EmailClient(IConfiguration configuration) 
        {
            _configuration = configuration;
            Address = MailboxAddress.Parse(_configuration["Email:Address"]!);

            _client = new SmtpClient();
        }

        public Task SendAsync(MimeMessage message) => _client.SendAsync(message);

        private async Task InitAsync()
        {
            await _client.ConnectAsync(
                _configuration["Email:Host"], 
                int.Parse(_configuration["Email:Port"]!)
            );

            await _client.AuthenticateAsync(
                _configuration["Email:Address"], 
                _configuration["Email:Password"]
            );
        }

        public static async Task<EmailClient> Create(IConfiguration configuration)
        {
            var client = new EmailClient(configuration);
            await client.InitAsync();
            return client;
        }
    }
}
