using HelpdeskSystem.Models.Ticket;
using System.ComponentModel;
using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpdeskSystem.ViewModels
{
    public class TicketViewModel : UserActivity
    {
        [DisplayName("Nº")]
        public int Id { get; set; }

        [DisplayName("Título")]
        public string Title { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail Status { get; set; }

        [DisplayName("Prioridade")]
        public int PriorityId { get; set; }
        public SystemCodeDetail Priority { get; set; }

        [DisplayName("Criado por:")]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        [DisplayName("Criado Em:")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        [DisplayName("Sub-Categoria")]
        public int SubCategoryId { get; set; }
        public TicketSubCategory SubCategory { get; set; }

        [DisplayName("Anexo: ")]
        public String Attachment { get; set; }
        public List<TicketViewModel> Tickets { get; set; }
        public Ticket TicketDetails { get; set; }
        public Comment TicketComment { get; set; }
        public string TicketDescription { get; set; }
        public List<Comment> TicketComments { get; set; } = new();
        public List<TicketResolution> TicketResolutions { get; set; } = new();
        public TicketResolution Resolution { get; set; }
        public string? AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public DateTime? AssignedOn { get; set; }


        [DisplayName("Duração")]
        public int? TicketDuration
        {
            get
            {
                if (CreatedOn == null)
                {
                    return null;
                }
                DateTime now = DateTime.UtcNow;

                TimeSpan difference = now.Subtract(CreatedOn);

                return difference.Days;
            }
        }
        public string? TechnicianId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Priorities { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public IEnumerable<SelectListItem> Technicians { get; set; }
    }
}

