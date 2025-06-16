using HelpdeskSystem.Models.System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class SystemCodeDetailViewModel
    {
        // Filtros

        [DisplayName("Código")]
        public string Code { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Código Pai")]
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

