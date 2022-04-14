using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace TangyWeb_API.Helper
{
  public class EmailSender : IEmailSender
  {
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      try
      {
        var emailToSend = new MimeMessage();
        emailToSend.From.Add(MailboxAddress.Parse("testingapi231@gmail.com"));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Subject = subject;
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        // send email
        using var emailClient = new SmtpClient();
        emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        emailClient.Authenticate("testingapi231@gmail.com", "Doremon@123");
        emailClient.SendAsync(emailToSend);
        emailClient.Disconnect(true);

      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}
