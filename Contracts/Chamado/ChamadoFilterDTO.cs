using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketHub.Models.Enums;

namespace TicketHub.Contracts.Chamado
{
    public record ChamadoFilterDTO(
        int? SetorId,
        int? PrioridadeId,
        ChamadoStatus? Status
    );
}