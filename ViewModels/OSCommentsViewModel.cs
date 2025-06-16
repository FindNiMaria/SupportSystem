using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class OSCommentsViewModel : UserActivity
    {
        // Filtros
        [DisplayName("Nº")]
        public int? OSId { get; set; }

        [DisplayName("Criado por")]
        public string CreatedById { get; set; }

        [DisplayName("Palavra-chave")]
        public string DescriptionKeyword { get; set; }

        [DisplayName("Criado de")]
        public DateTime? CreatedFrom { get; set; }

        [DisplayName("Até")]
        public DateTime? CreatedTo { get; set; }

        // Lista de comentários
        public List<OSComment> Comments { get; set; }

        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }
        public OS OS { get; set; }
        // Dropdowns
        public IEnumerable<SelectListItem> Ordens { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
