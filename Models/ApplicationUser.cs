using Microsoft.AspNetCore.Identity;

namespace HelpdeskSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get;set; }
        public string Genero { get; set; }
        public string Pais { get; set; }
        public string Cidade { get; set; }
        public string FullName => $"{PrimeiroNome} {Sobrenome}";
    }
}
