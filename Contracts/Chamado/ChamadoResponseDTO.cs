using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Models.Enums;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoResponseDTO(
        int Id,
        string? Titulo,
        string Descricao,
        ChamadoStatus Status,
        DateTime DataAbertura,
        DateTime? DataInicio,
        DateTime? DataFinal,
        string? Solucao,
        int SetorId,
        int PrioridadeId,
        string SetorNome,
        string PrioridadeNome,
        TimeSpan TempoTotalAtendimento,
        bool Atrasado);
}