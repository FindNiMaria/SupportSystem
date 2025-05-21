using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpdeskSystem.Models.SO
{
    public class OS : UserActivity
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

        [DisplayName("Categoria: ")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public TicketCategory Category { get; set; }

        [DisplayName("Sub-Categoria: ")]
        public int? SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public TicketSubCategory SubCategory { get; set; }

        [DisplayName("Anexo: ")]
        public string? Attachment { get; set; }

        [DisplayName("Atribuído a")]
        public string? AssignedToId { get; set; }

        [ForeignKey("AssignedToId")]
        public ApplicationUser AssignedTo { get; set; }

        [DisplayName("Atribuído em")]
        public DateTime? AssignedOn { get; set; }

        public ICollection<OSComment> OSComments { get; set; }

        [DisplayName("Duração")]
        public int? OSDuration
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
    }
}
