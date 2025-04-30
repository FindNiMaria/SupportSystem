namespace HelpdeskSystem.Services
{
    public interface IEmailTicketService
    {
        Task ImportarEmailsComoTicketsAsync();
        Task EnviarEmailAsync(string para, string assunto, string mensagem);
    }
}
