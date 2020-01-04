using Microsoft.AspNetCore.Http;
using PublicTimeAPI.Repository;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace PublicTimeAPI.Services
{
  public interface IEmailService
  {
    void RegisterMail(string toMail, string user);
  }

  public class EmailService : IEmailService
  {
    private IHttpContextAccessor httpContextAccessor;
    private ApplicationDbContext context;

    public EmailService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext context)
    {
      this.httpContextAccessor = httpContextAccessor;
      this.context = context;
    }

    public void RegisterMail(string toMail, string user)
    {
      MimeMessage message = new MimeMessage();

      MailboxAddress from = new MailboxAddress("Admin", "admin@nokol.net");
      message.From.Add(from);

      MailboxAddress to = new MailboxAddress(user, toMail);
      message.To.Add(to);

      message.Subject = "This is email subject";

      SmtpClient client = new SmtpClient();
      client.Connect("smtp_address_here", 8080, true);
      client.Authenticate("user_name_here", "pwd_here");
      client.Send(message);
      client.Disconnect(true);
      client.Dispose();
    }
  }
}