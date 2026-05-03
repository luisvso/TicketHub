using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Contracts.Prioridade
{
    public record PrioridadeResponseDTO(
        int Id,
        string Nome,
        int HorasEstimadas
    );
}