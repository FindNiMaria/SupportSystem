using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class RolesViewModel
    {
        [DisplayName("Nº da Permissão")]
        public string Id { get; set; }

        [DisplayName("Permissão")]
        public string Permission { get; set; }
    }
}
