using MailKit.Net.Smtp;
using MailKit.Security;

using Microsoft.Extensions.Options;

using MimeKit;

using ODS.HelperModels;

using Org.BouncyCastle.Asn1.Ocsp;

namespace ODS.Services
{
    public class MailService : IMailService
    {
        private readonly MailConfiguration _config;
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailConfiguration> config, ILogger<MailService> logger)
        {
            _config = config.Value;
            _logger = logger;
        }
        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = new MailboxAddress(_config.DisplayName, request.From ?? _config.From),
                    Subject = request.Subject,
                    Body = new BodyBuilder
                    {
                        HtmlBody = request.Body
                    }.ToMessageBody()
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.UserName, _config.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }     
    }
}
