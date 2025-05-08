using System;
using System.Linq;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using HelpdeskSystem.Jobs;

namespace HelpdeskSystem.Jobs
{
    public class TicketPriorityJob
    {
        private readonly ApplicationDbContext _context;

        private const string STATUS_CLOSED = "CLO";
        private const string PRIORIDADE_BAIXA = "LOW";
        private const string PRIORIDADE_MEDIA = "MED";
        private const string PRIORIDADE_ALTA = "ALT";
        private const string PRIORIDADE_CRITICA = "URG";

        public TicketPriorityJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public void EscalarPrioridades()
        {
            var agora = DateTime.Now;

            // Buscar os IDs das prioridades
            var prioridades = _context.systemCodeDetails
                .Where(c => new[] { PRIORIDADE_BAIXA, PRIORIDADE_MEDIA, PRIORIDADE_ALTA, PRIORIDADE_CRITICA }.Contains(c.Code.ToUpper()))
                .ToDictionary(c => c.Code.ToUpper(), c => c.Id);

            // Buscar ID do status "CLOSED"
            var statusFechadoId = _context.systemCodeDetails
                .Where(c => c.Code.ToUpper() == STATUS_CLOSED)
                .Select(c => c.Id)
                .FirstOrDefault();

            var chamados = _context.Tickets
                .Where(t => t.StatusId != statusFechadoId)
                .ToList();

            foreach (var chamado in chamados)
            {
                double horasInativo;

                if (chamado.ModifiedOn.HasValue)
                {
                    horasInativo = (agora - chamado.ModifiedOn.Value).TotalHours;
                }
                else
                {
                    // Se não tiver ModifiedOn, trata como inativo há muito tempo
                    horasInativo = double.MaxValue;
                }

                if (horasInativo >= 72 && chamado.PriorityId != prioridades[PRIORIDADE_CRITICA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_CRITICA];
                }
                else if (horasInativo >= 48 && chamado.PriorityId != prioridades[PRIORIDADE_ALTA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_ALTA];
                }
                else if (horasInativo >= 24 && chamado.PriorityId != prioridades[PRIORIDADE_MEDIA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_MEDIA];
                }
            }

            _context.SaveChanges();
        }
    }
}
