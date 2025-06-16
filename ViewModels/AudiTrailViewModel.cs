using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class AuditTrailViewModel
    {
        // Filtros

        [DisplayName("Módulo")]
        public string Module { get; set; }

        [DisplayName("Tabela Afetada")]
        public string AffectedTable { get; set; }

        [DisplayName("Data")]
        public DateTime TimeStamp { get; set; }

        [DisplayName("Ação")]
        public string Action { get; set; }

        [DisplayName("Id do Usuario")]
        public string UserId { get; set; }

        [DisplayName("De")]
        public DateTime? From { get; set; }

        [DisplayName("Até")]
        public DateTime? To { get; set; }

        [DisplayName("IP")]
        public string IpAdress { get; set; }

        [DisplayName("Usuário")]
        public ApplicationUser User { get; set; }

        // Lista
        public List<AuditTrail> Logs { get; set; }

        // Dropdowns
        [DisplayName("Usuário")]
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
