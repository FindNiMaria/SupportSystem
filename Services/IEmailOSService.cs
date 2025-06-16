namespace HelpdeskSystem.Services
{
    public interface IEmailOSService
    {
        Task ImportarEmailsComoOSAsync();
        Task EnviarEmailAsync(string para, string assunto, string mensagem);
    }
}
