
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ASM1670.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic gui email
            return Task.CompletedTask;
        }
    }
}
