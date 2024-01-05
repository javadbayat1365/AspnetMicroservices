using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructures;
using Ordering.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Email;

public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }
    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> settings, ILogger<EmailService> logger)
    {
        _emailSettings = settings.Value;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(Application.Models.Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subject = email.Subject;
        var body = email.Body;
        var to = new EmailAddress(email.To);

        var from = new EmailAddress() {

            Email = _emailSettings.FromAddress,
             Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from,to, subject,body,body);
        var response = await client.SendEmailAsync(sendGridMessage);


        if (response.StatusCode == System.Net.HttpStatusCode.Accepted
            || response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation($"Email Sent to {to}");
            return true;
        }

        _logger.LogError($"Failed to Sent Email to {to}");
        return false;

    }
}
