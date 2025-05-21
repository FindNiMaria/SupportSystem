using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HelpdeskSystem.Models.User
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName("Nome")]
        public string FirstName { get; set; }

        [DisplayName("Sobrenome")]
        public string LastName { get;set; }

        [DisplayName("Genero")]
        public string Gender { get; set; }

        [Display(Name = "Telefone")]
        public override string PhoneNumber { get; set; }

        [Display(Name = "E-mail")]
        public override string Email { get; set; }

        [DisplayName("País")]
        public string Country { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Departamento")]
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        [DisplayName("Permissão")]
        public string? Role { get; set; } // "Admin", "Tecnico", "Coordenador", "Usuario"
        public string FullName => $"{FirstName} {LastName}";
    }
}
