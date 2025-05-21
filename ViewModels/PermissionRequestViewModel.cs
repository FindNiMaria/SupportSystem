using System.ComponentModel.DataAnnotations;

namespace HelpdeskSystem.Models.ViewModels
{
    public class PermissionRequestViewModel
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(1000)]
        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}
