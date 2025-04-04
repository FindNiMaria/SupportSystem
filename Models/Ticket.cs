﻿using System.ComponentModel;

namespace HelpdeskSystem.Models
{
    public class Ticket
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Prioridade")]
        public string Prioridade { get; set; }

        [DisplayName("Criado por:")]
        public string CriadoPorId { get; set; }
        public ApplicationUser CriadoPor { get; set; }

        [DisplayName("Criado Em:")]
        public DateTime CriadoEm { get; set; }

        [DisplayName("Sub-Categoria")]
        public int? SubCategoryId { get; set; }
        public TicketSubCategory SubCategory { get; set;}
    }
}
