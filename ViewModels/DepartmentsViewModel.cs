using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using System.Collections.Generic;

namespace HelpdeskSystem.ViewModels
{
    public class DepartmentViewModel
    {
        // Filtros
        public string Name { get; set; }
        public string Code { get; set; }

        // Lista de departamentos
        public List<Department> Departments { get; set; }
    }
}
