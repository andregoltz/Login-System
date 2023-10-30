using Login_System.Core;
using Login_System.Core.Contexts.AccountContext.Entities;
using Login_System.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Login_System.Infra.Contexts.AccountContext.UseCases.Create
{
    public class Services : IService
    {
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            var client = new SendGridClient(Configuration.SendGrid.ApiKey);
            var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
            var subject = "Verifique sua conta";
            var to = new EmailAddress(user.Email, user.Name);
            var content = $"Código {user.Email.Verification.Code}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg, cancellationToken);

        }
    }
}
