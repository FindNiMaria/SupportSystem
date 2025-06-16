using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using System.Collections.Generic;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class DepartmentViewModel
    {
        // Filtros

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Código")]
        public string Code { get; set; }

        // Lista de departamentos
        public List<Department> Departments { get; set; }
    }
}
