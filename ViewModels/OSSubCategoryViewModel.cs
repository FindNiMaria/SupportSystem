using HelpdeskSystem.Models.SO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class OSSubCategoryViewModel
    {
        // Filtros
        [DisplayName("Código")]
        public string Code { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        // Lista
        public List<OSSubCategory> OSSubCategories { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
