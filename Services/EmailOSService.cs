using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using MailKit;
using MailKit.Net.Smtp;
using HelpdeskSystem.Models.SO;
using System.Security.Claims;

namespace HelpdeskSystem.Services
{
    public class EmailOSService : IEmailOSService
    {
        private readonly EmailSettings _settings;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EmailOSService(EmailSettings settings, ApplicationDbContext context, IConfiguration configuration)
        {
            _settings = settings;
            _context = context;
            _configuration = configuration;
        }
        public async Task EnviarEmailAsync(string para, string assunto, string mensagem)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(
                _configuration["EmailSettings:SenderName"],
                _configuration["EmailSettings:SenderEmail"]
            ));
            email.To.Add(MailboxAddress.Parse(para));
            email.Subject = assunto;
            email.Body = new TextPart("plain") { Text = mensagem };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(
                _configuration["EmailSettings:SmtpServer"],
                int.Parse(_configuration["EmailSettings:SmtpPort"]),
                MailKit.Security.SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(
                _configuration["EmailSettings:Username"],
                _configuration["EmailSettings:Password"]
            );
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task ImportarEmailsComoOSAsync()
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            using var client = new ImapClient();
            await client.ConnectAsync(emailSettings.ImapServer, emailSettings.ImapPort, SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(emailSettings.Username, emailSettings.Password);

            var inbox = client.Inbox;
            await inbox.OpenAsync(FolderAccess.ReadWrite);

            for (int i = 0; i < inbox.Count; i++)
            {
                var message = await inbox.GetMessageAsync(i);

                var emailRemetente = message.From.Mailboxes.FirstOrDefault()?.Address;
                if (string.IsNullOrWhiteSpace(emailRemetente))
                    continue;

                var usuario = await _context.Users.FirstOrDefaultAsync(u => u.Email == emailRemetente);
                if (usuario == null)
                    continue;

                var statusPendente = await _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .FirstOrDefaultAsync(x => x.SystemCode.Code == "STS" && x.Code == "PND");

                var prioridadeDefault = await _context.systemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "PRD")
                    .OrderBy(x => x.Id)
                    .FirstOrDefaultAsync();

                var novaOS = new OS
                {
                    Title = message.Subject ?? "Sem Assunto",
                    Description = message.TextBody ?? message.HtmlBody ?? "Sem conteúdo",
                    CreatedById = usuario.Id,
                    CreatedOn = DateTime.Now,
                    StatusId = statusPendente?.Id ?? 0,
                    PriorityId = prioridadeDefault?.Id ?? 0,
                    CategoryId = _context.OSCategories.Select(c => c.Id).FirstOrDefault(), // usa a primeira categoria como padrão
                };

                _context.OS.Add(novaOS);

                // Marca o e-mail como lido
                await inbox.AddFlagsAsync(i, MessageFlags.Seen, true);
            }

            await _context.SaveChangesAsync();
            await client.DisconnectAsync(true);
        }

    }


}

