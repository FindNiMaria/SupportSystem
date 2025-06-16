using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class TicketAssignmentInputModel
{
    [Required]
    [DisplayName("Nº")]
    public int Id { get; set; } // Id do chamado

    [Required]
    [DisplayName("Atribuído a")]
    public int AssignedToId { get; set; } // Técnico selecionado (GUID)

    [Required]
    public string TicketDescription { get; set; } // Observação final

    public List<Comment> TicketComments { get; set; } = new();
    public List<TicketResolution> TicketResolutions { get; set; } = new();
    public TicketResolution Resolution { get; set; }
    public Ticket TicketDetails { get; set; }
}

