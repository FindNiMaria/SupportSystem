using System;
using System.Linq;
using System.Collections.Generic;
using HelpdeskSystem.Data;
using HelpdeskSystem.Models;
using Microsoft.EntityFrameworkCore;

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

        private static readonly Dictionary<string, int> NiveisPrioridade = new()
        {
            { PRIORIDADE_BAIXA, 1 },
            { PRIORIDADE_MEDIA, 2 },
            { PRIORIDADE_ALTA, 3 },
            { PRIORIDADE_CRITICA, 4 }
        };

        public TicketPriorityJob(ApplicationDbContext context)
        {
            _context = context;
        }

        public void EscalarPrioridades()
        {
            var agora = DateTime.Now;

            var prioridades = _context.systemCodeDetails
                .Where(c => NiveisPrioridade.Keys.Contains(c.Code.ToUpper()))
                .ToDictionary(c => c.Code.ToUpper(), c => c.Id);

            var statusFechadoId = _context.systemCodeDetails
                .Where(c => c.Code.ToUpper() == STATUS_CLOSED)
                .Select(c => c.Id)
                .FirstOrDefault();

            var chamados = _context.Tickets
                .Where(t => t.StatusId != statusFechadoId)
                .ToList();

            var configuracoes = _context.SystemSettings
                .Where(s => s.Code.StartsWith("TKTRESOLUTION"))
                .ToDictionary(
                    s => s.Code.ToUpper(),
                    s => int.TryParse(s.Value, out int dias) ? dias * 24 : int.MaxValue // converte dias em horas
                );

            configuracoes.TryGetValue("TKTRESOLUTIONURGENT", out int horasCritica);
            configuracoes.TryGetValue("TKTRESOLUTIONHIGH", out int horasAlta);
            configuracoes.TryGetValue("TKTRESOLUTIONMEDIUM", out int horasMedia);
            configuracoes.TryGetValue("TKTRESOLUTIONLOW", out int horasBaixa); // opcional

            foreach (var chamado in chamados)
            {
                double horasInativo = chamado.ModifiedOn.HasValue
                    ? (agora - chamado.ModifiedOn.Value).TotalHours
                    : double.MaxValue;

                var prioridadeAtualCode = _context.systemCodeDetails
                    .Where(c => c.Id == chamado.PriorityId)
                    .Select(c => c.Code.ToUpper())
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(prioridadeAtualCode) || !NiveisPrioridade.ContainsKey(prioridadeAtualCode))
                    continue;

                int nivelAtual = NiveisPrioridade[prioridadeAtualCode];

                if (horasInativo >= horasCritica && nivelAtual < NiveisPrioridade[PRIORIDADE_CRITICA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_CRITICA];
                    _context.Entry(chamado).State = EntityState.Modified;
                }
                else if (horasInativo >= horasAlta && nivelAtual < NiveisPrioridade[PRIORIDADE_ALTA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_ALTA];
                    _context.Entry(chamado).State = EntityState.Modified;
                }
                else if (horasInativo >= horasMedia && nivelAtual < NiveisPrioridade[PRIORIDADE_MEDIA])
                {
                    chamado.PriorityId = prioridades[PRIORIDADE_MEDIA];
                    _context.Entry(chamado).State = EntityState.Modified;
                }
            }

            _context.SaveChanges();
        }

    }
}
