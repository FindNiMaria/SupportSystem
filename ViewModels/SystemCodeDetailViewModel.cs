using HelpdeskSystem.Models.System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class SystemCodeDetailViewModel
    {
        // Filtros
        public string Code { get; set; }
        public string Description { get; set; }
        public int? SystemCodeId { get; set; }
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Ordem")]
        public int? OrderNo { get; set; }

        [DisplayName("Código de Sistema")]
        public SystemCode SystemCode { get; set; }
        // Lista
        public List<SystemCodeDetail> Details { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> SystemCodes { get; set; }
    }
}

