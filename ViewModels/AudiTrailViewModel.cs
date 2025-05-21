using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class AuditTrailViewModel
    {
        // Filtros
        public string Module { get; set; }
        public string AffectedTable { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Action { get; set; }
        public string UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string IpAdress { get; set; }

        public ApplicationUser User { get; set; }

        // Lista
        public List<AuditTrail> Logs { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Users { get; set; }
    }
}
