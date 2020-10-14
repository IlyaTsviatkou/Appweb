using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace Appweb.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "ilya.tsv@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync("ilya.tsv@mail.ru", "Ii07022001");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}