using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using System.ComponentModel;

namespace HelpdeskSystem.ViewModels
{
    public class OSViewModel
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
        public OSSubCategory SubCategory { get; set; }

        [DisplayName("Anexo: ")]
        public String Attachment { get; set; }
        public List<OS> OS { get; set; }
        public OS OSDetails { get; set; }
        public List<OSComment> OSComments { get; set; }
        public OSComment OSComment { get; set; }
        public string OSDescription { get; set; }
        public List<OSResolution> OSResolutions { get; set; }
        public OSResolution Resolution { get; set; }
        public int? AssignedToId { get; set; }
        public ApplicationUser AssignedTo { get; set; }
        public DateTime? AssignedOn { get; set; }

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
